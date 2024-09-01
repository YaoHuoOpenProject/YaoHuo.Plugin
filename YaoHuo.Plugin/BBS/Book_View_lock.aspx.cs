using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_View_lock : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string tops = "";

        public string whylock = "";

        public wap_bbs_Model bookVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            tops = GetRequestValue("tops");
            whylock = GetRequestValue("whylock");
            whylock = whylock.Replace("|", "");
            CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
            bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
            if (!(action == "gomod"))
            {
                return;
            }
            WapTool.ClearDataBBS("bbs" + siteid + classid);
            WapTool.ClearDataBBS("bbsTop" + siteid + classid);
            try
            {
                if (tops == "1")
                {
                    if (bookVo.islock == 1L)
                    {
                        INFO = "ERR";
                        return;
                    }
                    whylock = "{" + userVo.nickname + "(ID" + userVo.userid + ")锁定原因:" + whylock + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    whylock += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set islock=1,whylock='" + whylock + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    string book_title = bookVo.book_title;
                    book_title = book_title.Replace("[", "［");
                    book_title = book_title.Replace("]", "］");
                    MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题被锁定','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "锁定用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "  原因:" + whylock + "','" + IP + "')");
                    INFO = "OK";
                }
                else if (bookVo.islock == 0L)
                {
                    INFO = "ERR";
                }
                else
                {
                    whylock = "{" + userVo.nickname + "(ID" + userVo.userid + ")解锁贴子" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    whylock += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set islock=0,whylock='" + whylock + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    string book_title = bookVo.book_title;
                    book_title = book_title.Replace("[", "［");
                    book_title = book_title.Replace("]", "］");
                    MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题取消锁定!','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "取消锁定用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "','" + IP + "')");
                    INFO = "OK";
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}