using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class List : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var classid = base.Request.QueryString.Get("classid");
            if (!WapTool.IsNumeric(classid))
            {
                classid = "0";
            }

            var page = base.Request.QueryString.Get("page");
            if (page != null && page != "")
            {
                page = "&page=" + page;
            }

            var action = base.Request.QueryString.Get("action");
            if (action != null && action != "")
            {
                action = "&action=" + action;
            }

            var siteurl = base.Request.QueryString.Get("siteurl");
            if (siteurl != null && siteurl != "")
            {
                siteurl = "&siteurl=" + siteurl;
            }

            var sitename = base.Request.QueryString.Get("sitename");
            if (sitename != null && sitename != "")
            {
                sitename = "&sitename=" + sitename;
            }

            var stype = base.Request.QueryString.Get("stype");
            if (stype != null && stype != "")
            {
                stype = "&stype=" + stype;
            }

            var classToSiteid = WapTool.GetClassToSiteid(long.Parse(classid));

            base.Server.Transfer("/bbs/book_list.aspx?siteid=" + classToSiteid + "&classid=" + classid + page + action + siteurl + sitename + stype);
        }
    }
}
