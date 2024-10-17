using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_modadd : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public string action = "";

        public string id = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string book_content = "";

        public string titlemax = "2";

        public string contentmax = "2";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 4)))
            {
                ShowTipInfo("续帖功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            //needPassWordToAdmin();
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
            bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
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
            if (userid != bbsVo.book_pub.ToString())
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                book_content = GetRequestValue("book_content");
                bbsVo.book_title = GetRequestValue("book_title");
                if (bbsVo.book_title.Length > 200)
                {
                    bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
                }
                int num = bbsVo.book_title.IndexOf("[/img]");
                if (num > 0)
                {
                    string text = bbsVo.book_title.Substring(0, num + 6);
                    if (text.IndexOf("[img]face") == 0)
                    {
                        string text2 = bbsVo.book_title.Substring(num + 6, bbsVo.book_title.Length - num - 6);
                        bbsVo.book_title = text + text2.Replace("/", "／").Replace("[", "［").Replace("]", "］");
                    }
                    else
                    {
                        bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
                    }
                }
                else
                {
                    bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
                }
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
                string arryString = WapTool.getArryString(classVo.smallimg, '|', 21);
                if (arryString.Trim() != "")
                {
                    arryString = "_" + arryString + "_";
                    bool flag = false;
                    if (book_content.IndexOf("[/reply]") > 0 || book_content.IndexOf("[/buy]") > 0 || book_content.IndexOf("[/coin]") > 0 || book_content.IndexOf("[/grade]") > 0)
                    {
                        flag = true;
                    }
                    if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("_" + userVo.SessionTimeout + "_") < 0)
                    {
                        ShowTipInfo("您当前的身份不允许发特殊帖。", "bbs/book_view_modadd.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
                    }
                }
                if (book_content.Trim().Length < long.Parse(contentmax) || bbsVo.book_title.Trim().Length < long.Parse(titlemax))
                {
                    INFO = "NULL";
                    return;
                }
                bbsVo.book_content = bbsVo.book_content + "[br]" + book_content;
                if (!IsUserManager(userid, userVo.managerlvl, classVo.adminusername))
                {
                    bbsVo.book_content = bbsVo.book_content.ToLower().Replace("[sid]", "[sid2]");
                    bbsVo.book_content = bbsVo.book_content.ToLower().Replace("[sid1]", "[sid2]");
                }
                string text3 = "{" + userVo.nickname + "(ID" + userVo.userid + ")文字续帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                bbsVo.whylock = text3 + bbsVo.whylock;
                wap_bbs_BLL.Update(bbsVo);
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}