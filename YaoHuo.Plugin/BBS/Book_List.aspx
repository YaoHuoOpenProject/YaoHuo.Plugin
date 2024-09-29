<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_List.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_List" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%@ Import Namespace="System.Data" %>
<%
    //论坛专题名称
    if (this.stype != "")
    {
        classVo.classname = this.stypename + "(" + classVo.classname + ")";
    }
    Response.Write(WapTool.showTop(classVo.classname, wmlVo));
    //会员可见
    if (this.IsCheckManagerLvl("|00|01|02|03|04|", "") == true)
    {
        string isWebHtml = this.ShowWEB_list(this.classid);
        StringBuilder strhtml_list = new StringBuilder();
        if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
        {
            strhtml.Append("<div class=\"logo\"><img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/></div>");
        }
        if (classVo.introduce != "")
        {
            strhtml.Append(classVo.introduce);
        }
        //显示查询某人的所有贴子
        if (this.type == "pub")
        {
            if (this.IsUserManager(this.userid, userVo.managerlvl, "") && this.key != "1000")
            {
                strhtml.Append("<div class=\"tip\"><a class=\"urlbtn\" onclick=\"return confirm('清空帖子前请先确认操作');\" href=\"" + this.http_start + "bbs/book_list_delmy.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.key + "\">清空(" + this.key + ")的所有贴子</a></div>");
            }
        }
        // 显示广告
        if (adVo.secondShowTop != "")
        {
            strhtml.Append(adVo.secondShowTop);
        }
        // 列表顶部按钮
        if ((this.action == "" || this.action == "class") && this.stype == "")
        {
            // 公共链接
            string commonLinks = "<a href=\"/wapindex.aspx?classid=206\">发帖</a> ";
            string commonEndLinks = string.Format("<a href=\"{0}bbs/book_list.aspx?action=good&amp;classid={1}\">精华</a> " +
                                    "<a href=\"{0}bbs/book_list.aspx?action=search&amp;classid={1}&amp;type=days&amp;key=365\">新帖</a>",
                                    this.http_start, this.classid);
            // 管理可见
            if (this.IsCheckManagerLvl("|00|01|03|04|", "") == true)
            {
                string adminLink = string.Format("<a href=\"{0}bbs/showadmin.aspx?classid={1}&amp;page={2}\">版务</a> ",
                                                 this.http_start, this.classid, this.CurrentPage);
                strhtml_list.Append("<div class=\"btBox\"><div class=\"bt4\">" + commonLinks + adminLink + commonEndLinks + "</div></div>");
            }
            // 会员可见
            else if (this.IsCheckManagerLvl("|02|", "") == true)
            {
                string searchLink = string.Format("<a href=\"{0}bbs/book_search.aspx?classid={1}\">搜索</a> ",
                                                  this.http_start, this.classid);
                strhtml_list.Append("<div class=\"btBox\"><div class=\"bt4\">" + commonLinks + searchLink + commonEndLinks + "</div></div>");
            }
            strhtml_list.Append("</div></div>");
        }
        //显示列表
        string lpagetemp = "";
        if (this.CurrentPage > 1)
        {
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                lpagetemp = "?lpage=" + CurrentPage;
            }
            else
            {
                lpagetemp = "&amp;lpage=" + CurrentPage;
            }
        }
        strhtml_list.Append("<!--listS-->");
        //置顶列表
        for (int i = 0; (listVoTop != null && i < listVoTop.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml_list.Append("<div class=\"listdata line1\">");
            }
            else
            {
                strhtml_list.Append("<div class=\"listdata line2\">");
            }
            if (listVoTop[i].book_top == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/ding.gif\" alt=\"顶\"/>");
            }
            else if (listVoTop[i].book_top == 2)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/top.gif\" alt=\"总顶\"/>");
            }
            if (listVoTop[i].topic != 0 && this.stype == "")
            {
            }
            if (listVoTop[i].book_click >= long.Parse(this.hots))
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/huo.gif\" alt=\"火\"/>");
            }
            if (listVoTop[i].book_good == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/jing.gif\" alt=\"精\"/>");
            }
            if (listVoTop[i].islock == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/suo.gif\" alt=\"锁\"/>");
            }
            if (listVoTop[i].islock == 2)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/jie.gif\" alt=\"结\"/>");
            }
            if (listVoTop[i].sendMoney > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/shang.gif\" alt=\"赏\"/>");
            }
            if (listVoTop[i].isVote > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/tuo.gif\" alt=\"投\"/>");
            }
            if (listVoTop[i].isdown == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/file.gif\" alt=\"附\"/>");
            }
            if (listVoTop[i].isdown == 2)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/d.gif\" alt=\"沉\"/>");
            }
            if (listVoTop[i].freeMoney > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/li.gif\" alt=\"礼\"/>");
            }
            if (titlecolor.Trim() != "")
            {
                listVoTop[i].book_title = "<font color=\"#" + titlecolor + "\">" + listVoTop[i].book_title + "</font>";
            }
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                strhtml_list.Append("<a class=\"topic-link\" href=\"" + http_start + "bbs-" + listVoTop[i].id + ".html" + lpagetemp + stypelink + "" + "\">" + listVoTop[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVoTop[i].book_pub), listVoTop[i].book_author) + "/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;getTotal=" + listVoTop[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVoTop[i].book_re + "</a>回/" + listVoTop[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVoTop[i].book_date) + "<span></div>");
            }
            else
            {
                strhtml_list.Append("<a href=\"" + http_start + "bbs/view.aspx?id=" + listVoTop[i].id + lpagetemp + stypelink + "" + "\">" + listVoTop[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVoTop[i].book_pub), listVoTop[i].book_author) + "/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;getTotal=" + listVoTop[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVoTop[i].book_re + "</a>回/" + listVoTop[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVoTop[i].book_date) + "<span></div>");
            }
        }
        //所有列表
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml_list.Append("<div class=\"listdata line1\">");
            }
            else
            {
                strhtml_list.Append("<div class=\"listdata line2\">");
            }
            index = index + kk;
            strhtml_list.Append(index + ".");
            //strhtml_list.Append("<span class=\"KL_num\">" +index + ".</span>");
            if (listVo[i].topic != 0 && this.stype == "")
            {
            }
            if (listVo[i].book_click >= long.Parse(this.hots))
            {
            }
            if (listVo[i].book_good == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/jing.gif\" alt=\"精\"/>");
            }
            if (listVo[i].islock == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/suo.gif\" alt=\"锁\"/>");
            }
            if (listVo[i].islock == 2)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/jie.gif\" alt=\"结\"/>");
            }
            if (listVo[i].sendMoney > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/shang.gif\" alt=\"赏\"/>");
            }
            if (listVo[i].isVote > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/tuo.gif\" alt=\"投\"/>");
            }
            if (listVo[i].isdown == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/file.gif\" alt=\"附\"/>");
            }
            if (listVo[i].isdown == 2)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/d.gif\" alt=\"沉\"/>");
            }
            if (listVo[i].freeMoney > 0)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/li.gif\" alt=\"礼\"/>");
            }
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                strhtml_list.Append("<a class=\"topic-link\" href=\"" + http_start + "bbs-" + listVo[i].id + ".html" + lpagetemp + stypelink + "" + "\">" + listVo[i].book_title + "</a><br/><span class=\"louzhunicheng\">" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "</span>/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");
            }
            else
            {
                strhtml_list.Append("<a class=\"topic-link\" href=\"" + http_start + "bbs/view.aspx?id=" + listVo[i].id + lpagetemp + stypelink + "" + "\">" + listVo[i].book_title + "</a><br/><span class=\"louzhunicheng\">" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "</span>/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");
            }
        }
        if (listVo == null)
        {
            strhtml_list.Append("<div class=\"tip\">暂无记录！</div>");
        }
        strhtml_list.Append("<!--listE-->");
        //显示导航分页
        strhtml_list.Append(linkURL);
        //显示固定按钮
        if ((this.action == "" || this.action == "class") && this.stype == "")
        {
            strhtml_list.Append("<div class=\"btBox\"><div class=\"bt4\"><a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">搜索</a> <a href=\"" + this.http_start + "bbs/Log_List.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">日志</a> <a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid=3&amp;siteid=" + this.siteid + "&amp;classid=0\">监狱</a> <a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;type=days&amp;key=365\">新帖</a></div></div>");
        }
        if (isWebHtml == "")
        {
            strhtml.Append(strhtml_list);
        }
        else  //显示电脑html
        {
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list.ToString()), wmlVo));
            Response.End();
        }
        //导航按钮
        if (action == "search")
        {
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "\">返回搜索</a> ");
            strhtml.Append("</div></div>");
        }
        else if (action == "good" && this.classid != "0")
        {
            strhtml.Append("<div class=\"bt1\">");
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=good&amp;siteid=" + siteid + "&amp;classid=0" + "\">全部精华</a> ");
            strhtml.Append("</div>");
        }
        if (downLink != "")
        {
            strhtml.Append(downLink);
        }
        else
        {
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
            }
            else
            {
            }
        }
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookList/TEST.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookList/KL_common.js\"></script>");
        //会员可见结束
    }
    //显示广告
    if (adVo.secondShowDown != "")
    {
        strhtml.Append(adVo.secondShowDown);
    }
    strhtml.Append(WapTool.GetVS(wmlVo));
    strhtml.Append(classVo.sitedowntip);
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>