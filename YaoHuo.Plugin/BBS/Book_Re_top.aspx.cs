using System;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_re_top : MyPageWap
    {
		private string string_10 = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string reid = "";

		public string page = "";

		public string lpage = "";

		public string ot = "";

		public string INFO = "";

		public string ERROR = "";

		public string tops = "";

		public wap_bbsre_Model bbsReVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
			{
				ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
			}
			action = GetRequestValue("action");
			id = GetRequestValue("id");
			reid = GetRequestValue("reid");
			page = GetRequestValue("page");
			lpage = GetRequestValue("lpage");
			ot = GetRequestValue("ot");
			tops = GetRequestValue("tops");
			IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
			if (action == "gomod")
			{
				try
				{
					MainBll.UpdateSQL("update wap_bbsre set book_top=" + long.Parse(tops) + " where devid='" + siteid + "' and  id=" + long.Parse(reid));
					INFO = "OK";
					WapTool.ClearDataBBSRe("bbsRe" + siteid + id);
				}
				catch (Exception ex)
				{
					ERROR = ex.ToString();
				}
			}
		}
	}
}