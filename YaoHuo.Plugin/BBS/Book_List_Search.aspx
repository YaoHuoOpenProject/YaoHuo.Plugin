<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_List_Search.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_List_Search" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%@ Import Namespace="System.Data" %>
<%
    Response.Write(WapTool.showTop(classVo.classname, wmlVo));
    StringBuilder strhtml_list = new StringBuilder();

    // 显示广告
    if (adVo.secondShowTop != "")
    {
        strhtml.Append(adVo.secondShowTop);
    }

    // 修改搜索框显示条件
    string pubUserId = Request.QueryString["pub"];
    string searchKey = Request.QueryString["key"];
    string searchType = Request.QueryString["type"];

    if (!string.IsNullOrEmpty(pubUserId) || (searchType == "pub" && !string.IsNullOrEmpty(searchKey)))
    {
        if (string.IsNullOrEmpty(pubUserId))
        {
            pubUserId = searchKey;
        }
        strhtml.Append("<div class='search-box'>");
        strhtml.Append("<form action='" + this.http_start + "bbs/book_list_search.aspx' style=\"margin-left:5px;\" method='get'>");
        strhtml.Append("<input type='hidden' name='action' value='search' />");
        strhtml.Append("<input type='hidden' name='siteid' value='" + siteid + "' />");
        strhtml.Append("<input type='hidden' name='classid' value='" + classid + "' />");
        strhtml.Append("<input type='hidden' name='type' value='title' />");
        strhtml.Append("<input type='hidden' name='pub' value='" + pubUserId + "' />");

        // 判断是否需要在搜索框中显示关键词
        string placeholderText = "搜索" + pubUserId + "的帖子";
        string inputValue = "";
        if (searchType == "title" && !string.IsNullOrEmpty(searchKey))
        {
            inputValue = HttpUtility.HtmlEncode(searchKey);
        }

        strhtml.Append("<input type='text' name='key' minlength=\"1\" maxlength=\"9\" style=\"padding:5px;width:45%;max-width:200px;margin-right:-2px;\" placeholder='" + placeholderText + "' value='" + inputValue + "' />");
        strhtml.Append("<input type='submit' value='搜索' />");
        strhtml.Append("</form>");
        strhtml.Append("</div>");
    }

    // 显示列表
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
    //所有列表
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            strhtml_list.Append("<div class=\"listdata line2\">");
        }
        else
        {
            strhtml_list.Append("<div class=\"listdata line1\">");
        }
        index = index + kk;
        strhtml_list.Append(index + ".");
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
            strhtml_list.Append("<a class=\"topic-link\" href=\"" + http_start + "bbs-" + listVo[i].id + ".html" + lpagetemp + "" + "\">" + listVo[i].book_title + "</a><br/><span class=\"louzhunicheng\">" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "</span>/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");
        }
        else
        {
            strhtml_list.Append("<a class=\"topic-link\" href=\"" + http_start + "bbs/view.aspx?id=" + listVo[i].id + lpagetemp + "" + "\">" + listVo[i].book_title + "</a><br/><span class=\"louzhunicheng\">" + ShowNickName_color(long.Parse(listVo[i].book_pub), listVo[i].book_author) + "</span>/<a class=\"topic-link\" href=\"" + this.http_start + "bbs/book_re.aspx?actoin=class&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].book_classid + "&amp;id=" + listVo[i].id + "&amp;getTotal=" + listVo[i].book_re + "&amp;lpage=" + this.CurrentPage + "\">" + listVo[i].book_re + "</a>回/" + listVo[i].book_click + "阅 <span class=\"right\">" + WapTool.ShowTime(listVo[i].book_date) + "<span></div>");
        }
    }
    if (listVo == null)
    {
        strhtml_list.Append("<div class=\"tip\">暂无记录！</div>");
    }
    strhtml_list.Append("<!--listE-->");

    //显示导航分页
    strhtml_list.Append(linkURL);
    strhtml.Append(strhtml_list);

    //显示广告
    if (adVo.secondShowDown != "")
    {
        strhtml.Append(adVo.secondShowDown);
    }
    strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookList/KL_common.js\"></script>");
    strhtml.Append(WapTool.GetVS(wmlVo));
    strhtml.Append(classVo.sitedowntip);
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>