<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View.aspx.cs" Inherits="YaoHuo.Plugin.Games.ChuiNiu.Book_View" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("查看状态|查看状态|view state"), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        Response.Write("<p>");
        Response.Write(this.ERROR);

        Response.Write("您目前共有" + userVo.money + "个" + siteVo.sitemoneyname + "！<br/>");
        Response.Write("应战ID:" + bookVo.id + "<br/>");
        Response.Write("挑战者:" + bookVo.nickName + "<br/>");
        Response.Write("赌注是:" + bookVo.myMoney + "个" + siteVo.sitemoneyname + "<br/>");
        Response.Write("问题是:" + bookVo.Question + "<br/>");
        Response.Write("答案1:" + bookVo.Answer1 + "<br/>");
        Response.Write("答案2:" + bookVo.Answer2 + "<br/>");
        Response.Write("发起时间:" + bookVo.addtime + "<br/>");
        if (bookVo.state == 0)
        {
            Response.Write("状态:进行中！<br/>");
        }
        else
        {
            Response.Write("挑战方出的是[答案" + bookVo.myAnswer + "]<br/>");
            Response.Write("应战者:" + bookVo.winNickname + "<br/>");
            Response.Write("应战出的是[答案" + bookVo.winAnswer + "]<br/>");
            //if (type == "0")
            //{
            //Response.Write("对挑战方状态:");
            //}
            //else
            //{
            Response.Write("对应战方状态:");
            //}
            Response.Write("<b>");

            if (bookVo.state == 3)
            {
                Response.Write("平局!");
            }
            else if (bookVo.state == 1)
            {
                Response.Write("获胜!");
            }
            else if (bookVo.state == 2)
            {
                Response.Write("失败!");
            }

            Response.Write("</b><br/>");
        }

        Response.Write("<a href=\"" + this.http_start + "games/chuiniu/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;type=" + type + "&amp;touserid=" + touserid + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
        Response.Write("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回首页|返回首页|Back to list") + "</a>");
        Response.Write(WapTool.GetVS(wmlVo));
        Response.Write("</p>");
    }
    else //2.0界面
    {
        Response.Write("<div class=\"subtitle\">" + this.GetLang("查看状态|查看状态|view state") + "</div>");

        Response.Write(this.ERROR);

        Response.Write("<div class=\"content\">");
        Response.Write("您目前共有" + userVo.money + "个" + siteVo.sitemoneyname + "！<br/>");
        Response.Write("应战ID:" + bookVo.id + "<br/>");
        Response.Write("挑战者:" + bookVo.nickName + "<br/>");
        Response.Write("赌注是:" + bookVo.myMoney + "个" + siteVo.sitemoneyname + "<br/>");
        Response.Write("问题是:" + bookVo.Question + "<br/>");
        Response.Write("答案1:" + bookVo.Answer1 + "<br/>");
        Response.Write("答案2:" + bookVo.Answer2 + "<br/>");
        Response.Write("发起时间:" + bookVo.addtime + "<br/>");
        if (bookVo.state == 0)
        {
            Response.Write("状态:进行中！<br/>");
        }
        else
        {
            Response.Write("挑战方出的是[答案" + bookVo.myAnswer + "]<br/>");
            Response.Write("应战者:" + bookVo.winNickname + "<br/>");
            Response.Write("应战出的是[答案" + bookVo.winAnswer + "]<br/>");
            //if (type == "0")
            //{
            //    Response.Write("对挑战方状态:");
            //}
            //else
            //{
            Response.Write("对应战方状态:");
            //}

            Response.Write("<b>");

            if (bookVo.state == 3)
            {
                Response.Write("平局!");
            }
            else if (bookVo.state == 1)
            {
                Response.Write("获胜!");
            }
            else if (bookVo.state == 2)
            {
                Response.Write("失败!");
            }

            Response.Write("</b><br/>");
        }
        Response.Write("</div>");

        Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
        Response.Write("<a href=\"" + this.http_start + "games/chuiniu/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;type=" + type + "&amp;touserid=" + touserid + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
        Response.Write("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回首页|返回首页|Back to list") + "</a>");
        Response.Write("</div></div>");
    }
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>
