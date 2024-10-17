using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Report_List_del : MyPageWap
	{
		private string string_10 = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string page = "";

		public string INFO = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			action = GetRequestValue("action");
			id = GetRequestValue("id");
			page = GetRequestValue("page");
			CheckManagerLvl("04", classVo.adminusername, "bbs/showadmin.aspx?siteid=" + siteid + "&amp;classid=" + classid);
			if (classid == "0")
			{
				CheckManagerLvl("04", "", "bbs/showadmin.aspx?siteid=" + siteid + "&amp;classid=" + classid);
			}
			if (action == "godel")
			{
				try
				{
					wap2_bbs_report_BLL wap2_bbs_report_BLL = new wap2_bbs_report_BLL(string_10);
					wap2_bbs_report_BLL.Delete(long.Parse(siteid), long.Parse(classid), long.Parse(id), "0");
					INFO = "OK";
				}
				catch (Exception ex)
				{
					INFO = ex.ToString();
				}
			}
		}
	}
}