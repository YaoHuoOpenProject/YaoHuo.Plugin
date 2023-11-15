<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="book_view_modfile.aspx.cs" Inherits="YaoHuo.Plugin.BBS.book_view_modfile" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %><%
StringBuilder strhtml=new StringBuilder ();
Response.Write(WapTool.showTop(this.GetLang("附件管理|附件管理|modify subject"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    strhtml.Append("<p>");
    strhtml.Append(this.ERROR);
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_modfile.aspx?action=class&amp;id="+this.id+"&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;sid=" + this.sid1 + "-2-" + this.cs + "-" + this.lang + "-" + this.myua + "-" + this.width + "\">" + this.GetLang("进入WAP2.0界面附件管理|进入WAP2.0界面附件管理|wap2.0 add upfile") + "</a><br/>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?id="+this.id+"&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
    strhtml.Append(WapTool.GetVS(wmlVo));
    strhtml.Append("</p>");
    Response.Write(strhtml);
}
else //2.0界面
{
    if (num > 20) num = 20;
    //strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    //strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addfileadd.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;page=" + this.page + "\">我要批量续传文件</a>");
    //strhtml.Append("</div></div>");
    strhtml.Append("<div class=\"title\">" + this.GetLang("附件管理|附件管理|modify subject") + "</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append(this.ERROR);    
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>修改成功！</b> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "" + "\">查看主题</a><br/>");
    }
    else if (this.INFO == "EXTERR")
    {
        strhtml.Append("<b>上传文件格式错误，只允许上传："+siteVo.UpFileType+"</b><br/>");
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<b>所有项不能为空！</b><br/>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<b>请不要发重复内容！</b><br/>");
    }

    else if (this.INFO == "PWERROR")
    {
        strhtml.Append("<b>密码错误，请重新录入我的密码！</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发贴规则！</b><br/>");
    }
    strhtml.Append("</div>");

    strhtml.Append("<div class=\"content\">");
    if (this.INFO != "OK")
    {
        strhtml.Append("<div style='padding: 7px 0px 7px 0px;'>");
		strhtml.Append("<form name=\"f1\" action=\"" + http_start + "bbs/book_view_modfile.aspx\" method=\"post\">");        
        strhtml.Append(this.GetLang("标题|標題|Title") + "：<a href=\"" + this.http_start + "bbs-" + this.id + ".html" + "\">" + bbsVo.book_title+ "</a><br/>");
        strhtml.Append("</div>");
		//strhtml.Append("<br/>");
        int kk = 0;
        for (int i = 0; (imgList!=null&&i < imgList.Count); i++)
        {
            strhtml.Append("<b>----- URL图片/文件" + (i + 1) + " -----</b><br/>");
            strhtml.Append("<input type=\"hidden\" name=\"book_id\" value=\"" + imgList[i].ID + "\"/>");
            strhtml.Append("[<a href=\"" + this.http_start + "bbs/book_view_modfile_del.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;delid="+imgList[i].ID+"" + "\">删除此文件</a>]<br/>");
            //strhtml.Append("路径：<b><a href=\"" + imgList[i].book_file + "\">" + imgList[i].book_file + "</a></b><br/>");
            strhtml.Append("资源名称 ");
            strhtml.Append("<input type=\"text\" name=\"book_file_title\" value=\"" + imgList[i].book_title + "\" style=\"width:70%;\"/><br/>");
            strhtml.Append("文件后缀 ");
            strhtml.Append("<input type=\"text\" name=\"book_ext\" value=\"" + imgList[i].book_ext + "\" size=\"12\"/><br/>");
            strhtml.Append("文件大小 ");
            strhtml.Append("<input type=\"text\" name=\"book_size\" value=\"" + imgList[i].book_size + "\" size=\"12\"/><br/>");
            strhtml.Append(this.GetLang("附件说明|说明|INFO") + "<br/>");
			strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<textarea name=\"book_file_info\" rows=\"5\" style=\"width:98.6%;margin-bottom:5px;;\">" + imgList[i].book_content + "</textarea>");
			strhtml.Append("</div>");
            //strhtml.Append("<hr/>");
            kk++;
        }
        if (kk > 0)
        {
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"num\" value=\"" + num + "\"/>");
            //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
            strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("修 改|修 改|submit modify subject") + "\"/>");
        }
        else
        {
            strhtml.Append("抱歉，没有查询到附件，如有需要请续传文件！<br/><br/>");
        }
            strhtml.Append("</form>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append("如果附件上传后不显示，修改提交即可。");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
    if (isWebHtml != "")
    {
        //string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?id="+this.id+"&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
//显示底部
Response.Write(WapTool.showDown(wmlVo)); %>