<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_wapClasslist.aspx.cs" Inherits="YaoHuo.Plugin.Admin.admin_wapClasslist" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    string str = this.http_start + gopath + "?action=wapAdmin" + "&amp;siteid=" + siteid;
    Response.Write(WapTool.showTop(this.GetLang("栏目列表|欄目列表|menu-list"), wmlVo));
    Response.Write("<div class=\"title\">");
    Response.Write("WML栏目列表</div>");
    Response.Write("<div class=\"content\">");
    Response.Write("0_<a href=\"" + str + "&amp;classid=0" + "\">所有" + gopathname + "栏目</a><br/>");
    for (int i = 0; (classList != null && i < classList.Count); i++)
    {
        Response.Write(classList[i].classid + "_<a href=\"" + str + "&amp;classid=" + classList[i].classid + "\">" + classList[i].classname + "(" + classList[i].total + "条)</a><br/>");
    }
    Response.Write("</div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"" + http_start + "admin/admin_waplist.aspx?siteid=" + siteid + "" + "\">返回上级</a> <a href=\"" + http_start + "\">返回首页</a>");
    Response.Write("</div></div>");
    Response.Write(WapTool.showDown(wmlVo));
%>