<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_Re_delmy.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_re_delmy" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("清空回复|清空回複|del all Replies"), wmlVo));
    Response.Write("<div class=\"title\">" + this.GetLang("清空回复操作|清空回复操作|delete") + "</div>");
    Response.Write(this.ERROR);
    if (this.INFO == "OK")
    {
        Response.Write("<div class=\"tip\">");
        Response.Write("<b>" + this.GetLang("清空成功！|清空成功！|Deleted successfully!") + "</b><br/>");
        Response.Write("</div>");
    }
    else
    {
        if (this.INFO == "PASSERROR")
        {
            Response.Write("<div class=\"tip\">");
            Response.Write("<b>" + this.GetLang("密码错误！|密码错误！|NO PASS !") + "</b><br/>");
            Response.Write("</div>");
        }
        Response.Write("<div class=\"content\">");
        if (!string.IsNullOrEmpty(this.INFO) && this.INFO != "PASSERROR")
        {
            Response.Write("<b>" + this.INFO + "</b><br/>");
        }
        if (this.INFO != "不允许清空此用户的回复") // 添加条件判断
        {
            Response.Write("<form name=\"f\" action=\"" + http_start + "bbs/book_re_delmy.aspx\" method=\"post\">");
            Response.Write("清空理由 <input type=\"text\" name=\"why\" value=\"" + why + "\" /><br/>");
            Response.Write("我的密码 <input type=\"text\" name=\"pw\" value=\"" + pw + "\" /><br/>");
            Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
            Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
            Response.Write("<input type=\"hidden\" name=\"reid\" value=\"" + (this.reid) + "\" />");
            Response.Write("<input type=\"hidden\" name=\"lpage\" value=\"" + (this.lpage) + "\" />");
            Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + (this.page) + "\" />");
            Response.Write("<input type=\"hidden\" name=\"ot\" value=\"" + (this.ot) + "\" />");
            Response.Write("<input type=\"hidden\" name=\"touserid\" value=\"" + (this.touserid) + "\" />");
            Response.Write("1.清空TA的所有回复（不清币和经验）<br/>");
            Response.Write("<input type=\"submit\" name=\"action\" value=\"Del_1\"/><br/>");
            Response.Write("2.清空TA的所有回复（清币）<br/>");
            Response.Write("<input type=\"submit\" name=\"action\" value=\"Del_2\"/><br/>");
            Response.Write("3.清空TA的所有回复（清币+清经验值）<br/>");
            Response.Write("<input type=\"submit\" name=\"action\" value=\"Del_3\"/><br/>");
            Response.Write("</form>");
        }
        Response.Write("</div>");
    }
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    Response.Write("<a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;touserid=" + this.touserid + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</div></div>");
    Response.Write(WapTool.showDown(wmlVo));
%>