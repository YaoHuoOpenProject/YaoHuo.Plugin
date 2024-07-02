using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_modadd : MyPageWap
{
	private string a = PubConstant.GetAppString("InstanceName");

	public wap_bbs_Model bbsVo = new wap_bbs_Model();

	public string action = "";

	public string id = "";

	public string lpage = "";

	public string INFO = "";

	public string ERROR = "";

	public string book_content = "";

	public string titlemax = "2";

	public string contentmax = "2";

	protected void Page_Load(object sender, EventArgs e)
	{
		//Discarded unreachable code: IL_0245
		bool flag = default(bool);
		string text3 = default(string);
		int num6 = default(int);
		string text = default(string);
		bool flag2 = default(bool);
		wap_bbs_BLL wap_bbs_BLL = default(wap_bbs_BLL);
		while (true)
		{
			int num = 4;
			while (true)
			{
				int num2;
				switch (num)
				{
				case 4:
					num = ((!(classid != "0")) ? 1 : 11);
					continue;
				case 7:
					flag = !(bbsVo.book_classid.ToString() != classid);
					num = 27;
					continue;
				case 27:
					if (!flag)
					{
						num = 8;
						continue;
					}
					goto IL_1065;
				case 23:
					if (!flag)
					{
						num = 25;
						continue;
					}
					goto case 6;
				case 21:
					ShowTipInfo("续贴功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
					num = 5;
					continue;
				case 16:
					flag = !(action == "gomod");
					num = 30;
					continue;
				case 30:
					if (!flag)
					{
						num = 12;
						continue;
					}
					return;
				case 14:
					ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
					num = 34;
					continue;
				case 8:
					base.Response.End();
					num = 9;
					continue;
				case 9:
					if (1 == 0)
					{
					}
					goto IL_1065;
				case 28:
					try
					{
						while (true)
						{
							IL_0351:
							book_content = GetRequestValue("book_content");
							bbsVo.book_title = GetRequestValue("book_title");
							flag = bbsVo.book_title.Length <= 200;
							num = 52;
							while (true)
							{
								int num7;
								int num5;
								int num4;
								int num8;
								int num3;
								switch (num)
								{
								case 52:
									if (!flag)
									{
										num = 38;
										continue;
									}
									goto case 7;
								case 1:
									num = 42;
									continue;
								case 34:
								case 40:
									titlemax = WapTool.getArryString(classVo.smallimg, '|', 24);
									contentmax = WapTool.getArryString(classVo.smallimg, '|', 25);
									num = 51;
									continue;
								case 51:
									num = ((!WapTool.IsNumeric(titlemax)) ? 41 : 49);
									continue;
								case 18:
									if (!flag)
									{
										num = 5;
										continue;
									}
									goto case 25;
								case 44:
									num = 46;
									continue;
								case 46:
									num7 = ((book_content.IndexOf("[/grade]") <= 0) ? 1 : 0);
									goto IL_0adc;
								case 35:
									text3 = bbsVo.book_title.Substring(0, num6 + 6);
									flag = text3.IndexOf("[img]face") != 0;
									num = 28;
									continue;
								case 28:
									if (!flag)
									{
										num = 55;
										continue;
									}
									bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
									num = 15;
									continue;
								case 41:
									num5 = 0;
									goto IL_0ab5;
								case 31:
									text = WapTool.getArryString(classVo.smallimg, '|', 21);
									flag = !(text.Trim() != "");
									num = 23;
									continue;
								case 23:
									if (!flag)
									{
										num = 48;
										continue;
									}
									goto case 42;
								case 32:
									num = 37;
									continue;
								case 37:
									if (book_content.IndexOf("[/buy]") <= 0)
									{
										num = 29;
										continue;
									}
									goto IL_0607;
								case 9:
									num4 = 1;
									goto IL_0821;
								case 3:
									num8 = 0;
									goto IL_0a8b;
								case 30:
									titlemax = "2";
									num = 26;
									continue;
								case 21:
									num7 = 0;
									goto IL_0adc;
								case 8:
									num = 17;
									continue;
								case 17:
									num8 = ((bbsVo.book_title.Trim().Length >= long.Parse(titlemax)) ? 1 : 0);
									goto IL_0a8b;
								case 13:
									flag2 = true;
									num = 47;
									continue;
								case 4:
									flag = text.IndexOf("_" + userVo.SessionTimeout + "_") >= 0;
									num = 43;
									continue;
								case 43:
									if (!flag)
									{
										num = 22;
										continue;
									}
									goto case 20;
								case 49:
									num = 0;
									continue;
								case 0:
									num5 = ((!(titlemax == "0")) ? 1 : 0);
									goto IL_0ab5;
								case 15:
								case 59:
									num = 40;
									continue;
								case 20:
									num = 1;
									continue;
								case 29:
									num = 36;
									continue;
								case 36:
									if (book_content.IndexOf("[/coin]") <= 0)
									{
										num = 44;
										continue;
									}
									goto IL_0607;
								case 54:
									num = 57;
									continue;
								case 57:
									num3 = ((!(contentmax == "0")) ? 1 : 0);
									goto IL_0b3b;
								case 5:
									bbsVo.book_content = bbsVo.book_content.ToLower().Replace("[sid]", "[sid2]");
									bbsVo.book_content = bbsVo.book_content.ToLower().Replace("[sid1]", "[sid2]");
									num = 25;
									continue;
								case 7:
									num6 = bbsVo.book_title.IndexOf("[/img]");
									flag = num6 <= 0;
									num = 16;
									continue;
								case 16:
									if (!flag)
									{
										num = 35;
										continue;
									}
									bbsVo.book_title = bbsVo.book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
									num = 34;
									continue;
								case 60:
									if (!flag)
									{
										num = 4;
										continue;
									}
									goto case 1;
								case 47:
									num = 56;
									continue;
								case 56:
									num = (flag2 ? 33 : 9);
									continue;
								case 48:
									text = "_" + text + "_";
									flag2 = false;
									num = 53;
									continue;
								case 53:
									if (book_content.IndexOf("[/reply]") <= 0)
									{
										num = 32;
										continue;
									}
									goto IL_0607;
								case 55:
								{
									string text2 = bbsVo.book_title.Substring(num6 + 6, bbsVo.book_title.Length - num6 - 6);
									bbsVo.book_title = text3 + text2.Replace("/", "／").Replace("[", "［").Replace("]", "］");
									num = 59;
									continue;
								}
								case 6:
									INFO = "NULL";
									num = 12;
									continue;
								case 42:
									num = 19;
									continue;
								case 19:
									num = ((book_content.Trim().Length >= long.Parse(contentmax)) ? 8 : 3);
									continue;
								case 25:
								{
									string text4 = "{" + userVo.nickname + "(ID" + userVo.userid + ")文字续贴" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
									bbsVo.whylock = text4 + bbsVo.whylock;
									wap_bbs_BLL.Update(bbsVo);
									INFO = "OK";
									num = 50;
									continue;
								}
								case 26:
									num = 27;
									continue;
								case 27:
									num = ((!WapTool.IsNumeric(contentmax)) ? 58 : 54);
									continue;
								case 39:
									if (flag)
									{
										bbsVo.book_content = bbsVo.book_content + "[br]" + book_content;
										flag = IsUserManager(userid, userVo.managerlvl, classVo.adminusername);
										num = 18;
									}
									else
									{
										num = 6;
									}
									continue;
								case 11:
									if (!flag)
									{
										num = 30;
										continue;
									}
									goto case 26;
								case 45:
									if (!flag)
									{
										num = 13;
										continue;
									}
									goto case 47;
								case 38:
									bbsVo.book_title = bbsVo.book_title.Substring(0, 200);
									num = 7;
									continue;
								case 24:
									if (!flag)
									{
										num = 2;
										continue;
									}
									goto case 31;
								case 2:
									contentmax = "2";
									num = 31;
									continue;
								case 33:
									num = 10;
									continue;
								case 10:
									num4 = (IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) ? 1 : 0);
									goto IL_0821;
								case 58:
									num3 = 0;
									goto IL_0b3b;
								case 22:
									ShowTipInfo("您当前的身份不允许发特殊贴。", "bbs/book_view_modadd.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
									num = 20;
									continue;
								case 12:
								case 50:
									num = 14;
									continue;
								case 14:
									goto end_IL_0254;
									IL_0ab5:
									flag = (byte)num5 != 0;
									num = 11;
									continue;
									IL_0adc:
									flag = (byte)num7 != 0;
									num = 45;
									continue;
									IL_0b3b:
									flag = (byte)num3 != 0;
									num = 24;
									continue;
									IL_0a8b:
									flag = (byte)num8 != 0;
									num = 39;
									continue;
									IL_0821:
									flag = (byte)num4 != 0;
									num = 60;
									continue;
									IL_0607:
									num = 21;
									continue;
								}
								goto IL_0351;
								end_IL_0254:
								break;
							}
							break;
						}
					}
					catch (Exception ex)
					{
						ERROR = ex.ToString();
					}
					num = 10;
					continue;
				case 24:
					ShowTipInfo("此贴已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
					num = 6;
					continue;
				case 5:
					IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
					//needPassWordToAdmin();
					wap_bbs_BLL = new wap_bbs_BLL(a);
					bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
					flag = bbsVo != null;
					num = 17;
					continue;
				case 17:
					if (!flag)
					{
						num = 31;
						continue;
					}
					flag = bbsVo.ischeck != 1;
					num = 19;
					continue;
				case 12:
					num = 28;
					continue;
				case 10:
					return;
				case 2:
					if (!flag)
					{
						num = 20;
						continue;
					}
					flag = bbsVo.islock != 1;
					num = 13;
					continue;
				case 11:
					num = 3;
					continue;
				case 3:
					num2 = ((!(classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
					goto IL_115b;
				case 34:
					action = GetRequestValue("action");
					id = GetRequestValue("id");
					lpage = GetRequestValue("lpage");
					flag = !"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 4));
					num = 36;
					continue;
				case 36:
					if (!flag)
					{
						num = 21;
						continue;
					}
					goto case 5;
				case 31:
					ShowTipInfo("已删除！或不存在！", "");
					num = 35;
					continue;
				case 19:
					if (flag)
					{
						flag = !(bbsVo.book_classid.ToString() != classid);
						num = 2;
					}
					else
					{
						num = 18;
					}
					continue;
				case 0:
					CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
					num = 16;
					continue;
				case 32:
					if (!flag)
					{
						num = 0;
						continue;
					}
					goto case 16;
				case 20:
					ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
					num = 33;
					continue;
				case 13:
					if (flag)
					{
						flag = bbsVo.islock != 2;
						num = 23;
					}
					else
					{
						num = 24;
					}
					continue;
				case 6:
				case 22:
				case 26:
				case 33:
				case 35:
					flag = !(bbsVo.userid.ToString() != siteid);
					num = 29;
					continue;
				case 29:
					if (!flag)
					{
						num = 15;
						continue;
					}
					goto case 7;
				case 37:
					if (!flag)
					{
						num = 14;
						continue;
					}
					goto case 34;
				case 18:
					ShowTipInfo("正在审核中！", "");
					num = 26;
					continue;
				case 1:
					num2 = 1;
					goto IL_115b;
				case 25:
					ShowTipInfo("此贴已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
					num = 22;
					continue;
				case 15:
					{
						base.Response.End();
						num = 7;
						continue;
					}
					IL_1065:
					flag = !(userid != bbsVo.book_pub.ToString());
					num = 32;
					continue;
					IL_115b:
					flag = (byte)num2 != 0;
					num = 37;
					continue;
				}
				break;
			}
		}
	}
}}