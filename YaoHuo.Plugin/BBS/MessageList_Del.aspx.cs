using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class MessageList_Del : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string types = "";

        public string id = "";

        public string backurl = "";

        public string INFO = "";

        public string page = "";

        public string issystem = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_0206
            bool flag = default(bool);
            string text = default(string);
            while (true)
            {
                this.action = base.GetRequestValue("action");
                this.backurl = base.Request.QueryString.Get("backurl");
                this.id = base.Request.QueryString.Get("id");
                this.page = base.Request.QueryString.Get("page");
                this.types = base.Request.QueryString.Get("types");
                this.backurl = base.Request.QueryString.Get("backurl");
                this.issystem = base.Request.QueryString.Get("issystem");
                int num;
                num = 25;
                while (true)
                {
                    int num2;
                    int num3;
                    switch (num)
                    {
                        case 25:
                            num = ((this.backurl == null) ? 1 : 14);
                            continue;
                        case 11:
                            num2 = 0;
                            goto IL_01b8;
                        case 3:
                            num = 16;
                            continue;
                        case 16:
                            num = ((this.backurl != null) ? 30 : 11);
                            continue;
                        case 7:
                            num = 12;
                            continue;
                        case 12:
                            return;

                        case 5:
                            if (!flag)
                            {
                                num = 18;
                                continue;
                            }
                            goto case 19;
                        case 30:
                            num = 17;
                            continue;
                        case 17:
                            num2 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_01b8;
                        case 24:
                            this.backurl = base.Request.Form.Get("backurl");
                            num = 3;
                            continue;
                        case 27:
                            base.needPassWordToAdmin();
                            num = 8;
                            continue;
                        case 14:
                            num = 4;
                            continue;
                        case 4:
                            num3 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_038f;
                        case 0:
                            base.IsLogin(base.userid, this.backurl);
                            flag = !(WapTool.getArryString(base.siteVo.Version, '|', 53) == "1");
                            num = 13;
                            continue;
                        case 13:
                            if (!flag)
                            {
                                num = 27;
                                continue;
                            }
                            goto case 8;
                        case 19:
                            this.backurl = base.ToHtm(this.backurl);
                            this.backurl = HttpUtility.UrlDecode(this.backurl);
                            this.backurl = WapTool.URLtoWAP(this.backurl);
                            flag = WapTool.IsNumeric(this.id);
                            num = 26;
                            continue;
                        case 26:
                            if (!flag)
                            {
                                num = 28;
                                continue;
                            }
                            goto case 0;
                        case 31:
                            return;

                        case 28:
                            this.id = "0";
                            num = 0;
                            continue;
                        case 20:
                            return;

                        case 23:
                            if (!flag)
                            {
                                num = 24;
                                continue;
                            }
                            goto case 3;
                        case 1:
                            num3 = 0;
                            goto IL_038f;
                        case 2:
                            return;

                        case 8:
                            text = this.action;
                            num = 22;
                            continue;
                        case 22:
                            if (text != null)
                            {
                                num = 9;
                                continue;
                            }
                            return;

                        case 18:
                            this.backurl = "myfile.aspx?siteid=" + base.siteid;
                            num = 19;
                            continue;
                        case 6:
                            num = 21;
                            continue;
                        case 21:
                            if (text == "godelall")
                            {
                                this.godelall();
                                num = 2;
                            }
                            else
                            {
                                num = 7;
                            }
                            continue;
                        case 9:
                            num = 29;
                            continue;
                        case 29:
                            if (text == "godel")
                            {
                                this.godel();
                                num = 31;
                            }
                            else
                            {
                                num = 10;
                            }
                            continue;
                        case 10:
                            num = 15;
                            continue;
                        case 15:
                            {
                                if (text == "godelother")
                                {
                                    this.godelother();
                                    num = 20;
                                }
                                else
                                {
                                    num = 6;
                                }
                                continue;
                            }
                        IL_01b8:
                            flag = (byte)num2 != 0;
                            num = 5;
                            continue;
                        IL_038f:
                            flag = (byte)num3 != 0;
                            num = 23;
                            continue;
                    }
                    break;
                }
            }
        }

        public void godelother()
        {
            //Discarded unreachable code: IL_0003
            wap_message_Model model;
            model = new wap_message_BLL(this.a).GetModel(long.Parse(this.id));
            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + base.siteid + " and isnew < 2 and issystem<> 2 and   ((touserid=" + base.userid + " and userid=" + model.userid + ") or (touserid=" + model.userid + " and userid=" + base.userid + ") )");
            this.INFO = "OK";
        }

        public void godel()
        {
            //Discarded unreachable code: IL_0003
            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and touserid=" + long.Parse(base.userid) + " and id=" + long.Parse(this.id));
            this.INFO = "OK";
        }

        public void godelall()
        {
            //Discarded unreachable code: IL_02a3
            while (true)
            {
                bool flag;
                flag = !(this.types == "2");
                int num;
                num = 5;
                while (true)
                {
                    switch (num)
                    {
                        case 5:
                            if (!flag)
                            {
                                num = 9;
                                continue;
                            }
                            flag = this.issystem != "";
                            num = 11;
                            continue;
                        case 14:
                            if (!flag)
                            {
                                num = 10;
                                continue;
                            }
                            goto case 2;
                        case 15:
                            if (flag)
                            {
                                flag = !(this.issystem == "2");
                                num = 14;
                            }
                            else
                            {
                                num = 1;
                            }
                            continue;
                        case 11:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            flag = this.issystem != "0";
                            num = 3;
                            continue;
                        case 10:
                            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and isnew<2 and issystem=2 and touserid=" + long.Parse(base.userid));
                            num = 13;
                            continue;
                        case 9:
                            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and isnew=2 and issystem<>2 and touserid=" + long.Parse(base.userid));
                            num = 6;
                            continue;
                        case 1:
                            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and isnew<2 and issystem=1 and touserid=" + long.Parse(base.userid));
                            num = 4;
                            continue;
                        case 2:
                        case 4:
                        case 7:
                        case 13:
                            num = 0;
                            continue;
                        case 3:
                            if (flag)
                            {
                                flag = this.issystem != "1";
                                num = 15;
                            }
                            else
                            {
                                num = 8;
                            }
                            continue;
                        case 8:
                            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and isnew<2 and issystem=0 and touserid=" + long.Parse(base.userid));
                            num = 2;
                            continue;
                        case 12:
                            base.MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(base.siteid) + " and isnew<2 and issystem<>2 and touserid=" + long.Parse(base.userid));
                            num = 7;
                            continue;
                        case 0:
                        case 6:
                            this.INFO = "OK";
                            return;
                    }
                    break;
                }
            }
        }
    }
}
