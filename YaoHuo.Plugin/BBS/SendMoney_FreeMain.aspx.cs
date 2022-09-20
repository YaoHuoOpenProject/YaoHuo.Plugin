using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;

public class SendMoney_FreeMain : PageWap
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
            num = 8;
            while (true)
            {
                int num2;
                switch (num)
                {
                    case 8:
                        num = ((!(base.classid != "0")) ? 5 : 3);
                        continue;
                    case 11:
                        num = 9;
                        continue;
                    case 2:
                        this.touserid = "0";
                        if (true)
                        {
                        }
                        num = 6;
                        continue;
                    case 5:
                        num2 = 1;
                        goto IL_06d7;
                    case 3:
                        num = 7;
                        continue;
                    case 7:
                        num2 = ((!(base.classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                        goto IL_06d7;
                    case 9:
                        try
                        {
                            while (true)
                            {
                            IL_0141:
                                string requestValue;
                                requestValue = base.GetRequestValue("sendmoney");
                                num = 6;
                                while (true)
                                {
                                    int num3;
                                    switch (num)
                                    {
                                        case 6:
                                            if (WapTool.IsNumeric(requestValue))
                                            {
                                                num = 7;
                                                continue;
                                            }
                                            goto IL_01db;
                                        case 10:
                                            if (!flag)
                                            {
                                                num = 9;
                                                continue;
                                            }
                                            flag = base.userVo.money >= long.Parse(requestValue);
                                            num = 2;
                                            continue;
                                        case 7:
                                            num = 13;
                                            continue;
                                        case 13:
                                            if (!(this.touserid == "0"))
                                            {
                                                num = 16;
                                                continue;
                                            }
                                            goto IL_01db;
                                        case 3:
                                            num3 = 0;
                                            goto IL_01ec;
                                        case 14:
                                            if (flag)
                                            {
                                                flag = long.Parse(requestValue) >= 1;
                                                num = 10;
                                            }
                                            else
                                            {
                                                num = 8;
                                            }
                                            continue;
                                        case 8:
                                            this.INFO = "ERR";
                                            num = 1;
                                            continue;
                                        case 17:
                                            {
                                                string text3;
                                                text3 = "原因:" + text + "[url=" + base.http_start + "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "]查看[/url]";
                                                base.MainBll.UpdateSQL("update [wap_bbs] set mygetmoney =mygetmoney + " + requestValue + " where id=" + long.Parse(this.id) + " and userid=" + base.siteid + " and book_pub='" + this.touserid + "'");
                                                string text4;
                                                text4 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                                string strSQL;
                                                strSQL = text4 + "  values(" + base.siteid + "," + base.userid + ",'" + base.userVo.nickname + "','" + text2 + "','" + text3 + "'," + this.touserid + ",1)";
                                                base.MainBll.UpdateSQL(strSQL);
                                                base.SaveBankLog(this.touserid, "转币操作", requestValue.ToString(), base.userid, base.nickname, "发贴奖励给我");
                                                base.SaveBankLog(base.userid, "转币操作", "-" + requestValue.ToString(), base.userid, base.nickname, "我奖币给会员ID(" + this.touserid + ")");
                                                this.INFO = "OK";
                                                num = 5;
                                                continue;
                                            }
                                        case 12:
                                            if (!flag)
                                            {
                                                num = 19;
                                                continue;
                                            }
                                            goto case 17;
                                        case 9:
                                            this.INFO = "ERR";
                                            num = 4;
                                            continue;
                                        case 2:
                                            if (flag)
                                            {
                                                base.MainBll.UpdateSQL("update [user] set money=money - " + requestValue + " where userid=" + base.userid);
                                                base.MainBll.UpdateSQL("update [user] set money=money + " + requestValue + " where userid=" + this.touserid);
                                                text2 = "恭喜您，" + base.userVo.nickname + "奖励" + requestValue + "个币给您！";
                                                text = "您的发贴得到奖励！";
                                                flag = !(base.GetRequestValue("remark") != "");
                                                num = 12;
                                            }
                                            else
                                            {
                                                num = 15;
                                            }
                                            continue;
                                        case 15:
                                            this.INFO = "ERR";
                                            num = 18;
                                            continue;
                                        case 16:
                                            num = 0;
                                            continue;
                                        case 0:
                                            num3 = ((!(this.touserid == base.userVo.userid.ToString())) ? 1 : 0);
                                            goto IL_01ec;
                                        case 19:
                                            text = base.GetRequestValue("remark");
                                            num = 17;
                                            continue;
                                        case 1:
                                        case 4:
                                        case 5:
                                        case 18:
                                            num = 11;
                                            continue;
                                        case 11:
                                            goto end_IL_00e8;
                                        IL_01db:
                                            num = 3;
                                            continue;
                                        IL_01ec:
                                            flag = (byte)num3 != 0;
                                            num = 14;
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
                        num = 1;
                        continue;
                    case 1:
                        return;

                    case 10:
                        this.action = base.GetRequestValue("action");
                        this.id = base.GetRequestValue("id");
                        this.reid = base.GetRequestValue("reid");
                        this.page = base.GetRequestValue("page");
                        this.lpage = base.GetRequestValue("lpage");
                        this.ot = base.GetRequestValue("ot");
                        this.touserid = base.GetRequestValue("touserid");
                        flag = WapTool.IsNumeric(this.touserid);
                        num = 12;
                        continue;
                    case 12:
                        if (!flag)
                        {
                            num = 2;
                            continue;
                        }
                        goto case 6;
                    case 4:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        goto case 10;
                    case 6:
                        //base.IsCheckManagerLvl("|00|01|03|04|", base.classVo.adminusername, base.GetUrlQueryString());
                        base.needPassWordToAdmin();
                        flag = !(this.action == "gomod");
                        num = 13;
                        continue;
                    case 13:
                        if (!flag)
                        {
                            num = 11;
                            continue;
                        }
                        return;

                    case 0:
                        {
                            base.ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                            num = 10;
                            continue;
                        }
                    IL_06d7:
                        flag = (byte)num2 != 0;
                        num = 4;
                        continue;
                }
                break;
            }
        }
    }
}
