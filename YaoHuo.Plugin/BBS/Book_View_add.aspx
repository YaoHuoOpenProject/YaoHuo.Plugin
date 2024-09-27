<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_add.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_add" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
if (this.INFO == "OK")
{
    wmlVo.timer = "0.3";
    wmlVo.strUrl = "bbs-" + this.getid + ".html";
}
Response.Write(WapTool.showTop(this.GetLang("发表主题|發表主題|add subject"), wmlVo));
strhtml.Append("<link href=\"/netcss/css/save-notification.css\" rel=\"stylesheet\" type=\"text/css\"/>");
strhtml.Append("<div class=\"title\">发表主题(");
strhtml.Append(classVo.classname+"");
strhtml.Append(")</div>");
strhtml.Append("<div class=\"notification-container\" style=\"display:none\"><div class=\"custom-notification\"> <div class=\"custom-notification-container\"> <p class=\"custom-notification-content\">草稿保存成功!</p> </div> </div></div>");
strhtml.Append(this.ERROR);
if (this.INFO == "OK")
{
    strhtml.Append("<div class=\"content\"><b>发表主题成功！</b> ");
    if (siteVo.isCheck == 1)
    {
        strhtml.Append("<b>审核后显示！</b> ");
    }
    strhtml.Append("获得" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + "" + getmoney + "，经验" + getexpr + "<br/> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.getid + "" + "\">自动返回主题</a></div>");
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
    strhtml.Append("<div class=\"tip\"><b>请不要发表重复内容！</b></div>");
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
    strhtml.Append("<b>今天你已超过发帖限制：" + this.KL_CheckBBSCount + " 个帖子了，请明天再来！</b><br/>");
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
strhtml.Append("<div class=\"content\">");
if (this.INFO != "OK")
{
    strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_add.aspx\" method=\"post\">");
    strhtml.Append("<div class='book_view_add_height'>");
    strhtml.Append(this.GetLang("标题|標題|Title") + "");
    strhtml.Append("</div>");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<input type=\"text\" name=\"book_title\" minlength=\"5\" maxlength=\"25\" required=\"required\" class=\"txt\" style=\"width:98.6%;\" value=\"" + book_title + "\"/>");
    strhtml.Append("</div>");
    if (this.action == "friends")
    {
        strhtml.Append(this.GetLang("内容|內容|Content") + " <a href=\"" + http_start + "bbs/ModifyUserFriends.aspx?siteid=" + siteid + "" + "\">完善交友资料</a><br/>");
    }
    else
    {
        strhtml.Append("<div class='book_view_add_height'>");
        strhtml.Append(this.GetLang("内容|內容|Content") + "<button type=\"button\" class=\"saveDraft\" id=\"saveDraftButton\">保存草稿</button><button type=\"button\" class=\"saveDraft\" id=\"clearDraftButton\" style=\"display: none;\">清除草稿</button>");
        strhtml.Append("</div>");
    }
    strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" minlength=\"15\" required=\"required\" placeholder=\"发推广（邀请类帖子）、宣传QQ微信群一律封号\" class=\"KL_bbs_textarea\" rows=\"5\" style=\"width:98.6%;margin-bottom:5px;min-height:46vh;\">" + book_content + "</textarea>");
    strhtml.Append("</div>");
    strhtml.Append("悬赏妖晶<input type=\"number\" name=\"sendmoney\" min=\"1000\" max=\"10000000\" value=\"" + (sendmoney != "0" ? sendmoney : "") + "\" placeholder=\"最少1000,可留空\" style=\"margin-left:6px;width:115px;max-width:50%;\"/>");
    if (this.needpwFlag == "1")
    {
        strhtml.Append("我的密码 <input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" size=\"10\" /><br/>");
    }
    if (this.isNeedSecret == true)
    {
        strhtml.Append("本版暗号 <input type=\"text\" name=\"secret\" value=\"\" size=\"10\" /><br/>");
    }
    strhtml.Append("<br/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");        
    strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("发表新帖|发表新帖|submit new subject") + "\"/>");
    strhtml.Append("</form><br/>");
    strhtml.Append("提示：每天最多2篇帖子，请珍惜发帖机会，不要发无聊的水帖、低质量求助帖。帖子发到对应版块，以免被删除。请遵守<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + 288 + "&amp;id=5248\">妖火网版规</a>。<br/>");
}
strhtml.Append("</div><script type=\"text/javascript\" src=\"/netcss/js/SaveDraft.js\"></script>");
strhtml.Append("<div class=\"bt2\">");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_sendmoney.aspx?action=class&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">发表派币帖</a>");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addvote.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表投票帖|发表投票帖|add vote") + "</a> ");
strhtml.Append("</div>");
strhtml.Append("<div class=\"btBox\">");
strhtml.Append("<div class=\"bt2\">");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表文件帖|发表文件帖|upload file") + "</a> ");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_ubb.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;backurl="+HttpUtility.UrlEncode("bbs/book_view_add.aspx?siteid="+this.siteid+"&classid="+this.classid)+"\">" + this.GetLang("UBB方法|查看UBB方法|view UBB fuction") + "</a>");
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