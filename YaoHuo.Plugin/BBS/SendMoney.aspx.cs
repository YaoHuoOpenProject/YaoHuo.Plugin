using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class SendMoney : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string reid = "";

        public string page = "";

        public string lpage = "";

        public string ot = "";

        public string INFO = "";

        public string ERROR = "";

        public wap_bbsre_Model bbsReVo = null;

        public wap_bbs_Model bbsVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            reid = GetRequestValue("reid");
            page = GetRequestValue("page");
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("ot");
            IsLogin(userid, GetUrlQueryString());
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                string requestValue = GetRequestValue("sendmoney");
                if (!WapTool.IsNumeric(requestValue))
                {
                    INFO = "NULL";
                    return;
                }
                if (long.Parse(requestValue) < 1L)
                {
                    INFO = "NULL";
                    return;
                }
                wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
                bbsReVo = wap_bbsre_BLL.GetModel(long.Parse(reid));
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
                long hasMoney = bbsVo.hasMoney;
                long sendMoney = bbsVo.sendMoney;
                long num = long.Parse(requestValue);
                if (bbsReVo.bookid != bbsVo.id)
                {
                    INFO = "ERR";
                }
                else if (bbsVo.book_pub != userid)
                {
                    INFO = "ERR";
                }
                else if (bbsVo.book_pub == bbsReVo.userid.ToString())
                {
                    INFO = "ERR";
                }
                else if (num + hasMoney <= sendMoney && sendMoney > 0L)
                {
                    if (bbsReVo.bookid == bbsVo.id && num != 0L && userid == bbsVo.book_pub && bbsVo.book_pub != bbsReVo.userid.ToString())
                    {
                        MainBll.UpdateSQL("update [wap_bbs] set hasMoney=" + (hasMoney + num) + " where id=" + bbsVo.id);
                        MainBll.UpdateSQL("update [wap_bbsre] set myGetMoney=" + (num + bbsReVo.myGetMoney) + " where id=" + bbsReVo.id);
                        MainBll.UpdateSQL("update [user] set money=money+" + num + " where userid=" + bbsReVo.userid);
                        SaveBankLog(bbsReVo.userid.ToString(), "论坛赏分", num.ToString(), userid, nickname, "得到帖子赏分[" + bbsVo.id + "]");
                        string text = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                        text = string.Concat(text, " values(", siteid, ",", userid, ",'", nickname, "','您得到赏分:", num, "喽~','时间：", DateTime.Now, "[br]点击查看:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", classid, "&amp;id=", bbsVo.id, "]", bbsVo.book_title.Replace("[", "［").Replace("]", "］"), "[/url]',", bbsReVo.userid, ",1)");
                        MainBll.UpdateSQL(text);
                        INFO = "OK";
                        WapTool.ClearDataBBSRe("bbsRe" + siteid + id);
                    }
                    else
                    {
                        INFO = "ERR";
                    }
                }
                else
                {
                    INFO = "ERR";
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}