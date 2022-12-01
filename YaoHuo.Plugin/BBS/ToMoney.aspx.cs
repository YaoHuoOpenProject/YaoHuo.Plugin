using KeLin.ClassManager;
using KeLin.WebSite;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class ToMoney : PageWap
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

        public string maxs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_0232
            bool flag = default(bool);
            string text = default(string);
            while (true)
            {
                this.action = base.Request.Form.Get("action");
                this.tomoney = base.GetRequestValue("tomoney");
                this.touserid = base.GetRequestValue("touserid");
                this.remark = base.GetRequestValue("remark");
                this.type = base.GetRequestValue("type");
                this.backurl = base.Request.QueryString.Get("backurl");
                int num;
                num = 13;
                while (true)
                {
                    int num2;
                    int num4;
                    int num3;
                    switch (num)
                    {
                        case 13:
                            num = ((this.backurl == null) ? 1 : 16);
                            continue;
                        case 38:
                            num = 10;
                            continue;
                        case 10:
                            num = ((base.userVo.managerlvl == "02") ? 27 : 23);
                            continue;
                        case 15:
                            num = 34;
                            continue;
                        case 34:
                            return;

                        case 1:
                            num2 = 0;
                            goto IL_0459;
                        case 18:
                            flag = long.Parse(this.maxs) >= 2;
                            num = 5;
                            continue;
                        case 5:
                            if (!flag)
                            {
                                num = 11;
                                continue;
                            }
                            goto IL_0414;
                        case 19:
                            base.needPassWordToAdmin();
                            text = this.action;
                            num = 0;
                            continue;
                        case 0:
                            if (text != null)
                            {
                                num = 22;
                                continue;
                            }
                            goto case 15;
                        case 11:
                            this.maxs = "1000";
                            num = 6;
                            continue;
                        case 6:
                            if (1 == 0)
                            {
                            }
                            goto IL_0414;
                        case 8:
                            this.maxs = "0";
                            num = 18;
                            continue;
                        case 22:
                            num = 7;
                            continue;
                        case 7:
                            if (!(text == "add"))
                            {
                                num = 3;
                                continue;
                            }
                            this.addMoney();
                            num = 4;
                            continue;
                        case 4:
                            return;

                        case 24:
                            return;

                        case 27:
                            num4 = 0;
                            goto IL_033a;
                        case 23:
                            num = 14;
                            continue;
                        case 14:
                            num4 = ((this.STATE != 2) ? 1 : 0);
                            goto IL_033a;
                        case 20:
                            base.ShowTipInfo(base.GetLang("此功能暂时关闭，如有需要，请联系站长启用！(网站默认配置--[3]会员转虚拟币功能)"), this.backurl);
                            num = 19;
                            continue;
                        case 2:
                            num = 35;
                            continue;
                        case 35:
                            num3 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_0396;
                        case 29:
                            if (!flag)
                            {
                                num = 20;
                                continue;
                            }
                            goto case 19;
                        case 33:
                            num3 = 0;
                            goto IL_0396;
                        case 16:
                            num = 28;
                            continue;
                        case 28:
                            num2 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_0459;
                        case 36:
                            if (!flag)
                            {
                                num = 30;
                                continue;
                            }
                            goto case 21;
                        case 32:
                            num = 26;
                            continue;
                        case 26:
                            num = ((this.backurl != null) ? 2 : 33);
                            continue;
                        case 12:
                            this.backurl = base.Request.Form.Get("backurl");
                            num = 32;
                            continue;
                        case 25:
                            if (this.STATE == 0)
                            {
                                num = 38;
                                continue;
                            }
                            goto case 23;
                        case 9:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            goto case 32;
                        case 17:
                            num = 15;
                            continue;
                        case 3:
                            num = 31;
                            continue;
                        case 31:
                            if (text == "sub")
                            {
                                this.subMoney();
                                num = 24;
                            }
                            else
                            {
                                num = 17;
                            }
                            continue;
                        case 30:
                            this.backurl = "myfile.aspx?siteid=" + base.siteid;
                            num = 21;
                            continue;
                        case 21:
                            this.backurl = base.ToHtm(this.backurl);
                            this.backurl = HttpUtility.UrlDecode(this.backurl);
                            this.backurl = WapTool.URLtoWAP(this.backurl);
                            base.IsLogin(base.userid, this.backurl);
                            this.maxs = WapTool.getArryString(base.siteVo.Version, '|', 22);
                            flag = WapTool.IsNumeric(this.maxs);
                            num = 37;
                            continue;
                        case 37:
                            {
                                if (!flag)
                                {
                                    num = 8;
                                    continue;
                                }
                                goto case 18;
                            }
                        IL_0414:
                            this.STATE = WapTool.getLvLRegular(base.siteVo.Version, 3);
                            num = 25;
                            continue;
                        IL_0459:
                            flag = (byte)num2 != 0;
                            num = 9;
                            continue;
                        IL_0396:
                            flag = (byte)num3 != 0;
                            num = 36;
                            continue;
                        IL_033a:
                            flag = (byte)num4 != 0;
                            num = 29;
                            continue;
                    }
                    break;
                }
            }
        }

        public void addMoney()
        {
            //Discarded unreachable code: IL_0349
            bool flag = default(bool);
            while (true)
            {
                int num;
                num = 1;
                while (true)
                {
                    int num2;
                    int num4;
                    int num3;
                    switch (num)
                    {
                        case 1:
                            if (WapTool.IsNumeric(this.tomoney))
                            {
                                num = 35;
                                continue;
                            }
                            goto IL_00e1;
                        case 38:
                            num2 = 0;
                            goto IL_0382;
                        case 14:
                            num4 = 0;
                            goto IL_0526;
                        case 4:
                            {
                                base.MainBll.UpdateSQL("update [user] set money=money + " + this.tomoney + " where userid=" + this.touserid);
                                //新消息
                                string text = "恭喜，" + base.userVo.nickname + "奖励" + this.tomoney + "个妖晶给您！";
                                string text2 = "原因：" + this.remark;
                                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                string strSQL2 = text3 + "  values(" + base.siteid + "," + base.userid + ",'" + base.userVo.nickname + "','" + text + "','" + text2 + "'," + this.touserid + ",1)";
                                base.SaveBankLog(this.touserid, "转币操作", this.tomoney.ToString(), base.userid, base.nickname, "操作人转币给我");
                                base.MainBll.UpdateSQL(strSQL2);
                                flag = base.siteid == this.touserid;
                                num = 16;
                                continue;
                            }
                        case 16:
                            if (!flag)
                            {
                                num = 24;
                                continue;
                            }
                            goto case 2;
                        case 28:
                            {
                                if (!flag)
                                {
                                    num = 36;
                                    continue;
                                }
                                //扣钱脚本
                                base.MainBll.UpdateSQL("update [user] set money=money - " + this.tomoney + " where userid=" + base.userid);
                                //写日志记录(转币者)
                                base.SaveBankLog(base.userid, "转币操作", "-" + this.tomoney.ToString(), base.userid, base.nickname, "我转币给会员ID(" + this.touserid + ")");
                                //扣除手续费
                                var handlingFee = SendMoneyTool.GetHandlingFee(this.tomoney);
                                this.tomoney = $"{long.Parse(this.tomoney) - handlingFee}";
                                //加钱脚本
                                base.MainBll.UpdateSQL("update [user] set money=money + " + this.tomoney + " where userid=" + this.touserid);
                                //写日志记录(被转币者)
                                base.SaveBankLog(this.touserid, "转币操作", this.tomoney.ToString(), base.userid, base.nickname, "操作人转币给我");
                                //新消息
                                string text = "" + base.userVo.nickname + "转账" + this.tomoney + "个妖晶给您！";
                                string text2 = "备注：" + this.remark;
                                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                string strSQL = text3 + "  values(" + base.siteid + "," + base.userid + ",'" + base.userVo.nickname + "','" + text + "','" + text2 + "'," + this.touserid + ",1)";
                                base.MainBll.UpdateSQL(strSQL);
                                this.INFO = "OK";
                                num = 17;
                                continue;
                            }
                        case 31:
                        case 37:
                            num = 9;
                            continue;
                        case 9:
                            return;

                        case 10:
                            this.INFO = "NUM";
                            num = 6;
                            continue;
                        case 6:
                            return;

                        case 36:
                            this.INFO = "MAXMONEY";
                            num = 18;
                            continue;
                        case 2:
                            this.INFO = "OK";
                            num = 31;
                            continue;
                        case 39:
                            num3 = 0;
                            goto IL_0587;
                        case 15:
                            this.INFO = "CLOSE";
                            num = 0;
                            continue;
                        case 32:
                            if (!flag)
                            {
                                num = 10;
                                continue;
                            }
                            flag = WapTool.isExistUser(base.siteid, this.touserid);
                            num = 34;
                            continue;
                        case 34:
                            num = (flag ? 13 : 27);
                            continue;
                        case 11:
                            num = 3;
                            continue;
                        case 3:
                            num4 = ((base.userVo.money >= long.Parse(this.tomoney)) ? 1 : 0);
                            goto IL_0526;
                        case 13:
                            if (base.userVo.managerlvl == "00")
                            {
                                num = 39;
                            }
                            else
                            {
                                num = 19;
                            }
                            continue;
                        case 24:
                            base.SaveBankLog(base.userid, "转币操作", "-" + this.tomoney.ToString(), base.userid, base.nickname, "我转币给会员ID(" + this.touserid + ")");
                            num = 2;
                            continue;
                        case 0:
                        case 17:
                        case 18:
                        case 22:
                            num = 37;
                            continue;
                        case 35:
                            num = 26;
                            continue;
                        case 26:
                            if (this.tomoney.IndexOf('-') < 0)
                            {
                                num = 5;
                                continue;
                            }
                            goto IL_00e1;
                        case 7:
                            flag = !(base.siteid != this.touserid);
                            num = 25;
                            continue;
                        case 25:
                            if (!flag)
                            {
                                num = 29;
                                continue;
                            }
                            goto case 4;
                        case 23:
                            if (flag)
                            {
                                flag = long.Parse(this.tomoney) <= long.Parse(this.maxs);
                                num = 28;
                            }
                            else
                            {
                                num = 12;
                            }
                            continue;
                        case 29:
                            base.MainBll.UpdateSQL("update [user] set money=money - " + this.tomoney + " where userid=" + base.userid);
                            num = 4;
                            continue;
                        case 21:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            flag = this.STATE != 0;
                            num = 8;
                            continue;
                        case 12:
                            this.INFO = "NOTMONEY";
                            num = 22;
                            continue;
                        case 33:
                            num = ((base.userVo.money >= 500) ? 11 : 14);
                            continue;
                        case 19:
                            num = 20;
                            continue;
                        case 20:
                            num3 = ((!(base.userVo.managerlvl == "01")) ? 1 : 0);
                            goto IL_0587;
                        case 8:
                            num = ((!flag) ? 15 : 33);
                            continue;
                        case 27:
                            this.INFO = "NOTUSER";
                            num = 30;
                            continue;
                        case 30:
                            return;

                        case 5:
                            num = 40;
                            continue;
                        case 40:
                            {
                                num2 = (WapTool.IsNumeric(this.touserid) ? 1 : 0);
                                goto IL_0382;
                            }
                        IL_0587:
                            flag = (byte)num3 != 0;
                            num = 21;
                            continue;
                        IL_0382:
                            flag = (byte)num2 != 0;
                            num = 32;
                            continue;
                        IL_00e1:
                            num = 38;
                            continue;
                        IL_0526:
                            flag = (byte)num4 != 0;
                            num = 23;
                            continue;
                    }
                    break;
                }
            }
        }

        public void subMoney()
        {
            //Discarded unreachable code: IL_0347
            bool flag = default(bool);
            while (true)
            {
                int num;
                num = 6;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 6:
                            if (WapTool.IsNumeric(this.tomoney))
                            {
                                num = 7;
                                continue;
                            }
                            goto IL_00b8;
                        case 2:
                            num3 = 0;
                            goto IL_02f3;
                        case 14:
                            this.INFO = "NOTUSER";
                            num = 4;
                            continue;
                        case 4:
                            return;

                        case 19:
                            num2 = 0;
                            goto IL_02ce;
                        case 8:
                            {
                                base.MainBll.UpdateSQL("update [user] set money=money - " + this.tomoney + " where userid=" + this.touserid);
                                string text;
                                text = "抱歉，" + base.userVo.nickname + "扣除您" + this.tomoney + "个币！";
                                string text2;
                                text2 = "原因:" + this.remark;
                                string text3;
                                text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                string strSQL;
                                strSQL = text3 + "  values(" + base.siteid + "," + base.userid + ",'" + base.userVo.nickname + "','" + text + "','" + text2 + "'," + this.touserid + ",1)";
                                base.MainBll.UpdateSQL(strSQL);
                                base.SaveBankLog(this.touserid, "转币操作", "-" + this.tomoney.ToString(), base.userid, base.nickname, "操作人扣除我币");
                                this.INFO = "OK";
                                num = 5;
                                continue;
                            }
                        case 11:
                            num = 18;
                            continue;
                        case 18:
                            num2 = (WapTool.IsNumeric(this.touserid) ? 1 : 0);
                            goto IL_02ce;
                        case 9:
                            num = 0;
                            continue;
                        case 0:
                            num3 = ((!(base.userVo.managerlvl == "01")) ? 1 : 0);
                            goto IL_02f3;
                        case 5:
                            num = 15;
                            continue;
                        case 15:
                            return;

                        case 12:
                            num = (flag ? 1 : 14);
                            continue;
                        case 3:
                            if (flag)
                            {
                                flag = WapTool.isExistUser(base.siteid, this.touserid);
                                num = 12;
                            }
                            else
                            {
                                num = 10;
                            }
                            continue;
                        case 17:
                            if (!flag)
                            {
                                num = 8;
                                continue;
                            }
                            goto case 5;
                        case 1:
                            if (base.userVo.managerlvl == "00")
                            {
                                num = 2;
                                continue;
                            }
                            num = 9;
                            continue;
                        case 7:
                            num = 16;
                            continue;
                        case 16:
                            if (this.tomoney.IndexOf('-') < 0)
                            {
                                num = 11;
                                continue;
                            }
                            goto IL_00b8;
                        case 10:
                            this.INFO = "NUM";
                            num = 13;
                            continue;
                        case 13:
                            return;
                        IL_02ce:
                            flag = (byte)num2 != 0;
                            num = 3;
                            continue;
                        IL_02f3:
                            flag = (byte)num3 != 0;
                            num = 17;
                            continue;
                        IL_00b8:
                            num = 19;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
