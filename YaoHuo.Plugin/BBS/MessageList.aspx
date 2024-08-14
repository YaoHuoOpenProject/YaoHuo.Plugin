<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="YaoHuo.Plugin.BBS.MessageList" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    string msgbox = "";
    if (types == "0")
    {
        msgbox = "收件箱|收件箱|Receive";
    }
    else
    {
        msgbox = "发件箱|发件箱|Send";
    }

    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang(msgbox), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p align=\"left\">");
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">[返回源来页]</a><br/>");
        strhtml.Append("<input type=\"text\" name=\"key\" value=\"" + key + "\" size=\"8\"/>");
        strhtml.Append("<anchor><go href=\"" + http_start + "bbs/messagelist.aspx\" method=\"get\" accept-charset=\"utf-8\">");
        strhtml.Append("<postfield name=\"action\" value=\"class\" />");
        strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<postfield name=\"key\" value=\"$(key)\" />");
        strhtml.Append("<postfield name=\"backurl\" value=\"" + (backurl) + "\" />");
        strhtml.Append("<postfield name=\"types\" value=\"" + (types) + "\" />");
        strhtml.Append("<postfield name=\"issystem\" value=\"" + (issystem) + "\" />");
        strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\" />");
        strhtml.Append("</go>" + this.GetLang("搜索|搜索|Search") + "</anchor><br/>");
        strhtml.Append("---------<br/>");
        strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">发短信息</a>-");
        if (types == "0")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("发件箱|发件箱|Send") + "</a>");

            strhtml.Append("<br/>");
            strhtml.Append("收件箱[");
            if (issystem == "")
            {
                strhtml.Append("所有.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">收藏</a>");
            }
            else if (issystem == "0")
            {
                strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a>.聊天.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">收藏</a>");
            }
            else if (issystem == "1")
            {
                strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a>.系统.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">收藏</a>");
            }
            else if (issystem == "2")
            {
                strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a>.<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>.收藏");
            }
            strhtml.Append("]");
        }
        else
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("收件箱|收件箱|Receive") + "</a>");
        }

        strhtml.Append("<br/>");
        strhtml.Append(linkTOP);
        //显示列表
        //string YesOrNo = "";
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            index = index + kk;
            strhtml.Append(index + ".");
            if (listVo[i].title.Length > 20)
            {
                listVo[i].title = listVo[i].title.Substring(0, 20) + "...";
            }

            if (listVo[i].issystem == 1)
            {
                strhtml.Append("<img src=\"" + http_start + "NetImages/msg.png\" alt=\"系统\"/>");
            }
            if (listVo[i].isnew == 1)
            {
                strhtml.Append("<img src=\"" + http_start + "NetImages/new.gif\" alt=\"新\"/>");
            }

            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + listVo[i].id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.CurrentPage + "" + "\">" + listVo[i].title + "</a>来自" + listVo[i].nickname);

            strhtml.Append("<br/>" + listVo[i].addtime + "");
            strhtml.Append(" [<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>");
            if (types != "2" && issystem != "2")
            {
                strhtml.Append(".<a href=\"" + http_start + "bbs/messagelist.aspx?action=save&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">收藏</a>");
            }
            strhtml.Append("]");
            strhtml.Append("<br/>");

        }
        if (listVo == null)
        {
            strhtml.Append("暂无短信！<br/>");
        }
        //显示导航分页
        strhtml.Append(linkURL);

        strhtml.Append("----------<br/>");

        if (types == "0")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空系统消息</a><br />");
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空聊天消息</a><br />");
            //strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空收藏消息</a><br />");
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=3&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">一键阅读消息</a><br />");
        }
        strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清所有" + this.GetLang(msgbox) + "</a><br/>");

        strhtml.Append("---------<br/>");
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a>.");

        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</p>");

        //输出
        Response.Write(strhtml);
    }
    else //2.0界面
    {
        strhtml.Append("<div class=\"subtitle\">" + this.GetLang(msgbox) + "");
        //strhtml.Append("<a class=\"urlbtn\" href=\"" + this.http_start + (backurl) + "" + "\">[返回源来页]</a><br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/messagelist.aspx\" method=\"post\">");
        strhtml.Append("关键字 <input type=\"text\" name=\"key\" value=\"" + key + "\" size=\"25\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"types\" value=\"" + (types) + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
        strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("搜索|搜索|Search") + "\"/><br/>");
        strhtml.Append("</form>");

        strhtml.Append("</div>");
        strhtml.Append("<div class=\"subtitle\">");
        strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">发短信息</a>-");
        if (types == "0")
        {
            strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("发件箱|发件箱|Send") + "</a>");

            strhtml.Append("</div><div class=\"content\">");
            strhtml.Append("收件箱 [");
            if (issystem == "")
            {
                strhtml.Append("所有 <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a> <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>");
            }
            else if (issystem == "0")
            {
                strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a> <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a> 聊天");
            }
            else if (issystem == "1")
            {
                strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a> 系统 <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>");
            }
            else if (issystem == "2")
            {
                strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">所有</a> <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">系统</a> <a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">聊天</a>");
            }
            strhtml.Append("]");
        }
        else
        {
            strhtml.Append("<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "" + "\">" + this.GetLang("收件箱|收件箱|Receive") + "</a>");
        }
        strhtml.Append("</div>");

        strhtml.Append(linkTOP);
        //显示列表
        //string YesOrNo = "";
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml.Append("<div class=\"listmms line1\">");
            }
            else
            {
                strhtml.Append("<div class=\"listmms line2\">");
            }
            index = index + kk;
            strhtml.Append(index + ".");
            if (listVo[i].title.Length > 20)
            {
                listVo[i].title = listVo[i].title.Substring(0, 20) + "...";
            }

            if (listVo[i].issystem == 1)
            {
                strhtml.Append("<img src=\"" + http_start + "NetImages/msg.png\" alt=\"系统\"/>");
            }
            if (listVo[i].isnew == 1)
            {
                strhtml.Append("<img src=\"" + http_start + "NetImages/new.gif\" alt=\"新\"/>");
            }

            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;id=" + listVo[i].id + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;page=" + this.CurrentPage + "" + "\">" + listVo[i].title + "</a>来自" + listVo[i].nickname);

            strhtml.Append("<br/>" + listVo[i].addtime + "");
            strhtml.Append(" [<a class=\"urlbtn\" href=\"" + http_start + "bbs/messagelist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;types=" + this.types + "&amp;issystem=" + this.issystem + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\" >删除</a>");
            if (types != "2" && issystem != "2")
            {

            }
            strhtml.Append("]");

            strhtml.Append("</div>");
        }
        if (listVo == null)
        {
            strhtml.Append("<div class=\"tip\">暂无短信！</div>");
        }
        //显示导航分页
        strhtml.Append(linkURL);

        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        if (isWebHtml != "")
        {
            //string strhtml_list = strhtml.ToString();
            //int s = strhtml_list.IndexOf("<div class=\"title\">");
            //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);

            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        if (types == "0")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=1&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空系统消息</a><br />");
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=0&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空聊天消息</a><br />");
            //strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=2&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空收藏消息</a><br />");
            strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_clear.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;issystem=3&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">一键阅读消息</a><br />");
        }
        strhtml.Append("<a href=\"" + http_start + "bbs/messagelist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + this.types + "&amp;issystem=&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清所有" + this.GetLang(msgbox) + "</a> ");
        strhtml.Append("</div></div>");

        strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a> ");

        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</div></div>");

        Response.Write(strhtml);
    }

    Response.Write(ERROR);
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>
