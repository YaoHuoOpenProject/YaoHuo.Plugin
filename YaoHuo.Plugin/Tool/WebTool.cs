using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.DAL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using KeLin.ClassManager.Tool;
using Microsoft.VisualBasic;
using PVUVWeb.visiteCount;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using UBB_Expand;

namespace YaoHuo.Plugin.Tool
{
    public static class WapTool
    {
        public const string KL_Version = "10.2014.12.18";

        public const string KL_Kill_Domain = "|cuqb.com|vl5.pw|24n.pw|sfw.3gwxw.com|92tan.net|xldao.com|mrpty.com|97wb.com|oimx.cn|www.znzj.org|hxwvip.com|xcxcw.com|jiaoyiw.com|wappx.cn|mrpej.com|waphxs.com|waphww.cn|wap.mrpej.com|vl1.cc|97wb.com|longying.xglhckjxc.com|waphk.cn|d9g.cn|yzmobi.com.cn|u0w.cc|lonka.cn|9tjc.com|anke2.com|lzqx.cn|iyoxi.cn|5sm.cc|yontu.cn|so789.net|czwap.cn|2gqq.net|wap.iiixiao.com|viexw.cn|xswap.cn|daoyun.cc|xzasp.cn|tyuw.net|3.yh2.cn|xyggb.cn";

        public static string _InstanceName = PubConstant.GetAppString("InstanceName");

        public static string _ConnStr = PubConstant.GetConnectionString(_InstanceName);

        public static string regcode = PubConstant.GetAppString("KL_License");

        public static string KL_Kill_None = PubConstant.GetAppString("KL_Kill_None");

        public static string KL_OpenCache = PubConstant.GetAppString("KL_OpenCache");

        public static string KL_OpenCacheTime = PubConstant.GetAppString("KL_OpenCacheTime");

        public static string KL_OrderByNew = PubConstant.GetAppString("KL_OrderByNew");

        public static string ISAPI_Rewrite3_Open = PubConstant.GetAppString("ISAPI_Rewrite3_Open");

        public static int KL_OpenCacheNowTime = DateTime.Now.Hour;

        public static Dictionary<string, string> WeatherArray = new Dictionary<string, string>();

        public static Dictionary<string, string> DataTempArray = new Dictionary<string, string>();

        public static Dictionary<string, List<class_Model>> DataClassArray = new Dictionary<string, List<class_Model>>();

        public static Dictionary<string, List<wap_bbs_Model>> DataBBSArray = new Dictionary<string, List<wap_bbs_Model>>();

        public static Dictionary<string, List<wap_bbsre_Model>> DataBBSReArray = new Dictionary<string, List<wap_bbsre_Model>>();

        public static Dictionary<string, List<wap_book_Model>> DataArticleArray = new Dictionary<string, List<wap_book_Model>>();

        public static string WeatherDay = DateTime.Now.Day.ToString();

        public static string KL_NotDownAndUpload_SYS = "|ashx|stm|shtml|ad|adprototype|asa|asax|ascx|ashx|asmx|axd|browser|cd|cdx|cer|compiled|confi|cs|csproj|dd|exclude|idc|java|jsl|ldb|ldd|lddprototype|ldf|licx|master|mdb|mdf|msgx|refresh|rem|resources|resx|sd|sdm|sdmdocument|shtm|shtml|sitemap|skin|soap|stm|svc|vb|vbproj|vjsproj|vsdisco|webinfo|asp|jsp|asa|asax|cer|cdx|htr|php|aspx|shtml|config|exe|mdb";

        public static string Decode_KL(string string_0)
        {
            string text = "";
            int length = string_0.Length;
            int num = 0;
            string text2 = string_0.Substring(length - 1, 1);
            string_0 = string_0.Substring(0, length - 1);
            if (text2 == "0" || text2 == "2")
            {
                length = string_0.Length;
            }
            num = ((length % 2 != 0) ? 3 : 2);
            for (int i = 0; i < length; i += num)
            {
                string text3 = "";
                text3 = ((string_0.Length - i < num) ? string_0.Substring(i, string_0.Length - i) : string_0.Substring(i, num));
                if (i == 0)
                {
                    if (text2 == "1")
                    {
                        text3 = text3.Substring(text3.Length - 2, 2);
                    }
                    else if (text2 == "2")
                    {
                        text3 = text3.Substring(text3.Length - 1, 1);
                    }
                }
                text = text3 + text;
            }
            return text;
        }

        public static string EnCode_KL(string string_0)
        {
            int num = 0;
            int num2 = 0;
            string text = "";
            int length = string_0.Length;
            num2 = ((length % 2 != 0) ? 3 : 2);
            for (int i = 0; i < length; i += num2)
            {
                string text2 = "";
                text2 = ((string_0.Length - i < num2) ? string_0.Substring(i, string_0.Length - i) : string_0.Substring(i, num2));
                if (num2 == 3)
                {
                    if (text2.Length == 1)
                    {
                        text2 = "00" + text2;
                        num = 2;
                    }
                    else if (text2.Length == 2)
                    {
                        text2 = "0" + text2;
                        num = 1;
                    }
                }
                text = text2 + text;
            }
            return text + num;
        }

        public static user_Model getSiteInfo(string siteid)
        {
            user_DAL user_DAL = new user_DAL(_InstanceName);
            return user_DAL.getSiteInfo(siteid);
        }

        public static class_Model getClassInfo(string siteid, string classid)
        {
            class_DAL class_DAL = new class_DAL(_InstanceName);
            return class_DAL.GetModel(long.Parse(siteid), long.Parse(classid));
        }

        public static string GetNavigation(wml wmlVo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "wapindex";
            if (wmlVo.ver == "0")
            {
                text = "admin/wapindexedit";
            }
            if (wmlVo.classid != "" && wmlVo.classid != "0")
            {
                class_Model classInfo = getClassInfo(wmlVo.siteid, wmlVo.classid);
                if (classInfo != null)
                {
                    if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                    {
                        stringBuilder.Append("<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-" + classInfo.classid + ".html\">" + classInfo.classname + "</a>");
                    }
                    else
                    {
                        stringBuilder.Append("<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + classInfo.classid + "&amp;path=" + classInfo.typePath + "\">" + classInfo.classname + "</a>");
                    }
                    class_Model classInfo2 = getClassInfo(wmlVo.siteid, classInfo.childid.ToString());
                    if (classInfo2 != null)
                    {
                        if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                        {
                            stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-" + classInfo2.classid + ".html\">" + classInfo2.classname + "</a>");
                        }
                        else
                        {
                            stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + classInfo2.classid + "&amp;path=" + classInfo.typePath + "\">" + classInfo2.classname + "</a>");
                        }
                        class_Model classInfo3 = getClassInfo(wmlVo.siteid, classInfo2.childid.ToString());
                        if (classInfo3 != null)
                        {
                            if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                            {
                                stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-" + classInfo3.classid + ".html\">" + classInfo3.classname + "</a>");
                            }
                            else
                            {
                                stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + classInfo3.classid + "&amp;path=" + classInfo.typePath + "\">" + classInfo3.classname + "</a>");
                            }
                            class_Model classInfo4 = getClassInfo(wmlVo.siteid, classInfo3.childid.ToString());
                            if (classInfo4 != null)
                            {
                                if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                                {
                                    stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-" + classInfo4.classid + ".html\">" + classInfo4.classname + "</a>");
                                }
                                else
                                {
                                    stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + classInfo4.classid + "&amp;path=" + classInfo.typePath + "\">" + classInfo4.classname + "</a>");
                                }
                                class_Model classInfo5 = getClassInfo(wmlVo.siteid, classInfo4.childid.ToString());
                                if (classInfo5 != null)
                                {
                                    if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                                    {
                                        stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-" + classInfo5.classid + ".html\">" + classInfo5.classname + "</a>");
                                    }
                                    else
                                    {
                                        stringBuilder.Insert(0, "<font class='arrow'><span></span></font></span><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + classInfo5.classid + "&amp;path=" + classInfo.typePath + "\">" + classInfo5.classname + "</a>");
                                    }
                                }
                            }
                        }
                    }
                    stringBuilder.Append("<font class='arrow'><span></span></font></span>");
                }
                if (ISAPI_Rewrite3_Open == "1" && wmlVo.ver != "0")
                {
                    stringBuilder.Insert(0, "<div class='breadcrumb'><span class='crust'><a href=\"" + wmlVo.http_start + text + "-" + wmlVo.siteid + "-0.html\">" + GetLang("首页|首页|Index", wmlVo.lang) + "</a>");
                }
                else
                {
                    stringBuilder.Insert(0, "<div class='breadcrumb'><span class='crust'><a href=\"" + wmlVo.http_start + text + ".aspx?siteid=" + wmlVo.siteid + "&amp;classid=0\">" + GetLang("首页|首页|Index", wmlVo.lang) + "</a>");
                }
                stringBuilder.Append("</div>");
            }
            return stringBuilder.ToString();
        }

        public static void setTotal(string siteid, string classid, long total)
        {
            class_DAL class_DAL = new class_DAL(_InstanceName);
            class_DAL.setTotal(long.Parse(siteid), long.Parse(classid), total);
        }

        public static List<class_Model> GetChildList(long siteid, long classid)
        {
            class_DAL class_DAL = new class_DAL(_InstanceName);
            return class_DAL.GetChildList(siteid, classid);
        }

        public static user_Model getUserInfo(string userid, string siteid)
        {
            user_DAL user_DAL = new user_DAL(_InstanceName);
            return user_DAL.getUserInfo(userid, siteid);
        }

        public static void setUpdatePostion(long siteid, long classid, string string_0)
        {
            class_DAL class_DAL = new class_DAL(_InstanceName);
            class_DAL.setUpdatePostion(siteid, classid, string_0);
        }

        public static void setDefault(long ID, long siteid)
        {
            wap2_style_DAL wap2_style_DAL = new wap2_style_DAL(PubConstant.GetAppString("InstanceName"));
            wap2_style_DAL.setDefault(ID, siteid);
        }

        public static void setDefault_WEB(long ID, long siteid)
        {
            wap3_style_DAL wap3_style_DAL = new wap3_style_DAL(PubConstant.GetAppString("InstanceName"));
            wap3_style_DAL.setDefault(ID, siteid);
        }

        public static string checkSiteInfo(user_Model siteinfo, string IP_UA, string userid)
        {
            string result = "";
            if (siteinfo == null)
            {
                result = "此域名对应的网站还没有开通，请联系网站系统管理员！";
                result = result + "<br/>----------<br/>" + GetAdminSuperInfo("infodomainname");
            }
            else if (siteinfo.managerlvl == "02")
            {
                result = "此域名对应的网站还没有开通或权限降级，请联系网站系统管理员！";
                result = result + "<br/>----------<br/>" + GetAdminSuperInfo("infodomainname");
            }
            else if (siteinfo.isValid < 0L)
            {
                result = "此域名对应的网站已过期，请登录电脑WEB网站后台充值或联系管理员！";
                result = result + "<br/>----------<br/>" + GetAdminSuperInfo("infoclose");
            }
            else if (siteinfo.LockUser == 1L)
            {
                result = "此域名对应的网站被锁定，请联系管理员！";
                result = result + "<br/>----------<br/>" + GetAdminSuperInfo("infolock");
            }
            else if (siteinfo.siteid.ToString() != userid)
            {
                string string_ = IP_UA.Split('|')[0];
                string uA = IP_UA.Split('|')[1];
                switch (GetSiteDefault(siteinfo.Version.ToString(), 7))
                {
                    case "1":
                        if (!isAllowUA(uA))
                        {
                            result = "此网站设置了只允许手机访问[UA]，如果你是手机访问请将以下信息发给站长：" + IP_UA;
                            result = result + "<br/>----------<br/>TEL:" + siteinfo.mobile + "<br/>Email:" + siteinfo.email;
                        }
                        break;

                    case "2":
                        if (!isAllowIP(string_))
                        {
                            result = "此网站设置了只允许手机访问[IP]，如果你是手机访问请将以下信息发给站长：" + IP_UA;
                            result = result + "<br/>----------<br/>TEL:" + siteinfo.mobile + "<br/>Email:" + siteinfo.email;
                        }
                        break;

                    case "3":
                        if (!isAllowUA(uA) && !isAllowIP(string_))
                        {
                            result = "此网站设置了只允许手机访问[UA+IP]，如果你是手机访问请将以下信息发给站长：" + IP_UA;
                            result = result + "<br/>----------<br/>TEL:" + siteinfo.mobile + "<br/>Email:" + siteinfo.email;
                        }
                        break;
                }
                string text = siteinfo.UAFilter.Replace("_", "|").Replace("-", "|");
                string[] array = text.Split('|');
                string[] array2 = array;
                foreach (string text2 in array2)
                {
                    if (text2.Trim() != "")
                    {
                        IP_UA = IP_UA.ToLower();
                        if (IP_UA.IndexOf(text2.ToLower()) >= 0)
                        {
                            result = "抱歉，本网站设置了手机型号或IP过滤:" + text2 + "，你的无权查看，如果您是手机用户，请与该站站长联系。";
                            result = result + "<br/>----------<br/>TEL:" + siteinfo.mobile + "<br/>Email:" + siteinfo.email + "<br/>--------------<br/>我的手机信息：" + IP_UA;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static string GetAdminSuperInfo(string infotype)
        {
            string text = "";
            DomainName_DAL domainName_DAL = new DomainName_DAL(_InstanceName);
            DomainName_Model model = domainName_DAL.GetModel(1L);
            switch (infotype)
            {
                case "infolock":
                    text = model.infolock;
                    break;

                case "infoclose":
                    text = model.infoclose;
                    break;

                case "infodomainname":
                    text = model.infodomainname;
                    break;
            }
            if (text == "")
            {
                text = "Contact Me!<b>TEL:" + model.domain + "<br/>Email:" + model.email + "</b>";
            }
            return text;
        }

        public static string GetAdminWEBInfo()
        {
            return "";
        }

        public static StringBuilder showTop(string title, wml wmlVo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (wmlVo.ver == null)
            {
                wmlVo.ver = "2";
            }
            if (wmlVo.ver != "1" && "1".Equals(KL_Kill_None))
            {
                Random random = new Random();
                random.Next(5000, 99999).ToString();
            }
            if (wmlVo.ver == "1")
            {
                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><!DOCTYPE wml PUBLIC \"-//WAPFORUM//DTD WML 1.1//EN\" \"http://www.wapforum.org/DTD/wml_1.1.xml\">\n");
                stringBuilder.Append("<wml>\n");
                stringBuilder.Append("<head>\n");
                //stringBuilder.Append("<meta http-equiv=\"Cache-Control\" content=\"max-age=0\"/>\n");
                //stringBuilder.Append("<meta http-equiv=\"Cache-Control\" content=\"no-cache\"/>\n");
                if (wmlVo.timer != null && wmlVo.timer != "")
                {
                    string text = wmlVo.strUrl;
                    if (!text.StartsWith("http://"))
                    {
                        text = wmlVo.http_start + wmlVo.strUrl;
                    }
                    stringBuilder.Append("</head><card id=\"main\" ontimer=\"" + text + "\" title=\"");
                    stringBuilder.Append(title.Replace("［", "[").Replace("］", "]"));
                    stringBuilder.Append("\"><timer value=\"" + long.Parse(wmlVo.timer) * 10L + "\" />\n");
                }
                else
                {
                    stringBuilder.Append("</head><card id=\"main\" title=\"");
                    stringBuilder.Append(title.Replace("［", "[").Replace("］", "]"));
                    stringBuilder.Append("\">\n");
                }
                if (wmlVo.KL_PAGE_TOP.Trim() != "")
                {
                    stringBuilder.Append("<p>");
                    stringBuilder.Append(ToWML(wmlVo.KL_PAGE_TOP, wmlVo));
                    stringBuilder.Append("</p>");
                }
            }
            else
            {
                stringBuilder.Append("<!DOCTYPE html><html>");
                stringBuilder.Append("<head>\n");
                //stringBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\n");
                //stringBuilder.Append("<meta http-equiv=\"Cache-Control\" content=\"no-cache\"/>\n");
                if (wmlVo.mycss.IndexOf("viewport") < 0)
                {
                    stringBuilder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0\">\n");
                }
                if (wmlVo.timer != null && wmlVo.timer != "")
                {
                    string text = wmlVo.strUrl;
                    if (!text.StartsWith("http://"))
                    {
                        text = wmlVo.http_start + wmlVo.strUrl;
                    }
                    stringBuilder.Append("<meta http-equiv=\"refresh\" content=\"" + wmlVo.timer + ";url=" + text + "\" />\n");
                }
                try
                {
                    if (wmlVo.mycss.IndexOf("keywords") < 0)
                    {
                        stringBuilder.Append("<meta name=\"keywords\" content=\"" + wmlVo.title + " " + wmlVo.classVo.classname + "\" />\r\n");
                    }
                    if (wmlVo.mycss.IndexOf("description") < 0)
                    {
                        stringBuilder.Append("<meta name=\"description\" content=\"" + wmlVo.title + " " + wmlVo.classVo.classname + " " + wmlVo.siteVo.sitename + " " + wmlVo.http_start.Replace("http:", "").Replace("/", "") + "\" />\r\n");
                    }
                    if (wmlVo.mycss.IndexOf("author") < 0)
                    {
                        stringBuilder.Append("<meta name=\"author\" content=\"" + wmlVo.siteVo.sitename + " " + wmlVo.http_start.Replace("http:", "").Replace("/", "") + "\" />\r\n");
                    }
                }
                catch (Exception)
                {
                }
                if (wmlVo.mycss.IndexOf("]") > 0)
                {
                    stringBuilder.Append(ToWML(wmlVo.mycss, wmlVo).Replace("<br/>", "\r\n"));
                }
                else
                {
                    stringBuilder.Append(wmlVo.mycss);
                }
                if (wmlVo.mycss.IndexOf(".btBox") < 0)
                {
                    stringBuilder.Append("\r\n<link rel=\"stylesheet\" href=\"/Template/default/default.css?v=" + $"{DateTime.Now:yyyyMMdd}" + "\" type=\"text/css\" />");
                }
                stringBuilder.Append("\n<title>");
                stringBuilder.Append(title.Replace("［", "[").Replace("］", "]"));
                string text2 = "";
                if (wmlVo.siteVo != null)
                {
                    text2 = GetSiteDefault(wmlVo.siteVo.Version, 58);
                }
                if (wmlVo.strUrl.IndexOf("list.") > 0 || (wmlVo.strUrl.IndexOf("apindex.") > 0 && wmlVo.classid != "0"))
                {
                    if (wmlVo.siteVo != null)
                    {
                        stringBuilder.Append("_");
                        if (text2 != "")
                        {
                            stringBuilder.Append(text2);
                        }
                        else
                        {
                            stringBuilder.Append(wmlVo.siteVo.sitename);
                        }
                    }
                }
                else if (wmlVo.strUrl.IndexOf("view.") > 0)
                {
                    if (wmlVo.classVo != null)
                    {
                        stringBuilder.Append("_");
                        stringBuilder.Append(wmlVo.classVo.classname);
                    }
                    if (wmlVo.siteVo != null)
                    {
                        stringBuilder.Append("_");
                        if (text2 != "")
                        {
                            stringBuilder.Append(text2);
                        }
                        else
                        {
                            stringBuilder.Append(wmlVo.siteVo.sitename);
                        }
                    }
                }
                stringBuilder.Append("</title>\n");
                stringBuilder.Append("</head>\n");
                stringBuilder.Append("<body>\n");
                if (wmlVo.KL_PAGE_TOP.IndexOf("]") > 0)
                {
                    stringBuilder.Append(ToWML(wmlVo.KL_PAGE_TOP.Trim(), wmlVo));
                }
                else
                {
                    stringBuilder.Append(wmlVo.KL_PAGE_TOP.Trim());
                }
            }
            return stringBuilder;
        }

        /// <summary>
        /// 显示版权信息
        /// </summary>
        /// <param name="wmlVo"></param>
        /// <returns></returns>
        public static StringBuilder showDown(wml wmlVo)
        {
            var stringBuilder = new StringBuilder();
            if (wmlVo.ver == "1")
            {
                if (wmlVo.KL_PAGE_DOWN.Trim() != "")
                {
                    stringBuilder.Append("<p>");
                    stringBuilder.Append(ToWML(wmlVo.KL_PAGE_DOWN, wmlVo));
                    stringBuilder.Append("</p>");
                }
                stringBuilder.Append("\n</card>");
                stringBuilder.Append("\n</wml>");
            }
            else if (wmlVo.ver == "2")
            {
                if (wmlVo.KL_PAGE_DOWN.Trim() != "")
                {
                    stringBuilder.Append(ToWML(wmlVo.KL_PAGE_DOWN, wmlVo));
                }
                stringBuilder.Append("\n</body>");
                stringBuilder.Append("\n</html>");
            }
            else
            {
                if (wmlVo.KL_PAGE_DOWN.Trim() != "")
                {
                    stringBuilder.Append(ToWML(wmlVo.KL_PAGE_DOWN, wmlVo));
                }
                stringBuilder.Append("\n</body>");
                stringBuilder.Append("\n</html>");
            }
            return stringBuilder;
        }

        public static StringBuilder getSiteCSS(string siteid, string string_0)
        {
            user_DAL user_DAL = new user_DAL(_InstanceName);
            return user_DAL.getSiteCSS(siteid, string_0);
        }

        public static string ToWML(string WapHtmlStr, wml wmlvo)
        {
            if (string.IsNullOrEmpty(WapHtmlStr))
            {
                return "";
            }
            WapHtmlStr = UBB.StartIntercept(WapHtmlStr, wmlvo);
            if (WapHtmlStr.IndexOf("[/code]") > 0)
            {
                Regex regex = new Regex("(\\[code\\])(.[^\\[]*)(\\[\\/code\\])");
                try
                {
                    Match match = regex.Match(WapHtmlStr);
                    while (match.Success)
                    {
                        string value = match.Groups[2].Value;
                        WapHtmlStr = ((!IsNumeric(value)) ? regex.Replace(WapHtmlStr, "{格式错误}", 1) : regex.Replace(WapHtmlStr, GetCode_WEB(value, wmlvo), 1));
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapHtmlStr = regex.Replace(WapHtmlStr, "{格式错误}");
                }
            }
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "getwml", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "wml", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "article", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "bbs", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "bbsre", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "bbstopic", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "download", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "dl", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "guessbook", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "picture", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "shop", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "video", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "ring", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "link", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "linkactive", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "yuehui", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "online", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "adlink", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "gongqiu", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "album", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "rizhi", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "weibo", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "games", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "users", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "airplane", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "hotel", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "paimai", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "chat", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "clanchat", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "guangbo", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "novel", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "section", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = GetAllMid(wmlvo.ver, wmlvo.lang, WapHtmlStr, "wabao", wmlvo.userid, wmlvo.http_start, wmlvo.siteid, wmlvo.classid, wmlvo.sid, wmlvo);
            WapHtmlStr = getExtendFun(WapHtmlStr, wmlvo);
            WapHtmlStr = UBBCode(WapHtmlStr, wmlvo);
            WapHtmlStr = WapHtmlStr.Replace("［", "[");
            WapHtmlStr = WapHtmlStr.Replace("］", "]");
            WapHtmlStr = WapHtmlStr.Replace("[sid]", wmlvo.sid);
            if (wmlvo.defaultLang == "1" && wmlvo.lang == "0")
            {
                WapHtmlStr = WordTraditionalToSimple(WapHtmlStr);
            }
            if (wmlvo.defaultLang == "0" && wmlvo.lang == "1")
            {
                WapHtmlStr = WordSimpleToTraditional(WapHtmlStr);
            }
            return WapHtmlStr;
        }

        public static string WordSimpleToTraditional(string Str)
        {
            return Strings.StrConv(Str, VbStrConv.TraditionalChinese, CultureInfo.CurrentCulture.LCID);
        }

        public static string WordTraditionalToSimple(string Str)
        {
            return Strings.StrConv(Str, VbStrConv.SimplifiedChinese);
        }

        public static int CountTime(string WapHtmlStr, string sFind, int intTime)
        {
            int num = 0;
            if (sFind.Length > 0)
            {
                num = 1;
                for (int i = 0; i < intTime; i++)
                {
                    num = WapHtmlStr.IndexOf(sFind, num);
                    num += sFind.Length;
                }
            }
            return num;
        }

        public static bool IsNumeric(string Number)
        {
            if (Number == null || Number == "")
            {
                return false;
            }
            if (Number.Trim().StartsWith("-"))
            {
                return false;
            }
            try
            {
                long.Parse(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric_FuShuOK(string Number)
        {
            if (Number == null || Number == "")
            {
                return false;
            }
            try
            {
                long.Parse(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string Number)
        {
            if (Number == null || Number == "")
            {
                return false;
            }
            try
            {
                decimal.Parse(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetShowImg(string ImageObj, string strLenTitle, string dbname)
        {
            int num = 0;
            int num2 = 0;
            if (!IsNumeric(strLenTitle))
            {
                strLenTitle = "1000";
            }
            if (dbname == "adlink" && (ImageObj.ToLower().StartsWith("http") || ImageObj.ToLower().StartsWith("/")))
            {
                return "<img src=\"" + ImageObj + "\" alt=\"load..\"/>";
            }
            num = ImageObj.Length;
            num2 = ImageObj.IndexOf(">");
            if (num2 > 0)
            {
                ImageObj = ImageObj.Substring(num2 + 1, num - (num2 + 1));
                num2 = 0;
            }
            num2 = ImageObj.IndexOf("[/img]");
            if (num2 > 0)
            {
                ImageObj = ImageObj.Substring(num2 + 6, num - (num2 + 6));
            }
            int num3 = Convert.ToInt32(strLenTitle);
            if (ImageObj.Length > num3 && num3 > 0)
            {
                ImageObj = ImageObj.Substring(0, num3);
            }
            if (dbname != "adlink" && dbname.IndexOf("chat") < 0)
            {
                ImageObj = ImageObj.Replace("[", "［");
                ImageObj = ImageObj.Replace("]", "］");
            }
            return ImageObj;
        }

        public static string GetOnlyImg(string content)
        {
            Regex regex = new Regex("(\\[img\\])(.[^\\[]*)(\\[\\/img\\])");
            content = regex.Replace(content, "<img src=\"$2\" alt=\".\"/>");
            content = content.Replace("[", "［");
            content = content.Replace("]", "］");
            return content;
        }

        private static string smethod_0(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8)
        {
            if (string_4 == "2" || string_4 == "3")
            {
                return smethod_2(string_5, string_4, string_6);
            }
            if (string_4 == "4" || string_4 == "5")
            {
                return smethod_1(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8);
            }
            if (string_4 == "1")
            {
                StringBuilder stringBuilder = new StringBuilder();
                string text = "";
                switch (string_5)
                {
                    case "stone":
                        {
                            text = "select top " + string_3 + " * from wap2_games_stone where state=0 and siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row in dataSet.Tables[0].Rows)
                            {
                                stringBuilder.Append(string.Concat("<a href=\"", string_7, "games/stone/doit.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;id=", row["id"], "\">", row["nickName"], "(ID", row["userid"], ")公开挑战(", row["myMoney"], "币)</a><br/>"));
                            }
                            return stringBuilder.ToString();
                        }
                    case "touzi":
                        {
                            text = "select top " + string_3 + " * from wap2_games_touzi where siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row2 in dataSet.Tables[0].Rows)
                            {
                                if (row2["Result"].ToString() == "")
                                {
                                    long tS = long.Parse((DateTime.Parse(row2["endTime"].ToString()) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
                                    stringBuilder.Append(string.Concat("第<b>", row2["touziID"], "</b>期还有", DateToString(tS, string_1, 0), "开盅,<a href=\"", string_7, "games/touzi/index.aspx?siteid=", string_6, "&amp;classid=", string_2, "\">瞧瞧</a><br/>"));
                                }
                                else
                                {
                                    stringBuilder.Append(string.Concat("第<a href=\"", string_7, "games/touzi/book_list.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;toid=", row2["touziID"], "\">", row2["touziID"], "</a>期开", row2["num1"].ToString(), row2["num2"].ToString(), row2["num3"].ToString(), row2["Result"], "<br/>"));
                                }
                            }
                            return stringBuilder.ToString();
                        }
                    case "apple":
                        {
                            text = "select top " + string_3 + " * from wap2_games_apple where siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row3 in dataSet.Tables[0].Rows)
                            {
                                if (row3["Result"].ToString() == "")
                                {
                                    long tS = long.Parse((DateTime.Parse(row3["endTime"].ToString()) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
                                    stringBuilder.Append(string.Concat("第<b>", row3["appleID"], "</b>期还有", DateToString(tS, string_1, 0), "开盅,<a href=\"", string_7, "games/apple/index.aspx?siteid=", string_6, "&amp;classid=", string_2, "\">瞧瞧</a><br/>"));
                                }
                                else
                                {
                                    stringBuilder.Append(string.Concat("第<a href=\"", string_7, "games/apple/book_list.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;toid=", row3["appleID"], "\">", row3["appleID"], "</a>期开", row3["Result"], "<br/>"));
                                }
                            }
                            return stringBuilder.ToString();
                        }
                    case "quankun":
                        {
                            text = "select top " + string_3 + " * from wap2_games_quankun where siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row4 in dataSet.Tables[0].Rows)
                            {
                                if (row4["Result"].ToString() == "")
                                {
                                    long tS = long.Parse((DateTime.Parse(row4["endTime"].ToString()) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
                                    stringBuilder.Append(string.Concat("第<b>", row4["periodID"], "</b>期还有", DateToString(tS, string_1, 0), "开盅,<a href=\"", string_7, "games/quankun/index.aspx?siteid=", string_6, "&amp;classid=", string_2, "\">瞧瞧</a><br/>"));
                                }
                                else
                                {
                                    stringBuilder.Append(string.Concat("第<a href=\"", string_7, "games/quankun/book_list.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;toid=", row4["periodID"], "\">", row4["periodID"], "</a>期开", row4["Result"], "<br/>"));
                                }
                            }
                            return stringBuilder.ToString();
                        }
                    case "chuiniu":
                        {
                            text = "select top " + string_3 + " * from wap2_games_chuiniu where state=0 and siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row5 in dataSet.Tables[0].Rows)
                            {
                                stringBuilder.Append(string.Concat("<a href=\"", string_7, "games/chuiniu/doit.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;id=", row5["id"], "\">", row5["Question"], "(", row5["myMoney"], "币)</a><br/>"));
                            }
                            return stringBuilder.ToString();
                        }
                    case "war":
                        {
                            text = "select top " + string_3 + " * from wap2_games_war where state=0 and siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row6 in dataSet.Tables[0].Rows)
                            {
                                stringBuilder.Append(string.Concat("<a href=\"", string_7, "games/war/doit.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;id=", row6["id"], "\">", row6["nickName"], "(ID", row6["userid"], ")公开挑战(", row6["myMoney"], "币)</a><br/>"));
                            }
                            return stringBuilder.ToString();
                        }
                    case "shoot":
                        {
                            text = "select top " + string_3 + " * from wap2_games_shoot where state=0 and siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row7 in dataSet.Tables[0].Rows)
                            {
                                stringBuilder.Append(string.Concat("<a href=\"", string_7, "games/shoot/doit.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;id=", row7["id"], "\">", row7["nickName"], "(ID", row7["userid"], ")公开挑战(", row7["myMoney"], "币)</a><br/>"));
                            }
                            return stringBuilder.ToString();
                        }
                    case "lucky28":
                        {
                            text = "select top " + string_3 + " * from wap2_games_lucky28 where siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row8 in dataSet.Tables[0].Rows)
                            {
                                if (row8["Result"].ToString() == "")
                                {
                                    long tS = long.Parse((DateTime.Parse(row8["endTime"].ToString()) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
                                    stringBuilder.Append(string.Concat("第<b>", row8["periodID"], "</b>期还有", DateToString(tS, string_1, 0), "开奖,<a href=\"", string_7, "games/lucky28/index.aspx?siteid=", string_6, "&amp;classid=", string_2, "\">瞧瞧</a><br/>"));
                                }
                                else
                                {
                                    stringBuilder.Append(string.Concat("第", row8["periodID"], "期开<a href=\"", string_7, "games/lucky28/book_list.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;toid=", row8["periodID"], "\">", row8["Result"], "=", row8["num1"], "+", row8["num2"], "+", row8["num3"], "</a><br/>"));
                                }
                            }
                            return stringBuilder.ToString();
                        }
                    case "horse":
                        {
                            text = "select top " + string_3 + " * from wap2_games_horse where siteid=" + string_6 + " order by id desc";
                            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                            {
                                return "";
                            }
                            foreach (DataRow row9 in dataSet.Tables[0].Rows)
                            {
                                if (row9["Result"].ToString() == "")
                                {
                                    long tS = long.Parse((DateTime.Parse(row9["endTime"].ToString()) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
                                    stringBuilder.Append(string.Concat("第<b>", row9["periodID"], "</b>场还有", DateToString(tS, string_1, 0), "开赛,<a href=\"", string_7, "games/horse/index.aspx?siteid=", string_6, "&amp;classid=", string_2, "\">瞧瞧</a><br/>"));
                                }
                                else
                                {
                                    stringBuilder.Append(string.Concat("第<a href=\"", string_7, "games/horse/book_list.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;toid=", row9["periodID"], "\">", row9["periodID"], "</a>场", row9["Result"], "胜出<br/>"));
                                }
                            }
                            return stringBuilder.ToString();
                        }
                }
            }
            return "";
        }

        private static string smethod_1(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "select top " + string_3 + " * from [wap2_games_rank] where gameen='" + string_5 + "' and siteid=" + string_6;
            if (string_4 == "4")
            {
                text += " order by rankTimes desc";
            }
            else if (string_4 == "5")
            {
                text += " order by rankMoney desc";
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (string_4 == "4")
                {
                    stringBuilder.Append(string.Concat("<a href=\"", string_7, "bbs/userinfo.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;touserid=", row["userid"], "\">", row["nickName"], "</a>(ID", row["userid"], ")净胜", row["rankTimes"], "局<br/>"));
                }
                else if (string_4 == "5")
                {
                    stringBuilder.Append(string.Concat("<a href=\"", string_7, "bbs/userinfo.aspx?siteid=", string_6, "&amp;classid=", string_2, "&amp;touserid=", row["userid"], "\">", row["nickName"], "</a>(ID", row["userid"], ")净胜", row["rankMoney"], "币<br/>"));
                }
            }
            return stringBuilder.ToString();
        }

        private static string smethod_2(string string_0, string string_1, string string_2)
        {
            string commandText = "select todaytimes,todaymoney from [wap2_games_config] where gameen='" + string_0 + "' and siteid=" + string_2;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (string_1 == "2")
                {
                    return dataSet.Tables[0].Rows[0]["todaytimes"].ToString();
                }
                if (string_1 == "3")
                {
                    return dataSet.Tables[0].Rows[0]["todaymoney"].ToString();
                }
            }
            return "0";
        }

        private static string smethod_3(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "";
            string text2 = "";
            string text3 = "";
            if (string_4 == "7" || string_4 == "1")
            {
                if (string_4 == "1")
                {
                    text2 = " and userid > 0 ";
                }
                if (string_2 == "0")
                {
                    text3 = "  order by newid() ";
                }
                if (string_2 == "1")
                {
                    text3 = " order by ftime desc";
                }
                text = "select top " + string_3 + " userid,nickname,classid,datediff(ss,ftime,getdate()) as passtime,classname from [fcount] where fuserid=" + string_6 + text2 + text3;
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (i > 0 && string_5 == "0")
                        {
                            stringBuilder.Append("<br/>");
                        }
                        else
                        {
                            stringBuilder.Append(" ");
                        }
                        if (string_0 == "0")
                        {
                            stringBuilder.Append("<a href=\"javascript:alert('UBB链接，请在浏览方式访问!');\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>于" + DateToString(long.Parse(dataSet.Tables[0].Rows[i]["passtime"].ToString()), string_1, 1) + "前" + dataSet.Tables[0].Rows[i]["classname"].ToString().Replace("[sid]", string_8));
                        }
                        else
                        {
                            stringBuilder.Append("<a href=\"" + string_7 + "bbs/userinfo.aspx?siteid=" + string_6 + "&amp;touserid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>于" + DateToString(long.Parse(dataSet.Tables[0].Rows[i]["passtime"].ToString()), string_1, 1) + "前" + dataSet.Tables[0].Rows[i]["classname"].ToString());
                        }
                    }
                }
            }
            else if (string_4 == "0")
            {
                DataSet dataSet = DbHelperSQL.ExecuteDataset(commandText: (!(string_2 == "1")) ? ("select  top " + string_3 + " * from (select top 20 * from [wap_log] where siteid=" + string_6 + " and oper_type=1 order by id desc) a order by newid()") : ("select top " + string_3 + " * from [wap_log] where siteid=" + string_6 + " and oper_type=1 order by id desc"), connectionString: _ConnStr, commandType: CommandType.Text);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (i > 0 && string_5 == "0")
                        {
                            stringBuilder.Append("<br/>");
                        }
                        else
                        {
                            stringBuilder.Append(" ");
                        }
                        if (string_0 == "0")
                        {
                            stringBuilder.Append("<a href=\"javascript:alert('UBB链接，请在浏览方式访问!');\">" + dataSet.Tables[0].Rows[i]["oper_nickname"].ToString() + "</a>于" + DateToString(DateTime.Parse(dataSet.Tables[0].Rows[i]["oper_time"].ToString()), string_1, 1) + "前" + dataSet.Tables[0].Rows[i]["log_info"].ToString().Replace("[sid]", string_8));
                        }
                        else
                        {
                            stringBuilder.Append("<a href=\"" + string_7 + "bbs/userinfo.aspx?siteid=" + string_6 + "&amp;touserid=" + dataSet.Tables[0].Rows[i]["oper_userid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["oper_nickname"].ToString() + "</a>于" + DateToString(DateTime.Parse(dataSet.Tables[0].Rows[i]["oper_time"].ToString()), string_1, 1) + "前" + dataSet.Tables[0].Rows[i]["log_info"].ToString());
                        }
                    }
                }
            }
            else
            {
                if (string_4 == "2")
                {
                    text2 = " order by userid desc ";
                }
                if (string_4 == "3")
                {
                    text2 = " and sex=1 order by userid desc ";
                }
                if (string_4 == "4")
                {
                    text2 = " and sex=0 order by userid desc ";
                }
                if (string_4 == "5")
                {
                    text2 = " order by money desc ";
                }
                if (string_4 == "6")
                {
                    text2 = " order by expr desc ";
                }
                text = "select top " + string_3 + " userid,nickname,sex,money,expr,regtime from [user] where siteid=" + string_6 + text2;
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (i > 0 && string_5 == "0")
                        {
                            stringBuilder.Append("<br/>");
                        }
                        else
                        {
                            stringBuilder.Append(" ");
                        }
                        if (string_4 == "5" || string_4 == "6")
                        {
                            if (string_0 == "0")
                            {
                                stringBuilder.Append("<a href=\"javascript:alert('手机或opera上访问!');\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>财富:" + dataSet.Tables[0].Rows[i]["money"].ToString() + "/经验:" + dataSet.Tables[0].Rows[i]["expr"].ToString());
                            }
                            else
                            {
                                stringBuilder.Append("<a href=\"" + string_7 + "bbs/userinfo.aspx?siteid=" + string_6 + "&amp;touserid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>财富:" + dataSet.Tables[0].Rows[i]["money"].ToString() + "/经验:" + dataSet.Tables[0].Rows[i]["expr"].ToString());
                            }
                        }
                        else if (string_0 == "0")
                        {
                            stringBuilder.Append("<a href=\"javascript:alert('手机或opera上访问!');\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "(ID:" + dataSet.Tables[0].Rows[i]["userid"].ToString() + ")</a>注册于" + string.Format("{0:MM-dd HH:mm}", DateTime.Parse(dataSet.Tables[0].Rows[i]["regtime"].ToString())));
                        }
                        else
                        {
                            stringBuilder.Append("<a href=\"" + string_7 + "bbs/userinfo.aspx?siteid=" + string_6 + "&amp;touserid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "(ID:" + dataSet.Tables[0].Rows[i]["userid"].ToString() + ")</a>注册于" + string.Format("{0:MM-dd HH:mm}", DateTime.Parse(dataSet.Tables[0].Rows[i]["regtime"].ToString())));
                        }
                    }
                }
            }
            return stringBuilder.ToString().Replace("[", "［").Replace("]", "］");
        }

        private static string smethod_4(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, wml wml_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "";
            string[] array = (string_9 + "**").Split('*');
            string text2 = array[0];
            string text3 = array[1];
            string text4 = array[2];
            text = "select top " + string_3 + " * from wap_novel_view where ischeck=0 and siteid=" + string_6;
            if (string_2 != "0")
            {
                text = ((string_2.IndexOf('|') <= 0) ? (text + " and book_classid=" + string_2) : (text + " and book_classid in (" + string_2.Replace("|", ",") + ")"));
            }
            switch (string_4)
            {
                case "1":
                    text += "  order by id desc ";
                    break;

                case "2":
                    text += "  order by book_click desc ";
                    break;

                case "3":
                    text += "  order by recommendnum desc ";
                    break;

                case "5":
                    if (!IsNumeric(KL_OrderByNew))
                    {
                        KL_OrderByNew = "100";
                    }
                    text = " select top " + string_3 + " * from (" + text.Replace("top " + string_3, "top " + KL_OrderByNew) + " order by id desc) t order by newid() ";
                    break;

                default:
                    text = " select top " + string_3 + " * from (" + text.Replace("top " + string_3, "top 20") + " order by id desc) t order by newid() ";
                    break;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                wml_0.parameter3 = string_2;
                if (int.Parse(text2) > 5)
                {
                    List<Page_Layout_Model> list = new List<Page_Layout_Model>();
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        Page_Layout_Model page_Layout_Model = new Page_Layout_Model();
                        page_Layout_Model.title = dataSet.Tables[0].Rows[i]["book_title"].ToString();
                        page_Layout_Model.content = dataSet.Tables[0].Rows[i]["book_info"].ToString();
                        if (dataSet.Tables[0].Rows[i]["book_img"].ToString().StartsWith("http") || dataSet.Tables[0].Rows[i]["book_img"].ToString().StartsWith("/"))
                        {
                            page_Layout_Model.imageURL = dataSet.Tables[0].Rows[i]["book_img"].ToString();
                        }
                        else
                        {
                            page_Layout_Model.imageURL = "/novel/" + dataSet.Tables[0].Rows[i]["book_img"].ToString();
                        }
                        page_Layout_Model.linkURL = string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString();
                        page_Layout_Model.time = dataSet.Tables[0].Rows[i]["lastupdatetime"].ToString();
                        list.Add(page_Layout_Model);
                    }
                    wml_0.parameter2 = "9";
                    return Page_Layout.GetListLayout(list, text2 + "*" + text3 + "*" + text4, wml_0, "novel");
                }
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        stringBuilder.Append("<br/>");
                    }
                    else
                    {
                        stringBuilder.Append(" ");
                    }
                    switch (text2)
                    {
                        case "1":
                            stringBuilder.Append("［<a href=\"" + string_7 + "novel/book_list.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"" + string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">");
                            if (string_5 != "0")
                            {
                                stringBuilder.Append(left(dataSet.Tables[0].Rows[i]["book_title"].ToString(), int.Parse(string_5)));
                            }
                            else if (string_5 == "0")
                            {
                                stringBuilder.Append(dataSet.Tables[0].Rows[i]["book_title"].ToString());
                                stringBuilder.Append("/" + dataSet.Tables[0].Rows[i]["book_author"].ToString());
                            }
                            stringBuilder.Append("</a>");
                            break;

                        case "2":
                            stringBuilder.Append("<a href=\"" + string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["book_title"].ToString() + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["LastUpdateTime"].ToString())) + "</span></a>");
                            break;

                        case "3":
                            stringBuilder.Append("[" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["LastUpdateTime"].ToString())) + "]<a href=\"" + string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["book_title"].ToString() + "</a>");
                            break;

                        case "4":
                            stringBuilder.Append(i + 1 + ".<a href=\"" + string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">");
                            if (string_5 != "0")
                            {
                                stringBuilder.Append(left(dataSet.Tables[0].Rows[i]["book_title"].ToString(), int.Parse(string_5)));
                            }
                            else if (string_5 == "0")
                            {
                                stringBuilder.Append(dataSet.Tables[0].Rows[i]["book_title"].ToString());
                                stringBuilder.Append("/" + dataSet.Tables[0].Rows[i]["book_author"].ToString());
                            }
                            stringBuilder.Append("</a>");
                            break;

                        default:
                            stringBuilder.Append("<a href=\"" + string_7 + "novel/book_listsect.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;bookid=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">");
                            if (string_5 != "0")
                            {
                                stringBuilder.Append(left(dataSet.Tables[0].Rows[i]["book_title"].ToString(), int.Parse(string_5)));
                            }
                            else if (string_5 == "0")
                            {
                                stringBuilder.Append(dataSet.Tables[0].Rows[i]["book_title"].ToString());
                                stringBuilder.Append("/" + dataSet.Tables[0].Rows[i]["book_author"].ToString());
                            }
                            stringBuilder.Append("</a>");
                            break;
                    }
                }
            }
            return stringBuilder.ToString().Replace("[", "［").Replace("]", "］");
        }

        private static string smethod_5(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, wml wml_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "";
            string[] array = (string_9 + "**").Split('*');
            string text2 = array[0];
            try
            {
                text = "select top " + string_3 + " * from wap_novel_section_VIEW  where ischeck=0 and siteid=" + string_6;
                if (string_2 != "0")
                {
                    text = ((string_2.IndexOf('|') <= 0) ? (text + " and book_classid=" + string_2) : (text + " and book_classid in (" + string_2.Replace("|", ",") + ")"));
                }
                switch (string_4)
                {
                    case "1":
                        text += "  order by sectionid desc ";
                        break;

                    case "2":
                        text += "  order by book_click desc ";
                        break;

                    case "3":
                        text += "  order by book_re desc ";
                        break;

                    case "5":
                        text = " select top " + string_3 + " * from (" + text.Replace("top " + string_3, "top 50") + " order by sectionid desc) t order by newid() ";
                        break;

                    default:
                        text = " select top " + string_3 + " * from (" + text.Replace("top " + string_3, "top 20") + " order by sectionid desc) t order by newid() ";
                        break;
                }
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (i > 0 && string_5 == "0")
                        {
                            stringBuilder.Append("<br/>");
                        }
                        else
                        {
                            stringBuilder.Append(" ");
                        }
                        switch (text2)
                        {
                            case "1":
                                if (ISAPI_Rewrite3_Open == "1")
                                {
                                    stringBuilder.Append("［<a href=\"" + string_7 + "novellist-" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"" + string_7 + "novel-" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + ".html</a>");
                                }
                                else
                                {
                                    stringBuilder.Append("［<a href=\"" + string_7 + "novel/book_list.aspx?siteid=" + string_6 + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"" + string_7 + "novel/view.aspx?id=" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                break;

                            case "2":
                                if (ISAPI_Rewrite3_Open == "1")
                                {
                                    stringBuilder.Append("<a href=\"" + string_7 + "novel-" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["addtime"].ToString())) + "</span></a>");
                                }
                                else
                                {
                                    stringBuilder.Append("<a href=\"" + string_7 + "novel/view.aspx?id=" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["addtime"].ToString())) + "</span></a>");
                                }
                                break;

                            case "3":
                                if (ISAPI_Rewrite3_Open == "1")
                                {
                                    stringBuilder.Append("[" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["addtime"].ToString())) + "]<a href=\"" + string_7 + "novel-" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                else
                                {
                                    stringBuilder.Append("[" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["addtime"].ToString())) + "]<a href=\"" + string_7 + "novel/view.aspx?id=" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                break;

                            case "4":
                                if (ISAPI_Rewrite3_Open == "1")
                                {
                                    stringBuilder.Append(i + 1 + ".<a href=\"" + string_7 + "novel-" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                else
                                {
                                    stringBuilder.Append(i + 1 + ".<a href=\"" + string_7 + "novel/view.aspx?id=" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                break;

                            default:
                                if (ISAPI_Rewrite3_Open == "1")
                                {
                                    stringBuilder.Append("<a href=\"" + string_7 + "novel-" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                else
                                {
                                    stringBuilder.Append("<a href=\"" + string_7 + "novel/view.aspx?id=" + dataSet.Tables[0].Rows[i]["sectionid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["sectiontitle"].ToString() + "</a>");
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stringBuilder.Append(ErrorToString(ex.ToString()));
            }
            return stringBuilder.ToString().Replace("[", "［").Replace("]", "］");
        }

        private static string smethod_6(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            string result = "0";
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("select count(id) as id from wap_" + string_0 + " where userid=" + string_5 + " and ischeck=0 ");
                if (string_1 != "0")
                {
                    stringBuilder.Append(" and book_classid=" + string_1);
                }
                switch (string_2)
                {
                    case "101":
                        stringBuilder.Append(" and DATEDIFF(day, book_date, getdate())=0 ");
                        break;

                    case "102":
                        stringBuilder.Append(" and DATEDIFF(day, book_date, getdate())=1 ");
                        break;

                    case "103":
                        stringBuilder.Append(" and DATEPART(wk, book_date) = DATEPART(wk, GETDATE()) and DATEPART(yy, book_date) = DATEPART(yy, GETDATE()) ");
                        break;

                    case "104":
                        stringBuilder.Append(" and DATEPART(mm, book_date) = DATEPART(mm, GETDATE()) and DATEPART(yy, book_date) = DATEPART(yy, GETDATE()) ");
                        break;

                    case "105":
                        stringBuilder.Append(" and DATEPART(qq, book_date) = DATEPART(qq, GETDATE()) and DATEPART(yy, book_date) = DATEPART(yy, GETDATE()) ");
                        break;

                    case "106":
                        stringBuilder.Append(" and DATEPART(yy, book_date) = DATEPART(yy, GETDATE()) ");
                        break;
                }
                if (string_0 == "article")
                {
                    stringBuilder.Replace("wap_article", "wap_book");
                }
                if (string_0 == "bbsre")
                {
                    stringBuilder.Replace("book_classid", "classid");
                    stringBuilder.Replace("book_date", "redate");
                    stringBuilder.Replace("userid", "devid");
                }
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    result = dataSet.Tables[0].Rows[0]["id"].ToString();
                }
            }
            catch (Exception)
            {
                result = "{格式错误ERROR}";
            }
            return result;
        }

        public static string GetWMLContent(string string_0, string strCid, string strSid, string strSiteId)
        {
            string result = "";
            if (IsNumeric(strCid))
            {
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select book_content,book_content2 from [wap_wml] where ischeck=0 and id=" + long.Parse(strCid) + " and (userid=1000 or userid=" + long.Parse(strSiteId) + ")");
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    result = ((!(string_0 == "1")) ? dataSet.Tables[0].Rows[0]["book_content2"].ToString() : dataSet.Tables[0].Rows[0]["book_content"].ToString());
                }
            }
            return result;
        }

        public static string GetTitle(string string_0, string lang, string WapHtmlStr, string strUserID, string strHttp_Start, string strSiteId, string strClassID, string strSid, wml vmlvo)
        {
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            string text5 = "";
            string text6 = "";
            int num = 0;
            string text7 = "";
            string text8 = WapHtmlStr.Split('=')[0];
            int num2 = CountTime(WapHtmlStr, "=", 1);
            string text9 = WapHtmlStr.Substring(num2, WapHtmlStr.Length - num2);
            if (text8 != "" && text8 != "rss" && text9 != "")
            {
                text9 += "_____";
                string[] array = text9.Split('_');
                text9 = array[0];
                text4 = array[1];
                text5 = array[2];
                text3 = array[3];
                text7 = array[4];
                text6 = text7.Split('*')[0];
                if (text6.IndexOf("[") > 0)
                {
                    return "{格式错误}";
                }
                if (!IsNumeric(text6))
                {
                    text6 = "0";
                }
                if (text8 == "games")
                {
                    return smethod_0(string_0, lang, text9, text4, text5, text3, strSiteId, strHttp_Start, strSid);
                }
                if (text4 == "" || !IsNumeric(text4))
                {
                    text4 = "1";
                }
                int num3 = Convert.ToInt32(text4);
                if (num3 > 99)
                {
                    return smethod_6(text8, text9, num3.ToString(), text5, text3, strSiteId);
                }
                if (num3 > 20)
                {
                    num3 = 20;
                }
                if (!IsNumeric(text5))
                {
                    text5 = "0";
                }
                if (!IsNumeric_FuShuOK(text3))
                {
                    text3 = "0";
                }
                if (text9.IndexOf("-") > 0)
                {
                    num = 1;
                    string text10 = text9.Split('-')[0];
                    string text11 = text9.Split('-')[1];
                    text10 = (IsNumeric(text10) ? $"{Convert.ToInt32(text10) - 1}" : "0");
                    text11 = (IsNumeric(text11) ? $"{Convert.ToInt32(text11) + 1}" : "0");
                    text = "   (book_classid > " + text10 + " and  book_classid < " + text11 + ") ";
                }
                if (text9.IndexOf("|") > 0)
                {
                    num = 1;
                    string[] array2 = text9.Split('|');
                    for (int i = 0; i < array2.Length; i++)
                    {
                        if (IsNumeric(array2[i]) && array2[i] != "")
                        {
                            text = text + array2[i] + ",";
                        }
                    }
                    text = " book_classid in (" + text + ") ";
                    text = text.Replace(",)", ")");
                }
                if (num == 0)
                {
                    if (text9 == "" || !IsNumeric(text9))
                    {
                        text9 = "0";
                        text = "";
                    }
                    else
                    {
                        text = " book_classid=" + text9;
                    }
                }
                switch (text8)
                {
                    case "users":
                        return smethod_3(string_0, lang, text9, text4, text5, text3, strSiteId, strHttp_Start, strSid);

                    case "novel":
                        return smethod_4(string_0, lang, text9, text4, text5, text3, strSiteId, strHttp_Start, strSid, text7, vmlvo);

                    case "section":
                        return smethod_5(string_0, lang, text9, text4, text5, text3, strSiteId, strHttp_Start, strSid, text7, vmlvo);

                    case "online":
                        if (string_0 == "0")
                        {
                            return "<a href=\"javascript:alert('UBB方式链接至:" + strHttp_Start + "bbs/onlist.aspx?siteid=" + strSiteId + "&amp;classid=" + text9 + "请用手机或IE访问！');\">" + GetOnlineCount(text9, strSiteId) + "</a>";
                        }
                        return "<a href=\"" + strHttp_Start + "bbs/onlist.aspx?siteid=" + strSiteId + "&amp;classid=" + text9 + "\">" + GetOnlineCount(text9, strSiteId) + "</a>";
                    case "getwml":
                        return GetWMLContent(string_0, text9, strSid, strSiteId);
                }
                string text12 = "";
                if (text8 != "linkactive")
                {
                    switch (text8)
                    {
                        case "bbstopic":
                            text8 = "bbs";
                            text12 = "select top " + text4 + " userid,book_classid,id,book_title from wap_" + text8 + " where ischeck=0 and userid=" + strSiteId;
                            text12 = ((!(text9 != "0")) ? (text12 + " and topic>0 ") : (text12 + " and  " + text.Replace("book_classid", "topic")));
                            break;

                        case "article":
                            text12 = "select top " + text4 + " userid,book_classid,id,book_title,book_content,book_date,book_img,classname from wap_book_view where ischeck=0 and userid=" + strSiteId;
                            if (text9 != "0")
                            {
                                text12 = text12 + " and " + text;
                            }
                            break;

                        default:
                            if (!(text8 == "dl"))
                            {
                                switch (text8)
                                {
                                    case "adlink":
                                        text12 = "select top " + text4 + " userid,book_classid,id,book_title,book_date,classname from wap_adlink_view where ischeck=0 ";
                                        if (text9 != "0")
                                        {
                                            text12 = text12 + " and " + text;
                                        }
                                        break;

                                    case "section":
                                        text12 = "select top " + text4 + " siteid as userid,book_classid,sectionid as id,sectiontitle as book_title,addtime as book_date,classname from wap_novel_section_view where 1=1 ";
                                        if (text9 != "0")
                                        {
                                            text12 = text12 + " and " + text;
                                        }
                                        break;

                                    default:
                                        if (!(text8 == "album"))
                                        {
                                            if (text8 == "chat")
                                            {
                                                text12 = "select top " + text4 + " nickname,classid as book_classid,content from wap_room where  siteid=" + strSiteId;
                                                if (text9 != "0")
                                                {
                                                    text12 = text12 + "  and " + text.Replace("book_classid", "classid");
                                                }
                                            }
                                            else if (text8 == "clanchat")
                                            {
                                                text12 = "select top " + text4 + " nickname,clan_id as book_classid,message as content from wap_clan_pk_message where type=5 and siteid=" + strSiteId;
                                                if (text9 == "1")
                                                {
                                                    string text13 = text12;
                                                    text12 = text13 + " and clan_id in( select clan_id from wap_clan_user where siteid=" + strSiteId + " and userid=" + strUserID + " )";
                                                }
                                                else if (text9 != "0")
                                                {
                                                    text12 = text12 + "  and " + text.Replace("book_classid", "clan_id");
                                                }
                                            }
                                            else if (long.Parse(text6) > 0L)
                                            {
                                                text12 = "select top " + text4 + " userid,book_classid,id,book_title,book_content,book_img,book_date,classname from wap_" + text8 + "_view where ischeck=0 and userid=" + strSiteId;
                                                if (text8 == "bbs" && strUserID != "")
                                                {
                                                    text12 += BlackTool.GetExcludeUserSql(strUserID, "book_pub");//排除拉黑的用户
                                                }
                                                if (text9 != "0")
                                                {
                                                    text12 = text12 + " and " + text;
                                                }
                                                if (text8 == "shop")
                                                {
                                                    text12 = text12.Replace("book_img", " replace(cast(book_file as varchar(500)),'|','') as book_img ");
                                                }
                                            }
                                            else
                                            {
                                                text12 = "select top " + text4 + " userid,book_classid,id,book_title from wap_" + text8 + " where ischeck=0 and userid=" + strSiteId;
                                                if (text9 != "0")
                                                {
                                                    text12 = text12 + " and " + text;
                                                }
                                            }
                                            break;
                                        }
                                        goto case "picture";
                                    case "picture":
                                        text12 = "select top " + text4 + " userid,book_classid,id,book_title,book_content,book_img,book_date,classname from wap_" + text8 + "_view where book_img is not null and ischeck=0 and userid=" + strSiteId;
                                        if (text9 != "0")
                                        {
                                            text12 = text12 + "  and " + text;
                                        }
                                        break;
                                }
                                break;
                            }
                            goto case "download";
                        case "download":
                            text12 = "select top " + text4 + " userid,book_classid,id,book_title,book_content,book_imgtrue as book_img,book_date,classname,attachementID,book_ext,book_re,book_score,book_size from wap_download_view where ischeck=0 and userid=" + strSiteId;
                            if (text9 != "0")
                            {
                                text12 = text12 + " and " + text;
                            }
                            break;
                    }
                    switch (text5)
                    {
                        case "1":
                            text12 += "  order by id desc ";
                            if (text8 == "download")
                            {
                                text12 = text12.Replace("by id desc", "by book_date desc,id desc");
                            }
                            break;

                        case "2":
                            text12 += "  order by book_click desc ";
                            break;

                        case "3":
                            text12 += "  and book_good=1 order by id desc ";
                            break;

                        case "4":
                            text12 += "  and book_top=1 order by id desc ";
                            if (text8 == "bbs")
                            {
                                text12 = text12.Replace("book_top=1", "book_top in(1,2)");
                            }
                            break;

                        case "5":
                            text12 = " select top " + text4 + " * from (" + text12.Replace("top " + text4, "top 50") + " order by id desc) t order by newid() ";
                            break;

                        case "6":
                            text12 += "  order by book_re desc ";
                            break;

                        case "7":
                            text12 += "  order by redate desc ";
                            break;

                        case "8":
                            text12 += "  and book_recommend=1 order by id desc ";
                            break;

                        default:
                            text12 = " select top " + text4 + " * from (" + text12.Replace("top " + text4, "top 20") + " order by id desc) t order by newid() ";
                            break;
                    }
                    if (text8 == "link")
                    {
                        text12 = text12.Replace("ischeck=0", "ishidden=0");
                    }
                    if (text8 == "wabao")
                    {
                        text12 = text12.Replace("userid", "siteid");
                        text12 = text12.Replace("siteid,", "siteid as userid,");
                    }
                    if (text8 == "rizhi")
                    {
                        text12 = text12.Replace("ischeck=0", "ischeck=0 and ishidden=0 and book_type=1");
                    }
                    if (text8 == "weibo")
                    {
                        text12 = text12.Replace("ischeck=0", "ischeck=0 and ishidden=0 and book_type=0");
                        text12 = text12.Replace("weibo", "rizhi");
                        text8 = "rizhi";
                    }
                    if (text8 == "hotel")
                    {
                        text12 = text12.Replace("userid", "siteid");
                        text12 = text12.Replace("siteid,", "siteid as userid,");
                        text12 = text12.Replace(",book_classid", ",classid as book_classid");
                        text12 = text12.Replace("book_title", "name as book_title");
                        text12 = text12.Replace("book_click", "hits");
                        text12 = text12.Replace("book_good", "isgood");
                        text12 = text12.Replace("book_classid=", "classid=");
                    }
                    if (text8 == "guangbo")
                    {
                        text12 = text12.Replace("userid", "siteid");
                        text12 = text12.Replace("siteid,", "siteid as userid,nickname,book_content,");
                        text12 = text12.Replace("ischeck=0", "ischeck=0 and DATEDIFF(n, GETDATE(),endtime) >= 0");
                    }
                    if (text8 == "airplane")
                    {
                        text12 = text12.Replace("userid", "siteid");
                        text12 = text12.Replace("siteid,", "siteid as userid,");
                        text12 = text12.Replace(",book_classid", ",classid as book_classid");
                        text12 = text12.Replace("book_title", "(planeNum+'.'+planeltd) as book_title");
                        text12 = text12.Replace("book_click", "hits");
                        text12 = text12.Replace("book_good", "isgood");
                        text12 = text12.Replace("book_classid=", "classid=");
                    }
                    if (text8 == "yuehui")
                    {
                        text12 = ((!(text9 != "0")) ? ("select top " + text4 + " siteid as userid,classid as book_classid,id,yh_title as book_title from wap_" + text8 + " where ischeck=0 and siteid=" + strSiteId) : ("select top " + text4 + " siteid as userid,classid as book_classid,id,yh_title as book_title from wap_" + text8 + " where ischeck=0 and siteid=" + strSiteId + " and " + text.Replace("book_classid", "classid")));
                        switch (text5)
                        {
                            case "1":
                                text12 += " order by id desc ";
                                break;

                            case "2":
                                text12 += " order by yh_click desc ";
                                break;

                            case "3":
                                text12 += " and yh_good=1 order by id desc ";
                                break;

                            case "5":
                                text12 = "select top " + text4 + " * from (" + text12.Replace("top " + text4, "top 50") + " order by id desc) t order by newid() ";
                                break;

                            default:
                                text12 = "select top " + text4 + " * from (" + text12.Replace("top " + text4, "top 20") + " order by id desc) t order by newid() ";
                                break;
                        }
                    }
                    if (text8 == "dl")
                    {
                        string[] array3 = strSid.Split('-');
                        string text14 = "0";
                        if (array3.Length > 4)
                        {
                            text14 = array3[4];
                        }
                        text12 = text12.Replace("wap_dl", "wap_download");
                        if (text6 == "5")
                        {
                            text12 = text12.Replace("userid,book_classid,id,book_title,book_date,classname", " * ");
                        }
                        if (text14 != "0")
                        {
                            text14 = GetMobileOS(text14);
                            if (text14 != "")
                            {
                                text12 = text12.Replace("ischeck=0", "ischeck=0 and book_img like '%" + text14 + "%' ");
                            }
                        }
                        text8 = "download";
                    }
                    if (text8 == "bbs" && long.Parse(text6) > 5L)
                    {
                        text12 = text12.Replace("where", "where book_img <>'' and ");
                    }
                }
                else
                {
                    if (!(text8 == "linkactive"))
                    {
                        return WapHtmlStr;
                    }
                    text12 = ((!(text9 != "0")) ? ("select top " + text4 + " userid,book_classid,id,book_title from wap_link where userid=" + strSiteId + " and ishidden=0 order by last_time  desc") : ("select top " + text4 + " userid,book_classid,id,book_title from wap_link where userid=" + strSiteId + " and " + text + " and ishidden=0 order by last_time  desc"));
                    text8 = "link";
                }
                try
                {
                    int num4 = 1;
                    DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text12);
                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<Page_Layout_Model> list = new List<Page_Layout_Model>();
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            if (long.Parse(text6) > 5L)
                            {
                                Page_Layout_Model page_Layout_Model = new Page_Layout_Model();
                                page_Layout_Model.title = GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8);
                                page_Layout_Model.content = dataSet.Tables[0].Rows[i]["book_content"].ToString();
                                page_Layout_Model.contentLen = text3;
                                page_Layout_Model.id = long.Parse(dataSet.Tables[0].Rows[i]["id"].ToString());
                                if (dataSet.Tables[0].Rows[i]["book_img"].ToString() == "")
                                {
                                    page_Layout_Model.imageURL = "/NetImages/load.gif";
                                }
                                else if (dataSet.Tables[0].Rows[i]["book_img"].ToString().StartsWith("/") || dataSet.Tables[0].Rows[i]["book_img"].ToString().ToLower().StartsWith("http://"))
                                {
                                    page_Layout_Model.imageURL = dataSet.Tables[0].Rows[i]["book_img"].ToString();
                                }
                                else
                                {
                                    page_Layout_Model.imageURL = strHttp_Start + text8 + "/" + dataSet.Tables[0].Rows[i]["book_img"].ToString();
                                }
                                if (string_0 == "0")
                                {
                                    page_Layout_Model.linkURL = "javascript: var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();";
                                }
                                else if (ISAPI_Rewrite3_Open == "1")
                                {
                                    page_Layout_Model.linkURL = strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html";
                                }
                                else
                                {
                                    page_Layout_Model.linkURL = strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString();
                                }
                                page_Layout_Model.time = dataSet.Tables[0].Rows[i]["book_date"].ToString();
                                list.Add(page_Layout_Model);
                            }
                            else if (long.Parse(text6) > 0L)
                            {
                                switch (text6)
                                {
                                    case "1":
                                        if (num4 > 1)
                                        {
                                            text2 += "<br/>";
                                        }
                                        text2 = ((!(string_0 == "0")) ? ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "［<a href=\"" + strHttp_Start + "wapindex.aspx?siteid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>") : (text2 + "［<a href=\"" + strHttp_Start + text8 + "list-" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>")) : (text2 + "［<a href=\"javascript:var klwin = window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a>］<a href=\"javascript:var klwin = window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>"));
                                        break;

                                    case "2":
                                        if (num4 > 1)
                                        {
                                            text2 += "<br/>";
                                        }
                                        text2 = ((!(string_0 == "0")) ? ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "</span></a>") : (text2 + "<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "</span></a>")) : (text2 + "<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + " <span class=\"right\">" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "</span></a>"));
                                        break;

                                    case "3":
                                        if (num4 > 1)
                                        {
                                            text2 += "<br/>";
                                        }
                                        text2 = ((!(string_0 == "0")) ? ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "［" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "］<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>") : (text2 + "［" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "］<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>")) : (text2 + "［" + string.Format("{0:MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + "］<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>"));
                                        break;

                                    case "4":
                                        if (num4 > 1)
                                        {
                                            text2 += "<br/>";
                                        }
                                        text2 = ((!(string_0 == "0")) ? ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + num4 + ".<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>") : (text2 + num4 + ".<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>")) : (text2 + num4 + ".<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>"));
                                        break;

                                    case "5":
                                        {
                                            text2 = ((i % 2 != 0) ? (text2 + "<div class=\"line\">") : (text2 + "<div class=\"line\">"));
                                            string text15 = "";
                                            text15 = ((string_0 == "0") ? ("<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">") : ((!(ISAPI_Rewrite3_Open == "1")) ? ("<a href=\"" + strHttp_Start + "download/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">") : ("<a href=\"" + strHttp_Start + "download-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">")));
                                            text2 += "<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tr><td width=\"60\" rowspan=\"3\" align =\"center\">";
                                            if (dataSet.Tables[0].Rows[i]["book_img"].ToString() != "")
                                            {
                                                text2 += text15;
                                                text2 = ((!dataSet.Tables[0].Rows[i]["book_img"].ToString().StartsWith("/") && !dataSet.Tables[0].Rows[i]["book_img"].ToString().ToLower().StartsWith("http://")) ? (text2 + "<img border=\"0\" src=\"" + strHttp_Start + "download/" + dataSet.Tables[0].Rows[i]["book_img"].ToString() + "\" alt=\"ICO\" width=\"60\" height=\"60\"/>") : (text2 + "<img border=\"0\" src=\"" + dataSet.Tables[0].Rows[i]["book_img"].ToString() + "\" alt=\"ICO\" width=\"60\" height=\"60\"/>"));
                                                text2 += "</a>";
                                            }
                                            text2 = text2 + "</td><td width=\"70%\" align =\"left\">" + text15 + dataSet.Tables[0].Rows[i]["book_title"].ToString() + "</a></td><td width=\"30%\" rowspan=\"3\" align =\"center\">";
                                            text2 = text2 + "<a href=\"" + strHttp_Start + "download/download.aspx?siteid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "&amp;book_id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "&amp;id=" + dataSet.Tables[0].Rows[i]["attachementID"].ToString() + "&amp;RndPath=" + vmlvo.siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(dataSet.Tables[0].Rows[i]["book_title"].ToString()) + "." + dataSet.Tables[0].Rows[i]["book_ext"].ToString().Replace(".", "") + "\" class=\"Btn\"><span class=\"KL_down_img\">下 载</span></a><br/>" + getStart(long.Parse(dataSet.Tables[0].Rows[i]["book_re"].ToString()), long.Parse(dataSet.Tables[0].Rows[i]["book_score"].ToString()), "★", "☆", "1");
                                            text2 = ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "</td></tr><tr><td width=\"70%\" align =\"left\"><a href=\"" + strHttp_Start + "download/book_list.aspx?siteid=" + dataSet.Tables[0].Rows[i]["userid"].ToString() + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a></td></tr><tr><td width=\"70%\" align =\"left\">" + string.Format("{0:yyy-MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + " | " + dataSet.Tables[0].Rows[i]["book_size"].ToString() + "</td>") : (text2 + "</td></tr><tr><td width=\"70%\" align =\"left\"><a href=\"" + strHttp_Start + "downloadlist-" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["classname"].ToString() + "</a></td></tr><tr><td width=\"70%\" align =\"left\">" + string.Format("{0:yyy-MM-dd}", DateTime.Parse(dataSet.Tables[0].Rows[i]["book_date"].ToString())) + " | " + dataSet.Tables[0].Rows[i]["book_size"].ToString() + "</td>"));
                                            text2 += "</tr></table></div>";
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                text2 = ((num4 <= 1 || (int.Parse(text3) <= 5 && " |0|-2|-3|".IndexOf("|" + text3) <= 0)) ? (text2 + " ") : (text2 + "<br/>"));
                                if (text3 == "-1" || text3 == "-2")
                                {
                                    if (string_0 == "0")
                                    {
                                        string text13 = text2;
                                        text2 = text13 + "<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\"><img src=\"" + strHttp_Start + text8 + "/" + dataSet.Tables[0].Rows[i]["book_img"].ToString() + "\" alt=\"" + dataSet.Tables[0].Rows[i]["book_title"].ToString() + "\"/></a>";
                                    }
                                    else if (ISAPI_Rewrite3_Open == "1")
                                    {
                                        string text13 = text2;
                                        text2 = text13 + "<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\"><img src=\"" + strHttp_Start + text8 + "/" + dataSet.Tables[0].Rows[i]["book_img"].ToString() + "\" alt=\"" + dataSet.Tables[0].Rows[i]["book_title"].ToString() + "\"/></a>";
                                    }
                                    else
                                    {
                                        string text13 = text2;
                                        text2 = text13 + "<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\"><img src=\"" + strHttp_Start + text8 + "/" + dataSet.Tables[0].Rows[i]["book_img"].ToString() + "\" alt=\"" + dataSet.Tables[0].Rows[i]["book_title"].ToString() + "\"/></a>";
                                    }
                                }
                                else
                                {
                                    switch (text8)
                                    {
                                        case "chat":
                                            text2 = ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "<a href=\"" + strHttp_Start + "chat/book_list.aspx?siteid=" + strSiteId + "&amp;classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>说:" + GetShowImg(dataSet.Tables[0].Rows[i]["content"].ToString(), text3, text8)) : (text2 + "<a href=\"" + strHttp_Start + "chatlist-" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + ".html\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>说:" + GetShowImg(dataSet.Tables[0].Rows[i]["content"].ToString(), text3, text8)));
                                            break;

                                        case "clanchat":
                                            text2 = text2 + "<a href=\"" + strHttp_Start + "clan/chat_list.aspx?siteid=" + strSiteId + "&amp;classid=0&amp;id=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "\">" + dataSet.Tables[0].Rows[i]["nickname"].ToString() + "</a>说:" + GetShowImg(dataSet.Tables[0].Rows[i]["content"].ToString(), text3, text8);
                                            break;

                                        case "guangbo":
                                            if (text3 == "-3")
                                            {
                                                text2 = text2 + dataSet.Tables[0].Rows[i]["nickname"].ToString() + ":";
                                            }
                                            text2 += GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8);
                                            if (dataSet.Tables[0].Rows[i]["book_content"].ToString().Trim() != "")
                                            {
                                                text2 = ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + " <a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">详情&gt;&gt;</a>") : (text2 + " <a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">详情&gt;&gt;</a>"));
                                            }
                                            break;

                                        default:
                                            text2 = ((!(string_0 == "0")) ? ((!(ISAPI_Rewrite3_Open == "1")) ? (text2 + "<a href=\"" + strHttp_Start + text8 + "/view.aspx?id=" + dataSet.Tables[0].Rows[i]["id"].ToString() + "\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>") : (text2 + "<a href=\"" + strHttp_Start + text8 + "-" + dataSet.Tables[0].Rows[i]["id"].ToString() + ".html\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>")) : (text2 + "<a href=\"javascript:var klwin=window.showModalDialog('/admin/ContentTree2.aspx?function=" + text8 + "&classid=" + dataSet.Tables[0].Rows[i]["book_classid"].ToString() + "',null,'dialogWidth=900px;dialogHeight=700px;menuba=no;resizable=yes');if(klwin=='refresh') window.location.reload();\">" + GetShowImg(dataSet.Tables[0].Rows[i]["book_title"].ToString(), text3, text8) + "</a>"));
                                            break;
                                    }
                                }
                            }
                            num4++;
                        }
                        if (long.Parse(text6) > 5L)
                        {
                            vmlvo.parameter3 = text9;
                            vmlvo.parameter2 = "10";
                            text2 = Page_Layout.GetListLayout(list, text7, vmlvo, text8);
                        }
                    }
                }
                catch (Exception ex)
                {
                    text2 = text2 + "{请将Y或Z换其它参数，格式错误:" + ErrorToStringFirstLine(ex.ToString()) + "}";
                }
            }
            return text2;
        }

        public static string GetAllMid(string string_0, string lang, string WapHtmlStr, string RelplaceStr, string strUserID, string strHttp_Start, string strSiteId, string strClassID, string strSid, wml wmlvo)
        {
            try
            {
                int num = WapHtmlStr.IndexOf("[" + RelplaceStr + "=");
                if (num < 0)
                {
                    return WapHtmlStr;
                }
                int num2 = WapHtmlStr.IndexOf("]", num);
                string text = WapHtmlStr.Substring(num + 1, num2 - num - 1);
                if ("|users|link|wml|games|dl|adlink|linkactive|".IndexOf(RelplaceStr) < 0 && "1".Equals(KL_OpenCache))
                {
                    string value = "";
                    DataTempArray.TryGetValue(strSiteId + "[" + text + "]", out value);
                    if (value != null && value != "")
                    {
                        WapHtmlStr = WapHtmlStr.Replace("[" + text + "]", value);
                    }
                    else
                    {
                        string title = GetTitle(string_0, lang, text, strUserID, strHttp_Start, strSiteId, strClassID, strSid, wmlvo);
                        WapHtmlStr = WapHtmlStr.Replace("[" + text + "]", title);
                        try
                        {
                            DataTempArray.Add(strSiteId + "[" + text + "]", title);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    WapHtmlStr = WapHtmlStr.Replace("[" + text + "]", GetTitle(string_0, lang, text, strUserID, strHttp_Start, strSiteId, strClassID, strSid, wmlvo));
                }
                WapHtmlStr = GetAllMid(string_0, lang, WapHtmlStr, RelplaceStr, strUserID, strHttp_Start, strSiteId, strClassID, strSid, wmlvo);
            }
            catch (Exception)
            {
                return WapHtmlStr.Replace("[" + RelplaceStr + "=", "[格式错误ERROR=");
            }
            return WapHtmlStr;
        }

        public static string UBBCode(string WapStr, wml wmlVo)
        {
            if (WapStr.IndexOf('[') < 0 || WapStr.IndexOf(']') < 0)
            {
                return WapStr;
            }
            WapStr = UBB.CenterIntercept(WapStr, wmlVo);
            if (WapStr.IndexOf("[/img]") > 0)
            {
                if (wmlVo.strUrl.ToLower().IndexOf("index") > 0)
                {
                    Regex regex = new Regex("(\\[img\\])(.[^\\[]*)(\\[\\/img\\])");
                    WapStr = ((!(wmlVo.ver == "0"))
                        ? regex.Replace(WapStr, "<img src=\"$2\" referrerpolicy=\"no-referrer\" alt=\".\"/>")
                        : regex.Replace(WapStr, "<a href=\"javascript:T('{{img}}$2{{/img}}');\"><img src=\"$2\" referrerpolicy=\"no-referrer\" alt=\".\"/></a>"));
                }
                else
                {
                    //Regex regex = new Regex("(\\[img\\])(.[^\\[|^\\?^&]*\\.(gif|jpg|bmp|jpeg|png|GIF|JPG))(\\[\\/img\\])");
                    Regex regex = new Regex("(\\[img\\])(.[^\\[|^\\?^&]*\\.(gif|jpg|bmp|jpeg|png|webp))(\\[\\/img\\])", RegexOptions.IgnoreCase);
                    WapStr = ((!(wmlVo.ver == "0"))
                        ? regex.Replace(WapStr, "<img class=\"ubbimg\" src=\"$2\" referrerpolicy=\"no-referrer\" alt=\".\"/>")
                        : regex.Replace(WapStr, "<a href=\"javascript:T('{{img}}$2{{/img}}');\"><img src=\"$2\" referrerpolicy=\"no-referrer\" alt=\".\"/></a>"));
                }
                //Regex regex2 = new Regex("(\\[img=(.[^\\[|^\\?^&]*\\.(gif|jpg|bmp|jpeg|png|GIF|JPG))\\])(.[^\\[]*)(\\[\\/img\\])");
                Regex regex2 = new Regex("(\\[img=(.[^\\[|^\\?^&]*\\.(gif|jpg|bmp|jpeg|png|webp))\\])(.[^\\[]*)(\\[\\/img\\])", RegexOptions.IgnoreCase);
                WapStr = ((!(wmlVo.ver == "0"))
                    ? regex2.Replace(WapStr, "<img src=\"$2\" referrerpolicy=\"no-referrer\" alt=\"$4\"/>")
                    : regex2.Replace(WapStr, "<a href=\"javascript:T('{{img=$2}}$4{{/img}}');\"><img src=\"$2\" referrerpolicy=\"no-referrer\" alt=\"$4\"/></a>"));
            }
            //if (WapStr.IndexOf("[/bgsound]") > 0)
            //{
            //    Regex regex = new Regex("(\\[bgsound\\])(.[^\\[]*)(\\[\\/bgsound\\])");
            //    WapStr = regex.Replace(WapStr, "<bgsound src=\"$2\" loop=\"infinite\"/>");
            //}
            if (WapStr.IndexOf("[/call]") > 0)
            {
                Regex regex = new Regex("(\\[call\\])(.[^\\[]*)(\\[\\/call\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"tel:$2\">$2</a>") : regex.Replace(WapStr, "<a href=\"javascript:T('{{call}}$2{{/call}}');\">$2</a>"));
                regex = new Regex("(\\[call=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/call\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"tel:$2\">$3</a>") : regex.Replace(WapStr, "<a href=\"javascript:T('{{call=$2}}$3{{/call}}');\">$3</a>"));
            }
            //if (WapStr.IndexOf("[/map]") > 0)
            //{
            //    Regex regex = new Regex("(\\[map\\])(.[^\\[]*)(\\[\\/map\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string[] array = match.Groups[2].Value.Split('*');
            //            WapStr = regex.Replace(WapStr, Page_Layout.GetMap(array[0], array[1], "", ""), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //    regex = new Regex("(\\[map=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/map\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string[] array = match.Groups[2].Value.Split('*');
            //            string[] array2 = match.Groups[3].Value.Split('*');
            //            WapStr = regex.Replace(WapStr, Page_Layout.GetMap(array2[0], array2[1], array[0], array[1]), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/shopstate]") > 0)
            //{
            //    Regex regex = new Regex("(\\[shopstate\\])(.[^\\[]*)(\\[\\/shopstate\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value;
            //            WapStr = regex.Replace(WapStr, GetShopState(wmlVo, value), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            if (WapStr.IndexOf("[/back]") > 0)
            {
                Regex regex = new Regex("(\\[back\\])(.[^\\[]*)(\\[\\/back\\])");
                WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "<a href=\"wapindexEdit.aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + wmlVo.parentid + "\">$2</a>") : ((!(ISAPI_Rewrite3_Open == "1")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "wapindex.aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + wmlVo.parentid + "\">$2</a>") : regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "wapindex-" + wmlVo.siteid + "-" + wmlVo.parentid + ".html\">$2</a>")));
            }
            if (WapStr.IndexOf("[/return]") > 0)
            {
                Regex regex = new Regex("(\\[return\\])(.[^\\[]*)(\\[\\/return\\])");
                WapStr = ((!(wmlVo.ver == "1")) ? regex.Replace(WapStr, "<a href=\"javascript:window.history.back();\">$2</a>") : regex.Replace(WapStr, "<anchor title=\"back\"><prev/>$2</anchor>"));
            }
            if (WapStr.IndexOf("[/index]") > 0)
            {
                Regex regex = new Regex("(\\[index\\])(.[^\\[]*)(\\[\\/index\\])");
                WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "<a href=\"javascript:T('{{index}}$2{{/index}}');\">$2</a>") : ((!(ISAPI_Rewrite3_Open == "1")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "wapindex.aspx?siteid=" + wmlVo.siteid + "&amp;classid=0\">$2</a>") : regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "wapindex-" + wmlVo.siteid + "-0.html\">$2</a>")));
            }
            if (WapStr.IndexOf("[/text]") > 0)
            {
                Regex regex = new Regex("(\\[text\\])(.[^\\[]*)(\\[\\/text\\])");
                if (wmlVo.ver == "0")
                {
                    WapStr = regex.Replace(WapStr, "<a href=\"javascript:T('{{text}}$2{{/text}}');\">$2</a>");
                }
                else
                {
                    foreach (Match matche in regex.Matches(WapStr))
                    {
                        var bbsData = matche.Value;
                        var bbsContent = Regex.Match(bbsData, "(?<=\\[text\\]).+?(?=\\[\\/text\\])").Value;
                        bbsContent = bbsContent.Replace("“", "\"")
                                                .Replace("‘", "'")
                                                .Replace("”", "\"")
                                                .Replace("’", "'")
                                                .Replace("《", "<")
                                                .Replace("》", ">")
                                                .Replace("：", ";")
                                                .Replace("，", ",")
                                                .Replace("。", ".")
                                                .Replace("【", "[")
                                                .Replace("】", "]");
                        WapStr = WapStr.Replace(bbsData, bbsContent);
                    }
                }
            }
            if (WapStr.IndexOf("[/a]") > 0)
            {
                Regex regex = new Regex("(\\[a\\])(.[^\\[]*)(\\[\\/a\\])");
                WapStr = ((!(wmlVo.ver == "1")) ? regex.Replace(WapStr, "<a name=\"$2\"></a>") : regex.Replace(WapStr, ""));
                regex = new Regex("(\\[a=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/a\\])");
                WapStr = ((!(wmlVo.ver == "1")) ? regex.Replace(WapStr, "<a href=\"#$2\">$3</a>") : regex.Replace(WapStr, ""));
            }
            if (WapStr.IndexOf("[/url]") > 0)
            {
                // 正则表达式匹配 [url]...[/url] 形式
                Regex regex = new Regex("(\\[url\\])(.[^\\[]*)(\\[\\/url\\])");
                WapStr = regex.Replace(WapStr, match =>
                {
                    string url = match.Groups[2].Value.Trim(); // 去掉前后的空格
                                                               // 检查是否包含 javascript: 协议
                    if (url.ToLower().Replace(" ", "").StartsWith("javascript:"))
                    {
                        return "链接无效";
                    }
                    return wmlVo.ver == "0" ? $"<a href=\"javascript:T('{{url}}{url}{{/url}}');\">{url}</a>" : $"<a href=\"{url}\">{url}</a>";
                });

                // 正则表达式匹配 [url=...]...[/url] 形式
                regex = new Regex("(\\[url=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/url\\])");
                WapStr = regex.Replace(WapStr, match =>
                {
                    string url = match.Groups[2].Value.Trim(); // 去掉前后的空格
                    string linkText = match.Groups[3].Value;
                    // 检查是否包含 javascript: 协议
                    if (url.ToLower().Replace(" ", "").StartsWith("javascript:"))
                    {
                        return "链接无效";
                    }
                    return wmlVo.ver == "0" ? $"<a href=\"javascript:T('{{url={url}}}{linkText}{{/url}}');\">{linkText}</a>" : $"<a href=\"{url}\">{linkText}</a>";
                });
            }
            if (WapStr.IndexOf("[/js]") > 0)
            {
                Regex regex = new Regex("(\\[js\\])(.[^\\[]*)(\\[\\/js\\])");
                WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "<a href=\"javascript:T('{{js}}$2{{/js}}');\">{JS}</a>") : ((wmlVo.ver == "1") ? regex.Replace(WapStr, "") : ((wmlVo.strUrl.ToLower().IndexOf("index") <= 0) ? regex.Replace(WapStr, "{此处UBB只有排版页面有效其它页面无效}") : regex.Replace(WapStr, "<script type=\"text/javascript\" src=\"$2\"></script>"))));
            }
            //if (WapStr.IndexOf("[/css]") > 0)
            //{
            //    Regex regex = new Regex("(\\[css\\])(.[^\\[]*)(\\[\\/css\\])");
            //    WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "<a href=\"javascript:T('{{css}}$2{{/css}}');\">{CSS}</a>") : ((!(wmlVo.ver == "1")) ? regex.Replace(WapStr, "<link href=\"$2\" rel=\"stylesheet\" type=\"text/css\">") : regex.Replace(WapStr, "")));
            //}
            //if (WapStr.IndexOf("[/rndurl]") > 0)
            //{
            //    Regex regex = new Regex("(\\[rndurl=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/rndurl\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array3 = value.Split('|');
            //            string[] array4 = text.Split('|');
            //            int num = random.Next(0, array3.Length);
            //            WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + array3[num] + "\">" + showImg(array4[num]) + "</a>", 1) : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB方法:" + array3[num] + "');\">" + showImg(array4[num]) + "</a>", 1));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            if (WapStr.IndexOf("[/rndtxt]") > 0)
            {
                Regex regex = new Regex("(\\[rndtxt\\])(.[^\\[]*)(\\[\\/rndtxt\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        string text2 = match.Groups[2].Value.Replace("｜", "|");
                        string[] array3 = text2.Split('|');
                        int num = random.Next(0, array3.Length);
                        WapStr = regex.Replace(WapStr, showImg(array3[num]), 1);
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                }
            }
            if (WapStr.IndexOf("[/imgurl]") > 0)
            {
                Regex regex = new Regex("(\\[imgurl=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/imgurl\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        string value = match.Groups[2].Value.Replace("｜", "|");
                        string text = match.Groups[3].Value.Replace("｜", "|");
                        string[] array5 = value.Split('*');
                        string[] array6 = text.Split('*');
                        string text3 = array5[0];
                        string text4 = array5[1];
                        StringBuilder stringBuilder = new StringBuilder();
                        if (!IsNumeric(text3))
                        {
                            text3 = "0";
                        }
                        else
                        {
                            stringBuilder.Append(" width=" + text3);
                        }
                        if (!IsNumeric(text4))
                        {
                            text4 = "0";
                        }
                        else
                        {
                            stringBuilder.Append(" height=" + text4);
                        }
                        WapStr = ((!(wmlVo.ver == "0")) ? ((!(wmlVo.ver == "1")) ? regex.Replace(WapStr, "<a href=\"" + array6[1] + "\"><img src=\"" + array6[0] + "\"  border=\"0\" " + stringBuilder.ToString() + " alt=\"Load...\"/></a>", 1) : regex.Replace(WapStr, "<a href=\"" + array6[1] + "\"><img src=\"" + array6[0] + "\" alt=\"Load...\"/></a>", 1)) : regex.Replace(WapStr, "<a href=\"javascript:T('{{imgurl=$2}}$3{{/imgurl}}');\"><img src=\"" + array6[0] + "\"  border=\"0\" " + stringBuilder.ToString() + " alt=\"Load...\"/></a>", 1));
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                }
            }
            //if (WapStr.IndexOf("[/iframe]") > 0)
            //{
            //    Regex regex = new Regex("(\\[iframe=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/iframe\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array5 = value.Split('*');
            //            string text3 = array5[0];
            //            string text4 = array5[1];
            //            StringBuilder stringBuilder = new StringBuilder();
            //            if (!IsNumeric(text3))
            //            {
            //                text3 = "0";
            //            }
            //            else
            //            {
            //                stringBuilder.Append(" width=" + text3);
            //            }
            //            if (!IsNumeric(text4))
            //            {
            //                text4 = "0";
            //            }
            //            else
            //            {
            //                stringBuilder.Append(" height=" + text4);
            //            }
            //            WapStr = ((!(wmlVo.ver == "0")) ? ((!(wmlVo.ver == "1")) ? ((wmlVo.strUrl.ToLower().IndexOf("index") <= 0) ? regex.Replace(WapStr, "{此处UBB只有排版页面有效，其它页面无效}", 1) : regex.Replace(WapStr, "<iframe src=\"" + text + "\" " + stringBuilder.ToString() + " scrolling=\"yes\"></iframe>", 1)) : regex.Replace(WapStr, "<a href=\"" + text + "\">{查看页面}</a>", 1)) : regex.Replace(WapStr, "<a href=\"javascript:T('{{iframe=$2}}$3{{/iframe}}');\"><img src=\"\"  border=\"0\" " + stringBuilder.ToString() + " alt=\"页面框架，预览网站能看到效果。\"/></a>", 1));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/float]") > 0)
            //{
            //    Regex regex = new Regex("(\\[float=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/float\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array5 = value.Split('*');
            //            string[] array6 = text.Split('*');
            //            string text3 = array5[0];
            //            string text4 = array5[1];
            //            string text5 = array5[2];
            //            string text6 = array5[3];
            //            string text7 = "0";
            //            try
            //            {
            //                text7 = array5[4];
            //            }
            //            catch (Exception)
            //            {
            //                text7 = "0";
            //            }
            //            StringBuilder stringBuilder = new StringBuilder();
            //            if (!IsNumeric(text3))
            //            {
            //                text3 = "30";
            //            }
            //            if (!IsNumeric(text4))
            //            {
            //                text4 = "30";
            //            }
            //            if (!IsNumeric(text5))
            //            {
            //                text4 = "10";
            //            }
            //            if (!IsNumeric(text6))
            //            {
            //                text6 = "20";
            //            }
            //            if (int.Parse(text3) > 320)
            //            {
            //                text3 = "150";
            //            }
            //            if (int.Parse(text4) > 320)
            //            {
            //                text4 = "150";
            //            }
            //            WapStr = ((!(wmlVo.ver == "0")) ? ((wmlVo.strUrl.ToLower().IndexOf("ustomeronline") <= 0) ? ((!(text7 == "1")) ? regex.Replace(WapStr, "<a href='" + array6[1] + "'><img id='AdLayer1' style='position: absolute;visibility:hidden;z-index:1' src='" + array6[0] + "' width=" + text3 + "px  height=" + text4 + "px alt='快点我哦～' /></a><script>KL_AD_X=" + text5 + "; KL_AD_Y=" + text6 + "</script><script type=\"text/javascript\" src=\"/NetCSS/adjs.js\"></script>", 1) : regex.Replace(WapStr, "<a href='" + array6[1] + "'><img id='AdLayer1' style='position: absolute;visibility:hidden;z-index:1' src='" + array6[0] + "' width=" + text3 + "px  height=" + text4 + "px alt='快点我哦～' /></a><script>KL_AD_X=" + text5 + "; KL_AD_Y=" + text6 + "</script><script type=\"text/javascript\" src=\"/NetCSS/adjs_right.js\"></script>", 1)) : regex.Replace(WapStr, "", 1)) : ((!(text7 == "1")) ? regex.Replace(WapStr, "<a href='" + array6[1] + "'><img id='AdLayer1' style='position: absolute;visibility:hidden;z-index:1' src='" + array6[0] + "' width=" + text3 + "px  height=" + text4 + "px alt='快点我哦～' /></a><script>KL_AD_X=" + text5 + "; KL_AD_Y=" + text6 + "</script><script type=\"text/javascript\" src=\"/NetCSS/adjs.js\"></script>", 1) : regex.Replace(WapStr, "<a href='" + array6[1] + "'><img id='AdLayer1' style='position: absolute;visibility:hidden;z-index:1' src='" + array6[0] + "' width=" + text3 + "px  height=" + text4 + "px alt='快点我哦～' /></a><script>KL_AD_X=" + text5 + "; KL_AD_Y=" + text6 + "</script><script type=\"text/javascript\" src=\"/NetCSS/adjs_right.js\"></script>", 1)));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            if (WapStr.IndexOf("[/getform]") > 0)
            {
                Regex regex = new Regex("(\\[getform\\])(.[^\\[]*)(\\[\\/getform\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        WapStr = regex.Replace(WapStr, Page_Layout.GetFormEdit(wmlVo, match.Groups[2].Value), 1);
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                    UpdateSystemAuto();
                }
            }
            //if (WapStr.IndexOf("[/picurl8]") > 0)
            //{
            //    Regex regex = new Regex("(\\[picurl8=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/picurl8\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        int num2 = 0;
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array6 = text.Split('*');
            //            string[] array3 = array6[1].Split('|');
            //            string[] array4 = array6[0].Split('|');
            //            List<Page_Layout_Model> list = new List<Page_Layout_Model>();
            //            for (int num = 0; num < array3.Length; num++)
            //            {
            //                Page_Layout_Model page_Layout_Model = new Page_Layout_Model();
            //                if (wmlVo.ver == "0")
            //                {
            //                    page_Layout_Model.linkURL = "javascript:T('{{picurl8=$2}}$3{{/picurl8}}');";
            //                }
            //                else
            //                {
            //                    page_Layout_Model.linkURL = array3[num];
            //                }
            //                page_Layout_Model.imageURL = array4[num];
            //                page_Layout_Model.title = ".";
            //                page_Layout_Model.content = "";
            //                list.Add(page_Layout_Model);
            //            }
            //            num2++;
            //            wmlVo.parameter2 = num2.ToString();
            //            WapStr = regex.Replace(WapStr, Page_Layout.GetListLayout(list, "8*" + value, wmlVo, ""), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/picurl9]") > 0)
            //{
            //    Regex regex = new Regex("(\\[picurl9=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/picurl9\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        int num2 = 0;
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array6 = text.Split('*');
            //            string[] array3 = array6[1].Split('|');
            //            string[] array4 = array6[0].Split('|');
            //            List<Page_Layout_Model> list = new List<Page_Layout_Model>();
            //            for (int num = 0; num < array3.Length; num++)
            //            {
            //                Page_Layout_Model page_Layout_Model = new Page_Layout_Model();
            //                if (wmlVo.ver == "0")
            //                {
            //                    page_Layout_Model.linkURL = "javascript:T('{{picurl9=$2}}$3{{/picurl9}}');";
            //                }
            //                else
            //                {
            //                    page_Layout_Model.linkURL = array3[num];
            //                }
            //                page_Layout_Model.imageURL = array4[num];
            //                page_Layout_Model.title = ".";
            //                page_Layout_Model.content = "";
            //                list.Add(page_Layout_Model);
            //            }
            //            num2++;
            //            wmlVo.parameter2 = num2.ToString();
            //            WapStr = regex.Replace(WapStr, Page_Layout.GetListLayout(list, "9*" + value, wmlVo, ""), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/picurl10]") > 0)
            //{
            //    Regex regex = new Regex("(\\[picurl10=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/picurl10\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        Random random = new Random();
            //        int num2 = 2;
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value.Replace("｜", "|");
            //            string text = match.Groups[3].Value.Replace("｜", "|");
            //            string[] array6 = text.Split('*');
            //            string[] array3 = array6[1].Split('|');
            //            string[] array4 = array6[0].Split('|');
            //            List<Page_Layout_Model> list = new List<Page_Layout_Model>();
            //            for (int num = 0; num < array3.Length; num++)
            //            {
            //                Page_Layout_Model page_Layout_Model = new Page_Layout_Model();
            //                if (wmlVo.ver == "0")
            //                {
            //                    page_Layout_Model.linkURL = "javascript:T('{{picurl10=$2}}$3{{/picurl10}}');";
            //                }
            //                else
            //                {
            //                    page_Layout_Model.linkURL = array3[num];
            //                }
            //                page_Layout_Model.imageURL = array4[num];
            //                page_Layout_Model.title = ".";
            //                page_Layout_Model.content = "";
            //                list.Add(page_Layout_Model);
            //            }
            //            num2++;
            //            wmlVo.parameter2 = num2.ToString();
            //            WapStr = regex.Replace(WapStr, Page_Layout.GetListLayout(list, "10*" + value, wmlVo, ""), 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            if (WapStr.IndexOf("[/movie]") > 0)
            {
                Regex regex = new Regex("(\\[movie=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/movie\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        string[] array = match.Groups[2].Value.Replace("｜", "|").Split('*');
                        string[] array2 = match.Groups[3].Value.Replace("｜", "|").Split('|');
                        string text8 = array2[1];
                        if (text8 == "")
                        {
                            text8 = "/NetImages/play.gif";
                        }
                        //宽度
                        string width = array[0];
                        bool isWidthPercentage = width.EndsWith("%");
                        width = isWidthPercentage ? width.TrimEnd('%') : width;
                        string widthAttribute = isWidthPercentage ? $"width=\"{width}%\"" : $"width=\"{width}px\"";
                        //高度
                        string height = array[1];
                        bool isHeightPercentage = height.EndsWith("%");
                        height = isHeightPercentage ? height.TrimEnd('%') : height;
                        string heightAttribute = isHeightPercentage ? $"height=\"{height}%\"" : $"height=\"{height}px\"";
                        WapStr = regex.Replace(WapStr, "<video class=\"ubbvideo\" onclick=\"if(this.paused) { this.play();}else{ this.pause();}\" src=\"" + array2[0]
                            + "\" " + widthAttribute + " " + heightAttribute
                            + " poster=\"" + text8 + "\" controls>{不支持在线播放，请更换浏览器}</video>", 1);
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                }
            }
            if (WapStr.IndexOf("[/audio]") > 0)
            {
                Regex regex = new Regex("(\\[audio=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/audio\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        string value = match.Groups[2].Value.Replace("｜", "|");
                        string text = match.Groups[3].Value.Replace("｜", "|");
                        string text9 = "";
                        if (value == "1")
                        {
                            text9 = " autoplay ";
                        }
                        else if (value == "2")
                        {
                            text9 = " autoplay loop ";
                        }
                        WapStr = regex.Replace(WapStr, "<audio referrerpolicy='no-referrer' controls src=\"" + text + "\" " + text9 + ">{不支持在线播放，请更换浏览器}</audio>", 1);
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                }
            }
            if (WapStr.IndexOf("[/msg]") > 0)
            {
                Regex regex = new Regex("(\\[msg\\])(.[^\\[]*)(\\[\\/msg\\])");
                try
                {
                    Match match = regex.Match(WapStr);
                    Random random = new Random();
                    while (match.Success)
                    {
                        string text2 = match.Groups[2].Value;
                        string message = GetMessage(wmlVo.ver, wmlVo.userid, wmlVo.siteid, wmlVo.http_start, wmlVo.sid, wmlVo.classid, "1");
                        if (message != "0")
                        {
                            text2 = text2.Replace("x", message) ?? "";
                            text2 = ((!(wmlVo.ver == "0")) ? ("<a href=\"" + wmlVo.http_start + "bbs/messagelist.aspx?siteid=" + wmlVo.siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + wmlVo.siteid + "&amp;classid=" + wmlVo.classid) + "\">" + text2 + "</a>") : ("<a href=\"javascript:alert('UBB方法：链接至" + wmlVo.http_start + "bbs/messagelist.aspx?siteid=" + wmlVo.siteid + "\n\n请用IE或手机访问测试！');\">" + text2 + "</a>"));
                            WapStr = regex.Replace(WapStr, text2, 1);
                        }
                        else
                        {
                            WapStr = regex.Replace(WapStr, "", 1);
                        }
                        match = match.NextMatch();
                    }
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{格式错误}");
                }
            }
            //if (WapStr.IndexOf("[/friend]") > 0)
            //{
            //    Regex regex = new Regex("(\\[friend\\])(.[^\\[]*)(\\[\\/friend\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        while (match.Success)
            //        {
            //            string message = GetFriendCount(wmlVo.siteid, wmlVo.userid, match.Groups[2].Value);
            //            WapStr = regex.Replace(WapStr, message, 1);
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/anchor]") > 0)
            //{
            //    Regex regex = new Regex("(\\[anchor=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/anchor\\])");
            //    WapStr = ((wmlVo.ver == "1" || wmlVo.mycss == "") ? regex.Replace(WapStr, "<anchor>$3<go sendreferer=\"true\" href=\"$2\"></go></anchor>") : ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<input type=\"button\" value=\"$3\" name=\"BT\" onclick=\"javascript:window.location.href='$2';\" >") : regex.Replace(WapStr, "<input type=\"button\" value=\"$3\" name=\"BT\" onclick=\"javascript:alert('UBB方法链接至:$2 请用手机或IE访问测试！');\" >")));
            //}
            if (WapStr.IndexOf("[/i]") > 0)
            {
                Regex regex = new Regex("(\\[i\\])(.[^\\[]*)(\\[\\/i\\])");
                WapStr = regex.Replace(WapStr, "<i>$2</i>");
            }
            if (WapStr.IndexOf("[/u]") > 0)
            {
                Regex regex = new Regex("(\\[u\\])(.[^\\[]*)(\\[\\/u\\])");
                WapStr = regex.Replace(WapStr, "<u>$2</u>");
            }
            if (WapStr.IndexOf("[/b]") > 0)
            {
                Regex regex = new Regex("(\\[b\\])(.[^\\[]*)(\\[\\/b\\])");
                WapStr = regex.Replace(WapStr, "<b>$2</b>");
            }
            if (WapStr.IndexOf("[/input]") > 0)
            {
                Regex regex = new Regex("(\\[input=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/input\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "<input type=\"text\" format=\"*m\" name=\"key\" size=\"$2\" value=\"$3\"/>"));
            }
            if (WapStr.IndexOf("[/option]") > 0)
            {
                Regex regex = new Regex("(\\[option=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/option\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<option value=\"$2\">$3</option>") : regex.Replace(WapStr, "<option onpick=\"$2\">$3</option>"));
            }
            if (WapStr.IndexOf("[/sel]") > 0)
            {
                Regex regex = new Regex("(\\[sel\\])(.[^\\[]*)(\\[\\/sel\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<select name=\"sel\" onchange=\"window.location.href=this.value;\">$2</select>") : regex.Replace(WapStr, "<select value=\"0\">$2</select>"));
            }
            if (WapStr.IndexOf("[/ancho]") > 0)
            {
                Regex regex = new Regex("(\\[ancho=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/ancho\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "<anchor><go href=\"" + wmlVo.http_start + "$2/book_list.aspx\" method=\"get\"><postfield name=\"action\" value=\"search\"/><postfield name=\"siteid\" value=\"" + wmlVo.siteid + "\"/><postfield name=\"classid\" value=\"$3\"/><postfield name=\"key\" value=\"$(key)\"/><postfield name=\"type\" value=\"title\"/><postfield name=\"sid\" value=\"" + wmlVo.sid + "\"/></go>$4</anchor>"));
            }
            if (wmlVo.ver == "1" || wmlVo.mycss == "" || wmlVo.ver == "0")
            {
                WapStr = WapStr.Replace("[form2]", "");
                WapStr = WapStr.Replace("[/form2]", "");
            }
            else
            {
                WapStr = WapStr.Replace("[form2]", "<form name=\"fs\" action=\"" + wmlVo.http_start + "search.aspx\" method=\"get\">");
                WapStr = WapStr.Replace("[/form2]", "<input type=\"hidden\" name=\"sid\" value=\"" + wmlVo.sid + "\"/><input type=\"hidden\" name=\"siteid\" value=\"" + wmlVo.siteid + "\"/><input type=\"hidden\" name=\"classid\" value=\"" + wmlVo.classid + "\"/></form>");
            }
            if (WapStr.IndexOf("[/input2]") > 0)
            {
                Regex regex = new Regex("(\\[input2=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/input2\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<input type=\"text\" name=\"key\" class=\"KL_search_txt\" size=\"$2\" value=\"$3\"/>") : regex.Replace(WapStr, ""));
            }
            if (WapStr.IndexOf("[/option2]") > 0)
            {
                WapStr = WapStr.Replace("[/option2][br][option2=", "[/option2][option2=");
                Regex regex = new Regex("(\\[option2=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/option2\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<option value=\"$2\">$3</option>") : regex.Replace(WapStr, " "));
            }
            if (WapStr.IndexOf("[/sel2]") > 0)
            {
                Regex regex = new Regex("(\\[sel2\\])(.[^\\[]*)(\\[\\/sel2\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<select name=\"function\" class=\"KL_search_sel\">$2</select>") : regex.Replace(WapStr, ""));
            }
            if (WapStr.IndexOf("[/button2]") > 0)
            {
                Regex regex = new Regex("(\\[button2\\])(.[^\\[]*)(\\[\\/button2\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<input type=\"submit\" class=\"urlbtn\" value=\"$2\"/>") : regex.Replace(WapStr, ""));
            }
            if (WapStr.IndexOf("[/urlancho]") > 0)
            {
                Regex regex = new Regex("(\\[urlancho=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/urlancho\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<form name=\"f\" action=\"$2\" method=\"get\"/><input type=\"button\" value=\"$4\"></form>") : regex.Replace(WapStr, "<anchor><go href=\"$2\" method=\"get\" accept-charset=\"utf-8\"><postfield name=\"$3\" value=\"$(key)\"/></go>$4</anchor>"));
            }
            if (WapStr.IndexOf("[/search]") > 0)
            {
                Regex regex = new Regex("(\\[search=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/search\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_search.aspx?siteid=" + wmlVo.siteid + "&amp;classid=$3\">$4</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_search.aspx?siteid=" + wmlVo.siteid + "&amp;classid=$3');\">$4</a>"));
            }
            if (WapStr.IndexOf("[/key]") > 0)
            {
                Regex regex = new Regex("(\\[key=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/key\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_list.aspx?action=search&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3&amp;type=title&amp;key=$4\">$4</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_list.aspx?action=search&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3&amp;type=title&amp;key=$4');\">$4</a>"));
            }
            if (WapStr.IndexOf("[/new]") > 0)
            {
                Regex regex = new Regex("(\\[new=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/new\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_list.aspx?action=new&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3\">$4</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_list.aspx?action=new&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3');\">$4</a>"));
            }
            if (WapStr.IndexOf("[/hot]") > 0)
            {
                Regex regex = new Regex("(\\[hot=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/hot\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_list.aspx?action=hot&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3\">$4</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_list.aspx?action=hot&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3');\">$4</a>"));
            }
            if (WapStr.IndexOf("[/good]") > 0)
            {
                Regex regex = new Regex("(\\[good=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/good\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_list.aspx?action=good&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3\">$4</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_list.aspx?action=good&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3');\">$4</a>"));
            }
            if (WapStr.IndexOf("[/days]") > 0)
            {
                Regex regex = new Regex("(\\[days=(.[^\\]]*)_(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/days\\])");
                WapStr = ((!(wmlVo.ver == "0")) ? regex.Replace(WapStr, "<a href=\"" + wmlVo.http_start + "$2/book_list.aspx?action=search&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3&amp;type=days&amp;key=$4\">$5</a>") : regex.Replace(WapStr, "<a href=\"javascript:alert('UBB链接至:" + wmlVo.http_start + "$2/book_list.aspx?action=search&amp;siteid=" + wmlVo.siteid + "&amp;classid=$3&amp;type=days&amp;key=$4');\">$5</a>"));
            }
            if (WapStr.IndexOf("[/login]") > 0)
            {
                Regex regex = new Regex("(\\[login\\])(.[^\\[]*)(\\[\\/login\\])");
                WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "{此处内容须登陆后才能浏览}") : ((!(wmlVo.userid == "0")) ? regex.Replace(WapStr, "$2") : regex.Replace(WapStr, "{此处内容须<a href=\"" + wmlVo.http_start + "waplogin.aspx?siteid=" + wmlVo.siteid + "&amp;classid=0&amp;sid" + wmlVo.sid + "\">登陆</a>后才能浏览}")));
            }
            if (WapStr.IndexOf("[/mobi]") > 0)
            {
                Regex regex = new Regex("(\\[mobi\\])(.[^\\[]*)(\\[\\/mobi\\])");
                WapStr = ((wmlVo.ver == "0") ? regex.Replace(WapStr, "{此处内容须手机才能浏览}") : ((isAllowUA(wmlVo.UA) || isAllowIP(wmlVo.IP)) ? regex.Replace(WapStr, "$2") : regex.Replace(WapStr, "{此处内容须手机才能浏览}")));
            }
            if (WapStr.IndexOf("[/coin]") > 0)
            {
                Regex regex = new Regex("(\\[coin=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/coin\\])");
                if (wmlVo.ver == "0")
                {
                    WapStr = regex.Replace(WapStr, "{此处内容需要您拥有金钱数达到<b>$2</b>才可以浏览}");
                }
                else
                {
                    Match match = regex.Match(WapStr);
                    string value2 = match.Groups[2].Value;
                    if (IsNumeric(value2))
                    {
                        WapStr = ((Convert.ToInt64(wmlVo.money) <= 0L || Convert.ToInt64(wmlVo.money) < Convert.ToInt64(value2)) ? regex.Replace(WapStr, "{此处内容需要您拥有金钱数达到<b>$2</b>才可以浏览}") : regex.Replace(WapStr, "$3"));
                    }
                }
            }
            if (WapStr.IndexOf("[/grade]") > 0)
            {
                Regex regex = new Regex("(\\[grade=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/grade\\])");
                if (wmlVo.ver == "0")
                {
                    WapStr = regex.Replace(WapStr, "{此处内容需要您拥有经验达到<b>$2</b>才可以浏览}");
                }
                else
                {
                    Match match = regex.Match(WapStr);
                    string value2 = match.Groups[2].Value;
                    if (IsNumeric(value2))
                    {
                        WapStr = ((Convert.ToInt64(wmlVo.expR) <= 0L || Convert.ToInt64(wmlVo.expR) < Convert.ToInt64(value2)) ? regex.Replace(WapStr, "{此处内容需要您拥有经验达到<b>$2</b>才可以浏览}") : regex.Replace(WapStr, "$3"));
                    }
                }
            }
            if (WapStr.IndexOf("[/vip]") > 0)
            {
                Regex regex = new Regex("(\\[vip=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/vip\\])");
                if (wmlVo.ver == "0")
                {
                    WapStr = regex.Replace(WapStr, "{此处内容需要您拥有<b>VIP$2</b>才可以浏览}");
                }
                else
                {
                    Match match = regex.Match(WapStr);
                    string value2 = match.Groups[2].Value;
                    WapStr = ((("|" + value2 + "|").IndexOf("|" + wmlVo.userVo.SessionTimeout + "|") < 0) ? regex.Replace(WapStr, "{此处内容需要您拥有身份级别：<b>" + GetCardIDNameFormID_multiple(wmlVo.siteid, value2, wmlVo.lang) + "</b>才可以浏览，我当前为：" + GetMyID(wmlVo.userVo.idname, wmlVo.lang, wmlVo.userVo.endTime) + "}") : ((showIDEndTime(wmlVo.siteVo.siteid, wmlVo.userVo.userid, wmlVo.userVo.endTime) >= 1L) ? regex.Replace(WapStr, "$3") : regex.Replace(WapStr, "{抱歉，此处内容需要您拥有身份级别：<b>" + GetCardIDNameFormID_multiple(wmlVo.siteid, value2, wmlVo.lang) + "</b>才可以浏览，我当前为：" + GetMyID(wmlVo.userVo.idname, wmlVo.lang, wmlVo.userVo.endTime) + "，我的身份已过期：" + showIDEndTime(wmlVo.userVo.siteid, wmlVo.userVo.userid, wmlVo.userVo.endTime) + "天，请<a href=\"" + wmlVo.http_start + "bbs/toGroupInfo.aspx?siteid=" + wmlVo.siteid + "\">点击此充值</a>！}")));
                }
            }
            if (WapStr.IndexOf("[/font]") > 0)
            {
                Regex regex = new Regex("(\\[font=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/font\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<font face='$2'>$3</font>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/size]") > 0)
            {
                Regex regex = new Regex("(\\[size=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/size\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<font size='$2'>$3</font>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/backcolor]") > 0)
            {
                Regex regex = new Regex("(\\[backcolor=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/backcolor\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<span style='background-color: $2'>$3</span>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/forecolor]") > 0)
            {
                Regex regex = new Regex("(\\[forecolor=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/forecolor\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<font color='$2'>$3</font>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[img=") > 0)
            {
                Regex regex = new Regex("\\[img=([0-9]+)(?:,([0-9]+))?\\](.[^\\[]*)\\[\\/img]");
                MatchCollection matches = regex.Matches(WapStr);
                foreach (Match match in matches)
                {
                    string width = match.Groups[1].Value;
                    string height = match.Groups[2].Success ? match.Groups[2].Value : "";
                    string imageUrl = match.Groups[3].Value;
                    string replacement = "";
                    if (string.IsNullOrEmpty(height))
                    {
                        replacement = $"<img class=\"ubbimg\" src=\"{imageUrl}\" width=\"{width}\" />";
                    }
                    else
                    {
                        replacement = $"<img class=\"ubbimg\" src=\"{imageUrl}\" width=\"{width}\" height=\"{height}\" />";
                    }
                    WapStr = WapStr.Replace(match.Value, replacement);
                }
            }
            if (WapStr.IndexOf("[/strike]") > 0)
            {
                Regex regex = new Regex("(\\[strike\\])(.[^\\[]*)(\\[\\/strike\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<strike>$2</strike>") : regex.Replace(WapStr, "$2"));
            }
            if (WapStr.IndexOf("[/tq]") > 0)
            {
                WapStr = WapStr.Replace("[tq]", "[tq=0]");
                Regex regex = new Regex("(\\[tq=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/tq\\])");
                Match match = regex.Match(WapStr);
                Random random = new Random();
                while (match.Success)
                {
                    string value = match.Groups[2].Value;
                    if (wmlVo.cityCode == "")
                    {
                        wmlVo.cityCode = match.Groups[3].Value;
                    }
                    StringBuilder stringBuilder2 = new StringBuilder();
                    switch (value)
                    {
                        case "0":
                            stringBuilder2.Append("<iframe allowtransparency=\"true\" frameborder=\"0\" width=\"317\" height=\"18\" scrolling=\"no\" src=\"http://tianqi.2345.com/plugin/widget/index.htm?s=3&z=1&t=1&v=0&d=1&bd=0&k=&f=&q=1&e=0&a=1&c=54511&w=317&h=18&align=center\"></iframe>");
                            break;

                        case "1":
                            stringBuilder2.Append("<iframe allowtransparency=\"true\" frameborder=\"0\" width=\"317\" height=\"64\" scrolling=\"no\" src=\"http://tianqi.2345.com/plugin/widget/index.htm?s=2&z=3&t=1&v=2&d=2&bd=0&k=&f=&q=0&e=1&a=1&c=54511&w=317&h=64&align=center\"></iframe>");
                            break;

                        case "2":
                            stringBuilder2.Append("<iframe allowtransparency=\"true\" frameborder=\"0\" width=\"317\" height=\"98\" scrolling=\"no\" src=\"http://tianqi.2345.com/plugin/widget/index.htm?s=1&z=1&t=1&v=0&d=2&bd=0&k=&f=&q=1&e=1&a=1&c=54511&w=317&h=98&align=center\"></iframe>");
                            break;
                    }
                    WapStr = regex.Replace(WapStr, stringBuilder2.ToString(), 1);
                    match = match.NextMatch();
                }
            }
            if (WapStr.IndexOf("[/codo]") > 0)
            {
                Regex regex = new Regex("(\\[codo\\])(.[^\\[]*)(\\[\\/codo\\])");
                Match match = regex.Match(WapStr);
                try
                {
                    WapStr = regex.Replace(WapStr, DateTime.Parse(match.Groups[2].Value + " 00:00:01").Subtract(DateTime.Now).TotalDays.ToString("f0"));
                }
                catch (Exception)
                {
                    WapStr = regex.Replace(WapStr, "{DateTime ERROR}");
                }
            }
            if (WapStr.IndexOf("[/u]") > 0)
            {
                Regex regex = new Regex("(\\[u=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/u\\])");
                if (wmlVo.ver == "1")
                {
                    WapStr = regex.Replace(WapStr, "$3");
                }
                else
                {
                    Match match = regex.Match(WapStr);
                    while (match.Success)
                    {
                        string value3 = match.Groups[3].Value;
                        value3 = value3.Replace("<br/>", "</li><li>");
                        WapStr = regex.Replace(WapStr, "<u class=\"$2\"><li>" + value3 + "</li></u>", 1);
                        match = match.NextMatch();
                    }
                }
            }
            if (WapStr.IndexOf("[/tab]") > 0)
            {
                Regex regex = new Regex("(\\[tab=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/tab\\])");
                if (wmlVo.ver == "1")
                {
                    WapStr = regex.Replace(WapStr, " ");
                }
                else
                {
                    int num3 = 0;
                    Match match = regex.Match(WapStr);
                    while (match.Success)
                    {
                        num3++;
                        StringBuilder stringBuilder3 = new StringBuilder();
                        string[] array7 = match.Groups[2].Value.Split('|');
                        string[] array8 = match.Groups[3].Value.Split('|');
                        if (array7.Length != array8.Length)
                        {
                            WapStr = regex.Replace(WapStr, "{格式错误:|个数对不上}");
                        }
                        else
                        {
                            stringBuilder3.Append("<div class=\"tab\" >");
                            stringBuilder3.Append("<div class=\"tabTitle\" id=\"tabTitle" + num3 + "\">");
                            for (int num = 0; num < array7.Length; num++)
                            {
                                stringBuilder3.Append("<a href=\"javascript:void(0);\" ");
                                if (num == 0)
                                {
                                    stringBuilder3.Append("class=\"selecter\" ");
                                }
                                if (num == array7.Length - 1)
                                {
                                    stringBuilder3.Append("style=\"border:none;\" ");
                                }
                                stringBuilder3.Append(">" + array7[num] + "</a>");
                            }
                            stringBuilder3.Append(" </div>");
                            stringBuilder3.Append("<div id=\"theTabContent" + num3 + "\">");
                            for (int num = 0; num < array8.Length; num++)
                            {
                                stringBuilder3.Append("<div class=\"tabContent\" ");
                                if (num != 0)
                                {
                                    stringBuilder3.Append("style=\"display:none;\" ");
                                }
                                stringBuilder3.Append(">");
                                stringBuilder3.Append("<ul><li>");
                                array8[num] = array8[num].Replace("<br/>", "</li><li>");
                                if (array8[num].EndsWith("</li><li>"))
                                {
                                    array8[num] = right(array8[num], array8[num].Length - 4);
                                }
                                else
                                {
                                    array8[num] += "</li>";
                                }
                                stringBuilder3.Append(array8[num]);
                                stringBuilder3.Append("</ul>");
                                stringBuilder3.Append("</div>");
                            }
                            stringBuilder3.Append("</div></div>");
                            stringBuilder3.Append("<script>var Tab" + num3 + "=new Tab({ControlPanelID:\"tabTitle" + num3 + "\",ContentPanelID:\"theTabContent" + num3 + "\"});</script>");
                        }
                        WapStr = regex.Replace(WapStr, stringBuilder3.ToString(), 1);
                        match = match.NextMatch();
                    }
                }
            }
            //if (WapStr.IndexOf("[/fly]") > 0)
            //{
            //    Regex regex = new Regex("(\\[fly\\])(.[^\\[]*)(\\[\\/fly\\])");
            //    WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<marquee  behavior=\"scroll\" scrollamount=\"2\" onMouseOut=\"this.start()\" onMouseOver=\"this.stop()\">$2</marquee>") : regex.Replace(WapStr, "$2"));
            //}
            WapStr = UBB.EndIntercept(WapStr, wmlVo);
            //if (WapStr.IndexOf("[/span2]") > 0)
            //{
            //    Regex regex = new Regex("(\\[span2=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/span2\\])");
            //    WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<span class=\"$2\">$3</span>") : regex.Replace(WapStr, "$3"));
            //}
            //if (WapStr.IndexOf("[/span1]") > 0)
            //{
            //    Regex regex = new Regex("(\\[span1=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/span1\\])");
            //    WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<span class=\"$2\">$3</span>") : regex.Replace(WapStr, "$3"));
            //}
            //if (WapStr.IndexOf("[/span]") > 0)
            //{
            //    Regex regex = new Regex("(\\[span=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/span\\])");
            //    WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<span class=\"$2\">$3</span>") : regex.Replace(WapStr, "$3"));
            //}
            if (WapStr.IndexOf("[/p]") > 0)
            {
                Regex regex = new Regex("(\\[p=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/p\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<p class=\"$2\">$3</p>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/div5]") > 0)
            {
                Regex regex = new Regex("(\\[div5=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div5\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/div4]") > 0)
            {
                Regex regex = new Regex("(\\[div4=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div4\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/div3]") > 0)
            {
                Regex regex = new Regex("(\\[div3=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div3\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/div2]") > 0)
            {
                Regex regex = new Regex("(\\[div2=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div2\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/div1]") > 0)
            {
                Regex regex = new Regex("(\\[div1=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div1\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/table]") > 0)
            {
                Regex regex = new Regex("(\\[td\\])(.[^\\[]*)(\\[\\/td\\])");
                WapStr = regex.Replace(WapStr, "<td>$2</td>");
                Regex regex3 = new Regex("(\\[td=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/td\\])");
                WapStr = regex3.Replace(WapStr, "<td class=\"$2\">$3</td>");
                Regex regex4 = new Regex("(\\[tr\\])(.[^\\[]*)(\\[\\/tr\\])");
                WapStr = regex4.Replace(WapStr, "<tr>$2</tr>");
                Regex regex5 = new Regex("(\\[tr=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/tr\\])");
                WapStr = regex5.Replace(WapStr, "<tr class=\"$2\">$3</tr>");
                Regex regex6 = new Regex("(\\[table\\])(.[^\\[]*)(\\[\\/table\\])");
                WapStr = regex6.Replace(WapStr, "<table>$2</table>");
                Regex regex7 = new Regex("(\\[table=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/table\\])");
                WapStr = regex7.Replace(WapStr, "<talbe class=\"$2\">$3</table>");
            }
            if (WapStr.IndexOf("[/div]") > 0)
            {
                Regex regex = new Regex("(\\[div=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/div\\])");
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "<div class=\"$2\">$3</div>") : regex.Replace(WapStr, "$3"));
            }
            if (WapStr.IndexOf("[/wap1]") > 0)
            {
                Regex regex = new Regex("(\\[wap1\\])(.[^\\[]*)(\\[\\/wap1\\])");
                if (wmlVo.ver == "2" || wmlVo.ver == "3")
                {
                    WapStr = regex.Replace(WapStr, "");
                }
                WapStr = ((!(wmlVo.ver == "1") && !(wmlVo.mycss == "")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            }
            if (WapStr.IndexOf("[/wap2]") > 0)
            {
                Regex regex = new Regex("(\\[wap2\\])(.[^\\[]*)(\\[\\/wap2\\])");
                WapStr = ((!(wmlVo.ver == "2") && (!(wmlVo.ver == "0") || !(wmlVo.mycss != ""))) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            }
            //if (WapStr.IndexOf("[/wap3]") > 0)
            //{
            //    Regex regex = new Regex("(\\[wap3\\])(.[^\\[]*)(\\[\\/wap3\\])");
            //    WapStr = ((!(wmlVo.ver == "3")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            //}
            //if (WapStr.IndexOf("[/wap4]") > 0)
            //{
            //    Regex regex = new Regex("(\\[wap4\\])(.[^\\[]*)(\\[\\/wap4\\])");
            //    WapStr = ((!(wmlVo.ver == "4")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            //}
            //if (WapStr.IndexOf("[/wap5]") > 0)
            //{
            //    Regex regex = new Regex("(\\[wap5\\])(.[^\\[]*)(\\[\\/wap5\\])");
            //    WapStr = ((!(wmlVo.ver == "5")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            //}
            if (WapStr.IndexOf("[/nologins]") > 0)
            {
                Regex regex = new Regex("(\\[nologins\\])(.[^\\[]*)(\\[\\/nologins\\])");
                WapStr = ((!(wmlVo.userid != "0")) ? regex.Replace(WapStr, "$2") : regex.Replace(WapStr, ""));
            }
            if (WapStr.IndexOf("[/logins]") > 0)
            {
                Regex regex = new Regex("(\\[logins\\])(.[^\\[]*)(\\[\\/logins\\])");
                WapStr = ((!(wmlVo.userid != "0")) ? regex.Replace(WapStr, "") : regex.Replace(WapStr, "$2"));
            }
            //if (WapStr.IndexOf("[/show]") > 0)
            //{
            //    string text10 = wmlVo.strUrl.Split('?')[0];
            //    Regex regex = new Regex("(\\[show=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/show\\])");
            //    Match match = regex.Match(WapStr);
            //    while (match.Success)
            //    {
            //        string value4 = match.Groups[2].Value;
            //        string value5 = match.Groups[3].Value;
            //        string value6 = match.Groups[4].Value;
            //        WapStr = ((text10.ToLower().IndexOf(value4.ToLower()) <= -1 && value4.ToLower().IndexOf(text10.ToLower()) <= -1 && ("," + value4 + ",").ToLower().IndexOf("," + wmlVo.classid + ",") <= -1) ? regex.Replace(WapStr, "", 1) : ((!(value5 == "2")) ? ((!(value5 == "1")) ? regex.Replace(WapStr, value6, 1) : ((!"1".Equals(wmlVo.siteVo.siteVIP)) ? regex.Replace(WapStr, "", 1) : regex.Replace(WapStr, value6, 1))) : ((!"0".Equals(wmlVo.siteVo.siteVIP)) ? regex.Replace(WapStr, "", 1) : regex.Replace(WapStr, value6, 1))));
            //        match = match.NextMatch();
            //    }
            //}
            //if (WapStr.IndexOf("[/noshow]") > 0)
            //{
            //    string text10 = wmlVo.strUrl.Split('?')[0];
            //    Regex regex = new Regex("(\\[noshow=(.[^\\]]*)_(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/noshow\\])");
            //    Match match = regex.Match(WapStr);
            //    while (match.Success)
            //    {
            //        string value4 = match.Groups[2].Value;
            //        string value5 = match.Groups[3].Value;
            //        string value6 = match.Groups[4].Value;
            //        WapStr = ((text10.ToLower().IndexOf(value4.ToLower()) <= -1 && value4.ToLower().IndexOf(text10.ToLower()) <= -1 && ("," + value4 + ",").ToLower().IndexOf("," + wmlVo.classid + ",") <= -1) ? regex.Replace(WapStr, value6, 1) : ((!(value5 == "2")) ? ((!(value5 == "1")) ? regex.Replace(WapStr, "", 1) : ((!"1".Equals(wmlVo.siteVo.siteVIP)) ? regex.Replace(WapStr, value6, 1) : regex.Replace(WapStr, "", 1))) : ((!"0".Equals(wmlVo.siteVo.siteVIP)) ? regex.Replace(WapStr, value6, 1) : regex.Replace(WapStr, "", 1))));
            //        match = match.NextMatch();
            //    }
            //}
            //if (WapStr.IndexOf("[/mtu]") > 0)
            //{
            //    Regex regex = new Regex("(\\[mtu=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/mtu\\])");
            //    try
            //    {
            //        string text11 = IPToALL.IPLocate(wmlVo.parameter1 + "\\visiteCount\\QQWry.Dat", wmlVo.IP);
            //        Match match = regex.Match(WapStr);
            //        while (match.Success)
            //        {
            //            string text2 = match.Groups[2].Value;
            //            WapStr = ((!IsNumeric(text2)) ? regex.Replace(WapStr, "{格式错误}", 1) : ((text2 == "0" && text11.IndexOf("移动") > 0) ? regex.Replace(WapStr, "$3", 1) : ((text2 == "1" && text11.IndexOf("联通") > 0) ? regex.Replace(WapStr, "$3", 1) : ((text2 == "2" && text11.IndexOf("电信") > 0) ? regex.Replace(WapStr, "$3", 1) : ((!(text2 == "3") || text11.IndexOf("铁通") <= 0) ? regex.Replace(WapStr, "", 1) : regex.Replace(WapStr, "$3", 1))))));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            //if (WapStr.IndexOf("[/wap]") > 0)
            //{
            //    Regex regex = new Regex("(\\[wap=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/wap\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapStr);
            //        while (match.Success)
            //        {
            //            string text2 = match.Groups[2].Value;
            //            WapStr = ((!IsNumeric(text2)) ? regex.Replace(WapStr, "{格式错误}", 1) : ((!(text2 == wmlVo.ver)) ? regex.Replace(WapStr, "", 1) : regex.Replace(WapStr, "$3", 1)));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapStr = regex.Replace(WapStr, "{格式错误}");
            //    }
            //}
            return WapStr;
        }

        public static string getExtendFun(string WapHtmlStr, wml wmlVo)
        {
            WapHtmlStr = WapHtmlStr.Replace("///", "<br/>");
            WapHtmlStr = WapHtmlStr.Replace("[br]", "<br/>");
            WapHtmlStr = WapHtmlStr.Replace("[tab]", "&nbsp;");
            WapHtmlStr = WapHtmlStr.Replace("&amp;gt;", "&gt;");
            WapHtmlStr = WapHtmlStr.Replace("&amp;lt;", "&lt;");
            if (wmlVo.ver == "1" || wmlVo.mycss == "")
            {
                WapHtmlStr = WapHtmlStr.Replace("[hr]", "<br/>----------<br/>");
                WapHtmlStr = WapHtmlStr.Replace("[/div][div=", "[/div]<br/>[div=");
                WapHtmlStr = WapHtmlStr.Replace("[/li][li]", "<br/>");
                WapHtmlStr = WapHtmlStr.Replace("[/li]", "");
                WapHtmlStr = WapHtmlStr.Replace("[li]", "");
            }
            else
            {
                WapHtmlStr = WapHtmlStr.Replace("[hr]", "<hr/>");
                WapHtmlStr = WapHtmlStr.Replace("[li]", "<li class=\"ulli\">");
                WapHtmlStr = WapHtmlStr.Replace("[/li]", "</li>");
            }
            WapHtmlStr = WapHtmlStr.Replace("[left]", "</p><p align=\"left\">");
            WapHtmlStr = WapHtmlStr.Replace("[center]", "</p><p align=\"center\">");
            WapHtmlStr = WapHtmlStr.Replace("[right]", "</p><p align=\"right\">");
            LunarDateHocy lunarDateHocy = new LunarDateHocy(DateTime.Now);
            WapHtmlStr = WapHtmlStr.Replace("[weekday]", lunarDateHocy.weekValue);
            WapHtmlStr = WapHtmlStr.Replace("[now]", DateTime.Now.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[year]", DateTime.Now.Year.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[month]", DateTime.Now.Month.ToString());
            WapHtmlStr = WapHtmlStr.Replace("(month)", DateTime.Now.Month.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[day]", DateTime.Now.Day.ToString());
            WapHtmlStr = WapHtmlStr.Replace("(day)", DateTime.Now.Day.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[hour]", DateTime.Now.Hour.ToString());
            WapHtmlStr = WapHtmlStr.Replace("(hour)", DateTime.Now.Hour.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[minute]", DateTime.Now.Minute.ToString());
            WapHtmlStr = WapHtmlStr.Replace("(minute)", DateTime.Now.Minute.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[second]", DateTime.Now.Second.ToString());
            WapHtmlStr = WapHtmlStr.Replace("(second)", DateTime.Now.Second.ToString());
            WapHtmlStr = WapHtmlStr.Replace("[date]", DateTime.Now.ToShortDateString());
            if (WapHtmlStr.IndexOf("[ttcc]") >= 0)
            {
                WapHtmlStr = WapHtmlStr.Replace("[ttcc]", lunarDateHocy.getLunarDate(DateTime.Now));
            }
            WapHtmlStr = WapHtmlStr.Replace("[time]", $"{DateTime.Now:HH:mm:ss}");
            WapHtmlStr = ((wmlVo.userVo == null) ? WapHtmlStr.Replace("[nickname]", wmlVo.nickname) : WapHtmlStr.Replace("[nickname]", GetColorNickName(wmlVo.userVo.idname, wmlVo.userVo.nickname, wmlVo.lang, wmlVo.ver, wmlVo.userVo.endTime)));
            WapHtmlStr = WapHtmlStr.Replace("[fid]", new Random().Next(1000, 10000).ToString());
            WapHtmlStr = WapHtmlStr.Replace("[siteid]", wmlVo.siteid);
            WapHtmlStr = WapHtmlStr.Replace("[classid]", wmlVo.classid);
            WapHtmlStr = WapHtmlStr.Replace("[parentid]", wmlVo.parentid);
            if (wmlVo != null && wmlVo.classVo != null)
            {
                WapHtmlStr = WapHtmlStr.Replace("[classname]", wmlVo.classVo.classname);
            }
            if (wmlVo != null && wmlVo.siteVo != null)
            {
                WapHtmlStr = WapHtmlStr.Replace("[sitename]", wmlVo.siteVo.sitename);
            }
            WapHtmlStr = ((wmlVo.title == null) ? WapHtmlStr.Replace("[title]", "") : WapHtmlStr.Replace("[title]", wmlVo.title));
            WapHtmlStr = ((wmlVo.id == 0L) ? WapHtmlStr.Replace("[id]", "") : WapHtmlStr.Replace("[id]", wmlVo.id.ToString()));
            if (WapHtmlStr.IndexOf("[myaction]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[myaction]", GetMyAction(wmlVo));
            }
            WapHtmlStr = WapHtmlStr.Replace("[domain]", wmlVo.http_start.Replace("http:", "").Replace("/", ""));
            WapHtmlStr = WapHtmlStr.Replace("[userid]", wmlVo.userid);
            WapHtmlStr = WapHtmlStr.Replace("[sid]", wmlVo.sid);
            WapHtmlStr = WapHtmlStr.Replace("[money]", wmlVo.money.ToString());
            WapHtmlStr = ((wmlVo.userVo == null) ? WapHtmlStr.Replace("[rmb]", "0") : WapHtmlStr.Replace("[rmb]", wmlVo.userVo.RMB.ToString("f2")));
            WapHtmlStr = ((wmlVo.userVo == null || wmlVo.userVo.idname == null) ? WapHtmlStr.Replace("[myvip]", "\u3000") : WapHtmlStr.Replace("[myvip]", GetMyID(wmlVo.userVo.idname, wmlVo.lang, wmlVo.userVo.endTime)));
            WapHtmlStr = WapHtmlStr.Replace("[ver]", wmlVo.ver);
            WapHtmlStr = WapHtmlStr.Replace("[sid1]", wmlVo.sid1);
            WapHtmlStr = WapHtmlStr.Replace("[sid2]", wmlVo.sid2);
            WapHtmlStr = WapHtmlStr.Replace("[cs]", wmlVo.cs);
            WapHtmlStr = WapHtmlStr.Replace("[lang]", wmlVo.lang);
            WapHtmlStr = WapHtmlStr.Replace("[myua]", wmlVo.myua);
            WapHtmlStr = WapHtmlStr.Replace("[width]", wmlVo.width);
            WapHtmlStr = WapHtmlStr.Replace("[ip]", wmlVo.IP);
            if (WapHtmlStr.IndexOf("[ua]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[ua]", GetUAUBB(wmlVo.myua, wmlVo.ver, wmlVo.lang, wmlVo.sid, 0));
            }
            if (WapHtmlStr.IndexOf("[ua1]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[ua1]", GetUAUBB(wmlVo.myua, wmlVo.ver, wmlVo.lang, wmlVo.sid, 1));
            }
            //if (WapHtmlStr.IndexOf("[vs]") > -1)
            //{
            //    wmlVo.showlink = "1";
            //    WapHtmlStr = WapHtmlStr.Replace("[vs]", GetVS_True(wmlVo));
            //}
            //if (WapHtmlStr.IndexOf("[vs2]") > -1)
            //{
            //    wmlVo.showlink = "2";
            //    WapHtmlStr = WapHtmlStr.Replace("[vs2]", GetVS_True(wmlVo));
            //}
            //if (WapHtmlStr.IndexOf("[vs3]") > -1)
            //{
            //    wmlVo.showlink = "3";
            //    WapHtmlStr = WapHtmlStr.Replace("[vs3]", GetVS_True(wmlVo));
            //}
            //if (WapHtmlStr.IndexOf("[nv]") > -1)
            //{
            //    WapHtmlStr = WapHtmlStr.Replace("[nv]", GetNavigation(wmlVo));
            //}
            if (WapHtmlStr.IndexOf("[hello]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[hello]", GetHello());
            }
            //if (WapHtmlStr.IndexOf("[shopcard]") > -1)
            //{
            //    WapHtmlStr = ((wmlVo.siteVo == null || wmlVo.siteVo.myShopCardList == null) ? WapHtmlStr.Replace("[shopcard]", "<span id=\"shopcard\" class=\"shopcard\">0</span>") : WapHtmlStr.Replace("[shopcard]", "<span id=\"shopcard\" class=\"shopcard\">" + wmlVo.siteVo.myShopCardList + "</span>"));
            //}
            if (WapHtmlStr.IndexOf("[vtoday]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[vtoday]", GetCount("vtoday", wmlVo.siteUserName, wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[vyestaday]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[vyestaday]", GetCount("vyestaday", wmlVo.siteUserName, wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[vtotal]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[vtotal]", GetCount("vtotal", wmlVo.siteUserName, wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[vweek]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[vweek]", GetCount("vweek", wmlVo.siteUserName, wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[vmonth]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[vmonth]", GetCount("vmonth", wmlVo.siteUserName, wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[online]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[online]", GetOnlineCount("0", wmlVo.siteid));
            }
            if (WapHtmlStr.IndexOf("[valltotal]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[valltotal]", GetAllCount());
            }
            if (WapHtmlStr.IndexOf("[message]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[message]", GetMessage(wmlVo.ver, wmlVo.userid, wmlVo.siteid, wmlVo.http_start, wmlVo.sid, wmlVo.classid, "0"));
            }
            if (WapHtmlStr.IndexOf("[automsg]") > -1)
            {
                WapHtmlStr = WapHtmlStr.Replace("[automsg]", GetAutoMessage(wmlVo.ver, wmlVo.userid, wmlVo.siteid, wmlVo.http_start, wmlVo.sid, wmlVo.classid));
            }
            //if (WapHtmlStr.IndexOf("[/ui]") > 0)
            //{
            //    Regex regex = new Regex("(\\[ui\\])(.[^\\[]*)(\\[\\/ui\\])");
            //    try
            //    {
            //        Match match = regex.Match(WapHtmlStr);
            //        while (match.Success)
            //        {
            //            string value = match.Groups[2].Value;
            //            WapHtmlStr = ((!IsNumeric(value)) ? regex.Replace(WapHtmlStr, "{格式错误}", 1) : regex.Replace(WapHtmlStr, GetUserInfoFromUBB(value, wmlVo), 1));
            //            match = match.NextMatch();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        WapHtmlStr = regex.Replace(WapHtmlStr, "{格式错误}");
            //    }
            //}
            return WapHtmlStr;
        }

        public static string GetUserInfoFromUBB(string string_0, wml wmlVo)
        {
            if (string_0 == "0")
            {
                return wmlVo.userid.ToString();
            }
            if (string_0 == "1")
            {
                if (wmlVo.userVo != null)
                {
                    return wmlVo.userVo.nickname;
                }
                return "游客888";
            }
            if (wmlVo.userVo == null)
            {
                return "";
            }
            switch (string_0)
            {
                case "2":
                    if (wmlVo.userVo.sex == 0L)
                    {
                        return "女";
                    }
                    return "男";

                case "3":
                    return wmlVo.userVo.age.ToString();

                case "4":
                    if (wmlVo.userVo.tizhong != null)
                    {
                        return wmlVo.userVo.tizhong.ToString();
                    }
                    break;
            }
            if (string_0 == "5" && wmlVo.userVo.xingzuo != null)
            {
                return wmlVo.userVo.xingzuo.ToString();
            }
            if (string_0 == "6" && wmlVo.userVo.aihao != null)
            {
                string[] array = (wmlVo.userVo.aihao + "_").Split('_');
                return array[0];
            }
            if (string_0 == "7" && wmlVo.userVo.aihao != null)
            {
                string[] array = (wmlVo.userVo.aihao + "_").Split('_');
                return array[1];
            }
            if (string_0 == "8" && wmlVo.userVo.fenfuo != null)
            {
                return wmlVo.userVo.fenfuo.ToString();
            }
            if (string_0 == "9" && wmlVo.userVo.zhiye != null)
            {
                return wmlVo.userVo.zhiye.ToString();
            }
            if (string_0 == "10" && wmlVo.userVo.city != null)
            {
                return wmlVo.userVo.city.ToString();
            }
            if (string_0 == "11" && wmlVo.userVo.mobile != null)
            {
                return wmlVo.userVo.mobile.ToString();
            }
            if (string_0 == "12" && wmlVo.userVo.email != null)
            {
                return wmlVo.userVo.email.ToString();
            }
            if (string_0 == "13")
            {
                return wmlVo.userVo.money.ToString();
            }
            if (string_0 == "14" && wmlVo.userVo.moneyname != null)
            {
                return GetMedal(wmlVo.userVo.userid.ToString(), wmlVo.userVo.moneyname, GetSiteDefault(wmlVo.siteVo.Version, 47), wmlVo);
            }
            if (string_0 == "15")
            {
                _ = wmlVo.userVo.RegTime;
                if (0 == 0)
                {
                    if (wmlVo.userVo.RegTime == DateTime.MinValue)
                    {
                        return "";
                    }
                    return $"{wmlVo.userVo.RegTime:yyyy-MM-dd hh:mm:ss}";
                }
            }
            if (string_0 == "16" && wmlVo.userVo.LastLoginIP != null)
            {
                return wmlVo.userVo.LastLoginIP.ToString();
            }
            if (string_0 == "17")
            {
                _ = wmlVo.userVo.LastLoginTime;
                if (0 == 0)
                {
                    if (wmlVo.userVo.LastLoginTime == DateTime.MinValue)
                    {
                        return "";
                    }
                    return $"{wmlVo.userVo.LastLoginTime:yyyy-MM-dd hh:mm:ss}";
                }
            }
            if (string_0 == "18" && wmlVo.userVo.headimg != null)
            {
                if (wmlVo.userVo.headimg.ToLower().StartsWith("http"))
                {
                    return "<img src=\"" + wmlVo.userVo.headimg + "\" alt=\"头像\"/><br/>";
                }
                if (wmlVo.userVo.headimg.IndexOf("/") >= 0)
                {
                    return "<img src=\"" + wmlVo.http_start + wmlVo.userVo.headimg + "\" alt=\"头像\"/><br/>";
                }
                return "<img src=\"" + wmlVo.http_start + "bbs/head/" + wmlVo.userVo.headimg + "\" alt=\"头像\"/><br/>";
            }
            if (string_0 == "19" && wmlVo.userVo.remark != null)
            {
                return wmlVo.userVo.remark.ToString();
            }
            switch (string_0)
            {
                case "20":
                    return wmlVo.userVo.expr.ToString();

                case "21":
                    return wmlVo.userVo.RMB.ToString("f2");

                case "22":
                    return wmlVo.userVo.myBankMoney.ToString();

                case "23":
                    {
                        string siteDefault = GetSiteDefault(wmlVo.siteVo.Version, 27);
                        return GetLevl(wmlVo.siteVo.lvlNumer, wmlVo.userVo.expr, wmlVo.userVo.money, siteDefault);
                    }
                case "24":
                    {
                        string siteDefault = GetSiteDefault(wmlVo.siteVo.Version, 27);
                        return GetHandle(wmlVo.siteVo.lvlNumer, wmlVo.userVo.expr, wmlVo.userVo.money, siteDefault);
                    }
                case "25":
                    if (wmlVo.userVo.isonline != null)
                    {
                        if (wmlVo.userVo.isonline == "1")
                        {
                            return "在线";
                        }
                        return "离线";
                    }
                    break;
            }
            switch (string_0)
            {
                case "26":
                    return wmlVo.userVo.bbsCount.ToString();

                case "27":
                    return wmlVo.userVo.bbsReCount.ToString();

                case "28":
                    return wmlVo.userVo.TJCount.ToString();

                case "29":
                    return GetOLtimePic(wmlVo.http_start, wmlVo.siteVo.lvlTimeImg, wmlVo.userVo.LoginTimes);

                case "30":
                    return wmlVo.userVo.zoneCount.ToString();

                default:
                    return "";
            }
        }

        public static string GetVS_True(wml wmlVo)
        {
            if (wmlVo.showlink == "")
            {
                wmlVo.showlink = "1";
            }
            if (wmlVo.showlink == "0")
            {
                if ("|00|01|03|".IndexOf(wmlVo.managerlvl) > 0)
                {
                    return "<div class=\"btBox\"><div class=\"bt1\"><a href=\"" + wmlVo.http_start + "admin/loginwap.aspx?siteid=" + wmlVo.siteid + "\">【网站管理后台】</a></div></div>";
                }
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            string strUrl = wmlVo.strUrl;
            strUrl = strUrl.Replace("&", "%26");
            strUrl = strUrl.Replace("=", "%3D");
            strUrl = strUrl.Replace("?", "%3F");
            if (wmlVo.ver == "1111")
            {
                return "<br/><a href=\"javascript:alert('WAP上浏览!');\">普通-彩版-电脑-选择<br/>简体-繁体-英文-选择</a>";
            }
            stringBuilder.Append("<div class=\"btBox\">");
            if (wmlVo.showlink == "1" || wmlVo.showlink == "2" || wmlVo.ver == "0")
            {
                stringBuilder.Append("<div class=\"bt4\">");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-1-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">普通</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-2-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">彩版</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-3-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">电脑</a> ");
                if (ISAPI_Rewrite3_Open == "1")
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/skin-" + wmlVo.siteid + "-" + wmlVo.classid + ".html?backurl=" + strUrl + "\">选择</a>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/skin.aspx?siteid=" + wmlVo.siteid + "&amp;backurl=" + strUrl + "\">选择</a>");
                }
                stringBuilder.Append("</div>");
            }
            if (wmlVo.showlink == "1" || wmlVo.showlink == "3" || wmlVo.ver == "0")
            {
                stringBuilder.Append("<div class=\"bt4\">");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-" + wmlVo.ver + "-" + wmlVo.cs + "-0-" + wmlVo.myua + "-" + wmlVo.width + "\">简体</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-" + wmlVo.ver + "-" + wmlVo.cs + "-1-" + wmlVo.myua + "-" + wmlVo.width + "\">繁体</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-" + wmlVo.ver + "-" + wmlVo.cs + "-2-" + wmlVo.myua + "-" + wmlVo.width + "\">英文</a> ");
                if (ISAPI_Rewrite3_Open == "1")
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/lang-" + wmlVo.siteid + "-" + wmlVo.classid + ".html?backurl=" + strUrl + "\">选择</a>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/lang.aspx?siteid=" + wmlVo.siteid + "&amp;backurl=" + strUrl + "\">选择</a>");
                }
            }
            if (wmlVo.showlink == "4")
            {
                stringBuilder.Append("<div class=\"bt4\">");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-1-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">1</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-2-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">2</a> ");
                stringBuilder.Append("<a href=\"" + wmlVo.http_start + wmlVo.strUrl + "&amp;sid=-3-" + wmlVo.cs + "-" + wmlVo.lang + "-" + wmlVo.myua + "-" + wmlVo.width + "\">3</a> ");
                if (ISAPI_Rewrite3_Open == "1")
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/skin-" + wmlVo.siteid + "-" + wmlVo.classid + ".html?backurl=" + strUrl + "\">0</a>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"" + wmlVo.http_start + "wapstyle/skin.aspx?siteid=" + wmlVo.siteid + "&amp;backurl=" + strUrl + "\">0</a>");
                }
                stringBuilder.Append("</div>");
            }
            stringBuilder.Append("</div></div>");
            if (wmlVo.ver == "1")
            {
                stringBuilder.Replace("<div class=\"bt4\">", "").Replace("<div class=\"btBox\">", "").Replace("</div>", "");
                stringBuilder.Insert(0, "<br/>");
            }
            return stringBuilder.ToString();
        }

        public static string GetVS(wml wmlVo)
        {
            return "";
        }

        public static string GetBus(string string_0, string strHttp_Start, string strSiteId, string strClassId, string strSid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string_0 == "0" || string_0 == "1")
            {
                stringBuilder.Append("按城市:<input type=\"text\" size=\"4\" name=\"city\" value=\"\"/> <anchor><go href=\"" + strHttp_Start + "bus/book_list.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                stringBuilder.Append("<postfield name=\"action\" value=\"search\"/>");
                stringBuilder.Append("<postfield name=\"stype\" value=\"city\"/>");
                stringBuilder.Append("<postfield name=\"siteid\" value=\"" + strSiteId + "\"/>");
                stringBuilder.Append("<postfield name=\"classid\" value=\"" + strClassId + "\"/>");
                stringBuilder.Append("<postfield name=\"city\" value=\"$(city)\"/>");
                stringBuilder.Append("<postfield name=\"sid\" value=\"" + strSid + "\"/>");
                stringBuilder.Append("</go>查询</anchor><br/>");
                stringBuilder.Append("按线路:<input type=\"text\" size=\"4\" name=\"line\" value=\"\"/> <anchor><go href=\"" + strHttp_Start + "bus/book_list.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                stringBuilder.Append("<postfield name=\"action\" value=\"search\"/>");
                stringBuilder.Append("<postfield name=\"stype\" value=\"line\"/>");
                stringBuilder.Append("<postfield name=\"siteid\" value=\"" + strSiteId + "\"/>");
                stringBuilder.Append("<postfield name=\"classid\" value=\"" + strClassId + "\"/>");
                stringBuilder.Append("<postfield name=\"city\" value=\"$(city)\"/>");
                stringBuilder.Append("<postfield name=\"line\" value=\"$(line)\"/>");
                stringBuilder.Append("<postfield name=\"sid\" value=\"" + strSid + "\"/>");
                stringBuilder.Append("</go>查询</anchor><br/>");
                stringBuilder.Append("按站点:<input type=\"text\" size=\"4\" name=\"station\" value=\"\"/> <anchor><go href=\"" + strHttp_Start + "bus/book_list.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                stringBuilder.Append("<postfield name=\"action\" value=\"search\"/>");
                stringBuilder.Append("<postfield name=\"stype\" value=\"station\"/>");
                stringBuilder.Append("<postfield name=\"siteid\" value=\"" + strSiteId + "\"/>");
                stringBuilder.Append("<postfield name=\"classid\" value=\"" + strClassId + "\"/>");
                stringBuilder.Append("<postfield name=\"city\" value=\"$(city)\"/>");
                stringBuilder.Append("<postfield name=\"station\" value=\"$(station)\"/>");
                stringBuilder.Append("<postfield name=\"sid\" value=\"" + strSid + "\"/>");
                stringBuilder.Append("</go>查询</anchor><br/>");
                stringBuilder.Append("从<input type=\"text\" size=\"4\" name=\"starts\" value=\"\"/>到<input type=\"text\" size=\"4\" name=\"ends\" value=\"\"/>站 <anchor><go href=\"" + strHttp_Start + "bus/book_list.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                stringBuilder.Append("<postfield name=\"action\" value=\"search\"/>");
                stringBuilder.Append("<postfield name=\"stype\" value=\"start2end\"/>");
                stringBuilder.Append("<postfield name=\"siteid\" value=\"" + strSiteId + "\"/>");
                stringBuilder.Append("<postfield name=\"classid\" value=\"" + strClassId + "\"/>");
                stringBuilder.Append("<postfield name=\"city\" value=\"$(city)\"/>");
                stringBuilder.Append("<postfield name=\"starts\" value=\"$(starts)\"/>");
                stringBuilder.Append("<postfield name=\"ends\" value=\"$(ends)\"/>");
                stringBuilder.Append("<postfield name=\"sid\" value=\"" + strSid + "\"/>");
                stringBuilder.Append("</go>查询</anchor>");
                return stringBuilder.ToString();
            }
            return "*公交查询*";
        }

        public static string GetAutoMessage(string string_0, string strUserId, string strSiteID, string strHttp_Start, string strSid, string classid)
        {
            string text = "";
            if (strUserId == "" || strUserId == "0")
            {
                text = "0";
            }
            else
            {
                string commandText = "select count(id)  from wap_message where isnew=1 and  touserid=" + strUserId + " and siteid=" + strSiteID;
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    text = dataSet.Tables[0].Rows[0][0].ToString();
                }
            }
            if (text != "0")
            {
                if (string_0 == "0")
                {
                    return "您有<a href=\"javascript:alert('UBB方法：链接至" + strHttp_Start + "bbs/messagelist.aspx?siteid=" + strSiteID + "&sid=" + strSid + "\n\n请用IE或手机访问测试！');\">" + text + "</a>条新信息！<br/>";
                }
                return "您有<a href=\"" + strHttp_Start + "bbs/messagelist.aspx?siteid=" + strSiteID + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + strSiteID + "&amp;classid=" + classid) + "\">" + text + "</a>条新信息！<br/>";
            }
            return " ";
        }

        public static string GetMessage(string string_0, string strUserId, string strSiteID, string strHttp_Start, string strSid, string classid, string showtype)
        {
            string text = "0";
            if (strUserId == "" || strUserId == "0")
            {
                text = "0";
            }
            else
            {
                string commandText = "select count(id)  from wap_message where isnew=1 and  touserid=" + strUserId + " and siteid=" + strSiteID;
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    text = dataSet.Tables[0].Rows[0][0].ToString();
                }
            }
            if (showtype == "0")
            {
                if (string_0 == "0")
                {
                    return "<a href=\"javascript:alert('UBB方法：链接至" + strHttp_Start + "bbs/messagelist.aspx?siteid=" + strSiteID + "&sid=" + strSid + "\n\n请用IE或手机访问测试！');\">" + text + "</a>";
                }
                return "<a href=\"" + strHttp_Start + "bbs/messagelist.aspx?siteid=" + strSiteID + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + strSiteID + "&amp;classid=" + classid) + "\">" + text + "</a>";
            }
            return text;
        }

        public static string GetAllCount()
        {
            string commandText = "select  sum(vtotal) as mm  from vcount";
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0]["mm"].ToString();
            }
            return "0";
        }

        public static string GetFriendCount(string siteid, string userid, string friendtype)
        {
            if (!IsNumeric(friendtype))
            {
                return "{格式错误}";
            }
            string result = "0";
            string text = " siteid= " + siteid + " and  userid=" + userid + " and friendtype=" + friendtype;
            string commandText = "select  count(id) as mm  from wap_friends where " + text;
            if (friendtype == "4")
            {
                text = " siteid= " + siteid + " and  frienduserid=" + userid;
                commandText = "select  count(id) as mm  from wap_friends_view where " + text;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                result = dataSet.Tables[0].Rows[0]["mm"].ToString();
            }
            return result;
        }

        public static string GetOnlineCount(string strType, string strSiteID)
        {
            string text = "";
            DataSet dataSet = DbHelperSQL.ExecuteDataset(commandText: (!(strType != "") || !(strType != "0")) ? ("select  count(fid)  from fcount where  fuserid='" + strSiteID + "' ") : ("select  count(fid)  from fcount where  fuserid='" + strSiteID + "' and classid=" + strType), connectionString: _ConnStr, commandType: CommandType.Text);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0][0].ToString();
            }
            return "0";
        }

        public static string GetHello()
        {
            if (DateTime.Now.Hour >= 4 && DateTime.Now.Hour < 8)
            {
                return "早上好！";
            }
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 12)
            {
                return "上午好！";
            }
            if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 14)
            {
                return "中午好！";
            }
            if (DateTime.Now.Hour >= 14 && DateTime.Now.Hour < 18)
            {
                return "下午好！";
            }
            if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 23)
            {
                return "晚上好！";
            }
            return "午夜好！";
        }

        public static bool isExistBinDomain(string siteid, string domain)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from domainname where domain='" + domain + "' and siteid<>" + long.Parse(siteid));
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isHasReply(string siteid, string userid, string bbsid)
        {
            if (userid == "0")
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from [wap_bbsre]  where devid='" + siteid + "' and userid=" + long.Parse(userid) + " and bookid=" + long.Parse(bbsid));
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isHasReplyToday(string siteid, string userid, string bbsid)
        {
            if (userid == "0")
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from [wap_bbsre]  where (DATEDIFF(dd, redate, GETDATE()) < 1) and devid='" + siteid + "' and userid=" + long.Parse(userid) + " and bookid=" + long.Parse(bbsid));
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isExistString(string searchString, string string_0)
        {
            if (string_0.IndexOf("|") > 0)
            {
                string[] array = string_0.Split('|');
                for (int i = 0; i < 2; i++)
                {
                    if (searchString.IndexOf(array[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            else if (searchString.IndexOf(string_0) >= 0)
            {
                return true;
            }
            return false;
        }

        public static string GetCount(string CountType, string strSiteUserName, string siteid)
        {
            string result = "0";
            if (CountType == "vtotal")
            {
                string commandText = "select top 1 vtoday,vyestaday,vtotal,vmonth,vweek from vcount where vuser='" + strSiteUserName + "'";
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    switch (CountType)
                    {
                        case "vweek":
                            result = dataSet.Tables[0].Rows[0]["vweek"].ToString();
                            break;

                        case "vmonth":
                            result = dataSet.Tables[0].Rows[0]["vmonth"].ToString();
                            break;

                        case "vtotal":
                            result = dataSet.Tables[0].Rows[0]["vtotal"].ToString();
                            break;

                        case "vyestaday":
                            result = dataSet.Tables[0].Rows[0]["vyestaday"].ToString();
                            break;

                        case "vtoday":
                            result = dataSet.Tables[0].Rows[0]["vtoday"].ToString();
                            break;
                    }
                }
            }
            else
            {
                string commandText = "";
                switch (CountType)
                {
                    case "vtoday":
                        commandText = "select pv as count from wap_vcount_everyDate  where DateDiff(dd,everyDate,getdate())=0 and siteid=" + siteid + " and types=0";
                        break;

                    case "vyestaday":
                        commandText = "select pv as count from wap_vcount_everyDate  where DateDiff(dd,everyDate,getdate())=1 and siteid=" + siteid + " and types=0";
                        break;

                    case "vmonth":
                        commandText = "select pv as count from wap_vcount_everyDate  where DateDiff(month,everyDate,getdate())=0 and siteid=" + siteid + " and types=0";
                        break;

                    case "vweek":
                        commandText = "select pv as count from wap_vcount_everyDate  where DateDiff(week,everyDate,getdate())=0 and siteid=" + siteid + " and types=0";
                        break;
                }
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    result = dataSet.Tables[0].Rows[0]["count"].ToString();
                }
            }
            return result;
        }

        public static string DateToString(DateTime datatime, string lang, int longOrShort)
        {
            DateTime now = DateTime.Now;
            return DateToString(long.Parse((now - datatime).TotalSeconds.ToString().Split('.')[0]), lang, longOrShort);
        }

        public static string ShowTime(DateTime dateTime_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int days = (DateTime.Parse(DateTime.Now.ToString().Split(' ')[0] + " 00:00:01") - DateTime.Parse(dateTime_0.ToString().Split(' ')[0] + " 00:00:01")).Days;
            switch (days)
            {
                case 0:
                    stringBuilder.Append("今天");
                    break;

                case 1:
                    stringBuilder.Append("昨天");
                    break;

                case 2:
                    stringBuilder.Append("前天");
                    break;
            }
            if (days < 2)
            {
                stringBuilder.Append(" ");
                if (dateTime_0.Hour >= 4 && dateTime_0.Hour < 8)
                {
                    stringBuilder.Append("早上");
                }
                else if (dateTime_0.Hour >= 8 && dateTime_0.Hour < 12)
                {
                    stringBuilder.Append("上午");
                }
                else if (dateTime_0.Hour >= 12 && dateTime_0.Hour < 14)
                {
                    stringBuilder.Append("中午");
                }
                else if (dateTime_0.Hour >= 14 && dateTime_0.Hour < 18)
                {
                    stringBuilder.Append("下午");
                }
                else if (dateTime_0.Hour >= 18 && dateTime_0.Hour < 23)
                {
                    stringBuilder.Append("晚上");
                }
                else
                {
                    stringBuilder.Append("午夜");
                }
            }
            else
            {
                stringBuilder.Append(dateTime_0.Month);
                stringBuilder.Append("月");
                stringBuilder.Append(dateTime_0.Day.ToString());
                stringBuilder.Append("日");
            }
            return stringBuilder.ToString();
        }

        public static string DateToString(long TS, string lang, int longOrShort)
        {
            string text = "";
            long num = TS;
            if (num > 60L)
            {
                long num2 = num / 60L;
                num -= 60L * num2;
                if (num2 > 60L)
                {
                    long num3 = num2 / 60L;
                    num2 -= num3 * 60L;
                    if (num3 > 24L)
                    {
                        long num4 = num3 / 24L;
                        num3 -= num4 * 24L;
                        if (num4 > 30L)
                        {
                            long num5 = num4 / 30L;
                            num4 -= num5 * 30L;
                            if (num5 > 12L)
                            {
                                long num6 = num5 / 12L;
                                num5 -= num6 * 12L;
                                if (longOrShort == 0)
                                {
                                    return num6 + "年" + num5 + "个月" + num4 + "天" + num3 + "小时" + num2 + "分" + num + "秒";
                                }
                                return num6 + "年";
                            }
                            if (longOrShort == 0)
                            {
                                return num5 + "个月" + num4 + "天" + num3 + "小时" + num2 + "分" + num + "秒";
                            }
                            return num5 + "个月";
                        }
                        if (longOrShort == 0)
                        {
                            return num4 + "天" + num3 + "小时" + num2 + "分" + num + "秒";
                        }
                        if (num4 < 2L)
                        {
                            return "昨天";
                        }
                        if (num4 < 3L)
                        {
                            return "前天";
                        }
                        if (num4 > 15L)
                        {
                            return "半月";
                        }
                        if (num4 > 7L)
                        {
                            return "一周";
                        }
                        return num4 + "天";
                    }
                    if (longOrShort == 0)
                    {
                        return num3 + "小时" + num2 + "分" + num + "秒";
                    }
                    if (num3 > 12L)
                    {
                        return "半天";
                    }
                    return num3 + "小时";
                }
                if (longOrShort == 0)
                {
                    return num2 + "分" + num + "秒";
                }
                if (num2 > 30L)
                {
                    return "半小时";
                }
                return num2 + "分钟";
            }
            if (num < 0L)
            {
                num = 1L;
            }
            return num + "秒";
        }

        public static string GetMedal(string moneyname, string http_start)
        {
            string text = "";
            if (!string.IsNullOrEmpty(moneyname))
            {
                string[] array = moneyname.Split('|');
                string[] array2 = array;
                foreach (string text2 in array2)
                {
                    if (text2 != "")
                    {
                        text = ((text2.IndexOf("XinZhang") < 0) ? ((!text2.StartsWith("/") && !text2.StartsWith("http://")) ? (text + "<img src=\"" + http_start + "bbs/medal/" + text2 + "\" alt=\".\"/>") : (text + "<img src=\"" + text2 + "\" alt=\".\"/>")) : (text + "<img src=\"" + http_start + text2 + "\" alt=\".\"/>"));
                    }
                }
            }
            return text;
        }

        public static string GetMedal(string touserid, string tomoneyname, string string_0, wml wmlVo)
        {
            if (!IsNumeric(string_0) || string_0 == "0")
            {
                string_0 = "5";
            }
            string text = "";
            if (!string.IsNullOrEmpty(tomoneyname))
            {
                string[] array = tomoneyname.Split('|');
                int num = 0;
                string[] array2 = array;
                foreach (string text2 in array2)
                {
                    if (text2 != "")
                    {
                        text = ((text2.IndexOf("XinZhang") >= 0) ? (text + "<img src=\"" + wmlVo.http_start + text2 + "\" alt=\".\"/>") : ((!text2.StartsWith("/") && !text2.StartsWith("http://")) ? (text + "<img src=\"" + wmlVo.http_start + "bbs/medal/" + text2 + "\" alt=\".\"/>") : (text + "<img src=\"" + text2 + "\" alt=\".\"/>")));
                        num++;
                        if (num > int.Parse(string_0))
                        {
                            text = text + "<a href=\"" + wmlVo.http_start + "bbs/viewXunZhangALL.aspx?siteid=" + wmlVo.siteid + "&amp;touserid=" + touserid + "\">更多...</a>";
                            break;
                        }
                    }
                }
            }
            return text;
        }

        public static string GetLevl(string lvlNumer, long expr, long money, string type)
        {
            long num = 0L;
            num = ((!"1".Equals(type)) ? expr : money);
            try
            {
                if (string.IsNullOrEmpty(lvlNumer))
                {
                    return "";
                }
                string[] array = lvlNumer.Split('_')[0].Split('|');
                string[] array2 = lvlNumer.Split('_')[2].Split('|');
                int num2 = array.Length - 1;
                while (num2 >= 0)
                {
                    if (num <= long.Parse(array[num2]))
                    {
                        num2--;
                        continue;
                    }
                    if (array2[num2].IndexOf("[/img]") > 0)
                    {
                        Regex regex = new Regex("(\\[img\\])(.[^\\[]*)(\\[\\/img\\])");
                        return regex.Replace(array2[num2], "<img src=\"$2\" alt=\".\"/>");
                    }
                    return array2[num2];
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string GetHandle(string lvlNumer, long expr, long money, string type)
        {
            long num = 0L;
            num = ((!"1".Equals(type)) ? expr : money);
            try
            {
                if (string.IsNullOrEmpty(lvlNumer))
                {
                    return "";
                }
                string[] array = lvlNumer.Split('_')[0].Split('|');
                string[] array2 = lvlNumer.Split('_')[1].Split('|');
                int num2 = array.Length - 1;
                while (num2 >= 0)
                {
                    if (num <= long.Parse(array[num2]))
                    {
                        num2--;
                        continue;
                    }
                    if (array2[num2].IndexOf("[/img]") > 0)
                    {
                        Regex regex = new Regex("(\\[img\\])(.[^\\[]*)(\\[\\/img\\])");
                        return regex.Replace(array2[num2], "<img src=\"$2\" alt=\".\"/>");
                    }
                    return array2[num2];
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string GetOLtimePic(string http_start, string lvlTimeImg, long mytimes)
        {
            try
            {
                if (string.IsNullOrEmpty(lvlTimeImg))
                {
                    return "";
                }
                string[] array = lvlTimeImg.Split('_')[0].Split('|');
                string[] array2 = lvlTimeImg.Split('_')[1].Split('|');
                int num = array.Length - 1;
                while (num >= 0)
                {
                    if (mytimes <= Convert.ToInt32(array[num]) * 86400)
                    {
                        num--;
                        continue;
                    }
                    return "<img src=\"" + http_start + "bbs/medal/" + array2[num] + "\" alt=\".\"/>";
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string GetXunZhangAutoPic(string ruleString1, string ruleString2, long MoneyOrExpr)
        {
            try
            {
                string[] array = ruleString1.Split(',');
                string[] array2 = ruleString2.Split(',');
                int num = array.Length - 1;
                while (num >= 0)
                {
                    if (MoneyOrExpr <= Convert.ToInt32(array[num]))
                    {
                        num--;
                        continue;
                    }
                    return array2[num];
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static long CheckCurrpage(long total, long pagesize, long currpage)
        {
            long num = 0L;
            num = ((total % pagesize != 0L) ? (total / pagesize + 1L) : (total / pagesize));
            if (currpage > num)
            {
                currpage = num;
            }
            else if (currpage < 1L)
            {
                currpage = 1L;
            }
            return currpage;
        }

        public static string GetPageLinkShowTOP(string string_0, string lang, long total, long pagesize, long currpage, string strlink)
        {
            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            num = ((total % pagesize != 0L) ? (total / pagesize + 1L) : (total / pagesize));
            if (currpage > num)
            {
                currpage = num;
            }
            else if (currpage < 1L)
            {
                currpage = 1L;
            }
            if (string_0 != "1")
            {
                stringBuilder.Append("<div class=\"btBox\"><div class=\"bt2\">");
            }
            stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage + 1L) + "\">下一页</a> ");
            stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage - 1L) + "\">上一页</a> ");
            if (string_0 != "1")
            {
                stringBuilder.Append("</div></div>");
            }
            if (string_0 == "1" && stringBuilder.ToString() != "")
            {
                stringBuilder.Append("<br/>");
            }
            if (num > 1L)
            {
                return stringBuilder.ToString();
            }
            return "";
        }

        public static string GetPageLink(string string_0, string lang, long total, long pagesize, long currpage, string strlink, string isShow_old_link)
        {
            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            num = ((total % pagesize != 0L) ? (total / pagesize + 1L) : (total / pagesize));
            if (currpage < 1L)
            {
                currpage = 1L;
            }
            if (num < 2L)
            {
                return "";
            }
            stringBuilder.Append("<div id=\"KL_show_next_list\" style=\"display:none\"></div>");
            stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\"><a id=\"KL_loadmore\" href=\"javascript:;\" onclick=\"KL_show_next('" + num + "','" + pagesize + "','" + currpage + "','" + strlink + "','page')\"><span id=\"KL_show_loadimg\" ></span> <span id=\"KL_show_tip\">加载更多(" + currpage + "/" + num + ")</span></a></div></div>");
            //stringBuilder.Append("<script src=\"/NetCSS/KL_common.js\" language=\"javascript\"></script>");
            if (isShow_old_link != "1" && strlink.IndexOf("classid=0") < 0)
            {
                return stringBuilder.ToString();
            }
            if (string_0 != "1")
            {
                stringBuilder.Append("<div class=\"btBox\"><div class=\"bt2\">");
            }
            stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage + 1L) + "\">下一页</a> ");
            stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage - 1L) + "\">上一页</a> ");
            if (string_0 != "1")
            {
                stringBuilder.Append("</div></div>");
            }
            if (num > 0L)
            {
                stringBuilder.Append("<div class=\"showpage\">第 " + currpage + "/" + num + " 页，共 " + total + " 条 ");
                if (string_0 == "1")
                {
                    stringBuilder.Append("<input title=\"输入页码\" name=\"page\" format=\"*N\" size=\"3\" maxlength=\"8\" value=\"" + currpage + "\"/><anchor><go href=\"" + strlink + "\" method=\"get\" accept-charset=\"utf-8\"><postfield name=\"page\" value=\"$(page)\"/></go>翻页</anchor><br/>");
                }
                else
                {
                    strlink = strlink.Replace("&amp;", "&");
                    string text = strlink.Split('?')[0];
                    string text2 = strlink.Split('?')[1];
                    string[] array = text2.Split('&');
                    stringBuilder.Append("<form name=\"go\" method=\"get\" action=\"" + text + "\"><input type=\"text\" name=\"page\" value=\"" + currpage + "\" style=\"width:30px\" data-role=\"none\"/>");
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split('=');
                        stringBuilder.Append(" <input type=\"hidden\" name=\"" + array2[0] + "\" value=\"" + array2[1] + "\" /> ");
                    }
                    stringBuilder.Append("<input type=\"submit\" name=\"go\" class=\"urlbtn\" value=\"翻页\" data-role=\"none\"/></form>");
                }
                stringBuilder.Append("</div>");
            }
            return stringBuilder.ToString();
        }

        public static string GetPageLink(string string_0, string lang, long total, long pagesize, long currpage, string strlink)
        {
            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            num = ((total % pagesize != 0L) ? (total / pagesize + 1L) : (total / pagesize));
            if (currpage > num)
            {
                currpage = num;
            }
            else if (currpage < 1L)
            {
                currpage = 1L;
            }
            if (num > 0L)
            {
                if (string_0 != "1")
                {
                    stringBuilder.Append("<div class=\"btBox\"><div class=\"bt2\">");
                }
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage + 1L) + "\">下一页</a> ");
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage - 1L) + "\">上一页</a> ");
                if (string_0 != "1")
                {
                    stringBuilder.Append("</div></div>");
                }
                stringBuilder.Append("<div class=\"showpage\">第 " + currpage + "/" + num + " 页，共 " + total + " 条 ");
                if (string_0 == "1")
                {
                    stringBuilder.Append("<input title=\"输入页码\" name=\"page\" format=\"*N\" size=\"3\" maxlength=\"8\" value=\"" + currpage + "\"/><anchor><go href=\"" + strlink + "\" method=\"get\" accept-charset=\"utf-8\"><postfield name=\"page\" value=\"$(page)\"/></go>翻页</anchor><br/>");
                }
                else
                {
                    strlink = strlink.Replace("&amp;", "&");
                    string text = strlink.Split('?')[0];
                    string text2 = strlink.Split('?')[1];
                    string[] array = text2.Split('&');
                    stringBuilder.Append("<form name=\"go\" method=\"get\" action=\"" + text + "\"><input type=\"text\" name=\"page\" value=\"" + currpage + "\" style=\"width:30px\" data-role=\"none\"/>");
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split('=');
                        stringBuilder.Append(" <input type=\"hidden\" name=\"" + array2[0] + "\" value=\"" + array2[1] + "\" /> ");
                    }
                    stringBuilder.Append("<input type=\"submit\" name=\"go\" class=\"urlbtn\" value=\"翻页\" data-role=\"none\"/></form>");
                    stringBuilder.Append("</div>");
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetPageLinkWEB(string string_0, string lang, long total, long pagesize, long currpage, string strlink)
        {
            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            num = ((total % pagesize != 0L) ? (total / pagesize + 1L) : (total / pagesize));
            if (currpage > num)
            {
                currpage = num;
            }
            else if (currpage < 1L)
            {
                currpage = 1L;
            }
            try
            {
                strlink = strlink.Replace("&amp;", "&");
                string text = strlink.Split('?')[0];
                string text2 = strlink.Split('?')[1];
                string[] array = text2.Split('&');
                stringBuilder.Append("<form name=\"go\" method=\"get\" action=\"" + text + "\">");
                stringBuilder.Append("第 " + currpage + "/" + num + " 页，共 " + total + " 条 ");
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=1\">首页</a> ");
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage - 1L) + "\">上一页</a> ");
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + (currpage + 1L) + "\">下一页</a> ");
                stringBuilder.Append("<a href=\"" + strlink + "&amp;page=" + num + "\">尾页</a> ");
                stringBuilder.Append("第<input type=\"text\" name=\"page\" value=\"" + currpage + "\" size=\"3\"/>页");
                stringBuilder.Append("/每页<input type=\"text\" name=\"setpagesize\" value=\"" + pagesize + "\" size=\"3\"/>");
                for (int i = 0; i < array.Length; i++)
                {
                    string[] array2 = array[i].Split('=');
                    stringBuilder.Append("<input type=\"hidden\" name=\"" + array2[0] + "\" value=\"" + array2[1] + "\" />");
                }
                stringBuilder.Append("<input type=\"submit\" name=\"go\" value=\"翻页\" class=\"bt\"/></form>");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return stringBuilder.ToString();
        }

        public static string GetMyID(string string_0, string lang, DateTime dateTime_0)
        {
            if (string_0 == null || string_0 == "" || string_0 == "#")
            {
                string_0 = GetLang("普通会员|普通會員|Ordinary Members", lang);
            }
            string_0 = (string_0 + "#").Split('#')[0];
            if (string_0.ToLower().IndexOf(".gif") > 0 || string_0.ToLower().IndexOf(".jpg") > 0 || string_0.ToLower().IndexOf(".png") > 0)
            {
                string_0 = "<img src=\"" + string_0 + "\" alt=\"" + GetLang("图|圖|PIC", lang) + "\"/>";
            }
            if (dateTime_0 == DateTime.MinValue)
            {
                return string_0;
            }
            long num = long.Parse(dateTime_0.Subtract(DateTime.Now).TotalDays.ToString("f0"));
            if (num < 1L)
            {
                return "<img src=\"/NetImages/vipx.gif\" alt=\"已过期" + num + "天\"/>";
            }
            return string_0;
        }

        public static string GetMyID(string string_0, string lang)
        {
            if (string_0 == null || string_0 == "" || string_0 == "#")
            {
                string_0 = GetLang("普通会员|普通會員|Ordinary Members", lang);
            }
            string_0 = (string_0 + "#").Split('#')[0];
            if (string_0.ToLower().IndexOf(".gif") > 0 || string_0.ToLower().IndexOf(".jpg") > 0 || string_0.ToLower().IndexOf(".png") > 0)
            {
                string_0 = "<img src=\"" + string_0 + "\" alt=\"" + GetLang("图|圖|PIC", lang) + "\"/>";
            }
            return string_0;
        }

        public static string GetColorNickName(string idname, string nickname, string lang, string string_0, DateTime dateTime_0)
        {
            string text = "";
            string text2 = "";
            StringBuilder stringBuilder = new StringBuilder();
            string[] array = (idname + "#").Split('#');
            text = array[0];
            text2 = array[1];
            if (text.ToLower().IndexOf(".gif") > 0 || text.ToLower().IndexOf(".jpg") > 0)
            {
                if (dateTime_0 == DateTime.MinValue)
                {
                    stringBuilder.Append("<img src=\"" + text + "\" alt=\"" + GetLang("图|圖|PIC", lang) + "\"/>");
                }
                else
                {
                    long num = long.Parse(dateTime_0.Subtract(DateTime.Now).TotalDays.ToString("f0"));
                    if (num < 1L)
                    {
                        stringBuilder.Append("<img src=\"/NetImages/vipx.gif\" alt=\"已过期" + num + "天\"/>");
                    }
                    else
                    {
                        stringBuilder.Append("<img src=\"" + text + "\" alt=\"" + GetLang("图|圖|PIC", lang) + "\"/>");
                    }
                }
                stringBuilder.Append(" ");
            }
            if (string_0 != "1" && text2 != "")
            {
                stringBuilder.Append("<font color=\"#" + text2 + "\">");
                stringBuilder.Append(nickname);
                stringBuilder.Append("</font>");
            }
            else
            {
                stringBuilder.Append(nickname);
            }
            return stringBuilder.ToString();
        }

        public static string GetColorNickName(string idname, string nickname, string lang, string string_0)
        {
            string text = "";
            string text2 = "";
            StringBuilder stringBuilder = new StringBuilder();
            string[] array = (idname + "#").Split('#');
            text = array[0];
            text2 = array[1];
            if (text.ToLower().IndexOf(".gif") > 0 || text.ToLower().IndexOf(".jpg") > 0)
            {
                stringBuilder.Append("<img src=\"" + text + "\" alt=\"" + GetLang("图|圖|PIC", lang) + "\"/>");
                stringBuilder.Append(" ");
            }
            if (string_0 != "1" && text2 != "")
            {
                stringBuilder.Append("<font color=\"#" + text2 + "\">");
                stringBuilder.Append(nickname);
                stringBuilder.Append("</font>");
            }
            else
            {
                stringBuilder.Append(nickname);
            }
            return stringBuilder.ToString();
        }

        public static string GetCardIDNameFormID(string siteid, string string_0, string lang)
        {
            string string_ = "";
            string commandText = "select top 1 * from [wap2_smallType] where siteid = " + siteid + " and systype='card' and id = " + string_0;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                string_ = dataSet.Tables[0].Rows[0]["subclassname"].ToString();
            }
            return GetMyID(string_, lang);
        }

        public static string GetCardIDNameFormID_multiple(string siteid, string id_multiple, string lang)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            id_multiple = id_multiple.Replace("_", "|");
            string[] array = id_multiple.Split('|');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Trim() != "" && IsNumeric(array[i].Trim()))
                {
                    stringBuilder.Append(array[i].Trim());
                    stringBuilder.Append(",");
                    if (array[i].Trim() == "0")
                    {
                        stringBuilder2.Append("普通会员;");
                    }
                }
            }
            if (stringBuilder.ToString() != "")
            {
                stringBuilder.Append("0");
                string commandText = "select * from [wap2_smallType] where siteid = " + siteid + " and systype='card' and id in (" + stringBuilder.ToString() + ")";
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                {
                    return " ";
                }
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    stringBuilder2.Append(GetMyID(row["subclassname"].ToString(), lang));
                    stringBuilder2.Append(" ; ");
                }
            }
            return stringBuilder2.ToString();
        }

        public static string showIDEndTime(long siteid, long userid, DateTime dateTime_0, string lang)
        {
            if (siteid.ToString() == userid.ToString())
            {
                return "有效期至:无期限";
            }
            if (dateTime_0 == DateTime.MinValue)
            {
                return "有效期至:无期限";
            }
            string text = "有效期至:" + $"{dateTime_0:yyyy-MM-dd}";
            long num = long.Parse(dateTime_0.Subtract(DateTime.Now).TotalDays.ToString("f0"));
            if (num < 1L)
            {
                UpdateSQL("update [user] set sessiontimeout=0,endtime=null where userid=" + userid + " and siteid=" + siteid);
                text = text + " 已过:" + num + "天";
            }
            else
            {
                text = text + " 还有:" + num + "天";
            }
            return text;
        }

        public static long showIDEndTime(long siteid, long userid, DateTime dateTime_0)
        {
            if (siteid.ToString() == userid.ToString())
            {
                dateTime_0 = DateTime.MaxValue;
            }
            else if (dateTime_0 == DateTime.MinValue)
            {
                dateTime_0 = DateTime.MaxValue;
            }
            return long.Parse(dateTime_0.Subtract(DateTime.Now).TotalDays.ToString("f0"));
        }

        public static string GetSiteMoneyName(string string_0, string lang)
        {
            if (string_0 == null || string_0 == "")
            {
                string_0 = GetLang("积分|積分|Integral", lang);
            }
            return string_0;
        }

        public static string GetOnline(string http_start, string flag, string string_0)
        {
            if (flag == "1")
            {
                return "<img src=\"" + http_start + "NetImages/on" + string_0 + ".gif\" alt=\"ONLINE\"/>";
            }
            return "<img src=\"" + http_start + "NetImages/off" + string_0 + ".gif\" alt=\"OFFLINE\"/>";
        }

        public static string GetPageContentLink(string string_0, string lang, long totalpages, long pagesize, long currpage, string strlink)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string_0 != "1" && totalpages > 1L)
            {
                stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\"><a id=\"KL_loadmore\" href=\"javascript:;\" onclick=\"KL_show_next('" + totalpages + "','" + pagesize + "','" + currpage + "','" + strlink + "','vpage')\"><span id=\"KL_show_loadimg\" ></span> <span id=\"KL_show_tip\">加载更多(" + currpage + "/" + totalpages + ")</span></a></div></div>");
                stringBuilder.Append("<script src=\"/NetCSS/KL_common.js\" language=\"javascript\"></script>");
            }
            if (totalpages < 4L)
            {
                return stringBuilder.ToString();
            }
            if (string_0 != "1")
            {
                stringBuilder.Append("<div class=\"showpage\">");
            }
            if (currpage > totalpages)
            {
                currpage = totalpages;
            }
            else if (currpage < 1L)
            {
                currpage = 1L;
            }
            if (totalpages <= 0L)
            {
            }
            stringBuilder.Append("<a class=\"urlbtn\" href=\"" + strlink + "&amp;vpage=" + (currpage + 1L) + "\">下页</a> &nbsp;&nbsp;&nbsp;");
            stringBuilder.Append("<a class=\"urlbtn\" href=\"" + strlink + "&amp;vpage=" + (currpage - 1L) + "\">上页</a> &nbsp;&nbsp;&nbsp;");
            if (totalpages > 1L)
            {
                stringBuilder.Append("<a class=\"urlbtn\" href=\"" + strlink + "&amp;view=all&amp;vpage=" + currpage + "\">全部</a> &nbsp;&nbsp;&nbsp;");
                stringBuilder.Append("<a class=\"urlbtn\" href=\"" + strlink + "&amp;viewleave=" + currpage + "&amp;vpage=" + currpage + "\">余下</a> &nbsp;&nbsp;&nbsp;");
            }
            if (totalpages > 1L)
            {
                if (string_0 == "1")
                {
                    stringBuilder.Append("<input title=\"输入页码\" name=\"page\" format=\"*N\" size=\"3\" maxlength=\"8\" value=\"" + currpage + "\"/><anchor><go href=\"" + strlink + "\" method=\"get\" accept-charset=\"utf-8\"><postfield name=\"vpage\" value=\"$(page)\"/></go>GO</anchor><br/>");
                }
                else
                {
                    strlink = strlink.Replace("&amp;", "&");
                    string text = strlink.Split('?')[0];
                    string text2 = strlink.Split('?')[1];
                    string[] array = text2.Split('&');
                    stringBuilder.Append("<form name=\"go\" method=\"get\" action=\"" + text + "\"><input type=\"text\" name=\"vpage\" value=\"" + currpage + "\" style=\"width:30px\" data-role=\"none\"/>");
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split('=');
                        stringBuilder.Append("<input type=\"hidden\" name=\"" + array2[0] + "\" value=\"" + array2[1] + "\" />");
                    }
                    stringBuilder.Append(" &nbsp;<input type=\"submit\" name=\"go\" value=\"翻页\" data-role=\"none\"/></form>");
                }
            }
            if (string_0 != "1")
            {
                stringBuilder.Append("</div>");
            }
            return stringBuilder.ToString();
        }

        public static string GetLang(string title, string lang)
        {
            try
            {
                return title.Split('|')[int.Parse(lang)];
            }
            catch (Exception)
            {
                try
                {
                    return title.Split('|')[2];
                }
                catch (Exception)
                {
                    return title.Split('|')[0];
                }
            }
        }

        public static string GetSiteDefault(string stremp, int ints)
        {
            stremp += "||||||||||||||||||||||||||||||||||||||";
            try
            {
                return stremp.Split('|')[ints];
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public static string SetSiteDefault(string oriStr, string ReplaceStr, int index)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                string[] array = oriStr.Split('|');
                int num = array.Length;
                if (index > num)
                {
                    num = index;
                }
                for (int i = 0; i < num; i++)
                {
                    if (i <= array.Length)
                    {
                        if (i == index)
                        {
                            stringBuilder.Append(ReplaceStr);
                            stringBuilder.Append("|");
                        }
                        else
                        {
                            stringBuilder.Append(array[i]);
                            stringBuilder.Append("|");
                        }
                    }
                    else if (i == index)
                    {
                        stringBuilder.Append(ReplaceStr);
                        stringBuilder.Append("|");
                    }
                    else
                    {
                        stringBuilder.Append("|");
                    }
                }
            }
            catch (Exception)
            {
                return oriStr;
            }
            return stringBuilder.ToString();
        }

        public static int CheckStrCount(string strings, string string_0)
        {
            int startIndex = 0;
            int num = 0;
            while (strings.IndexOf(string_0, startIndex) >= 0)
            {
                startIndex = strings.IndexOf(string_0, startIndex) + 1;
                num++;
            }
            return num;
        }

        public static long getMoneyRegular(string tempStr, int int_0)
        {
            long num = 0L;
            try
            {
                return long.Parse(tempStr.Split('|')[int_0]);
            }
            catch (Exception)
            {
                return 0L;
            }
        }

        public static long getLvLRegular(string tempStr, int int_0)
        {
            long num = 0L;
            try
            {
                return long.Parse(tempStr.Split('|')[int_0]);
            }
            catch (Exception)
            {
                return 0L;
            }
        }

        public static string getArryString(string tempStr, char splitstr, int int_0)
        {
            try
            {
                if (!string.IsNullOrEmpty(tempStr))
                {
                    return tempStr.Split(splitstr)[int_0];
                }
            }
            catch { }
            return "";
        }

        public static void setUserMoney(string siteid, string userid, string money)
        {
            user_DAL user_DAL = new user_DAL(_InstanceName);
            user_DAL.UpdateSQL("update [user] set money=money " + money + " where siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(userid));
        }

        public static void UpdateSQL(string string_0)
        {
            user_DAL user_DAL = new user_DAL(_InstanceName);
            user_DAL.UpdateSQL(string_0);
        }

        public static string UpdateStringKeyValue(string string_0, char splitChar, int int_0, string keyValue)
        {
            string text = "";
            string[] array = string_0.Split(splitChar);
            if (int_0 > array.Length - 1)
            {
                for (int i = 0; i < int_0 - (array.Length - 1); i++)
                {
                    string_0 += splitChar;
                }
                array = string_0.Split(splitChar);
            }
            for (int j = 0; j < array.Length; j++)
            {
                text = ((j != int_0) ? ((j != 0) ? (text + splitChar + array[j]) : array[j]) : ((j != 0) ? (text + splitChar + keyValue) : keyValue));
            }
            return text;
        }

        public static bool CheckUserBBSCount(string siteid, string userid, string count, string stype)
        {
            bool result = false;
            if (count == "0")
            {
                return true;
            }
            string text = "";
            string commandText = "";
            if (stype == "bbsre")
            {
                commandText = "select count(id) as n from [wap_bbsre] where devid='" + siteid + "' and userid=" + userid + " and   DATEDIFF(dd, redate, GETDATE()) <1";
            }
            else if (stype == "bbs")
            {
                commandText = "select count(id) as n from [wap_bbs] where userid=" + siteid + " and book_pub='" + userid + "' and   DATEDIFF(dd, book_date, GETDATE()) <1";
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                text = dataSet.Tables[0].Rows[0]["n"].ToString();
            }
            if (IsNumeric(text) && long.Parse(text) < long.Parse(count))
            {
                return true;
            }
            return result;
        }

        public static long isLockuser(string siteid, string userid, string classid)
        {
            long num = -1L;
            long num2 = 0L;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "SELECT lockdate, DATEDIFF(dd, GETDATE(), operdate + lockdate) AS dd FROM user_lock where siteid=" + siteid + " and (classid=" + classid + " or classid=0) and lockuserid=" + userid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                num = long.Parse(dataSet.Tables[0].Rows[0]["lockdate"].ToString());
                num2 = long.Parse(dataSet.Tables[0].Rows[0]["dd"].ToString());
            }
            if (num == 0L)
            {
                return 0L;
            }
            if (num2 == 0L)
            {
                return -1L;
            }
            return num2;
        }

        public static string Browser(string string_0)
        {
            if (string_0 == null)
            {
                return "";
            }
            if (string_0 == "")
            {
                return "Mobile Visit";
            }
            if (string_0.Length > 50)
            {
                string_0 = string_0.Substring(0, 50);
            }
            return string_0;
        }

        public static string Osystem(string string_0)
        {
            if (string_0 == null)
            {
                return "";
            }
            if (string_0.IndexOf("NT") > 0)
            {
                string_0 = "Windows XP/2000/NT...";
            }
            if (string_0.Length > 50)
            {
                string_0 = string_0.Substring(0, 50);
            }
            return string_0;
        }

        public static string URLtoWAP(string string_0)
        {
            return string_0;
        }

        public static string ErrorToString(string string_0)
        {
            if (string_0 != null)
            {
                if (PubConstant.GetAppString("KL_ShowAllError") == "0")
                {
                    string_0 += "\n";
                    string_0 = string_0.Split('\n')[0] + "<br/>";
                }
                return string_0.Replace("&", "&amp;").Replace("\r\n", "<br/>");
            }
            return "";
        }

        public static string ErrorToStringFirstLine(string string_0)
        {
            if (string_0 != null)
            {
                string_0 += "\n";
                string_0 = string_0.Split('\n')[0];
                return "<div class=\"tip\">" + string_0.Replace("&", "&amp;").Replace("\r\n", "<br/>") + "</div>";
            }
            return "";
        }

        public static bool isExistUser(string siteid, string touserid)
        {
            if (!IsNumeric(touserid))
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "SELECT top 1 userid from [user] where siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(touserid));
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isExistFriend(string siteid, string userid, string touserid, string type)
        {
            if (!IsNumeric(touserid))
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from wap_friends where siteid=" + siteid + " and userid=" + userid + " and frienduserid=" + touserid + " and friendtype=" + type);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isExistArticleTitle(string siteid, string title)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from wap_book where userid=" + siteid + " and book_title like '" + title.Replace("'", "") + "'");
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isClassAdmin(string siteid, string touserid)
        {
            if (!IsNumeric(touserid))
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 classid from [class] where userid=" + siteid + " and adminusername is not null and adminusername<>'' and  '|'+adminusername+'|' like '%|" + touserid + "|%'");
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string GetClassAdmin(string http_start, string string_0, string siteid, string touserid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 1;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select  * from (select  '|'+adminusername+'|' ad,classid,classname from [class] where adminusername is not null and adminusername<>'' and userid=" + siteid + " ) a  where a.ad like '%|" + touserid + "|%'");
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                stringBuilder.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + row["classid"].ToString() + "&amp;sid=" + string_0 + "\">" + row["classname"].ToString() + "</a>");
                if (num % 2 == 0)
                {
                    stringBuilder.Append("<br/>");
                }
                else
                {
                    stringBuilder.Append(" ");
                }
                num++;
            }
            if (!stringBuilder.ToString().EndsWith("<br/>"))
            {
                stringBuilder.Append("<br/>");
            }
            return stringBuilder.ToString();
        }

        public static bool isExistCSS(string siteid, long systemid)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from wap2_style where siteid=" + siteid + " and issystem=" + systemid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isExistCSS_WEB(string siteid, long systemid)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from wap3_style where siteid=" + siteid + " and issystem=" + systemid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static void DaoRuSystemCSS(string siteid, string userid)
        {
            try
            {
                wap2_style_BLL wap2_style_BLL = new wap2_style_BLL(PubConstant.GetAppString("InstanceName"));
                List<wap2_style_Model> list = new List<wap2_style_Model>();
                wap2_style_Model wap2_style_Model = new wap2_style_Model();
                wap2_style_Model.siteid = 0L;
                wap2_style_Model.isSystem = 1L;
                list = wap2_style_BLL.GetStyleList(wap2_style_Model);
                int num = 0;
                while (list != null && num < list.Count)
                {
                    list[num].isSystem = list[num].ID;
                    list[num].siteid = long.Parse(siteid);
                    list[num].create_user = long.Parse(userid);
                    list[num].create_time = DateTime.Now;
                    wap2_style_BLL.Add(list[num]);
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static string GetMobielUA(string uaid, string lang)
        {
            string result = "";
            if (!IsNumeric(uaid))
            {
                return "";
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select  top 1 nameCN,nameEN,Mode from wap2_mobile_UA  where id=" + uaid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                result = ((!(lang == "2")) ? (dataSet.Tables[0].Rows[0]["nameCN"].ToString() + " " + dataSet.Tables[0].Rows[0]["Mode"].ToString()) : (dataSet.Tables[0].Rows[0]["nameEN"].ToString() + " " + dataSet.Tables[0].Rows[0]["Mode"].ToString()));
            }
            return result;
        }

        public static string GetShopState(wml VO, string state)
        {
            string result = "0";
            if (!IsNumeric(state))
            {
                state = "0";
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select count(id) as num from wap_shopOrder where siteid=" + VO.siteid + " and ischeck=0 and userid=" + VO.userid + " and state=" + state);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["num"].ToString() != "")
            {
                result = dataSet.Tables[0].Rows[0]["num"].ToString();
            }
            return result;
        }

        public static bool isExistNotFriends(string siteid, string userid, string touserid)
        {
            if (!IsNumeric(touserid))
            {
                return false;
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 id from wap_friends where friendtype=1 and siteid=" + siteid + " and userid=" + touserid + " and frienduserid=" + userid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string GetMyAllFirend(string siteid, string userid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select frienduserid from wap_friends where siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(userid) + " and friendtype=0");
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                stringBuilder.Append(row["frienduserid"].ToString());
                stringBuilder.Append(".");
            }
            return stringBuilder.ToString();
        }

        public static string GetSiteid(string userid, string username)
        {
            string text = "";
            if (userid != "" && IsNumeric(userid))
            {
                text = "select top 1 siteid from [user] where userid=" + userid;
            }
            else
            {
                if (!(username != "") || !isNotChinese(username))
                {
                    return "0";
                }
                username = username.Replace("'", "‘");
                text = "select top 1 siteid from [user] where username='" + username + "'";
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataRow dataRow = (DataRow)enumerator.Current;
                    return dataRow["siteid"].ToString();
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return "0";
        }

        public static string GetDomain()
        {
            string result = "kelink.com";
            try
            {
                string commandText = "select top 1 domain from [domainname] where id=1";
                DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
                try
                {
                    if (enumerator.MoveNext())
                    {
                        DataRow dataRow = (DataRow)enumerator.Current;
                        if (dataRow["domain"].ToString() != "")
                        {
                            if ("xfjz8.com" == dataRow["domain"].ToString().Trim().ToLower() || "mrpej.com" == dataRow["domain"].ToString().Trim().ToLower())
                            {
                                return "wap.kelink.com";
                            }
                            return dataRow["domain"].ToString();
                        }
                        return result;
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string GetALLAdmin(string siteid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select adminusername from [class] where adminusername<>'' and adminusername is not null and userid=" + long.Parse(siteid));
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                stringBuilder.Append(row["adminusername"].ToString());
                stringBuilder.Append(".");
            }
            return stringBuilder.ToString().Replace("|", ".");
        }

        public static string GetClassToSiteid(long classid)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 userid from [class] where classid=" + classid);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataRow dataRow = (DataRow)enumerator.Current;
                    return dataRow["userid"].ToString();
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return "0";
        }

        public static long GetShop_Stock(long long_0, string siteid)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select top 1 book_hottel from [wap_shop] where id=" + long_0 + " and userid=" + siteid);
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return 0L;
            }
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (row["book_hottel"].ToString() != "")
                {
                    return long.Parse(row["book_hottel"].ToString());
                }
            }
            return 0L;
        }

        public static long CheckSendMSGCount(string siteid, string userid)
        {
            long result = 0L;
            string commandText = "select count(id) as n from wap_message where (DATEDIFF(dd, addtime, GETDATE()) < 1) and siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(userid) + " and isnew=1";
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                result = long.Parse(dataSet.Tables[0].Rows[0]["n"].ToString());
            }
            return result;
        }

        public static bool isUserRightPASS(string touserid, string toclassid)
        {
            if (toclassid == "" || toclassid == "0")
            {
                return true;
            }
            if (touserid == "" || touserid == "0")
            {
                return false;
            }
            string commandText = "SELECT top 1 a.* FROM sys_role_class a WHERE a.role_id IN  (SELECT b.role_id from sys_user_role b WHERE b.user_id = " + touserid + ") and a.classid=" + toclassid;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string getFcountSubMoneyFlag(string siteid, string userid, string string_0)
        {
            string result = "";
            string commandText = "select top 1  SubMoneyFlag from fcount where fip='" + string_0 + "' and fuserid=" + long.Parse(siteid) + " and userid=" + long.Parse(userid) + " order by fid desc ";
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                result = dataSet.Tables[0].Rows[0]["SubMoneyFlag"].ToString();
            }
            return result;
        }

        public static string getSmallTypeName(string siteid, string string_0)
        {
            if (string_0 == "" || string_0 == "0")
            {
                return "";
            }
            string commandText = "select subclassname from wap2_smalltype where siteid=" + long.Parse(siteid) + " and id=" + long.Parse(string_0);
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0]["subclassname"].ToString();
            }
            return "";
        }

        public static bool isNotChinese(string string_0)
        {
            if (string_0 == null || string_0 == "")
            {
                return false;
            }
            string_0 = string_0.ToLower();
            char[] array = string_0.ToCharArray();
            int num = 0;
            while (true)
            {
                if (num < array.Length)
                {
                    int num2 = Convert.ToInt32(array[num]);
                    if (num2 < 48 || (num2 > 57 && num2 < 97) || num2 > 122)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return true;
            }
            return false;
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            if (num > 1000)
            {
                num = 1000;
            }
            if (num2 > 1000)
            {
                num2 = 1000;
            }
            int x = 0;
            int y = 0;
            int num3 = image.Width;
            int num4 = image.Height;
            switch (mode)
            {
                case "Cut":
                    if ((double)image.Width / (double)image.Height > (double)num / (double)num2)
                    {
                        num4 = image.Height;
                        num3 = image.Height * num / num2;
                        y = 0;
                        x = (image.Width - num3) / 2;
                    }
                    else
                    {
                        num3 = image.Width;
                        num4 = image.Width * height / num;
                        x = 0;
                        y = (image.Height - num4) / 2;
                    }
                    break;

                case "H":
                    num = image.Width * height / image.Height;
                    if (num > 1000)
                    {
                        num = 1000;
                    }
                    break;

                case "W":
                    num2 = image.Height * width / image.Width;
                    if (num2 > 1000)
                    {
                        num2 = 1000;
                    }
                    break;
            }
            System.Drawing.Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
            try
            {
                image.Dispose();
                graphics.Dispose();
                EncoderParameters encoderParameters = new EncoderParameters();
                EncoderParameter encoderParameter = new EncoderParameter(value: new long[1] { 90L }, encoder: System.Drawing.Imaging.Encoder.Quality);
                encoderParameters.Param[0] = encoderParameter;
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo imageCodecInfo = null;
                for (int i = 0; i < imageEncoders.Length; i++)
                {
                    if (imageEncoders[i].FormatDescription.Equals("JPEG"))
                    {
                        imageCodecInfo = imageEncoders[i];
                        break;
                    }
                }
                if (thumbnailPath.ToLower().EndsWith(".jpg") || thumbnailPath.ToLower().EndsWith(".jpeg"))
                {
                    if (imageCodecInfo != null)
                    {
                        image2.Save(thumbnailPath, imageCodecInfo, encoderParameters);
                    }
                    else
                    {
                        image2.Save(thumbnailPath, ImageFormat.Jpeg);
                    }
                }
                else if (thumbnailPath.ToLower().EndsWith(".gif"))
                {
                    image2.Save(thumbnailPath, ImageFormat.Gif);
                }
                else if (thumbnailPath.ToLower().EndsWith(".png"))
                {
                    image2.Save(thumbnailPath, ImageFormat.Png);
                }
                else if (thumbnailPath.ToLower().EndsWith(".bmp"))
                {
                    image2.Save(thumbnailPath, ImageFormat.Bmp);
                }
                else
                {
                    image2.Save(thumbnailPath);
                }
                image2.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public static void AddWater(string Path, string Path_sy, string addtext, string logourl, long fontsize, string color)
        {
            long num = fontsize;
            if (logourl != "" && logourl.IndexOf(":") > 0)
            {
                AddWaterPic(Path, Path_sy, logourl);
                return;
            }
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            Graphics graphics = null;
            if (image.Width < 100)
            {
                image.Dispose();
                return;
            }
            if (num < 10L || num > 100L)
            {
                num = 18L;
            }
            if (color.Length != 6)
            {
                color = "FAE264";
            }
            color = "#" + color;
            try
            {
                WebColorConverter webColorConverter = new WebColorConverter();
                graphics = Graphics.FromImage(image);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                Font font = new Font("SimSun", num);
                Brush brush = new SolidBrush((Color)webColorConverter.ConvertFromString(color));
                graphics.DrawString(addtext, font, brush, 2f, 2f);
                graphics.Dispose();
                System.Drawing.Image image2 = new Bitmap(image);
                image.Dispose();
                EncoderParameters encoderParameters = new EncoderParameters();
                EncoderParameter encoderParameter = new EncoderParameter(value: new long[1] { 90L }, encoder: System.Drawing.Imaging.Encoder.Quality);
                encoderParameters.Param[0] = encoderParameter;
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo imageCodecInfo = null;
                for (int i = 0; i < imageEncoders.Length; i++)
                {
                    if (imageEncoders[i].FormatDescription.Equals("JPEG"))
                    {
                        imageCodecInfo = imageEncoders[i];
                        break;
                    }
                }
                if (Path_sy.ToLower().EndsWith(".jpg") || Path_sy.ToLower().EndsWith(".jpeg"))
                {
                    if (imageCodecInfo != null)
                    {
                        image2.Save(Path_sy, imageCodecInfo, encoderParameters);
                    }
                    else
                    {
                        image2.Save(Path_sy, ImageFormat.Jpeg);
                    }
                }
                else if (Path_sy.ToLower().EndsWith(".gif"))
                {
                    image2.Save(Path_sy, ImageFormat.Gif);
                }
                else if (Path_sy.ToLower().EndsWith(".png"))
                {
                    image2.Save(Path_sy, ImageFormat.Png);
                }
                else if (Path_sy.ToLower().EndsWith(".bmp"))
                {
                    image2.Save(Path_sy, ImageFormat.Bmp);
                }
                else
                {
                    image2.Save(Path_sy);
                }
                image2.Dispose();
            }
            catch (Exception ex)
            {
                graphics.Dispose();
                image.Dispose();
                image = null;
                graphics = null;
                throw new Exception(ex.ToString());
            }
        }

        public static void AddWaterPic(string Path, string Path_syp, string Path_sypf)
        {
            System.Drawing.Image image = null;
            System.Drawing.Image image2 = null;
            try
            {
                image = System.Drawing.Image.FromFile(Path);
                image2 = System.Drawing.Image.FromFile(Path_sypf);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width, image.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
                graphics.Dispose();
                System.Drawing.Image image3 = new Bitmap(image);
                image.Dispose();
                EncoderParameters encoderParameters = new EncoderParameters();
                EncoderParameter encoderParameter = new EncoderParameter(value: new long[1] { 90L }, encoder: System.Drawing.Imaging.Encoder.Quality);
                encoderParameters.Param[0] = encoderParameter;
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo imageCodecInfo = null;
                for (int i = 0; i < imageEncoders.Length; i++)
                {
                    if (imageEncoders[i].FormatDescription.Equals("JPEG"))
                    {
                        imageCodecInfo = imageEncoders[i];
                        break;
                    }
                }
                if (Path_syp.ToLower().EndsWith(".jpg") || Path_syp.ToLower().EndsWith(".jpeg"))
                {
                    if (imageCodecInfo != null)
                    {
                        image3.Save(Path_syp, imageCodecInfo, encoderParameters);
                    }
                    else
                    {
                        image3.Save(Path_syp, ImageFormat.Jpeg);
                    }
                }
                else if (Path_syp.ToLower().EndsWith(".gif"))
                {
                    image3.Save(Path_syp, ImageFormat.Gif);
                }
                else if (Path_syp.ToLower().EndsWith(".png"))
                {
                    image3.Save(Path_syp, ImageFormat.Png);
                }
                else if (Path_syp.ToLower().EndsWith(".bmp"))
                {
                    image3.Save(Path_syp, ImageFormat.Bmp);
                }
                else
                {
                    image3.Save(Path_syp);
                }
                image3.Dispose();
            }
            catch (Exception ex)
            {
                image.Dispose();
                image = null;
                image2 = null;
                throw new Exception(ex.ToString());
            }
        }

        public static string GetMachineCode()
        {
            string macAddress = GetMacAddress();
            macAddress += getArryString(DesDecrypt(GetFunction()).ToLower(), '|', 3);
            macAddress = macAddress.Replace(".", "").Replace(":", "").Replace(" ", "");
            return EnCode_KL(macAddress);
        }

        public static string GetCpuInfo()
        {
            string result = "";
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection instances = managementClass.GetInstances();
            foreach (ManagementObject item in instances)
            {
                result = item.Properties["ProcessorId"].Value.ToString();
            }
            return result;
        }

        public static string GetMacAddress()
        {
            string text = "";
            try
            {
                ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection instances = managementClass.GetInstances();
                foreach (ManagementObject item in instances)
                {
                    if ((bool)item["IPEnabled"])
                    {
                        text = item["MacAddress"].ToString();
                        string[] array = (string[])item["IPAddress"];
                        if (array.Length > 0)
                        {
                            text += array[0];
                        }
                    }
                    item.Dispose();
                }
            }
            catch (Exception)
            {
            }
            return text;
        }

        public static string isCheckREG(string username)
        {
            try
            {
                string s = regcode;
                string xmlString = "<RSAKeyValue><Modulus>uh9F4L860qaXoFEPfnrkA5E205HsebGF1yf27JtAbY9CURc7cgx4mZFHn0pljnVRV6g/pUpgqXhzdbIAXulh1bm+Q3L5tjUY6/MEHWrDWmWQzXr1L01MaFdeS6cMi2h/GnXjnKLTJT2sj2g4LiKWjmJQST60PkXuZPqHErLSyT0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider())
                {
                    rSACryptoServiceProvider.FromXmlString(xmlString);
                    RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rSACryptoServiceProvider);
                    rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
                    byte[] rgbSignature = Convert.FromBase64String(s);
                    SHA1Managed sHA1Managed = new SHA1Managed();
                    byte[] rgbHash = sHA1Managed.ComputeHash(Encoding.ASCII.GetBytes(username));
                    if (rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, rgbSignature))
                    {
                        return "1";
                    }
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public static string GetMobieleUAInfo(string username)
        {
            try
            {
                var text = regcode;
                text = text.Substring(0, text.IndexOf('=') + 1);
                var rgbSignature = Convert.FromBase64String(text);
                //
                var xmlString = "<RSAKeyValue><Modulus>uh9F4L86QqaXoFEPfnrkA5E205HsebGF1yf27JtAbY9CURc7cgx4mZFHn0pljnVRV6g/pUpgqXhzdbIAXulh1bm+Q3L5tjUY6/MEHWrDWmWQzXr1LO1MaFdeS6cMi2h/GnXjnKLTJT2sj2g4LiKWjmJQST60PkXuZPqHErLSyT0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                using (var rSACryptoServiceProvider = new RSACryptoServiceProvider())
                {
                    var sHA1Managed = new SHA1Managed();
                    var rgbHash = sHA1Managed.ComputeHash(Encoding.ASCII.GetBytes(username));
                    //
                    rSACryptoServiceProvider.FromXmlString(xmlString);
                    var rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rSACryptoServiceProvider);
                    rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
                    if (rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, rgbSignature))
                    {
                        return "1";
                    }
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string GetFunction()
        {
            int num = regcode.IndexOf('=');
            return regcode.Substring(num + 1, regcode.Length - num - 1);
        }

        public static bool isHasFunction(string string_0)
        {
            string text = DesDecrypt(GetFunction());
            string_0 = "|" + string_0 + "|";
            if (text.IndexOf(string_0) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 解密授权信息
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptString)
        {
            string text = "KL****KL**Kelink.com";
            byte[] bytes = Encoding.UTF8.GetBytes(text.Substring(0, 8));
            byte[] rgbIV = bytes;
            byte[] array = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// 加密授权信息
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptString)
        {
            string text = "KL****KL**Kelink.com";
            byte[] bytes = Encoding.UTF8.GetBytes(text.Substring(0, 8));
            byte[] rgbIV = bytes;
            byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string GetSelected(string key1, string key2)
        {
            if (key1 == key2)
            {
                return "selected";
            }
            return "";
        }

        public static string GetVersionAuto(string string_0)
        {
            if (string_0 == null || string_0 == "")
            {
                return "2";
            }
            string appString = PubConstant.GetAppString("KL_GoVersion0");
            string[] array = appString.Split('|');
            int num = 0;
            while (true)
            {
                if (num < array.Length)
                {
                    if (array[num].Trim() != "" && string_0.ToLower().IndexOf(array[num].ToLower().Trim()) >= 0)
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                string appString2 = PubConstant.GetAppString("KL_GoVersion1");
                string[] array2 = appString2.Split('|');
                num = 0;
                while (true)
                {
                    if (num < array2.Length)
                    {
                        if (array2[num].Trim() != "" && string_0.ToLower().IndexOf(array2[num].ToLower().Trim()) >= 0)
                        {
                            break;
                        }
                        num++;
                        continue;
                    }
                    string appString3 = PubConstant.GetAppString("KL_GoVersion4");
                    string[] array3 = appString3.Split('|');
                    num = 0;
                    while (true)
                    {
                        if (num < array3.Length)
                        {
                            if (array3[num].Trim() != "" && string_0.ToLower().IndexOf(array3[num].ToLower().Trim()) >= 0)
                            {
                                break;
                            }
                            num++;
                            continue;
                        }
                        string appString4 = PubConstant.GetAppString("KL_GoVersion3");
                        string[] array4 = appString4.Split('|');
                        num = 0;
                        while (true)
                        {
                            if (num < array4.Length)
                            {
                                if (array4[num].Trim() != "" && string_0.ToLower().IndexOf(array4[num].ToLower().Trim()) >= 0)
                                {
                                    break;
                                }
                                num++;
                                continue;
                            }
                            string appString5 = PubConstant.GetAppString("KL_GoVersion2");
                            string[] array5 = appString5.Split('|');
                            num = 0;
                            while (true)
                            {
                                if (num < array5.Length)
                                {
                                    if (array5[num].Trim() != "" && string_0.ToLower().IndexOf(array5[num].ToLower().Trim()) >= 0)
                                    {
                                        break;
                                    }
                                    num++;
                                    continue;
                                }
                                return "2";
                            }
                            return "2";
                        }
                        return "3";
                    }
                    return "4";
                }
                return "2";
            }
            return "2";
        }

        public static string GetUAUBB(string string_0, string string_1, string lang, string string_2, int showAll)
        {
            if (!IsNumeric(string_0))
            {
                string_0 = "0";
            }
            if (string_0 == "0")
            {
                return GetLang("未选|未选|未选", lang);
            }
            string commandText = "select  namecn,nameen,mode,remark from wap2_mobile_ua where id=" + string_0;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (showAll == 1)
                {
                    if (long.Parse(lang) > 1L)
                    {
                        return dataSet.Tables[0].Rows[0]["nameen"].ToString() + " " + dataSet.Tables[0].Rows[0]["mode"].ToString() + " " + dataSet.Tables[0].Rows[0]["remark"].ToString().Replace("[sid]", string_2);
                    }
                    return dataSet.Tables[0].Rows[0]["namecn"].ToString() + " " + dataSet.Tables[0].Rows[0]["mode"].ToString() + " " + dataSet.Tables[0].Rows[0]["remark"].ToString().Replace("[sid]", string_2);
                }
                return dataSet.Tables[0].Rows[0]["mode"].ToString() + " " + dataSet.Tables[0].Rows[0]["remark"].ToString().Replace("[sid]", string_2);
            }
            return GetLang("未选|未选|未选", lang);
        }

        public static string GetMobileOS(string string_0)
        {
            if (!IsNumeric(string_0))
            {
                string_0 = "0";
            }
            if (string_0 == "0")
            {
                return "";
            }
            string commandText = "select  OSystem from wap2_mobile_ua where id=" + string_0;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0]["OSystem"].ToString();
            }
            return "";
        }

        public static string GetUAAuto(string string_0)
        {
            string text = "";
            try
            {
                string text2 = "";
                if (string_0.ToLower().IndexOf("nokia") >= 0)
                {
                    Regex regex = new Regex("Nokia([^/]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("NOKIA", "");
                }
                if (string_0.ToLower().IndexOf("mot") >= 0)
                {
                    Regex regex = new Regex("MOT[-]?([^/]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("MOT", "");
                }
                if (string_0.ToLower().IndexOf("lg") >= 0)
                {
                    Regex regex = new Regex("LG[-]?([^/]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("LG", "");
                }
                if (string_0.ToLower().IndexOf("samsung") >= 0)
                {
                    Regex regex = new Regex("SGH[-]?([^/\\*]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("SGH", "");
                }
                if (string_0.ToLower().IndexOf("panasonic") >= 0)
                {
                    Regex regex = new Regex("Panasonic[-]?([^/\\*]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("PANASONIC", "");
                }
                if (string_0.ToLower().IndexOf("sonyericsson") >= 0)
                {
                    Regex regex = new Regex("SonyEricsson[-]?([^/\\*]+)", RegexOptions.IgnoreCase);
                    Match match = regex.Match(string_0);
                    text2 = match.Groups[0].Value;
                    return text2.ToUpper().Replace("SONYERICSSON", "");
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetDatePathString()
        {
            string text = DateTime.Now.Month.ToString();
            string text2 = DateTime.Now.Day.ToString();
            if (text.Length == 1)
            {
                text = "0" + text;
            }
            if (text2.Length == 1)
            {
                text2 = "0" + text2;
            }
            return DateTime.Now.Year + "/" + text + "/" + text2 + "/";
        }

        public static string GetMine(string string_0)
        {
            string_0 = string_0.ToLower();
            string appString = PubConstant.GetAppString("KL_MINE_" + string_0);
            if (appString != null && appString != "")
            {
                return appString;
            }
            switch (string_0)
            {
                case "apk":
                    return "application/vnd.android.package-archive";

                case "sisx":
                    return "x-epoc/x-sisx-app";

                case "sis":
                    return "application/vnd.symbian.install";

                case "cab":
                    return "application/vnd.smartpohone";

                case "hme":
                    return "application/x-hme";

                case "jad":
                    return "text/vnd.sun.j2me.app-descriptor";

                case "jar":
                    return "application/java-archive";

                case "ogg":
                    return "application/ogg";

                case "pdb":
                    return "application/ebook";

                case "rm":
                    return "video/rm";

                case "rng":
                    return "application/vnd.nokia.ringing-tone";

                case "sdt":
                    return "application/vnd.sie.thm";

                case "mpkg":
                    return "application/octet-stream";

                case "pkg":
                    return "application/octet-stream";

                case "nth":
                    return "application/x-nth";

                case "mtf":
                    return "application/x-mtf";

                case "thm":
                    return "application/vnd.eri.thm";

                case "tsk":
                    return "application/vnd.ppc.thm";

                case "umd":
                    return "application/umd";

                case "utz":
                    return "application/vnd.uiq.thm";

                case "abs":
                    return "audio/x-mpeg";

                case "3gp":
                    return "video/3gpp";

                case "amr":
                    return "audio/amr";

                case "avi":
                    return "video/x-msvideo";

                case "mid":
                    return "audio/mid";

                case "midi":
                    return "audio/mid";

                case "mmf":
                    return "application/vnd.smaf";

                case "mov":
                    return "video/quicktime";

                case "mp3":
                    return "audio/mpeg";

                case "mp4":
                    return "application/octet-stream";

                case "mpeg":
                    return "video/mpeg";

                case "mpg":
                    return "video/mpeg";

                case "swf":
                    return "application/x-shockwave-flash";

                case "wav":
                    return "audio/x-wav";

                case "zip":
                    return "application/zip";

                case "gif":
                    return "image/gif";

                default:
                    if (!(string_0 == "jpeg"))
                    {
                        if (string_0 == "png")
                        {
                            return "image/png";
                        }
                        if (string_0 == "bmp")
                        {
                            return "application/x-MS-bmp";
                        }
                        return "application/octet-stream";
                    }
                    goto case "jpg";
                case "jpg":
                    return "image/jpeg";
            }
        }

        public static string GetPage(string posturl, string postData, string method)
        {
            Stream stream = null;
            Stream stream2 = null;
            StreamReader streamReader = null;
            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] bytes = encoding.GetBytes(postData);
            try
            {
                httpWebRequest = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer2 = (httpWebRequest.CookieContainer = new CookieContainer());
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = bytes.Length;
                stream = httpWebRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                stream2 = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream2, encoding);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return "";
            }
        }

        public static string SendEmail(string toEmail, string toEmailName, string sitename, string title, string content, bool ishtml)
        {
            string appString = PubConstant.GetAppString("KL_SMTP_ADRRESS");
            string appString2 = PubConstant.GetAppString("KL_SMTP_UID");
            string appString3 = PubConstant.GetAppString("KL_SMTP_PW");
            string appString4 = PubConstant.GetAppString("KL_SMTP_EMAIL");
            if (appString == "")
            {
                return "邮件发送功能已关闭！(Mail function has been closed)";
            }
            try
            {
                SmtpClient smtpClient = new SmtpClient(appString);
                string address = appString4;
                MailAddress from = new MailAddress(address, sitename, Encoding.UTF8);
                MailMessage mailMessage = new MailMessage();
                string[] array = toEmail.Split(';');
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (text.Trim() != "")
                    {
                        mailMessage.To.Add(text);
                    }
                }
                mailMessage.From = from;
                mailMessage.Subject = title.Trim();
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = content.Trim();
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = ishtml;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                if (appString.ToLower().IndexOf("gmail") >= 0)
                {
                    smtpClient.EnableSsl = true;
                }
                else
                {
                    smtpClient.EnableSsl = false;
                }
                smtpClient.UseDefaultCredentials = true;
                string userName = appString2.Trim();
                string password = appString3.Trim();
                NetworkCredential networkCredential = (NetworkCredential)(smtpClient.Credentials = new NetworkCredential(userName, password));
                smtpClient.Send(mailMessage);
                return "Success";
            }
            catch (Exception ex)
            {
                string text2 = ex.ToString();
                text2 += "\n";
                return text2.Split('\n')[0].Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
            }
        }

        public static bool isAllowIP(string string_0)
        {
            if (string_0 == null)
            {
                return true;
            }
            string appString = PubConstant.GetAppString("KL_Allow_IP");
            long num = IPToNum(string_0);
            try
            {
                string[] array = appString.Split('|');
                for (int i = 0; i < array.Length; i++)
                {
                    string[] array2 = array[i].Split('-');
                    if (num >= IPToNum(array2[0]) && num <= IPToNum(array2[1]))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool isAllowUA(string UA)
        {
            if (UA == null)
            {
                return true;
            }
            string appString = PubConstant.GetAppString("KL_Allow_UA");
            appString += "|UC|CE|Symbian|NetFront|SmartPhone|SP|kia|WAP|MIDP";
            try
            {
                if ("".Equals(UA))
                {
                    return true;
                }
                UA = UA.ToLower();
                appString = appString.ToLower();
            }
            catch (Exception)
            {
                return true;
            }
            try
            {
                string[] array = appString.Split('|');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != "" && UA.IndexOf(array[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool isFileNameURLencode(string UA)
        {
            string appString = PubConstant.GetAppString("KL_FileNameURLencode");
            appString += "|Mozilla|MSIE";
            try
            {
                if ("".Equals(UA))
                {
                    return false;
                }
                UA = UA.ToLower();
                appString = appString.ToLower();
            }
            catch
            {
                return false;
            }
            try
            {
                string[] array = appString.Split('|');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != "" && UA.IndexOf(array[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static long IPToNum(string string_0)
        {
            if (string_0 == null || string_0 == "")
            {
                return 0L;
            }
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                string[] array = string_0.Split('.');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Length != 3)
                    {
                        string text = array[i] + "000";
                        stringBuilder.Append(text.Substring(0, 3));
                    }
                    else
                    {
                        stringBuilder.Append(array[i]);
                    }
                }
            }
            catch (Exception)
            {
                return 0L;
            }
            if (IsNumeric(stringBuilder.ToString()))
            {
                return long.Parse(stringBuilder.ToString());
            }
            return 0L;
        }

        public static string GetIDName(string siteid, string touserid, string managerlvl, string lang)
        {
            string result = "";
            switch (managerlvl)
            {
                case "01":
                    result = ((!(siteid == touserid)) ? GetLang("副站长|副站長|Webmaster", lang) : GetLang("站长|站長|Webmaster", lang));
                    break;

                case "00":
                    result = ((!(siteid == touserid)) ? GetLang("副超级管理员|副超級管理員|Super Admin", lang) : GetLang("超级管理员|超級管理員|Super Admin", lang));
                    break;

                case "03":
                    result = GetLang("总编辑|總編輯|Editor In Chief", lang);
                    break;

                case "04":
                    result = GetLang("总版主|總版主|Super Moderator", lang);
                    break;

                case "02":
                    result = GetLang("普通|普通|Normal", lang);
                    if (touserid != "" && siteid != "" && isClassAdmin(siteid, touserid))
                    {
                        result = GetLang("栏目管理员(版主)|欄目管理員(版主)|Column Master", lang);
                    }
                    break;
            }
            return result;
        }

        public static string GetSiteVIP(string flag)
        {
            if (flag == "1")
            {
                return "<img src=\"/NetImages/vip.gif\" alt=\"VIP\"/>";
            }
            return "";
        }

        public static string GetSystemVersion(string string_0, string lang)
        {
            string result = "";
            switch (string_0)
            {
                case "0":
                    return GetLang("企业版|企业版|企业版", lang);

                case "1":
                    return GetLang("标准版|标准版|标准版", lang);

                case "2":
                    return GetLang("个人版|个人版|个人版", lang);

                case "3":
                    return GetLang("DIY版|DIY版|DIY版", lang);

                default:
                    return result;
            }
        }

        public static string GetUrlFileName(string string_0)
        {
            if (string_0.IndexOf("/") >= 0)
            {
                string[] array = string_0.Split('/');
                return array[array.Length - 1];
            }
            return string_0;
        }

        public static string GetWeatherContent(string citycode)
        {
            string text = "";
            try
            {
                text = GetWeatherTemp(citycode);
                if (text == "")
                {
                    string address = "http://fuwu.3g.cn/foryou/weather/tianqi.aspx?sid=&sr=&mob=0&waped=2&id=" + citycode;
                    if (!IsNumeric(citycode))
                    {
                        address = "http://fuwu.3g.cn/foryou/weather/tianqi.aspx?sid=&sr=&mob=0&waped=2&city=" + citycode;
                    }
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add("Content-Type", "text/vnd.wap.wml");
                    webClient.Headers.Add("User-Agent", "Nokia3108/1.0 (05.08) Profile/MIDP-1.0 Configuration/CLDC-1.0");
                    Stream stream = webClient.OpenRead(address);
                    StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                    text = streamReader.ReadToEnd();
                    webClient.Dispose();
                    streamReader.Close();
                    text = text.Replace("----------------", "§");
                    text = text.Replace("http://fuwu.3g.cn/foryou/weather/images/", "/Utility/weather/images/");
                    text = text.Replace(".//", "/");
                    string[] array = text.Split('§');
                    if (array.Length >= 4)
                    {
                        text = ((array[1].IndexOf(".gif") <= 0) ? (array[2] + "--------------------------------") : (array[1] + "----------------" + array[2] + "----------------"));
                        text = text.Replace("3g.net.cn", "").Replace("3g.cn", "").Replace("fuwu.3g.cn", "")
                            .Replace("暂无数据", "暂无天气情况");
                        SetWeatherTemp(citycode, text);
                    }
                    else
                    {
                        text = "暂无天气情况----------------请稍后再试！----------------";
                        SetWeatherTemp(citycode, text);
                    }
                }
            }
            catch (Exception)
            {
                text = "暂无天气情况----------------请稍后再试！----------------";
                SetWeatherTemp(citycode, text);
            }
            return text.Replace("3g.net.cn", "").Replace("3g.cn", "").Replace("暂无数据", "暂无天气情况");
        }

        public static string GetWeatherContentFilter(string content)
        {
            int num = content.IndexOf("----------------");
            string text = "";
            if (num > 0)
            {
                string text2 = content.Replace("----------------", "§").Split('§')[1];
                content = content.Substring(0, num);
                int num2 = content.IndexOf("[风向]");
                if (num2 > 0)
                {
                    content = content.Substring(0, num2);
                }
                if (content.IndexOf("暂无") > 0 || content.Trim() == "")
                {
                    text = content.Replace("暂无天气情况", "");
                    content = text2;
                    int num3 = content.IndexOf("℃");
                    int num4 = content.IndexOf("℃", num3 + 1);
                    content = ((num3 <= 0) ? (text + " 暂无天气情况") : (text + " " + content.Substring(0, num4 + 1).Trim()));
                }
                content = content.Replace("地区天气预报", "");
                content = content.Replace("0" + DateTime.Now.Month + "月", "");
                content = content.Replace("0" + DateTime.Now.Day + "日", "");
                content = content.Replace(DateTime.Now.Month + "月", "");
                content = content.Replace(DateTime.Now.Day + "日", "");
                content = content.Replace("[天气]", "");
                content = content.Replace("[风向]", "");
                content = content.Replace("[气温]", "");
                content = content.Replace("[", "");
                content = content.Replace("]", "");
                content = content.Replace("\r\n", "");
                content = content.Replace("<br/>", "");
                content = content.Replace("<br />", "");
                content = content.Replace(DateTime.Now.Day - 1 + "日", "");
                content = content.Replace("今天", "");
                content = content.Replace("明天", "");
            }
            if (content == "")
            {
                content = text + " 暂无天气情况";
            }
            content = content.Replace("3g.net.cn", "").Replace("3g.cn", "");
            return content;
        }

        public static string GetWeatherShowIndex(string citycode, string content, string type)
        {
            string text = GetWeatherShowIndex(citycode, content);
            if (type == "1")
            {
                int num = text.IndexOf(">");
                if (num > 0)
                {
                    text = text.Substring(num + 1, text.Length - num - 1);
                }
            }
            else if (type == "2")
            {
                int num = text.IndexOf("(");
                if (num > 0)
                {
                    text = text.Substring(0, num);
                }
                text = text.Replace("/> ", "/><br/>");
            }
            return text;
        }

        public static string GetWeatherShowIndex(string citycode, string content)
        {
            if (content == "")
            {
                content = GetWeatherTemp(citycode);
            }
            if (content == "")
            {
                GetWeatherContent(citycode);
                content = GetWeatherTemp(citycode);
            }
            content = GetWeatherContentFilter(content);
            int num = content.IndexOf("℃");
            int num2 = content.IndexOf("℃", num + 1);
            if (num2 > 0)
            {
                content = content.Substring(0, num2 + 1);
            }
            else if (num > 0)
            {
                content = content.Substring(0, num + 1);
            }
            content = content.Replace("(", " ").Replace(")", "");
            return content;
        }

        public static string GetWeatherTemp(string city)
        {
            WeatherRemoveAll();
            string result = "";
            if (WeatherDay == DateTime.Now.Day.ToString())
            {
                foreach (KeyValuePair<string, string> item in WeatherArray)
                {
                    if (item.Key.ToLower() == city && item.Value != "")
                    {
                        result = item.Value;
                        break;
                    }
                }
            }
            return result;
        }

        public static void SetWeatherTemp(string city, string content)
        {
            try
            {
                WeatherRemoveAll();
                foreach (KeyValuePair<string, string> item in WeatherArray)
                {
                    if (item.Key.ToLower() == city && item.Value == "")
                    {
                        WeatherArray.Remove(item.Key);
                        break;
                    }
                }
                WeatherArray.Add(city, content);
            }
            catch (Exception)
            {
            }
        }

        public static void WeatherRemoveAll()
        {
            if (WeatherDay != DateTime.Now.Day.ToString())
            {
                WeatherArray.Clear();
                WeatherDay = DateTime.Now.Day.ToString();
            }
        }

        public static long DateDiff(DateTime nowDateTime, DateTime subDateTime, string type)
        {
            try
            {
                TimeSpan timeSpan = new TimeSpan(nowDateTime.Ticks);
                TimeSpan ts = new TimeSpan(subDateTime.Ticks);
                TimeSpan timeSpan2 = timeSpan.Subtract(ts).Duration();
                switch (type)
                {
                    case "DD":
                        return Convert.ToInt64(timeSpan2.TotalDays);

                    case "HH":
                        return Convert.ToInt64(timeSpan2.TotalHours);

                    case "MM":
                        return Convert.ToInt64(timeSpan2.TotalMinutes);

                    case "SS":
                        return Convert.ToInt64(timeSpan2.TotalSeconds);
                }
            }
            catch
            {
            }
            return 0L;
        }

        public static string GetRepeatString(string word, int int_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < int_0; i++)
            {
                stringBuilder.Append(word);
            }
            return stringBuilder.ToString();
        }

        public static int CalStringLength(string string_0)
        {
            return Encoding.Default.GetByteCount(string_0);
        }

        public static int CalStringLength_EN2(string string_0)
        {
            int num = 0;
            char[] array = string_0.ToCharArray();
            for (int i = 0; i < string_0.Length; i++)
            {
                if (Convert.ToInt32(array[i]) > 0)
                {
                    num++;
                }
            }
            return string_0.Length - num + num * 2;
        }

        public static string showImg(string string_0)
        {
            if (string_0 == null)
            {
                return "";
            }
            string_0 = string_0.ToLower();
            if (string_0.IndexOf('#') > 0)
            {
                string_0 = string_0.Split('#')[0];
            }
            if (string_0.EndsWith(".gif") || string_0.EndsWith(".jpg") || string_0.EndsWith(".jpeg") || string_0.EndsWith(".png"))
            {
                return "<img src=\"" + string_0 + "\" alt=\"load...\"/>";
            }
            return string_0;
        }

        public static string showNickNameColor(string string_0)
        {
            string text = "";
            string_0 += "#";
            text = string_0.Split('#')[1];
            return "#" + text;
        }

        public static string getCondition(string string_0, wap2_mobile_UA_Model mobileVo)
        {
            string text = "";
            if (mobileVo.Mode == "未选")
            {
                return "";
            }
            switch (string_0)
            {
                case "1":
                    if (mobileVo.OSystem != "")
                    {
                        text = text + " and book_img like '%" + mobileVo.OSystem + "%' ";
                    }
                    break;

                case "2":
                    text = text + " and book_img like '%" + mobileVo.widthpx + "X" + mobileVo.heightpx + "%' ";
                    break;

                case "3":
                    if (mobileVo.Series != "")
                    {
                        text = text + " and book_img like '%" + mobileVo.Series + "%' ";
                    }
                    break;

                case "4":
                    text = text + " and book_img like '%" + mobileVo.OSystem + mobileVo.widthpx + "X" + mobileVo.heightpx + "%' ";
                    break;

                case "5":
                    text = text + " and ( book_img like '%" + mobileVo.widthpx + "X" + mobileVo.heightpx + "%' ";
                    if (mobileVo.OSystem != "")
                    {
                        text = text + " or book_img like '%" + mobileVo.OSystem + "%' ";
                    }
                    if (mobileVo.Series != "")
                    {
                        text = text + " or book_img like '%" + mobileVo.Series + "%' ";
                    }
                    text += " ) ";
                    break;

                case "6":
                    text = text + " and book_img like '%" + mobileVo.OSystem + mobileVo.widthpx + "X" + mobileVo.heightpx + mobileVo.Series + "%' ";
                    break;
            }
            return text;
        }

        public static string ShowSizeInfo(long fsizelong)
        {
            decimal num = decimal.Parse(fsizelong.ToString());
            string result = fsizelong + "Byte";
            if (num > 1024m)
            {
                num /= 1024m;
                result = num.ToString("0.0") + "KB";
            }
            if (num > 1024m)
            {
                num /= 1024m;
                result = num.ToString("0.0") + "MB";
            }
            if (num > 1024m)
            {
                result = (num / 1024m).ToString("0.0") + "GB";
            }
            return result;
        }

        public static string GetMobileSearchKey(string platform, string screen, string serial, string string_0)
        {
            return "";
        }

        public static void DeleteFolder(string string_0)
        {
            try
            {
                if (!Directory.Exists(string_0))
                {
                    return;
                }
                string[] fileSystemEntries = Directory.GetFileSystemEntries(string_0);
                foreach (string text in fileSystemEntries)
                {
                    if (File.Exists(text))
                    {
                        File.Delete(text);
                    }
                    else
                    {
                        DeleteFolder(text);
                    }
                }
                Directory.Delete(string_0, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static long GetIsCheck(long ischeck, string version)
        {
            if (ischeck == 1L)
            {
                return 1L;
            }
            if ("1".Equals(GetSiteDefault(version, 30)))
            {
                return 1L;
            }
            return 0L;
        }

        public static void SaveUploadFileToLog(string siteid, string userid, string classid_type, string book_title, string string_0, string sizeKB, string book_file, string ischeck)
        {
            try
            {
                if (sizeKB == "0")
                {
                    sizeKB = "1";
                }
                string_0 = string_0.Replace(".", "");
                string commandText = "insert into sys_wap_files(userid,username,book_classid,book_title,book_ext,book_size,book_file,ischeck)values(@userid,@username,@book_classid,@book_title,@book_ext,@book_size,@book_file,@ischeck)";
                SqlParameter[] commandParameters = new SqlParameter[8]
                {
                new SqlParameter("@userid", siteid),
                new SqlParameter("@username", userid),
                new SqlParameter("@book_classid", classid_type),
                new SqlParameter("@book_title", book_title),
                new SqlParameter("@book_ext", string_0.Replace(".", "")),
                new SqlParameter("@book_size", sizeKB),
                new SqlParameter("@book_file", book_file),
                new SqlParameter("@ischeck", ischeck)
                };
                DbHelperSQL.ExecuteNonQuery(_ConnStr, CommandType.Text, commandText, commandParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GetSystemAndMyConfig(string systemconfig, string myconfig)
        {
            if (systemconfig == null)
            {
                systemconfig = "0";
            }
            if (myconfig == null)
            {
                myconfig = "0";
            }
            if (systemconfig == "1" || myconfig == "1")
            {
                return "1";
            }
            return "0";
        }

        public static void ClearDataTemp(string string_0)
        {
            try
            {
                if (string_0 == "0")
                {
                    DataTempArray.Clear();
                    return;
                }
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, string> item in DataTempArray)
                {
                    if (item.Key.IndexOf(string_0) >= 0)
                    {
                        list.Add(item.Key);
                    }
                }
                int num = 0;
                while (list != null && num < list.Count)
                {
                    DataTempArray.Remove(list[num].ToString());
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearDataClass(string siteid)
        {
            try
            {
                if (siteid == "0")
                {
                    DataClassArray.Clear();
                    return;
                }
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, List<class_Model>> item in DataClassArray)
                {
                    if (item.Key.IndexOf(siteid) >= 0)
                    {
                        list.Add(item.Key);
                    }
                }
                int num = 0;
                while (list != null && num < list.Count)
                {
                    DataClassArray.Remove(list[num].ToString());
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearDataBBS(string string_0)
        {
            try
            {
                if (string_0 == "0")
                {
                    DataBBSArray.Clear();
                    return;
                }
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, List<wap_bbs_Model>> item in DataBBSArray)
                {
                    if (item.Key.IndexOf(string_0) >= 0)
                    {
                        list.Add(item.Key);
                    }
                }
                int num = 0;
                while (list != null && num < list.Count)
                {
                    DataBBSArray.Remove(list[num].ToString());
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearDataBBSRe(string string_0)
        {
            try
            {
                if (string_0 == "0")
                {
                    DataBBSReArray.Clear();
                    return;
                }
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, List<wap_bbsre_Model>> item in DataBBSReArray)
                {
                    if (item.Key.IndexOf(string_0) >= 0)
                    {
                        list.Add(item.Key);
                    }
                }
                int num = 0;
                while (list != null && num < list.Count)
                {
                    DataBBSReArray.Remove(list[num].ToString());
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearDataArticle(string string_0)
        {
            try
            {
                if (string_0 == "0")
                {
                    DataArticleArray.Clear();
                    return;
                }
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, List<wap_book_Model>> item in DataArticleArray)
                {
                    if (item.Key.IndexOf(string_0) >= 0)
                    {
                        list.Add(item.Key);
                    }
                }
                int num = 0;
                while (list != null && num < list.Count)
                {
                    DataArticleArray.Remove(list[num].ToString());
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static string GetMyAction(wml wml_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (wml_0.userid == "0")
            {
                stringBuilder.Append("领任务需要<a href=\"" + wml_0.http_start + "waplogin.aspx?siteid=" + wml_0.siteid + "&amp;classid=" + wml_0.classid + "&amp;sid=" + wml_0.sid + "\">先登录网站</a>哦～<br/>");
                return stringBuilder.ToString();
            }
            wap_action_BLL wap_action_BLL = new wap_action_BLL(_InstanceName);
            List<wap_action_Model> list = new List<wap_action_Model>();
            string strSQL = "select * from wap_action where siteid =" + wml_0.siteid + " and userid=" + wml_0.userid + " and issystem=0 and DATEDIFF(dd, addtime, GETDATE()) = 0";
            list = wap_action_BLL.GetListVo_ALL(strSQL);
            int num = 0;
            while (list != null && num < list.Count)
            {
                stringBuilder.Append("任务" + (num + 1) + ":" + list[num].actionName + "(" + list[num].num + "/" + list[num].numFinish + "),");
                if (list[num].state == 1L)
                {
                    stringBuilder.Append("已完成");
                }
                else
                {
                    stringBuilder.Append("<a href=\"" + list[num].actionPath + "\">去完成</a>");
                }
                stringBuilder.Append("<br/>");
                num++;
            }
            if (list == null)
            {
                if (wml_0.userVo != null && $"{wml_0.userVo.actionTime:yyyy-MM-dd}" == $"{DateTime.Now:yyyy-MM-dd}" && wml_0.userVo.actionState == "1")
                {
                    stringBuilder.Append("恭喜您今天完成领任务啦，明天再来领任务吧～<br/>");
                }
                else
                {
                    stringBuilder.Append("今天还没有领任务哦～<a href=\"" + wml_0.http_start + "action/book_list.aspx?action=daorumy&amp;siteid=" + wml_0.siteid + "&amp;classid=0&amp;sid=" + wml_0.sid + "\">点击此领任务</a>！<br/>");
                }
            }
            else
            {
                stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\">");
                stringBuilder.Append("<a href=\"" + wml_0.http_start + "action/user_WAPdel.aspx?siteid=" + wml_0.siteid + "&amp;classid=" + wml_0.classid + "\">删除我当前的任务</a>");
                stringBuilder.Append("</div></div>");
            }
            return stringBuilder.ToString();
        }

        public static List<string> stringToArry(string content, int pageSize)
        {
            int num = 0;
            int num2 = 1;
            List<string> list = new List<string>();
            if (content == null)
            {
                content = "";
            }
            if (pageSize == 0)
            {
                list.Add(content);
                return list;
            }
            num = content.Length / pageSize;
            if (content.Length > num * pageSize)
            {
                num++;
            }
            for (int i = 0; i < num; i++)
            {
                num2 = i + 1;
                string text = "";
                if (num <= 1 || num2 < num)
                {
                    text = ((num <= 1 || num2 >= num) ? content : content.Substring((num2 - 1) * pageSize, pageSize));
                }
                else
                {
                    num2 = num;
                    text = content.Substring((num2 - 1) * pageSize, content.Length - (num2 - 1) * pageSize);
                }
                list.Add(text);
            }
            return list;
        }

        public static string left(string string_0, int charCount)
        {
            if (string_0 == null)
            {
                return "";
            }
            if (string_0.Length > charCount)
            {
                string_0 = string_0.Substring(0, charCount);
            }
            return string_0;
        }

        public static string right(string string_0, int charCount)
        {
            if (string_0 == null)
            {
                return "";
            }
            if (string_0.Length > charCount)
            {
                string_0 = string_0.Substring(string_0.Length - charCount, charCount);
            }
            return string_0;
        }

        public static string filePathFilter(string pathName)
        {
            if (pathName == null)
            {
                pathName = "";
            }
            else
            {
                pathName = pathName.Replace(";", "_");
                pathName = pathName.Replace("'", "_");
                pathName = pathName.Replace(" ", "");
            }
            return pathName;
        }

        public static StringBuilder getSiteCSS_WEB(string siteid, string string_0)
        {
            wap3_style_DAL wap3_style_DAL = new wap3_style_DAL(_InstanceName);
            return wap3_style_DAL.getSiteCSS(siteid, string_0);
        }

        public static string GetCode_WEB(string string_0, wml wmlVo)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select htmlcontent from [wap3_ModelFunc] where id =" + string_0 + " and siteid=" + wmlVo.siteid);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0]["htmlcontent"].ToString();
            }
            dataSet.Clear();
            return "";
        }

        public static string OnSale(decimal book_jiage, decimal book_yhjiage)
        {
            string text = "0";
            if (book_jiage == 0m || book_yhjiage == 0m)
            {
                text = "0";
            }
            else if (book_yhjiage <= book_jiage)
            {
                text = (book_yhjiage / book_jiage * 10m).ToString("f1");
                if (text.EndsWith(".0"))
                {
                    text = text.Replace(".0", "");
                }
                if (text.EndsWith("10"))
                {
                    text = text.Replace("10", "0");
                }
            }
            else if (book_yhjiage > book_jiage)
            {
                text = (book_jiage / book_yhjiage).ToString("f1");
            }
            return text;
        }

        public static string getStart(long book_re, long book_score, string YesImgOrTxt, string NoImgOrTxt, string ImgeOrTxt)
        {
            long num = 0L;
            string text = "";
            if (book_re < 1L)
            {
                book_re = 1L;
            }
            num = book_score / book_re;
            if (num > 5L)
            {
                num = 5L;
            }
            for (int i = 0; i < 5; i++)
            {
                text = ((i >= num) ? ((!(ImgeOrTxt == "0")) ? (text + NoImgOrTxt) : (text + "<img src=\"" + NoImgOrTxt + "\" alt=\"☆\"/>")) : ((!(ImgeOrTxt == "0")) ? (text + YesImgOrTxt) : (text + "<img src=\"" + YesImgOrTxt + "\" alt=\"★\"/>")));
            }
            return text;
        }

        public static string GetCityName(string allCityName)
        {
            string text = "";
            string text2 = "";
            text = allCityName.Split('省')[0];
            text2 = allCityName.Replace(text + "省", "").Split('市')[0];
            text = text.Split(' ')[0];
            text2 = text2.Split(' ')[0];
            if (text2.IndexOf("内蒙古") > -1)
            {
                text2 = text2.Replace("内蒙", "");
            }
            else if (text2.IndexOf("宁夏") > -1)
            {
                text2 = text2.Replace("宁夏", "");
            }
            else if (text2.IndexOf("新疆") > -1)
            {
                text2 = text2.Replace("新疆", "");
            }
            else if (text2.IndexOf("西藏") > -1)
            {
                text2 = text2.Replace("西藏", "");
            }
            else if (text2.IndexOf("广西") > -1)
            {
                text2 = text2.Replace("广西", "");
            }
            if (text2.IndexOf("地址") > -1)
            {
                text2 = "";
            }
            else if (text2.IndexOf("未知") > -1)
            {
                text2 = "";
            }
            return text2;
        }

        public static void toMakeSiteModel(string mySiteid, string ModelSiteid, int Dell_or_Add)
        {
            user_BLL user_BLL = new user_BLL(_InstanceName);
            user_Model model = user_BLL.GetModel(Convert.ToInt64(ModelSiteid));
            if (model != null)
            {
                user_BLL.UpdateSQL("update [user] set siteimg='" + model.siteimg + "',siteuptip='" + model.siteuptip + "',sitedowntip='" + model.sitedowntip + "',siteposition='" + model.siteposition + "',siterowremark='" + model.siterowremark + "',version='" + model.Version + "',sitelistflag=" + model.sitelistflag + " where userid=" + mySiteid);
            }
            if (Dell_or_Add == 1)
            {
                user_BLL.UpdateSQL("delete from [class] where userid=" + mySiteid);
                user_BLL.UpdateSQL("delete from [wap_book] where userid=" + mySiteid);
                user_BLL.UpdateSQL("delete from [wap_wml] where userid=" + mySiteid);
                user_BLL.UpdateSQL("delete from [Form_List] where siteid=" + mySiteid);
                user_BLL.UpdateSQL("delete from [Form_List_Detail] where siteid=" + mySiteid);
            }
            user_BLL.UpdateSQL("update wap2_style set style_type=0 where siteid=" + mySiteid + " and  style_type=1");
            user_BLL.UpdateSQL("insert into wap2_style  (siteid,style_name,style,style_type,style_color,rank,create_user,create_time,issystem) select " + mySiteid + ",style_name,style,1,style_color,0," + mySiteid + ",getdate(),0 from wap2_style where siteid=" + ModelSiteid + " and style_type=1");
            class_BLL class_BLL = new class_BLL(_InstanceName);
            class_Model class_Model = new class_Model();
            DataSet list = class_BLL.GetList("childid=0 and userid=" + ModelSiteid, "rank");
            DataView dataView = new DataView(list.Tables[0]);
            foreach (DataRowView item in dataView)
            {
                string text = item["classid"].ToString();
                class_Model.childid = 0L;
                class_Model.userid = Convert.ToInt64(mySiteid);
                class_Model.classname = item["classname"].ToString();
                class_Model.typeid = Convert.ToInt64(item["typeid"].ToString());
                class_Model.position = item["position"].ToString();
                class_Model.smallimg = item["smallimg"].ToString();
                class_Model.siteimg = item["siteimg"].ToString();
                class_Model.sitelist = Convert.ToInt64(item["sitelist"].ToString());
                class_Model.siterowremark = item["siterowremark"].ToString();
                class_Model.sitedowntip = item["sitedowntip"].ToString();
                class_Model.introduce = item["introduce"].ToString();
                class_Model.rank = Convert.ToInt64(item["rank"].ToString());
                class_Model.ishidden = Convert.ToInt64(item["ishidden"].ToString());
                class_Model.password = item["password"].ToString();
                class_Model.creatdate = DateTime.Now;
                long num = class_BLL.Add(class_Model);
                DataSet list2 = class_BLL.GetList("childid=" + text + " and userid=" + ModelSiteid, "rank");
                DataView dataView2 = new DataView(list2.Tables[0]);
                foreach (DataRowView item2 in dataView2)
                {
                    class_Model = null;
                    class_Model = new class_Model();
                    string text2 = item2["classid"].ToString();
                    class_Model.childid = num;
                    class_Model.userid = Convert.ToInt64(mySiteid);
                    class_Model.classname = item2["classname"].ToString();
                    class_Model.typeid = Convert.ToInt64(item2["typeid"].ToString());
                    class_Model.position = item2["position"].ToString();
                    class_Model.smallimg = item2["smallimg"].ToString();
                    class_Model.siteimg = item2["siteimg"].ToString();
                    class_Model.sitelist = Convert.ToInt64(item2["sitelist"].ToString());
                    class_Model.siterowremark = item2["siterowremark"].ToString();
                    class_Model.sitedowntip = item2["sitedowntip"].ToString();
                    class_Model.introduce = item2["introduce"].ToString();
                    class_Model.rank = Convert.ToInt64(item2["rank"].ToString());
                    class_Model.ishidden = Convert.ToInt64(item2["ishidden"].ToString());
                    class_Model.password = item2["password"].ToString();
                    class_Model.creatdate = DateTime.Now;
                    long num2 = class_BLL.Add(class_Model);
                    if (class_Model.typeid == 4L)
                    {
                        string text3 = "insert into [wap_book] (userid,book_classid,book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_file,book_fileinfo,book_img) ";
                        text3 = text3 + "select " + mySiteid + "," + num2 + ",book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_file,book_fileinfo,book_img from [wap_book] where userid=" + ModelSiteid + " and book_classid=" + text2;
                        DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, text3);
                    }
                    if (class_Model.typeid == 128L)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("insert into [wap_wml] (userid,book_classid,book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_content2,smalltype,sysid) ");
                        stringBuilder.Append("select " + mySiteid + "," + num2 + ",book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_content2,smalltype,id from [wap_wml] where userid=" + ModelSiteid + " and book_classid=" + text2);
                        DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                    }
                    string commandText = "update [user] set Version=replace(cast(Version as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "'),siteuptip=replace(cast(siteuptip as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "') where userid=" + mySiteid;
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText);
                    string commandText2 = "update [class] set siterowremark=replace(cast(siterowremark as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "'),introduce=replace(cast(introduce as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'classid=" + text2 + "','classid=" + num2 + "') where userid=" + mySiteid + " and classid=" + num2;
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText2);
                }
                if (class_Model.typeid == 4L)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("insert into [wap_book] (userid,book_classid,book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_file,book_fileinfo,book_img) ");
                    stringBuilder.Append("select " + mySiteid + "," + num + ",book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_file,book_fileinfo,book_img from [wap_book] where userid=" + ModelSiteid + " and book_classid=" + text);
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                }
                if (class_Model.typeid == 128L)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("insert into [wap_wml] (userid,book_classid,book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_content2,smalltype,sysid) ");
                    stringBuilder.Append("select " + mySiteid + "," + num + ",book_title,book_author,book_pub,book_content,book_re,book_click,book_date,makerid,book_content2,smalltype,id from [wap_wml] where userid=" + ModelSiteid + " and book_classid=" + text);
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                }
                if (class_Model.typeid == 143L)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("insert into [Form_List] (siteid,book_classid,book_title,book_content,book_type,book_submit,submitname,toemail,makerid,addtime,ischeck,hangbiaoshi,tomobile,tocontent) ");
                    stringBuilder.Append("select " + mySiteid + "," + num + ",book_title,book_content,book_type,book_submit,submitname,''," + mySiteid + ",getdate(),0,id,tomobile,tocontent      from [Form_List] where siteid=" + ModelSiteid + " and book_classid=" + text);
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                }
                string commandText3 = "update [user] set Version=replace(cast(Version as varchar(8000)),'classid=" + text + "','classid=" + num + "'),siteuptip=replace(cast(siteuptip as varchar(8000)),'classid=" + text + "','classid=" + num + "'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'classid=" + text + "','classid=" + num + "') where userid=" + mySiteid;
                DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText3);
                string commandText4 = "update [class] set siterowremark=replace(cast(siterowremark as varchar(8000)),'classid=" + text + "','classid=" + num + "'),introduce=replace(cast(introduce as varchar(8000)),'classid=" + text + "','classid=" + num + "'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'classid=" + text + "','classid=" + num + "') where userid=" + mySiteid + " and classid=" + num;
                DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText4);
            }
            string text4 = "";
            string text5 = "";
            string commandText5 = "select top 1 id,hangbiaoshi from Form_List where siteid = " + mySiteid;
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText5);
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    text4 = row["hangbiaoshi"].ToString();
                    text5 = row["id"].ToString();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("insert into [Form_List_Detail] (siteid,bookid,rank,formname,formtype,formlenth,formtext,formneed,votecount,hangbiaoshi) ");
                    stringBuilder.Append("select " + mySiteid + "," + text5 + ",rank,formname,formtype,formlenth,formtext,formneed,votecount,hangbiaoshi from Form_List_Detail where siteid=" + ModelSiteid + " and bookid=" + text4);
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
                    string commandText6 = "update [user] set Version=replace(cast(Version as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]'),siteuptip=replace(cast(siteuptip as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]') where userid=" + mySiteid;
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText6);
                    string commandText7 = "update [class] set siterowremark=replace(cast(siterowremark as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]'),introduce=replace(cast(introduce as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'[getform]" + text4 + "[/getform]','[getform]" + text5 + "[/getform]') where userid=" + mySiteid;
                    DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText7);
                }
            }
            string commandText8 = "select id,sysid,book_content from wap_wml where userid = " + mySiteid;
            DataSet dataSet2 = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText8);
            if (dataSet2 == null || dataSet2.Tables.Count <= 0 || dataSet2.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow row2 in dataSet2.Tables[0].Rows)
            {
                string text6 = row2["sysid"].ToString();
                string text7 = row2["id"].ToString();
                string commandText6 = "update [user] set Version=replace(cast(Version as varchar(8000)),'[getwml=" + text6 + "]','[getwml=" + text7 + "]'),siteuptip=replace(cast(siteuptip as varchar(8000)),'[getwml=" + text6 + "]','[getwml=" + text7 + "]'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'[getwml=" + text6 + "]','[getwml=" + text7 + "]') where userid=" + mySiteid;
                DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText6);
                string commandText7 = "update [class] set siterowremark=replace(cast(siterowremark as varchar(8000)),'[gewml=" + text6 + "]','[getwml=" + text7 + "]'),introduce=replace(cast(introduce as varchar(8000)),'[getwml=" + text6 + "]','[getwml=" + text7 + "]'),sitedowntip=replace(cast(sitedowntip as varchar(8000)),'[getwml=" + text6 + "]','[getwml=" + text7 + "]') where userid=" + mySiteid;
                DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, commandText7);
                if (text4 != "" && text5 != "" && text5 != "0")
                {
                    wap_wml_BLL wap_wml_BLL = new wap_wml_BLL(_InstanceName);
                    wap_wml_Model wap_wml_Model = new wap_wml_Model();
                    wap_wml_Model = wap_wml_BLL.GetModel(long.Parse(text7));
                    wap_wml_Model.book_content = wap_wml_Model.book_content.Replace("[getform]" + text4 + "[/getform]", "[getform]" + text5 + "[/getform]");
                    wap_wml_Model.book_content2 = wap_wml_Model.book_content2.Replace("[getform]" + text4 + "[/getform]", "[getform]" + text5 + "[/getform]");
                    wap_wml_BLL.Update(wap_wml_Model);
                    wap_wml_Model = null;
                }
            }
        }

        public static void UpdateSystemAuto()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("if not exists(select * from syscolumns where id=object_id('Form_List') and name='toMobile') begin  ALTER TABLE [Form_List] ADD toMobile nvarchar(20)  null,toContent nvarchar(200)  null  end  ");
            DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, stringBuilder.ToString());
        }

        public static string decodePhoneNo(string string_0)
        {
            string text = "";
            char[] array = string_0.ToCharArray();
            long num = 0L;
            for (int i = 0; i < 6; i++)
            {
                num = j64_bin(array[i]);
                string text2 = "000000" + Convert.ToString(num, 2);
                int startIndex = text2.Length - 6;
                text = text2.Substring(startIndex, 6) + text;
            }
            return Convert.ToInt64(text, 2).ToString();
        }

        public static long j64_bin(long iVal)
        {
            if (97L <= iVal && 122L >= iVal)
            {
                return (int)(iVal - 97L);
            }
            if (46L == iVal)
            {
                return 26L;
            }
            if (48L <= iVal && 57L >= iVal)
            {
                return iVal - 48L + 27L;
            }
            if (45L == iVal)
            {
                return 37L;
            }
            if (65L <= iVal && 90L >= iVal)
            {
                return (int)(iVal - 65L + 38L);
            }
            return 64L;
        }

        public static string GetRMBTypeName(string typeid)
        {
            switch (typeid)
            {
                case "0":
                    return "易宝支付";

                case "1":
                    return "WAP支付宝";

                case "2":
                    return "手机网银";

                case "3":
                    return "手工入帐";

                case "-3":
                    return "手工扣帐";

                case "4":
                    return "商品支付";

                case "-1":
                    return "购买虚拟币";

                case "-2":
                    return "购买身份级别";

                case "5":
                    return "RMB购买内容收入";

                case "-5":
                    return "RMB购买内容消费";

                default:
                    return "";
            }
        }

        public static string GetHeadImgHTML(string http_start, string headimg)
        {
            if (headimg == null || headimg == "")
            {
                return "<img src=\"" + http_start + "bbs/head/64.gif\" alt=\"头像\"/>";
            }
            if (headimg.StartsWith("http://qzapp.qlogo.cn"))
            {
                return "<img src=\"" + headimg + "\" alt=\"头像\"/>";
            }
            else if (headimg.IndexOf("/") >= 0)
            {
                return "<img src=\"" + http_start + headimg + "\" alt=\"头像\"/>";
            }
            return "<img src=\"" + http_start + "bbs/head/" + headimg + "\" alt=\"头像\"/>";
        }

        public static string GetHeadImgURL(string http_start, string headimg)
        {
            if (headimg == null || headimg == "")
            {
                return http_start + "bbs/head/64.gif";
            }
            if (headimg.StartsWith("http://qzapp.qlogo.cn"))
            {
                return headimg;
            }
            if (headimg.IndexOf("/") >= 0)
            {
                return http_start + headimg;
            }
            return http_start + "bbs/head/" + headimg;
        }

        public static bool isKillDomain()
        {
            if (DateTime.Now.Hour > 20)
            {
                return true;
            }
            return false;
        }

        public static string GetNickNameFromID(string siteid, string userid)
        {
            DataSet dataSet = DbHelperSQL.ExecuteDataset(_ConnStr, CommandType.Text, "select nickname from [user] where siteid=" + long.Parse(siteid) + " and  userid=" + long.Parse(userid));
            if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            IEnumerator enumerator = dataSet.Tables[0].Rows.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataRow dataRow = (DataRow)enumerator.Current;
                    return dataRow["nickname"].ToString();
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return "";
        }

        public static string NoHTML(string Htmlstring)
        {
            Htmlstring = Htmlstring.Replace("<br/>", "┐");
            Htmlstring = Htmlstring.Replace("<br>", "┐");
            Htmlstring = Htmlstring.Replace("<p", "┐<p");
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("┐", "<br/>");
            return Htmlstring;
        }

        public static string NoHTML2(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }

        public static string NoUBB(string Htmlstring)
        {
            return Htmlstring;
        }
    }
}
