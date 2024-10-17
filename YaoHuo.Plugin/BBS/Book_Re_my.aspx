<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_Re_my.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_Re_My" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("查看" + this.touserid + "回复|查看+this.touserid+回複|View Reply"), wmlVo));
    // 会员可见
    if (this.IsCheckManagerLvl("|00|01|02|03|04|", "") == true)
    {
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/HyperLink.js?L15\" defer></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/Shad@w.js?l\" defer></script>");
        strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");

        string newestUrl = this.http_start + "bbs/book_re_my.aspx?action=class&amp;touserid=" + this.touserid +
                           "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid +
                           "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage +
                           "&amp;ot=0&amp;go=" + this.r;
        string oldestUrl = this.http_start + "bbs/book_re_my.aspx?action=class&amp;touserid=" + this.touserid +
                           "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid +
                           "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage +
                           "&amp;ot=1&amp;go=" + this.r;

        if (!string.IsNullOrEmpty(this.searchKey))
        {
            newestUrl += "&amp;searchKey=" + HttpUtility.UrlEncode(this.searchKey);
            oldestUrl += "&amp;searchKey=" + HttpUtility.UrlEncode(this.searchKey);
        }

        strhtml.Append("<a ");
        if (this.ot == "0")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + newestUrl + "\">按最新回复</a> ");

        strhtml.Append("<a ");
        if (this.ot == "1")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + oldestUrl + "\">按最早回复</a>");

        strhtml.Append("</div></div>");

        // 错误信息处理
        if (this.ERROR == "ERR_PERMISSION")
        {
            strhtml.Append("<div class=\"tip\">您只能搜索自己的回复内容!</div>");
        }
        else if (this.ERROR == "ERR_LENGTH")
        {
            strhtml.Append("<div class=\"tip\">搜索关键词长度必须在1-10个字符之间!</div>");
        }
        else
        {
            strhtml.Append(ERROR);
        }

        // 仅当用户查看自己的回复列表时显示搜索框
        if (this.touserid == this.userid || this.CheckManagerLvl("01", "") == true)
        {
            strhtml.Append("<div id=\"searchBox\">");
            strhtml.Append("<form action=\"" + this.http_start + "bbs/book_re_my.aspx\" style=\"margin-left:11px;\" method=\"get\">");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\">");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + this.siteid + "\">");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + this.classid + "\">");
            strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + this.touserid + "\">");
            strhtml.Append("<input type=\"hidden\" name=\"ot\" value=\"" + this.ot + "\">");
            strhtml.Append("<input type=\"text\" name=\"searchKey\" id=\"searchKey\" minlength=\"1\" maxlength=\"8\" value=\"" + this.searchKey + "\" style=\"padding:5px;width:35%; max-width:200px;margin-right:-2px;\" placeholder=\"搜索" + this.touserid + "的回复\">");
            strhtml.Append("<input type=\"submit\" value=\"搜索\">");
            strhtml.Append("</form>");
            strhtml.Append("</div>");
        }

        // 修改JavaScript函数
        strhtml.Append("<script type=\"text/javascript\">");
        strhtml.Append("function toggleSearch() {");
        strhtml.Append("  var searchBox = document.getElementById('searchBox');");
        strhtml.Append("  if (searchBox.style.display === 'none') {");
        strhtml.Append("    searchBox.style.display = 'block';");
        strhtml.Append("  } else {");
        strhtml.Append("    searchBox.style.display = 'none';");
        strhtml.Append("  }");
        strhtml.Append("}");
        strhtml.Append("</script>");

        // 管理员操作
        if (this.CheckManagerLvl("04", "") == true && this.touserid != "1000") // 检查是否为管理员且ID不为1000
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<a class=\"urlbtn\" onclick=\"return confirm('清空回复前请先确认操作');\" href=\"" +
                this.http_start + "bbs/Book_re_delmy.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" +
                this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;touserid=" +
                this.touserid + "&amp;ot=" + this.ot + "\">清空(" + this.touserid + ")的所有回复</a><br />");
            strhtml.Append("</div>");
        }
        // 显示导航分页
        if (string.IsNullOrEmpty(this.searchKey))
        {
            strhtml.Append(linkTOP);
        }
	// 如果URL包含searchKey参数，显示广告
    if (!string.IsNullOrEmpty(this.searchKey)) strhtml.Append("<script async src=\"https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-2577891689868694\" crossorigin=\"anonymous\"></script> <ins class=\"adsbygoogle\" style=\"display:block\" data-ad-client=\"ca-pub-2577891689868694\" data-ad-slot=\"4083150768\" data-ad-format=\"auto\" data-full-width-responsive=\"true\"></ins> <script> (adsbygoogle = window.adsbygoogle || []).push({}); </script>");

        //显示列表
        kk = kk + ((CurrentPage - 1) * pageSize) - 1;
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml.Append("<div class=\"line2\">");
            }
            else
            {
                strhtml.Append("<div class=\"line1\">");
            }
            if (ot == "1")
            {
                index = (kk + 1);
            }
            else
            {
                index = (total - kk);
            }
            strhtml.Append("" + index + ".<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a>：<span class=\"retext\">");
            strhtml.Append(listVo[i].content + "</span><br/> " + string.Format("{0:MM-dd HH:mm}", listVo[i].redate) + " <a href=\"" + this.http_start + "bbs-" + listVo[i].bookid + "" + ".html\">查看</a></div>");
            kk = kk + 1;
        }
        if (listVo == null)
        {
            strhtml.Append("<div class=\"tip\">暂无回复记录</div>");
        }
        strhtml.Append(linkURL);
    }
    // 会员可见结束
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/userinfo.aspx?touserid=" + this.userid + "\">我的空间</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    strhtml.Append("<script type=\"text/javascript\"> window.onload = function() { var paginationForm = document.querySelector('.showpage form'); if (paginationForm) { var searchKeyInput = paginationForm.querySelector('input[name=\"searchKey\"]'); if (searchKeyInput) { // 移除原有的searchKey输入字段 searchKeyInput.parentNode.removeChild(searchKeyInput); // 获取URL中的searchKey参数 var urlParams = new URLSearchParams(window.location.search); var searchKeyValue = urlParams.get('searchKey'); // 如果存在searchKey参数，添加一个隐藏字段 if (searchKeyValue) { var hiddenInput = document.createElement('input'); hiddenInput.type = 'hidden'; hiddenInput.name = 'searchKey'; hiddenInput.value = searchKeyValue; paginationForm.appendChild(hiddenInput); } } // 拦截表单提交事件 paginationForm.addEventListener('submit', function(e) { e.preventDefault(); var formData = new FormData(paginationForm); var searchParams = new URLSearchParams(formData); // 构建新的URL var newUrl = paginationForm.action + '?' + searchParams.toString(); // 跳转到新的URL window.location.href = newUrl; }); } }; </script>");
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(WapTool.showDown(wmlVo));
%>