<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_re_mod.aspx.cs" Inherits="KeLin.WebSite.bbs.Book_re_mod" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("修改回复操作|修改回複|Modify Reply"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    Response.Write("<p>");
    Response.Write(this.ERROR);
    if (this.INFO == "OK")
    {
        Response.Write("<b>" + this.GetLang("修改成功！|修改成功！|Modify successfully!") + "</b> <a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回|返回|Back") + "</a><br/>");
    }
    else if (this.INFO == "NULL")
    {
        Response.Write("<b>" + this.GetLang("内容要大于2字！|内容要大于2字！|Content to more than 2 words!") + "</b><br/>");
    }
    Response.Write(this.GetLang("回复内容|回複內容|Reply Content") + "*:<br/>");
    Response.Write("<input type=\"text\"  name=\"content" + r + "\" value=\"" + bbsReVo.content + "\" /><br/>");
    Response.Write("<anchor><go href=\"" + http_start + "bbs/book_re_mod.aspx\" method=\"post\" accept-charset=\"utf-8\">");
    Response.Write("<postfield name=\"action\" value=\"gomod\"/>");
    Response.Write("<postfield name=\"id\" value=\"" + id + "\"/>");
    Response.Write("<postfield name=\"reid\" value=\"" + reid + "\"/>");
    Response.Write("<postfield name=\"classid\" value=\"" + classid + "\"/>");
    Response.Write("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
    Response.Write("<postfield name=\"page\" value=\"" + page + "\"/>");
    Response.Write("<postfield name=\"lpage\" value=\"" + lpage + "\"/>");
    Response.Write("<postfield name=\"content\" value=\"$(content" + r + ")\"/>");
    //Response.Write("<postfield name=\"sid\" value=\"" + sid + "\"/>");
    Response.Write("</go>" + this.GetLang("修 改|修 改|Modify") + "</anchor><br/>");
    Response.Write("<br/>说明:为安全起见，普通会员发表回复内容中的[sid]会过滤掉，如有需要请添加回去！");
    Response.Write("<br/><a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage="+this.lpage+"&amp;page=" + this.page + "&amp;ot="+this.ot+"&amp;id="+this.id+"\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</p>");
}
else //2.0界面
{
    Response.Write("<div class=\"title\">" + this.GetLang("修改回复|修改操作|Modify") + "</div>");
	Response.Write("<div class=\"tip\">");
    Response.Write(this.ERROR);
    if (this.INFO == "OK")
    {
        Response.Write("<b>" + this.GetLang("修改成功！|修改成功！|Modify successfully!") + "</b> <a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回|返回|Back") + "</a><br/>");
    }
    else if (this.INFO == "NULL")
    {
        Response.Write("<b>" + this.GetLang("内容要大于2字！|内容要大于2字！|Content to more than 2 words!") + "</b><br/>");
    }
    Response.Write("</div>");
    Response.Write("<div class=\"content\">");
    Response.Write("<form name=\"f\" aciont=\""+this.http_start+"bbs/Book_re_mod.aspx\" method=\"post\">");
    //Response.Write(this.GetLang("回复内容|回複內容|Reply Content") + "<br/>");
    Response.Write("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
	Response.Write("<div class='centered-container'>");
    Response.Write("<textarea type=\"text\" oninput=\"adjustTextareaHeight(this)\" name=\"content\" value=\"" + bbsReVo.content + "\" style=\"min-height:72vh;width:98.6%;\">" + bbsReVo.content.Replace("[br]", "\r\n") + "</textarea>");  
	Response.Write("</div>");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    Response.Write("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"reid\" value=\"" + reid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");    
    //Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    Response.Write("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("修 改|修 改|Modify") + "\"/><br/>");
    Response.Write("</form></div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");   
    Response.Write("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.page + "&amp;ot=" + this.ot + "&amp;id=" + this.id + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</div></div>"); 
}
//显示底部
Response.Write(WapTool.showDown(wmlVo)); %>