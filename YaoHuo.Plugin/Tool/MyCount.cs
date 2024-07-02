using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.DAL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace YaoHuo.Plugin.Tool
{
	public static class MyCount
	{
		public static string city = "其它";

		public static string city1 = "其它";

		public static string city2 = "其它";

		public static string searchName = "其它";

		public static string browser = "其它";

		public static string string_0 = "其它";

		public static long bookid = 0L;

		public static Dictionary<string, string> IPArray = new Dictionary<string, string>();

		public static string KL_VisiteCount_Detail = PubConstant.GetAppString("KL_VisiteCount_Detail");

		public static void SaveCount(long siteid, long types, long userid, string localurl, string welcomeurl, string string_1, string string_2, string userStartTime, long otherPageCount, string AppPath, string locationDomain, string siteUserName, string mobile, string cookies, string CityName, string classname, string book_title)
		{
			wap_vcount_everyDate_Model wap_vcount_everyDate_Model = null;
			YaoHuo_wap_vcount_everyDate_BLL wap_vcount_everyDate_BLL = new YaoHuo_wap_vcount_everyDate_BLL(WapTool._InstanceName);
			wap_vcount_everyDate_Model = wap_vcount_everyDate_BLL.GetModel_Today(siteid, types);
			if (wap_vcount_everyDate_Model == null)
			{
				wap_vcount_everyDate_Model = new wap_vcount_everyDate_Model();
			}
			bookid = wap_vcount_everyDate_Model.id;
			wap_vcount_everyDate_Model.siteid = siteid;
			wap_vcount_everyDate_Model.types = types;
			wap_vcount_everyDate_Model.everyDate = DateTime.Now;
			wap_vcount_everyDate_Model.PV = wap_vcount_everyDate_Model.PV + 1L + otherPageCount;
			if (userStartTime == null || userStartTime == "")
			{
				wap_vcount_everyDate_Model.UV++;
			}
			if (userStartTime == null || userStartTime == "")
			{
				wap_vcount_everyDate_Model.VV++;
			}
			else
			{
				try
				{
					DateTime dateTime = DateTime.Parse(userStartTime);
					long num = (DateTime.Now - dateTime).Minutes % 30;
					if (num > 0L)
					{
						wap_vcount_everyDate_Model.VV += num;
					}
					userStartTime = DateTime.Now.ToString();
				}
				catch (Exception)
				{
				}
			}
			if (!GetIPTemp(siteid.ToString(), string_2 + "." + types))
			{
				wap_vcount_everyDate_Model.IP++;
				SaveIPTemp(siteid.ToString(), string_2 + "." + types);
			}
			city = CityName;
			if (city != "")
			{
				city1 = city.Split('省')[0];
				city2 = city.Replace(city1 + "省", "").Split('市')[0];
				city1 = city1.Split(' ')[0];
				city2 = city2.Split(' ')[0];
				if (city1.IndexOf("北京") > -1)
				{
					city1 = "北京";
				}
				else if (city1.IndexOf("上海") > -1)
				{
					city1 = "上海";
				}
				else if (city1.IndexOf("天津") > -1)
				{
					city1 = "天津";
				}
				else if (city1.IndexOf("重庆") > -1)
				{
					city1 = "重庆";
				}
				else if (city1.IndexOf("内蒙古") > -1)
				{
					city1 = "内蒙";
				}
				else if (city1.IndexOf("宁夏") > -1)
				{
					city1 = "宁夏";
				}
				else if (city1.IndexOf("新疆") > -1)
				{
					city1 = "新疆";
				}
				else if (city1.IndexOf("西藏") > -1)
				{
					city1 = "西藏";
				}
				else if (city1.IndexOf("广西") > -1)
				{
					city1 = "广西";
				}
			}
			if (welcomeurl.ToLower().IndexOf("google") > -1)
			{
				wap_vcount_everyDate_Model.SH_google++;
				searchName = "谷歌";
			}
			if (welcomeurl.ToLower().IndexOf("soso") > -1)
			{
				wap_vcount_everyDate_Model.SH_soso++;
				searchName = "搜搜";
			}
			if (welcomeurl.ToLower().IndexOf("baidu") > -1)
			{
				wap_vcount_everyDate_Model.SH_baidu++;
				searchName = "百度";
			}
			if (welcomeurl.ToLower().IndexOf("sogou") > -1)
			{
				wap_vcount_everyDate_Model.SH_sogou++;
				searchName = "搜狗";
			}
			if (welcomeurl.ToLower().IndexOf("yahoo") > -1)
			{
				wap_vcount_everyDate_Model.SH_yahoo++;
				searchName = "雅虎";
			}
			if (welcomeurl.ToLower().IndexOf("bing") > -1)
			{
				wap_vcount_everyDate_Model.SH_bing++;
				searchName = "必应";
			}
			if (welcomeurl.ToLower().IndexOf("youdao") > -1)
			{
				wap_vcount_everyDate_Model.SH_youdao++;
				searchName = "有道";
			}
			if (welcomeurl.ToLower().IndexOf("gougou") > -1)
			{
				wap_vcount_everyDate_Model.SH_gougou++;
				searchName = "狗狗";
			}
			if (city.IndexOf("北京") > -1)
			{
				wap_vcount_everyDate_Model.CT_beijing++;
			}
			if (city.IndexOf("上海") > -1)
			{
				wap_vcount_everyDate_Model.CT_shanghai++;
			}
			if (city.IndexOf("广州") > -1)
			{
				wap_vcount_everyDate_Model.CT_guangzhou++;
			}
			if (city.IndexOf("深圳") > -1)
			{
				wap_vcount_everyDate_Model.CT_shenzhen++;
			}
			if (city.IndexOf("电信") > -1)
			{
				wap_vcount_everyDate_Model.NT_ChinaTelecom++;
				string_0 = "电信";
			}
			if (city.IndexOf("移动") > -1)
			{
				wap_vcount_everyDate_Model.NT_ChinaMobile++;
				string_0 = "移动";
			}
			if (city.IndexOf("联通") > -1)
			{
				wap_vcount_everyDate_Model.NT_ChinaUnicom++;
				string_0 = "联通";
			}
			if (city.IndexOf("铁通") > -1)
			{
				string_0 = "铁通";
			}
			if (city.IndexOf("网通") > -1)
			{
				string_0 = "网通";
			}
			if (string_1.ToLower().IndexOf("safari") > -1)
			{
				wap_vcount_everyDate_Model.BS_Safari++;
				browser = "safari";
			}
			else if (string_1.ToLower().IndexOf("chrome") > -1)
			{
				wap_vcount_everyDate_Model.BS_Chrome++;
				browser = "chrome";
			}
			else if (string_1.ToLower().IndexOf("opera") > -1)
			{
				wap_vcount_everyDate_Model.BS_Opera++;
				browser = "opera";
			}
			else if (string_1.ToLower().IndexOf("uc") > -1)
			{
				wap_vcount_everyDate_Model.BS_UC++;
				browser = "uc";
			}
			else if (string_1.ToLower().IndexOf("qq") > -1)
			{
				wap_vcount_everyDate_Model.BS_QQ++;
				browser = "qq";
			}
			else if (string_1.ToLower().IndexOf("window") > -1)
			{
				wap_vcount_everyDate_Model.BS_IE++;
				browser = "window";
			}
			if (bookid == 0L)
			{
				bookid = wap_vcount_everyDate_BLL.Add(wap_vcount_everyDate_Model);
			}
			else
			{
				wap_vcount_everyDate_BLL.Update(wap_vcount_everyDate_Model);
			}
			if (userStartTime == null || userStartTime == "")
			{
				wap_vcount_everyDate_BLL.UpdateSQL("update [vcount] set vtotal=vtotal+" + (otherPageCount + 1L) + ",vtotal1=vtotal1+1 where vuser='" + siteUserName + "'");
			}
			else
			{
				wap_vcount_everyDate_BLL.UpdateSQL("update [vcount] set vtotal=vtotal+" + (otherPageCount + 1L) + " where vuser='" + siteUserName + "'");
			}
			if (KL_VisiteCount_Detail != "1")
			{
				city = "其它";
				city1 = "其它";
				city2 = "其它";
				searchName = "其它";
				browser = "其它";
				string_0 = "其它";
				bookid = 0L;
				return;
			}
			wap_vcount_Detail_BLL wap_vcount_Detail_BLL = new wap_vcount_Detail_BLL(WapTool._InstanceName);
			wap_vcount_Detail_Model wap_vcount_Detail_Model = new wap_vcount_Detail_Model();
			wap_vcount_Detail_Model.siteid = siteid;
			wap_vcount_Detail_Model.bookid = bookid;
			wap_vcount_Detail_Model.localURL = localurl;
			wap_vcount_Detail_Model.welcomeURL = welcomeurl;
			wap_vcount_Detail_Model.everyDate = DateTime.Now;
			wap_vcount_Detail_Model.classname = WapTool.left(classname, 50);
			wap_vcount_Detail_Model.book_title = WapTool.left(book_title, 50);
			wap_vcount_Detail_Model.types = types;
			if (mobile != null && mobile.Length == 11)
			{
				wap_vcount_Detail_Model.fromTypes = "信息下行";
			}
			else if (welcomeurl == "")
			{
				wap_vcount_Detail_Model.fromTypes = "直接访问";
			}
			else if (welcomeurl.IndexOf("_QR") == 0)
			{
				wap_vcount_Detail_Model.fromTypes = "二维码";
			}
			else if (searchName != "" && searchName != "其它")
			{
				wap_vcount_Detail_Model.fromTypes = "搜索引擎";
			}
			else if (!welcomeurl.ToLower().StartsWith("http://" + locationDomain))
			{
				wap_vcount_Detail_Model.fromTypes = "外部链接";
			}
			else
			{
				wap_vcount_Detail_Model.fromTypes = "本站链接";
			}
			wap_vcount_Detail_Model.search = searchName;
			wap_vcount_Detail_Model.searchKey = "";
			wap_vcount_Detail_Model.UA = string_1;
			wap_vcount_Detail_Model.browser = browser;
			wap_vcount_Detail_Model.IP = string_2;
			wap_vcount_Detail_Model.city1 = city1;
			wap_vcount_Detail_Model.city2 = city2;
			wap_vcount_Detail_Model.net = string_0;
			wap_vcount_Detail_Model.mobile = mobile;
			wap_vcount_Detail_Model.cookies = cookies;
			wap_vcount_Detail_Model.userid = userid;
			try
			{
				wap_vcount_Detail_BLL.Add(wap_vcount_Detail_Model);
			}
			catch (Exception ex)
			{
				if (ex.ToString().IndexOf("classname") > 0)
				{
					WapTool.UpdateSystemAuto();
				}
			}
			city = "其它";
			city1 = "其它";
			city2 = "其它";
			searchName = "其它";
			browser = "其它";
			string_0 = "其它";
			bookid = 0L;
		}

		public static bool GetIPTemp(string types, string string_1)
		{
			IPRemoveAll();
			foreach (KeyValuePair<string, string> item in IPArray)
			{
				if (item.Key.ToLower() == types && item.Value.IndexOf(string_1) > -1)
				{
					return true;
				}
			}
			return false;
		}

		public static void SaveIPTemp(string types, string string_1)
		{
			try
			{
				IPRemoveAll();
				bool flag = false;
				foreach (KeyValuePair<string, string> item in IPArray)
				{
					if (item.Key.ToLower() == types)
					{
						IPArray[types] = item.Value + string_1;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					IPArray.Add(types, string_1);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void IPRemoveAll()
		{
			if (WapTool.WeatherDay != DateTime.Now.Day.ToString())
			{
				IPArray.Clear();
				WapTool.WeatherDay = DateTime.Now.Day.ToString();
			}
		}
	}

	/// <summary>
	/// YAOHUO BLL 层
	/// </summary>
	public class YaoHuo_wap_vcount_everyDate_BLL : wap_vcount_everyDate_BLL
	{
		string aSelf = null;
		public YaoHuo_wap_vcount_everyDate_BLL(string InstanceName) : base(InstanceName)
		{
			aSelf = InstanceName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="siteid"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public new wap_vcount_everyDate_Model GetModel_Today(long siteid, long types)
		{
			return new YaoHuowap_vcount_everyDate_DAL(aSelf).GetModel_Today(siteid, types);
		}
	}

	/// <summary>
	/// 妖火自定义DAL层 重写DAL层GetModel_Today方法 
	/// 注释掉每次删除
	/// </summary>
	public class YaoHuowap_vcount_everyDate_DAL : wap_vcount_everyDate_DAL
	{
		string selfB = null;
		public YaoHuowap_vcount_everyDate_DAL(string InstanceName) : base(InstanceName)
		{
			selfB = PubConstant.GetConnectionString(InstanceName);
		}

		public new wap_vcount_everyDate_Model GetModel_Today(long siteid, long types)
		{
			//防止统计日期重复
			//DbHelperSQL.ExecuteDataset(selfB, CommandType.Text, "delete  a from  wap_vcount_everydate a where siteid=" + siteid + " and types=" + types + " and   exists(select 1 from wap_vcount_everydate where siteid=" + siteid + " and types=" + types + " and DateDiff(dd,everyDate,getdate()) =0 and DateDiff(dd,everyDate,getdate())=DateDiff(dd,a.everyDate,getdate()) and ID<a.ID)");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 id,siteid,types,everyDate,PV,UV,VV,IP,SH_google,SH_soso,SH_baidu,SH_sogou,SH_yahoo,SH_bing,SH_youdao,SH_gougou,CT_beijing,CT_shanghai,CT_guangzhou,CT_shenzhen,NT_ChinaMobile,NT_ChinaUnicom,NT_ChinaTelecom,BS_Safari,BS_Chrome,BS_Opera,BS_IE,BS_UC,BS_QQ,HangBiaoShi from wap_vcount_everyDate ");
			stringBuilder.Append(" where siteid=@siteid and types=@types and DateDiff(dd,everyDate,getdate())=0 ");
			SqlParameter[] array = new SqlParameter[2]
			{
			new SqlParameter("@siteid", SqlDbType.BigInt),
			new SqlParameter("@types", SqlDbType.BigInt)
			};
			array[0].Value = siteid;
			array[1].Value = types;
			wap_vcount_everyDate_Model wap_vcount_everyDate_Model = new wap_vcount_everyDate_Model();
			DataSet dataSet = DbHelperSQL.ExecuteDataset(selfB, CommandType.Text, stringBuilder.ToString(), array);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
				{
					wap_vcount_everyDate_Model.id = long.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["siteid"].ToString() != "")
				{
					wap_vcount_everyDate_Model.siteid = long.Parse(dataSet.Tables[0].Rows[0]["siteid"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["types"].ToString() != "")
				{
					wap_vcount_everyDate_Model.types = long.Parse(dataSet.Tables[0].Rows[0]["types"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["everyDate"].ToString() != "")
				{
					wap_vcount_everyDate_Model.everyDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["everyDate"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["PV"].ToString() != "")
				{
					wap_vcount_everyDate_Model.PV = long.Parse(dataSet.Tables[0].Rows[0]["PV"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["UV"].ToString() != "")
				{
					wap_vcount_everyDate_Model.UV = long.Parse(dataSet.Tables[0].Rows[0]["UV"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["VV"].ToString() != "")
				{
					wap_vcount_everyDate_Model.VV = long.Parse(dataSet.Tables[0].Rows[0]["VV"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["IP"].ToString() != "")
				{
					wap_vcount_everyDate_Model.IP = long.Parse(dataSet.Tables[0].Rows[0]["IP"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_google"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_google = long.Parse(dataSet.Tables[0].Rows[0]["SH_google"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_soso"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_soso = long.Parse(dataSet.Tables[0].Rows[0]["SH_soso"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_baidu"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_baidu = long.Parse(dataSet.Tables[0].Rows[0]["SH_baidu"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_sogou"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_sogou = long.Parse(dataSet.Tables[0].Rows[0]["SH_sogou"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_yahoo"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_yahoo = long.Parse(dataSet.Tables[0].Rows[0]["SH_yahoo"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_bing"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_bing = long.Parse(dataSet.Tables[0].Rows[0]["SH_bing"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_youdao"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_youdao = long.Parse(dataSet.Tables[0].Rows[0]["SH_youdao"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["SH_gougou"].ToString() != "")
				{
					wap_vcount_everyDate_Model.SH_gougou = long.Parse(dataSet.Tables[0].Rows[0]["SH_gougou"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["CT_beijing"].ToString() != "")
				{
					wap_vcount_everyDate_Model.CT_beijing = long.Parse(dataSet.Tables[0].Rows[0]["CT_beijing"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["CT_shanghai"].ToString() != "")
				{
					wap_vcount_everyDate_Model.CT_shanghai = long.Parse(dataSet.Tables[0].Rows[0]["CT_shanghai"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["CT_guangzhou"].ToString() != "")
				{
					wap_vcount_everyDate_Model.CT_guangzhou = long.Parse(dataSet.Tables[0].Rows[0]["CT_guangzhou"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["CT_shenzhen"].ToString() != "")
				{
					wap_vcount_everyDate_Model.CT_shenzhen = long.Parse(dataSet.Tables[0].Rows[0]["CT_shenzhen"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["NT_ChinaMobile"].ToString() != "")
				{
					wap_vcount_everyDate_Model.NT_ChinaMobile = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaMobile"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["NT_ChinaUnicom"].ToString() != "")
				{
					wap_vcount_everyDate_Model.NT_ChinaUnicom = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaUnicom"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["NT_ChinaTelecom"].ToString() != "")
				{
					wap_vcount_everyDate_Model.NT_ChinaTelecom = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaTelecom"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_Safari"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_Safari = long.Parse(dataSet.Tables[0].Rows[0]["BS_Safari"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_Chrome"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_Chrome = long.Parse(dataSet.Tables[0].Rows[0]["BS_Chrome"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_Opera"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_Opera = long.Parse(dataSet.Tables[0].Rows[0]["BS_Opera"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_IE"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_IE = long.Parse(dataSet.Tables[0].Rows[0]["BS_IE"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_UC"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_UC = long.Parse(dataSet.Tables[0].Rows[0]["BS_UC"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["BS_QQ"].ToString() != "")
				{
					wap_vcount_everyDate_Model.BS_QQ = long.Parse(dataSet.Tables[0].Rows[0]["BS_QQ"].ToString());
				}

				if (dataSet.Tables[0].Rows[0]["HangBiaoShi"].ToString() != "")
				{
					wap_vcount_everyDate_Model.HangBiaoShi = long.Parse(dataSet.Tables[0].Rows[0]["HangBiaoShi"].ToString());
				}

				return wap_vcount_everyDate_Model;
			}

			return null;
		}



	}
}
