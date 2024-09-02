using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin
{
    public class WapIndex : MyPageWap
    {
        public List<class_Model> classList = new List<class_Model>();

        public StringBuilder strHtml = new StringBuilder();

        public string strShowHtml = "";

        /// <summary>
        /// 测试代码
        /// </summary>
        public void TestCode()
        {
            //配置文件 KL_License 值存储授权信息
            //解密数据
            var d1 = "se9v+6Rln020DWhvmJTy8HLJQXBmYp26wE2s3a8s87TKDfh1ytd76lFsmnOrqbgArhglXIMdr8gfVDa7vWoUdse+d5HcbOjRDmwFbp8OHPw=";
            var d1d = WapTool.DesDecrypt(d1);
            var d1dd = WapTool.GetSiteDefault(d1d, 3);
            //加密数据
            var d2 = "0|2|1000|kelink.com|art|bbs|pic|dow|rin|vid|wml|sho|gue|cha|lin|adl|";
            var d2d = WapTool.DesEncrypt(d2);
            //版权去除
            //1、domainname 表的 domain 改为自己的域名
            //2、user 表的 version 有底部信息
            //3、配置文件 KL_PAGE_DOWN 值改为空格
            //
            var stringBuilder = new StringBuilder();
            var text12 = WapTool.DesDecrypt(WapTool.GetFunction()).ToLower();
            stringBuilder.Append("<b>内核版本</b>:10.2014.12.18<br/>");
            stringBuilder.Append("<b>机器码域名</b>:" + WapTool.GetDomain() + "<br/>");
            stringBuilder.Append("<b>授权版本</b>:" + WapTool.GetSystemVersion(this.KL_VERSION, "0") + "<br/>");
            stringBuilder.Append("<b>授权ＩＤ</b>:" + WapTool.GetSiteDefault(text12, 0) + "<br/>");
            stringBuilder.Append("<b>授权域名</b>:" + WapTool.GetSiteDefault(text12, 3) + "<br/>");
            var text13 = this.KL_ISREG;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //测试代码
            //TestCode();

            if (classid == "0")
            {
                classVo.childid = 0L;
                classVo.classname = siteVo.sitename;
                classVo.position = siteVo.siteposition;
                classVo.siteimg = siteVo.siteimg;
                classVo.introduce = siteVo.siteuptip;
                classVo.sitedowntip = siteVo.sitedowntip;
                classVo.siterowremark = siteVo.siterowremark;
                classVo.sitelist = siteVo.sitelistflag;
            }
            if ("1".Equals(WapTool.KL_OpenCache))
            {
                WapTool.DataClassArray.TryGetValue(siteid + classid, out classList);
                if (classList == null)
                {
                    classList = WapTool.GetChildList(long.Parse(siteid), long.Parse(classid));
                    try
                    {
                        WapTool.DataClassArray.Add(siteid + classid, classList);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                classList = WapTool.GetChildList(long.Parse(siteid), long.Parse(classid));
            }
            string text = ShowWEB_index();
            if (ver == "1")
            {
                strHtml.Append("<p align=\"" + classVo.position + "\">");
            }
            if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
            {
                strHtml.Append("[div=logo]<img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/>[/div]");
            }
            if (classVo.sitelist == 1L)
            {
                strHtml.Append("<div class=\"btBox\">");
                strHtml.Append("<div class=\"bt1\">");
                string str = "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid;
                if (classid == "0")
                {
                    str = "myfile.aspx?siteid=" + siteid;
                }
                if (userVo != null)
                {
                    if (WapTool.ISAPI_Rewrite3_Open == "1")
                    {
                        strHtml.Append("<a href=\"" + http_start + "myfile-" + siteid + "-" + classid + ".html\">[hello]" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver, userVo.endTime) + "</a>");
                    }
                    else
                    {
                        strHtml.Append("<a href=\"" + http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">[hello]" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver, userVo.endTime) + "</a>");
                    }
                }
                else if (WapTool.ISAPI_Rewrite3_Open == "1")
                {
                    strHtml.Append("<a href=\"" + http_start + "myfile-" + siteid + "-" + classid + ".html\">[hello]" + nickname + "</a>");
                }
                else
                {
                    strHtml.Append("<a href=\"" + http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">[hello]" + nickname + "</a>");
                }
                strHtml.Append("</div>");
                if (userid == "0")
                {
                    strHtml.Append("<div class=\"bt5\">");
                    if (WapTool.ISAPI_Rewrite3_Open == "1")
                    {
                        strHtml.Append("<a href=\"" + http_start + "waplogin-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode(str) + "\">" + GetLang("登录|登錄|Login") + "</a> <a href=\"" + http_start + "wapreg-" + siteid + "-" + classid + ".html\">" + GetLang("注册|注冊|register") + "</a> <a href=\"" + http_start + "wapstyle/skin-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("皮肤|皮膚|Skin") + "</a> <a href=\"" + http_start + "wapstyle/lang-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("语言|語言|Language") + "</a> <a href=\"" + http_start + "wapstyle/mobileua-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("机型|機型|MobileAgent") + "</a>");
                    }
                    else
                    {
                        strHtml.Append("<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;backurl=" + HttpUtility.UrlEncode(str) + "\">" + GetLang("登录|登錄|Login") + "</a> <a href=\"" + http_start + "wapreg.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">" + GetLang("注册|注冊|register") + "</a> <a href=\"" + http_start + "wapstyle/skin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("皮肤|皮膚|Skin") + "</a> <a href=\"" + http_start + "wapstyle/lang.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("语言|語言|Language") + "</a> <a href=\"" + http_start + "wapstyle/mobileua.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("机型|機型|MobileAgent") + "</a>");
                    }
                    strHtml.Append("</div>");
                }
                strHtml.Append("</div>");
            }
            if (ver == "1" && classVo.introduce != "" && !classVo.introduce.StartsWith("[div"))
            {
                strHtml.Append("<br/>");
            }
            strHtml.Append(classVo.introduce);
            int num = 0;
            while (classList != null && num < classList.Count)
            {
                if (num == 0 && ver == "1")
                {
                    strHtml.Append("<br/>");
                }
                strHtml.Append(classList[num].siterowremark);
                if (classList[num].ishidden == 0L)
                {
                    if (WapTool.ISAPI_Rewrite3_Open == "1")
                    {
                        strHtml.Append("<a href=\"" + http_start + "wapindex-" + siteid + "-" + classList[num].classid + ".html?path=" + classList[num].typePath + "\">" + classList[num].classname + "</a>");
                    }
                    else
                    {
                        strHtml.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classList[num].classid + "&amp;path=" + classList[num].typePath + "\">" + classList[num].classname + "</a>");
                    }
                }
                num++;
            }
            if (classList == null && classid != "0")
            {
                if (classVo.typePath.ToLower() != "null/index.asp" && classVo.typePath.ToLower() != "null/index.aspx")
                {
                    getPath(classVo.typePath);
                    return;
                }
                CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            if (ver == "1")
            {
                strHtml.Append("<br/>");
            }
            strHtml.Append(classVo.sitedowntip);
            if (!(classid != "0"))
            {
            }
            if (ver == "1")
            {
                strHtml.Append("</p>");
            }
            if (text != "")
            {
                strShowHtml = WapTool.ToWML(text.Replace("[view]", strHtml.ToString()), wmlVo);
            }
            else
            {
                strShowHtml = WapTool.showTop(classVo.classname, wmlVo).ToString();
                strShowHtml += WapTool.ToWML(strHtml.ToString(), wmlVo);
                strShowHtml += WapTool.showDown(wmlVo);
            }
            VisiteCount("正在浏览:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
        }

        public void getPath(string ver0)
        {
            ver0 = ver0.Trim().ToLower();
            if (ver0.StartsWith("http://"))
            {
                ver0 = ver0.Replace("[sid]", sid);
                ver0 = ver0.Replace("[siteid]", siteid);
                ver0 = ver0.Replace("[classid]", classid);
                base.Response.Redirect(ver0.Replace("&amp;", "&"));
            }
            else if (ver0.EndsWith(".aspx"))
            {
                base.Server.Transfer("/" + ver0 + "?siteid=" + siteid + "&classid=" + classid);
            }
            else
            {
                base.Response.Redirect(http_start + ver0 + "?siteid=" + siteid + "&classid=" + classid + "&sid=" + sid);
            }
        }

        public string ShowWEB_index()
        {
            if (PageWap.KL_Open_Web == "1" && (ver == "3" || ver == "4"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                wap3_htmlContent_BLL wap3_htmlContent_BLL = new wap3_htmlContent_BLL(PubConstant.GetAppString("InstanceName"));
                wap3_htmlContent_Model wap3_htmlContent_Model = new wap3_htmlContent_Model();
                if (classid == "0")
                {
                    wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), 0L);
                    if (wap3_htmlContent_Model != null)
                    {
                        if (ver == "3")
                        {
                            if (wap3_htmlContent_Model.html3_2 == "" || wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")
                            {
                                return "";
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
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
                }
                else if (classList == null)
                {
                    if (classid != "0")
                    {
                        if (!(classVo.typePath.ToLower() == "null/index.asp") && !(classVo.typePath.ToLower() == "null/index.aspx"))
                        {
                            getPath(classVo.typePath);
                            return "";
                        }
                        CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), classVo.classid);
                        if (wap3_htmlContent_Model != null)
                        {
                            if (ver == "3")
                            {
                                if (wap3_htmlContent_Model.html3_2 == "" || wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")
                                {
                                    return "";
                                }
                                StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                                stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                                stringBuilder.Append(wap3_htmlContent_Model.html3_2);
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
                    }
                }
                else if (classid != "0" && (classVo.typePath.ToLower() == "null/index.asp" || classVo.typePath.ToLower() == "null/index.aspx"))
                {
                    CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                    wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), classVo.classid);
                    if (wap3_htmlContent_Model != null)
                    {
                        if (ver == "3")
                        {
                            if (wap3_htmlContent_Model.html3_2 == "" || wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")
                            {
                                return "";
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
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
    }
}