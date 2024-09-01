using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ResetVIP : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

		public string INFO = "";

		public string ERROR = "";

		public string tousername = "";

		public string topw = "";

		public string backurl = "";

		public string getuserid = "";

		public user_Model toUserVo = null;

		public string KL_Check_Repeat_Nickname = PubConstant.GetAppString("KL_Check_Repeat_Nickname");

		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = default(bool);
			while (true)
			{
				string requestValue = GetRequestValue("action");
				int num = 11;
				while (true)
				{
					int num2;
					int num4;
					int num3;
					switch (num)
					{
						case 11:
							num = ((!IsCheckManagerLvl("|00|", "")) ? 24 : 9);
							continue;
						case 26:
							num2 = 0;
							goto IL_0222;
						case 13:
							backurl = ToHtm(backurl);
							backurl = HttpUtility.UrlDecode(backurl);
							backurl = WapTool.URLtoWAP(backurl);
							topw = GetRequestValue("topw");
							flag = !(requestValue == "godoit");
							num = 25;
							continue;
						case 25:
							if (!flag)
							{
								num = 16;
								continue;
							}
							return;
						case 8:
							num = 4;
							continue;
						case 4:
							num4 = ((!(backurl == "")) ? 1 : 0);
							goto IL_0267;
						case 16:
							flag = !(PubConstant.md5(topw) != userVo.password);
							num = 3;
							continue;
						case 3:
							if (flag)
							{
								num = 22;
								continue;
							}
							if (true)
							{
							}
							num = 2;
							continue;
						case 17:
							needPassWordToAdmin();
							backurl = base.Request.QueryString.Get("backurl");
							num = 1;
							continue;
						case 1:
							num = ((backurl != null) ? 6 : 26);
							continue;
						case 28:
							if (!flag)
							{
								num = 12;
								continue;
							}
							goto case 19;
						case 2:
							INFO = "NULL";
							num = 18;
							continue;
						case 7:
							if (!flag)
							{
								num = 23;
								continue;
							}
							goto case 13;
						case 0:
							num4 = 0;
							goto IL_0267;
						case 19:
							num = 20;
							continue;
						case 20:
							num = ((backurl != null) ? 8 : 0);
							continue;
						case 23:
							backurl = "admin/basesitemodifywml.aspx?siteid=" + siteid;
							num = 13;
							continue;
						case 22:
							try
							{
								MainBll.UpdateSQL("update [user] set sessiontimeout=0,endtime=null where siteid=" + siteid + " and endtime is not null and datediff(day, getdate(),endtime) < 1 ");
								INFO = "OK";
							}
							catch (Exception ex)
							{
								ERROR = WapTool.ErrorToString(ex.ToString());
							}
							num = 14;
							continue;
						case 9:
							num = 10;
							continue;
						case 10:
							num3 = ((siteVo.siteid == userVo.userid) ? 1 : 0);
							goto IL_0371;
						case 5:
							if (!flag)
							{
								num = 21;
								continue;
							}
							goto case 17;
						case 12:
							backurl = base.Request.Form.Get("backurl");
							num = 19;
							continue;
						case 14:
						case 18:
							num = 27;
							continue;
						case 27:
							return;
						case 21:
							ShowTipInfo("抱歉，只有正超级管理员才有权限操作。", "");
							num = 17;
							continue;
						case 24:
							num3 = 0;
							goto IL_0371;
						case 6:
							num = 15;
							continue;
						case 15:
							{
								num2 = ((!(backurl == "")) ? 1 : 0);
								goto IL_0222;
							}
						IL_0222:
							flag = (byte)num2 != 0;
							num = 28;
							continue;
						IL_0371:
							flag = (byte)num3 != 0;
							num = 5;
							continue;
						IL_0267:
							flag = (byte)num4 != 0;
							num = 7;
							continue;
					}
					break;
				}
			}
		}
	}
}