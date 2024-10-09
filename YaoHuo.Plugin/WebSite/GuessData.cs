using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using KeLin.ClassManager;
using System.Linq;
using System.Transactions;

namespace YaoHuo.Plugin.WebSite
{
    public class GuessData
    {
        public long Id { get; set; }
        public long BbsId { get; set; }
        public string Title { get; set; }
        public List<GuessingOption> Options { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsClosed { get; set; }
        public int? ResultOptionId { get; set; }
        public int? WinningOptionId { get; set; }
        public string WinningOptionText { get; set; }

    }

    public class GuessingOption
    {
        public string Text { get; set; }
        public int Amount { get; set; }
        public bool IsWinner { get; set; }
    }

    public class BetInfo
    {
        public long UserId { get; set; }
        public int OptionId { get; set; }
        public decimal Amount { get; set; }
    }

    public class GuessManager
    {
        private string connString;

        public GuessManager(string instanceName)
        {
            connString = PubConstant.GetConnectionString(instanceName);
        }

        public long AddGuessing(long bbsId, string title, string options, DateTime deadline)
        {
            try
            {
                string sql = @"
                    INSERT INTO bbs_guessing (bbs_id, title, options, deadline, is_closed, created_at)
                    VALUES (@BbsId, @Title, @Options, @Deadline, 0, @CreatedAt);
                    SELECT SCOPE_IDENTITY();";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@BbsId", bbsId),
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Options", options),
                    new SqlParameter("@Deadline", deadline),
                    new SqlParameter("@CreatedAt", DateTime.Now)
                };

                DataAccess dataAccess = new DataAccess(connString);
                object result = dataAccess.ExecuteScalar(sql, parameters);

                return result != null && result != DBNull.Value ? Convert.ToInt64(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"AddGuessing error: {ex.Message}", ex);
            }
        }

        public GuessData GetGuessingById(long guessId)
        {
            try
            {
                string sql = @"
                    SELECT id, bbs_id, title, options, deadline, is_closed, result_option_id
                    FROM bbs_guessing
                    WHERE id = @GuessId";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@GuessId", guessId)
                };

                DataAccess dataAccess = new DataAccess(connString);
                using (SqlDataReader reader = dataAccess.ExecuteReader(sql, parameters))
                {
                    if (reader.Read())
                    {
                        var options = JsonConvert.DeserializeObject<List<GuessingOption>>(reader.GetString(3));
                        bool isResultOptionIdNull = reader.IsDBNull(6);
                        int? resultOptionId = isResultOptionIdNull ? (int?)null : reader.GetInt32(6);

                        System.Diagnostics.Debug.WriteLine($"IsResultOptionIdNull: {isResultOptionIdNull}");
                        System.Diagnostics.Debug.WriteLine($"ResultOptionId from DB: {resultOptionId}");

                        var guessData = new GuessData
                        {
                            Id = reader.GetInt64(0),
                            BbsId = reader.GetInt64(1),
                            Title = reader.GetString(2),
                            Options = options,
                            Deadline = reader.GetDateTime(4),
                            IsClosed = reader.GetBoolean(5),
                            ResultOptionId = resultOptionId
                        };

                        // 设置新的属性
                        if (resultOptionId.HasValue && options != null && options.Count >= resultOptionId.Value)
                        {
                            guessData.WinningOptionId = resultOptionId.Value;
                            guessData.WinningOptionText = options[resultOptionId.Value - 1].Text;
                            System.Diagnostics.Debug.WriteLine($"Winning Option: [{guessData.WinningOptionId}] {guessData.WinningOptionText}");
                        }
                        else
                        {
                            guessData.WinningOptionId = null;
                            guessData.WinningOptionText = null;
                            System.Diagnostics.Debug.WriteLine("Winning Option: 未设置");
                        }

                        return guessData;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetGuessingById error: {ex.Message}", ex);
            }
        }

        public bool HasUserBet(long guessId, long userId)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM bbs_guessing_bets
                    WHERE guessing_id = @GuessId AND userid = @UserId";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@GuessId", guessId),
                    new SqlParameter("@UserId", userId)
                };

                DataAccess dataAccess = new DataAccess(connString);
                int count = Convert.ToInt32(dataAccess.ExecuteScalar(sql, parameters));

                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"HasUserBet error: {ex.Message}", ex);
            }
        }

        public bool UpdateGuessingVote(long guessId, string selectedOption, int betAmount, long userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 首先检查竞猜是否已结束
                            string checkSql = @"
                                SELECT is_closed, deadline
                                FROM bbs_guessing
                                WHERE id = @GuessId";
                            SqlCommand checkCmd = new SqlCommand(checkSql, conn, transaction);
                            checkCmd.Parameters.AddWithValue("@GuessId", guessId);
                            using (SqlDataReader reader = checkCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bool isClosed = reader.GetBoolean(0);
                                    DateTime deadline = reader.GetDateTime(1);

                                    if (isClosed)
                                    {
                                        throw new Exception("竞猜已结束，无法继续下注");
                                    }

                                    if (DateTime.Now > deadline)
                                    {
                                        throw new Exception("截止时间已到，无法继续下注");
                                    }
                                }
                                else
                                {
                                    throw new Exception("未找到指定的竞猜");
                                }
                            }

                            string selectSql = @"
                                SELECT options
                                FROM bbs_guessing
                                WHERE id = @GuessId";
                            SqlCommand selectCmd = new SqlCommand(selectSql, conn, transaction);
                            selectCmd.Parameters.AddWithValue("@GuessId", guessId);
                            string optionsJson = (string)selectCmd.ExecuteScalar();

                            List<GuessingOption> options = JsonConvert.DeserializeObject<List<GuessingOption>>(optionsJson);
                            int selectedOptionIndex = options.FindIndex(o => o.Text == selectedOption);

                            System.Diagnostics.Debug.WriteLine($"UpdateGuessingVote - selectedOption: {selectedOption}, selectedOptionIndex: {selectedOptionIndex}");

                            if (selectedOptionIndex == -1)
                            {
                                throw new Exception("选项不存在");
                            }

                            options[selectedOptionIndex].Amount += betAmount;
                            string updatedOptionsJson = JsonConvert.SerializeObject(options);

                            string updateSql = @"
                                UPDATE bbs_guessing
                                SET options = @Options
                                WHERE id = @GuessId";
                            SqlCommand updateCmd = new SqlCommand(updateSql, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@Options", updatedOptionsJson);
                            updateCmd.Parameters.AddWithValue("@GuessId", guessId);
                            updateCmd.ExecuteNonQuery();

                            string insertBetSql = @"
                                INSERT INTO bbs_guessing_bets (guessing_id, userid, option_id, amount)
                                VALUES (@GuessingId, @UserId, @OptionId, @Amount)";
                            SqlCommand insertBetCmd = new SqlCommand(insertBetSql, conn, transaction);
                            insertBetCmd.Parameters.AddWithValue("@GuessingId", guessId);
                            insertBetCmd.Parameters.AddWithValue("@UserId", userId);
                            insertBetCmd.Parameters.AddWithValue("@OptionId", selectedOptionIndex + 1); // 将索引转换为从1开始的ID
                            insertBetCmd.Parameters.AddWithValue("@Amount", betAmount);
                            insertBetCmd.ExecuteNonQuery();

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"UpdateGuessingVote error: {ex.Message}", ex);
            }
        }

        public List<BetInfo> GetBetsForGuessing(long guessingId)
        {
            List<BetInfo> bets = new List<BetInfo>();
            string sql = @"
                SELECT userid, option_id, amount
                FROM bbs_guessing_bets
                WHERE guessing_id = @GuessingId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@GuessingId", guessingId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            using (SqlDataReader reader = dataAccess.ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    bets.Add(new BetInfo
                    {
                        UserId = reader.GetInt64(0),
                        OptionId = reader.GetInt32(1),
                        Amount = reader.GetDecimal(2)
                    });
                }
            }
            return bets;
        }

        public void UpdateGuessingResult(long guessingId, int winningOptionId)
        {
            string sql = @"UPDATE [bbs_guessing] 
                           SET [result_option_id] = @winningOptionId, 
                               [is_closed] = 1, 
                               [updated_at] = GETDATE() 
                           WHERE [id] = @guessingId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@winningOptionId", winningOptionId),
                new SqlParameter("@guessingId", guessingId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            dataAccess.ExecuteNonQuery(sql, parameters);

            System.Diagnostics.Debug.WriteLine($"UpdateGuessingResult - guessingId: {guessingId}, winningOptionId: {winningOptionId}");
        }

        public void UpdateUserMoney(long userId, int amount, string siteId)
        {
            string sql = @"
                UPDATE [user]
                SET [money] = [money] + @Amount
                WHERE userid = @UserId AND siteid = @SiteId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@SiteId", siteId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            dataAccess.ExecuteNonQuery(sql, parameters);
        }

        public long GetAuthorIdByBbsId(long bbsId)
        {
            string sql = "SELECT book_pub FROM wap_bbs WHERE id = @BbsId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@BbsId", bbsId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            object result = dataAccess.ExecuteScalar(sql, parameters);

            return result != null ? Convert.ToInt64(result) : 0;
        }

        public void SendSystemMessage(string siteId, long userId, string title, string content)
        {
            string sql = @"
                INSERT INTO wap_message (siteid, userid, nickname, title, content, touserid, issystem)
                VALUES (@SiteId, @SiteId, '系统消息', @Title, @Content, @UserId, 1)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@SiteId", siteId),
                new SqlParameter("@Title", title),
                new SqlParameter("@Content", content),
                new SqlParameter("@UserId", userId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            dataAccess.ExecuteNonQuery(sql, parameters);
        }

        public GuessData GetGuessingByBbsId(long guessId)
        {
            try
            {
                string sql = @"
                    SELECT id, bbs_id, title, options, deadline, is_closed, result_option_id
                    FROM bbs_guessing
                    WHERE bbs_id = @BbsId";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@BbsId", guessId)
                };

                DataAccess dataAccess = new DataAccess(connString);
                using (SqlDataReader reader = dataAccess.ExecuteReader(sql, parameters))
                {
                    if (reader.Read())
                    {
                        var options = JsonConvert.DeserializeObject<List<GuessingOption>>(reader.GetString(3));
                        bool isResultOptionIdNull = reader.IsDBNull(6);
                        int? resultOptionId = isResultOptionIdNull ? (int?)null : reader.GetInt32(6);

                        System.Diagnostics.Debug.WriteLine($"IsResultOptionIdNull: {isResultOptionIdNull}");
                        System.Diagnostics.Debug.WriteLine($"ResultOptionId from DB: {resultOptionId}");

                        var guessData = new GuessData
                        {
                            Id = reader.GetInt64(0),
                            BbsId = guessId,
                            Title = reader.GetString(2),
                            Options = options,
                            Deadline = reader.GetDateTime(4),
                            IsClosed = reader.GetBoolean(5),
                            ResultOptionId = resultOptionId
                        };

                        // 设置新的属性
                        if (resultOptionId.HasValue && options != null && options.Count >= resultOptionId.Value)
                        {
                            guessData.WinningOptionId = resultOptionId.Value;
                            guessData.WinningOptionText = options[resultOptionId.Value - 1].Text;
                            System.Diagnostics.Debug.WriteLine($"Winning Option: [{guessData.WinningOptionId}] {guessData.WinningOptionText}");
                        }
                        else
                        {
                            guessData.WinningOptionId = null;
                            guessData.WinningOptionText = null;
                            System.Diagnostics.Debug.WriteLine("Winning Option: 未设置");
                        }

                        return guessData;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetGuessingByBbsId error: {ex.Message}", ex);
            }
        }

        // 添加新方法
        public BetInfo GetUserBet(long guessId, long userId)
        {
            string sql = @"
                SELECT option_id, amount
                FROM bbs_guessing_bets
                WHERE guessing_id = @GuessId AND userid = @UserId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@GuessId", guessId),
                new SqlParameter("@UserId", userId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            using (SqlDataReader reader = dataAccess.ExecuteReader(sql, parameters))
            {
                if (reader.Read())
                {
                    return new BetInfo
                    {
                        UserId = userId,
                        OptionId = reader.GetInt32(0),
                        Amount = reader.GetDecimal(1)
                    };
                }
            }
            return null;
        }

        public void UpdateGuessingResultWithTransaction(long guessingId, int winningOptionId, Action<long, int, string, string, long, long, string> saveBankLogAction, string siteId, string adminUserId, string adminNickname)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    // 1. 更新竞猜结果
                    UpdateGuessingResult(guessingId, winningOptionId);

                    // 2. 获取所有下注信息
                    List<BetInfo> bets = GetBetsForGuessing(guessingId);

                    // 3. 计算总下注金额和获胜选项的下注金额
                    decimal totalBetAmount = bets.Sum(b => b.Amount);
                    decimal winningBetAmount = bets.Where(b => b.OptionId == winningOptionId).Sum(b => b.Amount);

                    // 4. 获取竞猜数据以获取获胜选项文本
                    GuessData guessData = GetGuessingById(guessingId);
                    string winningOptionText = guessData.Options[winningOptionId - 1].Text;

                    foreach (var bet in bets.Where(b => b.OptionId == winningOptionId))
                    {
                        decimal reward = (bet.Amount / winningBetAmount) * totalBetAmount;
                        int rewardAmount = (int)Math.Floor(reward);

                        // 计算税后奖励金额
                        int taxAmount = (int)Math.Floor(rewardAmount * 0.1m); // 10% 税
                        int afterTaxRewardAmount = rewardAmount - taxAmount;

                        // 5. 更新用户金币
                        UpdateUserMoney(bet.UserId, afterTaxRewardAmount, siteId);

                        // 6. 记录银行日志
                        saveBankLogAction(bet.UserId, afterTaxRewardAmount, adminUserId, adminNickname, guessData.BbsId, guessingId, winningOptionText);

                        // 7. 发送消息通知
                        string messageTitle = "恭喜您在竞猜中获胜！";
                        string messageContent = $"您在帖子[<a href=\"/bbs-{guessData.BbsId}.html\">{guessData.BbsId}</a>]的竞猜中选择了正确选项[{winningOptionText}]，获得奖励{afterTaxRewardAmount}妖晶！";
                        SendSystemMessage(siteId, bet.UserId, messageTitle, messageContent);
                    }

                    // 提交事务
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // 记录错误日志
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateGuessingResultWithTransaction: {ex.Message}");
                    // 事务会自动回滚
                    throw;
                }
            }
        }

        public (bool IsClosed, bool IsDeadlinePassed) CheckGuessingStatus(long guessId)
        {
            string sql = @"
                SELECT is_closed, deadline
                FROM bbs_guessing
                WHERE id = @GuessId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@GuessId", guessId)
            };

            DataAccess dataAccess = new DataAccess(connString);
            using (SqlDataReader reader = dataAccess.ExecuteReader(sql, parameters))
            {
                if (reader.Read())
                {
                    bool isClosed = reader.GetBoolean(0);
                    DateTime deadline = reader.GetDateTime(1);
                    bool isDeadlinePassed = DateTime.Now > deadline;

                    return (isClosed, isDeadlinePassed);
                }
            }

            throw new Exception("未找到指定的竞猜");
        }
    }

    public class DataAccess
    {
        private string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int ExecuteNonQuery(string sql, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string sql, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public SqlDataReader ExecuteReader(string sql, List<SqlParameter> parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}