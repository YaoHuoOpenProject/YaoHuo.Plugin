<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumList.aspx.cs" Inherits="YaoHuo.Plugin.Album.AlbumList" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    string title = "";
    StringBuilder strhtml = new StringBuilder();
    if (smalltypeid == "0")
    {
        title = this.GetLang("我的相册||");
    }
    else if(bookVo!=null)
    {
        title = bookVo.subjectname;
    }
    Response.Write(WapTool.showTop(title, wmlVo)); //显示头
    if (this.touserid == this.userid)
    {
    strhtml.Append("<div class=\"title\">" + title + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<div class=\"bt1\"><a href=\"" + this.http_start + "album/admin_WAPadd.aspx?siteid=" + this.siteid + "&amp;classid=0&amp;smalltypeid=" + this.smalltypeid + "" + "\">上传相片</a></div>");
    strhtml.Append("<div class=\"content\"><form name=\"f\" action=\"" + http_start + "album/albumlist.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"text\" style=\"width:50%;max-width:200px;height:19px;\" name=\"key\" value=\"" + key + "\" size=\"5\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\" />");
    strhtml.Append("<input type=\"hidden\" name=\"smalltypeid\" value=\"" + (this.smalltypeid) + "\" />");
    strhtml.Append("<input type=\"submit\" name=\"f\" value=\"" + this.GetLang("搜索相册|搜索|Search") + "\"/>");
    strhtml.Append("</form></div>");
    //strhtml.Append(linkTOP);
    //显示列表
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        //index = index + kk;
        //strhtml.Append(index + ".");
        if (listVo[i].ishidden == 1)
        {
            //strhtml.Append("[隐]");
        }
        strhtml.Append("<a href=\"" + http_start + "album/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "" + "\">" + listVo[i].book_title + "</a> [<a class=\"urlbtn\" href=\"" + http_start + "album/albumlist_del.aspx?action=del&amp;siteid=" + this.siteid + "&amp;classid=0&amp;smalltypeid=" + this.smalltypeid + "&amp;id=" + listVo[i].id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>]<br/>");
        if (this.touserid == this.userid)
        {
            //strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "album/albumlist_mod.aspx?action=del&amp;siteid=" + this.siteid + "&amp;classid=0&amp;smalltypeid=" + this.smalltypeid + "&amp;id=" + listVo[i].id + "&amp;backurl=&amp;page=" + this.CurrentPage + "" + "\">修改</a>");
            //strhtml.Append("<span class=\"right\">上传于" + string.Format("{0:MM-dd HH:mm}", listVo[i].book_date) + "</span>");
        }
        strhtml.Append("<p align=\"left\"><img src=\"" + this.http_start + "album/" + listVo[i].book_img + "\"/></p></div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">没有记录</div>");
    }
    //显示导航分页
    strhtml.Append("</div>");
    strhtml.Append(linkURL);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"/myfile.aspx\">返回上级</a>");
    strhtml.Append("<a href=\"/\">返回首页</a>");
    strhtml.Append("</div></div>");
    }
    string isWebHtml = this.ShowWEB_list(this.classid); //看是存在html代码
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    Response.Write(strhtml);
Response.Write(ERROR);                                                                                                                                                                              
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>