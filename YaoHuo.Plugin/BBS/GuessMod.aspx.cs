using KeLin.ClassManager;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class GuessMod : MyPageWap
    {
        protected Label lblGuessTitle;
        protected Label lblDeadline;
        protected DropDownList ddlOptions;
        protected Button btnSubmit;
        protected HiddenField hfGuessingId;
        protected Label lblResult;
        protected PlaceHolder phOptions;
        private long guessingId;
        private long bbsId;
        private GuessData guessData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!long.TryParse(Request.QueryString["id"], out guessingId) || guessingId == 0)
                {
                    ShowTipInfo("无效的竞猜ID", "");
                    return;
                }

                ViewState["guessingId"] = guessingId;
                if (!LoadGuessingInfo())
                {
                    ShowTipInfo("无法找到指定的竞猜数据", "");
                    return;
                }

                // 使用新方法检查竞猜状态
                GuessManager guessManager = new GuessManager(PubConstant.GetAppString("InstanceName"));
                var (isClosed, isDeadlinePassed) = guessManager.CheckGuessingStatus(guessingId);

                if (isClosed)
                {
                    ShowTipInfo("该竞猜已结束，无法修改结果", $"bbs-{guessData.BbsId}.html");
                    return;
                }

                if (!isDeadlinePassed)
                {
                    ShowTipInfo("竞猜还未结束，无法公布结果", $"bbs-{guessData.BbsId}.html");
                    return;
                }

                long authorId = GetAuthorIdByBbsId(guessData.BbsId);
                if (!IsAuthor(authorId))
                {
                    ShowTipInfo("您没有权限更新此竞猜结果", $"bbs-{guessData.BbsId}.html");
                    return;
                }
            }
            else
            {
                if (ViewState["guessingId"] != null)
                {
                    guessingId = (long)ViewState["guessingId"];
                    if (!LoadGuessingInfo())
                    {
                        ShowTipInfo("无法找到指定的竞猜数据", "");
                        return;
                    }
                }
                else
                {
                    ShowTipInfo("无法找到指定的竞猜数据", "");
                    return;
                }
            }

            // 添加这行调试信息
            System.Diagnostics.Debug.WriteLine($"Page_Load - ddlOptions.SelectedValue: {ddlOptions.SelectedValue}");
        }

        private bool LoadGuessingInfo()
        {
            try
            {
                string instanceName = PubConstant.GetAppString("InstanceName");
                GuessManager guessManager = new GuessManager(instanceName);
                guessData = guessManager.GetGuessingById(guessingId);

                if (guessData == null)
                {
                    return false;
                }

                bbsId = guessData.BbsId;
                lblGuessTitle.Text = guessData.Title;
                lblDeadline.Text = guessData.Deadline.ToString("yyyy-MM-dd HH:mm");

                if (guessData.IsClosed)
                {
                    phOptions.Visible = false;
                    lblResult.Visible = true;
                }
                else
                {
                    phOptions.Visible = true;
                    lblResult.Visible = false;

                    if (!IsPostBack)
                    {
                        ddlOptions.DataSource = guessData.Options.Select((option, index) => new { Id = index + 1, Text = option.Text }).ToList();
                        ddlOptions.DataTextField = "Text";
                        ddlOptions.DataValueField = "Id";
                        ddlOptions.DataBind();
                    }
                }

                // 添加调试信息
                System.Diagnostics.Debug.WriteLine($"LoadGuessingInfo called. IsPostBack: {IsPostBack}, IsClosed: {guessData.IsClosed}");
                System.Diagnostics.Debug.WriteLine($"ddlOptions.Items.Count: {ddlOptions.Items.Count}");
                foreach (ListItem item in ddlOptions.Items)
                {
                    System.Diagnostics.Debug.WriteLine($"Option: Value = {item.Value}, Text = {item.Text}");
                }
                System.Diagnostics.Debug.WriteLine($"ddlOptions.SelectedValue: {ddlOptions.SelectedValue}");

                return true;
            }
            catch (Exception ex)
            {
                // 记录异常，但不显示给用户
                System.Diagnostics.Debug.WriteLine($"LoadGuessingInfo error: {ex.Message}");
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ViewState["SelectedOptionValue"] = ddlOptions.SelectedValue;
            try
            {
                if (guessData == null)
                {
                    LoadGuessingInfo();
                    if (guessData == null)
                    {
                        ShowTipInfo("无法加载竞猜数据，请刷新页面重试", Request.Url.ToString());
                        return;
                    }
                }

                if (string.IsNullOrEmpty(ddlOptions.SelectedValue))
                {
                    ShowTipInfo("请选择获胜选项", Request.Url.ToString());
                    return;
                }

                if (guessData.IsClosed)
                {
                    ShowTipInfo("该竞猜已经关闭，无法更新结果", Request.Url.ToString());
                    return;
                }

                long authorId = GetAuthorIdByBbsId(guessData.BbsId);
                if (!IsAuthor(authorId))
                {
                    ShowTipInfo("您没有权限更新此竞猜结果", $"bbs/book_view.aspx?siteid={siteid}&classid={classid}&id={guessData.BbsId}");
                    return;
                }

                int winningOptionId = int.Parse(ddlOptions.SelectedValue);

                string instanceName = PubConstant.GetAppString("InstanceName");
                GuessManager guessManager = new GuessManager(instanceName);

                // 使用事务更新竞猜结果
                guessManager.UpdateGuessingResultWithTransaction(guessingId, winningOptionId, SaveBankLog, siteid, this.userid, this.userVo.nickname);

                Response.Redirect("/bbs-" + bbsId + ".html");
            }
            catch (Exception ex)
            {
                ShowTipInfo("更新结果时发生错误：" + ex.Message, Request.Url.ToString());
            }
        }

        // 新增方法，用于在事务中调用 SaveBankLog
        private void SaveBankLog(long userId, int amount, string adminUserId, string adminNickname, long bbsId, long guessingId, string winningOptionText)
        {
            base.SaveBankLog(userId.ToString(), "竞猜获胜(税后)", amount.ToString(), adminUserId, adminNickname, $"竞猜获胜奖励，帖子ID[{bbsId}]竞猜ID[{guessingId}],选项[{winningOptionText}]");
        }

        private void UpdateGuessingResult(int winningOptionId)
        {
            try
            {
                if (guessingId == 0)
                {
                    throw new InvalidOperationException("无效的竞猜ID");
                }

                // 添加这行来确保 guessData 被正确加载
                LoadGuessingInfo();

                if (guessData == null)
                {
                    throw new InvalidOperationException("无法加载竞猜数据");
                }

                // 添加日志，记录即将更新的获胜选项ID
                System.Diagnostics.Debug.WriteLine($"Updating guessing result with winning option ID: {winningOptionId}");

                string instanceName = PubConstant.GetAppString("InstanceName");
                GuessManager guessManager = new GuessManager(instanceName);
                guessManager.UpdateGuessingResult(guessingId, winningOptionId);
            }
            catch
            {
                throw;
            }
        }

        private void DistributeRewards(int winningOptionId)
        {
            try
            {
                if (guessingId == 0)
                {
                    throw new InvalidOperationException("无效的竞猜ID");
                }

                // 将winningOptionId转换为从0开始的索引
                int winningOptionIndex = winningOptionId - 1;

                // 添加日志，记录开始分发奖励的获胜选项ID
                System.Diagnostics.Debug.WriteLine($"Distributing rewards for winning option ID: {winningOptionId}, Index: {winningOptionIndex}");

                string instanceName = PubConstant.GetAppString("InstanceName");
                GuessManager guessManager = new GuessManager(instanceName);
                List<BetInfo> bets = guessManager.GetBetsForGuessing(guessingId);

                decimal totalBetAmount = bets.Sum(b => b.Amount);
                decimal winningBetAmount = bets.Where(b => b.OptionId == winningOptionId).Sum(b => b.Amount);

                // 获取获胜选项的文本
                string winningOptionText = guessData.Options[winningOptionIndex].Text;

                foreach (var bet in bets.Where(b => b.OptionId == winningOptionId))
                {
                    decimal reward = (bet.Amount / winningBetAmount) * totalBetAmount;
                    int rewardAmount = (int)Math.Floor(reward);

                    // 计算税后奖励金额
                    int taxAmount = (int)Math.Floor(rewardAmount * 0.1m); // 10% 税
                    int afterTaxRewardAmount = rewardAmount - taxAmount;

                    // 更新用户金币
                    guessManager.UpdateUserMoney(bet.UserId, afterTaxRewardAmount, siteid);

                    // 记录银行日志
                    SaveBankLog(bet.UserId.ToString(), "竞猜获胜(税后)", afterTaxRewardAmount.ToString(), this.userid, this.userVo.nickname, $"竞猜获胜奖励，帖子ID[{guessData.BbsId}]竞猜ID[{guessingId}],选项[{winningOptionText}]");

                    // 发送消息通知
                    string messageTitle = $"恭喜您在竞猜中获胜！";
                    string messageContent = $"您在帖子[<a href=\"/bbs-{guessData.BbsId}.html\">{guessData.BbsId}</a>]的竞猜中选择了正确选项[{winningOptionText}]，获得奖励{afterTaxRewardAmount}妖晶！";
                    guessManager.SendSystemMessage(siteid, bet.UserId, messageTitle, messageContent);
                }
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                System.Diagnostics.Debug.WriteLine($"Error in DistributeRewards: {ex.Message}");
                throw;
            }
        }

        private void EnsureUserDataLoaded()
        {
            if (string.IsNullOrEmpty(siteid) || userVo == null)
            {
                throw new InvalidOperationException("用户数据未正确加载");
            }
        }

        private long GetAuthorIdByBbsId(long bbsId)
        {
            string instanceName = PubConstant.GetAppString("InstanceName");
            GuessManager guessManager = new GuessManager(instanceName);
            return guessManager.GetAuthorIdByBbsId(bbsId);
        }

        private bool IsAuthor(long authorId)
        {
            // 确保 userid 是有效的
            if (string.IsNullOrEmpty(userid))
            {
                return false;
            }

            // 将 userid 转换为 long 类型进行比较
            long currentUserId;
            if (!long.TryParse(userid, out currentUserId))
            {
                return false;
            }

            // 比较当前用户ID和作者ID
            return currentUserId == authorId;
        }
    }
}