using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class ModifyUserName : MyPageWap
	{
		private string a = PubConstant.GetAppString("InstanceName");

		public string INFO = "";

		public string ERROR = "";

		public string tousername = "";

		public string touserid = "";

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
				int num = 5;
				while (true)
				{
					int num4;
					int num3;
					int num2;
					switch (num)
					{
						case 5:
							num = ((!IsCheckManagerLvl("|00|", "")) ? 28 : 23);
							continue;
						case 40:
							tousername = GetRequestValue("tousername");
							tousername = WapTool.left(tousername, 20);
							getuserid = MainBll.isHasExistUserName(tousername);
							flag = !(tousername.Trim() == "");
							num = 24;
							continue;
						case 24:
							if (!flag)
							{
								num = 37;
								continue;
							}
							flag = !(getuserid != "0");
							num = 6;
							continue;
						case 19:
							flag = !(touserid != "");
							num = 29;
							continue;
						case 29:
							if (!flag)
							{
								num = 25;
								continue;
							}
							goto case 0;
						case 10:
							num4 = 0;
							goto IL_0267;
						case 4:
							INFO = "HASEXIST";
							num = 17;
							continue;
						case 2:
							ShowTipInfo("抱歉，只有正超级管理员才有权限操作。", "");
							num = 32;
							continue;
						case 0:
							flag = !(requestValue == "gomod");
							num = 12;
							continue;
						case 12:
							if (!flag)
							{
								num = 40;
								continue;
							}
							return;
						case 3:
							ShowTipInfo("抱歉，正站长对应的用户名不在此修改，请到注册会员管理中修改。", "");
							if (true)
							{
							}
							num = 19;
							continue;
						case 38:
							if (!flag)
							{
								num = 27;
								continue;
							}
							goto case 20;
						case 15:
							try
							{
								MainBll.UpdateSQL("update [user] set username='" + tousername + "' where siteid=" + siteid + " and userid=" + toUserVo.userid);
								toUserVo.username = tousername;
								INFO = "OK";
							}
							catch (Exception ex)
							{
								ERROR = WapTool.ErrorToString(ex.ToString());
							}
							num = 31;
							continue;
						case 17:
						case 31:
						case 33:
							num = 11;
							continue;
						case 11:
							return;
						case 30:
							num3 = 0;
							goto IL_0446;
						case 23:
							num = 26;
							continue;
						case 26:
							num2 = ((siteVo.siteid == userVo.userid) ? 1 : 0);
							goto IL_04e4;
						case 35:
							num = 18;
							continue;
						case 18:
							num4 = ((!(backurl == "")) ? 1 : 0);
							goto IL_0267;
						case 32:
							needPassWordToAdmin();
							backurl = base.Request.QueryString.Get("backurl");
							num = 1;
							continue;
						case 1:
							num = ((backurl != null) ? 35 : 10);
							continue;
						case 20:
							num = 7;
							continue;
						case 7:
							num = ((backurl != null) ? 8 : 30);
							continue;
						case 37:
							INFO = "NULL";
							num = 33;
							continue;
						case 22:
							if (!flag)
							{
								num = 21;
								continue;
							}
							goto case 34;
						case 21:
							backurl = "admin/basesitemodifywml.aspx?siteid=" + siteid;
							num = 34;
							continue;
						case 39:
							touserid = "0";
							num = 36;
							continue;
						case 27:
							backurl = base.Request.Form.Get("backurl");
							num = 20;
							continue;
						case 16:
							if (!flag)
							{
								num = 2;
								continue;
							}
							goto case 32;
						case 6:
							num = ((!flag) ? 4 : 15);
							continue;
						case 8:
							num = 14;
							continue;
						case 14:
							num3 = ((!(backurl == "")) ? 1 : 0);
							goto IL_0446;
						case 34:
							backurl = ToHtm(backurl);
							backurl = HttpUtility.UrlDecode(backurl);
							backurl = WapTool.URLtoWAP(backurl);
							touserid = GetRequestValue("touserid");
							flag = !(touserid == siteid);
							num = 9;
							continue;
						case 9:
							if (!flag)
							{
								num = 3;
								continue;
							}
							goto case 19;
						case 28:
							num2 = 0;
							goto IL_04e4;
						case 36:
							toUserVo = MainBll.getUserInfo(touserid, siteid);
							num = 0;
							continue;
						case 25:
							flag = WapTool.IsNumeric(touserid);
							num = 13;
							continue;
						case 13:
							{
								if (!flag)
								{
									num = 39;
									continue;
								}
								goto case 36;
							}
						IL_0267:
							flag = (byte)num4 != 0;
							num = 38;
							continue;
						IL_04e4:
							flag = (byte)num2 != 0;
							num = 16;
							continue;
						IL_0446:
							flag = (byte)num3 != 0;
							num = 22;
							continue;
					}
					break;
				}
			}
		}
	}
}