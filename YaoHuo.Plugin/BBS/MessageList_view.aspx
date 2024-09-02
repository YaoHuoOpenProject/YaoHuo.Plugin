<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList_View.aspx.cs" Inherits="YaoHuo.Plugin.BBS.MessageList_view" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("查看信息|查看信息|view Message"), wmlVo));//显示头                                                                                                                                                                       
Response.Write("<script type=\"text/javascript\" defer src=\"/NetCSS/JS/HyperLink.js\"></script>");
if (ver == "1")
{
    Response.Write("<p>");
    Response.Write(this.ERROR);
    Response.Write("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a>.<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a><br/>");
    Response.Write("<b>"+bookVo.title+"</b><br/>");
    Response.Write("<b>发件人:</b><a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + bookVo.userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/messagelist.aspx?siteid="+this.siteid+"&amp;classid=0&amp;types="+this.types+"&amp;issystem="+this.issystem) + "" + "\">" + bookVo.nickname + "</a><br/>");
    Response.Write("<b>时间:</b>" + bookVo.addtime + "<br/>");
    Response.Write("<b>内容:</b>" + WapTool.ToWML(bookVo.content,wmlVo) + "<br/>");
    Response.Write("<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;touserid=" + bookVo.userid + "" + "\">回复/转发</a>.");
    Response.Write("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">删除本条</a><br/>");
    //Response.Write("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delother&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">删除我跟此用户会话</a><br/>");
    Response.Write("----------<br/>");
    Response.Write("聊天模式【");
    if (this.isclose != "1")
    {
        Response.Write("<a href=\"" + http_start + "bbs/messagelist_view.aspx?isclose=1&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">关闭</a>");
    }
    else
    {
        Response.Write("<a href=\"" + http_start + "bbs/messagelist_view.aspx?isclose=0&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">开启</a>");
    }
    Response.Write(".<a href=\"" + http_start + "bbs/messagelist_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">刷新</a>");
    Response.Write("】<br/>");  
    if (bookVo.isnew == 2)
    {
        Response.Write("<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;touserid=" + bookVo.userid + "&amp;id=" + this.id + "" + "\">重发/转发</a><br/>");
    }
    else
    {
        if (this.needpwFlag == "1")
        {
            Response.Write("回复内容*:<input type=\"text\"  name=\"content" + r + "\" value=\"\" size=\"8\" />");
            Response.Write("<br/>我的密码*:<input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" size=\"10\" /><br/>");
        }
        else
        {
            Response.Write("<input type=\"text\"  name=\"content" + r + "\" value=\"\" size=\"8\" />");
        }
        Response.Write("<anchor><go href=\"" + http_start + "bbs/messagelist_add.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        Response.Write("<postfield name=\"action\" value=\"gomod\"/>");
        Response.Write("<postfield name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<postfield name=\"types\" value=\"" + types + "\"/>");
        Response.Write("<postfield name=\"issystem\" value=\"" + issystem + "\"/>");
        Response.Write("<postfield name=\"backurl\" value=\"" + backurl + "\"/>");
        Response.Write("<postfield name=\"toid\" value=\"" + id + "\"/>");
        Response.Write("<postfield name=\"title\" value=\"\"/>");
        Response.Write("<postfield name=\"content\" value=\"$(content" + r + ")\"/>");
        Response.Write("<postfield name=\"needpw\" value=\"$(needpw)\"/>");
        Response.Write("<postfield name=\"touseridlist\" value=\"" + bookVo.userid + "\"/>");
        //Response.Write("<postfield name=\"sid\" value=\"" + sid + "\"/>");
        Response.Write("</go>" + this.GetLang("速回|速回|Send") + "</anchor><br/>");
        StringBuilder html = new StringBuilder();
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            //html.Append((i + 1) + "." + listVo[i].addtime + "<br/>");
            //html.Append("标题:" + listVo[i].title + "<br/>");
            if (listVo[i].touserid.ToString() != this.userid)
            {
                html.Append("<b>我:</b>" + listVo[i].content + "<br/>");
            }
            else
            {
                html.Append("<b><a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode(this.GetUrlQueryString()) + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a>:</b>" + listVo[i].content + "<br/>");
            }
            html.Append(listVo[i].addtime + "<br/>----------<br/>");
        }
    if (html.ToString()!="")
    {
        Response.Write(WapTool.ToWML(html.ToString(),wmlVo));
    }
    }
    if (this.isclose != "1")
    {
        Response.Write("显示最近20条聊天记录<br/>");
    }
    Response.Write("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a> ");
    Response.Write("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    Response.Write(WapTool.GetVS(wmlVo));
    Response.Write("</p>");
}
else //2.0界面
{
    Response.Write("<link rel=\"stylesheet\" href=\"/NetCSS/CSS/ChatMessage.css\">");
    Response.Write("<div class=\"title\"><a href=\"/\">首页</a>><a href=\"/bbs/messagelist.aspx\">信箱</a>>" + this.GetLang("查看信息|查看信息|view Message") + "</div>");
    //Response.Write("<div class=\"subtitle\">");
    //Response.Write("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a>.<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a><br/>");
    //Response.Write("</div>");
    Response.Write("<div style=\"padding: 7px 8px 0px 8px;\" class=\"content\">");
    Response.Write("<b>" + bookVo.title + "</b><br/>");
    Response.Write("<b>发件人：</b><a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + bookVo.userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/messagelist.aspx?siteid=" + this.siteid + "&amp;classid=0&amp;types=" + this.types+"&amp;issystem="+this.issystem) + "" + "\">" + bookVo.nickname + "</a><br/>");
    Response.Write("<b>时间：</b>" + string.Format("{0:yyyy/M/d HH:mm}", bookVo.addtime) + "<br/>");
    Response.Write("<b>内容：</b><span class=\"retext\">" + WapTool.ToWML(bookVo.content, wmlVo) + "</span><br/>");
    //Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;touserid=" + bookVo.userid + "" + "\">回复/转发</a>.");
    //Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">删除本条</a><br/>");
    //Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_del.aspx?action=delother&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">删除我跟此用户会话</a><br/>");
    Response.Write("</div>");
    //Response.Write("<div class=\"subtitle\">");
    //Response.Write("聊天模式【");
    //Response.Write("【");
    if (this.isclose != "1")
    {
        //Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_view.aspx?isclose=1&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">关闭聊天</a>");
    }
    else
    {
        Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_view.aspx?isclose=0&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">开启聊天</a><br/>");
    }
    //Response.Write("<span class=\"separate2\"> </span><a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_del.aspx?action=delother&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">删除对话</a>");
    //Response.Write("<span class=\"separate2\"> </span><a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + this.id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.page + "&amp;r=" + r + "" + "\">刷新</a>");
    //Response.Write("】<br/>");
    //Response.Write("</div>");
    Response.Write("<div class=\"content\">");
    if (bookVo.isnew == 2)
    {
        Response.Write("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=2&amp;issystem="+this.issystem+"&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;touserid=" + bookVo.userid + "&amp;id=" + this.id + "" + "\">重发/转发</a><br/>");
    }
    else
    {
        Response.Write("<script type=\"text/javascript\" src=\"/NetCSS/JS/Message.js\"></script>");
        Response.Write("<form name=\"f\" action=\"" + http_start + "bbs/messagelist_add.aspx\" method=\"post\">");
        if (this.needpwFlag == "1")
        {
            Response.Write("回复内容：<input type=\"text\"  name=\"content\" value=\"\" size=\"8\" />");
            Response.Write("<br/>我的密码：<input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" size=\"10\" /><br/>");
        }
        else
        {
           Response.Write("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
           Response.Write("<div class='centered-container'>");
           Response.Write("<textarea name=\"content\" oninput=\"adjustTextareaHeight(this)\" rows=\"5\" style=\"width:98.6%;margin-bottom:2px;\"></textarea>");
           Response.Write("</div>");
        }
        Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"types\" value=\"" + types + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"issystem\" value=\"" + issystem + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"toid\" value=\"" + id + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"title\" value=\"\"/>");
        Response.Write("<input type=\"hidden\" name=\"touseridlist\" value=\"" + bookVo.userid + "\"/>");
        //Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
        Response.Write("<input type=\"submit\" name=\"g\" style=\"margin-right:10px;\" class=\"btn\" value=\"发送消息\"/></form><br/>");
        Response.Write("<div style=\"padding: 6px;\"></div>");
        //Response.Write("</div>");
        Response.Write("<div class=\"mmscontent\">");
        StringBuilder html = new StringBuilder();
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            //html.Append((i + 1) + "." + listVo[i].addtime + "<br/>");
            //html.Append("标题:" + listVo[i].title + "<br/>");
            if (listVo[i].touserid.ToString() != this.userid)
            {
                //html.Append("<b>我:</b>" + listVo[i].content + "<br/>");
                html.Append("<div class=\"listmms the_me\">");
                html.Append("<div class=\"bubble\">");
                html.Append("<div class=\"con\">");
                html.Append(listVo[i].content);
                html.Append("</div>");
                html.Append("</div>");
                html.Append("<div class=\"info\"><div class=\"u_name\">");
                html.Append("我 ");
                html.Append("<label>");
                html.Append("" + string.Format("{0:yyyy/M/d HH:mm}", listVo[i].addtime) + "");
                html.Append("</label>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
            }
            else
            {
                html.Append("<div class=\"listmms the_user\">");
                html.Append("<div class=\"bubble\">");
                html.Append("<div class=\"con\">");
                html.Append(listVo[i].content);
                html.Append("</div>");
                html.Append("</div>");
                html.Append("<div class=\"info\"><div class=\"u_name\">");
                html.Append("<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode(this.GetUrlQueryString()) + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a>");
                html.Append("<label>");
                html.Append("" + string.Format("{0:yyyy/M/d HH:mm}", listVo[i].addtime) + "");
                html.Append("</label>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                //html.Append("<b><a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode(this.GetUrlQueryString()) + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a>:</b>" + listVo[i].content + "<br/>");
            }
            //html.Append(listVo[i].addtime + "<br/>----------<br/>");
        }
        if (html.ToString() != "")
        {
            Response.Write(WapTool.ToWML(html.ToString(), wmlVo));
        }
    }
    if (this.isclose != "1")
    {
        Response.Write("<div class=\"line1\">显示最近20条聊天记录</div>");
    }
    Response.Write("</div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    //Response.Write("<a class=\"noafter\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a> <a class=\"noafter\" href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a><br/>");
    Response.Write("<a class=\"noafter\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a>");
    //Response.Write("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem="+this.issystem+"&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "" + "\">返回上级</a> ");
    Response.Write("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    Response.Write("</div></div>");  
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>