using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;

public class WapIndex : PageWap
{
    public List<class_Model> classList = new List<class_Model>();

    public StringBuilder strHtml = new StringBuilder();

    public string strShowHtml = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_0723
        int num4 = default(int);
        string str = default(string);
        string text = default(string);
        while (true)
        {
            bool flag = !(classid == "0");
            int num = 68;
            while (true)
            {
                int num3;
                int num7;
                int num5;
                int num2;
                int num6;
                switch (num)
                {
                    case 68:
                        num = (flag ? 101 : 13);
                        continue;
                    case 92:
                        num = 24;
                        continue;
                    case 24:
                        num3 = ((!(classVo.siteimg != "NetImages/no.gif")) ? 1 : 0);
                        goto IL_082c;
                    case 34:
                        flag = !(ver == "1");
                        num = 37;
                        continue;
                    case 37:
                        if (!flag)
                        {
                            num = 53;
                            continue;
                        }
                        goto IL_15de;
                    case 6:
                        strHtml.Append("</p>");
                        num = 87;
                        continue;
                    case 1:
                        num = 10;
                        continue;
                    case 19:
                        num = 42;
                        continue;
                    case 42:
                        num7 = ((!(classVo.typePath.ToLower() != "null/index.aspx")) ? 1 : 0);
                        goto IL_1286;
                    case 97:
                        num = 48;
                        continue;
                    case 48:
                        num5 = ((!(ver == "1")) ? 1 : 0);
                        goto IL_1673;
                    case 11:
                        strHtml.Append(classVo.introduce);
                        num4 = 0;
                        num = 52;
                        continue;
                    case 46:
                    case 79:
                        strHtml.Append("</div>");
                        num = 72;
                        continue;
                    case 50:
                        num4++;
                        num = 0;
                        continue;
                    case 77:
                        num = 7;
                        continue;
                    case 7:
                        num2 = (classVo.introduce.StartsWith("[div") ? 1 : 0);
                        goto IL_169d;
                    case 9:
                        strHtml.Append("<div class=\"btBox\">");
                        strHtml.Append("<div class=\"bt1\">");
                        str = "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid;
                        flag = !(classid == "0");
                        num = 82;
                        continue;
                    case 82:
                        if (!flag)
                        {
                            num = 5;
                            continue;
                        }
                        goto case 2;
                    case 78:
                        flag = !(classid != "0");
                        num = 89;
                        continue;
                    case 89:
                        if (!flag)
                        {
                            num = 73;
                            continue;
                        }
                        goto case 30;
                    case 21:
                        num = 83;
                        continue;
                    case 83:
                        num = ((!(classVo.siteimg != "UploadFiles/no.gif")) ? 84 : 92);
                        continue;
                    case 27:
                        flag = classList != null;
                        num = 22;
                        continue;
                    case 22:
                        if (!flag)
                        {
                            num = 78;
                            continue;
                        }
                        goto case 34;
                    case 13:
                        classVo.childid = 0L;
                        classVo.classname = siteVo.sitename;
                        classVo.position = siteVo.siteposition;
                        classVo.siteimg = siteVo.siteimg;
                        classVo.introduce = siteVo.siteuptip;
                        classVo.sitedowntip = siteVo.sitedowntip;
                        classVo.siterowremark = siteVo.siterowremark;
                        classVo.sitelist = siteVo.sitelistflag;
                        num = 88;
                        continue;
                    case 0:
                    case 52:
                        num = 61;
                        continue;
                    case 61:
                        num = ((classList == null) ? 43 : 75);
                        continue;
                    case 98:
                        num = 58;
                        continue;
                    case 58:
                        if (ver == "1")
                        {
                            num = 93;
                            continue;
                        }
                        goto IL_17ae;
                    case 28:
                        num7 = 1;
                        goto IL_1286;
                    case 38:
                    case 102:
                        VisiteCount("正在浏览:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
                        num = 69;
                        continue;
                    case 69:
                        return;

                    case 10:
                    case 86:
                        text = ShowWEB_index();
                        flag = !(ver == "1");
                        num = 51;
                        continue;
                    case 51:
                        if (!flag)
                        {
                            num = 41;
                            continue;
                        }
                        goto case 21;
                    case 53:
                        strHtml.Append("<br/>");
                        num = 14;
                        continue;
                    case 14:
                        if (1 == 0)
                        {
                        }
                        goto IL_15de;
                    case 96:
                        num = ((num4 != 0) ? 18 : 97);
                        continue;
                    case 12:
                        if (!flag)
                        {
                            num = 90;
                            continue;
                        }
                        strHtml.Append("<a href=\"" + http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">[hello]" + nickname + "</a>");
                        num = 29;
                        continue;
                    case 49:
                        strHtml.Append(classList[num4].siterowremark);
                        flag = classList[num4].ishidden != 0;
                        num = 62;
                        continue;
                    case 62:
                        if (!flag)
                        {
                            num = 64;
                            continue;
                        }
                        goto case 50;
                    case 41:
                        strHtml.Append("<p align=\"" + classVo.position + "\">");
                        num = 21;
                        continue;
                    case 32:
                        if (!flag)
                        {
                            num = 4;
                            continue;
                        }
                        goto case 33;
                    case 5:
                        str = "myfile.aspx?siteid=" + siteid;
                        num = 2;
                        continue;
                    case 99:
                        strHtml.Append("<br/>");
                        num = 11;
                        continue;
                    case 100:
                        strShowHtml = WapTool.ToWML(text.Replace("[view]", strHtml.ToString()), wmlVo);
                        num = 38;
                        continue;
                    case 23:
                        WapTool.DataClassArray.TryGetValue(siteid + classid, out classList);
                        flag = classList != null;
                        num = 25;
                        continue;
                    case 25:
                        if (!flag)
                        {
                            num = 85;
                            continue;
                        }
                        goto case 1;
                    case 15:
                        strHtml.Append("<a href=\"" + http_start + "waplogin-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode(str) + "\">" + GetLang("登录|登錄|Login") + "</a> <a href=\"" + http_start + "wapreg-" + siteid + "-" + classid + ".html\">" + GetLang("注册|注冊|register") + "</a> <a href=\"" + http_start + "wapstyle/skin-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("皮肤|皮膚|Skin") + "</a> <a href=\"" + http_start + "wapstyle/lang-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("语言|語言|Language") + "</a> <a href=\"" + http_start + "wapstyle/mobileua-" + siteid + "-" + classid + ".html?backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("机型|機型|MobileAgent") + "</a>");
                        num = 79;
                        continue;
                    case 75:
                        num = 20;
                        continue;
                    case 20:
                        num6 = ((num4 < classList.Count) ? 1 : 0);
                        goto IL_10d4;
                    case 72:
                        strHtml.Append("</div>");
                        num = 98;
                        continue;
                    case 73:
                        num = 35;
                        continue;
                    case 35:
                        num = ((classVo.typePath.ToLower() != "null/index.asp") ? 19 : 28);
                        continue;
                    case 80:
                        num = 104;
                        continue;
                    case 87:
                        flag = !(text != "");
                        num = 26;
                        continue;
                    case 26:
                        if (flag)
                        {
                            strShowHtml = WapTool.showTop(classVo.classname, wmlVo).ToString();
                            strShowHtml += WapTool.ToWML(strHtml.ToString(), wmlVo);
                            strShowHtml += WapTool.showDown(wmlVo);
                            num = 102;
                        }
                        else
                        {
                            num = 100;
                        }
                        continue;
                    case 64:
                        flag = !(WapTool.ISAPI_Rewrite3_Open == "1");
                        num = 65;
                        continue;
                    case 65:
                        if (flag)
                        {
                            strHtml.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classList[num4].classid + "&amp;path=" + classList[num4].typePath + "\">" + classList[num4].classname + "</a>");
                            num = 71;
                        }
                        else
                        {
                            num = 56;
                        }
                        continue;
                    case 74:
                        strHtml.Append("<br/>");
                        num = 49;
                        continue;
                    case 43:
                        num6 = 0;
                        goto IL_10d4;
                    case 16:
                        num = ((!flag) ? 27 : 96);
                        continue;
                    case 63:
                        strHtml.Append("<a href=\"" + http_start + "myfile-" + siteid + "-" + classid + ".html\">[hello]" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver, userVo.endTime) + "</a>");
                        num = 59;
                        continue;
                    case 44:
                        flag = !(WapTool.ISAPI_Rewrite3_Open == "1");
                        num = 57;
                        continue;
                    case 57:
                        if (flag)
                        {
                            strHtml.Append("<a href=\"" + http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">[hello]" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver, userVo.endTime) + "</a>");
                            num = 70;
                        }
                        else
                        {
                            num = 63;
                        }
                        continue;
                    case 2:
                        flag = userVo == null;
                        num = 45;
                        continue;
                    case 45:
                        if (flag)
                        {
                            flag = !(WapTool.ISAPI_Rewrite3_Open == "1");
                            num = 12;
                        }
                        else
                        {
                            num = 44;
                        }
                        continue;
                    case 18:
                        num5 = 1;
                        goto IL_1673;
                    case 76:
                        try
                        {
                            WapTool.DataClassArray.Add(siteid + classid, classList);
                        }
                        catch (Exception)
                        {
                        }
                        num = 1;
                        continue;
                    case 66:
                        if (!flag)
                        {
                            num = 81;
                            continue;
                        }
                        CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        num = 30;
                        continue;
                    case 81:
                        getPath(classVo.typePath);
                        num = 55;
                        continue;
                    case 55:
                        return;

                    case 8:
                    case 31:
                        strHtml.Append("</div>");
                        flag = !(userid == "0");
                        num = 36;
                        continue;
                    case 36:
                        if (!flag)
                        {
                            num = 17;
                            continue;
                        }
                        goto case 72;
                    case 88:
                    case 101:
                        flag = !"1".Equals(WapTool.KL_OpenCache);
                        num = 60;
                        continue;
                    case 60:
                        if (!flag)
                        {
                            num = 23;
                            continue;
                        }
                        classList = WapTool.GetChildList(long.Parse(siteid), long.Parse(classid));
                        num = 86;
                        continue;
                    case 104:
                        flag = !(ver == "1");
                        num = 103;
                        continue;
                    case 103:
                        if (!flag)
                        {
                            num = 6;
                            continue;
                        }
                        goto case 87;
                    case 90:
                        strHtml.Append("<a href=\"" + http_start + "myfile-" + siteid + "-" + classid + ".html\">[hello]" + nickname + "</a>");
                        num = 54;
                        continue;
                    case 56:
                        strHtml.Append("<a href=\"" + http_start + "wapindex-" + siteid + "-" + classList[num4].classid + ".html?path=" + classList[num4].typePath + "\">" + classList[num4].classname + "</a>");
                        num = 91;
                        continue;
                    case 84:
                        num3 = 1;
                        goto IL_082c;
                    case 29:
                    case 54:
                        num = 8;
                        continue;
                    case 71:
                    case 91:
                        num = 50;
                        continue;
                    case 93:
                        num = 95;
                        continue;
                    case 95:
                        if (classVo.introduce != "")
                        {
                            num = 77;
                            continue;
                        }
                        goto IL_17ae;
                    case 40:
                        if (!flag)
                        {
                            num = 80;
                            continue;
                        }
                        goto case 104;
                    case 4:
                        strHtml.Append("[div=logo]<img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/>[/div]");
                        num = 33;
                        continue;
                    case 94:
                        if (!flag)
                        {
                            num = 74;
                            continue;
                        }
                        goto case 49;
                    case 47:
                        if (!flag)
                        {
                            num = 99;
                            continue;
                        }
                        goto case 11;
                    case 85:
                        classList = WapTool.GetChildList(long.Parse(siteid), long.Parse(classid));
                        num = 76;
                        continue;
                    case 17:
                        strHtml.Append("<div class=\"bt5\">");
                        flag = !(WapTool.ISAPI_Rewrite3_Open == "1");
                        num = 67;
                        continue;
                    case 67:
                        if (flag)
                        {
                            strHtml.Append("<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;backurl=" + HttpUtility.UrlEncode(str) + "\">" + GetLang("登录|登錄|Login") + "</a> <a href=\"" + http_start + "wapreg.aspx?siteid=" + siteid + "&amp;classid=" + classid + "\">" + GetLang("注册|注冊|register") + "</a> <a href=\"" + http_start + "wapstyle/skin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("皮肤|皮膚|Skin") + "</a> <a href=\"" + http_start + "wapstyle/lang.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("语言|語言|Language") + "</a> <a href=\"" + http_start + "wapstyle/mobileua.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid) + "\">" + GetLang("机型|機型|MobileAgent") + "</a>");
                            num = 46;
                        }
                        else
                        {
                            num = 15;
                        }
                        continue;
                    case 59:
                    case 70:
                        num = 31;
                        continue;
                    case 33:
                        flag = classVo.sitelist != 1;
                        num = 39;
                        continue;
                    case 39:
                        if (!flag)
                        {
                            num = 9;
                            continue;
                        }
                        goto case 98;
                    case 30:
                        num = 34;
                        continue;
                    case 3:
                        {
                            num2 = 1;
                            goto IL_169d;
                        }
                    IL_15de:
                        strHtml.Append(classVo.sitedowntip);
                        flag = !(classid != "0");
                        num = 40;
                        continue;
                    IL_082c:
                        flag = (byte)num3 != 0;
                        num = 32;
                        continue;
                    IL_169d:
                        flag = (byte)num2 != 0;
                        num = 47;
                        continue;
                    IL_10d4:
                        flag = (byte)num6 != 0;
                        num = 16;
                        continue;
                    IL_1673:
                        flag = (byte)num5 != 0;
                        num = 94;
                        continue;
                    IL_1286:
                        flag = (byte)num7 != 0;
                        num = 66;
                        continue;
                    IL_17ae:
                        num = 3;
                        continue;
                }
                break;
            }
        }
    }

    public void getPath(string str)
    {
        //Discarded unreachable code: IL_006c
        while (true)
        {
            str = str.Trim().ToLower();
            bool flag = !str.StartsWith("http://");
            int num = 4;
            while (true)
            {
                switch (num)
                {
                    case 4:
                        if (!flag)
                        {
                            num = 3;
                            continue;
                        }
                        flag = !str.EndsWith(".aspx");
                        num = 1;
                        continue;
                    case 5:
                        base.Server.Transfer("/" + str + "?siteid=" + siteid + "&classid=" + classid);
                        num = 0;
                        continue;
                    case 3:
                        str = str.Replace("[sid]", sid);
                        str = str.Replace("[siteid]", siteid);
                        str = str.Replace("[classid]", classid);
                        base.Response.Redirect(str.Replace("&amp;", "&"));
                        num = 2;
                        continue;
                    case 2:
                        return;

                    case 0:
                    case 6:
                        num = 7;
                        continue;
                    case 7:
                        return;

                    case 1:
                        if (flag)
                        {
                            if (true)
                            {
                            }
                            base.Response.Redirect(http_start + str + "?siteid=" + siteid + "&classid=" + classid + "&sid=" + sid);
                            num = 6;
                        }
                        else
                        {
                            num = 5;
                        }
                        continue;
                }
                break;
            }
        }
    }

    public string ShowWEB_index()
    {
        //Discarded unreachable code: IL_0d3b
        string result = default(string);
        wap3_htmlContent_Model wap3_htmlContent_Model = default(wap3_htmlContent_Model);
        StringBuilder stringBuilder = default(StringBuilder);
        wap3_htmlContent_BLL wap3_htmlContent_BLL = default(wap3_htmlContent_BLL);
        while (true)
        {
            bool flag = !(PageWap.KL_Open_Web == "1");
            int num = 21;
            while (true)
            {
                int num6;
                int num7;
                int num3;
                int num2;
                int num10;
                int num8;
                int num9;
                int num5;
                int num4;
                switch (num)
                {
                    case 21:
                        if (!flag)
                        {
                            num = 22;
                            continue;
                        }
                        goto case 92;
                    case 22:
                        num = 30;
                        continue;
                    case 30:
                        num = ((ver == "3") ? 88 : 93);
                        continue;
                    case 93:
                        num = 56;
                        continue;
                    case 56:
                        num6 = ((!(ver == "4")) ? 1 : 0);
                        goto IL_0e5b;
                    case 104:
                        num = 50;
                        continue;
                    case 90:
                        result = "";
                        num = 46;
                        continue;
                    case 101:
                        num = 19;
                        continue;
                    case 19:
                        num7 = ((!(wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_08df;
                    case 29:
                        num3 = 0;
                        goto IL_04a9;
                    case 36:
                        num = ((wap3_htmlContent_Model.html4_2 == "") ? 89 : 3);
                        continue;
                    case 41:
                        num2 = 0;
                        goto IL_0403;
                    case 108:
                        num = 83;
                        continue;
                    case 83:
                        num = ((classVo.typePath.ToLower() == "null/index.asp") ? 85 : 20);
                        continue;
                    case 20:
                        num = 31;
                        continue;
                    case 31:
                        num10 = ((!(classVo.typePath.ToLower() == "null/index.aspx")) ? 1 : 0);
                        goto IL_0607;
                    case 1:
                        if (!flag)
                        {
                            num = 45;
                            continue;
                        }
                        flag = !(classid != "0");
                        num = 81;
                        continue;
                    case 7:
                        {
                            if (!flag)
                            {
                                num = 57;
                                continue;
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config4_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html4_2);
                            num = 51;
                            continue;
                        }
                    case 79:
                        num = 70;
                        continue;
                    case 70:
                        num = ((wap3_htmlContent_Model.html3_2 == "") ? 53 : 64);
                        continue;
                    case 84:
                        result = "";
                        num = 32;
                        continue;
                    case 38:
                        result = "";
                        num = 54;
                        continue;
                    case 35:
                    case 65:
                        num = 59;
                        continue;
                    case 102:
                        if (!flag)
                        {
                            num = 55;
                            continue;
                        }
                        getPath(classVo.typePath);
                        result = "";
                        num = 8;
                        continue;
                    case 64:
                        num = 27;
                        continue;
                    case 27:
                        num8 = ((!(wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_07b2;
                    case 81:
                        if (!flag)
                        {
                            num = 108;
                            continue;
                        }
                        goto case 33;
                    case 23:
                        num = 106;
                        continue;
                    case 99:
                        CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), classVo.classid);
                        flag = wap3_htmlContent_Model == null;
                        num = 82;
                        continue;
                    case 82:
                        if (!flag)
                        {
                            num = 62;
                            continue;
                        }
                        goto case 23;
                    case 50:
                        num = 75;
                        continue;
                    case 16:
                        if (!flag)
                        {
                            num = 99;
                            continue;
                        }
                        goto case 106;
                    case 53:
                        num8 = 0;
                        goto IL_07b2;
                    case 33:
                        num = 14;
                        continue;
                    case 86:
                        stringBuilder = new StringBuilder();
                        wap3_htmlContent_BLL = new wap3_htmlContent_BLL(PubConstant.GetAppString("InstanceName"));
                        wap3_htmlContent_Model = new wap3_htmlContent_Model();
                        flag = !(classid == "0");
                        num = 67;
                        continue;
                    case 67:
                        if (flag)
                        {
                            flag = classList != null;
                            num = 1;
                        }
                        else
                        {
                            num = 77;
                        }
                        continue;
                    case 73:
                        result = "";
                        num = 68;
                        continue;
                    case 85:
                        num10 = 0;
                        goto IL_0607;
                    case 87:
                        flag = stringBuilder.ToString().IndexOf("</body>") >= 0;
                        num = 98;
                        continue;
                    case 98:
                        if (!flag)
                        {
                            num = 39;
                            continue;
                        }
                        goto case 107;
                    case 89:
                        num9 = 0;
                        goto IL_0a30;
                    case 74:
                        flag = !(ver == "3");
                        num = 72;
                        continue;
                    case 72:
                        num = ((!flag) ? 105 : 36);
                        continue;
                    case 13:
                        flag = !(ver == "3");
                        num = 60;
                        continue;
                    case 60:
                        num = (flag ? 91 : 47);
                        continue;
                    case 42:
                        {
                            if (!flag)
                            {
                                num = 38;
                                continue;
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
                            num = 95;
                            continue;
                        }
                    case 18:
                        num = 10;
                        continue;
                    case 10:
                        num = ((!(classVo.typePath.ToLower() == "null/index.asp")) ? 26 : 29);
                        continue;
                    case 55:
                        CheckUserViewSubMoney("CLASS" + classid, GetUrlQueryString(), "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), classVo.classid);
                        flag = wap3_htmlContent_Model == null;
                        num = 12;
                        continue;
                    case 12:
                        if (!flag)
                        {
                            num = 74;
                            continue;
                        }
                        goto case 104;
                    case 92:
                        result = "";
                        num = 28;
                        continue;
                    case 44:
                        {
                            if (!flag)
                            {
                                num = 90;
                                continue;
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
                            num = 6;
                            continue;
                        }
                    case 77:
                        wap3_htmlContent_Model = wap3_htmlContent_BLL.GetModel(long.Parse(siteid), 0L);
                        flag = wap3_htmlContent_Model == null;
                        num = 25;
                        continue;
                    case 25:
                        if (!flag)
                        {
                            num = 13;
                            continue;
                        }
                        goto case 59;
                    case 9:
                        num = 58;
                        continue;
                    case 58:
                        num5 = ((!(wap3_htmlContent_Model.html3_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_0d0d;
                    case 96:
                        result = "";
                        num = 61;
                        continue;
                    case 62:
                        flag = !(ver == "3");
                        num = 11;
                        continue;
                    case 11:
                        num = (flag ? 76 : 79);
                        continue;
                    case 51:
                    case 95:
                        num = 23;
                        continue;
                    case 2:
                        {
                            if (!flag)
                            {
                                num = 84;
                                continue;
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config4_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html4_2);
                            num = 49;
                            continue;
                        }
                    case 52:
                    case 103:
                        flag = !(stringBuilder.ToString() != "");
                        num = 4;
                        continue;
                    case 4:
                        num = (flag ? 71 : 87);
                        continue;
                    case 40:
                        num4 = 0;
                        goto IL_0b66;
                    case 106:
                        num = 33;
                        continue;
                    case 45:
                        flag = !(classid != "0");
                        num = 37;
                        continue;
                    case 37:
                        if (!flag)
                        {
                            num = 18;
                            continue;
                        }
                        goto case 75;
                    case 63:
                        {
                            if (!flag)
                            {
                                num = 73;
                                continue;
                            }
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config4_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html4_2);
                            num = 35;
                            continue;
                        }
                    case 75:
                        num = 69;
                        continue;
                    case 39:
                        stringBuilder.Append("</body></html>");
                        num = 107;
                        continue;
                    case 14:
                    case 69:
                        num = 52;
                        continue;
                    case 26:
                        num = 0;
                        continue;
                    case 0:
                        num3 = ((!(classVo.typePath.ToLower() == "null/index.aspx")) ? 1 : 0);
                        goto IL_04a9;
                    case 76:
                        num = ((!(wap3_htmlContent_Model.html4_2 == "")) ? 80 : 41);
                        continue;
                    case 3:
                        num = 48;
                        continue;
                    case 48:
                        num9 = ((!(wap3_htmlContent_Model.html4_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_0a30;
                    case 17:
                        if (flag)
                        {
                            StringBuilder siteCSS_WEB = WapTool.getSiteCSS_WEB(siteid, cs);
                            stringBuilder.Append(wap3_htmlContent_Model.config3_2.Replace("</head>", string.Concat(siteCSS_WEB, "</head>")));
                            stringBuilder.Append(wap3_htmlContent_Model.html3_2);
                            num = 65;
                        }
                        else
                        {
                            num = 96;
                        }
                        continue;
                    case 47:
                        if (true)
                        {
                        }
                        num = 24;
                        continue;
                    case 24:
                        num = ((wap3_htmlContent_Model.html3_2 == "") ? 97 : 9);
                        continue;
                    case 78:
                        num7 = 0;
                        goto IL_08df;
                    case 107:
                        result = stringBuilder.ToString();
                        num = 15;
                        continue;
                    case 88:
                        num6 = 0;
                        goto IL_0e5b;
                    case 97:
                        num5 = 0;
                        goto IL_0d0d;
                    case 66:
                        num = 43;
                        continue;
                    case 43:
                        num4 = ((!(wap3_htmlContent_Model.html4_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_0b66;
                    case 91:
                        num = ((!(wap3_htmlContent_Model.html4_2 == "")) ? 66 : 40);
                        continue;
                    case 105:
                        num = 34;
                        continue;
                    case 34:
                        num = ((!(wap3_htmlContent_Model.html3_2 == "")) ? 101 : 78);
                        continue;
                    case 100:
                        if (!flag)
                        {
                            num = 86;
                            continue;
                        }
                        goto case 71;
                    case 57:
                        result = "";
                        num = 5;
                        continue;
                    case 80:
                        num = 94;
                        continue;
                    case 94:
                        num2 = ((!(wap3_htmlContent_Model.html4_2 == "<P>&nbsp;</P>")) ? 1 : 0);
                        goto IL_0403;
                    case 6:
                    case 49:
                        num = 104;
                        continue;
                    case 59:
                        num = 103;
                        continue;
                    case 71:
                        num = 92;
                        continue;
                    case 5:
                    case 8:
                    case 15:
                    case 28:
                    case 32:
                    case 46:
                    case 54:
                    case 61:
                    case 68:
                        {
                            return result;
                        }
                    IL_04a9:
                        flag = (byte)num3 != 0;
                        num = 102;
                        continue;
                    IL_07b2:
                        flag = (byte)num8 != 0;
                        num = 42;
                        continue;
                    IL_08df:
                        flag = (byte)num7 != 0;
                        num = 44;
                        continue;
                    IL_0b66:
                        flag = (byte)num4 != 0;
                        num = 63;
                        continue;
                    IL_0d0d:
                        flag = (byte)num5 != 0;
                        num = 17;
                        continue;
                    IL_0e5b:
                        flag = (byte)num6 != 0;
                        num = 100;
                        continue;
                    IL_0607:
                        flag = (byte)num10 != 0;
                        num = 16;
                        continue;
                    IL_0a30:
                        flag = (byte)num9 != 0;
                        num = 2;
                        continue;
                    IL_0403:
                        flag = (byte)num2 != 0;
                        num = 7;
                        continue;
                }
                break;
            }
        }
    }
}
