<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_addfileAddURL.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_addfileAddURL" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("续传文件|续传文件|add subject"), wmlVo));
    if (num > 9) num = 9;
    if (num < 1) num = 1;
    strhtml.Append("<div class=\"btBox\">");
    strhtml.Append("<div class=\"bt2\">");
    strhtml.Append("<a   href=\"" + this.http_start + "bbs/Book_View_addfileAdd.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "&amp;id=" + this.id + "\">本地续传文件</a> ");
    strhtml.Append("<a class=\"btSelect\" href=\"" + this.http_start + "bbs/book_view_addfileaddURL.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "&amp;id=" + this.id + "&amp;num=1\">外站资源续传</a> ");
    strhtml.Append("</div></div>");
    strhtml.Append(this.ERROR);
    strhtml.Append("<div class=\"tip\">");
    if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"content\"><b>上传成功！</b> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>审核后显示！</b> ");
        }
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a></div>");
    }
    else if (this.INFO == "EXTERR")
    {
        strhtml.Append("<b>上传文件格式错误，只允许上传：" + siteVo.UpFileType + "</b><br/>");
    }
    else if (this.INFO == "NOTSPACE")
    {
        strhtml.Append("<b>网站总空间已经大于系统分配给此网站的最大空间了，网站空间：" + siteVo.sitespace + "M；此网站已使用：" + (siteVo.myspace) + "KB</b><br/>");
    }
    else if (this.INFO == "MAXFILE")
    {
        strhtml.Append("<b>你上传的单个文件总大小超出了最大限制" + siteVo.MaxFileSize + "KB</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    if (this.INFO != "OK")
    {
        strhtml.Append("<form name=\"g1\" action=\"" + http_start + "bbs/Book_View_addfileAddURL.aspx\" method=\"get\">");
        strhtml.Append(this.GetLang("上传数量|上传数量|Upload Number") + " <input type=\"number\" oninput=\"if(value.length>1)value=value.slice(0,1)\" style=\"width:30px;text-align:center\" name=\"num\" value=\"" + this.num + "\" size=\"2\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"lpage\" value=\"" + lpage + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"id\" value=\"" + id + "\"/>");
        strhtml.Append(" <input type=\"submit\"  name=\"bt\" value=\"" + this.GetLang("确定|确定|GO") + "\"/></form>");
        strhtml.Append("<br/>");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/Book_View_addfileAddURL.aspx\" enctype=\"multipart/form-data\" method=\"post\">");
        for (int i = 0; i < this.num; i++)
        {
            strhtml.Append("----- 资源文件" + (i + 1) + " -----<br/>");
            strhtml.Append("资源名称 <input type=\"text\" maxlength=\"35\" placeholder=\" 必填项 \" required=\"required\" name=\"file_title\" style=\"width:70%;\" value=\"\"/><br/>");
            strhtml.Append("链接地址 <input type=\"text\" placeholder=\" http 或 https 开头的链接\" required=\"required\" name=\"file_url\" style=\"width:70%;\" value=\"\"/><br/>");
            strhtml.Append("文件大小 <input type=\"text\" maxlength=\"7\" placeholder=\" 例如：8MB \" name=\"file_size\" value=\"\" size=\"20\"/><br/>");
            strhtml.Append("文件后缀 <input type=\"text\" maxlength=\"5\" placeholder=\" 例如：txt \" required=\"required\" name=\"file_ext\" value=\"\" size=\"20\"/><br/>");
            strhtml.Append(this.GetLang("附件说明|附件说明|Source") + "<br/>");
            strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
            strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<textarea name=\"file_info\" oninput=\"adjustTextareaHeight(this)\" placeholder=\"填写备注信息，比如网盘提取密码、解压密码 \" rows=\"6\" style=\"width:98.6%;margin-bottom:5px;\"></textarea>");
            strhtml.Append("</div>");
        }
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"num\" value=\"" + num + "\"/>");
        strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("上传文件|上传文件|upload new subject") + "\"/>");
        strhtml.Append("</form>");
    }
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append("提示：严禁上传色情文件、病毒文件和恶意软件。");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_admin.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回上级|返回上级|add content") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>