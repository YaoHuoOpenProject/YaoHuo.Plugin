<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Share.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Share" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("��������|��������|share Notes"), wmlVo));
    string title = "�Ƽ�����һƪ�����¡�" + bookVo.book_title + "��";
    string content2 = "�Ƽ�����һƪ�����¡�" + bookVo.book_title + "��[url=/bbs-" + this.id + ".html]����鿴[/url]";
    Response.Write("<div class=\"title\">" + this.GetLang("��������|��������|share Notes") + "</div>");
    Response.Write(this.ERROR);
    if (this.INFO == "WAITING")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>���ٹ�" + this.KL_CheckIPTime + "��������</b><br/>");
        Response.Write("</div>");
    }
    else if (this.INFO == "REPEAT")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>�����ղع������ӣ�</b><br />");
        Response.Write("</div>");
        Response.Write("<div class=\"content\">");
        Response.Write("[<a href=\"/bbs/favlist.aspx\">�鿴�ղؼ�</a>]<br />");
        Response.Write("</div>");
    }
    else
    {
        Response.Write("<div class=\"content\">");
        Response.Write("<form name=\"f\" action=\"/bbs/messagelist_add.aspx\" method=\"post\">");
        Response.Write("<input type=\"hidden\" name=\"action\" value=\"go\"/>");
        Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"content\" value=\"" + content2 + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"title\" value=\"" + title + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"backurl\" value=\"" + HttpUtility.UrlEncode("bbs/share.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;stype=" + bookVo.topic + "&amp;lpage=" + this.lpage) + "\"/>");
        Response.Write("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"�Ƽ���վ�ں���\"/></form><br/>");
        Response.Write("<form name=\"f\" action=\"/bbs/share.aspx\" method=\"get\">");
        Response.Write("<input type=\"hidden\" name=\"action\" value=\"goclan\"/>");
        Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"vpage\" value=\"" + vpage + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
        Response.Write("<a href=\"" + this.http_start + "bbs/Share.aspx?action=fav&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\">�����ղ�</a><br/>");
        Response.Write("</div></div>");
    }
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"" + this.http_start + "bbs-" + this.id + ".html\">" + this.GetLang("��������|��������|Back to subject") + "</a>");
    Response.Write("<a href=\"" + this.http_start + "bbs/list.aspx?classid=" + this.classid + "\">" + this.GetLang("�����б�|�����б�|Back to list") + "</a>");
    Response.Write("</div></div>");
    Response.Write(WapTool.showDown(wmlVo));
%>