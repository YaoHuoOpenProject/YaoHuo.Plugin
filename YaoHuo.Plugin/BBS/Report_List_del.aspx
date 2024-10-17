<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_List_del.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Report_List_del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("删除举报|刪除舉報|Delete Report"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang("删除操作|刪除操作|delete") + "</div>");
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\"><a href=\"" + this.http_start + "bbs/Report_List_del.aspx?action=godel&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("确定要删除此条记录吗？是！|確定要刪除此條記錄嗎？是！|Are you sure? YES") + "</a></div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b></div>");
    }
    else
    {
        strhtml.Append("<div class=\"tip\">" + this.INFO + "</div>");
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\"><a href=\"" + this.http_start + "bbs/Report_List.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a></div></div>");
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