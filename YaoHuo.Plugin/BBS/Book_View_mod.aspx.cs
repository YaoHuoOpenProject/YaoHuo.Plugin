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

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            //needPassWordToAdmin();
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 28)) && "|00|01|".IndexOf(userVo.managerlvl) < 0)
            {
                ShowTipInfo("修改帖子功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            try
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
            catch (Exception)
            {
            }
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
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
            else
            {
                IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
                //needPassWordToAdmin();
            }
            int num = bbsVo.book_title.LastIndexOf("]");
            if (num > 0)
            {
                bbsVo.book_title = bbsVo.book_title.Substring(num + 1, bbsVo.book_title.Length - num - 1);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                bbsVo.book_title = GetRequestValue("book_title").Trim(); // 移除标题前后的空格
                if (bbsVo.book_title.Length > 200)
                {
                    bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
                }
                bbsVo.book_content = GetRequestValue("book_content");
                bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
                bbsVo.book_img = GetRequestValue("book_img");
                face = GetRequestValue("face");
                stype = GetRequestValue("stype");
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
                string text = GetRequestValue("viewtype");
                string text2 = GetRequestValue("viewmoney");
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
                string arryString = WapTool.getArryString(classVo.smallimg, '|', 21);
                if (arryString != "")
                {
                    arryString = "_" + arryString + "_";
                    bool flag = false;
                    if (int.Parse(text) > 2 || bbsVo.book_content.IndexOf("[/reply]") > 0 || bbsVo.book_content.IndexOf("[/buy]") > 0 || bbsVo.book_content.IndexOf("[/coin]") > 0 || bbsVo.book_content.IndexOf("[/grade]") > 0)
                    {
                        flag = true;
                    }
                    if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("_" + userVo.SessionTimeout + "_") < 0)
                    {
                        ShowTipInfo("您当前的身份不允许发特殊帖。", "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
                    }
                }
                if (bbsVo.book_title.Trim().Length < long.Parse(titlemax) || bbsVo.book_content.Trim().Length < long.Parse(contentmax))
                {
                    INFO = "NULL";
                    return;
                }
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
                bbsVo.book_title = bbsVo.book_title.Trim(); // 再次移除标题前后的空格
                if (bbsVo.book_title.Length > 200)
                {
                    bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
                }
                if (WapTool.getArryString(classVo.smallimg, '|', 41) == "1" && stype.Trim() == "")
                {
                    ShowTipInfo("类别不能为空！", "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
                }
                string text3 = "{" + userVo.nickname + "(ID" + userVo.userid + ")修改此帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                bbsVo.whylock = text3 + bbsVo.whylock;
                string arryString2 = WapTool.getArryString(classVo.smallimg, '|', 43);
                if (arryString2 == "2")
                {
                    bbsVo.reDate = DateTime.Now;
                }
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
}