using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Report_List : MyPageWap
    {
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string linkURL = "";

		public string condition = "";

		public string ERROR = "";

		public string id = "0";

		public string lpage = "";

		public List<wap2_bbs_report_Model> listVo = null;

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
				lpage = GetRequestValue("lpage");
				CheckManagerLvl("04", classVo.adminusername, "bbs/showadmin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=1");
				string text = action;
				int num = 6;
				while (true)
				{
					switch (num)
					{
						case 6:
							if (text != null)
							{
								num = 2;
								continue;
							}
							goto case 7;
						case 0:
							return;
						case 5:
							num = 7;
							continue;
						case 2:
							num = 9;
							continue;
						case 9:
							if (!(text == "del"))
							{
								num = 4;
								continue;
							}
							godel();
							num = 3;
							continue;
						case 7:
							showclass();
							num = 8;
							continue;
						case 8:
							return;
						case 4:
							num = 1;
							continue;
						case 1:
							if (text == "class")
							{
								if (true)
								{
								}
								showclass();
								num = 0;
							}
							else
							{
								num = 5;
							}
							continue;
						case 3:
							return;
					}
					break;
				}
			}
		}

		public void showclass()
		{
			wap2_bbs_report_BLL wap2_bbs_report_BLL = default(wap2_bbs_report_BLL);
			bool flag = default(bool);
			while (true)
			{
				int num = 1;
				while (true)
				{
					int num3;
					switch (num)
					{
						case 1:
							num = ((!CheckManagerLvl("04", "")) ? 6 : 4);
							continue;
						case 0:
						case 3:
							condition += " and types=0 ";
							if (true)
							{
							}
							num = 7;
							continue;
						case 6:
							num3 = 1;
							goto IL_0475;
						case 4:
							num = 2;
							continue;
						case 2:
							num3 = ((!(classid == "0")) ? 1 : 0);
							goto IL_0475;
						case 7:
							try
							{
								while (true)
								{
									flag = classVo.ismodel >= 1;
									num = 3;
									while (true)
									{
										int num2;
										switch (num)
										{
											case 3:
												if (!flag)
												{
													num = 0;
													continue;
												}
												pageSize = Convert.ToInt32(classVo.ismodel);
												num = 5;
												continue;
											case 5:
											case 14:
												wap2_bbs_report_BLL = new wap2_bbs_report_BLL(a);
												num = 7;
												continue;
											case 7:
												num = ((!(GetRequestValue("getTotal") != "")) ? 13 : 6);
												continue;
											case 8:
											case 9:
												flag = !(GetRequestValue("page") != "");
												num = 4;
												continue;
											case 4:
												if (!flag)
												{
													num = 1;
													continue;
												}
												goto case 10;
											case 1:
												CurrentPage = long.Parse(GetRequestValue("page"));
												num = 10;
												continue;
											case 13:
												num2 = 1;
												goto IL_02b5;
											case 0:
												pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
												num = 14;
												continue;
											case 6:
												num = 11;
												continue;
											case 11:
												num2 = ((!(GetRequestValue("getTotal") != "0")) ? 1 : 0);
												goto IL_02b5;
											case 12:
												if (!flag)
												{
													num = 2;
													continue;
												}
												total = wap2_bbs_report_BLL.GetListCount(condition);
												num = 9;
												continue;
											case 2:
												total = Convert.ToInt32(GetRequestValue("getTotal"));
												num = 8;
												continue;
											case 10:
												CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
												index = pageSize * (CurrentPage - 1);
												linkURL = http_start + "bbs/Report_List.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;getTotal=" + total;
												linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
												listVo = wap2_bbs_report_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
												num = 15;
												continue;
											case 15:
												return;
											IL_02b5:
												flag = (byte)num2 != 0;
												num = 12;
												continue;
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
						case 8:
							condition = " siteid=" + siteid;
							num = 0;
							continue;
						case 5:
							{
								if (flag)
								{
									condition = " classid=" + classid + " and siteid=" + siteid;
									num = 3;
								}
								else
								{
									num = 8;
								}
								continue;
							}
						IL_0475:
							flag = (byte)num3 != 0;
							num = 5;
							continue;
					}
					break;
				}
			}
		}

		public void godel()
		{
		}
	}
}