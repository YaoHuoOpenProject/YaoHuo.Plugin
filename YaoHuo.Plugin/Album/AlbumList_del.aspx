<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumList_del.aspx.cs" Inherits="YaoHuo.Plugin.Album.AlbumList_del" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
StringBuilder strhtml=new StringBuilder ();
Response.Write(WapTool.showTop(this.GetLang("删除|删除|delete"), wmlVo)); //显示头
    strhtml.Append("<div class=\"title\">" + this.GetLang("删除|删除|delete") + "</div>");
    strhtml.Append(this.ERROR);
    strhtml.Append("<div class=\"tip\">");
    if (this.INFO == "")
    {
        strhtml.Append("<a href=\"" + http_start + "album/albumlist_del.aspx?action=go" + action + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;page=" + this.page + "&amp;smalltypeid=" + this.smalltypeid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("确定要删除吗？是！|確定要刪除嗎？是！|Are you sure? YES") + "</a><br/>");
    }
    else if (this.INFO == "OK")
    {
        strhtml.Append("<b>" + this.GetLang("删除成功！|刪除成功！|Deleted successfully!") + "</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    strhtml.Append("<a href=\"" + http_start + "album/albumlist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;smalltypeid=" + this.smalltypeid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a>");
    strhtml.Append("</div></div>");
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    Response.Write(strhtml);
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>