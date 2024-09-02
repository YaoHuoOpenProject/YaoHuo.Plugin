<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_List.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Report_List" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("举报管理|舉報管理|Report Management"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    strhtml.Append(ERROR);
    strhtml.Append("<div class=\"title\">");
    strhtml.Append(this.GetLang("举报管理|舉報管理|Report Management"));
    strhtml.Append("</div>");
    if (this.CheckManagerLvl("04", "") == true)
    {
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a class=\"noafter\" href=\"" + this.http_start + "bbs/Report_List.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=0\">查看所有</a><a href=\"" + this.http_start + "bbs/Report_List_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=0&amp;id=0\">删除所有</a> ");
        strhtml.Append("</div></div>");
    }
    //显示列表    
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        index = index + kk;
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        strhtml.Append(index + "." + listVo[i].addtime + "<br/>举报人：<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a><br/><a href=\"" + http_start + "bbs-" + listVo[i].bbsid + ".html\">查看帖子(ID:" + listVo[i].bbsid + " 栏目:" + listVo[i].classid + ")</a><br/>");
        strhtml.Append("类型：" + listVo[i].ReportType + "<br/>");
        strhtml.Append("备注：" + listVo[i].ReportWhy + "<br/>");
        strhtml.Append("操作：<a href=\"" + this.http_start + "bbs/Report_List_del.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].classid + "&amp;page=" + this.CurrentPage + "\">删除此举报</a>");
        strhtml.Append("</div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录！</div>");
    }
    //显示导航分页
    strhtml.Append(linkURL);
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/showadmin.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + "" + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>