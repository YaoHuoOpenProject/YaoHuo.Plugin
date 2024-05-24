using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class SendMoney : PageWap
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
            //Discarded unreachable code: IL_0840
            bool flag = default(bool);
            long sendMoney = default(long);
            long num3 = default(long);
            long hasMoney = default(long);
            while (true)
            {
                int num;
                num = 2;
                while (true)
                {
                    int num2;
                    switch (num)
                    {
                        case 2:
                            num = ((!(base.classid != "0")) ? 6 : 10);
                            continue;
                        case 3:
                            try
                            {
                                while (true)
                                {
                                IL_010a:
                                    string requestValue;
                                    requestValue = base.GetRequestValue("sendmoney");
                                    flag = WapTool.IsNumeric(requestValue);
                                    num = 24;
                                    while (true)
                                    {
                                        int num4;
                                        int num5;
                                        switch (num)
                                        {
                                            case 24:
                                                if (!flag)
                                                {
                                                    num = 8;
                                                    continue;
                                                }
                                                flag = long.Parse(requestValue) >= 1;
                                                num = 5;
                                                continue;
                                            case 0:
                                            case 7:
                                            case 11:
                                            case 27:
                                            case 31:
                                                num = 10;
                                                continue;
                                            case 14:
                                                num = 17;
                                                continue;
                                            case 17:
                                                num4 = ((sendMoney <= 0) ? 1 : 0);
                                                goto IL_06aa;
                                            case 8:
                                                this.INFO = "NULL";
                                                num = 21;
                                                continue;
                                            case 3:
                                                this.INFO = "ERR";
                                                num = 7;
                                                continue;
                                            case 35:
                                                if (!flag)
                                                {
                                                    num = 19;
                                                    continue;
                                                }
                                                flag = !(this.bbsVo.book_pub == this.bbsReVo.userid.ToString());
                                                num = 36;
                                                continue;
                                            case 19:
                                                this.INFO = "ERR";
                                                num = 27;
                                                continue;
                                            case 4:
                                            case 12:
                                                num = 11;
                                                continue;
                                            case 6:
                                                num5 = 1;
                                                goto IL_07e9;
                                            case 1:
                                                num = ((num3 + hasMoney > sendMoney) ? 9 : 14);
                                                continue;
                                            case 9:
                                                num4 = 1;
                                                goto IL_06aa;
                                            case 29:
                                                num = 15;
                                                continue;
                                            case 15:
                                                if (base.userid == this.bbsVo.book_pub)
                                                {
                                                    num = 20;
                                                    continue;
                                                }
                                                goto IL_0248;
                                            case 36:
                                                num = (flag ? 1 : 23);
                                                continue;
                                            case 13:
                                                if (flag)
                                                {
                                                    flag = !(this.bbsVo.book_pub != base.userid);
                                                    num = 35;
                                                }
                                                else
                                                {
                                                    num = 3;
                                                }
                                                continue;
                                            case 18:
                                                {
                                                    base.MainBll.UpdateSQL("update [wap_bbs] set hasMoney=" + (hasMoney + num3) + " where id=" + this.bbsVo.id);
                                                    base.MainBll.UpdateSQL("update [wap_bbsre] set myGetMoney=" + (num3 + this.bbsReVo.myGetMoney) + " where id=" + this.bbsReVo.id);
                                                    base.MainBll.UpdateSQL("update [user] set money=money+" + num3 + " where userid=" + this.bbsReVo.userid);
                                                    base.SaveBankLog(this.bbsReVo.userid.ToString(), "论坛赏分", num3.ToString(), base.userid, base.nickname, "得到贴子赏分[" + this.bbsVo.id + "]");
                                                    string text;
                                                    text = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                                    string strSQL;
                                                    strSQL = string.Concat(text, " values(", base.siteid, ",", base.userid, ",'", base.nickname, "','您得到赏分:", num3, "喽~','时间：", DateTime.Now, "[br]点击查看:[url=/bbs/book_view.aspx?siteid=", base.siteid, "&amp;classid=", base.classid, "&amp;id=", this.bbsVo.id, "]", this.bbsVo.book_title.Replace("[", "［").Replace("]", "］"), "[/url]',", this.bbsReVo.userid, ",1)");
                                                    base.MainBll.UpdateSQL(strSQL);
                                                    this.INFO = "OK";
                                                    WapTool.ClearDataBBSRe("bbsRe" + base.siteid + this.id);
                                                    num = 12;
                                                    continue;
                                                }
                                            case 23:
                                                this.INFO = "ERR";
                                                num = 31;
                                                continue;
                                            case 2:
                                                if (!flag)
                                                {
                                                    num = 16;
                                                    continue;
                                                }
                                                this.INFO = "ERR";
                                                num = 0;
                                                continue;
                                            case 28:
                                                this.INFO = "NULL";
                                                num = 33;
                                                continue;
                                            case 20:
                                                num = 32;
                                                continue;
                                            case 32:
                                                num5 = ((!(this.bbsVo.book_pub != this.bbsReVo.userid.ToString())) ? 1 : 0);
                                                goto IL_07e9;
                                            case 5:
                                                if (flag)
                                                {
                                                    this.bbsReVo = new wap_bbsre_BLL(this.a).GetModel(long.Parse(this.reid));
                                                    this.bbsVo = new wap_bbs_BLL(this.a).GetModel(long.Parse(this.id));
                                                    hasMoney = this.bbsVo.hasMoney;
                                                    sendMoney = this.bbsVo.sendMoney;
                                                    num3 = long.Parse(requestValue);
                                                    flag = this.bbsReVo.bookid == this.bbsVo.id;
                                                    num = 13;
                                                }
                                                else
                                                {
                                                    num = 28;
                                                }
                                                continue;
                                            case 22:
                                                num = 34;
                                                continue;
                                            case 34:
                                                if (num3 != 0)
                                                {
                                                    num = 29;
                                                    continue;
                                                }
                                                goto IL_0248;
                                            case 16:
                                                num = 30;
                                                continue;
                                            case 30:
                                                if (this.bbsReVo.bookid == this.bbsVo.id)
                                                {
                                                    num = 22;
                                                    continue;
                                                }
                                                goto IL_0248;
                                            case 26:
                                                if (flag)
                                                {
                                                    this.INFO = "ERR";
                                                    num = 4;
                                                }
                                                else
                                                {
                                                    num = 18;
                                                }
                                                continue;
                                            case 10:
                                            case 21:
                                            case 33:
                                                num = 25;
                                                continue;
                                            case 25:
                                                goto end_IL_006d;
                                            IL_06aa:
                                                flag = (byte)num4 != 0;
                                                num = 2;
                                                continue;
                                            IL_07e9:
                                                flag = (byte)num5 != 0;
                                                num = 26;
                                                continue;
                                            IL_0248:
                                                num = 6;
                                                continue;
                                        }
                                        goto IL_010a;
                                    end_IL_006d:
                                        break;
                                    }
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.ERROR = ex.ToString();
                            }
                            num = 5;
                            continue;
                        case 4:
                            this.action = base.GetRequestValue("action");
                            this.id = base.GetRequestValue("id");
                            this.reid = base.GetRequestValue("reid");
                            this.page = base.GetRequestValue("page");
                            this.lpage = base.GetRequestValue("lpage");
                            this.ot = base.GetRequestValue("ot");
                            base.IsLogin(base.userid, base.GetUrlQueryString());
                            flag = !(this.action == "gomod");
                            num = 0;
                            continue;
                        case 0:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            return;

                        case 6:
                            num2 = 1;
                            goto IL_0982;
                        case 10:
                            num = 1;
                            continue;
                        case 1:
                            num2 = ((!(base.classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                            goto IL_0982;
                        case 7:
                            num = 3;
                            continue;
                        case 5:
                            return;

                        case 8:
                            base.ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                            num = 4;
                            continue;
                        case 9:
                            {
                                if (!flag)
                                {
                                    num = 8;
                                    continue;
                                }
                                goto case 4;
                            }
                        IL_0982:
                            flag = (byte)num2 != 0;
                            num = 9;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
