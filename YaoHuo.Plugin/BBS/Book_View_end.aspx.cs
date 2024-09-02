using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_end : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

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
            IsLogin(userid, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            //needPassWordToAdmin();
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
            bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
            if (bookVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bookVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bookVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bookVo.islock == 1L)
            {
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            if (userid != bookVo.book_pub.ToString())
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            WapTool.ClearDataBBS("bbs" + siteid + classid);
            WapTool.ClearDataBBS("bbsTop" + siteid + classid);
            try
            {
                string book_title;
                if (tops == "2")
                {
                    if (bookVo.islock == 2L)
                    {
                        INFO = "ERR";
                        return;
                    }
                    whylock = "{" + userVo.nickname + "(ID" + userVo.userid + ")结束原因:" + whylock + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    whylock += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set islock=2,whylock='" + whylock + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    book_title = bookVo.book_title;
                    book_title = book_title.Replace("[", "［");
                    book_title = book_title.Replace("]", "］");
                    MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题被结束','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "结束用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "  原因:" + whylock + "','" + IP + "')");
                    INFO = "OK";
                    return;
                }
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
                if (bookVo.islock == 0L)
                {
                    INFO = "ERR";
                    return;
                }
                whylock = "{" + userVo.nickname + "(ID" + userVo.userid + ")解除结束帖子" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                whylock += bookVo.whylock;
                MainBll.UpdateSQL("update wap_bbs set islock=0,whylock='" + whylock + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                book_title = bookVo.book_title;
                book_title = book_title.Replace("[", "［");
                book_title = book_title.Replace("]", "］");
                MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题取消结束!','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "取消结束用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "','" + IP + "')");
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}