<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_end.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_end" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    string lang1 = "";
    string lang2 = "";
    string lang3 = "";
    if (tops == "0")
    {
        lang1 = "取消";
        lang2 = "取消";
        lang3 = "Cancel";
    }
    Response.Write(WapTool.showTop(this.GetLang(lang1 + "结束帖子|" + lang2 + "结束貼子|" + lang3 + " Lock"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang(lang1 + "结束帖子|" + lang2 + "结束貼子|" + lang3 + " Lock Notes") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        if (tops == "2")
        {
            strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_end.aspx\" method=\"post\">");
            strhtml.Append("输入结帖原因 <br/><input type=\"text\" style=\"width:80%;\" name=\"whylock\" value=\" 已结束 " + this.whylock + "\"/><br/>");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + this.lpage + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"tops\" value=\"2\"/>");
            strhtml.Append("<input type=\"submit\" name=\"g\" value=\"确 定\"/>");
            strhtml.Append("</form>");
        }
        else
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_end.aspx?action=gomod&amp;tops=" + this.tops + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定要" + lang1 + "结束此帖吗？是！|確定要" + lang2 + "结束此帖嗎？是！|Are you sure? YES") + "</a><br/>");
        }
        strhtml.Append("</div>");
    }
    else if (this.INFO == "ERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "结束失败！|" + lang2 + "结束失敗！|" + lang3 + " TOP Failure!") + "</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "结束成功！|" + lang2 + "结束成功！|" + lang3 + " TOP successfully!") + "</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>