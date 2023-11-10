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
                base.Server.Transfer("/bbs/book_view.aspx?siteid=" + model.userid + "&classid=" + model.book_classid + "&id=" + text + "&lpage=" + text2 + "&stype=" + text3);
            }
            else
            {
                base.Response.Write("<html><title>提示</title><body>抱歉，找不到ID=" + text + "此记录！<br/><br/><a href=\"javascript:;\" onClick=\"javascript:history.back(-1);\">返回上级</a>-<a href=\"/\">返回首页</a></body></html>");
            }
        }
    }
}
