﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_userlistWAP.aspx.cs" Inherits="YaoHuo.Plugin.BBS.admin_userlistWAP" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("论坛内容|论坛內容|Content of the bbs"), wmlVo));
    strhtml.Append("<div class=\"title\">" + classVo.classname + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("内容管理<span class=\"separate\"></span><a href=\"" + this.http_start + "bbs/admin_guestlistWAP.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">回帖管理</a><span class=\"separate\"></span>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/admin_WAPadvertise.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">插入广告</a><br/>");
    strhtml.Append("<form id=\"form1\" action=\"" + http_start + "bbs/admin_userlistWAP.aspx\" method=\"get\">");
    strhtml.Append("关键字 <input type=\"text\" name=\"key\" value=\"" + key + "\" style=\"width:60%\"/><br/>");
    strhtml.Append("栏目ID <input type=\"text\" name=\"classid\" style=\"width:30%\" value=\"" + classid + "\" /><br/>");
    strhtml.Append("<input type=\"submit\" value=\"搜索\" />");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("</form>");
    strhtml.Append("</div>");
    //显示列表
    strhtml.Append(linkTOP);
    string YesOrNo = "";
    string CheckStr = "";
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        index = index + kk;
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        if (listVo[i].ischeck.ToString() == "1")
        {
            YesOrNo = "×";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">审核</a>]";
        }
        else
        {
            YesOrNo = "√";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取审</a>]";
        }
        if (listVo[i].book_top.ToString() == "1")
        {
            YesOrNo = YesOrNo + "<b>顶</b>";
            //CheckStr = CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gotop&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取顶</a>]";
        }
        else
        {
            //CheckStr = CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gotop&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">置顶</a>]";
        }
        if (listVo[i].book_good.ToString() == "1")
        {
            YesOrNo = YesOrNo + "<b>荐</b>";
            //CheckStr = CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gogood&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取荐</a>]";
        }
        else
        {
            // CheckStr = CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=gogood&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">推荐</a>]";
        }
        strhtml.Append(index + "." + YesOrNo + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "" + "\">" + listVo[i].classname + "</a>]<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "" + "\">" + listVo[i].book_title + "(" + listVo[i].book_click.ToString() + ")</a><br/>[" + listVo[i].book_author + "]" + listVo[i].book_date + "</div>");
        YesOrNo = "";
        CheckStr = "";
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录！</div>");
    }
    strhtml.Append(linkURL);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/admin_wapClasslist.aspx?siteid=" + siteid + "&amp;gopathname=" + HttpUtility.UrlEncode("论坛类") + "&amp;gopath=bbs/index.aspx\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>