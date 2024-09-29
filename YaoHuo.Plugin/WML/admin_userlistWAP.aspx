<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_userlistWAP.aspx.cs" Inherits="YaoHuo.Plugin.WML.admin_userlistWAP" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("WML内容|WML內容|Content of the wml"), wmlVo));
    strhtml.Append("<div class=\"title\">" + classVo.classname + "</div>");
    strhtml.Append("<div class=\"content\">");
    //strhtml.Append("内容管理.<a href=\"" + this.http_start + "wml/admin_guestlistWAP.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">评论管理</a><br/>");
    strhtml.Append(" <a href=\"" + this.http_start + "wml/admin_WAPadd.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">批量新增</a> ");
    strhtml.Append(" <a href=\"" + this.http_start + "wml/admin_WAPadvertise.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">插入广告</a><br/>");
    strhtml.Append("<form id=\"form1\" action=\"" + http_start + "wml/admin_userlistWAP.aspx\" method=\"get\">");
    strhtml.Append("标题 <input type=\"text\" name=\"key\" value=\"" + key + "\" style=\"width:18%\"/>");
    strhtml.Append(" 栏目ID <input type=\"text\" name=\"classid\" value=\"" + classid + "\" style=\"width:18%\"/><br/>");
    strhtml.Append("<input type=\"submit\" value=\"搜索\" />");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("</form>");
    strhtml.Append("<br/>");
    //strhtml.Append("<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?action=gocheckall&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">审核所有</a>.<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?action=gocheckall&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取消所有审核</a><br/>");
    strhtml.Append("</div>");
    //显示列表
    strhtml.Append(linkTOP);
    string YesOrNo = "";
    string CheckStr = "";
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        index = index + kk;
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        if (listVo[i].ischeck.ToString() == "1")
        {
            YesOrNo = "nono.gif";
            CheckStr = "[<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">审核</a>]";
        }
        else
        {
            YesOrNo = "yes.gif";
            CheckStr = "[<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取审</a>]";
        }
        strhtml.Append(index + ".<img src=\"" + this.http_start + "NetImages/" + YesOrNo + "\" alt=\"YES\"/>" + CheckStr + "[ID" + listVo[i].id + "][<a href=\"" + this.http_start + "wml/admin_WAPdel.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">删</a>][<a href=\"" + this.http_start + "wml/admin_WAPmodify.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;page=" + this.CurrentPage + "\">修</a>][<a href=\"" + this.http_start + "wml/admin_WAPchange.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;page=" + this.CurrentPage + "\">转</a>][<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "" + "\">" + listVo[i].classname + "</a>]<a href=\"" + http_start + "wml/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "" + "\">" + listVo[i].book_title + "(" + listVo[i].book_click.ToString() + ")</a></div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录</div>");
    }
    //显示导航分页
    strhtml.Append(linkURL);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/admin_wapClasslist.aspx?siteid=" + siteid + "&amp;gopathname=" + HttpUtility.UrlEncode("WML类") + "&amp;gopath=wml/index.aspx\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(ERROR);
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>