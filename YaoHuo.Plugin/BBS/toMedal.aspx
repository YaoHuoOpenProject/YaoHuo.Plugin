<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToMedal.aspx.cs" Inherits="YaoHuo.Plugin.BBS.toMedal" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("奖励会员勋章|奖励会员勋章设置|Content of the guessbook"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    Response.Write("<p>");
    if (ERROR != "")
    {
        Response.Write(ERROR);
        Response.Write("<br/>");
    }
    if (INFO == "OK")
    {
        Response.Write("<b>更新成功！</b>");
        Response.Write("<br/>");
    }
    else if (INFO == "NOTEXIST")
    {
        Response.Write("<b>会员不存！</b>");
        Response.Write("<br/>");
    }
    Response.Write("<b>第一步：</b><br/>");
    Response.Write("<a href=\"" + http_start + "bbs/toMedalView.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "&amp;facetype=1&amp;facesmall=1&amp;facebig=22\">查看小图系列</a><br/>");
    Response.Write("<a href=\"" + http_start + "bbs/toMedalView.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "&amp;facetype=2&amp;facesmall=23&amp;facebig=40\">查看大图系列</a><br/>");
    Response.Write("<a href=\"" + http_start + "bbs/toMedalView.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "&amp;facetype=3&amp;facesmall=41&amp;facebig=60\">查看QQ系列</a><br/>");
    Response.Write("<br/>");
    Response.Write("<b>第二步：</b><br/>");
    Response.Write("用户ID：<input type=\"text\" name=\"touserid" + r + "\" value=\"" + touserid + "\" size=\"5\"/>");
    Response.Write("<anchor><go href=\"" + http_start + "bbs/toMedal.aspx\" method=\"post\" accept-charset=\"utf-8\">");
    Response.Write("<postfield  name=\"action\"  value=\"search\" />");
    Response.Write("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
    Response.Write("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
    Response.Write("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
    Response.Write("<postfield name=\"touserid\" value=\"$(touserid" + r + ")\"/>");
    Response.Write("</go>" + this.GetLang("查询|查询|Search") + "</anchor><br/>");
    Response.Write("查询勋章显示：<br/>");
    if (smedal != "")
    {
        Response.Write(WapTool.GetMedal(smedal, this.http_start));
    }
    else
    {
        Response.Write("<b>" + this.GetLang("暂时无勋章|暂时无勋章|暂时无勋章") + "</b>");
    }
    Response.Write("<br/>");
    Response.Write("<b>第三步：</b><br/>");
    Response.Write("手动奖励勋章：<br/><input type=\"text\" name=\"smedal" + r + "\" value=\"" + smedal + "\" size=\"25\"/><br/>");
    Response.Write("(多个用|区分,如1.gif|2.gif)<br/>");
    Response.Write("原因：<br/><input type=\"text\" name=\"remark" + r + "\" value=\"" + remark + "\" size=\"25\"/><br/>");
    Response.Write("<anchor><go href=\"" + http_start + "bbs/toMedal.aspx\" method=\"post\" accept-charset=\"utf-8\">");
    Response.Write("<postfield  name=\"action\"  value=\"gomod\" />");
    Response.Write("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
    Response.Write("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
    Response.Write("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
    Response.Write("<postfield  name=\"touserid\"  value=\"" + this.touserid + "\"  />");
    Response.Write("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
    Response.Write("<postfield name=\"smedal\" value=\"$(smedal" + r + ")\"/>");
    Response.Write("</go>" + this.GetLang("确定更新|确定更新|update") + "</anchor><br/>");
    Response.Write("<a href=\"" + this.http_start + "admin/basesitemodifywml.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
    Response.Write(WapTool.GetVS(wmlVo));
    Response.Write("</p>");
}
else //2.0界面
{
    Response.Write("<div class=\"title\">" + "奖励会员勋章" + "</div>");
    if (ERROR != "")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write(ERROR);
        Response.Write("</div>");
    }
    if (INFO == "OK")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>更新成功！</b>");
        Response.Write("</div>");
    }
    else if (INFO == "NOTEXIST")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>会员不存！</b>");
        Response.Write("</div>");
    }
    Response.Write("<div class=\"content\">");
    //显示列表
    Response.Write("<b>第一步：</b>");
    Response.Write("<a href=\"" + http_start + "wapindex.aspx?classid=224\">查看勋章图片</a><br/><br/>");
    //Response.Write("<a href=\"" + http_start + "bbs/toMedalView.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "&amp;facetype=2&amp;facesmall=23&amp;facebig=40\">查看大图系列</a><br/>");
    //Response.Write("<a href=\"" + http_start + "bbs/toMedalView.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "&amp;facetype=3&amp;facesmall=41&amp;facebig=60\">查看QQ系列</a><br/>");
    //Response.Write("<br/>");
    Response.Write("<b>第二步：</b>");
    Response.Write("<form id=\"form1\" action=\"" + http_start + "bbs/toMedal.aspx\" method=\"post\">");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"search\" />");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    //Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
    Response.Write("<input type=\"text\" name=\"touserid\" value=\"" + touserid + "\" size=\"12\"/>");
    Response.Write("<input type=\"submit\" value=\"查询ID\" /></form><br/>");
    //Response.Write("显示勋章<br/>");
    if (smedal != "")
    {
        Response.Write(WapTool.GetMedal(smedal, this.http_start));
    }
    else
    {
        Response.Write("<b>" + this.GetLang("暂无勋章|暂无勋章|暂无勋章") + "</b>");
    }
    Response.Write("<br/><br/>");
    Response.Write("<b>第三步：</b>");
    Response.Write("<form id=\"form1\" action=\"" + http_start + "bbs/toMedal.aspx\" method=\"post\">");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\" />");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    Response.Write("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\" />");
    //Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
    Response.Write("奖励勋章<br/>");
    Response.Write("<input type=\"text\" name=\"smedal\" style='width:98.6%;margin-bottom:5px;' value=\"" + smedal + "\" size=\"25\"/><br/>");
    Response.Write("<br/>奖励原因 ");
    Response.Write("<input type=\"text\" name=\"remark\" value=\"" + remark + "\" style=\"width:80%;\"/><br/>");
    Response.Write("<input type=\"submit\" value=\"确认提交\" />");
    Response.Write("</form>");
    Response.Write("</div>");
    //显示导航分页
    //Response.Write( linkURL );
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    Response.Write("<a href=\"" + this.http_start + "admin/basesitemodifywml.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
    Response.Write("</div></div>");
}

Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>