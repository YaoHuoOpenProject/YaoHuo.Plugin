using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class LockUser_List_add : MyPageWap
	{
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string toclassid = "";

		public string touserid = "";

		public string backurlid = "";

		public string lockdate = "";

		public string INFO = "";

		public string ERROR = "";

		public wap_bbsre_Model bbsReVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			while (true)
			{
				action = GetRequestValue("action");
				id = GetRequestValue("id");
				toclassid = GetRequestValue("toclassid");
				touserid = GetRequestValue("touserid");
				backurlid = GetRequestValue("backurlid");
				lockdate = GetRequestValue("lockdate");
				bool flag = !(lockdate == "");
				int num = 1;
				while (true)
				{
					switch (num)
					{
						case 1:
							if (!flag)
							{
								num = 5;
								continue;
							}
							goto case 4;
						case 5:
							if (true)
							{
							}
							lockdate = "0";
							num = 4;
							continue;
						case 2:
							num = 0;
							continue;
						case 6:
							return;
						case 4:
							CheckManagerLvl("04", classVo.adminusername, "bbs/book_list.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid);
							needPassWordToAdmin();
							flag = !(action == "gomod");
							num = 3;
							continue;
						case 3:
							if (!flag)
							{
								num = 2;
								continue;
							}
							return;
						case 0:
							try
							{
								while (true)
								{
								IL_01ee:
									bool flag2 = true;
									user_Model user_Model = null;
									flag = WapTool.IsNumeric(touserid);
									num = 15;
									while (true)
									{
										int num2;
										switch (num)
										{
											case 15:
												if (!flag)
												{
													num = 4;
													continue;
												}
												goto case 2;
											case 17:
												INFO = "NOTALLOW";
												num = 12;
												continue;
											case 21:
												num = 9;
												continue;
											case 9:
												num2 = (WapTool.IsNumeric(toclassid) ? 1 : 0);
												goto IL_0504;
											case 8:
												flag2 = false;
												num = 5;
												continue;
											case 16:
												{
													if (!flag)
													{
														num = 17;
														continue;
													}
													user_lock_Model user_lock_Model = new user_lock_Model();
													user_lock_Model.siteid = long.Parse(siteid);
													user_lock_Model.lockuserid = long.Parse(touserid);
													user_lock_Model.lockdate = long.Parse(lockdate);
													user_lock_Model.operdate = DateTime.Now;
													user_lock_Model.operuserid = long.Parse(userid);
													user_lock_Model.classid = long.Parse(toclassid);
													user_lock_BLL user_lock_BLL = new user_lock_BLL(a);
													user_lock_BLL.Add(user_lock_Model);
													MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "加黑用户ID:" + touserid + "','" + IP + "')");
													INFO = "OK";
													num = 14;
													continue;
												}
											case 5:
											case 13:
												num = 6;
												continue;
											case 6:
												if (WapTool.IsNumeric(lockdate))
												{
													num = 0;
													continue;
												}
												goto IL_0542;
											case 4:
												touserid = "0";
												num = 2;
												continue;
											case 22:
												if (!flag)
												{
													num = 10;
													continue;
												}
												goto case 5;
											case 2:
												user_Model = MainBll.getUserInfo(touserid, siteid);
												flag = user_Model != null;
												num = 1;
												continue;
											case 1:
												if (flag)
												{
													flag = !IsUserManager(touserid, user_Model.managerlvl, classVo.adminusername);
													num = 22;
												}
												else
												{
													num = 8;
												}
												continue;
											case 0:
												num = 18;
												continue;
											case 18:
												if (WapTool.IsNumeric(touserid))
												{
													num = 21;
													continue;
												}
												goto IL_0542;
											case 7:
												INFO = "NOTNUM";
												num = 3;
												continue;
											case 20:
												if (flag)
												{
													flag = flag2;
													num = 16;
												}
												else
												{
													num = 7;
												}
												continue;
											case 10:
												flag2 = false;
												num = 13;
												continue;
											case 19:
												num2 = 0;
												goto IL_0504;
											case 3:
											case 12:
											case 14:
												num = 11;
												continue;
											case 11:
												goto end_IL_0189;
											IL_0504:
												flag = (byte)num2 != 0;
												num = 20;
												continue;
											IL_0542:
												num = 19;
												continue;
										}
										goto IL_01ee;
										//continue;
									end_IL_0189:
										break;
									}
									break;
								}
							}
							catch (Exception ex)
							{
								ERROR = ex.ToString();
							}
							num = 6;
							continue;
					}
					break;
				}
			}
		}
	}
}