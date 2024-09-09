<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_guestlistWAP.aspx.cs" Inherits="YaoHuo.Plugin.BBS.admin_guestlistWAP" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("论坛回帖|论坛回帖|re of the bbs"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    strhtml.Append("<div class=\"title\">" + classVo.classname + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/admin_userlistWAP.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "\">内容管理</a><span class=\"separate\"></span>回帖管理<br/>");
    strhtml.Append("<form id=\"form1\" action=\"" + http_start + "bbs/admin_guestlistWAP.aspx\" method=\"get\">");
    strhtml.Append("关键字 <input type=\"text\" name=\"key\" value=\"" + key + "\" style=\"width:60%\"/><br/>");
    strhtml.Append("栏目ID <input type=\"text\" name=\"classid\" value=\"" + classid + "\"  style=\"width:30%\"/><br/>");
    strhtml.Append("<input type=\"submit\" value=\"搜索\" />");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
    strhtml.Append("</form>");
    strhtml.Append("<br/>");
    strhtml.Append("</div>");
    //显示列表
    strhtml.Append(linkTOP);
    string YesOrNo = "";
    string CheckStr = "";
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
        if (listVo[i].ischeck.ToString() == "1")
        {
            YesOrNo="nono.gif";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">审核</a>]";
        }
        else
        {
            YesOrNo = "yes.gif";
            CheckStr = "[<a href=\"" + this.http_start + "bbs/admin_guestlistWAP.aspx?action=gocheck&amp;id=" + listVo[i].id + "&amp;state=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;tositeid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">取审</a>]";
        }
        strhtml.Append(index + ".<img src=\"" + this.http_start + "NetImages/" + YesOrNo + "\" alt=\"YES\"/>" + CheckStr + "[<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + listVo[i].devid + "&amp;classid=" + listVo[i].classid + "&amp;id=" + listVo[i].bookid + "\">看</a>]<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=\">" + listVo[i].nickname + "</a>:" + listVo[i].content + "<br/>(" + listVo[i].redate + ")</div>");
    }
    if (listVo==null)
    {
        strhtml.Append("<div class=\"tip\">暂无记录！</div>");
    }
    //显示导航分页
    strhtml.Append( linkURL );
    //导航按钮
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "admin/admin_wapClasslist.aspx?siteid=" + siteid + "&amp;gopathname=" + HttpUtility.UrlEncode("论坛类") + "&amp;gopath=bbs/index.aspx\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    //输出
    //Response.Write(strhtml);
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
}
Response.Write(ERROR);
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>