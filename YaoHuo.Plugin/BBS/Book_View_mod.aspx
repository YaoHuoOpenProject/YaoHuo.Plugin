<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_mod" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {

        wmlVo.timer = "3";
        wmlVo.strUrl = "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage;


    }
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("修改内容|修改內容|content modification"), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p>");
        strhtml.Append(this.ERROR);
        if (this.INFO == "OK")
        {
            strhtml.Append("<b>");
            strhtml.Append(this.GetLang("修改成功！|修改成功！|Successfully modified"));
            strhtml.Append("</b><br/>");

        }
        else if (this.INFO == "NULL")
        {
            strhtml.Append("<b>");
            strhtml.Append("<b>标题最少" + this.titlemax + "字，内容最少" + this.contentmax + "字！</b><br/>");
            strhtml.Append("</b><br/>");

        }

        strhtml.Append(this.GetLang("标题|標題|Title") + "*:<br/>");
        strhtml.Append("<input type=\"text\" name=\"book_title" + r + "\" value=\"" + bbsVo.book_title + "\"/><br/>");
        strhtml.Append(this.GetLang("作者|作者|Author") + ":" + bbsVo.book_author + "<br/>");

        strhtml.Append(this.GetLang("内容|內容|Content") + "*:<br/>");
        strhtml.Append("<input type=\"text\"  name=\"book_content" + r + "\" value=\"" + bbsVo.book_content + "\" /><br/>");
        strhtml.Append(this.GetLang("缩放图地址|缩放图地址|Photo") + "*:<br/>");
        strhtml.Append("<input type=\"text\" name=\"book_img" + r + "\" value=\"" + bbsVo.book_img + "\"/><br/>");

        strhtml.Append("特殊贴:<select name=\"viewtype" + r + "\" value=\"" + bbsVo.viewtype + "\">");
        strhtml.Append("<option value=\"0\">0_特殊贴↓</option>");
        strhtml.Append("<option value=\"1\">1_登录可见</option>");
        strhtml.Append("<option value=\"2\">2_手机可见</option>");
        strhtml.Append("<option value=\"3\">3_回复可见</option>");
        strhtml.Append("<option value=\"4\">4_金钱可见需要</option>");
        strhtml.Append("<option value=\"5\">5_经验可见需要</option>");
        strhtml.Append("<option value=\"6\">6_付费" + siteVo.sitemoneyname + "可见需要</option>");
        strhtml.Append("<option value=\"7\">7_付费RMB可见需要</option>");
        strhtml.Append("</select><br/>");
        //查看值
        strhtml.Append("需要:<input type=\"text\" format=\"*N\" name=\"viewmoney" + r + "\" value=\"" + bbsVo.viewmoney + "\" size=\"4\" /><br/>");

        strhtml.Append("<anchor><go href=\"" + http_start + "bbs/book_view_mod.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        strhtml.Append("<postfield name=\"action\" value=\"gomod\"/>");
        strhtml.Append("<postfield name=\"id\" value=\"" + id + "\"/>");
        strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<postfield name=\"lpage\" value=\"" + lpage + "\"/>");
        strhtml.Append("<postfield name=\"book_title\" value=\"$(book_title" + r + ")\"/>");
        strhtml.Append("<postfield name=\"book_content\" value=\"$(book_content" + r + ")\"/>");
        strhtml.Append("<postfield name=\"viewtype\" value=\"$(viewtype" + r + ")\"/>");
        strhtml.Append("<postfield name=\"viewmoney\" value=\"$(viewmoney" + r + ")\"/>");
        strhtml.Append("<postfield name=\"book_img\" value=\"$(book_img" + r + ")\"/>");
        strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\"/>");
        strhtml.Append("</go>" + this.GetLang("修 改|修 改|Modify") + "</anchor><br/><br/>");

        strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to subject") + "</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");

        strhtml.Append("</p>");
        Response.Write(strhtml);
    }
    else //2.0界面
    {

        strhtml.Append("<div class=\"subtitle\">" + this.GetLang("修改操作|修改操作|Modify operation") + "</div>");
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append(this.ERROR);
        if (this.INFO == "OK")
        {
            strhtml.Append("<b>");
            strhtml.Append(this.GetLang("修改成功！|修改成功！|Successfully modified"));
            strhtml.Append("</b> 自动跳转至 <a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("贴子主题|贴子主题|Back to subject") + "</a><br/>");

        }
        else if (this.INFO == "NULL")
        {
            strhtml.Append("<b>");
            strhtml.Append("<b>标题最少" + this.titlemax + "字，内容最少" + this.contentmax + "字！</b>");
            strhtml.Append("</b><br/>");

        }
        strhtml.Append("</div>");
        if (this.INFO != "OK")
        {
            //显示表情
            strhtml.Append("<form name=\"go\" action=\"" + this.http_start + "bbs/book_view_mod.aspx\" method=\"post\">");

            strhtml.Append("<select name=\"face\" style=\"display:none;\">");
            strhtml.Append("<option value=\"\">表情</option>");
            for (int i = 0; (facelist != null && i < this.facelist.Length); i++)
            {
            }
            strhtml.Append("</select>");
            //显示类别
            strhtml.Append("<select name=\"stype\" style=\"display:none;\">");
            strhtml.Append("<option value=\"\">类别</option>");
            for (int i = 0; (stypelist != null && i < this.stypelist.Length); i++)
            {
            }
            strhtml.Append("</select>");


            strhtml.Append("<div class=\"content\">");
            strhtml.Append(this.GetLang("标题|標題|Title") + " <br/>");
            strhtml.Append("<input type=\"text\" minlength=\"5\" maxlength=\"25\" required=\"required\" name=\"book_title\" value=\"" + bbsVo.book_title + "\" style=\"width:97%\"/><br/>");
            //strhtml.Append(this.GetLang("作者|作者|Author") + "：" + bbsVo.book_author + "<br/>");
            strhtml.Append(this.GetLang("内容|內容|Content") + " <br/>");
            strhtml.Append("<textarea name=\"book_content\" minlength=\"15\" required=\"required\" rows=\"12\" style=\"width:97%\">" + bbsVo.book_content.Replace("[br]", "\r\n") + "</textarea><br/>");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
            strhtml.Append("<input type=\"submit\" name=\"bt\" class=\"btn\" value=\"" + this.GetLang("修 改|修 改|Modify") + "\"/>");

            strhtml.Append("</div></form>");
        }

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

        strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
        strhtml.Append("</div></div>");
        Response.Write(strhtml);

    }


    //显示底部
    Response.Write(WapTool.showDown(wmlVo)); %>
