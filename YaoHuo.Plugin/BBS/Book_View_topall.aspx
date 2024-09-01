<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_topall.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_topall" %>
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
    Response.Write(WapTool.showTop(this.GetLang(lang1 + "全局置顶帖子|" + lang2 + "置顶貼子|" + lang3 + " Top Notes"), wmlVo));//显示头                                                                                                                                                             
if (ver == "1")
{
    strhtml.Append("<p>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_topall.aspx?action=gomod&amp;tops=" + this.tops + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定要" + lang1 + "置顶此帖吗？是！|確定要" + lang2 + "置顶此帖嗎？是！|Are you sure? YES") + "</a><br/>");
    }
    else if(this.INFO=="ERR"){
        strhtml.Append("<b>" + this.GetLang(lang1 + "置顶失败！|" + lang2 + "置顶失敗！|" + lang3 + " TOP Failure!") + "</b><br/>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<b>" + this.GetLang(lang1 + "置顶成功！|" + lang2 + "置顶成功！|" + lang3 + " TOP successfully!") + "</b><br/>");
    }
    strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to subject") + "</a>");
    strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
    strhtml.Append("</p>");
    Response.Write(strhtml);
}
else //2.0界面
{
    strhtml.Append("<div class=\"title\">" + this.GetLang(lang1 + "全局置顶帖子|" + lang2 + "置顶貼子|" + lang3 + " Top Notes") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_topall.aspx?action=gomod&amp;tops=" + this.tops + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("确定要" + lang1 + "全局置顶此帖吗？是！|確定要" + lang2 + "置顶此帖嗎？是！|Are you sure? YES") + "</a><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "ERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "置顶失败！|" + lang2 + "置顶失敗！|" + lang3 + " TOP Failure!") + "</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>" + this.GetLang(lang1 + "置顶成功！|" + lang2 + "置顶成功！|" + lang3 + " TOP successfully!") + "</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
        strhtml.Append("</div>");
    }
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
    if (isWebHtml != "")
    {
        //string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml,wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>