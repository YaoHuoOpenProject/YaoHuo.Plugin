<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToMedal.aspx.cs" Inherits="YaoHuo.Plugin.BBS.toMedal" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("奖励会员勋章|奖励会员勋章设置|Content of the guessbook"), wmlVo));    Response.Write("<div class=\"title\">" + "奖励会员勋章" + "</div>");
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
    Response.Write("<b>第一步：</b>");
    Response.Write("<a href=\"" + http_start + "wapindex.aspx?classid=224\">查看勋章图片</a><br/><br/>");
    Response.Write("<b>第二步：</b>");
    Response.Write("<form id=\"form1\" action=\"" + http_start + "bbs/toMedal.aspx\" method=\"post\">");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"search\" />");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    Response.Write("<input type=\"text\" name=\"touserid\" value=\"" + touserid + "\" size=\"12\"/>");
    Response.Write("<input type=\"submit\" value=\"查询ID\" /></form><br/>");
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
    Response.Write(WapTool.showDown(wmlVo));
%>