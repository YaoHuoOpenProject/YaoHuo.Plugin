<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_mod.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_mod" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {
        // 修改：根据是否有追加悬赏提示信息来设置跳转时间
        wmlVo.timer = string.IsNullOrEmpty(this.additionalRewardMessage) ? "0.5" : "35";
        wmlVo.strUrl = "bbs-" + id + ".html";
    }
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("修改帖子内容|修改帖子內容|content modification"), wmlVo));
        strhtml.Append("<div class=\"title\">" + this.GetLang("修改帖子|修改操作|Modify operation") + "</div>");
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append(this.ERROR);
        if (this.INFO == "OK")
        {
            strhtml.Append("<b>");
            strhtml.Append(this.GetLang("修改成功！|修改成功！|Successfully modified"));
            strhtml.Append("</b>正在跳转至 <a style=\"font-size: unset;\" href=\"" + this.http_start + "bbs-" + this.id + ".html\">" + this.GetLang("帖子主题|帖子主题|Back to subject") + "</a><br/>");
        }
        else if (this.INFO == "NULL")
        {
            strhtml.Append("<b>");
            strhtml.Append("<b>标题最少" + this.titlemax + "字，内容最少" + this.contentmax + "字！</b>");
            strhtml.Append("</b><br/>");
        }
        strhtml.Append("</div>");
        // 新增: 追加悬赏提示信息
        if (!string.IsNullOrEmpty(this.additionalRewardMessage))
        {
            strhtml.Append("<div class=\"tip\" id=\"additionalRewardMessage\"></div>");
            strhtml.Append("<script>");
            strhtml.Append("var message = '" + this.additionalRewardMessage + "';");
            strhtml.Append("var messageElement = document.getElementById('additionalRewardMessage');");
            strhtml.Append("if (message.startsWith('success')) {");
            strhtml.Append("    var amount = message.split(',')[1];");
            strhtml.Append("    messageElement.innerHTML = '追加悬赏成功，悬赏已增加:' + amount;");
            strhtml.Append("} else if (message === 'insufficient_balance') {");
            strhtml.Append("    messageElement.innerHTML = '追加悬赏失败，余额不足！';");
            strhtml.Append("} else if (message === 'not_author') {");
            strhtml.Append("    messageElement.innerHTML = '只有帖子作者可以追加悬赏！';");
            strhtml.Append("} else if (message === 'min_amount') {");
            strhtml.Append("    messageElement.innerHTML = '追加悬赏失败，最低金额为:1000';");
            strhtml.Append("}");
            strhtml.Append("</script>");
        }
        if (this.INFO != "OK")
        {
            strhtml.Append("<form name=\"go\" action=\"" + this.http_start + "bbs/book_view_mod.aspx\" method=\"post\">");
            strhtml.Append("<select name=\"face\" style=\"display:none;\">");
            strhtml.Append("<option value=\"\">表情</option>");
            for (int i = 0; (facelist != null && i < this.facelist.Length); i++)
            {
            }
            strhtml.Append("</select>");
            strhtml.Append("<select name=\"stype\" style=\"display:none;\">");
            strhtml.Append("<option value=\"\">类别</option>");
            for (int i = 0; (stypelist != null && i < this.stypelist.Length); i++)
            {
            }
            strhtml.Append("</select>");
            strhtml.Append("<div class=\"content\">");
            strhtml.Append(this.GetLang("标题|標題|Title") + " <br/>");
            strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<input type=\"text\" minlength=\"5\" maxlength=\"25\" required=\"required\" name=\"book_title\" value=\"" + bbsVo.book_title + "\" style=\"width:98.6%;\"/>");
            strhtml.Append("</div>");
            strhtml.Append(this.GetLang("内容|內容|Content") + " <br/>");
            strhtml.Append("<script>function adjustTextareaHeight(t){var st=window.pageYOffset||document.documentElement.scrollTop;t.style.height='auto';t.style.height=t.scrollHeight+'px';window.scrollTo(0,st)}function initTextareas(){document.querySelectorAll('textarea[name=\"book_content\"]').forEach(function(t){adjustTextareaHeight(t);t.addEventListener('input',function(){adjustTextareaHeight(this)})})}document.readyState==='loading'?document.addEventListener('DOMContentLoaded',initTextareas):initTextareas();</script>");
            strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" minlength=\"15\" required=\"required\" style=\"min-height:47vh;margin-bottom:5px;width:98.6%;white-space:pre-wrap;\">" + bbsVo.book_content.Replace("[br]", "\n") + "</textarea>");
            strhtml.Append("</div>");
            if (isAuthor && !isFreeMoney)
            {
                strhtml.Append("追加悬赏");
                strhtml.Append("<input type=\"number\" name=\"additionalReward\" value=\"\" placeholder=\"最少1000,可留空\" min=\"1000\" max=\"10000000\" style=\"margin-left:6px;width:115px;max-width:50%;\"/>");
                strhtml.Append("<br/>");
            }
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
            strhtml.Append("<input type=\"submit\" name=\"bt\" style=\"margin-right:10px;\" class=\"btn\" value=\"" + this.GetLang("修 改|修 改|Modify") + "\"/>");
            strhtml.Append("</div></form>");
        }
        string isWebHtml = this.ShowWEB_view(this.classid);
        if (isWebHtml != "")
        {
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }
        strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/list.aspx?classid=" + this.classid + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
        strhtml.Append("</div></div>");
        Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>