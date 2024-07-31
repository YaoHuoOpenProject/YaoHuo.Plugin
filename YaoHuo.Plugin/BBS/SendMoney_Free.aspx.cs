using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class SendMoney_Free : MyPageWap
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

        public string touserid = "";

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
            touserid = GetRequestValue("touserid");
            if (!WapTool.IsNumeric(touserid))
            {
                touserid = "0";
            }
            IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, GetUrlQueryString());
            needPassWordToAdmin();
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                string requestValue = GetRequestValue("sendmoney");
                if (!WapTool.IsNumeric(requestValue) || touserid == "0" || touserid == userVo.userid.ToString())
                {
                    INFO = "ERR";
                    return;
                }
                if (long.Parse(requestValue) < 1L)
                {
                    INFO = "ERR";
                    return;
                }
                if (userVo.money < long.Parse(requestValue))
                {
                    INFO = "ERR";
                    return;
                }
                MainBll.UpdateSQL("update [user] set money=money - " + requestValue + " where userid=" + userid);
                MainBll.UpdateSQL("update [user] set money=money + " + requestValue + " where userid=" + touserid);
                string text = "恭喜您，" + userVo.nickname + "奖励" + requestValue + "个币给您！";
                string text2 = "您的回贴得到奖励！";
                if (GetRequestValue("remark") != "")
                {
                    text2 = GetRequestValue("remark");
                }
                string text3 = "原因:" + text2 + "[url=" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "]查看[/url]";
                MainBll.UpdateSQL("update [wap_bbsre] set mygetmoney =mygetmoney + " + requestValue + " where id=" + long.Parse(reid) + " and userid=" + touserid);
                string text4 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                text4 = text4 + "  values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text3 + "'," + touserid + ",1)";
                MainBll.UpdateSQL(text4);
                SaveBankLog(touserid, "转币操作", requestValue.ToString(), userid, nickname, "回贴奖励给我");
                SaveBankLog(userid, "转币操作", "-" + requestValue.ToString(), userid, nickname, "我奖币给会员ID(" + touserid + ")");
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}
