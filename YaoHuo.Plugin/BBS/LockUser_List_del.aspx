<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockUser_List_del.aspx.cs" Inherits="YaoHuo.Plugin.BBS.LockUser_List_del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("解除加黑|解除加黑|Cancle LockUser"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    Response.Write("<div class=\"title\">" + this.GetLang("解除加黑|解除加黑|Cancle LockUser") + "</div>");
    Response.Write(this.ERROR);
    if (this.INFO == "")
    {
        Response.Write("<div class=\"content\">");
        Response.Write("<a href=\"" + this.http_start + "bbs/LockUser_List_del.aspx?action=godel&amp;delid=" + delid + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;backurlid=" + this.backurlid + "&amp;touserid=" + this.touserid + "&amp;toclassid=" + this.toclassid + "\">" + this.GetLang("确定要删除吗？是！|確定要刪除嗎？是！|Are you sure? YES") + "</a><br/>");
        Response.Write("</div>");
    }
    else if (this.INFO == "OK")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b><br/>");
        Response.Write("</div>");
    }
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    Response.Write("<a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid="+backurlid+"&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "&amp;toclassid=" + this.toclassid + "\">返回上级</a>");
    Response.Write("</div></div>");
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>