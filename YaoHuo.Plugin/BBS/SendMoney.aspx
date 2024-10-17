<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMoney.aspx.cs" Inherits="YaoHuo.Plugin.BBS.SendMoney" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("赏分|赏分|Modify Reply"), wmlVo));
    Response.Write("<div class=\"subtitle\">" + this.GetLang("赏分操作|赏分操作|Modify") + "</div>");
    Response.Write(this.ERROR);
    Response.Write("<div class=\"tip\">");
    if (this.INFO == "OK")
    {
        Response.Write("<b>" + this.GetLang("赏分成功！|赏分成功！|Modify successfully!") + "</b> <a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回|返回|Back") + "</a><br/>");
    }
    else if (this.INFO == "NULL")
    {
        Response.Write("<b>" + this.GetLang("需要数字！|需要数字！|需要数字!") + "</b><br/>");
    }
    else if (this.INFO == "ERR")
    {
        Response.Write("<b>" + this.GetLang("赏分失败!原因: 1.分值已赏满；2.输入参数不对；3.不能自己赏自己！|ERR！|ERR!") + "</b><br/>");
    }
    Response.Write("</div>");
    Response.Write("<div class=\"content\">");
    Response.Write("<form name=\"f\" aciont=\"" + this.http_start + "bbs/sendmoney.aspx\" method=\"post\">");
    Response.Write(this.GetLang("输入赏分值|输入赏分值|输入赏分值") + ":<br/>");
    Response.Write("<input type=\"text\"  name=\"sendmoney\" value=\"\" /><br/>");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    Response.Write("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"reid\" value=\"" + reid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
    Response.Write("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("确定|确定|submit") + "\"/><br/>");
    Response.Write("</form></div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    Response.Write("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</div></div>");
    Response.Write(WapTool.showDown(wmlVo));
%>