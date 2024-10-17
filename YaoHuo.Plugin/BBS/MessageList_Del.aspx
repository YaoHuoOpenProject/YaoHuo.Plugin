<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList_Del.aspx.cs" Inherits="YaoHuo.Plugin.BBS.MessageList_Del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("删除|删除|delete"), wmlVo));
    strhtml.Append("<div class=\"subtitle\">" + this.GetLang("删除|删除|delete") + "</div>");
    strhtml.Append(this.ERROR);

    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a   href=\"" + http_start + "bbs/messagelist_del.aspx?action=go" + action + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;page=" + this.page + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("确定要删除吗？是！|確定要刪除嗎？是！|Are you sure? YES") + "</a><br/>");
        strhtml.Append("</div></div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b><br/>");
        strhtml.Append("</div>");
    }
    strhtml.Append("<div class=\"bt1\">");
    strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a>");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>