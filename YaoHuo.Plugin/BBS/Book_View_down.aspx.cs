using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_View_down : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string tops = "";

        public wap_bbs_Model bookVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            tops = GetRequestValue("tops");
            CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
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
            else if (bookVo.islock == 2L)
            {
                ShowTipInfo("此帖已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            else if (bookVo.isdown == 1L)
            {
                INFO = "NOTDOWN";
            }
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
                    if (bookVo.isdown == 2L)
                    {
                        INFO = "ERR";
                        return;
                    }
                    if (bookVo.isdown == 1L)
                    {
                        INFO = "NOTDOWN";
                        return;
                    }
                    string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")设沉帖子" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    text += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set isdown=2,whylock='" + text + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "设沉用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + bookVo.book_title + "','" + IP + "')");
                    INFO = "OK";
                }
                else if (bookVo.isdown == 0L)
                {
                    INFO = "ERR";
                }
                else if (bookVo.isdown == 1L)
                {
                    INFO = "NOTDOWN";
                }
                else
                {
                    string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")解除沉帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    text += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set isdown=0,whylock='" + text + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    string book_title = bookVo.book_title;
                    book_title = book_title.Replace("[", "［");
                    book_title = book_title.Replace("]", "］");
                    MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题取消沉帖!','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "取消设沉用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "','" + IP + "')");
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