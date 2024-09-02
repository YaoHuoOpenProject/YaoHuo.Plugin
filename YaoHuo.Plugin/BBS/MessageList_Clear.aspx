<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList_Clear.aspx.cs" Inherits="YaoHuo.Plugin.BBS.MessageList_Clear" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("清除|清除|clear"), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p>");
        strhtml.Append(this.ERROR);
        if (this.INFO == "")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_clear.aspx?action=go" + action + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;page=" + this.page + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("确定要清除吗？是！|確定要清除嗎？是！|Are you sure? YES") + "</a><br/>");
        }
        else if (this.INFO == "OK")
        {
            strhtml.Append("<b>" + this.GetLang("清除成功！|清除成功！|Clear successfully!") + "</b><br/>");
        }
        strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;\">返回上级</a>");
        strhtml.Append("</p>");
        Response.Write(strhtml);
    }
    else //2.0界面
    {
        strhtml.Append("<div class=\"subtitle\">" + this.GetLang("清除|清除|clear") + "</div>");
        strhtml.Append(this.ERROR);
        if (this.INFO == "")
        {
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a   href=\"" + http_start + "bbs/messagelist_clear.aspx?action=go" + action + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;page=" + this.page + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("确定要清除吗？是！|確定要清除嗎？是！|Are you sure? YES") + "</a><br/>");
            strhtml.Append("</div></div>");
        }
        else if (this.INFO == "OK")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("清除成功！|清除成功！|Clear successfully!") + "</b><br/>");
            strhtml.Append("</div>");
        }
        strhtml.Append("<div class=\"bt1\">");
        strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;\">返回上级</a>");
        strhtml.Append("</div>");
        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        if (isWebHtml != "")
        {
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }
        Response.Write(strhtml);
    }
    Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>