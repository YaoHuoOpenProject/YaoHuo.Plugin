<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_userlistWAP00.aspx.cs" Inherits="YaoHuo.Plugin.BBS.admin_userlistWAP00" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("论坛内容审核|论坛內容审核|Check Content of the bbs"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    strhtml.Append("<div class=\"title\">" + classVo.classname + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("内容审核<span class=\"separate\"></span><a href=\"" + this.http_start + "bbs/admin_guestlistWAP00.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "\">回复审核</a><br/>");
    strhtml.Append("<form name=\"f\" aciont=\"" + http_start + "bbs/admin_userlistWAP00.aspx\" method=\"post\">");
    //strhtml.Append("网站ID：<input type=\"text\" name=\"tositeid\" value=\"" + tositeid + "\" size=\"5\"/> ");
    strhtml.Append("<input style=\"display: none;\" type=\"text\" name=\"tositeid\" value=\"" + tositeid + "\" size=\"5\"/> ");
    strhtml.Append("栏目ID <input type=\"text\" name=\"classid\" value=\"" + classid + "\" size=\"5\"/> ");
    strhtml.Append(" 状态 <select style='height:28px;' name=\"ischeck\">");
    strhtml.Append("<option value=\"\">所有</option>");
    strhtml.Append("<option value=\"1\">未审核</option>");
    strhtml.Append("<option value=\"0\" selected>已审核</option>");
	strhtml.Append("</select><br/>");
    strhtml.Append("关键字 <input style='width:60%' type=\"text\" name=\"key\" value=\"" + key + "\" size=\"5\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
    strhtml.Append("<br/><input type=\"submit\" name=\"g\" value=\"" + this.GetLang("搜 索|搜 索|Search") + "\"/>");
    strhtml.Append("</form></div>");
    //strhtml.Append("<div class=\"tip\"><a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=gocheckall&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">审核所有</a>.<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=gocheckall&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">取消所有审核</a></div>");
    //显示列表
    strhtml.Append(this.linkTOP);
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
            //YesOrNo = "×";
            YesOrNo = "nono.gif";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">审核</a>]";
        }
        else
        {
            //YesOrNo = "√";
            YesOrNo = "yes.gif";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">取审</a>]";
        }
        //strhtml.Append(index + "." + YesOrNo + "[" + this.tositeid + "]" + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_WAPdel00.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">删</a>][<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;tositeid=" + this.tositeid + "&amp;classid=" + listVo[i].book_classid + "" + "\">" + listVo[i].classname + "</a>]<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "\">" + listVo[i].book_title + "(" + listVo[i].book_click.ToString() + ")</a></div>");
        //strhtml.Append(index + ".<img src=\"" + this.http_start + "NetImages/" + YesOrNo + "\" alt=\"YES\"/>" + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_WAPdel00.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">删</a>][<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;tositeid=" + this.tositeid + "&amp;classid=" + listVo[i].book_classid + "" + "\">" + listVo[i].classname + "</a>]<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "\">" + listVo[i].book_title + "(" + listVo[i].book_click.ToString() + ")</a></div>");
        strhtml.Append(index + ".<img src=\"" + this.http_start + "NetImages/" + YesOrNo + "\" alt=\"YES\"/>" + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_WAPdel00.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">删</a>][<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;tositeid=" + this.tositeid + "&amp;classid=" + listVo[i].book_classid + "" + "\">" + listVo[i].classname + "</a>]<a href=\"" + http_start + "bbs-" + listVo[i].id + ".html\">" + listVo[i].book_title + "(" + listVo[i].book_click.ToString() + ")</a></div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录！</div>");
    }
    //显示导航分页
    strhtml.Append(linkURL);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/basesitemodifywml00.aspx?siteid=" + siteid + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    //输出
    //Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(strhtml);
}
Response.Write(ERROR);                                                                                                                                                                              
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>