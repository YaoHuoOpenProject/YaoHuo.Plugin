<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_List.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_List" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%@ Import Namespace="System.Data" %>
<%
    //论坛专题名称
    if (this.stype != "")
    {
        classVo.classname = this.stypename + "(" + classVo.classname + ")";
    }
    Response.Write(WapTool.showTop(classVo.classname, wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p align=\"" + classVo.position + "\">");

        if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
        {
            strhtml.Append("<img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/><br/>");
        }
        if (classVo.introduce != "")
        {
            strhtml.Append(classVo.introduce + "<br/>");
        }

        //显示查询某人的所有帖子
        if (this.type == "pub")
        {
            if (this.CheckManagerLvl("04", "") == true)
            {
                strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_List_delmy.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.key + "\">清空(" + this.key + ")的所有帖子</a><br/>");
            }
        }

        //显示广告
        if (adVo.secondShowTop != "")
        {
            strhtml.Append(adVo.secondShowTop + "<br/>");
        }
        //显示固定按钮
        if ((this.action == "" || this.action == "class") && this.stype == "")
        {
            string addtypes = "bbs/book_view_add.aspx?";
            string ranktypes = "bbs/book_list_rank.aspx?";
            if (WapTool.getArryString(classVo.smallimg, '|', 39) != "")
            {
                addtypes = "bbs/marksix/doit.aspx?mymarksix=" + WapTool.getArryString(classVo.smallimg, '|', 39) + "&amp;";
                ranktypes = "bbs/marksix/rank.aspx?";
            }

            strhtml.Append("<a href=\"" + this.http_start + addtypes + "page=" + this.CurrentPage + "&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">发帖</a>.<a href=\"" + this.http_start + "bbs/showadmin.aspx?classid=" + this.classid + "&amp;siteid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">版务</a>.<a href=\"" + this.http_start + "bbs/book_list.aspx?action=good&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">精华</a>.<a href=\"" + this.http_start + "bbs/showTopic.aspx?classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">专题</a>.");

            strhtml.Append(" <a href=\"" + this.http_start + ranktypes + "classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">排行</a><br/>");

        }

        strhtml.Append("------------<br/>");
        //显示导航分页
        strhtml.Append(linkTOP);
        //显示列表
        string isBR = "";
        if (classVo.articlenum == 1)
        {
            isBR = "<br/>";
        }

        //置顶列表
        for (int i = 0; (listVoTop != null && i < listVoTop.Count); i++)
        {
            if (listVoTop[i].book_top == 1)
            {
                strhtml.Append("<b>[顶]</b>");
            }
            else if (listVoTop[i].book_top == 2)
            {
                strhtml.Append("<b>[总顶]</b>");
            }
            if (listVoTop[i].topic != 0 && this.stype == "")
            {
                strhtml.Append("<b>[专]</b>");
            }
            if (listVoTop[i].book_click >= long.Parse(this.hots))
            {
                strhtml.Append("<b>[火]</b>");
            }

            if (listVoTop[i].book_good == 1)
            {
                strhtml.Append("<b>[精]</b>");
            }
            if (listVoTop[i].islock == 1)
            {
                strhtml.Append("<b>[锁]</b>");
            }
            if (listVoTop[i].islock == 2)
            {
                strhtml.Append("<b>[结]</b>");
            }
            if (listVoTop[i].sendMoney > 0)
            {
                strhtml.Append("<b>[赏]</b>");
            }
            if (listVoTop[i].isVote > 0)
            {
                strhtml.Append("<b>[投]</b>");
            }
            if (listVoTop[i].isdown == 1)
            {
                strhtml.Append("<b>[附]</b>");
            }
            if (listVoTop[i].isdown == 2)
            {
                strhtml.Append("<b>[沉]</b>");
            }
            if (listVoTop[i].freeMoney > 0)
            {
                strhtml.Append("<b>[礼]</b>");
            }
            if (listVoTop[i].MarkSixWin == 1)
            {
                strhtml.Append("<b>[中]</b>");
            }
            strhtml.Append("<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;lpage=" + CurrentPage + stypelink + "" + "\">" + listVoTop[i].book_title + "</a>" + isBR + "[" + ShowNickName_color(long.Parse(listVoTop[i].book_pub), listVoTop[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;getTotal=" + listVoTop[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVoTop[i].book_re + "</a>回/" + listVoTop[i].book_click + "阅]<br/>");
        }
        //所有列表
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            index = index + kk;
            strhtml.Append(index + ".");
            //if (action != "" && action != "class")
            //{
            //   strhtml.Append(listVo[i].classname);
            //}

            //if (listVo[i].book_top == 1 && this.CurrentPage==1)
            //{
            //    strhtml.Append("<b>[顶]</b>");
            //}
            if (listVo[i].topic != 0 && this.stype == "")
            {
                strhtml.Append("<b>[专]</b>");
            }
            if (listVo[i].book_click >= long.Parse(this.hots))
            {
                strhtml.Append("<b>[火]</b>");
            }
            if (listVo[i].book_good == 1)
            {
                strhtml.Append("<b>[精]</b>");
            }
            if (listVo[i].islock == 1)
            {
                strhtml.Append("<b>[锁]</b>");
            }
            if (listVo[i].islock == 2)
            {
                strhtml.Append("<b>[结]</b>");
            }
            if (listVo[i].sendMoney > 0)
            {
                strhtml.Append("<b>[赏]</b>");
            }
            if (listVo[i].isVote > 0)
            {
                strhtml.Append("<b>[投]</b>");
            }
            if (listVo[i].isdown == 1)
            {
                strhtml.Append("<b>[附]</b>");
            }
            if (listVo[i].isdown == 2)
            {
                strhtml.Append("<b>[沉]</b>");
            }
            if (listVo[i].freeMoney > 0)
            {
                strhtml.Append("<b>[礼]</b>");
            }
            if (listVo[i].MarkSixWin == 1)
            {
                strhtml.Append("<b>[中]</b>");
            }
            strhtml.Append("<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;lpage=" + CurrentPage + stypelink + "" + "\">" + listVo[i].book_title + "</a>" + isBR + "[" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅]<br/>");
        }
        if (listVo == null)
        {
            strhtml.Append("暂无记录！<br/>");
        }
        strhtml.Append("------------<br/>");
        //显示导航分页
        strhtml.Append(linkURL);

        //显示固定按钮
        if ((this.action == "" || this.action == "class") && this.stype == "")
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">搜索</a>.<a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;type=days&amp;key=1\">今日</a>.<a href=\"" + this.http_start + "bbs/Log_List.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">日志</a>.<a href=\"" + this.http_start + "bbs/onlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">在线</a>.<a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid=3&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">监狱</a><br/>");
            strhtml.Append("------------<br/>");
        }

        //显示广告
        if (adVo.secondShowDown != "")
        {
            strhtml.Append(adVo.secondShowDown + "<br/>");
        }
        //导航按钮
        if (action == "search")
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "\">返回搜索</a><br/>");
        }
        else if (action == "good" && this.classid != "0")
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=good&amp;siteid=" + siteid + "&amp;classid=0" + "\">全部精华</a><br/>");

        }

        if (downLink != "")
        {
            strhtml.Append(downLink);
        }
        else
        {
            strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid + "" + "\">返回上级</a>-");
            strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        }
        strhtml.Append(WapTool.GetVS(wmlVo));


        if (classVo.sitedowntip != "")
        {
            strhtml.Append("<br/>" + classVo.sitedowntip);
        }

        strhtml.Append("</p>");
        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    }
    else //2.0界面
    {
        string isWebHtml = this.ShowWEB_list(this.classid); //看是存在html代码
        StringBuilder strhtml_list = new StringBuilder(); //[view]UBB对应代码
        if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
        {
            strhtml.Append("<div class=\"logo\"><img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/></div>");
        }
        if (classVo.introduce != "")
        {
            strhtml.Append(classVo.introduce);
        }

        //显示查询某人的所有帖子
        if (this.type == "pub")
        {
            if (this.IsUserManager(this.userid, userVo.managerlvl, "") && this.key != "1000") // 检查是否为管理员且ID不为1000
            {
                strhtml.Append("<div class=\"tip\"><a class=\"urlbtn\" href=\"" + this.http_start + "bbs/Book_List_delmy.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.key + "\">清空(" + this.key + ")的所有帖子</a></div>");
            }
        }

        //显示广告
        if (adVo.secondShowTop != "")
        {
            strhtml.Append(adVo.secondShowTop);
        }
        //显示固定按钮
        if ((this.action == "" || this.action == "class") && this.stype == "")
        {
            string addtypes = "bbs/book_view_add.aspx?";
            string ranktypes = "bbs/book_list_rank.aspx?";
            if (WapTool.getArryString(classVo.smallimg, '|', 39) != "")
            {
                addtypes = "bbs/marksix/doit.aspx?mymarksix=" + WapTool.getArryString(classVo.smallimg, '|', 39) + "&amp;";
                ranktypes = "bbs/marksix/rank.aspx?";
            }
            strhtml_list.Append("<div class=\"btBox\"><div class=\"bt5\"><a href=\"" + this.http_start + addtypes + "page=" + this.CurrentPage + "&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">发帖</a> <a href=\"" + this.http_start + "bbs/showadmin.aspx?classid=" + this.classid + "&amp;siteid=" + this.siteid + "&amp;page=" + this.CurrentPage + "\">版务</a> <a href=\"" + this.http_start + "bbs/book_list.aspx?action=good&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">精华</a> <a href=\"" + this.http_start + "bbs/showTopic.aspx?classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">专题</a> <a href=\"" + this.http_start + ranktypes + "classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">排行</a>");
            strhtml_list.Append("</div></div>");
        }

        //显示导航分页
        //strhtml_list.Append(linkTOP);
        //显示列表
        //if (classVo.articlenum == 1)
        //{
        //   isBR = "<br/>";
        //}
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
                strhtml_list.Append("<div class=\"line1\">");
            }
            else
            {
                strhtml_list.Append("<div class=\"line2\">");
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
                strhtml_list.Append("<b>[专]</b>");
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
            if (listVoTop[i].MarkSixWin == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/zhong.gif\" alt=\"中\"/>");
            }
            if (titlecolor.Trim() != "")
            {
                listVoTop[i].book_title = "<font color=\"#" + titlecolor + "\">" + listVoTop[i].book_title + "</font>";
            }
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                strhtml_list.Append("<a href=\"" + http_start + "bbs-" + listVoTop[i].id + ".html" + lpagetemp + stypelink + "" + "\">" + listVoTop[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVoTop[i].book_pub), listVoTop[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;getTotal=" + listVoTop[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVoTop[i].book_re + "</a>回/" + listVoTop[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVoTop[i].book_date) + "<span></div>");

            }
            else
            {
                strhtml_list.Append("<a href=\"" + http_start + "bbs/view.aspx?id=" + listVoTop[i].id + lpagetemp + stypelink + "" + "\">" + listVoTop[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVoTop[i].book_pub), listVoTop[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVoTop[i].book_classid + "&amp;id=" + listVoTop[i].id + "&amp;getTotal=" + listVoTop[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVoTop[i].book_re + "</a>回/" + listVoTop[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVoTop[i].book_date) + "<span></div>");
            }
        }
        //所有列表
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml_list.Append("<div class=\"line1\">");
            }
            else
            {
                strhtml_list.Append("<div class=\"line2\">");
            }

            index = index + kk;
            //strhtml_list.Append("<span class=\"KL_num\">" +index + ".</span>");

            ;

            if (listVo[i].topic != 0 && this.stype == "")
            {
                strhtml_list.Append("<b>[专]</b>");
            }
            if (listVo[i].book_click >= long.Parse(this.hots))
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/huo.gif\" alt=\"火\"/>");
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
            if (listVo[i].MarkSixWin == 1)
            {
                strhtml_list.Append("<img src=\"" + this.http_start + "NetImages/zhong.gif\" alt=\"中\"/>");
            }
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                if (lpagetemp == "") { stypelink = stypelink.Replace("&amp;", "?"); }
                strhtml_list.Append("<a href=\"" + http_start + "bbs-" + listVo[i].id + ".html" + lpagetemp + stypelink + "" + "\">" + listVo[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");

            }
            else
            {
                strhtml_list.Append("<a href=\"" + http_start + "bbs/view.aspx?id=" + listVo[i].id + lpagetemp + stypelink + "" + "\">" + listVo[i].book_title + "</a><br/>" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "/<a href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");
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
            strhtml_list.Append("<div class=\"btBox\"><div class=\"bt5\"><a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">搜索</a> <a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;type=days&amp;key=1\">今日</a> <a href=\"" + this.http_start + "bbs/Log_List.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">日志</a> <a href=\"" + this.http_start + "bbs/onlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">在线</a> <a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid=3&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">监狱</a></div></div>");
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

        //显示广告
        if (adVo.secondShowDown != "")
        {
            strhtml.Append(adVo.secondShowDown);
        }

        //导航按钮

        if (action == "search")
        {
            strhtml.Append("<div class=\"bt1\">");
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_search.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "\">返回搜索</a> ");
            strhtml.Append("</div>");
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
            strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
            if (WapTool.ISAPI_Rewrite3_Open == "1")
            {
                strhtml.Append("<a href=\"" + this.http_start + "wapindex-" + siteid + "-" + classVo.childid + ".html" + "\">返回上级</a> ");
                strhtml.Append("<a href=\"" + this.http_start + "wapindex-" + siteid + "-0.html" + "\">返回首页</a>");
            }
            else
            {
                strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid + "" + "\">返回上级</a> ");
                strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
            }
            strhtml.Append("</div></div>");
        }


        strhtml.Append(WapTool.GetVS(wmlVo));

        strhtml.Append(classVo.sitedowntip);

        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));

    }

    Response.Write(ERROR);
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>