<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View.aspx.cs" Inherits="YaoHuo.Plugin.Album.Book_View" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(bookVo.book_title, wmlVo)); //显示头
    strhtml.Append("<!--web-->");
    //显示广告
    if (adVo.threeShowTop != "")
    {
        strhtml.Append(adVo.threeShowTop );
    }
    if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">设置头像成功，点击查看：<a style=\"font-size: unset;\" href=\"" + this.http_start + "bbs/userinfo.aspx?touserid="+this.userid+"\">我的空间</a></div>");
    }
    strhtml.Append("<div class=\"title\">" + bookVo.book_title +"</div>" );
    strhtml.Append("<div class=\"content\">");
    strhtml.Append(linkURL);
    strhtml.Append("<div id=\"KL_margin\"style=\"margin:8px;\"></div>");
    for (int i = 0; (filelist != null && i < filelist.Count); i++)
    {
        if (i == 0 && this.userid == bookVo.makerid.ToString())
        {
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a href=\"" + this.http_start + "album/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;action=addhead\">设为我的头像</a>");
            strhtml.Append("</div></div>");
        }
        strhtml.Append("<img src=\"" + this.http_start + "album/" + filelist[i].book_file + "\" alt=\"load...\"/><br/>");
    }
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        int s = strhtml_list.IndexOf("<!--web-->");
        strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    //显示广告
    if (adVo.threeShowDown != "")
    {
        strhtml.Append(adVo.threeShowDown);
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "album/albumlist.aspx?touserid="+this.userid+"\">返回列表</a>");
    strhtml.Append("<a href=\"/\">返回首页</a>");
    strhtml.Append("</div></div>");
    //输出错误
    strhtml.Append(ERROR);
    //输出
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(WapTool.showDown(wmlVo));
%>