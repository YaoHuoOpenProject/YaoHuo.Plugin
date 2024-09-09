using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_re_del : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string reid = "";

        public string page = "";

        public string lpage = "";

        public string ot = "";

        public string INFO = "";

        public string ERROR = "";

        public string sub = "";

        public wap_bbsre_Model bbsReVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            reid = GetRequestValue("reid");
            page = GetRequestValue("page");
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("ot");
            sub = GetRequestValue("sub");
            IsLogin(userid, GetUrlQueryString());
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
            wap_bbs_Model model = wap_bbs_BLL.GetModel(long.Parse(id));
            if (model == null)
            {
                ShowTipInfo("不存在主题帖！", "");
            }
            else if ((model.reShow > 0L || model.freeMoney > 0L) && !IsCheckManagerLvl("|00|01|", ""))
            {
                ShowTipInfo("抱歉，每日签到送币帖、派币帖的回复只能由站长权限删除。", "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
            }
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(string_10);
            bbsReVo = wap_bbsre_BLL.GetModel(long.Parse(reid));
            if (bbsReVo == null)
            {
                ShowTipInfo("不存在此记录！", "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
            }
            if (userid != bbsReVo.userid.ToString())
            {
                IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
            }
            else if (!CheckManagerLvl(userVo.managerlvl, classVo.adminusername) && WapTool.GetSiteDefault(siteVo.Version, 26) == "1")
            {
                ShowTipInfo("站长已关闭此功能！", "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
            }
            if (!(action == "godel"))
            {
                return;
            }
            try
            {
                WapTool.ClearDataBBSRe("bbsRe" + siteid + id);
                List<wap2_attachment_Model> list2 = null;
                wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(string_10);
                list2 = wap2_attachment_BLL.GetListVo(" siteid=" + siteid + " and book_type ='bbsre' and book_id=" + long.Parse(reid));
                StringBuilder stringBuilder = new StringBuilder();
                int num = 0;
                while (list2 != null && num < list2.Count)
                {
                    stringBuilder.Append(list2[num].book_file + "|");
                    num++;
                }
                DeleteFile("bbs", stringBuilder.ToString(), GetUrlQueryString().Replace("godel", "go"));
                MainBll.UpdateSQL("delete from wap2_attachment where siteid=" + siteid + " and book_type ='bbsre' and book_id=" + long.Parse(reid));
                wap_bbsre_BLL.Delete(long.Parse(siteid), long.Parse(reid));
                MainBll.UpdateSQL("update wap_bbs set book_re=book_re-1 where id=" + long.Parse(id));
                long num2 = WapTool.getMoneyRegular(siteVo.moneyregular, 1);
                long num3 = WapTool.getLvLRegular(siteVo.lvlRegular, 1);
                if (userid == bbsReVo.userid.ToString())
                {
                    num2 *= 2L;
                    num3 *= 2L;
                }
                else if (sub == "2")
                {
                    num2 *= 2L;
                    num3 *= 2L;
                }
                else if (sub == "0")
                {
                    num2 = 0L;
                    num3 = 0L;
                }
                MainBll.UpdateSQL("update [user] set money=money -" + num2 + ",expR=expR-" + num3 + "  where siteid=" + long.Parse(siteid) + " and userid=" + bbsReVo.userid);
                SaveBankLog(bbsReVo.userid.ToString(), "删除回帖", "-" + num2, siteid, "系统", "回帖ID" + bbsReVo.id);
                string text = "";
                if (userid == bbsReVo.userid.ToString())
                {
                    text = "您删除了自己的回帖";
                }
                MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一条回复删除，扣除", num2, "妖晶、", num3, "经验','", text, "[br]删除时间：", DateTime.Now, "[br]论坛主题：[url=/bbs-", id, ".html]点此查看[/url]',", bbsReVo.userid, ",1)"));
                MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'删除用户" + bbsReVo.userid + "发表的ID=" + reid + "回复:" + bbsReVo.content + "','" + IP + "')");
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}