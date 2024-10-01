<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_good.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_good" %>
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
    Response.Write(WapTool.showTop(this.GetLang(lang1 + "加精帖子|" + lang2 + "加精貼子|" + lang3 + " Top Good"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang(lang1 + "加精帖子|" + lang2 + "加精貼子|" + lang3 + " Top Notes") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_good.aspx?action=gomod&amp;tops=" + this.tops + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定要" + lang1 + "加精此帖吗？是！|確定要" + lang2 + "加精此帖嗎？是！|Are you sure? YES") + "</a><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "ERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "加精失败！|" + lang2 + "加精失敗！|" + lang3 + " TOP Failure!") + "</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "加精成功！|" + lang2 + "加精成功！|" + lang3 + " TOP successfully!") + "</b> ");
        strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
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