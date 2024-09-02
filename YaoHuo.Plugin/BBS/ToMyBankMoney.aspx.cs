using KeLin.ClassManager;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ToMyBankMoney : MyPageWap
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

        public long mainmoney = 0L;

        public long lvlmoney = 0L;

        public long allmoney = 0L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            tomoney = GetRequestValue("tomoney");
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
            mainmoney = WapTool.getMoneyRegular(siteVo.moneyregular, 7);
            lvlmoney = WapTool.getLvLRegular(siteVo.lvlRegular, 7);

            // 移除了手续费为0时关闭功能的代码

            addMoneyToMyBank();
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
            if (!WapTool.IsNumeric(tomoney) || tomoney.IndexOf('-') >= 0)
            {
                INFO = "NUM";
                return;
            }
            if (long.Parse(tomoney) < 1L || userVo.money < 1L)
            {
                INFO = "MAX";
                return;
            }
            if (userVo.money < long.Parse(tomoney))
            {
                INFO = "NOMONEY";
                return;
            }
            if (userVo.myBankMoney < 1L)
            {
                MainBll.UpdateSQL("update [user] set money=money - " + tomoney + ",mybankmoney=mybankmoney + " + tomoney + ",mybanktime=getdate() where siteid=" + siteid + " and userid=" + userid);
            }
            else
            {
                MainBll.UpdateSQL("update [user] set money=money - " + tomoney + ",mybankmoney=mybankmoney + " + tomoney + " where siteid=" + siteid + " and userid=" + userid);
            }
            string text = "恭喜您将" + tomoney + "个存入银行！";
            string text2 = "操作时间:" + DateTime.Now;
            string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
            text3 = text3 + "  values(" + siteid + "," + siteid + ",'" + siteVo.nickname + "','" + text + "','" + text2 + "'," + userid + ",1)";
            MainBll.UpdateSQL(text3);
            SaveBankLog(userid, "存入银行", "-" + tomoney.ToString(), userid, nickname, "将币存入银行");
            INFO = "OK";
        }

        public void subMoney()
        {
            if (!WapTool.IsNumeric(tomoney) || tomoney.IndexOf('-') >= 0)
            {
                INFO = "NUM";
                return;
            }
            if (long.Parse(tomoney) < 0L || userVo.myBankMoney < 1L)
            {
                INFO = "MAX";
                return;
            }
            long num = long.Parse(tomoney) * mainmoney / 100L;
            allmoney = long.Parse(tomoney) + num;
            if (userVo.myBankMoney < allmoney)
            {
                INFO = "NOBANKMONEY";
                return;
            }
            MainBll.UpdateSQL("update [user] set money=money + " + tomoney + ",mybankmoney=mybankmoney - " + allmoney + " where siteid=" + siteid + " and userid=" + userid);
            var handlingFeeMsg = num != 0 ? $"，手续费{num}妖晶" : "";
            string text = $"您从银行取出{tomoney}妖晶{handlingFeeMsg}";
            string text2 = "操作时间:" + DateTime.Now;
            string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
            string strSQL = text3 + "  values(" + siteid + "," + siteid + ",'" + siteVo.nickname + "','" + text + "','" + text2 + "'," + userid + ",1)";
            MainBll.UpdateSQL(strSQL);
            SaveBankLog(userid, "取出银行", tomoney.ToString(), userid, nickname, $"从银行取币{handlingFeeMsg}");
            INFO = "OK";
        }
    }
}