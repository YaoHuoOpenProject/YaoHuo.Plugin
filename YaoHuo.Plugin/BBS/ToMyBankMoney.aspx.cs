using KeLin.ClassManager;
using KeLin.WebSite;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class ToMyBankMoney : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

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
            //Discarded unreachable code: IL_01fd
            string text = default(string);
            bool flag = default(bool);
            while (true)
            {
                this.action = base.GetRequestValue("action");
                this.tomoney = base.GetRequestValue("tomoney");
                this.type = base.GetRequestValue("type");
                this.backurl = base.Request.QueryString.Get("backurl");
                int num;
                num = 17;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 17:
                            num = ((this.backurl == null) ? 8 : 23);
                            continue;
                        case 6:
                            num = 22;
                            continue;
                        case 22:
                            if (!(text == "add"))
                            {
                                num = 16;
                                continue;
                            }
                            this.addMoney();
                            num = 4;
                            continue;
                        case 18:
                            this.backurl = base.ToHtm(this.backurl);
                            this.backurl = HttpUtility.UrlDecode(this.backurl);
                            this.backurl = WapTool.URLtoWAP(this.backurl);
                            base.IsLogin(base.userid, this.backurl);
                            this.mainmoney = WapTool.getMoneyRegular(base.siteVo.moneyregular, 7);
                            this.lvlmoney = WapTool.getLvLRegular(base.siteVo.lvlRegular, 7);
                            flag = this.mainmoney >= 0;//为零时，免手续费
                            num = 26;
                            continue;
                        case 26:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 0;
                        case 25:
                            this.backurl = base.Request.Form.Get("backurl");
                            num = 19;
                            continue;
                        case 4:
                            return;

                        case 8:
                            num3 = 0;
                            goto IL_0331;
                        case 0:
                            base.addMoneyToMyBank();
                            base.needPassWordToAdmin();
                            text = this.action;
                            num = 7;
                            continue;
                        case 7:
                            if (text != null)
                            {
                                num = 6;
                                continue;
                            }
                            goto case 10;
                        case 2:
                            return;

                        case 21:
                            this.backurl = "myfile.aspx?siteid=" + base.siteid;
                            num = 18;
                            continue;
                        case 19:
                            num = 13;
                            continue;
                        case 13:
                            num = ((this.backurl == null) ? 24 : 12);
                            continue;
                        case 14:
                            num = 10;
                            continue;
                        case 10:
                            num = 3;
                            continue;
                        case 3:
                            return;

                        case 24:
                            num2 = 0;
                            goto IL_037c;
                        case 23:
                            num = 5;
                            continue;
                        case 5:
                            num3 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_0331;
                        case 20:
                            if (!flag)
                            {
                                num = 25;
                                continue;
                            }
                            goto case 19;
                        case 12:
                            num = 9;
                            continue;
                        case 9:
                            num2 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_037c;
                        case 11:
                            if (!flag)
                            {
                                num = 21;
                                continue;
                            }
                            goto case 18;
                        case 16:
                            num = 15;
                            continue;
                        case 15:
                            if (text == "sub")
                            {
                                this.subMoney();
                                num = 2;
                            }
                            else
                            {
                                num = 14;
                            }
                            continue;
                        case 1:
                            {
                                base.ShowTipInfo(base.GetLang("此功能暂时关闭，如有需要，请联系站长设置好 币种和经验规则设置--银行取款手续费(不能有小数点)！"), this.backurl);
                                num = 0;
                                continue;
                            }
                        IL_037c:
                            flag = (byte)num2 != 0;
                            num = 11;
                            continue;
                        IL_0331:
                            flag = (byte)num3 != 0;
                            num = 20;
                            continue;
                    }
                    break;
                }
            }
        }

        public void addMoney()
        {
            //Discarded unreachable code: IL_010b
            bool flag = default(bool);
            while (true)
            {
                int num;
                num = 1;
                while (true)
                {
                    int num2;
                    int num3;
                    switch (num)
                    {
                        case 1:
                            num = ((!WapTool.IsNumeric(this.tomoney)) ? 20 : 9);
                            continue;
                        case 18:
                            num = ((long.Parse(this.tomoney) < 1) ? 12 : 0);
                            continue;
                        case 11:
                            if (!flag)
                            {
                                num = 15;
                                continue;
                            }
                            base.MainBll.UpdateSQL("update [user] set money=money - " + this.tomoney + ",mybankmoney=mybankmoney + " + this.tomoney + " where siteid=" + base.siteid + " and userid=" + base.userid);
                            num = 2;
                            continue;
                        case 0:
                            num = 21;
                            continue;
                        case 21:
                            num2 = ((base.userVo.money >= 1) ? 1 : 0);
                            goto IL_0373;
                        case 20:
                            num3 = 0;
                            goto IL_034e;
                        case 9:
                            num = 8;
                            continue;
                        case 8:
                            num3 = ((this.tomoney.IndexOf('-') < 0) ? 1 : 0);
                            goto IL_034e;
                        case 2:
                        case 5:
                            {
                                string text = "恭喜您将" + this.tomoney + "个存入银行！";
                                string text2 = "操作时间:" + DateTime.Now;
                                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                string strSQL = text3 + "  values(" + base.siteid + "," + base.siteid + ",'" + base.siteVo.nickname + "','" + text + "','" + text2 + "'," + base.userid + ",1)";
                                base.MainBll.UpdateSQL(strSQL);
                                base.SaveBankLog(base.userid, "存入银行", "-" + this.tomoney.ToString(), base.userid, base.nickname, "将币存入银行");
                                this.INFO = "OK";
                                num = 13;
                                continue;
                            }
                        case 13:
                            return;

                        case 17:
                            this.INFO = "NUM";
                            num = 3;
                            continue;
                        case 3:
                            return;

                        case 12:
                            num2 = 0;
                            goto IL_0373;
                        case 16:
                            this.INFO = "NOMONEY";
                            num = 7;
                            continue;
                        case 7:
                            return;

                        case 6:
                            num = ((!flag) ? 17 : 18);
                            continue;
                        case 4:
                            if (!flag)
                            {
                                num = 10;
                                continue;
                            }
                            flag = base.userVo.money >= long.Parse(this.tomoney);
                            num = 14;
                            continue;
                        case 14:
                            if (flag)
                            {
                                flag = base.userVo.myBankMoney >= 1;
                                num = 11;
                            }
                            else
                            {
                                num = 16;
                            }
                            continue;
                        case 10:
                            this.INFO = "MAX";
                            num = 19;
                            continue;
                        case 19:
                            return;

                        case 15:
                            {
                                base.MainBll.UpdateSQL("update [user] set money=money - " + this.tomoney + ",mybankmoney=mybankmoney + " + this.tomoney + ",mybanktime=getdate() where siteid=" + base.siteid + " and userid=" + base.userid);
                                num = 5;
                                continue;
                            }
                        IL_0373:
                            flag = (byte)num2 != 0;
                            num = 4;
                            continue;
                        IL_034e:
                            flag = (byte)num3 != 0;
                            num = 6;
                            continue;
                    }
                    break;
                }
            }
        }

        public void subMoney()
        {
            //Discarded unreachable code: IL_03a4
            bool flag = default(bool);
            long num4 = default(long);
            while (true)
            {
                int num;
                num = 12;
                while (true)
                {
                    int num2;
                    int num3;
                    switch (num)
                    {
                        case 12:
                            num = ((!WapTool.IsNumeric(this.tomoney)) ? 17 : 4);
                            continue;
                        case 1:
                            this.INFO = "NUM";
                            num = 3;
                            continue;
                        case 3:
                            return;

                        case 17:
                            num2 = 0;
                            goto IL_00ae;
                        case 16:
                            num = ((!flag) ? 1 : 10);
                            continue;
                        case 4:
                            num = 7;
                            continue;
                        case 7:
                            num2 = ((this.tomoney.IndexOf('-') < 0) ? 1 : 0);
                            goto IL_00ae;
                        case 18:
                            num = 5;
                            continue;
                        case 5:
                            num3 = ((base.userVo.myBankMoney >= 1) ? 1 : 0);
                            goto IL_0117;
                        case 8:
                            if (!flag)
                            {
                                num = 15;
                                continue;
                            }
                            num4 = long.Parse(this.tomoney) * this.mainmoney / 100;
                            this.allmoney = long.Parse(this.tomoney) + num4;
                            flag = base.userVo.myBankMoney >= this.allmoney;
                            num = 11;
                            continue;
                        case 14:
                            this.INFO = "NOBANKMONEY";
                            num = 2;
                            continue;
                        case 11:
                            {
                                if (!flag)
                                {
                                    num = 14;
                                    continue;
                                }
                                //执行金币修改脚本
                                base.MainBll.UpdateSQL("update [user] set money=money + " + this.tomoney + ",mybankmoney=mybankmoney - " + this.allmoney + " where siteid=" + base.siteid + " and userid=" + base.userid);
                                //手续费内容
                                var handlingFeeMsg = num4 != 0 ? $"，手续费:{num4}" : "";
                                //消息发送
                                string text = $"您从银行取出{this.tomoney}个{handlingFeeMsg}！";
                                string text2 = "操作时间:" + DateTime.Now;
                                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                string strSQL = text3 + "  values(" + base.siteid + "," + base.siteid + ",'" + base.siteVo.nickname + "','" + text + "','" + text2 + "'," + base.userid + ",1)";
                                base.MainBll.UpdateSQL(strSQL);
                                //日志
                                base.SaveBankLog(base.userid, "取出银行", this.tomoney.ToString(), base.userid, base.nickname, $"从银行取币{handlingFeeMsg}");
                                this.INFO = "OK";
                                num = 13;
                                continue;
                            }
                        case 6:
                            num3 = 0;
                            goto IL_0117;
                        case 15:
                            this.INFO = "MAX";
                            num = 0;
                            continue;
                        case 0:
                            return;

                        case 2:
                        case 13:
                            num = 9;
                            continue;
                        case 9:
                            return;

                        case 10:
                            {
                                num = ((long.Parse(this.tomoney) >= 1) ? 18 : 6);
                                continue;
                            }
                        IL_00ae:
                            flag = (byte)num2 != 0;
                            num = 16;
                            continue;
                        IL_0117:
                            flag = (byte)num3 != 0;
                            num = 8;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
