using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class View : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = base.Request.QueryString.Get("id");
            if (!WapTool.IsNumeric(text))
            {
                text = "0";
            }
            string text2 = base.Request.QueryString.Get("lpage");
            string text3 = base.Request.QueryString.Get("stype");
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(WapTool._InstanceName);
            wap_bbs_Model model = wap_bbs_BLL.GetModel(long.Parse(text));
            if (model != null)
            {
                base.Server.Transfer("/bbs/book_view.aspx?classid=" + model.book_classid + "&id=" + text);
            }
            else
            {
                base.Response.Write("<!DOCTYPE html><html><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\"><title>提示信息</title><body>抱歉，找不到ID=" + text + "的记录！<br/><br/><a href=\"/\">返回首页</a></body></html>");
            }
        }
    }
}