<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_guestlistWAPdel00.aspx.cs" Inherits="YaoHuo.Plugin.BBS.admin_guestlistWAPdel00" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("论坛回复|论坛回复|Content of the bbs"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    Response.Write("<div class=\"subtitle\">" + this.GetLang("删除操作|刪除操作|delete") + "</div>");
    if (this.INFO == "")
    {
        Response.Write("<div class=\"content\"><a href=\"" + this.http_start + "bbs/admin_guestlistWAPdel00.aspx?action=godel&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.page + "\">" + this.GetLang("确定要删除此条记录吗？是！|確定要刪除此條記錄嗎？是！|Are you sure? YES") + "</a></div>");
    }
    else if (this.INFO == "OK")
    {
        Response.Write("<div class=\"tip\"><b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b></div>");
    }
    else
    {
        Response.Write("<div class=\"tip\">"+this.INFO+"</div>");
    }
    Response.Write("<div class=\"btBox\"><div class=\"bt1\"><a href=\"" + this.http_start + "bbs/admin_guestlistWAP00.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a></div></div>");
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>