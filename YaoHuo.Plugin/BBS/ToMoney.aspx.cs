using KeLin.ClassManager;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ToMoney : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string tomoney = "";

        public string backurl = "";

        public string INFO = "";

        public string ERROR = "";

        public long STATE = 0L;

        public string touserid = "";

        public string remark = "";

        public string type = "";

        public string maxs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = base.Request.Form.Get("action");
            tomoney = GetRequestValue("tomoney");
            touserid = GetRequestValue("touserid");
            remark = GetRequestValue("remark");
            type = GetRequestValue("type");
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "myfile.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            IsLogin(userid, backurl);
            maxs = WapTool.getArryString(siteVo.Version, '|', 22);
            if (!WapTool.IsNumeric(maxs))
            {
                maxs = "0";
            }
            if (long.Parse(maxs) < 2L)
            {
                maxs = "1000";
            }
            STATE = WapTool.getLvLRegular(siteVo.Version, 3);
            if ((STATE == 0L && userVo.managerlvl == "02") || STATE == 2L)
            {
                ShowTipInfo(GetLang("此功能暂时关闭，如有需要，请联系站长启用！(网站默认配置--[3]会员转虚拟币功能)"), backurl);
            }
            needPassWordToAdmin();
            switch (action)
            {
                case "sub":
                    subMoney();
                    break;
                case "add":
                    addMoney();
                    break;
            }
        }

        public void addMoney()
        {
            if (!WapTool.IsNumeric(tomoney) || tomoney.IndexOf('-') >= 0 || !WapTool.IsNumeric(touserid))
            {
                INFO = "NUM";
            }
            else if (!WapTool.isExistUser(siteid, touserid))
            {
                INFO = "NOTUSER";
            }
            else if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
            {
                if (siteid != touserid)
                {
                    MainBll.UpdateSQL("update [user] set money=money - " + tomoney + " where userid=" + userid);
                }
                MainBll.UpdateSQL("update [user] set money=money + " + tomoney + " where userid=" + touserid);
                string text = "恭喜您，" + userVo.nickname + "奖励" + tomoney + "个币给您！";
                string text2 = string.IsNullOrEmpty(remark) ? userVo.nickname + "奖励" + tomoney + "个币给您！" : "原因:" + remark;
                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                text3 = text3 + "  values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text2 + "'," + touserid + ",1)";
                MainBll.UpdateSQL(text3);
                SaveBankLog(touserid, "转币操作", tomoney.ToString(), userid, nickname, "操作人转币给我");
                if (siteid != touserid)
                {
                    SaveBankLog(userid, "转币操作", "-" + tomoney.ToString(), userid, nickname, "我转币给会员ID(" + touserid + ")");
                }
                INFO = "OK";
            }
            else if (STATE == 0L)
            {
                INFO = "CLOSE";
            }
            else if (userVo.money < 500L || userVo.money < long.Parse(tomoney))
            {
                INFO = "NOTMONEY";
            }
            else if (long.Parse(tomoney) > long.Parse(maxs))
            {
                INFO = "MAXMONEY";
            }
            else
            {
                // 扣钱脚本
                MainBll.UpdateSQL("update [user] set money=money - " + tomoney + " where userid=" + userid);
                // 扣除手续费
                var handlingFee = SendMoneyTool.GetHandlingFee(tomoney);
                tomoney = $"{long.Parse(tomoney) - handlingFee}";
                // 加钱脚本
                MainBll.UpdateSQL("update [user] set money=money + " + tomoney + " where userid=" + touserid);
                // 新消息
                string text = "" + userVo.nickname + "转账" + tomoney + "个妖晶给您！";
                string text2 = string.IsNullOrEmpty(remark) ? "对方未填写转账备注。" : "备注:" + remark;
                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                string strSQL = text3 + "  values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text2 + "'," + touserid + ",1)";
                MainBll.UpdateSQL(strSQL);
                // 写日志记录(被转币者)
                SaveBankLog(touserid, "转币操作", tomoney.ToString(), userid, nickname, "操作人转币给我");
                // 写日志记录(转币者)
                SaveBankLog(userid, "转币操作", "-" + tomoney.ToString(), userid, nickname, "我转币给会员ID(" + touserid + ")");
                INFO = "OK";
            }
        }

        public void subMoney()
        {
            if (!WapTool.IsNumeric(tomoney) || tomoney.IndexOf('-') >= 0 || !WapTool.IsNumeric(touserid))
            {
                INFO = "NUM";
            }
            else if (!WapTool.isExistUser(siteid, touserid))
            {
                INFO = "NOTUSER";
            }
            else if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
            {
                MainBll.UpdateSQL("update [user] set money=money - " + tomoney + " where userid=" + touserid);
                string text = "抱歉，" + userVo.nickname + "扣除您" + tomoney + "个币！";
                string text2 = string.IsNullOrEmpty(remark) ? userVo.nickname + "扣除您" + tomoney + "个币！" : "原因:" + remark;
                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                text3 = text3 + "  values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text2 + "'," + touserid + ",1)";
                MainBll.UpdateSQL(text3);
                SaveBankLog(touserid, "转币操作", "-" + tomoney.ToString(), userid, nickname, "操作人扣除我币");
                INFO = "OK";
            }
        }
    }
}