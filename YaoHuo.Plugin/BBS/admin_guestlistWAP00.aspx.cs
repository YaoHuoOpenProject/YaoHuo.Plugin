using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class admin_guestlistWAP00 : MyPageWap
    {
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string linkURL = "";

		public string linkTOP = "";

		public string condition = "";

		public string ERROR = "";

		public string key = "";

		public string ischeck = "";

		public string tositeid = "";

		public List<wap_bbsre_Model> listVo = null;

		public StringBuilder strhtml = new StringBuilder();

		public long kk = 1L;

		public long index = 0L;

		public long total = 0L;

		public long pageSize = 10L;

		public long CurrentPage = 1L;

		protected void Page_Load(object sender, EventArgs e)
		{
			while (true)
			{
				action = GetRequestValue("action");
				IsCheckSuperAdmin(userid, userVo.managerlvl, GetUrlQueryString());
				string text = action;
				int num = 3;
				while (true)
				{
					switch (num)
					{
						case 3:
							if (text != null)
							{
								num = 12;
								continue;
							}
							goto case 5;
						case 8:
							if (1 == 0)
							{
							}
							return;
						case 7:
							return;
						case 11:
							num = 0;
							continue;
						case 0:
							if (text == "gocheck")
							{
								gocheck();
								num = 8;
							}
							else
							{
								num = 10;
							}
							continue;
						case 5:
							showclass();
							num = 1;
							continue;
						case 1:
							return;
						case 12:
							num = 6;
							continue;
						case 6:
							if (text == "class")
							{
								showclass();
								num = 7;
							}
							else
							{
								num = 11;
							}
							continue;
						case 9:
							return;
						case 4:
							num = 5;
							continue;
						case 10:
							num = 2;
							continue;
						case 2:
							if (text == "gocheckall")
							{
								gocheckall();
								num = 9;
							}
							else
							{
								num = 4;
							}
							continue;
					}
					break;
				}
			}
		}

		public void showclass()
		{
			//Discarded unreachable code: IL_018a
			bool flag = default(bool);
			while (true)
			{
				key = GetRequestValue("key");
				ischeck = GetRequestValue("ischeck");
				tositeid = GetRequestValue("tositeid");
				condition = " 1=1 ";
				int num = 0;
				while (true)
				{
					int num2;
					switch (num)
					{
						case 0:
							num = ((!(tositeid != "")) ? 8 : 17);
							continue;
						case 17:
							num = 9;
							continue;
						case 9:
							num2 = ((!(tositeid != "0")) ? 1 : 0);
							goto IL_0572;
						case 10:
							classVo.classid = 0L;
							classVo.position = "left";
							classVo.classname = "管理 所有回复:" + key;
							classVo.siteimg = "NetImages/no.gif";
							num = 11;
							continue;
						case 7:
							condition = condition + " and ischeck=" + ischeck;
							if (true)
							{
							}
							num = 13;
							continue;
						case 4:
						case 12:
							flag = !(classid == "0");
							num = 14;
							continue;
						case 14:
							if (!flag)
							{
								num = 10;
								continue;
							}
							classVo.classname = "管理 " + classVo.classname + ":" + key;
							condition = condition + "  and   classid=" + long.Parse(classid) + " ";
							num = 3;
							continue;
						case 13:
							try
							{
								while (true)
								{
									pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
									wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
									flag = !(GetRequestValue("getTotal") != "");
									num = 3;
									while (true)
									{
										switch (num)
										{
											case 3:
												if (!flag)
												{
													num = 9;
													continue;
												}
												total = wap_bbsre_BLL.GetListCount(condition);
												num = 7;
												continue;
											case 10:
												CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
												CheckFunction("bbs", CurrentPage);
												index = pageSize * (CurrentPage - 1);
												linkURL = http_start + "bbs/admin_guestlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;ischeck=" + ischeck + "&amp;tositeid=" + tositeid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
												linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
												linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL);
												flag = CurrentPage != 1;
												num = 5;
												continue;
											case 5:
												if (!flag)
												{
													num = 12;
													continue;
												}
												goto case 13;
											case 11:
												WapTool.setTotal(siteid, classid, total);
												num = 6;
												continue;
											case 12:
												flag = classVo.total == total;
												num = 4;
												continue;
											case 4:
												if (!flag)
												{
													num = 11;
													continue;
												}
												goto case 6;
											case 6:
												num = 13;
												continue;
											case 1:
												CurrentPage = long.Parse(GetRequestValue("page"));
												num = 10;
												continue;
											case 9:
												total = long.Parse(GetRequestValue("getTotal"));
												num = 8;
												continue;
											case 7:
											case 8:
												flag = !(GetRequestValue("page") != "");
												num = 2;
												continue;
											case 2:
												if (!flag)
												{
													num = 1;
													continue;
												}
												goto case 10;
											case 13:
												listVo = wap_bbsre_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
												num = 0;
												continue;
											case 0:
												return;
										}
										break;
									}
								}
							}
							catch (Exception ex)
							{
								ERROR = WapTool.ErrorToString(ex.ToString());
								return;
							}
						case 6:
							if (flag)
							{
								tositeid = "0";
								num = 4;
							}
							else
							{
								num = 5;
							}
							continue;
						case 8:
							num2 = 1;
							goto IL_0572;
						case 5:
							condition = condition + " and devid='" + long.Parse(tositeid) + "'";
							num = 12;
							continue;
						case 15:
							flag = !(ischeck.Trim() != "");
							num = 16;
							continue;
						case 16:
							if (!flag)
							{
								num = 7;
								continue;
							}
							goto case 13;
						case 3:
						case 11:
							flag = !(key.Trim() != "");
							num = 2;
							continue;
						case 2:
							if (!flag)
							{
								num = 1;
								continue;
							}
							goto case 15;
						case 1:
							{
								condition = condition + " and content like '%" + key + "%' ";
								num = 15;
								continue;
							}
						IL_0572:
							flag = (byte)num2 != 0;
							num = 6;
							continue;
					}
					break;
				}
			}
		}

		public void gocheck()
		{
			string requestValue = GetRequestValue("id");
			string requestValue2 = GetRequestValue("state");
			try
			{
				MainBll.UpdateSQL("update wap_bbsre set ischeck=" + long.Parse(requestValue2) + " where id=" + long.Parse(requestValue));
			}
			catch (Exception ex)
			{
				ERROR = WapTool.ErrorToString(ex.ToString());
			}
			if (true)
			{
			}
			showclass();
		}

		public void gocheckall()
		{
			string requestValue = GetRequestValue("state");
			try
			{
				MainBll.UpdateSQL("update wap_bbsre set ischeck=" + long.Parse(requestValue) + " where ischeck <>" + long.Parse(requestValue));
			}
			catch (Exception ex)
			{
				ERROR = WapTool.ErrorToString(ex.ToString());
			}
			if (true)
			{
			}
			showclass();
		}
	}
}