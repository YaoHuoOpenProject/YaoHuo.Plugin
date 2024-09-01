using System;
using System.Collections.Generic;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Admin
{
	public class admin_wapClasslist : MyPageWap
    {
		public static string _InstanceName = PubConstant.GetAppString("InstanceName");

		public List<class_Model> classList = new List<class_Model>();

		public string gopathname = "";

		public string gopath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (true)
			{
			}
			gopath = GetRequestValue("gopath");
			gopathname = GetRequestValue("gopathname");
			needPassWordToAdmin();
			class_BLL class_BLL = new class_BLL(_InstanceName);
			classList = class_BLL.GetFromPathList(long.Parse(siteid), gopath);
		}
	}
}