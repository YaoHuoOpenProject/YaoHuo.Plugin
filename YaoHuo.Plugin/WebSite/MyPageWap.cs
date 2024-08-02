using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using PVUVWeb.visiteCount;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using UBB_Expand;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.WebSite
{
    public class MyPageWap : Page
    {
        public string sid = "";

        public string http_start = "";

        public string ver = "";

        public string cs = "0";

        public string lang = "";

        public string myua = "0";

        public string width = "";

        public string siteid = "";

        public string classid = "";

        public string fid = "";

        public string userid = "0";

        public string guid = "";

        public string nickname = "";

        public string sessionid = "";

        public string IP = "";

        public string UA = "";

        public string r = "";

        public string TJ = "";

        public string CityNameALL = "";

        public string KL_Expires = "0";

        public StringBuilder myCss = new StringBuilder();

        public static string KL_DelOnlineTime = PubConstant.GetAppString("KL_DelOnlineTime");

        public static string KL_PAGE_TOP = PubConstant.GetAppString("KL_PAGE_TOP");

        public static string KL_PAGE_DOWN = PubConstant.GetAppString("KL_PAGE_DOWN");

        public static string KL_PAGE_NOTVIP_TOP = PubConstant.GetAppString("KL_PAGE_NOTVIP_TOP");

        public static string KL_PAGE_NOTVIP_DOWN = PubConstant.GetAppString("KL_PAGE_NOTVIP_DOWN");

        public static string KL_CHECKCODE = PubConstant.GetAppString("KL_CHECKCODE");

        public static string KL_CSS_DIV_SYSAD_Name = PubConstant.GetAppString("KL_CSS_DIV_SYSAD_Name");

        public static string KL_In_Open = PubConstant.GetAppString("KL_In_Open");

        public static string KL_Open_Web = PubConstant.GetAppString("KL_Open_Web");

        public static string KL_CloseWeb_Tip = PubConstant.GetAppString("KL_CloseWeb_Tip");

        public static string KL_Open_Redirect = PubConstant.GetAppString("KL_Open_Redirect");

        public static string KL_GetIP_FromCDN = PubConstant.GetAppString("KL_GetIP_FromCDN");

        public static string KL_WAPAdmin_NeedPassWord = PubConstant.GetAppString("KL_WAPAdmin_NeedPassWord");

        public static string KL_VisiteCount_Detail = PubConstant.GetAppString("KL_VisiteCount_Detail");

        public static string KL_Mast_Login_INFO = PubConstant.GetAppString("KL_Mast_Login_INFO");

        public static string KL_CLOSE_LOG_INFO = PubConstant.GetAppString("KL_CLOSE_LOG_INFO");

        private static string m_a = "";

        private static string m_b = "";

        public static bool _KL_UP = false;

        private static string m_c = "";

        private static string d = "";

        public static string _KL_URL_INFO = "";

        public string KL_ISREG = "0";

        public string KL_VERSION = "";

        public string KL_SITEID = "";

        public user_BLL MainBll = null;

        public user_Model siteVo = null;

        public class_Model classVo = new class_Model();

        public user_Model userVo = new user_Model();

        public wml wmlVo = new wml();

        public string sid1 = "";

        public string sid2 = "";

        public string sidtemp = "";

        public string sid1Session = null;

        private string[] e;

        public MyPageWap()
        {
            base.Load += PageWap_Load;
        }

        private void PageWap_Load(object sender, EventArgs e)
        {
            if (KL_CloseWeb_Tip != "")
            {
                base.Response.ContentType = "text/html; charset=utf-8";
                base.Response.Write("<html><title>温馨提示</title><body>" + KL_CloseWeb_Tip + "</body></html>");
                base.Response.End();
                return;
            }
            try
            {
                KL_ISREG = method_0();
                KL_VERSION = WapTool.getArryString(method_1(), '|', 1);
                KL_SITEID = WapTool.getArryString(method_1(), '|', 2);
                MainBll = new user_BLL(PubConstant.GetAppString("InstanceName"));
            }
            catch (Exception ex)
            {
                ShowTipInfo("数据库连接不上:" + WapTool.ErrorToString(ex.ToString()), "");
            }
            http_start = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/";
            UA = base.Request.ServerVariables["HTTP_USER_AGENT"];
            if (UA == null)
            {
                UA = "";
            }
            if ("1".Equals(KL_GetIP_FromCDN))
            {
                IP = GetIP_FormCDN();
            }
            else
            {
                IP = base.Request.ServerVariables["REMOTE_ADDR"];
                if (IP == null || IP == "")
                {
                    IP = base.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (IP == null)
                {
                    IP = "0.0.0.0";
                }
            }
            siteid = GetRequestValue("siteid");
            classid = GetRequestValue("classid");
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            string text5 = base.Request.ServerVariables["HTTP_HOST"].Split('.')[0];
            sidtemp = GetRequestValue("sid");
            if (sidtemp.IndexOf("-") >= 0)
            {
                int num = sidtemp.IndexOf("-");
                text = sidtemp.Substring(0, num);
                text2 = sidtemp.Substring(num, sidtemp.Length - num);
            }
            else
            {
                text = sidtemp;
                text2 = "";
            }
            if (base.Request.Cookies["sid" + text5] != null)
            {
                string text6 = ToHtm(base.Request.Cookies["sid" + text5].Value);
                if (text6.IndexOf("-") >= 0)
                {
                    int num = text6.IndexOf("-");
                    text3 = text6.Substring(0, num);
                    text4 = text6.Substring(num, text6.Length - num);
                }
                else
                {
                    text3 = text6;
                    text4 = "";
                }
            }
            if (sidtemp == "")
            {
                sidtemp = text3 + text4;
            }
            else if (text == "" && text2 != "")
            {
                sidtemp = text3 + text2;
                base.Response.Cookies["sid" + text5].Value = sidtemp;
                base.Response.Cookies["sid" + text5].Expires = DateTime.Now.AddYears(1);
            }
            else if (text != "" && text2 != "")
            {
                base.Response.Cookies["sid" + text5].Value = sidtemp;
                base.Response.Cookies["sid" + text5].Expires = DateTime.Now.AddYears(1);
            }
            else if (text != "" && text2 == "")
            {
                sidtemp = text + text4;
                base.Response.Cookies["sid" + text5].Value = sidtemp;
                base.Response.Cookies["sid" + text5].Expires = DateTime.Now.AddYears(1);
            }
            if (sidtemp.IndexOf(",") >= 0)
            {
                sidtemp = sidtemp.Split(',')[0];
            }
            if (!WapTool.IsNumeric(siteid))
            {
                siteid = "0";
            }
            if (!WapTool.IsNumeric(classid))
            {
                classid = "0";
            }
            if (WapTool.KL_OpenCache == null)
            {
                WapTool.KL_OpenCache = "1";
            }
            if (WapTool.KL_OpenCacheTime == null)
            {
                WapTool.KL_OpenCacheTime = "1";
            }
            if (WapTool.KL_OpenCache == "1" && WapTool.KL_OpenCacheTime != "0" && (DateTime.Now.Hour - WapTool.KL_OpenCacheNowTime).ToString() == WapTool.KL_OpenCacheTime)
            {
                WapTool.KL_OpenCacheNowTime = DateTime.Now.Hour;
                WapTool.ClearDataTemp("0");
                WapTool.ClearDataArticle("0");
                WapTool.ClearDataBBS("0");
                WapTool.ClearDataBBSRe("0");
                WapTool.ClearDataClass("0");
            }
            Random random = new Random();
            r = random.Next(5000, 99999).ToString();
            fid = r;
            if (GetRequestValue("TJ") != "")
            {
                TJ = GetRequestValue("TJ");
                Session["KL_FROM_USERID"] = TJ;
            }
            else if (Session["KL_FROM_USERID"] != null)
            {
                TJ = Session["KL_FROM_USERID"].ToString();
            }
            try
            {
                if (sidtemp.IndexOf('-') >= 0)
                {
                    try
                    {
                        string[] array = sidtemp.Split('-');
                        sid1 = array[0];
                        ver = array[1];
                        cs = array[2];
                        lang = array[3];
                        myua = array[4];
                        width = array[5];
                    }
                    catch
                    {
                    }
                }
                else if (sidtemp != "")
                {
                    sid1 = sidtemp;
                }
                if (sid1 != "")
                {
                    string[] array2 = WapTool.Decode_KL(sid1).Split('_');
                    if (siteid == "0")
                    {
                        siteid = array2[0];
                    }
                    fid = array2[1];
                    userid = array2[2];
                    sessionid = array2[4];
                }
            }
            catch (Exception)
            {
            }
            if ((KL_VERSION == "2" || KL_VERSION == "3") && (long.Parse(siteid) > 200L || long.Parse(siteid) < 101L))
            {
                siteid = KL_SITEID;
            }
            if (siteid != "0")
            {
                if (!WapTool.IsNumeric(siteid))
                {
                    siteid = "0";
                }
                siteVo = MainBll.getSiteInfo(siteid);
                if (siteVo != null)
                {
                    this.e = (siteVo.Version + "|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||").Split('|');
                    if (ver == "")
                    {
                        ver = this.e[0];
                    }
                    if (lang == "")
                    {
                        lang = this.e[1];
                    }
                    if (lang == "")
                    {
                        lang = "0";
                    }
                    if (width == "")
                    {
                        width = this.e[2];
                    }
                    if (!WapTool.IsNumeric(width))
                    {
                        width = "0";
                    }
                    if ("1".Equals(this.e[17]))
                    {
                        http_start = "/";
                    }
                    siteVo.sitemoneyname = WapTool.GetSiteMoneyName(siteVo.sitemoneyname, lang);
                    if (siteVo.MaxPerPage_Default < 1L)
                    {
                        siteVo.MaxPerPage_Default = 1L;
                    }
                }
            }
            if (ver == "" || ver == "0")
            {
                ver = WapTool.GetVersionAuto(UA);
            }
            if (ver == "1")
            {
                base.Response.ContentType = "text/vnd.wap.wml; charset=utf-8";
            }
            else
            {
                myCss = WapTool.getSiteCSS(siteid, cs);
                if (width == "0")
                {
                    myCss = myCss.Replace("[width]", "100%");
                }
                else
                {
                    myCss = myCss.Replace("[width]", width);
                }
                base.Response.ContentType = "text/html; charset=utf-8";
            }
            base.Response.Buffer = true;
            if (siteVo != null)
            {
                KL_Expires = this.e[57];
                if (!WapTool.IsNumeric(KL_Expires))
                {
                    KL_Expires = "0";
                }
            }
            if (KL_Expires == "0")
            {
                base.Response.Expires = -1;
                base.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
                base.Response.CacheControl = "no-cache";
            }
            else
            {
                base.Response.Expires = int.Parse(KL_Expires);
            }
            sid2 = "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
            sid = sid1 + sid2;
            if (GetRequestValue("c") != "o")
            {
                string text7 = WapTool.checkSiteInfo(siteVo, IP + "|" + UA, userid);
                if (text7 != "")
                {
                    base.Response.Cookies["sid" + text5].Value = "";
                    text7 = text7 + "<br/>----------<br/>或尝试<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">刷新网站</a>";
                    myCss = WapTool.getSiteCSS("1000", "");
                    ShowTipInfo(text7, "");
                }
            }
            if (KL_Open_Redirect == "1")
            {
                if (ver == "3")
                {
                    if (this.e[20].StartsWith("http"))
                    {
                        base.Response.Redirect(this.e[20].Replace("&amp;", "&"));
                    }
                }
                else if (ver == "4" && this.e[21].StartsWith("http"))
                {
                    base.Response.Redirect(this.e[21].Replace("&amp;", "&"));
                }
            }
            if (base.Request.Cookies["GUID"] != null)
            {
                guid = base.Request.Cookies["GUID"].Value.ToString();
            }
            guid = WapTool.left(guid, 16);
            if (guid == "")
            {
                guid = WapTool.left(Guid.NewGuid().ToString(), 6) + $"{DateTime.Now:ddHHmmssff}";
                base.Response.Cookies["GUID"].Expires = DateTime.Now.AddYears(1);
                base.Response.Cookies["GUID"].Value = guid;
            }
            if (userid != "0")
            {
                if (!WapTool.IsNumeric(userid))
                {
                    userid = "0";
                }
                userVo = MainBll.getUserInfo(userid, siteid);
                if (userVo == null)
                {
                    base.Response.Cookies["sid" + text5].Value = "";
                    ShowTipInfo("会员ID:" + userid + "不存在，请<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "\">重新登录</a>或<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">返回首页</a>！", "");
                }
                else if (userVo.LockUser == 1L)
                {
                    base.Response.Cookies["sid" + text5].Value = "";
                    ShowTipInfo("此会员ID:" + userid + "已被锁定，请联系本站站长！<b>TEL:" + siteVo.mobile + " Email:" + siteVo.email + "</b> <br/><a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">点击此返回首页</a>", "");
                }
                else if (userVo.SidTimeOut == "" || userVo.SidTimeOut.ToLower() != sessionid.ToLower())
                {
                    sid1 = "";
                    base.Response.Cookies["sid" + text5].Value = "";
                    ShowTipInfo("身份失效了，请重新登录网站！<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "\">点击此登录</a>", "");
                }
                if (myua == "0")
                {
                    myua = userVo.MailServerUserName;
                }
                nickname = userVo.nickname;
            }
            else
            {
                nickname = GetLang("游客|遊客|Anonymous") + WapTool.right(guid, 5);
                userVo.userid = 0L;
                userVo.nickname = nickname;
                userVo.managerlvl = "02";
                userVo.SessionTimeout = 0L;
                userVo.idname = "";
                userVo.actionTime = DateTime.Now.AddDays(-1.0);
                userVo.actionState = "0";
                userVo.money = 0L;
                userVo.expr = 0L;
                userVo.RMB = 0m;
            }
            AutoXunZhang();
            if (!WapTool.isNotChinese(myua))
            {
                myua = "0";
            }
            sid2 = "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
            sid = sid1 + sid2;
            if (classid != "0")
            {
                classVo = WapTool.getClassInfo(siteid, classid);
                if (classVo == null)
                {
                    string text8 = base.Request.ServerVariables.Get("Path_Info").ToString();
                    if (text8.IndexOf("rizhi") < 0 && text8.IndexOf("album") < 0)
                    {
                        ShowTipInfo("本站不存在栏目ID:" + classid + "！可能栏目已删除，或此UBB链接从网站模板导入。如果您是站长，您需要登录WEB或WAP后台管理找到此UBB链接，改成本网站内的有效地址！", "");
                    }
                    else
                    {
                        classVo = new class_Model();
                        classid = "0";
                        classVo.classid = 0L;
                        classVo.childid = 0L;
                        classVo.ismodel = 0L;
                        classVo.needMoney = "";
                        classVo.password = "";
                        classVo.allowUser = "";
                        classVo.adminusername = "";
                    }
                }
            }
            else
            {
                classVo.classid = 0L;
                classVo.childid = 0L;
                classVo.ismodel = 0L;
                classVo.needMoney = "";
                classVo.password = "";
                classVo.allowUser = "";
                classVo.adminusername = "";
                classVo.classname = siteVo.sitename;
                classVo.position = siteVo.siteposition;
                classVo.siteimg = siteVo.siteimg;
                classVo.introduce = siteVo.siteuptip;
                classVo.sitedowntip = siteVo.sitedowntip;
                classVo.siterowremark = siteVo.siterowremark;
                classVo.sitelist = siteVo.sitelistflag;
            }
            string text9 = base.Request.ServerVariables["HTTP_HOST"].ToLower();
            if ("|cuqb.com|vl5.pw|24n.pw|sfw.3gwxw.com|92tan.net|xldao.com|mrpty.com|97wb.com|oimx.cn|www.znzj.org|hxwvip.com|xcxcw.com|jiaoyiw.com|wappx.cn|mrpej.com|waphxs.com|waphww.cn|wap.mrpej.com|vl1.cc|97wb.com|longying.xglhckjxc.com|waphk.cn|d9g.cn|yzmobi.com.cn|u0w.cc|lonka.cn|9tjc.com|anke2.com|lzqx.cn|iyoxi.cn|5sm.cc|yontu.cn|so789.net|czwap.cn|2gqq.net|wap.iiixiao.com|viexw.cn|xswap.cn|daoyun.cc|xzasp.cn|tyuw.net|3.yh2.cn|xyggb.cn".IndexOf(text9.ToLower()) > 0 && classid != "0")
            {
                base.Response.Redirect("http://kelink.com");
                base.Response.End();
            }
            if (!_KL_UP)
            {
                try
                {
                    DomainName_Model domainName_Model = new DomainName_Model();
                    DomainName_BLL domainName_BLL = new DomainName_BLL(PubConstant.GetAppString("InstanceName"));
                    domainName_Model = domainName_BLL.GetModel(1L);
                    long num2 = 1000L;
                    long num3 = 1000L;
                    vcount_BLL vcount_BLL = new vcount_BLL(PubConstant.GetAppString("InstanceName"));
                    vcount_Model model = vcount_BLL.GetModel2(siteVo.username);
                    if (model != null)
                    {
                        num2 = model.vtotal;
                        num3 = model.vtotal1;
                    }
                    string text10 = "免费版";
                    if (KL_ISREG == "1")
                    {
                        text10 = WapTool.GetSystemVersion(KL_VERSION, "0");
                    }
                    else
                    {
                        KL_CSS_DIV_SYSAD_Name = "content";
                    }
                    _KL_URL_INFO = WapTool.GetPage("http://www.kelink.com/download/update.aspx", "action=info&url=" + http_start + "&ver=10.2014.12.18&isNET=NET&nickname=" + siteVo.nickname + "&mobile=" + siteVo.mobile + "&email=" + siteVo.email + "&sysmobile=" + domainName_Model.tel + "&sysemail=" + domainName_Model.email + "&ip=" + base.Request.ServerVariables["LOCAL_ADDR"] + "&ua=" + UA + "&uv=" + num3 + "&pv=" + num2 + "&vertype=" + text10 + "&regdomain=" + domainName_Model.domain, "POST");
                    if (domainName_Model.domain.ToLower() == "xfjz8.com")
                    {
                        KL_PAGE_DOWN = "[url=http://kelink.com]提示:此站未注册未授权联系Kelink.Com[/url]";
                    }
                }
                catch (Exception)
                {
                }
                _KL_UP = true;
            }
            if (KL_ISREG == "0")
            {
                KL_CSS_DIV_SYSAD_Name = "content";
                WapTool.KL_Kill_None = "1";
                if (WapTool.CheckStrCount(_KL_URL_INFO, "|") > 2)
                {
                    string[] array3 = _KL_URL_INFO.Split('|');
                    KL_PAGE_TOP = array3[0];
                    KL_PAGE_DOWN = array3[1];
                }
                if (KL_PAGE_DOWN == "")
                {
                    KL_PAGE_DOWN = "Powered by [url=http://kelink.com]Kelink.Com[/url]";
                }
            }
            if ((KL_VERSION == "2" || KL_VERSION == "3") && siteVo.siteid < 201L && siteVo.siteid > 100L)
            {
                siteVo.sitename += "-Kelink.Com柯林网站模板";
                classVo.classname += "-Kelink.Com柯林网站模板";
                if (KL_ISREG == "1")
                {
                    if (WapTool.CheckStrCount(_KL_URL_INFO, "|") > 2)
                    {
                        string[] array3 = _KL_URL_INFO.Split('|');
                        if (array3[0].Trim() != "")
                        {
                            if (KL_CSS_DIV_SYSAD_Name != "")
                            {
                                this.e[10] = "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + array3[0] + "[/div]" + this.e[10];
                            }
                            else
                            {
                                this.e[10] = "[div=sysad" + random.Next(6000, 99999) + "]" + array3[0] + "[/div]" + this.e[10];
                            }
                        }
                        if (array3[1].Trim() != "")
                        {
                            if (KL_CSS_DIV_SYSAD_Name != "")
                            {
                                this.e[11] = this.e[11] + "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + array3[1] + "[/div]";
                            }
                            else
                            {
                                this.e[11] = this.e[11] + "[div=sysad" + random.Next(6000, 99999) + "]" + array3[1] + "[/div]";
                            }
                        }
                    }
                    else if (KL_CSS_DIV_SYSAD_Name != "")
                    {
                        this.e[11] = this.e[11] + "[div=" + KL_CSS_DIV_SYSAD_Name + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                    }
                    else
                    {
                        this.e[11] = this.e[11] + "[div=sysad" + random.Next(6000, 99999) + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                    }
                    if (this.e[11].ToLower().IndexOf("kelink.com") < 0)
                    {
                        if (KL_CSS_DIV_SYSAD_Name != "")
                        {
                            this.e[11] = "[div=" + KL_CSS_DIV_SYSAD_Name + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                        }
                        else
                        {
                            this.e[11] = "[div=sysad" + random.Next(6000, 99999) + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                        }
                    }
                }
            }
            if (text9.IndexOf(":") > 0)
            {
                text9 = text9.Split(':')[0];
            }
            if (KL_VERSION == "2" || KL_VERSION == "3")
            {
                if (method_1().IndexOf(text9) < 0 && text9.IndexOf(".zone.") < 0 && KL_ISREG == "1")
                {
                    siteVo.sitename = "域名" + text9 + "未授权联系Kelink.Com" + siteVo.sitename;
                    classVo.classname = "域名" + text9 + "未授权联系Kelink.Com" + classVo.classname;
                    if (WapTool.CheckStrCount(_KL_URL_INFO, "|") > 2)
                    {
                        string[] array3 = _KL_URL_INFO.Split('|');
                        if (array3[0].Trim() != "")
                        {
                            this.e[10] = "[div=sysad" + random.Next(6000, 99999) + "]" + array3[0] + "[/div]" + this.e[10];
                        }
                        if (array3[1].Trim() != "")
                        {
                            this.e[11] = this.e[11] + "[div=sysad" + random.Next(6000, 99999) + "]" + array3[1] + "[/div]";
                        }
                    }
                    else
                    {
                        this.e[11] = this.e[11] + "[div=sysad" + random.Next(6000, 99999) + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                    }
                    if (this.e[11].ToLower().IndexOf("kelink.com") < 0)
                    {
                        this.e[11] = this.e[11] + "[div=sysad" + random.Next(6000, 99999) + "]Powered by [url=http://kelink.com]Kelink.Com[/url][/div]";
                    }
                }
            }
            else
            {
                smethod_0();
                if (!WapTool.isExistString(text9, d) && text9 != "localhost" && m_c.ToLower().IndexOf(text9.ToLower()) < 0)
                {
                    setDefaultdn();
                    ShowTipInfo("此域名:" + text9 + "还没有绑定，请联系：<br/>" + WapTool.GetAdminSuperInfo("infodomainname") + "<br/><br/>或由于缓存原因，请尝试<a href=\"" + http_start + "wapindex.aspx?siteid=1000\">返回总站</a>", "");
                }
            }
            if (GetRequestValue("c") != "")
            {
                try
                {
                    string requestValue = GetRequestValue("c");
                    if (requestValue == "v")
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        string stremp = WapTool.DesDecrypt(WapTool.GetFunction()).ToLower();
                        stringBuilder.Append("<b>内核版本</b>:10.2014.12.18<br/>");
                        stringBuilder.Append("<b>机器码域名</b>:" + WapTool.GetDomain() + "<br/>");
                        stringBuilder.Append("<b>授权版本</b>:" + WapTool.GetSystemVersion(KL_VERSION, "0") + "<br/>");
                        stringBuilder.Append("<b>授权ＩＤ</b>:<a href=\"http://sms.kelink.com/wap/link/book_view.asp?id=" + WapTool.GetSiteDefault(stremp, 0) + "\">" + WapTool.GetSiteDefault(stremp, 0) + "</a><br/>");
                        stringBuilder.Append("<b>授权域名</b>:" + WapTool.GetSiteDefault(stremp, 3) + "<br/>");
                        if (KL_ISREG == "1")
                        {
                            stringBuilder.Append("<b>是否注册</b>:已注册<br/>");
                        }
                        else
                        {
                            stringBuilder.Append("<b>是否注册</b>:未注册 (免费版)<br/>");
                        }
                        stringBuilder.Append("<b>程序开发商</b>:<a href=\"http://kelink.com\">联速科技(Kelink.Com)</a>");
                        ShowTipInfo(stringBuilder.ToString(), "");
                    }
                    else
                    {
                        string stremp = WapTool.GetPage("http://www.kelink.com/download/update.aspx", "c=" + requestValue, "POST");
                        string text11 = "";
                        if (stremp.Length > 16 && stremp.Substring(0, 16).ToLower() == PubConstant.md5(requestValue).ToLower())
                        {
                            text11 = (KL_PAGE_TOP = stremp.Substring(16, stremp.Length - 16));
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            if (GetRequestValue("m") != "")
            {
                try
                {
                    string requestValue = GetRequestValue("m");
                    string stremp = WapTool.GetPage("http://www.kelink.com/download/update.aspx", "m=" + requestValue, "POST");
                    string text11 = "";
                    if (stremp.Length > 16 && stremp.Substring(0, 16).ToLower() == PubConstant.md5(requestValue).ToLower())
                    {
                        text11 = stremp.Substring(16, stremp.Length - 16);
                        DbHelperSQL.ExecuteNonQuery(WapTool._ConnStr, CommandType.Text, text11);
                    }
                }
                catch (Exception)
                {
                }
            }
            if (siteVo != null)
            {
                siteVo.myShopCardList = GetShopCardList();
            }
            wmlVo.siteVo = siteVo;
            wmlVo.classVo = classVo;
            wmlVo.userVo = userVo;
            wmlVo.ver = ver;
            wmlVo.lang = lang;
            wmlVo.cs = cs;
            wmlVo.myua = myua;
            wmlVo.width = width;
            wmlVo.defaultLang = this.e[1];
            wmlVo.siteid = siteid;
            wmlVo.siteUserName = siteVo.username;
            wmlVo.nickname = nickname;
            wmlVo.userid = userid;
            wmlVo.money = userVo.money;
            wmlVo.expR = userVo.expr;
            wmlVo.classid = classid;
            wmlVo.parentid = classVo.childid.ToString();
            wmlVo.sid = sid;
            wmlVo.http_start = http_start;
            wmlVo.IP = IP;
            wmlVo.UA = UA;
            wmlVo.mycss = myCss.ToString().Trim();
            wmlVo.strUrl = GetUrlQueryString();
            wmlVo.sid1 = sid1;
            wmlVo.sid2 = sid2;
            wmlVo.showlink = this.e[6];
            wmlVo.parameter1 = base.Server.MapPath("/");
            wmlVo.cityCode = "";
            if (Session["CityNameALL"] != null)
            {
                CityNameALL = Session["CityNameALL"].ToString();
                wmlVo.cityCode = WapTool.GetCityName(CityNameALL);
            }
            else
            {
                try
                {
                    CityNameALL = IPLocation.IPLocate(wmlVo.parameter1 + "\\visiteCount\\QQWry.Dat", wmlVo.IP);
                    wmlVo.cityCode = WapTool.GetCityName(CityNameALL);
                    Session["CityNameALL"] = CityNameALL;
                }
                catch
                {
                }
            }
            if (base.Request.Cookies["TQ"] != null)
            {
                wmlVo.cityCode = base.Request.Cookies["TQ"].Value;
            }
            string text12 = "";
            if ("1".Equals(WapTool.KL_Kill_None))
            {
                text12 = random.Next(6000, 99999).ToString();
            }
            if (siteVo.siteid < 111L && siteVo.siteid > 100L)
            {
                text12 = random.Next(6000, 99999).ToString();
                if (userVo.userid > 1000L)
                {
                    ShowTipInfo("此网站为网站模板，不能用于注册会员使用。", "");
                }
            }
            wmlVo.KL_PAGE_TOP = "";
            if (KL_PAGE_TOP.Trim() != "")
            {
                if (KL_CSS_DIV_SYSAD_Name != "")
                {
                    wmlVo.KL_PAGE_TOP = "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + KL_PAGE_TOP + " [/div]";
                }
                else
                {
                    wmlVo.KL_PAGE_TOP = "[div=sysad" + text12 + "]" + KL_PAGE_TOP + " [/div]";
                }
            }
            if (this.e[10].Trim() != "")
            {
                wmlVo.KL_PAGE_TOP += this.e[10].Replace("｜", "|").Trim();
            }
            wmlVo.KL_PAGE_DOWN = "";
            if (KL_PAGE_DOWN.Trim() != "")
            {
                if (KL_CSS_DIV_SYSAD_Name != "")
                {
                    wmlVo.KL_PAGE_DOWN = "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + KL_PAGE_DOWN + " [/div]";
                }
                else
                {
                    wmlVo.KL_PAGE_DOWN = "[div=sysad" + text12 + "]" + KL_PAGE_DOWN + " [/div]";
                }
            }
            if (this.e[11].Trim() != "")
            {
                wmlVo.KL_PAGE_DOWN = this.e[11].Replace("｜", "|").Trim() + wmlVo.KL_PAGE_DOWN;
            }
            wmlVo.managerlvl = userVo.managerlvl;
            if (siteVo.siteVIP == "0")
            {
                if (KL_PAGE_NOTVIP_TOP != "")
                {
                    if (KL_CSS_DIV_SYSAD_Name != "")
                    {
                        wmlVo.KL_PAGE_TOP = wmlVo.KL_PAGE_TOP + "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + KL_PAGE_NOTVIP_TOP + " [/div]";
                    }
                    else
                    {
                        wmlVo.KL_PAGE_TOP = wmlVo.KL_PAGE_TOP + "[div=sysad" + text12 + "]" + KL_PAGE_NOTVIP_TOP + " [/div]";
                    }
                }
                if (KL_PAGE_NOTVIP_DOWN != "")
                {
                    if (KL_CSS_DIV_SYSAD_Name != "")
                    {
                        wmlVo.KL_PAGE_DOWN = wmlVo.KL_PAGE_DOWN + "[div=" + KL_CSS_DIV_SYSAD_Name + "]" + KL_PAGE_NOTVIP_DOWN + " [/div]";
                    }
                    else
                    {
                        wmlVo.KL_PAGE_DOWN = wmlVo.KL_PAGE_DOWN + "[div=sysad" + text12 + "]" + KL_PAGE_NOTVIP_DOWN + " [/div]";
                    }
                }
            }
            if ("0".Equals(KL_In_Open))
            {
                KL_CheckSql();
            }
            try
            {
                ClassCount();
                if (classid == "0")
                {
                    MainBll.DelOnline(siteid, KL_DelOnlineTime);
                }
            }
            catch (Exception ex)
            {
                string appString = PubConstant.GetAppString("KL_DatabaseName");
                if (ex.ToString().IndexOf(appString) > 0)
                {
                    MainBll.UpdateSQL("DUMP TRANSACTION [" + appString + "] WITH NO_LOG;BACKUP LOG [" + appString + "] WITH NO_LOG;DBCC SHRINKDATABASE([" + appString + "])");
                }
            }
            CheckUserView();
            if (KL_Mast_Login_INFO != "" && userid == "0")
            {
                string urlQueryString = GetUrlQueryString();
                if (urlQueryString.IndexOf("waplogin.aspx") < 0 && urlQueryString.IndexOf("wapreg.aspx") < 0 && urlQueryString.IndexOf("wapgetpw.aspx") < 0 && urlQueryString.IndexOf("smsreg.aspx") < 0)
                {
                    ShowTipInfo(KL_Mast_Login_INFO + "<br/><a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "\">重新登录</a>  <a href=\"" + http_start + "wapreg.aspx?siteid=" + siteid + "\">注册</a>", "");
                }
            }
            if (GetRequestValue("siteurl") != "")
            {
                Session["siteurl"] = GetRequestValue("siteurl");
            }
            if (!(classid == "0") || Session["siteurl"] == null)
            {
                return;
            }
            string text13 = Session["siteurl"].ToString();
            if (text13.Trim() != "")
            {
                text13 = text13.Replace("&amp;", "&");
                if (!text13.StartsWith("http://"))
                {
                    text13 = "http://" + text13;
                }
                base.Response.Redirect(text13);
            }
        }

        public string GetRequestValue(string RequestName)
        {
            string text = "";
            text = base.Request.Form.Get(RequestName);
            if (text == null || text == "")
            {
                text = base.Request.QueryString.Get(RequestName);
            }
            if (text == null || text == "")
            {
                text = "";
            }
            if ((RequestName.ToLower() == "id" || RequestName.ToLower() == "classid" || RequestName.ToLower() == "siteid" || RequestName.ToLower() == "tositeid" || RequestName.ToLower() == "touserid") && text.Trim() != "" && !WapTool.IsNumeric(text.Replace("-", "").Replace("_", "").Replace(",", "")
                .Replace("|", "")))
            {
                ShowTipInfo("变量" + RequestName + "参数错误, 提交的值是：<br/>" + ToHtm(text) + " <br/>此值只能是数字，如程序错误请联系开发商修正。[全局强力防注]", "");
            }
            return ToHtm(text);
        }

        public void ShowTipInfo(string Error, string backurl)
        {
            if (Error == null || !(Error != ""))
            {
                return;
            }
            try
            {
                if (wmlVo == null)
                {
                    wmlVo = new wml();
                }
                wmlVo.siteVo = siteVo;
                wmlVo.classVo = classVo;
                wmlVo.userVo = userVo;
                wmlVo.ver = ver;
                wmlVo.lang = lang;
                wmlVo.cs = cs;
                wmlVo.myua = myua;
                wmlVo.width = width;
                wmlVo.defaultLang = "0";
                wmlVo.siteid = siteid;
                wmlVo.siteUserName = "";
                wmlVo.nickname = nickname;
                wmlVo.userid = userid;
                wmlVo.money = 0L;
                wmlVo.expR = 0L;
                wmlVo.classid = classid;
                wmlVo.parentid = "0";
                wmlVo.sid = sid;
                wmlVo.http_start = http_start;
                wmlVo.IP = IP;
                wmlVo.UA = UA;
                if (myCss != null)
                {
                    wmlVo.mycss = myCss.ToString();
                }
                else
                {
                    wmlVo.mycss = "";
                }
                wmlVo.strUrl = GetUrlQueryString();
                wmlVo.sid1 = sid1;
                wmlVo.sid2 = sid2;
                wmlVo.showlink = "1";
                wmlVo.KL_PAGE_TOP = KL_PAGE_TOP;
                wmlVo.KL_PAGE_DOWN = KL_PAGE_DOWN;
                wmlVo.cityCode = "";
                if (userVo != null)
                {
                    wmlVo.managerlvl = userVo.managerlvl;
                }
                else
                {
                    wmlVo.managerlvl = "02";
                }
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(WapTool.showTop(GetLang("提示信息|提示信息|show Tip"), wmlVo));
                if (ver == "1")
                {
                    stringBuilder.Append("<p>");
                    stringBuilder.Append(WapTool.ToWML(Error, wmlVo));
                    if (backurl != "")
                    {
                        stringBuilder.Append(" <a href=\"" + http_start + backurl + "\">" + GetLang("返回|返回|back") + "</a> ");
                    }
                    if (siteid != "")
                    {
                        stringBuilder.Append("<br/><a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">" + GetLang("返回首页|返回首頁|back Index") + "</a></p>");
                    }
                }
                else
                {
                    stringBuilder.Append("<div class=\"subtitle\">" + GetLang("温馨提示：|温馨提示：|Tips:") + "</div>");
                    stringBuilder.Append("<div class=\"tip\">");
                    stringBuilder.Append(WapTool.ToWML(Error, wmlVo));
                    stringBuilder.Append("</div>");
                    if (backurl != "")
                    {
                        stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\">");
                        stringBuilder.Append("<a href=\"" + http_start + backurl + "\">" + GetLang("返回|返回|back") + "</a></div></div>");
                    }
                    if (siteid != "")
                    {
                        stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\">");
                        stringBuilder.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">" + GetLang("返回首页|返回首頁|back Index") + "</a></div></div>");
                    }
                }
                string text = ShowWEB_list(classid);
                if (text != "")
                {
                    string text2 = stringBuilder.ToString();
                    int num = text2.IndexOf("<div class=\"subtitle\">");
                    text2 = text2.Substring(num, text2.Length - num);
                    base.Response.Write(text.Replace("[view]", text2).Replace("[classname]", siteVo.sitename));
                }
                else
                {
                    stringBuilder.Append(WapTool.showDown(wmlVo));
                    base.Response.Write(stringBuilder);
                }
            }
            catch (Exception ex)
            {
                base.Response.Write("参数异常:" + WapTool.ErrorToString(ex.ToString()));
            }
            base.Response.End();
        }

        public void ShowNeedPassword(string TIP, string backurl)
        {
            if ("1".Equals(KL_WAPAdmin_NeedPassWord))
            {
                return;
            }
            backurl = backurl.Replace("needpassword=", "n=");
            backurl = ToHtm(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(WapTool.showTop(GetLang("请输入密码|请输入密码|show my password"), wmlVo));
            if (ver == "1")
            {
                stringBuilder.Append("<p>");
                stringBuilder.Append(TIP);
                stringBuilder.Append("输入密码:<br/><input type=\"password\" name=\"needpassword\"/><br/>");
                stringBuilder.Append("<anchor><go href=\"" + http_start + backurl + "\" method=\"post\" accept-charset=\"utf-8\">");
                stringBuilder.Append("<postfield name=\"needpassword\" value=\"$(needpassword)\" />");
                stringBuilder.Append("<postfield name=\"sid\" value=\"" + sid + "\" />");
                stringBuilder.Append("</go>" + GetLang("确定|确定|GO") + "</anchor>");
                stringBuilder.Append("<br/><a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">" + GetLang("返回首页|返回首頁|back Index") + "</a></p>");
            }
            else
            {
                stringBuilder.Append("<div class=\"subtitle\">" + GetLang("温馨提示：|温馨提示：|Tips:") + "</div>");
                stringBuilder.Append("<div class=\"tip\">");
                stringBuilder.Append(TIP);
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div class=\"content\">");
                backurl = backurl.Replace("&amp;", "&");
                string text = backurl.Split('?')[0];
                string text2 = backurl.Split('?')[1];
                string[] array = text2.Split('&');
                stringBuilder.Append("<form name=\"go\" action=\"" + http_start + text + "\" method=\"post\">");
                stringBuilder.Append("输入密码:<br/><input type=\"password\" name=\"needpassword\" class=\"txt\" value=\"\"/><br/>");
                try
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split('=');
                        stringBuilder.Append("<input type=\"hidden\" name=\"" + array2[0] + "\" value=\"" + array2[1] + "\" />");
                    }
                }
                catch (Exception)
                {
                }
                stringBuilder.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
                stringBuilder.Append("<input type=\"submit\" name=\"go\" class=\"btn\" value=\"确定\"/></form>");
                stringBuilder.Append("</div>");
                //stringBuilder.Append("<div class=\"line\">");
                //stringBuilder.Append("当前IP:" + IP + "，如果老是提示输入密码，看IP是否老是在变。");
                if (userVo != null && "|00|01|".IndexOf(userVo.managerlvl) > 0)
                {
                    //stringBuilder.Append("(管理员可看：此验证可在web.config中参数KL_WAPAdmin_NeedPassWord设为1关闭。)");
                }
                //stringBuilder.Append("</div>");
                stringBuilder.Append("<div class=\"btBox\"><div class=\"bt1\">");
                stringBuilder.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "\">" + GetLang("返回首页|返回首頁|back Index") + "</a></div></div>");
            }
            string text3 = ShowWEB_view(classid);
            if (text3 != "")
            {
                string text4 = stringBuilder.ToString();
                int num = text4.IndexOf("<div class=\"subtitle\">");
                text4 = text4.Substring(num, text4.Length - num);
                base.Response.Write(WapTool.ToWML(text3.ToString().Replace("[view]", text4), wmlVo));
            }
            else
            {
                stringBuilder.Append(WapTool.showDown(wmlVo));
                base.Response.Write(stringBuilder);
            }
            base.Response.End();
        }

        public string GetLang(string title)
        {
            return WapTool.GetLang(title, lang);
        }

        public string ToFilterChar(string string_10)
        {
            if (string.IsNullOrEmpty(string_10))
            {
                return "";
            }
            string text = PubConstant.GetAppString("KL_Filter_All");
            if (siteVo != null)
            {
                text = text + "|" + siteVo.CharFilter;
            }
            text = text.Replace("[", "");
            text = text.Replace("]", "");
            text = text.Replace("*", "");
            text = text.Replace("+", "");
            text = text.Replace(".", "\\.");
            string[] array = text.Split('|');
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                if (text2 != "")
                {
                    try
                    {
                        string_10 = Regex.Replace(string_10, text2, "*", RegexOptions.IgnoreCase);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return string_10;
        }

        public string ToHtm(string string_10)
        {
            bool flag = false;
            try
            {
                flag = CheckManagerLvl("04", classVo.adminusername);
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(string_10))
            {
                return "";
            }
            if (siteVo != null && !flag)
            {
                if (siteVo.sitelist == 1L)
                {
                    string_10 = string_10.Replace("[url", "[***");
                    string_10 = string_10.Replace("[anchor", "[***");
                }
                else if (siteVo.sitelist == 2L)
                {
                    string_10 = string_10.Replace("[img", "[***");
                }
                else if (siteVo.sitelist == 3L)
                {
                    string_10 = string_10.Replace("[", "*");
                    string_10 = string_10.Replace("]", "*");
                }
            }
            string_10 = string_10.Replace("\"", "“");
            string_10 = string_10.Replace("'", "‘");
            string_10 = string_10.Replace("<", "&lt;");
            string_10 = string_10.Replace(">", "&gt;");
            string_10 = string_10.Replace("\r\n", "[br]");
            string text = PubConstant.GetAppString("KL_In") + "|" + PubConstant.GetAppString("KL_Filter_All");
            if (!flag && siteVo != null)
            {
                text = text + "|" + siteVo.CharFilter;
            }
            text = text.Replace("[", "");
            text = text.Replace("]", "");
            text = text.Replace("*", "");
            text = text.Replace("+", "");
            text = text.Replace(".", "\\.");
            string[] array = text.Split('|');
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                if (text2 != "")
                {
                    try
                    {
                        string_10 = Regex.Replace(string_10, text2, "*", RegexOptions.IgnoreCase);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (!flag)
            {
                string_10 = string_10.Replace("[sid]", "[sid2]");
                string_10 = string_10.Replace("[sid1]", "[sid2]");
            }
            return string_10;
        }

        public void IsCheckManagerLvl(string string_10, string classadmin, string backurl)
        {
            bool flag = false;
            bool flag2 = false;
            IsLogin(userid, backurl);
            classadmin = "|" + classadmin + "|";
            if (classadmin.IndexOf("|" + userid + "|") >= 0)
            {
                flag = true;
            }
            string_10 = "|" + string_10 + "|";
            if (string_10.IndexOf("|" + userVo.managerlvl + "|") >= 0)
            {
                flag2 = true;
            }
            if (!flag && !flag2)
            {
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            needPassWordToAdmin();
        }

        public bool IsCheckManagerLvl(string string_10, string classadmin)
        {
            if (userid == "0")
            {
                return false;
            }
            classadmin = "|" + classadmin + "|";
            if (classadmin.IndexOf("|" + userid + "|") >= 0)
            {
                return true;
            }
            string_10 = "|" + string_10 + "|";
            if (string_10.IndexOf("|" + userVo.managerlvl + "|") >= 0)
            {
                return true;
            }
            return false;
        }

        public void IsCheckUserManager(string userid, string string_10, string classadmin, string backurl)
        {
            bool flag = false;
            bool flag2 = false;
            IsLogin(userid, backurl);
            classadmin = "|" + classadmin + "|";
            if (classadmin.IndexOf("|" + userid + "|") >= 0)
            {
                flag = true;
            }
            if (string_10 == "00" || string_10 == "01")
            {
                flag2 = true;
            }
            if (!flag && !flag2)
            {
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            needPassWordToAdmin();
        }

        public void CheckManagerLvl(string allowLvl, string classadmin, string backurl)
        {
            IsLogin(userid, backurl);
            classadmin = "|" + classadmin + "|";
            if (classadmin.IndexOf("|" + userid + "|") >= 0)
            {
                return;
            }
            if (allowLvl == "00" && userVo.managerlvl != "00")
            {
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            if (allowLvl == "01" && userVo.managerlvl != "00" && userVo.managerlvl != "01")
            {
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            if (allowLvl == "03" && userVo.managerlvl != "00" && userVo.managerlvl != "01" && userVo.managerlvl != "03")
            {
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            if (allowLvl == "04")
            {
                if (userVo.managerlvl != "00" && userVo.managerlvl != "01" && userVo.managerlvl != "03" && userVo.managerlvl != "04")
                {
                    ShowTipInfo(GetLang("帖子已删除，无法查看。|您沒有權限！|You do not have permission!"), backurl);
                }
                else if (userVo.managerlvl == "04" && base.Request.ServerVariables["URL"].ToLower().StartsWith("/bbs/") && classVo.typePath != null && "bbs/index.aspx" != classVo.typePath.ToLower())
                {
                    ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
                }
                else if (userVo.managerlvl == "04" && !base.Request.ServerVariables["URL"].ToLower().StartsWith("/bbs/"))
                {
                    ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
                }
            }
            needPassWordToAdmin();
        }

        public bool CheckManagerLvl(string allowLvl, string classadmin)
        {
            if (userid == "0")
            {
                return false;
            }
            classadmin = "|" + classadmin + "|";
            if (classadmin.IndexOf("|" + userid + "|") >= 0)
            {
                return true;
            }
            if (allowLvl == "00" && userVo.managerlvl == "00")
            {
                return true;
            }
            if (allowLvl == "01" && (userVo.managerlvl == "00" || userVo.managerlvl == "01"))
            {
                return true;
            }
            if (allowLvl == "03" && (userVo.managerlvl == "00" || userVo.managerlvl == "01" || userVo.managerlvl == "03"))
            {
                return true;
            }
            if (allowLvl == "04")
            {
                if (userVo.managerlvl == "00" || userVo.managerlvl == "01" || userVo.managerlvl == "03")
                {
                    return true;
                }
                if (userVo.managerlvl == "04" && base.Request.ServerVariables["URL"].ToLower().StartsWith("/bbs/"))
                {
                    return true;
                }
            }
            return false;
        }

        public void needPassWordToAdmin()
        {
            IsLogin(userid, "");
            VisiteCount("进入管理密码验证中!");
            string requestValue = GetRequestValue("needpassword");
            string text = "";
            if (requestValue != "")
            {
                if (KL_CHECKCODE != "" && userVo.managerlvl == "00")
                {
                    if (requestValue == KL_CHECKCODE)
                    {
                        text = PubConstant.md5(PubConstant.md5(PubConstant.md5(userid + DateTime.Now.Day)));
                        base.Response.Cookies["GET" + userid].Expires = DateTime.Now.AddHours(1.0);
                        base.Response.Cookies["GET" + userid].Value = text;
                    }
                }
                else if (PubConstant.md5(requestValue).ToLower() == userVo.password.ToLower() || (userVo.password.Length != 16 && requestValue.ToLower() == userVo.password.ToLower()))
                {
                    text = PubConstant.md5(PubConstant.md5(PubConstant.md5(userid + DateTime.Now.Day)));
                    base.Response.Cookies["GET" + userid].Expires = DateTime.Now.AddHours(1.0);
                    base.Response.Cookies["GET" + userid].Value = text;
                }
            }
            if (text == "" && base.Request.Cookies["GET" + userid] != null)
            {
                text = base.Request.Cookies["GET" + userid].Value;
            }
            if (text != PubConstant.md5(PubConstant.md5(PubConstant.md5(userid + DateTime.Now.Day))))
            {
                if (requestValue != "")
                {
                    ShowNeedPassword("<b>密码错误！</b><br/>", GetUrlQueryString());
                }
                else
                {
                    ShowNeedPassword("请输入您的密码：<br/>", GetUrlQueryString());
                }
            }
        }

        public bool IsUserManager(string userid, string string_10, string classadmin)
        {
            bool result = false;
            if (userid == "0")
            {
                result = false;
            }
            if (classadmin != "")
            {
                classadmin = "|" + classadmin + "|";
                if (classadmin.IndexOf("|" + userid + "|") >= 0)
                {
                    result = true;
                }
            }
            if (string_10 == "00" || string_10 == "01")
            {
                result = true;
            }
            return result;
        }

        public void IsLogin(string userid, string backurl)
        {
            if (userid == "0")
            {
                backurl = backurl.Replace("&amp;", "&");
                base.Response.Redirect(http_start + "waplogin.aspx?siteid=" + siteid + "&classid=" + classid + "&backurl=" + HttpUtility.UrlEncode(backurl));
            }
        }

        public void IsCheckSuperAdmin(string userid, string string_10, string backurl)
        {
            IsLogin(userid, backurl);
            if (string_10 != "00")
            {
                backurl = ToHtm(backurl);
                backurl = WapTool.URLtoWAP(backurl);
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
            needPassWordToAdmin();
        }

        public void IsCheckWebSiteAdmin(string userid, string string_10, string backurl)
        {
            IsLogin(userid, backurl);
            if (string_10 != "00" && string_10 != "01")
            {
                backurl = ToHtm(backurl);
                backurl = WapTool.URLtoWAP(backurl);
                ShowTipInfo(GetLang("您没有权限！|您沒有權限！|You do not have permission!"), backurl);
            }
        }

        public string GetUrl()
        {
            string text = "";
            text = ((!(base.Request.ServerVariables["HTTPS"] == "off")) ? "https://" : "http://");
            text += base.Request.ServerVariables["SERVER_NAME"];
            if (base.Request.ServerVariables["SERVER_PORT"] != "80")
            {
                text = text + ":" + base.Request.ServerVariables["SERVER_PORT"];
            }
            text += base.Request.ServerVariables["URL"];
            if (base.Request.QueryString != null)
            {
                text = text + "?" + base.Request.QueryString;
            }
            return text.Replace("'", "’");
        }

        public string GetUrlQueryString()
        {
            string text = base.Request.ServerVariables["URL"];
            string text2 = "";
            string text3 = "";
            text = text.ToLower().Replace("/default.aspx", "/wapindex.aspx");
            if (base.Request.QueryString != null)
            {
                text2 = base.Request.QueryString.ToString();
            }
            if (base.Request.Form != null)
            {
                NameValueCollection form = base.Request.Form;
                string[] allKeys = form.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    if (i == 0)
                    {
                        text2 += "&";
                    }
                    text2 = ((i != allKeys.Length - 1) ? (text2 + allKeys[i] + "=" + base.Request.Form[allKeys[i]] + "&") : (text2 + allKeys[i] + "=" + base.Request.Form[allKeys[i]]));
                }
            }
            if (text2 != "")
            {
                string[] array = text2.Split('&');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].IndexOf("sid=") != 0)
                    {
                        text3 = ((i != 0) ? (text3 + "&amp;" + array[i]) : array[i]);
                    }
                }
            }
            else
            {
                text = "/wapindex.aspx?siteid=" + siteid;
            }
            text = text.Substring(1, text.Length - 1);
            if (text3 != "")
            {
                if (text3.Length > 5 && text3.Substring(0, 5) == "&amp;")
                {
                    text3 = text3.Substring(5, text3.Length - 5);
                }
                text = text + "?" + text3;
            }
            return text.Replace("'", "’");
        }

        public void ClassCount()
        {
            if (classVo.classid != 0L && base.Request.ServerVariables.Get("Path_Info").ToString().IndexOf("wapindex") > 0)
            {
                MainBll.UpdateSQL("update [class] set hits=hits+1 where userid=" + siteid + " and classid=" + classVo.classid);
            }
        }

        public bool isCheckIPTime(long iptime)
        {
            if (iptime == 0L)
            {
                return false;
            }
            long num = MainBll.userActionTime(siteid, userid, IP, "ss");
            if (iptime > num)
            {
                return true;
            }
            return false;
        }

        public void VisiteCount(string userAction)
        {
            if (classid == "0" || "1".Equals(KL_VisiteCount_Detail))
            {
                string text = "";
                string userStartTime = "";
                long otherPageCount = 0L;
                string text2 = base.Request.QueryString.Get("_K");
                if (text2 == null && Session["_K"] != null)
                {
                    text2 = Session["_K"].ToString();
                }
                if (text2 != null && text2.Length > 0)
                {
                    Session["_K"] = text2;
                    try
                    {
                        text = WapTool.decodePhoneNo(text2);
                    }
                    catch
                    {
                        text = "";
                    }
                }
                if (text == "" && base.Request.ServerVariables["x-up-calling-line-id"] != null)
                {
                    text = WapTool.right(base.Request.ServerVariables["x-up-calling-line-id"].ToString(), 11);
                }
                if (Session["userStartTime"] != null)
                {
                    userStartTime = Session["userStartTime"].ToString();
                }
                else
                {
                    Session["userStartTime"] = DateTime.Now.ToString();
                }
                if (base.Application["KLCOUNT" + siteid] != null)
                {
                    otherPageCount = long.Parse(base.Application["KLCOUNT" + siteid].ToString());
                }
                string welcomeurl = WapTool.left(base.Request.ServerVariables["HTTP_REFERER"], 300);
                if (GetRequestValue("_QR") != "")
                {
                    welcomeurl = "_QR=" + GetRequestValue("_QR");
                }
                else if (Session["_QR"] != null)
                {
                    welcomeurl = "_QR=" + Session["_QR"].ToString();
                }
                MyCount.SaveCount(siteVo.siteid, 0L, userVo.userid, WapTool.left(GetUrl(), 300), welcomeurl, UA, IP, userStartTime, otherPageCount, base.Server.MapPath("/"), base.Request.ServerVariables["HTTP_HOST"], siteVo.username, text, guid, CityNameALL, classVo.classname, wmlVo.title);
                base.Application["KLCOUNT" + siteid] = "0";
            }
            else if (base.Application["KLCOUNT" + siteid] != null)
            {
                base.Application["KLCOUNT" + siteid] = long.Parse(base.Application["KLCOUNT" + siteid].ToString()) + 1L;
            }
            else
            {
                base.Application["KLCOUNT" + siteid] = 0;
            }
            try
            {
                userAction = userAction.Replace("'", "’");
                if (MainBll.isLogin(siteid, userid, IP))
                {
                    MainBll.UpdateSQL("update fcount set userid=" + userid + ",nickname='" + nickname + "',classid=" + classid + ",classname='" + userAction + "',ftime=getdate() where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    long num = MainBll.userActionTime(siteid, userid, IP, "n");
                    if (num > 0L && num <= 30L)
                    {
                        long lvLRegular = WapTool.getLvLRegular(siteVo.lvlRegular, 10);
                        MainBll.UpdateSQL("update [user] set lastLogintime=getdate(),LoginTimes=LoginTimes+" + num * 60L + ",expR=expR+" + num * lvLRegular + " where userid=" + userid);
                    }
                    if (KL_CLOSE_LOG_INFO != "1" && userAction.IndexOf("浏览") < 0 && userAction.IndexOf("进入") < 0 && userAction.IndexOf("站内信息") < 0)
                    {
                        MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',1,'" + userAction + "','" + IP + "')");
                    }
                }
                else
                {
                    MainBll.UpdateSQL("insert into fcount (fuser,fip,userid,nickname,classname,fuserid,classid) values('" + siteVo.username + "','" + IP + "'," + userid + ",'" + nickname + "','" + userAction + "'," + siteid + "," + classid + ")");
                    if (userid != "0")
                    {
                        MainBll.UpdateSQL("update [user] set LastLoginIP='" + IP + "',logintimes=logintimes+100 , LastLoginTime=getdate() where userid=" + userid);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void CheckUserView()
        {
            if (siteVo.siteRight == 1L && userVo.managerlvl == "02" && !WapTool.isUserRightPASS(userid, classid))
            {
                ShowTipInfo(GetLang("抱歉，你对应的角色没有权限浏览！|抱歉，你对应的角色没有权限浏览！|Sorry, you do not have permission to view!"), "wapindex.aspx?siteid=" + siteid + "&amp;classid=0");
            }
            string text = userVo.SessionTimeout.ToString();
            string allowUser = classVo.allowUser;
            string password = classVo.password;
            if (password != "")
            {
                VisiteCount("登录需要密码的栏目...");
                string requestValue = GetRequestValue("needpassword");
                string text2 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                if (requestValue != "" && requestValue.ToLower() == password.ToLower())
                {
                    MainBll.UpdateSQL("update fcount set SubMoneyFlag='" + text2 + "CPASS" + classid + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    text2 = text2 + "CPASS" + classid + ",";
                }
                if (text2.IndexOf("CPASS" + classid) < 0)
                {
                    if (requestValue != "")
                    {
                        ShowNeedPassword("<b>密码错误！</b><br/>", GetUrlQueryString());
                    }
                    else
                    {
                        ShowNeedPassword("此栏目需要密码访问<br/>", GetUrlQueryString());
                    }
                }
            }
            if (!(allowUser != "") && !(classVo.needMoney != ""))
            {
                return;
            }
            if (userid == "0")
            {
                ShowTipInfo("请先登录网站！<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode(GetUrlQueryString().Replace("&amp;", "&")) + "\">点击此登录</a>", "");
            }
            else
            {
                if (IsUserManager(userid, userVo.managerlvl, classVo.adminusername))
                {
                    return;
                }
                if (allowUser != "")
                {
                    allowUser = "|" + allowUser + "|";
                    text = "|" + text + "|";
                    if (allowUser.IndexOf(text) < 0 && allowUser.IndexOf(userid) < 0)
                    {
                        string text3 = "";
                        wap2_smallType_BLL wap2_smallType_BLL = new wap2_smallType_BLL(WapTool._InstanceName);
                        wap2_smallType_Model model = wap2_smallType_BLL.GetModel("siteid=" + siteid + " and systype='card_info' ");
                        if (model != null)
                        {
                            text3 = model.subclassName + "<br/>----------<br/>";
                        }
                        string appString = PubConstant.GetAppString("KL_VIPID_TIME_OUT_INFO");
                        if (appString != "")
                        {
                            ShowTipInfo("允许身份为：" + WapTool.GetCardIDNameFormID_multiple(siteid, allowUser, lang) + "<br/>我当前身份为：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + "<br/>" + appString + "<br/>----------<br/>" + text3, "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        }
                        else
                        {
                            ShowTipInfo(GetLang("抱歉，此页面您没有权限浏览！|抱歉，你沒有權限浏覽！|Sorry, you do not have permission to view!") + "<br/>允许身份为：" + WapTool.GetCardIDNameFormID_multiple(siteid, allowUser, lang) + "<br/>我当前身份为：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + "<br/><a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime, lang) + "</a>。<br/>----------<br/>" + text3, "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        }
                    }
                    else if (WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) < 1L)
                    {
                        string appString = PubConstant.GetAppString("KL_VIPID_TIME_OUT_INFO");
                        if (appString != "")
                        {
                            ShowTipInfo(GetLang("抱歉，你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，" + appString + "|抱歉，你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，请<a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">点击此充值</a>！|Sorry, 你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，请<a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">点击此充值</a>!"), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        }
                        else
                        {
                            ShowTipInfo(GetLang("抱歉，你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，请<a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">点击此充值</a>！|抱歉，你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，请<a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">点击此充值</a>！|Sorry, 你身份已过期：" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime) + "天，请<a href=\"" + http_start + "bbs/toGroupInfo.aspx?siteid=" + siteid + "\">点击此充值</a>!"), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        }
                    }
                }
                if (classVo.needMoney != "" && WapTool.IsNumeric(classVo.needMoney) && userVo.money < long.Parse(classVo.needMoney))
                {
                    ShowTipInfo(GetLang("抱歉，需要" + classVo.needMoney + "币查看，你只有" + userVo.money + "！|抱歉，需要" + classVo.needMoney + "币查看，你只有" + userVo.money + "！|Sorry，need " + classVo.needMoney + "money,But you have" + userVo.money + "!"), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                }
            }
        }

        public void CheckUserViewSubMoney(string idFlag, string backurl, string PreURL)
        {
            if (!WapTool.IsNumeric(classVo.subMoney) || "0" == classVo.subMoney)
            {
                return;
            }
            IsLogin(userid, backurl);
            string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
            if (fcountSubMoneyFlag.IndexOf(idFlag) < 0)
            {
                if (userVo.money < long.Parse(classVo.subMoney))
                {
                    ShowTipInfo("你只有:" + userVo.money + "个币！查看此内容需要" + classVo.subMoney + "个。", PreURL);
                    return;
                }
                MainBll.UpdateSQL("update [user] set money=money-" + classVo.subMoney + " where siteid=" + siteid + " and userid=" + userid);
                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + idFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                SaveBankLog(userid, "内容扣币", "-" + classVo.subMoney.ToString(), userid, nickname, "内容标识[" + idFlag + "]");
            }
        }

        public void addMoneyToMyBank()
        {
            bool sendInterestMessage = false; // 控制是否发送利息消息和产生日志
            if (userVo.dtimes >= 30L)
            {
                long lvLRegular = WapTool.getLvLRegular(siteVo.lvlRegular, 7);
                long num = userVo.dtimes / 30L;
                long num2 = userVo.dtimes % 30L;
                long num3 = Convert.ToInt64(userVo.myBankMoney) * Convert.ToInt64(lvLRegular) * num / 100L;
                long num4 = Convert.ToInt64(userVo.myBankMoney) + num3;
                MainBll.UpdateSQL("update [user] set mybankmoney=" + num4 + ",mybanktime=getdate()-" + num2.ToString() + " where siteid=" + siteid + " and  userid=" + userid);
                string text = "您的银行月结息:" + num + "个月，利息:" + num3;
                string text2 = "执行时间" + DateTime.Now;
                if (userVo.myBankMoney != 0L)
                {
                    SaveBankLog(userid, "银行利息", num3.ToString(), siteid, "系统", "银行月结息");
                    MainBll.UpdateSQL("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(" + siteid + "," + siteid + ",'" + siteVo.nickname + "','" + text + "','" + text2 + "'," + userid + ",1)");
                }
            }
        }

        public void CheckFunction(string string_10, long page)
        {
            if (method_1().IndexOf(string_10) < 0)
            {
                ShowTipInfo("<!--listS-->" + GetLang("你无购买此功能！|無此功能|No such function") + "<!--listE-->", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            else if (page > 2L && KL_ISREG == "0")
            {
                ShowTipInfo("<!--listS-->抱歉，免费版限制了此功能，解除限制请登录柯林官方网站购买正版柯林WAP建站系统:<a href=\"http://kelink.com\">kelink.com</a><!--listE-->", "");
            }
        }

        public void ResponseFile(string realPath, string fileExt)
        {
            Stream stream = null;
            int num = 10240;
            byte[] buffer = new byte[10240];
            long num2 = 0L;
            string appString = PubConstant.GetAppString("KL_NotDownAndUpload");
            long num3 = long.Parse(PubConstant.GetAppString("KL_DownSpeed"));
            int num4 = int.Parse(PubConstant.GetAppString("KL_DownThread"));
            string text = Path.GetFileName(realPath);
            fileExt = fileExt.Replace(".", "");
            if (fileExt == "")
            {
                ShowTipInfo("扩展名为空！", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            else if (appString.IndexOf(fileExt.ToLower()) >= 0)
            {
                ShowTipInfo(fileExt + "格式不允许下载！", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            try
            {
                long length;
                if (num4 == 0)
                {
                    stream = new FileStream(realPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryReader binaryReader = new BinaryReader(stream);
                    length = stream.Length;
                    int num5 = (int)Math.Ceiling(1000.0 * (double)num / (double)num3);
                    base.Response.Clear();
                    base.Response.ClearHeaders();
                    base.Response.Buffer = false;
                    if (base.Request.Headers["Range"] != null)
                    {
                        base.Response.StatusCode = 206;
                        num2 = long.Parse(base.Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                    }
                    if (num2 != 0L)
                    {
                        base.Response.AddHeader("Content-Range", $"bytes {num2}-{length - 1L}/{length}");
                    }
                    base.Response.ContentType = WapTool.GetMine(fileExt);
                    base.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("[" + base.Request.ServerVariables["HTTP_HOST"] + "]" + text));
                    base.Response.AddHeader("Content-Length", length.ToString());
                    base.Response.AddHeader("Accept-Ranges", "bytes");
                    base.Response.ContentEncoding = Encoding.UTF8;
                    stream.Seek(num2, SeekOrigin.Begin);
                    int num6 = (int)Math.Ceiling(((double)(length - num2) + 0.0) / (double)num);
                    for (int i = 0; i < num6; i++)
                    {
                        if (!base.Response.IsClientConnected)
                        {
                            break;
                        }
                        base.Response.BinaryWrite(binaryReader.ReadBytes(num));
                        base.Response.Flush();
                        if (num5 > 0)
                        {
                            Thread.Sleep(num5);
                        }
                    }
                    return;
                }
                string arryString = WapTool.getArryString(siteVo.Version, '|', 24);
                if (!"1".Equals(arryString))
                {
                    if ("2".Equals(arryString))
                    {
                        text = HttpUtility.UrlEncode(text);
                    }
                    else if (WapTool.isFileNameURLencode(UA))
                    {
                        text = HttpUtility.UrlEncode(text);
                    }
                }
                if ("1".Equals(WapTool.getArryString(siteVo.Version, '|', 23)))
                {
                    text = base.Request.ServerVariables["HTTP_HOST"] + "_" + text;
                }
                stream = new FileStream(realPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                length = stream.Length;
                base.Response.ContentType = WapTool.GetMine(fileExt);
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + text);
                base.Response.AddHeader("Content-Length", length.ToString());
                while (length > 0L)
                {
                    if (base.Response.IsClientConnected)
                    {
                        int num7 = stream.Read(buffer, 0, 10000);
                        base.Response.OutputStream.Write(buffer, 0, num7);
                        base.Response.Flush();
                        buffer = new byte[10000];
                        length -= num7;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowTipInfo("文件下载时出现错误：<br/>" + WapTool.ErrorToString(ex.ToString()), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            finally
            {
                stream?.Close();
            }
        }

        public static void setDefaultdn()
        {
            d = "";
        }

        private static void smethod_0()
        {
            if (!(d == ""))
            {
                return;
            }
            m_c = "";
            DataSet dataSet = DbHelperSQL.ExecuteDataset(PubConstant.GetConnectionString(PubConstant.GetAppString("InstanceName")), CommandType.Text, "select id,domain from domainname ");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if ("1".Equals(row["id"].ToString()))
                {
                    d = row["domain"].ToString().ToLower();
                }
                else
                {
                    m_c = m_c + row["domain"].ToString().ToLower() + "|";
                }
            }
        }

        private string method_0()
        {
            if (m_a == "")
            {
                m_a = WapTool.GetMobieleUAInfo(GetRegUsername());
            }
            return m_a;
        }

        public static void setDefaultRG()
        {
            m_a = "";
        }

        private string method_1()
        {
            if (m_b == "")
            {
                m_b = WapTool.DesDecrypt(WapTool.GetFunction()).ToLower();
            }
            return m_b;
        }

        public string GetRegUsername()
        {
            string text = "";
            text += WapTool.GetDomain();
            text = text.Trim().Replace(".", "0");
            text = text.ToUpper();
            text = WapTool.EnCode_KL(text);
            text = WapTool.EnCode_KL(text);
            text = WapTool.EnCode_KL(text);
            text = WapTool.EnCode_KL(text);
            return WapTool.EnCode_KL(text);
        }

        public void DeleteFile(string directory, string path, string backurl)
        {
            string text = base.Server.MapPath("/");
            if (!text.EndsWith("\\"))
            {
                text += "\\";
            }
            if (directory != "")
            {
                text = text + directory + "\\";
            }
            string[] array = path.Split('|');
            string text2 = "";
            string text3 = "";
            int num = 0;
            int num2 = 0;
            while (num == 0 && num2 < array.Length)
            {
                text2 = text + array[num2];
                try
                {
                    if (array[num2].ToLower().IndexOf("http://") < 0 && !array[num2].StartsWith("/") && array[num2].IndexOf(".") > 0 && File.Exists(text2))
                    {
                        string[] array2 = array[num2].Split('.');
                        text3 = array2[array2.Length - 1].ToLower();
                        string appString = PubConstant.GetAppString("KL_NotDownAndUpload");
                        if (appString.ToLower().IndexOf(text3) >= 0)
                        {
                            num = 1;
                            break;
                        }
                        if (userVo.managerlvl != "00" && array[num2].IndexOf("/" + siteid + "/") < 0)
                        {
                            num = 2;
                            break;
                        }
                        File.Delete(text2);
                        text2 = "";
                    }
                }
                catch (Exception ex)
                {
                    ShowTipInfo(WapTool.ErrorToString(ex.ToString()), backurl);
                }
                num2++;
            }
            if (userVo.managerlvl != "00" && num == 1)
            {
                ShowTipInfo("此扩展名" + text3 + "文件不允许删除！", backurl);
            }
        }

        public void KL_CheckSql()
        {
            string appString = PubConstant.GetAppString("KL_In");
            StringBuilder stringBuilder = new StringBuilder();
            string[] array = appString.Split('|');
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (HttpContext.Current.Request.Form != null && text.Trim() != "")
                {
                    for (int j = 0; j < HttpContext.Current.Request.Form.Count; j++)
                    {
                        string name = HttpContext.Current.Request.Form.Keys[j];
                        if (HttpContext.Current.Request.Form[name].ToLower().Contains(text))
                        {
                            stringBuilder.Append("防注入程序提示您，以下字符被拦截，请在字符中加入空格后再提交！<br/>");
                            stringBuilder.Append("操 作 I  P ：" + IP + "<br/>");
                            stringBuilder.Append("操 作 时 间：" + DateTime.Now.ToString() + "<br/>");
                            stringBuilder.Append("操 作 页 面：" + HttpContext.Current.Request.ServerVariables["URL"].Replace("&", "&amp;") + "<br/>");
                            stringBuilder.Append("提 交 方 式：P O S T <br/>");
                            stringBuilder.Append("提 交 参 数：" + text + "<br/>");
                            ShowTipInfo(stringBuilder.ToString(), "");
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                if (HttpContext.Current.Request.QueryString != null && text.Trim() != "")
                {
                    for (int j = 0; j < HttpContext.Current.Request.QueryString.Count; j++)
                    {
                        string name = HttpContext.Current.Request.QueryString.Keys[j];
                        if (HttpContext.Current.Request.QueryString[name].ToLower().Contains(text))
                        {
                            stringBuilder.Append("防注入程序提示您，以下字符被拦截，请在字符中加入空格后再提交！<br/>");
                            stringBuilder.Append("操 作 I  P ：" + IP + "<br/>");
                            stringBuilder.Append("操 作 时 间：" + DateTime.Now.ToString() + "<br/>");
                            stringBuilder.Append("操 作 页 面：" + HttpContext.Current.Request.ServerVariables["URL"].Replace("&", "&amp;") + "<br/>");
                            stringBuilder.Append("提 交 方 式：G E T <br/>");
                            stringBuilder.Append("提 交 参 数：" + text + "<br/>");
                            ShowTipInfo(stringBuilder.ToString(), "");
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                if (HttpContext.Current.Request.Cookies == null || !(text.Trim() != ""))
                {
                    continue;
                }
                for (int j = 0; j < HttpContext.Current.Request.Cookies.Count; j++)
                {
                    string name = HttpContext.Current.Request.Cookies.Keys[j];
                    if (HttpContext.Current.Request.Cookies[name].Value.ToLower().Contains(text))
                    {
                        stringBuilder.Append("防注入程序提示您，以下字符被拦截，请在字符中加入空格后再提交！<br/>");
                        stringBuilder.Append("操 作 I  P ：" + IP + "<br/>");
                        stringBuilder.Append("操 作 时 间：" + DateTime.Now.ToString() + "<br/>");
                        stringBuilder.Append("操 作 页 面：" + HttpContext.Current.Request.ServerVariables["URL"].Replace("&", "&amp;") + "<br/>");
                        stringBuilder.Append("提 交 方 式： Cookies <br/>");
                        stringBuilder.Append("提 交 参 数：" + text + "<br/>");
                        ShowTipInfo(stringBuilder.ToString(), "");
                        HttpContext.Current.Response.End();
                    }
                }
            }
        }

        public string ShowWEB_list(string toclassid)
        {
            if (KL_Open_Web == "1" && (ver == "3" || ver == "4"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                wap3_htmlContent_BLL wap3_htmlContent_BLL = new wap3_htmlContent_BLL(PubConstant.GetAppString("InstanceName"));
                wap3_htmlContent_Model wap3_htmlContent_Model = new wap3_htmlContent_Model();
                wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), long.Parse(toclassid));
                if (wap3_htmlContent_Model != null)
                {
                    if (ver == "3")
                    {
                        if (toclassid == "0")
                        {
                            if (wap3_htmlContent_Model.html3_3 == "" || wap3_htmlContent_Model.html3_3 == "<P>&nbsp;</P>")
                            {
                                return "";
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_3.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_3);
                        }
                        else
                        {
                            if (wap3_htmlContent_Model.html3_2 == "" || wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")
                            {
                                return "";
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
                        }
                    }
                    else if (toclassid == "0")
                    {
                        if (wap3_htmlContent_Model.html4_3 == "" || wap3_htmlContent_Model.html4_3 == "<P>&nbsp;</P>")
                        {
                            return "";
                        }
                        StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                        stringBuilder.Append(wap3_htmlContent_Model.config4_3.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                        stringBuilder.Append(wap3_htmlContent_Model.html4_3);
                    }
                    else
                    {
                        if (wap3_htmlContent_Model.html4_2 == "" || wap3_htmlContent_Model.html4_2 == "<P>&nbsp;</P>")
                        {
                            return "";
                        }
                        StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                        stringBuilder.Append(wap3_htmlContent_Model.config4_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                        stringBuilder.Append(wap3_htmlContent_Model.html4_2);
                    }
                }
                if (stringBuilder.ToString() != "")
                {
                    if (stringBuilder.ToString().IndexOf("</body>") < 0)
                    {
                        stringBuilder.Append("</body></html>");
                    }
                    return stringBuilder.ToString();
                }
            }
            return "";
        }

        public string ShowWEB_view(string toclassid)
        {
            if (KL_Open_Web == "1" && (ver == "3" || ver == "4"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                wap3_htmlContent_BLL wap3_htmlContent_BLL = new wap3_htmlContent_BLL(PubConstant.GetAppString("InstanceName"));
                wap3_htmlContent_Model wap3_htmlContent_Model = new wap3_htmlContent_Model();
                wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), long.Parse(toclassid));
                if (wap3_htmlContent_Model != null)
                {
                    if (ver == "3")
                    {
                        if (wap3_htmlContent_Model.html3_3 == "" || wap3_htmlContent_Model.html3_3 == "<P>&nbsp;</P>")
                        {
                            return "";
                        }
                        StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                        string text = "";
                        if (wmlVo.timer != null && wmlVo.timer != "")
                        {
                            string text2 = wmlVo.strUrl;
                            if (!text2.StartsWith("http://"))
                            {
                                text2 = wmlVo.http_start + wmlVo.strUrl;
                            }
                            text = "<meta http-equiv=\"refresh\" content=\"" + wmlVo.timer + ";url=" + text2 + "\" />\n";
                        }
                        stringBuilder.Append(wap3_htmlContent_Model.config3_3.Replace("</head>", string.Concat(siteCSS_WEB, text, "</head>")));
                        stringBuilder.Append(wap3_htmlContent_Model.html3_3);
                    }
                    else
                    {
                        if (wap3_htmlContent_Model.html4_3 == "" || wap3_htmlContent_Model.html4_3 == "<P>&nbsp;</P>")
                        {
                            return "";
                        }
                        StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                        string text = "";
                        if (wmlVo.timer != null && wmlVo.timer != "")
                        {
                            string text2 = wmlVo.strUrl;
                            if (!text2.StartsWith("http://"))
                            {
                                text2 = wmlVo.http_start + wmlVo.strUrl;
                            }
                            text = "<meta http-equiv=\"refresh\" content=\"" + wmlVo.timer + ";url=" + text2 + "\" />\n";
                        }
                        stringBuilder.Append(wap3_htmlContent_Model.config4_3.Replace("</head>", string.Concat(siteCSS_WEB, text, "</head>")));
                        stringBuilder.Append(wap3_htmlContent_Model.html4_3);
                    }
                }
                if (stringBuilder.ToString() != "")
                {
                    if (stringBuilder.ToString().IndexOf("</body>") < 0)
                    {
                        stringBuilder.Append("</body></html>");
                    }
                    return stringBuilder.ToString();
                }
            }
            return "";
        }

        public static string GetIP_FormCDN()
        {
            string empty = string.Empty;
            empty = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(empty))
            {
                if (empty.IndexOf(".") == -1)
                {
                    empty = null;
                }
                else if (empty.IndexOf(",") != -1)
                {
                    empty = empty.Replace("  ", "").Replace("'", "");
                    string[] array = empty.Split(",;".ToCharArray());
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (IsIP(array[i]) && array[i].Substring(0, 3) != "10." && array[i].Substring(0, 7) != "192.168" && array[i].Substring(0, 7) != "172.16.")
                        {
                            empty = array[i];
                        }
                    }
                    string[] array2 = empty.Split(',');
                    if (array2.Length > 0)
                    {
                        empty = array2[0].ToString().Trim();
                    }
                }
                else if (IsIP(empty))
                {
                    return empty;
                }
            }
            if (string.IsNullOrEmpty(empty))
            {
                empty = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(empty))
            {
                empty = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(empty))
            {
                empty = "127.0.0.1";
            }
            return empty;
        }

        public static bool IsIP(string string_10)
        {
            return Regex.IsMatch(string_10, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        public void SaveBankLog(string local_userid, string actionName, string money, string opera_userid, string opera_nickname, string remark)
        {
            actionName = WapTool.left(actionName, 10);
            remark = WapTool.left(remark, 200);
            MainBll.UpdateSQL("insert into wap_bankLog select  '" + siteid + "','" + local_userid + "','" + actionName + "','" + money + "',money,'" + opera_userid + "','" + opera_nickname + "','" + remark + "','" + IP + "',getdate() ,null from [user] where userid=" + local_userid + " and siteid=" + siteid);
        }

        public void SaveRMBLog(string local_userid, string actionName_ID, string money, string opera_userid, string opera_nickname, string remark, string OrderID)
        {
            remark = WapTool.left(remark, 200);
            MainBll.UpdateSQL("insert into chinabank_wap_order select  '" + siteid + "'," + local_userid + ",'" + OrderID + "',convert(money,'" + money + "'),'','','','1','" + remark + "',getdate(),'" + actionName_ID + "',null,convert(money, rmb),'" + opera_userid + "','" + opera_nickname + "','" + IP + "' from [user] where userid=" + local_userid + " and siteid=" + siteid);
        }

        public void SaveMessage(string from_userid, string from_nickname, string title, string content, string to_userid, string to_nickname, int issystem)
        {
            MainBll.UpdateSQL("insert into wap_message (siteid,userid,nickname,title,content,touserid,tonickname,isnew,issystem,addtime)values(" + siteid + "," + from_userid + ",'" + from_nickname + "','" + title + "','" + content + "'," + to_userid + ",'" + to_nickname + "',1," + issystem + ",getdate())");
        }

        public void Action_user_doit(int actionID)
        {
            if (userid == "0" || $"{userVo.actionTime:yyyy-MM-dd}" != $"{DateTime.Now:yyyy-MM-dd}")
            {
                return;
            }
            MainBll.UpdateSQL("update wap_action set numFinish=numFinish+1,state=(case when (numFinish+1 )>=num then 1  else 0 end)  where siteid=" + siteid + " and userid=" + userid + " and issystem=0 and actionID=" + actionID);
            if (userVo.actionState == "0")
            {
                wap_action_BLL wap_action_BLL = new wap_action_BLL(WapTool._InstanceName);
                long num = wap_action_BLL.GetListCount(" siteid=" + siteid + " and userid=" + userid + " and issystem=0 and state=1 ");
                string text = WapTool.getArryString(siteVo.Version, '|', 36);
                string text2 = WapTool.getArryString(siteVo.Version, '|', 37);
                string text3 = WapTool.getArryString(siteVo.Version, '|', 38);
                if (!WapTool.IsNumeric(text))
                {
                    text = "0";
                }
                if (!WapTool.IsNumeric(text2))
                {
                    text2 = "0";
                }
                if (!WapTool.IsNumeric(text3))
                {
                    text3 = "0";
                }
                if ((text != "0") & (text == num.ToString()))
                {
                    MainBll.UpdateSQL("update [user] set [money]=([money]+" + text2 + "),expR=expR+" + text3 + ",actionstate=1 where siteid=" + siteid + " and userid=" + userid);
                    SaveBankLog(userid, "完成任务", text2, userid, nickname, string.Concat("完成时间[", DateTime.Now, "]"));
                    ShowTipInfo("恭喜您今天完成" + text + "个任务，送" + siteVo.sitemoneyname + ":" + text2 + "个；送经验:" + text3 + "个。", "");
                }
            }
        }

        public void AutoXunZhang()
        {
            if (userVo.userid != 0L)
            {
                string xunZhangAutoPic = WapTool.GetXunZhangAutoPic(e[42], e[43], userVo.money);
                if (xunZhangAutoPic != "" && userVo.moneyname.IndexOf(xunZhangAutoPic) < 0)
                {
                    userVo.moneyname = userVo.moneyname + "|" + ToHtm(xunZhangAutoPic);
                    MainBll.UpdateSQL("update [user] set moneyname='" + userVo.moneyname + "' where siteid=" + siteid + " and userid=" + userVo.userid);
                }
                string xunZhangAutoPic2 = WapTool.GetXunZhangAutoPic(e[44], e[45], userVo.expr);
                if (xunZhangAutoPic2 != "" && userVo.moneyname.IndexOf(xunZhangAutoPic2) < 0)
                {
                    userVo.moneyname = userVo.moneyname + "|" + ToHtm(xunZhangAutoPic2);
                    MainBll.UpdateSQL("update [user] set moneyname='" + userVo.moneyname + "' where siteid=" + siteid + " and userid=" + userVo.userid);
                }
            }
        }

        public string GetShopCardList()
        {
            if (Session["bycarlistVo_count"] != null)
            {
                return Session["bycarlistVo_count"].ToString();
            }
            return "0";
        }
    }
}
