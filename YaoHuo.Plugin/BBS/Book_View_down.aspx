﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_down.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_down" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    string lang1 = "";
    string lang2 = "";
    string lang3 = "";
    if (tops == "0")
    {
        lang1 = "取消";
        lang2 = "取消";
        lang3 = "Cancel";
    }
    Response.Write(WapTool.showTop(this.GetLang(lang1 + "设沉帖子|" + lang2 + "设沉貼子|" + lang3 + " Top Good"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang(lang1 + "设沉帖子|" + lang2 + "设沉貼子|" + lang3 + " Top Notes") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_down.aspx?action=gomod&amp;tops=" + this.tops + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定要" + lang1 + "设沉此帖吗？是！|確定要" + lang2 + "设沉此帖嗎？是！|Are you sure? YES") + "</a><br/>注意：如有附件请勿操作，否则看不到附件！<br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "ERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "设沉失败！|" + lang2 + "设沉失敗！|" + lang3 + " TOP Failure!") + "</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "NOTDOWN")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>附件帖不能设置</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "设沉成功！|" + lang2 + "设沉成功！|" + lang3 + " TOP successfully!") + "</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to subject") + "</a>");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to subject") + "</a>");
    strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>