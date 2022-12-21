<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_Buy.aspx.cs" Inherits="YaoHuo.Plugin.XinZhang.Book_View_Buy" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop("购买勋章", wmlVo));//显示头  

    if (ver == "1")
    {
        strhtml.Append("<p align=\"" + classVo.position + "\">");
        //显示广告
        //if(adVo.threeShowTop!=""){
        //   strhtml.Append(adVo.threeShowTop+"<br/>");
        //}

        if (this.INFO == "OK")
        {
            strhtml.Append("<b>*购买成功，我当前拥有勋章：" + WapTool.GetMedal(userVo.moneyname, http_start) + "</b><br/>");
        }
        else if (this.INFO == "NOMONEY")
        {
            strhtml.Append("<b>*我的币不够啦，当前我的币有" + userVo.money + "个。</b><br/>");
        }
        else if (this.INFO == "REPEAT")
        {
            strhtml.Append("<b>*购买失败，我已经拥该勋章：" + WapTool.GetMedal(userVo.moneyname, http_start) + "</b></div>");
        }
        else if (this.INFO == "NOPASS")
        {
            strhtml.Append("<b>*密码错误，请重新录入。</b><br/>");
        }

        if (this.INFO != "OK")
        {
            strhtml.Append("<b>" + bookVo.XinZhangMingChen + "</b><br/>");

            strhtml.Append("<img src=\"" + this.http_start + bookVo.XinZhangTuPian + "\" alt=\".\"/><br/>");

            strhtml.Append("价格：" + bookVo.XinZhangJiaGe + " " + siteVo.sitemoneyname + "<br/>");
            strhtml.Append("我当前拥有：" + userVo.money + " " + siteVo.sitemoneyname + "<br/>");
            strhtml.Append("我的密码：<input type=\"text\" name=\"pw\" value=\"\" size=\"8\"/><br/>");
            strhtml.Append("<anchor><go href=\"" + http_start + "xinzhang/book_view_buy.aspx\" method=\"post\" accept-charset=\"utf-8\">");
            strhtml.Append("<postfield name=\"action\" value=\"gomod\" />");
            strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\" />");
            strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\" />");
            strhtml.Append("<postfield name=\"pw\" value=\"$(pw)\" />");
            strhtml.Append("<postfield name=\"id\" value=\"" + (bookVo.ID) + "\" />");
            strhtml.Append("<postfield name=\"lpage\" value=\"" + (this.lpage) + "\" />");
            strhtml.Append("<postfield name=\"ordertype\" value=\"" + (this.ordertype) + "\" />");
            strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\" />");
            strhtml.Append("</go>确定购买</anchor> <br/>");
        }
        strhtml.Append("<a href=\"" + this.http_start + "XinZhang/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + this.lpage + "&amp;ordertype=" + this.ordertype + "" + "\">返回上级</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        //输出错误
        strhtml.Append(ERROR);
        strhtml.Append("</p>");

        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    }
    else
    {
        //2.0
        //显示广告
        //if (adVo.threeShowTop != "")
        //{
        //    strhtml.Append(adVo.threeShowTop );
        //}
        strhtml.Append("<div class=\"subtitle\">购买勋章（禁止重复购买）</div>");

        if (this.INFO == "OK")
        {
            strhtml.Append("<div class=\"tip\"><b>购买成功，我当前拥有勋章：" + WapTool.GetMedal(userVo.moneyname, http_start) + "</b></div>");
        }
        else if (this.INFO == "NOMONEY")
        {
            strhtml.Append("<div class=\"tip\"><b>我的币不够啦，当前我的币有" + userVo.money + "个。</b></div>");
        }
        else if (this.INFO == "REPEAT")
        {
            strhtml.Append("<div class=\"tip\"><b>购买失败，我已经拥该勋章：" + WapTool.GetMedal(userVo.moneyname, http_start) + "</b></div>");
        }
        else if (this.INFO == "NOPASS")
        {
            strhtml.Append("<div class=\"tip\"><b>密码错误，请重新录入。</b></div>");
        }

        if (this.INFO != "OK")
        {
            strhtml.Append("<div class=\"content\">");
            strhtml.Append("<b>" + bookVo.XinZhangMingChen + "</b><br/>");
            strhtml.Append("<img src=\"" + this.http_start + bookVo.XinZhangTuPian + "\" alt=\".\"/><br/>");
            strhtml.Append("价格：" + bookVo.XinZhangJiaGe + " " + siteVo.sitemoneyname + "<br/><br/>");
            strhtml.Append("当前拥有：" + userVo.money + " " + siteVo.sitemoneyname + " [<a href=\"" + http_start + "wapindex.aspx?classid=207\">赚币</a>]<br/><br/>");
            strhtml.Append("<form name=\"f\" action=\"" + http_start + "xinzhang/book_view_buy.aspx\" method=\"post\">");
            strhtml.Append("输入密码：<input type=\"text\" style=\"width: 65%; \" name=\"pw\" value=\"" + pw + "\" size=\"15\"/><br/><br/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + (bookVo.ID) + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + (this.lpage) + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"ordertype\" value=\"" + ordertype + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\" />");
            strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"\" value=\"确定购买\"/><br/>");
            strhtml.Append("</form>");
            strhtml.Append("</div>");
        }

        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a href=\"/wapindex.aspx?classid=226\">返回上级</a> ");

        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");

        strhtml.Append("</div></div>");


        if (isWebHtml != "")
        {
            //string strhtml_list = strhtml.ToString();
            //int s = strhtml_list.IndexOf("<div class=\"title\">");
            //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);

            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }

        //输出错误
        strhtml.Append(ERROR);
        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));

    }
    Response.Write(WapTool.showDown(wmlVo));
%>