using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.WebSite
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
            var wap_vcount_everyDate_BLL = new WapVCountEveryDateBLL(WapTool._InstanceName);
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
}
