<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="book_view_modfile_del.aspx.cs" Inherits="YaoHuo.Plugin.BBS.book_view_modfile_del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("删除贴子|刪除貼子|Delete posts"), wmlVo));
    strhtml.Append("<div class=\"subtitle\">" + this.GetLang("删除操作|刪除操作|delete") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_modfile_del.aspx?action=godel&amp;id=" + this.id + "&amp;delid=" + delid + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;sub=0&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定删除！|确定删除！|submit") + "</a><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_modfile.aspx?id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
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
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_modfile.aspx?id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>