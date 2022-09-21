using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class SendMoney_Free : PageWap
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
            //Discarded unreachable code: IL_008a
            bool flag = default(bool);
            string text = default(string);
            string text2 = default(string);
            while (true)
            {
                int num;
                num = 4;
                while (true)
                {
                    int num2;
                    switch (num)
                    {
                        case 4:
                            num = ((!(base.classid != "0")) ? 1 : 9);
                            continue;
                        case 10:
                            num = 12;
                            continue;
                        case 3:
                            this.touserid = "0";
                            num = 2;
                            continue;
                        case 1:
                            num2 = 1;
                            goto IL_06ba;
                        case 9:
                            num = 7;
                            continue;
                        case 7:
                            num2 = ((!(base.classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                            goto IL_06ba;
                        case 12:
                            try
                            {
                                while (true)
                                {
                                IL_0141:
                                    string requestValue;
                                    requestValue = base.GetRequestValue("sendmoney");
                                    num = 11;
                                    while (true)
                                    {
                                        int num3;
                                        switch (num)
                                        {
                                            case 11:
                                                if (WapTool.IsNumeric(requestValue))
                                                {
                                                    num = 6;
                                                    continue;
                                                }
                                                goto IL_01db;
                                            case 2:
                                                if (!flag)
                                                {
                                                    num = 17;
                                                    continue;
                                                }
                                                flag = base.userVo.money >= long.Parse(requestValue);
                                                num = 10;
                                                continue;
                                            case 6:
                                                num = 18;
                                                continue;
                                            case 18:
                                                if (!(this.touserid == "0"))
                                                {
                                                    num = 13;
                                                    continue;
                                                }
                                                goto IL_01db;
                                            case 15:
                                                num3 = 0;
                                                goto IL_01ec;
                                            case 7:
                                                if (flag)
                                                {
                                                    flag = long.Parse(requestValue) >= 1;
                                                    num = 2;
                                                }
                                                else
                                                {
                                                    num = 5;
                                                }
                                                continue;
                                            case 5:
                                                this.INFO = "ERR";
                                                num = 14;
                                                continue;
                                            case 12:
                                                {
                                                    string text3;
                                                    text3 = "原因:" + text + "[url=" + base.http_start + "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "]查看[/url]";
                                                    base.MainBll.UpdateSQL("update [wap_bbsre] set mygetmoney =mygetmoney + " + requestValue + " where id=" + long.Parse(this.reid) + " and userid=" + this.touserid);
                                                    string text4;
                                                    text4 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                                    string strSQL;
                                                    strSQL = text4 + "  values(" + base.siteid + "," + base.userid + ",'" + base.userVo.nickname + "','" + text2 + "','" + text3 + "'," + this.touserid + ",1)";
                                                    base.MainBll.UpdateSQL(strSQL);
                                                    base.SaveBankLog(this.touserid, "转币操作", requestValue.ToString(), base.userid, base.nickname, "回贴奖励给我");
                                                    base.SaveBankLog(base.userid, "转币操作", "-" + requestValue.ToString(), base.userid, base.nickname, "我奖币给会员ID(" + this.touserid + ")");
                                                    this.INFO = "OK";
                                                    num = 8;
                                                    continue;
                                                }
                                            case 9:
                                                if (!flag)
                                                {
                                                    num = 16;
                                                    continue;
                                                }
                                                goto case 12;
                                            case 17:
                                                this.INFO = "ERR";
                                                num = 19;
                                                continue;
                                            case 10:
                                                if (flag)
                                                {
                                                    base.MainBll.UpdateSQL("update [user] set money=money - " + requestValue + " where userid=" + base.userid);
                                                    base.MainBll.UpdateSQL("update [user] set money=money + " + requestValue + " where userid=" + this.touserid);
                                                    text2 = "恭喜您，" + base.userVo.nickname + "奖励" + requestValue + "个币给您！";
                                                    text = "您的回贴得到奖励！";
                                                    flag = !(base.GetRequestValue("remark") != "");
                                                    num = 9;
                                                }
                                                else
                                                {
                                                    num = 4;
                                                }
                                                continue;
                                            case 4:
                                                this.INFO = "ERR";
                                                num = 3;
                                                continue;
                                            case 13:
                                                num = 0;
                                                continue;
                                            case 0:
                                                num3 = ((!(this.touserid == base.userVo.userid.ToString())) ? 1 : 0);
                                                goto IL_01ec;
                                            case 16:
                                                text = base.GetRequestValue("remark");
                                                num = 12;
                                                continue;
                                            case 3:
                                            case 8:
                                            case 14:
                                            case 19:
                                                num = 1;
                                                continue;
                                            case 1:
                                                goto end_IL_00e8;
                                            IL_01db:
                                                num = 15;
                                                continue;
                                            IL_01ec:
                                                flag = (byte)num3 != 0;
                                                num = 7;
                                                continue;
                                        }
                                        goto IL_0141;
                                    end_IL_00e8:
                                        break;
                                    }
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.ERROR = ex.ToString();
                            }
                            num = 11;
                            continue;
                        case 11:
                            return;

                        case 8:
                            this.action = base.GetRequestValue("action");
                            this.id = base.GetRequestValue("id");
                            this.reid = base.GetRequestValue("reid");
                            this.page = base.GetRequestValue("page");
                            this.lpage = base.GetRequestValue("lpage");
                            this.ot = base.GetRequestValue("ot");
                            this.touserid = base.GetRequestValue("touserid");
                            flag = WapTool.IsNumeric(this.touserid);
                            num = 5;
                            continue;
                        case 5:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 2;
                        case 0:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            goto case 8;
                        case 2:
                            base.IsCheckManagerLvl("|00|01|03|04|", base.classVo.adminusername, base.GetUrlQueryString());
                            base.needPassWordToAdmin();
                            flag = !(this.action == "gomod");
                            num = 6;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 10;
                                continue;
                            }
                            return;

                        case 13:
                            {
                                base.ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                                num = 8;
                                continue;
                            }
                        IL_06ba:
                            flag = (byte)num2 != 0;
                            num = 0;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
