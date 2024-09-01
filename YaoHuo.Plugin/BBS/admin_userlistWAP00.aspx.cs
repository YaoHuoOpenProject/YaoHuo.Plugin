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
	public class admin_userlistWAP00 : MyPageWap
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

		public List<wap_bbs_Model> listVo = null;

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
				int num = 0;
				while (true)
				{
					switch (num)
					{
						case 0:
							if (text != null)
							{
								num = 4;
								continue;
							}
							goto case 11;
						case 6:
							if (1 == 0)
							{
							}
							return;
						case 9:
							return;
						case 12:
							num = 2;
							continue;
						case 2:
							if (text == "gocheck")
							{
								gocheck();
								num = 6;
							}
							else
							{
								num = 10;
							}
							continue;
						case 11:
							showclass();
							num = 3;
							continue;
						case 3:
							return;
						case 4:
							num = 5;
							continue;
						case 5:
							if (text == "class")
							{
								showclass();
								num = 9;
							}
							else
							{
								num = 12;
							}
							continue;
						case 1:
							return;
						case 7:
							num = 11;
							continue;
						case 10:
							num = 8;
							continue;
						case 8:
							if (text == "gocheckall")
							{
								gocheckall();
								num = 1;
							}
							else
							{
								num = 7;
							}
							continue;
					}
					break;
				}
			}
		}

		public void showclass()
		{
			bool flag = default(bool);
			while (true)
			{
				key = GetRequestValue("key");
				ischeck = GetRequestValue("ischeck");
				tositeid = GetRequestValue("tositeid");
				condition = " 1=1 ";
				int num = 2;
				while (true)
				{
					int num2;
					switch (num)
					{
						case 2:
							num = ((!(tositeid != "")) ? 10 : 3);
							continue;
						case 3:
							num = 9;
							continue;
						case 9:
							num2 = ((!(tositeid != "0")) ? 1 : 0);
							goto IL_0572;
						case 4:
							classVo.classid = 0L;
							classVo.position = "left";
							classVo.classname = "管理 所有论坛内容:" + key;
							classVo.siteimg = "NetImages/no.gif";
							num = 16;
							continue;
						case 15:
							condition = condition + " and ischeck=" + ischeck;
							if (true)
							{
							}
							num = 11;
							continue;
						case 0:
						case 5:
							flag = !(classid == "0");
							num = 14;
							continue;
						case 14:
							if (!flag)
							{
								num = 4;
								continue;
							}
							classVo.classname = "管理 " + classVo.classname + ":" + key;
							condition = condition + "  and   book_classid=" + long.Parse(classid) + " ";
							num = 13;
							continue;
						case 11:
							try
							{
								while (true)
								{
									pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
									wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
									flag = !(GetRequestValue("getTotal") != "");
									num = 11;
									while (true)
									{
										switch (num)
										{
											case 11:
												if (!flag)
												{
													num = 12;
													continue;
												}
												total = wap_bbs_BLL.GetListCount(condition);
												num = 1;
												continue;
											case 5:
												CheckFunction("bbs", CurrentPage);
												CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
												index = pageSize * (CurrentPage - 1);
												linkURL = http_start + "bbs/admin_userlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;ischeck=" + ischeck + "&amp;tositeid=" + tositeid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
												linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
												linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL);
												flag = CurrentPage != 1;
												num = 3;
												continue;
											case 3:
												if (!flag)
												{
													num = 9;
													continue;
												}
												goto case 13;
											case 6:
												WapTool.setTotal(siteid, classid, total);
												num = 4;
												continue;
											case 9:
												flag = classVo.total == total;
												num = 10;
												continue;
											case 10:
												if (!flag)
												{
													num = 6;
													continue;
												}
												goto case 4;
											case 4:
												num = 13;
												continue;
											case 7:
												CurrentPage = long.Parse(GetRequestValue("page"));
												num = 5;
												continue;
											case 12:
												total = long.Parse(GetRequestValue("getTotal"));
												num = 8;
												continue;
											case 1:
											case 8:
												flag = !(GetRequestValue("page") != "");
												num = 0;
												continue;
											case 0:
												if (!flag)
												{
													num = 7;
													continue;
												}
												goto case 5;
											case 13:
												listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
												num = 2;
												continue;
											case 2:
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
						case 8:
							if (flag)
							{
								tositeid = "0";
								num = 0;
							}
							else
							{
								num = 7;
							}
							continue;
						case 10:
							num2 = 1;
							goto IL_0572;
						case 7:
							condition = condition + " and userid=" + long.Parse(tositeid);
							num = 5;
							continue;
						case 17:
							flag = !(ischeck.Trim() != "");
							num = 6;
							continue;
						case 6:
							if (!flag)
							{
								num = 15;
								continue;
							}
							goto case 11;
						case 13:
						case 16:
							flag = !(key.Trim() != "");
							num = 1;
							continue;
						case 1:
							if (!flag)
							{
								num = 12;
								continue;
							}
							goto case 17;
						case 12:
							{
								condition = condition + " and book_title like '%" + key + "%' ";
								num = 17;
								continue;
							}
						IL_0572:
							flag = (byte)num2 != 0;
							num = 8;
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
				MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue2) + " where id=" + long.Parse(requestValue));
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
				MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue) + " where ischeck <>" + long.Parse(requestValue));
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