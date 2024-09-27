using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Text.RegularExpressions;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_mod : MyPageWap
    {
        private const long MIN_ADDITIONAL_REWARD = 1000;

        private string a = PubConstant.GetAppString("InstanceName");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public string action = "";

        public string id = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string[] facelist;

        public string[] facelistImg;

        public string[] stypelist;

        public string face = "";

        public string stype = "";

        public string titlemax = "2";

        public string contentmax = "2";

        public string logMessage = "";

        public bool isAuthor = false;

        public bool isFreeMoney = false;

        public string additionalRewardMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);

            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 28)) && "|00|01|".IndexOf(userVo.managerlvl) < 0)
            {
                ShowTipInfo("修改帖子功能已关闭！", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }

            InitializeBbsData();

            if (action == "gomod")
            {
                try
                {
                    UpdateBbsContent();
                    HandleAdditionalReward();
                    wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                    wap_bbs_BLL.Update(bbsVo);
                    WapTool.ClearDataBBS("bbs" + siteid + classid);
                    INFO = "OK";
                }
                catch (Exception ex)
                {
                    ERROR = ex.ToString();
                }
            }
        }

        private void InitializeBbsData()
        {
            try
            {
                InitializeFaceAndTypeList();

                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
                ValidateBbsData();

                isAuthor = userid == bbsVo.book_pub.ToString();
                if (!isAuthor)
                {
                    CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
                }
                else
                {
                    IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
                }

                isFreeMoney = bbsVo.freeMoney > 0;

                RemoveTitlePrefix();
            }
            catch (Exception)
            {
                // 处理异常
            }
        }

        private void InitializeFaceAndTypeList()
        {
            if (classVo.bbsFace.IndexOf('_') < 0)
            {
                classVo.bbsFace = "_";
            }
            facelist = classVo.bbsFace.Split('_')[0].Split('|');
            facelistImg = classVo.bbsFace.Split('_')[1].Split('|');
            if (classVo.bbsType.IndexOf('_') < 0)
            {
                classVo.bbsType = "_";
            }
            stypelist = classVo.bbsType.Split('_')[0].Split('|');
        }

        private void ValidateBbsData()
        {
            if (bbsVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bbsVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bbsVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bbsVo.islock == 1L)
            {
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
            }
            else if (bbsVo.islock == 2L)
            {
                ShowTipInfo("此帖已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
            }

            if (bbsVo.userid.ToString() != siteid)
            {
                base.Response.End();
            }
            if (bbsVo.book_classid.ToString() != classid)
            {
                base.Response.End();
            }
        }

        private void RemoveTitlePrefix()
        {
            int num = bbsVo.book_title.LastIndexOf("]");
            if (num > 0)
            {
                bbsVo.book_title = bbsVo.book_title.Substring(num + 1, bbsVo.book_title.Length - num - 1);
            }
        }

        private void UpdateBbsContent()
        {
            bbsVo.book_title = GetSafeRequestValue("book_title").Trim();
            if (bbsVo.book_title.Length > 200)
            {
                bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
            }
            // 修改: 使用 PreserveCodeContent 方法处理 book_content
            bbsVo.book_content = PreserveCodeContent(GetRequestValue("book_content"));
            bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
            bbsVo.book_img = GetSafeRequestValue("book_img");
            face = GetSafeRequestValue("face");
            stype = GetSafeRequestValue("stype");
            bbsVo.book_content = WapTool.URLtoWAP(bbsVo.book_content);
            titlemax = WapTool.getArryString(classVo.smallimg, '|', 24);
            contentmax = WapTool.getArryString(classVo.smallimg, '|', 25);
            if (!WapTool.IsNumeric(titlemax) || titlemax == "0")
            {
                titlemax = "2";
            }
            if (!WapTool.IsNumeric(contentmax) || contentmax == "0")
            {
                contentmax = "2";
            }
            string text = GetSafeRequestValue("viewtype");
            string text2 = GetSafeRequestValue("viewmoney");
            if (!WapTool.IsNumeric(text))
            {
                text = "0";
            }
            if (!WapTool.IsNumeric(text2))
            {
                text2 = "0";
            }
            bbsVo.viewtype = long.Parse(text);
            bbsVo.viewmoney = long.Parse(text2);
            ValidateSpecialPost();
            AddTypeAndFaceToTitle();
            UpdateBbsMetadata();
        }

        // 新增: PreserveCodeContent 方法，该方法用于保护 [code] 标签内的内容不被 HTML 编码
        private string PreserveCodeContent(string content)
        {
            // 使用正则表达式分割内容，保留 [code] 标签
            string[] parts = Regex.Split(content, @"(\[code\].*?\[/code\])", RegexOptions.Singleline);
            for (int i = 0; i < parts.Length; i++)
            {
                // 只对非 [code] 标签内的内容进行 HTML 编码
                if (i % 2 == 0)
                {
                    parts[i] = System.Web.HttpUtility.HtmlEncode(parts[i]);
                }
            }
            // 将所有部分重新组合成一个字符串
            return string.Join("", parts);
        }

        private string GetSafeRequestValue(string key)
        {
            return System.Web.HttpUtility.HtmlEncode(GetRequestValue(key));
        }

        private void ValidateSpecialPost()
        {
            string arryString = WapTool.getArryString(classVo.smallimg, '|', 21);
            if (arryString != "")
            {
                arryString = "_" + arryString + "_";
                bool flag = false;
                if (int.Parse(bbsVo.viewtype.ToString()) > 2 || bbsVo.book_content.IndexOf("[/reply]") > 0 || bbsVo.book_content.IndexOf("[/buy]") > 0 || bbsVo.book_content.IndexOf("[/coin]") > 0 || bbsVo.book_content.IndexOf("[/grade]") > 0)
                {
                    flag = true;
                }
                if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("_" + userVo.SessionTimeout + "_") < 0)
                {
                    ShowTipInfo("您当前的身份不允许发特殊帖。", "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
                }
            }
        }

        private void AddTypeAndFaceToTitle()
        {
            stype = stype.Replace("类别", "");
            face = face.Replace("表情", "");
            if (stype != "")
            {
                bbsVo.book_title = "[" + stype + "]" + bbsVo.book_title;
            }
            if (face.Trim().Length > 3 && face.Substring(face.Length - 3, 3).ToLower() == "gif")
            {
                bbsVo.book_title = "[img]face/" + face + "[/img]" + bbsVo.book_title;
            }
            bbsVo.book_title = bbsVo.book_title.Trim();
            if (bbsVo.book_title.Length > 200)
            {
                bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
            }
            if (WapTool.getArryString(classVo.smallimg, '|', 41) == "1" && stype.Trim() == "")
            {
                ShowTipInfo("类别不能为空！", "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            }
        }

        private void UpdateBbsMetadata()
        {
            string text3 = "{" + userVo.nickname + "(ID" + userVo.userid + ")修改此帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
            bbsVo.whylock = text3 + bbsVo.whylock;
            string arryString2 = WapTool.getArryString(classVo.smallimg, '|', 43);
            if (arryString2 == "2")
            {
                bbsVo.reDate = DateTime.Now;
            }
        }

        private void HandleAdditionalReward()
        {
            if (!isFreeMoney)
            {
                string additionalReward = GetSafeRequestValue("additionalReward");
                if (!string.IsNullOrEmpty(additionalReward) && long.TryParse(additionalReward, out long additionalRewardAmount))
                {
                    if (additionalRewardAmount >= MIN_ADDITIONAL_REWARD && isAuthor)
                    {
                        if (userVo.money >= additionalRewardAmount)
                        {
                            UpdateRewardAndUserBalance(additionalRewardAmount);
                        }
                        else
                        {
                            additionalRewardMessage = "insufficient_balance";
                        }
                    }
                    else if (additionalRewardAmount > 0 && !isAuthor)
                    {
                        additionalRewardMessage = "not_author";
                    }
                    else if (additionalRewardAmount > 0 && additionalRewardAmount < MIN_ADDITIONAL_REWARD)
                    {
                        additionalRewardMessage = "min_amount";
                    }
                }
            }
        }

        private void UpdateRewardAndUserBalance(long additionalRewardAmount)
        {
            bbsVo.sendMoney += additionalRewardAmount;
            MainBll.UpdateSQL("UPDATE [user] SET [money] = [money] - " + additionalRewardAmount + " WHERE siteid = " + siteid + " AND userid = " + userid);
            SaveBankLog(userid, "追加悬赏", "-" + additionalRewardAmount.ToString(), userid, userVo.nickname, "追加悬赏[" + id + "]");
            additionalRewardMessage = "success," + additionalRewardAmount;
            userVo.money -= additionalRewardAmount;
        }
    }
}