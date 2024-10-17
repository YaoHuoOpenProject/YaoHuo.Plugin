using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;
using System.Threading;

namespace YaoHuo.Plugin.BBS
{
    public class GuessAdd : MyPageWap
    {
        protected TextBox txtTitle;
        protected TextBox txtContent;
        protected TextBox txtGuessTitle;
        protected TextBox txtDeadline;
        protected TextBox txtOption1;
        protected TextBox txtOption2;
        protected Button btnSubmit;
        protected HiddenField hdnClassId;
        private string a = PubConstant.GetAppString("InstanceName");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid == "0")
            {
                ShowTipInfo("无此栏目ID", "");
                return;
            }

            if (classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块。", "");
                return;
            }

            if (!CheckLogin())
            {
                Response.Redirect("login.aspx?siteid=" + siteid + "&classid=" + classid);
                return;
            }

            if (!CheckManagerLvl("04", "0"))
            {
                ShowTipInfo("对不起，您没有发帖权限！", "");
                return;
            }

            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 2)) && !CheckManagerLvl("04", classVo.adminusername))
            {
                ShowTipInfo("发帖功能已关闭！", "");
                return;
            }

            if (!IsPostBack)
            {
                // 设置默认截止时间为当前整点时间间隔6小时后
                DateTime now = DateTime.Now;
                DateTime defaultDeadline = now.Date.AddHours(now.Hour).AddHours(6);
                if (defaultDeadline <= now)
                {
                    defaultDeadline = defaultDeadline.AddHours(6);
                }
                txtDeadline.Text = defaultDeadline.ToString("yyyy-MM-dd HH:00");
            }

            // 检查控件是否正确绑定
            if (txtTitle == null || txtContent == null || txtGuessTitle == null || txtDeadline == null || txtOption1 == null || txtOption2 == null)
            {
                throw new Exception("一个或多个控件未正确绑定。");
            }
        }

        private bool CheckLogin()
        {
            try
            {
                IsLogin(userid.ToString(), "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string backUrl = $"bbs/book_list.aspx?siteid={siteid}&classid={classid}";

            try
            {
                // 验证竞猜主题长度
                if (txtGuessTitle.Text.Trim().Length > 13)
                {
                    ShowTipInfo("竞猜主题最多13个字", backUrl);
                    return;
                }

                // 验证选项长度
                if (txtOption1.Text.Trim().Length > 5 || txtOption2.Text.Trim().Length > 5)
                {
                    ShowTipInfo("每个竞猜选项最多5个字", backUrl);
                    return;
                }

                // 验证截止时间
                if (!ValidateDeadline(out DateTime deadline))
                {
                    return;
                }

                // 插入帖子
                long bbsId = InsertBbsPost();

                if (bbsId > 0)
                {
                    string instanceName = PubConstant.GetAppString("InstanceName");
                    GuessManager guessManager = new GuessManager(instanceName);

                    // 创建竞猜选项（只允许两个选项）
                    List<GuessingOption> options = new List<GuessingOption>
                    {
                        new GuessingOption { Text = txtOption1.Text.Trim(), Amount = 0 },
                        new GuessingOption { Text = txtOption2.Text.Trim(), Amount = 0 }
                    };
                    string optionsJson = JsonConvert.SerializeObject(options);

                    // 添加竞猜
                    long guessingId = guessManager.AddGuessing(bbsId, txtGuessTitle.Text.Trim(), optionsJson, deadline);

                    if (guessingId > 0)
                    {
                        // 竞猜添加成功
                        ShowTipInfo("竞猜创建成功！", $"bbs-{bbsId}.html");
                    }
                    else
                    {
                        // 竞猜添加失败
                        ShowTipInfo("竞猜创建失败，请稍后重试。", backUrl);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                // 忽略 ThreadAbortException
            }
            catch (Exception ex)
            {
                string errorMessage = $"发生错误：{ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
                ShowTipInfo(errorMessage, backUrl);
            }
        }

        private bool ValidateDeadline(out DateTime deadline)
        {
            deadline = DateTime.MinValue;
            string backUrl = Request.Url.ToString();

            if (!DateTime.TryParseExact(txtDeadline.Text, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out deadline))
            {
                ShowTipInfo("请输入有效的截止时间，格式为：yyyy-MM-dd HH:00", backUrl);
                return false;
            }

            if (deadline.Minute != 0)
            {
                ShowTipInfo("截止时间必须为整点，例如：2024-10-05 06:00", backUrl);
                return false;
            }

            DateTime now = DateTime.Now;
            DateTime currentHour = now.Date.AddHours(now.Hour);
            DateTime minDeadline = currentHour.AddHours(6);
            if (minDeadline <= now)
            {
                minDeadline = minDeadline.AddHours(6);
            }
            DateTime maxDeadline = now.AddDays(30).Date.AddHours(now.Hour);

            if (deadline < minDeadline)
            {
                ShowTipInfo($"截止时间必须至少在 {minDeadline:yyyy-MM-dd HH:00} 之后", backUrl);
                return false;
            }

            if (deadline > maxDeadline)
            {
                ShowTipInfo($"截止时间不能超过 {maxDeadline:yyyy-MM-dd HH:00}", backUrl);
                return false;
            }

            return true;
        }

        private long InsertBbsPost()
        {
            try
            {
                if (string.IsNullOrEmpty(classid) || string.IsNullOrEmpty(userid.ToString()))
                {
                    throw new Exception($"Invalid classid or userid. classid: {classid}, userid: {userid}");
                }

                wap_bbs_BLL bbsBLL = new wap_bbs_BLL(a);
                wap_bbs_Model bbsModel = new wap_bbs_Model();

                bbsModel.ischeck = siteVo.isCheck;
                bbsModel.userid = long.Parse(siteid);
                bbsModel.book_classid = long.Parse(classid);
                bbsModel.book_title = txtTitle.Text;
                bbsModel.book_author = userVo.nickname;
                bbsModel.book_pub = userid;
                bbsModel.book_content = txtContent.Text;
                bbsModel.book_date = DateTime.Now;
                bbsModel.reDate = DateTime.Now;
                bbsModel.sendMoney = 0;

                long result = bbsBLL.Add(bbsModel);

                // 更新用户信息
                string getmoney = WapTool.GetSiteDefault(siteVo.moneyregular, 0);
                string getexpr = WapTool.GetSiteDefault(siteVo.lvlRegular, 0);
                if (!WapTool.IsNumeric(getmoney)) getmoney = "0";
                if (!WapTool.IsNumeric(getexpr)) getexpr = "0";

                // 使用参数化查询更新用户信息
                string updateSql = $@"UPDATE [user] 
                                      SET [money] = [money] + {int.Parse(getmoney)}, 
                                          expR = expR + {int.Parse(getexpr)}, 
                                          bbscount = {userVo.bbsCount + 1} 
                                      WHERE siteid = '{siteid}' AND userid = '{userid}'";

                // 使用 MainBll.UpdateSQL 方法执行更新
                MainBll.UpdateSQL(updateSql);

                SaveBankLog(userid, "论坛发帖", getmoney, userid, userVo.nickname, $"发新帖[{result}]");

                // 清除缓存
                WapTool.ClearDataBBS("bbs" + siteid + classid);
                WapTool.ClearDataTemp("bbsTotal" + siteid + classid);

                // 记录用户行为
                Action_user_doit(1);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"InsertBbsPost error: {ex.Message}", ex);
            }
        }
    }
}