using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;
using KeLin.ClassManager;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public partial class GuessVote : MyPageWap
    {
        protected Literal litGuessTitle;
        protected Literal litSelectedOption;
        protected long guessId;
        protected string selectedOption;
        protected GuessData guessingData;
        protected DropDownList ddlBetAmount;

        // 添加有效下注金额列表
        private static readonly int[] ValidBetAmounts = { 200, 300, 500, 1000, 2000, 3000, 5000, 8000, 10000, 20000, 30000, 50000, 80000, 100000 };

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "");

            System.Diagnostics.Debug.WriteLine($"Page_Load - Start");
            System.Diagnostics.Debug.WriteLine($"IsPostBack: {IsPostBack}");

            // 始终解析 guessId，不论是否回发
            if (!ParseGuessId())
            {
                ShowTipInfo("无效的竞猜ID", "");
                return;
            }

            // 获取竞猜数据
            guessingData = GetGuessingData(guessId);
            if (guessingData == null)
            {
                ShowTipInfo("未找到指定的竞猜", "");
                return;
            }

            // 使用新方法检查竞猜状态
            GuessManager guessManager = new GuessManager(PubConstant.GetAppString("InstanceName"));
            var (isClosed, isDeadlinePassed) = guessManager.CheckGuessingStatus(guessId);

            if (isClosed)
            {
                ShowTipInfo("该竞猜已结束，无法继续下注", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            if (isDeadlinePassed)
            {
                ShowTipInfo("截止时间已到，无法继续下注", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            if (!IsPostBack)
            {
                selectedOption = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["option"]);

                if (guessId == 0 || string.IsNullOrEmpty(selectedOption))
                {
                    ShowTipInfo("无效的请求参数", "");
                    return;
                }

                litGuessTitle.Text = guessingData.Title;
                litSelectedOption.Text = selectedOption;

                // 注册有效的下注金额
                foreach (int amount in ValidBetAmounts)
                {
                    ddlBetAmount.Items.Add(new ListItem(amount.ToString(), amount.ToString()));
                }
            }

            System.Diagnostics.Debug.WriteLine($"Page_Load - End");
        }

        private bool ParseGuessId()
        {
            string idParam = HttpContext.Current.Request.QueryString["id"];
            if (!long.TryParse(idParam, out guessId))
            {
                guessId = 0;
                return false;
            }
            return true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return; // 如果验证失败，不继续处理
            }

            // 添加日志
            System.Diagnostics.Debug.WriteLine($"btnConfirm_Click - guessId: {guessId}");

            // 重新获取最新的竞猜数据
            guessingData = GetGuessingData(guessId);
            if (guessingData == null)
            {
                ShowTipInfo("竞猜数据不存在", "");
                return;
            }

            // 检查竞猜是否已关闭
            if (guessingData.IsClosed)
            {
                ShowTipInfo("该竞猜已结束，无法继续下注", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            // 检查当前时间是否超过截止时间
            if (DateTime.Now > guessingData.Deadline)
            {
                ShowTipInfo("截止时间已到，无法继续下注", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            if (string.IsNullOrEmpty(selectedOption))
            {
                selectedOption = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["option"]);
            }

            // 确保选项被正确解码
            selectedOption = HttpUtility.UrlDecode(selectedOption);

            if (string.IsNullOrEmpty(selectedOption))
            {
                ShowTipInfo("无效的选项", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            int betAmount;
            if (ddlBetAmount == null || !int.TryParse(ddlBetAmount.SelectedValue, out betAmount) || !IsValidBetAmount(betAmount))
            {
                ShowTipInfo("无效的下注金额，请选择有效的金额。", $"GuessVote.aspx?id={guessId}&option={HttpUtility.UrlEncode(selectedOption)}");
                return;
            }

            long userIdLong;
            if (!long.TryParse(userid, out userIdLong))
            {
                ShowTipInfo("无效的用户ID", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            // 检查用户是否已经下注
            if (HasUserAlreadyBet(guessId, userIdLong))
            {
                ShowTipInfo("您已经在这个竞猜中下过注了", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            // 检查用户余额
            if (userVo.money < betAmount)
            {
                ShowTipInfo("您的金币余额不足，无法参与竞猜", $"bbs-{guessingData.BbsId}.html");
                return;
            }

            // 使用类成员变量 guessId，而不是局部变量
            if (UpdateGuessingVote(guessId, selectedOption, betAmount, userIdLong))
            {
                // 扣除用户金币
                GuessManager guessManager = new GuessManager(PubConstant.GetAppString("InstanceName"));
                guessManager.UpdateUserMoney(userIdLong, -betAmount, siteid);

                // 获取选项文本
                string optionText = selectedOption;

                // 写入银行日志（使用选项文本）
                SaveBankLog(userIdLong.ToString(), "竞猜下注", $"-{betAmount}", userIdLong.ToString(), userVo.nickname, $"参与帖子ID[{guessingData.BbsId}]竞猜ID[{guessId}],选项[{optionText}]");

                // 更新用户对象中的金币数量
                userVo.money -= betAmount;

                ShowTipInfo("下注成功", $"bbs-{guessingData.BbsId}.html");

                // 验证用户的下注
                BetInfo userBet = guessManager.GetUserBet(guessId, userIdLong);
                if (userBet != null)
                {
                    int optionIndex = userBet.OptionId - 1; // 将OptionId转换为从0开始的索引
                    System.Diagnostics.Debug.WriteLine($"User bet - OptionId: {userBet.OptionId}, OptionIndex: {optionIndex}, Amount: {userBet.Amount}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("User bet not found");
                }
            }
            else
            {
                ShowTipInfo("下注失败，请稍后重试", $"bbs-{guessingData.BbsId}.html");
            }
        }

        // 添加验证下注金额的方法
        private bool IsValidBetAmount(int amount)
        {
            return ValidBetAmounts.Contains(amount);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/bbs-{guessingData.BbsId}.html");
        }

        private GuessData GetGuessingData(long guessId)
        {
            string instanceName = PubConstant.GetAppString("InstanceName");
            GuessManager guessManager = new GuessManager(instanceName);
            return guessManager.GetGuessingById(guessId);
        }

        private bool UpdateGuessingVote(long guessId, string selectedOption, int betAmount, long userId)
        {
            string instanceName = PubConstant.GetAppString("InstanceName");
            GuessManager guessManager = new GuessManager(instanceName);
            return guessManager.UpdateGuessingVote(guessId, selectedOption, betAmount, userId);
        }

        // 添加这个新方法来获取选项ID
        private int GetOptionId(long guessId, string selectedOption)
        {
            GuessManager guessManager = new GuessManager(PubConstant.GetAppString("InstanceName"));
            GuessData guessingData = guessManager.GetGuessingById(guessId);

            if (guessingData != null && guessingData.Options != null)
            {
                for (int i = 0; i < guessingData.Options.Count; i++)
                {
                    if (guessingData.Options[i].Text == selectedOption)
                    {
                        return i; // 修改这里，直接返回索引 i，而不是 i + 1
                    }
                }
            }

            return -1; // 如果没有找到匹配的选项，返回-1（而不是0）
        }

        // 添加这个新方法来检查用户是否已经下注
        private bool HasUserAlreadyBet(long guessId, long userId)
        {
            string instanceName = PubConstant.GetAppString("InstanceName");
            GuessManager guessManager = new GuessManager(instanceName);
            return guessManager.HasUserBet(guessId, userId);
        }

        protected void cvBetAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int betAmount;
            if (int.TryParse(args.Value, out betAmount))
            {
                args.IsValid = IsValidBetAmount(betAmount);
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}