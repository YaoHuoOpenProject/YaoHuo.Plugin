using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Search
{
	public class toManager : MyPageWap
	{
		private string a = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string touserid = "";

		public string page = "";

		public string INFO = "";

		public string ERROR = "";

		public string backurl = "";

		public user_Model touserVo = null;

		public string tobankmoney = "";

		public string topassword = "";

		public string toexpR = "";

		public string tosessiontimeout = "";

		public string tomanagerlvl = "";

		public string tolockuser = "";

		public string tochangedate = "";

		public string needpw = "";

		public List<wap2_smallType_Model> idlistVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = default(bool);
			StringBuilder stringBuilder = default(StringBuilder);
			while (true)
			{
				action = GetRequestValue("action");
				backurl = base.Request.QueryString.Get("backurl");
				int num = 74;
				while (true)
				{
					int num4;
					int num5;
					switch (num)
					{
						case 74:
							num = ((backurl == null) ? 28 : 64);
							continue;
						case 9:
							flag = userVo.userid == userVo.siteid;
							num = 36;
							continue;
						case 36:
							if (!flag)
							{
								num = 83;
								continue;
							}
							flag = userVo.userid != touserVo.userid;
							num = 34;
							continue;
						case 28:
							num4 = 0;
							goto IL_0371;
						case 83:
							flag = userVo.userid != touserVo.userid;
							num = 54;
							continue;
						case 54:
							if (!flag)
							{
								num = 58;
								continue;
							}
							flag = !(touserVo.managerlvl != "02");
							num = 61;
							continue;
						case 55:
							num = 21;
							continue;
						case 24:
							num = 17;
							continue;
						case 44:
							num = 24;
							continue;
						case 66:
							flag = !(tomanagerlvl != "");
							num = 49;
							continue;
						case 49:
							if (!flag)
							{
								num = 12;
								continue;
							}
							goto case 24;
						case 53:
							num = 55;
							continue;
						case 4:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 80;
							continue;
						case 1:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 41;
							continue;
						case 64:
							num = 71;
							continue;
						case 71:
							num4 = ((!(backurl == "")) ? 1 : 0);
							goto IL_0371;
						case 72:
							if (!flag)
							{
								num = 89;
								continue;
							}
							goto case 86;
						case 67:
						case 76:
							num = 32;
							continue;
						case 18:
							num = 77;
							continue;
						case 11:
							flag = "|01|".IndexOf(tomanagerlvl) >= 0;
							num = 33;
							continue;
						case 33:
							if (!flag)
							{
								num = 79;
								continue;
							}
							goto case 18;
						case 90:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 53;
							continue;
						case 57:
							if (!flag)
							{
								num = 81;
								continue;
							}
							goto case 65;
						case 58:
							flag = !(tomanagerlvl != "");
							num = 93;
							continue;
						case 93:
							if (!flag)
							{
								num = 43;
								continue;
							}
							goto case 30;
						case 87:
							flag = !(tomanagerlvl != "");
							if (true)
							{
							}
							num = 40;
							continue;
						case 40:
							if (!flag)
							{
								num = 7;
								continue;
							}
							goto case 94;
						case 68:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 44;
							continue;
						case 12:
							flag = !(tomanagerlvl != userVo.managerlvl);
							num = 73;
							continue;
						case 73:
							if (!flag)
							{
								num = 68;
								continue;
							}
							goto case 44;
						case 8:
							flag = !(tomanagerlvl != "");
							num = 35;
							continue;
						case 35:
							if (!flag)
							{
								num = 52;
								continue;
							}
							goto case 55;
						case 7:
							flag = "|00|".IndexOf(tomanagerlvl) >= 0;
							num = 42;
							continue;
						case 42:
							if (!flag)
							{
								num = 84;
								continue;
							}
							goto case 16;
						case 20:
							flag = "|01|02|03|04|".IndexOf(tomanagerlvl) >= 0;
							num = 85;
							continue;
						case 85:
							if (!flag)
							{
								num = 1;
								continue;
							}
							goto case 41;
						case 65:
							num = 26;
							continue;
						case 56:
							num = 27;
							continue;
						case 61:
							if (!flag)
							{
								num = 62;
								continue;
							}
							goto case 8;
						case 21:
						case 88:
							num = 76;
							continue;
						case 41:
							num = 56;
							continue;
						case 47:
							flag = userVo.userid != touserVo.userid;
							num = 3;
							continue;
						case 3:
							if (!flag)
							{
								num = 66;
								continue;
							}
							flag = "|01|02|03|04|".IndexOf(touserVo.managerlvl) >= 0;
							num = 31;
							continue;
						case 82:
							num = 39;
							continue;
						case 63:
							ShowTipInfo("抱歉，你没有权限管理此用户！", "");
							num = 78;
							continue;
						case 32:
							flag = !(userVo.managerlvl == "00");
							num = 10;
							continue;
						case 10:
							if (!flag)
							{
								num = 92;
								continue;
							}
							goto case 22;
						case 31:
							if (!flag)
							{
								num = 63;
								continue;
							}
							goto case 78;
						case 17:
						case 27:
							num = 6;
							continue;
						case 6:
						case 39:
							num = 22;
							continue;
						case 86:
							num = 0;
							continue;
						case 0:
							num = ((backurl == null) ? 70 : 2);
							continue;
						case 29:
							num = 30;
							continue;
						case 92:
							flag = userVo.userid == userVo.siteid;
							num = 91;
							continue;
						case 91:
							if (!flag)
							{
								num = 47;
								continue;
							}
							flag = userVo.userid != touserVo.userid;
							num = 38;
							continue;
						case 14:
							tobankmoney = GetRequestValue("tobankmoney");
							topassword = GetRequestValue("topassword");
							toexpR = GetRequestValue("toexpR");
							tosessiontimeout = GetRequestValue("tosessiontimeout");
							tolockuser = GetRequestValue("tolockuser");
							tomanagerlvl = GetRequestValue("tomanagerlvl");
							tochangedate = GetRequestValue("tochangedate");
							needpw = GetRequestValue("needpw");
							num = 50;
							continue;
						case 77:
							num = 23;
							continue;
						case 34:
							if (flag)
							{
								flag = !(tomanagerlvl != "");
								num = 57;
							}
							else
							{
								num = 69;
							}
							continue;
						case 70:
							num5 = 0;
							goto IL_0a61;
						case 38:
							if (!flag)
							{
								num = 87;
								continue;
							}
							goto case 82;
						case 62:
							ShowTipInfo("抱歉，你没有权限管理此用户！", "");
							num = 8;
							continue;
						case 52:
							flag = !(tomanagerlvl != "02");
							num = 19;
							continue;
						case 19:
							if (!flag)
							{
								num = 90;
								continue;
							}
							goto case 53;
						case 79:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 18;
							continue;
						case 2:
							num = 59;
							continue;
						case 59:
							num5 = ((!(backurl == "")) ? 1 : 0);
							goto IL_0a61;
						case 5:
							if (!flag)
							{
								num = 75;
								continue;
							}
							goto case 51;
						case 51:
							{
								backurl = ToHtm(backurl);
								backurl = HttpUtility.UrlDecode(backurl);
								backurl = WapTool.URLtoWAP(backurl);
								touserid = GetRequestValue("touserid");
								user_BLL user_BLL = new user_BLL(a);
								touserVo = user_BLL.getUserInfo(touserid, siteid);
								IsCheckManagerLvl("|00|01", "", GetUrlQueryString());
								tomanagerlvl = GetRequestValue("tomanagerlvl");
								flag = !(userVo.managerlvl == "01");
								num = 15;
								continue;
							}
						case 15:
							if (!flag)
							{
								num = 9;
								continue;
							}
							goto case 32;
						case 84:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 16;
							continue;
						case 94:
							num = 82;
							continue;
						case 78:
							flag = !(tomanagerlvl != "");
							num = 48;
							continue;
						case 48:
							if (!flag)
							{
								num = 20;
								continue;
							}
							goto case 56;
						case 81:
							flag = "|01|02|03|04|".IndexOf(tomanagerlvl) >= 0;
							num = 46;
							continue;
						case 46:
							if (!flag)
							{
								num = 4;
								continue;
							}
							goto case 80;
						case 50:
							try
							{
								while (true)
								{
								IL_0c7a:
									flag = !(PubConstant.md5(needpw).ToLower() != userVo.password.ToLower());
									num = 22;
									while (true)
									{
										int num3;
										int num2;
										switch (num)
										{
											case 22:
												num = (flag ? 16 : 10);
												continue;
											case 11:
												stringBuilder.Append(",password='" + PubConstant.md5(topassword) + "'");
												num = 7;
												continue;
											case 6:
												num = 5;
												continue;
											case 5:
												num3 = (WapTool.IsNumeric(toexpR) ? 1 : 0);
												goto IL_0fb3;
											case 2:
												stringBuilder.Append(" where siteid=" + siteid + " and  userid=" + touserid);
												MainBll.UpdateSQL(stringBuilder.ToString());
												INFO = "OK";
												num = 14;
												continue;
											case 16:
												num = ((!WapTool.IsNumeric(tobankmoney)) ? 12 : 6);
												continue;
											case 18:
												num = 15;
												continue;
											case 21:
												num = 1;
												continue;
											case 1:
												num2 = ((!(tochangedate.Trim() != "")) ? 1 : 0);
												goto IL_0eef;
											case 7:
												num = 3;
												continue;
											case 3:
												num = ((!(siteid.ToString() != touserid.ToString())) ? 8 : 21);
												continue;
											case 13:
												if (!flag)
												{
													num = 11;
													continue;
												}
												goto case 7;
											case 19:
												if (!flag)
												{
													num = 18;
													continue;
												}
												goto case 2;
											case 20:
												INFO = "NUM";
												num = 4;
												continue;
											case 12:
												num3 = 0;
												goto IL_0fb3;
											case 15:
												try
												{
													DateTime endTime = DateTime.Parse(tochangedate.Trim());
													stringBuilder.Append(",endtime='" + tochangedate + "'");
													touserVo.endTime = endTime;
												}
												catch (Exception)
												{
												}
												num = 2;
												continue;
											case 10:
												INFO = "PWERROR";
												num = 9;
												continue;
											case 17:
												if (flag)
												{
													stringBuilder = new StringBuilder();
													stringBuilder.Append("update [user] set managerlvl='" + tomanagerlvl + "' , expR=" + toexpR + ",lockuser=" + tolockuser + ",myBankMoney=" + tobankmoney + ",sessiontimeout=" + tosessiontimeout);
													flag = !(topassword != "");
													num = 13;
												}
												else
												{
													num = 20;
												}
												continue;
											case 8:
												num2 = 1;
												goto IL_0eef;
											case 4:
											case 9:
											case 14:
												num = 0;
												continue;
											case 0:
												goto end_IL_0c15;
											IL_0fb3:
												flag = (byte)num3 != 0;
												num = 17;
												continue;
											IL_0eef:
												flag = (byte)num2 != 0;
												num = 19;
												continue;
										}
										goto IL_0c7a;
										//continue;
									end_IL_0c15:
										break;
									}
									break;
								}
							}
							catch (Exception ex)
							{
								ERROR = ex.ToString();
							}
							num = 60;
							continue;
						case 43:
							flag = !(tomanagerlvl != userVo.managerlvl);
							num = 13;
							continue;
						case 13:
							if (!flag)
							{
								num = 25;
								continue;
							}
							goto case 29;
						case 69:
							flag = !(tomanagerlvl != "");
							num = 45;
							continue;
						case 45:
							if (!flag)
							{
								num = 11;
								continue;
							}
							goto case 77;
						case 22:
							flag = !(action == "gomod");
							num = 37;
							continue;
						case 37:
							if (!flag)
							{
								num = 14;
								continue;
							}
							goto case 60;
						case 25:
							ShowTipInfo("抱歉，你权限选错了！", "");
							num = 29;
							continue;
						case 75:
							backurl = "myfile.aspx?siteid=" + siteid;
							num = 51;
							continue;
						case 23:
						case 26:
							num = 67;
							continue;
						case 16:
							num = 94;
							continue;
						case 80:
							num = 65;
							continue;
						case 89:
							backurl = base.Request.Form.Get("backurl");
							num = 86;
							continue;
						case 30:
							num = 88;
							continue;
						case 60:
							{
								tobankmoney = touserVo.myBankMoney.ToString();
								toexpR = touserVo.expr.ToString();
								tosessiontimeout = touserVo.SessionTimeout.ToString();
								tolockuser = touserVo.LockUser.ToString();
								string strWhere = "siteid=" + siteid + " and systype='card'";
								wap2_smallType_BLL wap2_smallType_BLL = new wap2_smallType_BLL(a);
								idlistVo = wap2_smallType_BLL.GetListVo(100L, 1L, strWhere, "*", "id", 100L, 1);
								return;
							}
						IL_0a61:
							flag = (byte)num5 != 0;
							num = 5;
							continue;
						IL_0371:
							flag = (byte)num4 != 0;
							num = 72;
							continue;
					}
					break;
				}
			}
		}
	}
}