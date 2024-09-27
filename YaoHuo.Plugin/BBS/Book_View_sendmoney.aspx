<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_SendMoney.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_SendMoney" %>
<%@ Import namespace="YaoHuo.Plugin.Tool" %>
<%
if (this.INFO == "OK")
{
    wmlVo.timer = "0.3";
    wmlVo.strUrl = "bbs-" + this.getid + ".html";
}
Response.Write(WapTool.showTop(this.GetLang("发表派币帖|发表派币帖|add subject"), wmlVo));
strhtml.Append("<link href=\"/netcss/css/save-notification.css\" rel=\"stylesheet\" type=\"text/css\"/>");
strhtml.Append("<div class=\"title\">发表派币帖(");
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
else if (this.INFO == "MAXMONEY")
{
    strhtml.Append("<b>派币值总值 大于 总礼金 了!</b><br/>");
}
else if (this.INFO == "NOEQUALMONEY")
{
    strhtml.Append("<b>您设置N层楼排币总值与总礼金不一致!</b><br/>");
}
else if (this.INFO == "MAX")
{
    strhtml.Append("<b>今天你已超过发帖限制：" + this.KL_CheckBBSCount + " 个帖子了，请明天再来！</b><br/>");
}
else if (this.INFO == "FORMATERR")
{
    strhtml.Append("<b>派币楼层和派币值配置错误，| 个数不一值 或 存在非数字值！</b><br/>");
}
else if (this.INFO == "NOMONEY")
{
    strhtml.Append("<b>我的钱币不够啦！或总礼金不能小于1 !</b><br/>");
}
else if (this.INFO == "LOCK")
{
    strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
}
strhtml.Append("</div>");
strhtml.Append("<div class=\"content\">");
if (this.INFO != "OK")
{
    strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_sendmoney.aspx\" method=\"post\">");
    strhtml.Append("<div class='book_view_add_height'>");
    strhtml.Append(this.GetLang("标题|標題|Title") + "");
    strhtml.Append("</div>");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<input type=\"text\" minlength=\"5\" maxlength=\"25\" required=\"required\" name=\"book_title\" class=\"txt\" style=\"width:98.6%;\" value=\"" + book_title + "\"/>");
    strhtml.Append("</div>");
    strhtml.Append("<div class='book_view_add_height'>");
    strhtml.Append(this.GetLang("内容|內容|Content") + "<button type=\"button\" class=\"saveDraft\" id=\"saveDraftButton\">保存草稿</button><button type=\"button\" class=\"saveDraft\" id=\"clearDraftButton\" style=\"display: none;\">清除草稿</button>");
    strhtml.Append("</div>");
    strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" minlength=\"15\" required=\"required\" placeholder=\"派币越多，回复越多\" rows=\"5\" class=\"KL_bbs_textarea\" style=\"width:98.6%;margin-bottom:5px;min-height:37vh;\">" + book_content + "</textarea>");
    strhtml.Append("</div>");
    strhtml.Append("我当前" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + " " + userVo.money + " <br/>");
    strhtml.Append("派币总礼金 <input type=\"number\" oninput=\"if(value.length>8)value=value.slice(0,8)\" min=\"2000\" required=\"required\" placeholder=\"最少2000\" format=\"*N\" style=\"width:40%\" name=\"freemoney\" value=\"" + freemoney + "\"/><br/>");
    strhtml.Append("<input type=\"hidden\" class=\"txt\" name=\"freerule1\" value=\"0" + freerule1 + "\"/>");
    strhtml.Append("每人派币值 <input oninput=\"if(value.length>5)value=value.slice(0,5)\" type=\"number\" min=\"200\" max=\"10000\" required=\"required\" placeholder=\"最少200\"class=\"txt\" style=\"width:40%\" name=\"freerule2\" value=\"" + freerule2 + "\"  /><br/>");
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
    strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("发表派币帖|发表派币帖|submit new subject") + "\"/>");
    strhtml.Append("</form><br/>");  
}
strhtml.Append("</div><script type=\"text/javascript\" src=\"/netcss/js/SaveDraft.js\"></script>");
strhtml.Append("<div class=\"bt2\">");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_add.aspx?action=class&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">发表普通帖</a>");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addvote.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表投票帖|发表投票帖|add vote") + "</a>");
strhtml.Append("</div>");
strhtml.Append("<div class=\"btBox\">");
strhtml.Append("<div class=\"bt2\">");
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表文件帖|发表文件帖|upload file") + "</a>");
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
strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=0\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
strhtml.Append("</div></div>");
Response.Write(strhtml);
Response.Write(WapTool.showDown(wmlVo));
%>