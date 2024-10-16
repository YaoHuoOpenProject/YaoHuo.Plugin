﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_del.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("删除帖子|刪除貼子|Delete posts"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang("删除帖子|刪除操作|delete") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        if (this.userid == bbsVo.book_pub.ToString()) //删除自己的帖子
        {
            strhtml.Append("删除自己帖子扣2倍币和经验<br/>");
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_del.aspx?action=godel&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定删除|确定删除|submit") + "</a><br/>");
        }
        else
        {
            strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_del.aspx\" method=\"get\">");
            strhtml.Append("删帖理由 <input type=\"text\" name=\"why\" value=\"" + why + "\" /><br/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + (this.id) + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + (this.lpage) + "\" />");
            strhtml.Append("1.删除帖子扣1倍币和经验<br/>");
            strhtml.Append("<input type=\"submit\" name=\"action\" value=\"Del_1\"/><br/>");
            strhtml.Append("2.删除帖子扣2倍币和经验<br/>");
            strhtml.Append("<input type=\"submit\" name=\"action\" value=\"Del_2\"/><br/>");
            strhtml.Append("3.删除帖子不扣币和经验<br/>");
            strhtml.Append("<input type=\"submit\" name=\"action\" value=\"Del_3\"/><br/>");
            strhtml.Append("</form>");
        }
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>