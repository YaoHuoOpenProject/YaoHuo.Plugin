<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_guestlistWAP00.aspx.cs" Inherits="YaoHuo.Plugin.BBS.admin_guestlistWAP00" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("论坛回复审核|论坛回复审核|Check Content of the bbs"), wmlVo));
    strhtml.Append("<div class=\"title\">" + classVo.classname + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/admin_userlistWAP00.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "\">内容审核</a><span class=\"separate\"></span>回复审核<br/>");
    strhtml.Append("<form name=\"f\" aciont=\"" + http_start + "bbs/admin_userlistWAP00.aspx\" method=\"post\">");
    strhtml.Append("<input style=\"display: none;\"  type=\"text\" name=\"tositeid\" value=\"" + tositeid + "\" size=\"5\"/> ");
    strhtml.Append("栏目ID <input type=\"text\" name=\"classid\" value=\"" + classid + "\" size=\"5\"/>");
    strhtml.Append(" 状态 <select style='height:28px;' name=\"ischeck\" value=\"" + this.ischeck + "\">");
    strhtml.Append("<option value=\"\">所有</option>");
    strhtml.Append("<option value=\"1\">未审核</option>");
    strhtml.Append("<option value=\"0\">已审核</option>");
    strhtml.Append("</select><br/>");
    strhtml.Append("关键字 <input style='width:60%' type=\"text\" name=\"key\" value=\"" + key + "\" size=\"5\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("<br/><input type=\"submit\" name=\"g\" value=\"" + this.GetLang("搜 索|搜 索|Search") + "\"/>");
    strhtml.Append("</form></div>");
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
            YesOrNo = "nono.gif";
            //YesOrNo = "×";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAP00.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">审核</a>]";
        }
        else
        {
            //YesOrNo = "√";
            YesOrNo = "yes.gif";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAP00.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">取审</a>]";
        }
        //strhtml.Append(index + "." + YesOrNo + "[" + this.tositeid + "]" + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAPdel00.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">删</a>]<a href=\"" + this.http_start + "bbs/userinfo.aspx?action=class&amp;siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "" + "\">" + listVo[i].nickname + "</a>:" + listVo[i].content + "<br/>(" + listVo[i].redate + ")</div>");
        strhtml.Append(index + ".<img src=\"" + this.http_start + "NetImages/" + YesOrNo + "\" alt=\"YES\"/>" + CheckStr + "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAPdel00.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.tositeid + "&amp;page=" + this.CurrentPage + "\">删</a>]<a href=\"" + this.http_start + "bbs/userinfo.aspx?action=class&amp;siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "" + "\">" + listVo[i].nickname + "</a>:" + listVo[i].content + "<br/>(" + listVo[i].redate + ")</div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录！</div>");
    }
    strhtml.Append(linkURL);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/basesitemodifywml00.aspx?siteid=" + siteid + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>