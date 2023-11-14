using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
	public class Book_re_addfileShow : PageWap
	{

		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string reid = "";

		public string page = "";

		public string lpage = "";

		public string ot = "";

		public string INFO = "";

		public string ERROR = "";

		public wap_bbsre_Model bbsReVo = null;

		public List<wap2_attachment_Model> dlist = new List<wap2_attachment_Model>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
			{
				ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
			}
			action = GetRequestValue("action");
			id = GetRequestValue("id");
			reid = GetRequestValue("reid");
			page = GetRequestValue("page");
			lpage = GetRequestValue("lpage");
			ot = GetRequestValue("ot");
			wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
			bbsReVo = wap_bbsre_BLL.GetModel(long.Parse(reid));
			wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(a);
			dlist = wap2_attachment_BLL.GetListVo(" book_type='bbsre' and book_id=" + long.Parse(reid));
		}
	}
}
