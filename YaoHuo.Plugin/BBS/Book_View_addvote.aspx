<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_addvote.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_addvote" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("发表投票帖|發表主題|add subject"), wmlVo));
    if (num > 9) num = 9;
    if (num < 2) num = 2;
    strhtml.Append("<link href=\"/netcss/css/save-notification.css\" rel=\"stylesheet\" type=\"text/css\"/>");
    strhtml.Append("<div class=\"title\">发表投票帖(");
    strhtml.Append(classVo.classname + "");
    strhtml.Append(")</div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"content\"><b>发表投票主题成功！</b> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>审核后显示！</b> ");
        }
        strhtml.Append("获得" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + ":" + getmoney + "，获得经验:" + getexpr + "<br/> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.getid + "" + "\">查看主题</a></div>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/netcss/js/ClearDraft.js\"></script>");
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<b>标题最少" + this.titlemax + "字，内容最少" + this.contentmax + "字！</b><br/>");
    }
    else if (this.INFO == "TITLEMAX")
    {
        if (title_max != "0")
        {
            strhtml.Append("<b>标题最大" + this.title_max + "字。</b><br/>");
        }
        if (content_max != "0")
        {
            strhtml.Append("<b>内容最大" + this.content_max + "字。</b><br/>");
        }
    }
    else if (this.INFO == "ERR_FORMAT")
    {
        strhtml.Append("<b>取到非法值:“$$(”请更换手机浏览器或重新编辑！</b><br/>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<b>请不要发重复内容！</b><br/>");
    }
    else if (this.INFO == "PWERROR")
    {
        strhtml.Append("<b>密码错误，请重新录入我的密码！</b><br/>");
    }
    else if (this.INFO == "ERROR_Secret")
    {
        strhtml.Append("<b>暗号错误，如果忘记联系站长索取！</b><br/>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<b>今天你已超过回帖限制：" + this.KL_CheckBBSCount + " 个帖子了，请明天再来！</b><br/>");
    }
    else if (this.INFO == "SENDMONEY")
    {
        strhtml.Append("<b>你当前的只有:" + userVo.money + "个，所以你悬赏值只能小于或等于" + userVo.money + "个</b><br/>");
    }
    else if (this.INFO == "NOMONEY")
    {
        strhtml.Append("<b>你当前的只有:" + userVo.money + "个，发帖需要扣掉：" + getmoney2 + "个</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"notification-container\" style=\"display:none\"><div class=\"custom-notification\"> <div class=\"custom-notification-container\"> <p class=\"custom-notification-content\">草稿保存成功!</p> </div> </div></div>");
    strhtml.Append("<div class=\"content\">");
    if (this.INFO != "OK")
    {
        strhtml.Append("<form name=\"g1\" action=\"" + http_start + "bbs/book_view_addvote.aspx\" method=\"get\">");
        strhtml.Append(this.GetLang("选择投票项|选择投票项|Select vote") + " <input type=\"number\" oninput=\"if(value.length>1)value=value.slice(0,1)\" name=\"num\" style=\"margin-right:-5px;margin-left:-2px;width:30px;text-align:center\" value=\"" + this.num + "\" size=\"2\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"go\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"page\" value=\"" + page + "\"/>");
        strhtml.Append(" <input type=\"submit\"  name=\"bt\" value=\"" + this.GetLang("确定|确定|GO") + "\"/></form><br/>");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_addvote.aspx\" method=\"post\">");
        strhtml.Append("<div class='book_view_add_height'>");
        strhtml.Append(this.GetLang("标题|標題|Title") + "");
        strhtml.Append("</div>");
        strhtml.Append("<div class='centered-container'>");
        strhtml.Append("<input type=\"text\" minlength=\"5\" maxlength=\"25\" required=\"required\" name=\"book_title\" class=\"txt\" style=\"width:98.6%;\" value=\"" + book_title + "\"/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class='book_view_add_height'>");
        strhtml.Append(this.GetLang("内容|內容|Content") + "<button type=\"button\" class=\"saveDraft\" id=\"saveDraftButton\">保存草稿</button><button type=\"button\" class=\"saveDraft\" id=\"clearDraftButton\" style=\"display: none;\" formnovalidate>清除草稿</button>");
        strhtml.Append("</div>");
        strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
        strhtml.Append("<div class='centered-container'>");
        strhtml.Append("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" minlength=\"15\" required=\"required\" rows=\"8\" class=\"KL_bbs_textarea\" style=\"width:98.6%;margin-bottom:5px;min-height:22vh;\">" + book_content + "</textarea>");
        strhtml.Append("</div>");
        strhtml.Append("悬赏妖晶<input type=\"number\" name=\"sendmoney\" min=\"1000\" max=\"10000000\" value=\"" + (sendmoney != "0" ? sendmoney : "") + "\" placeholder=\"最少1000,可留空\" style=\"margin-left:6px;width:115px;max-width:50%;\"/><br/>");
        for (int i = 0; i < this.num; i++)
        {
            strhtml.Append(this.GetLang("投票项 |投票项|Title") + (i + 1) + " ");
            strhtml.Append("<input type=\"text\" minlength=\"1\" maxlength=\"15\" required=\"required\" name=\"vote\" style=\"width:60%;max-width:205px;\" value=\"\"/><br/>");
        }
        if (this.needpwFlag == "1")
        {
            strhtml.Append("我的密码:<input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" size=\"10\" /><br/>");
        }
        if (this.isNeedSecret == true)
        {
            strhtml.Append("本版暗号:<input type=\"text\" name=\"secret\" value=\"\" size=\"10\" /><br/>");
        }
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
        strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("发表投票帖|发表投票帖|submit new subject") + "\"/>");
        strhtml.Append("</form>");
    }
    strhtml.Append("</div><script type=\"text/javascript\" src=\"/netcss/js/SaveDraft.js\"></script>");
    strhtml.Append("<div class=\"btBox\">");
    strhtml.Append("<div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_add.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表普通帖|发表普通帖|add subject") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表文件帖|发表文件帖|upload file") + "</a> ");
    strhtml.Append("</div>");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml.ToString()), wmlVo));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbslist-" + this.classid + ".html\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>