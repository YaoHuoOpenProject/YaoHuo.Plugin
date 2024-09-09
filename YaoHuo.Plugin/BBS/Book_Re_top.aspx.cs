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

		public string string_11 = "";

		public string reid = "";

		public string page = "";

		public string lpage = "";

		public string string_12 = "";

		public string INFO = "";

		public string ERROR = "";

		public string tops = "";

		public wap_bbsre_Model bbsReVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
			{
				ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
			}
			action = GetRequestValue("action");
			string_11 = GetRequestValue("id");
			reid = GetRequestValue("reid");
			page = GetRequestValue("page");
			lpage = GetRequestValue("lpage");
			string_12 = GetRequestValue("ot");
			tops = GetRequestValue("tops");
			IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + string_12 + "&amp;id=" + string_11);
			if (action == "gomod")
			{
				try
				{
					MainBll.UpdateSQL("update wap_bbsre set book_top=" + long.Parse(tops) + " where devid='" + siteid + "' and  id=" + long.Parse(reid));
					INFO = "OK";
					WapTool.ClearDataBBSRe("bbsRe" + siteid + string_11);
				}
				catch (Exception ex)
				{
					ERROR = ex.ToString();
				}
			}
		}
	}
}