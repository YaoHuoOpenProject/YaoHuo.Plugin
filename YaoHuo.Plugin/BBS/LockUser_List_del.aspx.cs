using System;
using KeLin.ClassManager;
using YaoHuo.Plugin.WebSite;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
	public class LockUser_List_del : MyPageWap
    {
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string toclassid = "";

		public string touserid = "";

		public string backurlid = "";

		public string delid = "";

		public string lpage = "";

		public string INFO = "";

		public string ERROR = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			while (true)
			{
				action = GetRequestValue("action");
				id = GetRequestValue("id");
				toclassid = GetRequestValue("toclassid");
				touserid = GetRequestValue("touserid");
				backurlid = GetRequestValue("backurlid");
				delid = GetRequestValue("delid");
				CheckManagerLvl("04", classVo.adminusername, "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + "&amp;id=" + id);
				needPassWordToAdmin();
				bool flag = !(action == "godel");
				int num = 1;
				while (true)
				{
					switch (num)
					{
						case 1:
							if (true)
							{
							}
							if (!flag)
							{
								num = 2;
								continue;
							}
							return;
						case 3:
							try
							{
								MainBll.UpdateSQL("delete from user_lock where siteid=" + siteid + " and id=" + long.Parse(delid));
								MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "解除加黑用户ID:" + touserid + "','" + IP + "')");
								INFO = "OK";
							}
							catch (Exception ex)
							{
								ERROR = ex.ToString();
							}
							num = 0;
							continue;
						case 2:
							num = 3;
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