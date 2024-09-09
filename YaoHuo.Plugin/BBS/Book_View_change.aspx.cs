using System;
using System.Collections.Generic;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_View_change : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public wap_book_Model bookVo = new wap_book_Model();

        public List<class_Model> classList = new List<class_Model>();

        public string action = "";

        public string id = "";

        public string toclassid = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public wap_bbs_Model bbsVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            toclassid = classid;
            CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            class_BLL class_BLL = new class_BLL(a);
            classList = class_BLL.GetFromPathList(long.Parse(siteid), "bbs/index.aspx");
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                toclassid = GetRequestValue("toclassid");
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
                if (toclassid == "")
                {
                    INFO = "SELECT";
                    return;
                }
                string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")转移(" + classid + "→" + toclassid + ")" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                text += bbsVo.whylock;
                MainBll.UpdateSQL("update wap_bbs set book_classid='" + toclassid + "',whylock='" + text + "'  where userid=" + siteid + " and id=" + long.Parse(id));
                MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "转移用户ID:" + bbsVo.book_pub + "发表的ID=" + id + "主题:" + bbsVo.book_title + " (原栏目ID:" + classid + ")至栏目ID:" + toclassid + "','" + IP + "')");
                INFO = "OK";
                WapTool.ClearDataBBS("bbs" + siteid + classid);
                WapTool.ClearDataBBS("bbsTop" + siteid + classid);
                WapTool.ClearDataTemp("bbsTotal" + siteid + classid);
                WapTool.ClearDataBBS("bbs" + siteid + toclassid);
                WapTool.ClearDataBBS("bbsTop" + siteid + toclassid);
                WapTool.ClearDataTemp("bbsTotal" + siteid + toclassid);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}