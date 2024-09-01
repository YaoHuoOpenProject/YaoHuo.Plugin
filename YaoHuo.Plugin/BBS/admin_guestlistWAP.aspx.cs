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
	public class admin_guestlistWAP : MyPageWap
    {
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string linkURL = "";

		public string linkTOP = "";

		public string condition = "";

		public string ERROR = "";

		public string key = "";

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
				CheckManagerLvl("03", classVo.adminusername, GetUrlQueryString());
				string text = action;
				int num = 5;
				while (true)
				{
					switch (num)
					{
						case 5:
							if (text != null)
							{
								num = 3;
								continue;
							}
							goto case 7;
						case 8:
							return;
						case 14:
							return;
						case 0:
							num = 15;
							continue;
						case 15:
							num = ((text == "godel") ? 1 : 13);
							continue;
						case 6:
							return;
						case 3:
							num = 2;
							continue;
						case 2:
							if (text == "class")
							{
								showclass();
								if (true)
								{
								}
								num = 14;
							}
							else
							{
								num = 0;
							}
							continue;
						case 1:
							return;
						case 7:
							showclass();
							num = 12;
							continue;
						case 12:
							return;
						case 10:
							num = 9;
							continue;
						case 9:
							if (text == "gocheckall")
							{
								gocheckall();
								num = 6;
							}
							else
							{
								num = 4;
							}
							continue;
						case 13:
							num = 11;
							continue;
						case 11:
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
						case 4:
							num = 7;
							continue;
					}
					break;
				}
			}
		}

		public void showclass()
		{
			while (true)
			{
				key = GetRequestValue("key");
				bool flag = !(classid == "0");
				int num = 1;
				while (true)
				{
					switch (num)
					{
						case 1:
							if (!flag)
							{
								num = 4;
								continue;
							}
							classVo.classname = "管理 " + classVo.classname + "回贴:" + key;
							condition = " devid='" + siteid + "' and   classid=" + classid + " ";
							num = 3;
							continue;
						case 2:
							try
							{
								while (true)
								{
									pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
									wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
									flag = !(GetRequestValue("getTotal") != "");
									num = 11;
									while (true)
									{
										switch (num)
										{
											case 11:
												if (!flag)
												{
													num = 9;
													continue;
												}
												total = wap_bbsre_BLL.GetListCount(condition);
												num = 3;
												continue;
											case 1:
												num = 12;
												continue;
											case 10:
												CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
												CheckFunction("bbs", CurrentPage);
												index = pageSize * (CurrentPage - 1);
												linkURL = http_start + "bbs/admin_guestlistWAP.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
												linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
												linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL);
												flag = CurrentPage != 1;
												num = 0;
												continue;
											case 0:
												if (!flag)
												{
													num = 13;
													continue;
												}
												goto case 12;
											case 2:
												WapTool.setTotal(siteid, classid, total);
												num = 1;
												continue;
											case 13:
												flag = classVo.total == total;
												num = 7;
												continue;
											case 7:
												if (!flag)
												{
													num = 2;
													continue;
												}
												goto case 1;
											case 3:
											case 8:
												flag = !(GetRequestValue("page") != "");
												num = 4;
												continue;
											case 4:
												if (!flag)
												{
													num = 6;
													continue;
												}
												goto case 10;
											case 9:
												total = long.Parse(GetRequestValue("getTotal"));
												if (true)
												{
												}
												num = 8;
												continue;
											case 6:
												CurrentPage = long.Parse(GetRequestValue("page"));
												num = 10;
												continue;
											case 12:
												listVo = wap_bbsre_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
												num = 5;
												continue;
											case 5:
												return;
										}
										break;
									}
								}
							}
							catch (Exception ex)
							{
								ERROR = ex.ToString();
								return;
							}
						case 3:
						case 6:
							flag = !(key.Trim() != "");
							num = 0;
							continue;
						case 0:
							if (!flag)
							{
								num = 5;
								continue;
							}
							goto case 2;
						case 4:
							condition = " devid='" + siteid + "'";
							classVo.classid = 0L;
							classVo.position = "left";
							classVo.classname = "管理 所有留言板回贴:" + key;
							classVo.siteimg = "NetImages/no.gif";
							num = 6;
							continue;
						case 5:
							condition = condition + " and content like '%" + key + "%' ";
							num = 2;
							continue;
					}
					break;
				}
			}
		}

		public void gocheck()
		{
			string requestValue = default(string);
			string requestValue2 = default(string);
			while (true)
			{
				CheckManagerLvl("01", "", GetUrlQueryString());
				bool flag = !(userVo.managerlvl != "00");
				int num = 5;
				while (true)
				{
					switch (num)
					{
						case 5:
							if (!flag)
							{
								num = 2;
								continue;
							}
							goto case 4;
						case 1:
							try
							{
								MainBll.UpdateSQL("update wap_bbsre set ischeck=" + long.Parse(requestValue) + "  where id=" + long.Parse(requestValue2) + " and devid='" + siteid + "'");
							}
							catch (Exception ex)
							{
								ERROR = WapTool.ErrorToString(ex.ToString());
							}
							showclass();
							num = 7;
							continue;
						case 4:
							requestValue2 = GetRequestValue("id");
							requestValue = GetRequestValue("state");
							num = 1;
							continue;
						case 0:
							return;
						case 7:
							return;
						case 2:
							flag = siteVo.isCheckSite != 1;
							num = 6;
							continue;
						case 6:
							num = (flag ? 4 : 3);
							continue;
						case 3:
							if (true)
							{
							}
							ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheck", "class"));
							num = 0;
							continue;
					}
					break;
				}
			}
		}

		public void gocheckall()
		{
			string requestValue = default(string);
			while (true)
			{
				CheckManagerLvl("01", "", GetUrlQueryString());
				bool flag = !(userVo.managerlvl != "00");
				int num = 0;
				while (true)
				{
					switch (num)
					{
						case 0:
							if (!flag)
							{
								num = 7;
								continue;
							}
							goto case 5;
						case 2:
							try
							{
								MainBll.UpdateSQL("update wap_bbsre set ischeck=" + long.Parse(requestValue) + "  where ischeck <>" + long.Parse(requestValue) + " and devid='" + siteid + "'");
							}
							catch (Exception ex)
							{
								ERROR = WapTool.ErrorToString(ex.ToString());
							}
							showclass();
							num = 3;
							continue;
						case 5:
							requestValue = GetRequestValue("state");
							num = 2;
							continue;
						case 1:
							ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheckall", "class"));
							num = 6;
							continue;
						case 6:
							return;
						case 3:
							return;
						case 7:
							if (true)
							{
							}
							flag = siteVo.isCheckSite != 1;
							num = 4;
							continue;
						case 4:
							num = ((!flag) ? 1 : 5);
							continue;
					}
					break;
				}
			}
		}
	}
}