using System;
using System.Linq;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;

namespace YaoHuo.Plugin.XinZhang
{
    public class Book_View_Buy : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string id = "0";

        public XinZhang_Model bookVo = new XinZhang_Model();

        public StringBuilder strhtml = new StringBuilder();

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string action = "";

        public string backurl = "";

        public string pw = "";

        public string ordertype = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_0195
            this.id = base.GetRequestValue("id");
            this.lpage = base.GetRequestValue("lpage");
            this.action = base.GetRequestValue("action");
            this.pw = base.GetRequestValue("pw");
            this.ordertype = base.GetRequestValue("ordertype");
            this.backurl = "xinzhang/book_list.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;lpage=" + this.lpage + "&amp;ordertype=" + this.ordertype;
            base.IsLogin(base.userid, this.backurl);
            try
            {
                while (true)
                {
                    this.bookVo = new XinZhang_BLL(this.a).GetModel(int.Parse(this.id));
                    bool flag;
                    flag = this.bookVo != null;
                    int num;
                    num = 0;
                    while (true)
                    {
                        switch (num)
                        {
                            case 0:
                                if (!flag)
                                {
                                    num = 8;
                                    continue;
                                }
                                goto case 2;
                            case 2:
                                flag = !(this.action == "gomod");
                                num = 11;
                                continue;
                            case 11:
                                if (!flag)
                                {
                                    num = 4;
                                    continue;
                                }
                                goto case 1;
                            case 8:
                                base.ShowTipInfo("已删除！或不存在！", this.backurl);
                                num = 2;
                                continue;
                            case 12:
                                this.INFO = "NOMONEY";
                                num = 6;
                                continue;
                            case 3:
                            case 6:
                            case 10:
                                num = 1;
                                continue;
                            case 13:
                                this.INFO = "NOPASS";
                                num = 3;
                                continue;
                            case 4:
                                flag = !(PubConstant.md5(this.pw).ToLower() != base.userVo.password.ToLower());
                                num = 5;
                                continue;
                            case 5:
                                if (!flag)
                                {
                                    num = 13;
                                    continue;
                                }
                                flag = this.bookVo.XinZhangJiaGe <= base.userVo.money;
                                num = 9;
                                continue;
                            case 9:
                                if (flag)
                                {
                                    //是否有重复的勋章
                                    var myMoneyNames = base.userVo.moneyname.Split('|').ToList();
                                    if (myMoneyNames.IndexOf(this.bookVo.XinZhangTuPian) != -1)
                                    {
                                        //重复勋章
                                        this.INFO = "REPEAT";
                                    }
                                    else
                                    {
                                        //添加勋章
                                        base.userVo.moneyname = base.userVo.moneyname + "|" + this.bookVo.XinZhangTuPian;
                                        base.MainBll.UpdateSQL("update [user] set money=money-" + this.bookVo.XinZhangJiaGe + ",moneyname='" + base.userVo.moneyname + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                                        base.SaveBankLog(base.userid, "购买勋章", "-" + this.bookVo.XinZhangJiaGe, base.userid, base.nickname, "购买勋章[" + this.bookVo.ID + "]");
                                        this.INFO = "OK";
                                    }
                                    num = 10;
                                }
                                else
                                {
                                    num = 12;
                                }
                                continue;
                            case 1:
                                num = 7;
                                continue;
                            case 7:
                                return;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ERROR = ex.ToString();
            }
        }
    }
}
