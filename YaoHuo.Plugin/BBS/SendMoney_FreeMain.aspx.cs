using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class SendMoney_FreeMain : MyPageWap
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
            //IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, GetUrlQueryString());
            //needPassWordToAdmin();
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
                //判断请求方式
                if (HttpContext.Current.Request.HttpMethod != "POST")
                {
                    INFO = "ERR";
                    return;
                }
                //判断值是否合理
                if (!SendMoneyTool.IsReasonable(requestValue))
                {
                    INFO = "ERR";
                    return;
                }
                //扣钱脚本
                MainBll.UpdateSQL("update [user] set money=money - " + requestValue + " where userid=" + userid);
                //写日志记录(打赏者)
                SaveBankLog(userid, "打赏送币", "-" + requestValue.ToString(), userid, nickname, "打赏给会员ID(" + touserid + ")");
                //扣除手续费
                var handlingFee = SendMoneyTool.GetHandlingFee(requestValue);
                requestValue = $"{long.Parse(requestValue) - handlingFee}";
                //加钱脚本
                MainBll.UpdateSQL("update [user] set money=money + " + requestValue + " where userid=" + touserid);
                //写日志记录(被打赏者)
                SaveBankLog(touserid, "打赏送币", requestValue.ToString(), userid, nickname, "发帖获得打赏");
                //新建消息(被打赏者)
                var text = "恭喜，" + userVo.nickname + "打赏" + requestValue + "个妖晶给您！";
                var text2 = "您的帖子获得赞赏！";
                if (GetRequestValue("remark") != "")
                {
                    text2 = GetRequestValue("remark");
                }
                var text3 = "备注原因：" + text + " [[url=" + http_start + "bbs-" + id + ".html]进入帖子查看[/url]]";
                MainBll.UpdateSQL("update [wap_bbs] set mygetmoney =mygetmoney + " + requestValue + " where id=" + long.Parse(id) + " and userid=" + siteid + " and book_pub='" + touserid + "'");
                var text4 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem) values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text3 + "'," + touserid + ",1)";
                MainBll.UpdateSQL(text4);
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}
