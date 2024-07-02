using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class MessageList : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string types = "";

        public string backurl = "";

        public string linkTOP = "";

        public string issystem = "";

        public List<wap_message_Model> listVo = null;

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_01d2
            string text = default(string);
            bool flag = default(bool);
            while (true)
            {
                this.action = base.GetRequestValue("action");
                this.issystem = base.GetRequestValue("issystem");
                this.backurl = base.Request.QueryString.Get("backurl");
                int num;
                num = 10;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 10:
                            num = ((this.backurl == null) ? 21 : 5);
                            continue;
                        case 15:
                            num = 20;
                            continue;
                        case 20:
                            if (!(text == "class"))
                            {
                                num = 25;
                                continue;
                            }
                            this.showclass();
                            num = 9;
                            continue;
                        case 14:
                            this.backurl = base.ToHtm(this.backurl);
                            this.backurl = HttpUtility.UrlDecode(this.backurl);
                            this.backurl = WapTool.URLtoWAP(this.backurl);
                            base.IsLogin(base.userid, this.backurl);
                            flag = !(WapTool.getArryString(base.siteVo.Version, '|', 53) == "1");
                            num = 11;
                            continue;
                        case 11:
                            if (!flag)
                            {
                                num = 26;
                                continue;
                            }
                            goto case 2;
                        case 4:
                            this.backurl = base.Request.Form.Get("backurl");
                            num = 16;
                            continue;
                        case 9:
                            return;

                        case 21:
                            num3 = 0;
                            goto IL_02f8;
                        case 2:
                            text = this.action;
                            num = 7;
                            continue;
                        case 7:
                            if (text != null)
                            {
                                num = 15;
                                continue;
                            }
                            goto case 22;
                        case 17:
                            return;

                        case 13:
                            this.backurl = "myfile.aspx?siteid=" + base.siteid;
                            num = 14;
                            continue;
                        case 16:
                            num = 12;
                            continue;
                        case 12:
                            num = ((this.backurl == null) ? 19 : 6);
                            continue;
                        case 8:
                            num = 22;
                            continue;
                        case 22:
                            this.showclass();
                            num = 24;
                            continue;
                        case 24:
                            return;

                        case 19:
                            num2 = 0;
                            goto IL_0343;
                        case 5:
                            num = 1;
                            continue;
                        case 1:
                            num3 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_02f8;
                        case 23:
                            if (!flag)
                            {
                                num = 4;
                                continue;
                            }
                            goto case 16;
                        case 6:
                            num = 0;
                            continue;
                        case 0:
                            num2 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_0343;
                        case 18:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            goto case 14;
                        case 25:
                            num = 3;
                            continue;
                        case 3:
                            num = ((!(text == "godel")) ? 8 : 17);
                            continue;
                        case 26:
                            {
                                base.needPassWordToAdmin();
                                num = 2;
                                continue;
                            }
                        IL_0343:
                            flag = (byte)num2 != 0;
                            num = 18;
                            continue;
                        IL_02f8:
                            flag = (byte)num3 != 0;
                            num = 23;
                            continue;
                    }
                    break;
                }
            }
        }

        public void showclass()
        {
            //Discarded unreachable code: IL_0145
            string requestValue = default(string);
            while (true)
            {
                this.key = base.GetRequestValue("key");
                this.types = base.GetRequestValue("types");
                bool flag;
                flag = !(this.types == "");
                int num;
                num = 9;
                while (true)
                {
                    switch (num)
                    {
                        case 9:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            goto case 0;
                        case 5:
                        case 17:
                            flag = !WapTool.IsNumeric(this.issystem);
                            num = 16;
                            continue;
                        case 16:
                            if (flag)
                            {
                                this.condition += " and issystem <>2 ";
                                num = 14;
                            }
                            else
                            {
                                num = 8;
                            }
                            continue;
                        case 18:
                            requestValue = base.GetRequestValue("id");
                            flag = !WapTool.IsNumeric(requestValue);
                            num = 6;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 4;
                                continue;
                            }
                            goto case 15;
                        case 3:
                            this.condition = this.condition + " and  title like '%" + this.key + "%' ";
                            num = 7;
                            continue;
                        case 0:
                            flag = !(this.action == "save");
                            num = 2;
                            continue;
                        case 2:
                            if (!flag)
                            {
                                num = 18;
                                continue;
                            }
                            goto case 11;
                        case 8:
                            this.condition = this.condition + " and issystem = " + this.issystem;
                            num = 10;
                            continue;
                        case 12:
                            this.condition += " and isnew=2 ";
                            num = 5;
                            continue;
                        case 4:
                            base.MainBll.UpdateSQL("update wap_message set issystem=2 where siteid=" + base.siteid + " and touserid=" + base.userid + " and id=" + requestValue);
                            num = 15;
                            continue;
                        case 7:
                            try
                            {
                                while (true)
                                {
                                    this.pageSize = Convert.ToInt32(base.siteVo.MaxPerPage_Default);
                                    wap_message_BLL wap_message_BLL;
                                    wap_message_BLL = new wap_message_BLL(this.a);
                                    flag = !(base.GetRequestValue("getTotal") != "");
                                    num = 1;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 1:
                                                if (!flag)
                                                {
                                                    num = 2;
                                                    continue;
                                                }
                                                this.total = wap_message_BLL.GetListCount(this.condition);
                                                num = 3;
                                                continue;
                                            case 2:
                                                this.total = long.Parse(base.GetRequestValue("getTotal"));
                                                num = 6;
                                                continue;
                                            case 7:
                                                this.CurrentPage = long.Parse(base.GetRequestValue("page"));
                                                num = 4;
                                                continue;
                                            case 3:
                                            case 6:
                                                flag = !(base.GetRequestValue("page") != "");
                                                num = 5;
                                                continue;
                                            case 5:
                                                if (!flag)
                                                {
                                                    num = 7;
                                                    continue;
                                                }
                                                goto case 4;
                                            case 4:
                                                this.CurrentPage = WapTool.CheckCurrpage(this.total, this.pageSize, this.CurrentPage);
                                                this.index = this.pageSize * (this.CurrentPage - 1);
                                                this.linkURL = base.http_start + "bbs/messagelist.aspx?action=class&amp;siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;key=" + HttpUtility.UrlEncode(this.key) + "&amp;backurl=" + HttpUtility.UrlEncode(this.backurl) + "&amp;getTotal=" + this.total;
                                                this.linkTOP = WapTool.GetPageLinkShowTOP(base.ver, base.lang, this.total, this.pageSize, this.CurrentPage, this.linkURL);
                                                this.linkURL = WapTool.GetPageLink(base.ver, base.lang, Convert.ToInt32(this.total), this.pageSize, this.CurrentPage, this.linkURL);
                                                this.listVo = wap_message_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 1L);
                                                num = 0;
                                                continue;
                                            case 0:
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
                        case 10:
                        case 14:
                            flag = !(this.key.Trim() != "");
                            num = 19;
                            continue;
                        case 19:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 7;
                        case 15:
                            num = 11;
                            continue;
                        case 13:
                            this.types = "0";
                            num = 0;
                            continue;
                        case 11:
                            this.condition = " siteid= " + base.siteid + " and touserid=" + base.userid;
                            flag = !(this.types == "2");
                            num = 1;
                            continue;
                        case 1:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            this.condition += " and isnew < 2 ";
                            num = 17;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
