using System;
using KeLin.ClassManager;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class admin_guestlistWAPdel00 : MyPageWap
    {
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string page = "";

		public string INFO = "";

		public string tositeid = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			while (true)
			{
				IsCheckSuperAdmin(userid, userVo.managerlvl, GetUrlQueryString());
				action = GetRequestValue("action");
				id = GetRequestValue("id");
				page = GetRequestValue("page");
				tositeid = GetRequestValue("tositeid");
				bool flag = !(action == "godel");
				int num = 3;
				while (true)
				{
					switch (num)
					{
						case 3:
							if (true)
							{
							}
							if (!flag)
							{
								num = 1;
								continue;
							}
							return;
						case 2:
							try
							{
								MainBll.UpdateSQL("update wap_bbs set book_re=book_re-1 where id= (select top 1 bookid from wap_bbsre where id= " + id + ")");
								MainBll.UpdateSQL("delete from wap_bbsre where  id=" + id);
								INFO = "OK";
							}
							catch (Exception ex)
							{
								INFO = ex.ToString();
							}
							num = 0;
							continue;
						case 1:
							num = 2;
							continue;
						case 0:
							return;
					}
					break;
				}
			}
		}
	}
}