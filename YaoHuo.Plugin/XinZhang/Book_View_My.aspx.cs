using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Text;

namespace YaoHuo.Plugin.XinZhang
{
    public class Book_View_My : PageWap
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

        public string g = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_00ac
            while (true)
            {
                this.id = base.GetRequestValue("id");
                this.g = base.GetRequestValue("g");
                this.lpage = base.GetRequestValue("lpage");
                this.action = base.GetRequestValue("action");
                this.pw = base.GetRequestValue("pw");
                this.ordertype = base.GetRequestValue("ordertype");
                bool flag;
                flag = !(this.g.Trim() != "");
                int num;
                num = 0;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 1;
                        case 2:
                            try
                            {
                                while (true)
                                {
                                    flag = !(this.action == "godel");
                                    num = 4;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 4:
                                                if (!flag)
                                                {
                                                    num = 1;
                                                    continue;
                                                }
                                                goto case 6;
                                            case 1:
                                                flag = !(PubConstant.md5(this.pw).ToLower() != base.userVo.password.ToLower());
                                                num = 0;
                                                continue;
                                            case 0:
                                                if (!flag)
                                                {
                                                    num = 2;
                                                    continue;
                                                }
                                                base.userVo.moneyname = base.userVo.moneyname.Replace(this.id, "");
                                                base.userVo.moneyname = base.userVo.moneyname.Replace("||", "|");
                                                base.MainBll.UpdateSQL("update [user] set moneyname='" + base.userVo.moneyname + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                                                this.INFO = "OK";
                                                num = 5;
                                                continue;
                                            case 3:
                                            case 5:
                                                num = 6;
                                                continue;
                                            case 2:
                                                this.INFO = "NOPASS";
                                                num = 3;
                                                continue;
                                            case 6:
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
                                return;
                            }
                        case 1:
                            this.backurl = "xinzhang/book_view_my.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;lpage=" + this.lpage + "&amp;ordertype=" + this.ordertype;
                            base.IsLogin(base.userid, this.backurl);
                            num = 2;
                            continue;
                        case 3:
                            this.id = this.g.Replace("删除_", "");
                            num = 1;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
