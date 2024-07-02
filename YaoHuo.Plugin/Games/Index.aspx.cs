using System;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.Games
{
    public class Index : MyPageWap
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = base.Request.QueryString.Get("siteid");
            string text2 = base.Request.QueryString.Get("classid");
            string text3 = base.Request.QueryString.Get("sid");
            string text4 = base.Request.QueryString.Get("action");
            string text5 = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/";
            switch (text4)
            {
                case "webAdmin":
                    base.Response.Redirect("gamesAdmin.aspx?classid=" + text2);
                    break;
                case "webAdmin00":
                    base.Response.Redirect("gamesAdmin00.aspx?classid=" + text2);
                    break;
                case "wapAdmin":
                    base.Response.Redirect(text5 + "Games/gamesAdmin.aspx?siteid=" + text + "&classid=" + text2 + "&sid=" + text3);
                    break;
                case "wapAdmin00":
                    base.Response.Redirect(text5 + "Games/gamesAdmin00.aspx?siteid=" + text + "&classid=" + text2 + "&sid=" + text3);
                    break;
                default:
                    base.Response.Redirect(text5 + "Games/gamesindex.aspx?siteid=" + text + "&classid=" + text2 + "&sid=" + text3);
                    break;
            }
        }
    }
}
