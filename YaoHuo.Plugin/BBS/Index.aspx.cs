using KeLin.ClassManager;
using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class Index : Page
    {
        public string KL_HiddenQuery = PubConstant.GetAppString("KL_HiddenQuery");

        protected void Page_Load(object sender, EventArgs e)
        {
            var text = base.Request.QueryString.Get("siteid");
            var classid = base.Request.QueryString.Get("classid");
            var sid = base.Request.QueryString.Get("sid");
            var action = base.Request.QueryString.Get("action");
            var url = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/";
            switch (action)
            {
                case "webAdmin":
                    base.Response.Redirect("userlist.aspx?classid=" + classid);
                    break;

                case "webAdmin00":
                    base.Response.Redirect("admin_userlist.aspx?classid=" + classid);
                    break;

                case "wapAdmin":
                    base.Response.Redirect(url + "bbs/admin_userlistWAP.aspx?siteid=" + text + "&classid=" + classid);
                    break;

                case "wapAdmin00":
                    base.Response.Redirect(url + "bbs/admin_userlistWAP00.aspx?siteid=" + text + "&classid=" + classid);
                    break;

                default:
                    if (WapTool.ISAPI_Rewrite3_Open == "1")
                    {
                        base.Response.Redirect(url + "bbslist-" + classid + ".html");
                    }
                    else
                    {
                        base.Response.Redirect(url + "bbs/list.aspx?classid=" + classid);
                    }
                    break;
            }
        }
    }
}
