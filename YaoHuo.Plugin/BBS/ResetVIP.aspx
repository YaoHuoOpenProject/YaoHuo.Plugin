<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetVIP.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ResetVIP" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("过期VIP变普通会员|过期VIP变普通会员|Change My NickName"), wmlVo));
    StringBuilder strhtml = new StringBuilder();
    strhtml.Append("<div class=\"title\">过期VIP变普通会员</div>");
    if (ERROR != "")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append(ERROR);
        strhtml.Append("</div>");
    }
    if (INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>操作成功！</b>");
        strhtml.Append("</div>");
    }
    else if (INFO == "NULL")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>我的密码错误！</b>");
        strhtml.Append("</div>");
    }
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<Form name=\"f\" action=\"" + http_start + "bbs/ResetVIP.aspx\" method=\"post\">");
    strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"godoit\" />");
    strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
    strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
    strhtml.Append("<input name=\"backurl\"  type=\"hidden\" value=\"" + this.backurl + "\"  />");
    strhtml.Append("输入密码<br />");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<input type=\"text\" name=\"topw\" style=\"width:98.6%;margin-bottom:5px;\" value=\"\" maxlength=\"50\" />");
    strhtml.Append("</div>");
    strhtml.Append("<input type=\"submit\" id=\"submit\" class=\"btn\" name=\"submit\" value=\"确认执行\" />");
    strhtml.Append("</Form><br/>");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/basesitemodifywml.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>