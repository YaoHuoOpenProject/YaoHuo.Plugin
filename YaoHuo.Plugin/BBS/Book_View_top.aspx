<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_top.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_top" %>
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
    Response.Write(WapTool.showTop(this.GetLang(lang1 + "置顶帖子|" + lang2 + "置顶貼子|" + lang3 + " Top Notes"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang(lang1 + "置顶帖子|" + lang2 + "置顶貼子|" + lang3 + " Top Notes") + "</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "")
    {
        strhtml.Append("<div class=\"content\">");
        if (bookVo.book_top == 0)
        {
            // 帖子未置顶，显示置顶选项
            string normalTopMessage = this.GetLang("普通置顶|普通置頂|Normal Top");
            string globalTopMessage = this.GetLang("全局置顶|全局置頂|Global Top");

            strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_top.aspx?action=gomod&amp;tops=1&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;topType=normal\">" + normalTopMessage + "</a><br /><br />");
            strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_top.aspx?action=gomod&amp;tops=1&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;topType=all\">" + globalTopMessage + "</a><br />");
        }
        else
        {
            // 帖子已置顶，显示取消置顶选项
            string cancelTopMessage = bookVo.book_top == 1 ?
                this.GetLang("取消普通置顶|取消普通置頂|Cancel Normal Top") :
                this.GetLang("取消全局置顶|取消全局置頂|Cancel Global Top");

            strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_top.aspx?action=gomod&amp;tops=0&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + cancelTopMessage + "</a><br />");
        }
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
        strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a>");
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