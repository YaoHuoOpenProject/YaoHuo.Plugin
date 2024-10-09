<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankList.aspx.cs" Inherits="YaoHuo.Plugin.BBS.BankList" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("账目明细|账目明细|bank list"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang("账目明细|账目明细|bank list") + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/banklist.aspx\" method=\"post\">");
    strhtml.Append("年份 <input type=\"text\" name=\"toyear\" value=\"" + toyear + "\" size=\"8\"/><br/>");
    strhtml.Append("月份 <input type=\"text\" name=\"tomonth\" value=\"" + tomonth + "\" size=\"8\"/><br/>");
    if (this.IsCheckManagerLvl("|00|", "") == true)
    {
        strhtml.Append("ID号 <input type=\"text\" name=\"key\" value=\"" + key + "\" size=\"8\"/><br/>");
    }
    strhtml.Append("<select name=\"typeid\" style='padding:4px;' required>");
    strhtml.Append("<option value=\"" + typeid + "\">" + typeid + "</option>");
    strhtml.Append("<option value=\"1\" selected>1.项目名称</option>");
    strhtml.Append("<option value=\"2\">2.操作人ID</option>");
    strhtml.Append("<option value=\"3\">3.操作昵称</option>");
    //strhtml.Append("<option value=\"4\">4_备注</option>");
    //strhtml.Append("<option value=\"5\">5_流水ID</option>");
    strhtml.Append("</select>");
    strhtml.Append("<span style=\"padding:0 5px;\">关键字</span><input type=\"text\" name=\"typekey\" value=\"" + typekey + "\" size=\"10\"/><br/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"search\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\" />");
    strhtml.Append(" <input type=\"submit\" class=\"btn\" name=\"g\" value=\"" + this.GetLang("查 询|搜索|Search") + "\"/><br/>");
    strhtml.Append("</form>");
    strhtml.Append("</div>");
    strhtml.Append(linkTOP);
    strhtml.Append("<table border=\"1\" width=\"100%\" id=\"table1\">");
    strhtml.Append("<tr align=\"center\">");
    strhtml.Append("<td width=\"20%\">项目</td>");
    strhtml.Append("<td width=\"25%\">交易数量</td>");
    strhtml.Append("<td width=\"25%\">帐户余额</td>");
    strhtml.Append("<td width=\"30%\">操作者</td>");
    strhtml.Append("<td width=\"104\">时间</td>");
    strhtml.Append("</tr>");
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            strhtml.Append("<tr bgcolor=\"#C0C0C0\">");
        }
        else
        {
            strhtml.Append("<tr>");
        }
        strhtml.Append("<td align=\"center\" width=\"20%\">" + listVo[i].actionName + "</td>");
        strhtml.Append("<td align=\"center\" width=\"25%\">");
        if (listVo[i].money.IndexOf("-") >= 0)
        {
            strhtml.Append("<font color=\"FF0000\">");
            strhtml.Append(listVo[i].money);
            strhtml.Append("</font>");
        }
        else
        {
            strhtml.Append("<font color=\"008000\">");
            strhtml.Append(listVo[i].money);
            strhtml.Append(" 妖晶</font>");
        }
        strhtml.Append("</td>");
        strhtml.Append("<td align=\"center\" width=\"25%\">" + listVo[i].leftMoney + "</td>");
        strhtml.Append("<td align=\"center\" width=\"30%\"><a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].opera_userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/banklist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;key=" + HttpUtility.UrlEncode(key)) + "" + "\">" + listVo[i].opera_nickname + "</a><br/>(ID:" + listVo[i].opera_userid + ")</td>");
        strhtml.Append("<td width=\"104\">" + listVo[i].addtime + "</td>");
        strhtml.Append("</tr>");
    }
    strhtml.Append("</table>");
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">没有记录</div>");
    }
    strhtml.Append(linkURL);
    if (this.isadmin == true)
    {
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/banklist.aspx?action=mod&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;toyear=" + this.toyear + "&amp;tomonth=" + this.tomonth + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">删除所有会员记录(保留本月)</a> ");
        strhtml.Append("</div></div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回来源页</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>