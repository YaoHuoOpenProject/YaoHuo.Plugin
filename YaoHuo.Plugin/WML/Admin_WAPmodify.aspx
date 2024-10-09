<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="admin_WAPmodify.aspx.cs" Inherits="YaoHuo.Plugin.WML.admin_WAPmodify" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("修改WML内容|修改內容|content modification"), wmlVo));
    Response.Write("<div class=\"title\">" + this.GetLang("修改WML内容|修改操作|Modify operation") + "</div>");
    Response.Write("<div class=\"tip\">");
    Response.Write(this.ERROR);
    if (this.INFO == "OK")
    {
        Response.Write("<b>");
        Response.Write(this.GetLang("修改成功！|修改成功！|Successfully modified"));
        Response.Write("</b><br/>");
        Response.Write("<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a><br/>");
    }
    else if (this.INFO == "NULL")
    {
        Response.Write("<b>");
        Response.Write(this.GetLang("标题或内容不能为空！|標題或內容不能爲空！|The title or content can not be empty!"));
        Response.Write("</b><br/>");
    }
    Response.Write("</div>");
    Response.Write("<div class=\"content\">");
    Response.Write("<form name=\"go\" action=\"" + this.http_start + "wml/admin_WAPmodify.aspx\" method=\"post\">");
    // Response.Write("<b>页面头部请在【页面综合排版】【CSS编辑器】中输入！</b><br/>");
    Response.Write(this.GetLang("标题|標題|Title") + " ");
    Response.Write("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } window.addEventListener('DOMContentLoaded', function() { var textareas = document.querySelectorAll('textarea'); textareas.forEach(function(textarea) { adjustTextareaHeight(textarea); }); }); </script>");
    Response.Write("<input type=\"text\" name=\"book_title\" style=\"width:80%\" value=\"" + bookVo.book_title + "\"/><br/>");    //Response.Write(this.GetLang("WAP1.0中&lt;card&gt;与&lt;/card&gt;中间代码|WAP1.0中&lt;card&gt;与&lt;/card&gt;中间代码|Content") + "*:<br/>");
                                                                                                                                  //Response.Write(this.GetLang("WAP1.0|Content") + "<br/>");
    Response.Write("<div class='centered-container'>");
    Response.Write("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" style=\"width:98.6%;margin-bottom:5px;\">" + Server.HtmlEncode(bookVo.book_content) + "</textarea>");
    Response.Write("</div>");    //Response.Write(this.GetLang("WAP2.0/HTML5中&lt;body&gt;与&lt;/body&gt;中间代码|WAP2.0/HTML5中&lt;body&gt;与&lt;/body&gt;中间代码|Content") + "<br/>");
                                 //Response.Write(this.GetLang("WAP2.0|WAP2.0/HTML5中&lt;body&gt;与&lt;/body&gt;中间代码|Content") + "<br/>");
    Response.Write("<div class='centered-container'>");
    Response.Write("<textarea oninput=\"adjustTextareaHeight(this)\" name=\"book_content2\" rows=\"15\" style=\"min-height:55vh;width:98.6%;margin:0px;\">" + Server.HtmlEncode(bookVo.book_content2) + "</textarea><br/>");
    Response.Write("</div>");
    //Response.Write("请将编辑好后复制进来！<br/><br/>");
    //Response.Write(this.GetLang("跳转时间|跳转时间|跳转时间") + " ");
    //Response.Write("<input type=\"text\" name=\"book_re\" value=\""+bookVo.book_re+"\" size=\"5\"/> ");
    //Response.Write("(秒)<br/>");
    //Response.Write(this.GetLang("跳转地址|跳转地址|跳转地址") + " ");
    //Response.Write("<input type=\"text\" name=\"book_pub\" size=\"30\" value=\""+bookVo.book_pub+"\"/><br/>");
    //Response.Write("(http://开头，&amp;可以不用改成&amp;amp;)");
    //Response.Write("<br/> <br/>");
    Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    Response.Write("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
    Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    Response.Write("<input type=\"submit\" name=\"bt\"  class=\"btn\" value=\"" + this.GetLang("修 改|修 改|Modify") + "\"/>");
    Response.Write("</form></div>");
    //Response.Write("<div class=\"tip\">说明:UBB调用二种方式，(1)显示列表链接[wml=N_M_X_Y]和(2)直接取其中的内容[getwml=标题ID]，以上支持UBB方法与wml1.0/2.0代码混编！[查看UBB大全]</div>");
    Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
    Response.Write("<a href=\"" + this.http_start + "wml/admin_userlistWAP.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a>");
    Response.Write("</div></div>");
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>