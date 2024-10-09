<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_re_addfileShow.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_re_addfileShow" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("查看回复上传文件|查看回复上传文件|see upfile"), wmlVo));
    Response.Write("<script type=\"text/javascript\" defer src=\"/NetCSS/JS/HyperLink.js\"></script>");
    Response.Write("<div class=\"title\">" + this.GetLang("查看回复上传文件|查看回复上传文件|see upfile") + "</div>");
    Response.Write(this.ERROR);
    Response.Write("<div class=\"content\">");
    Response.Write("<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + bbsReVo.userid + "&amp;backurl=" + "\">" + bbsReVo.nickname + "(" + bbsReVo.userid + ")</a>说:");
    Response.Write("<span class=\"retext\">");
    Response.Write(WapTool.ToWML(bbsReVo.content, wmlVo));
    Response.Write("</span></div>");
    Response.Write("<div class=\"content\">");
    if (bbsReVo.isdown > 0)
    {
        Response.Write("共有" + bbsReVo.isdown + "个附件：<br/>");
        for (int i = 0; (dlist != null && i < dlist.Count); i++)
        {
            Response.Write((i + 1) + "." + dlist[i].book_title);
            Response.Write("." + dlist[i].book_ext + "(" + dlist[i].book_size + ")<br/>");
            if (".gif|.jpg|.jpeg|.png|.bmp|".IndexOf(dlist[i].book_ext.ToLower()) >= 0)
            {
                //Response.Write("<a href=\"" + this.http_start + "bbs/picDIY.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;path=" + HttpUtility.UrlEncode(@"bbs\" + dlist[i].book_file) + "\"><img class=\"repic\" src=\"" + this.http_start + "bbs/" + dlist[i].book_file + "\" alt=\"" + dlist[i].book_title + "\"/></a><br/>");
                Response.Write("<img class=\"repic\" src=\"" + this.http_start + "bbs/" + dlist[i].book_file + "\" alt=\"" + dlist[i].book_title + "\"/><br/>");
            }
            else
            {
                Response.Write("<a href=\"" + this.http_start + "bbs/download.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;book_id=" + this.reid + "&amp;id=" + dlist[i].ID + "&amp;RndPath=" + siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(dlist[i].book_title) + "." + dlist[i].book_ext + "\">点击下载</a>(" + dlist[i].book_click + "次)<br/>");
            }
            Response.Write(dlist[i].book_content + "<br/>");
        }
    }
    Response.Write("</div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to list") + "</a> ");
    Response.Write("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</div></div>");
    Response.Write(WapTool.showDown(wmlVo));
%>