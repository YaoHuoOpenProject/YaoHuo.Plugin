<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_change.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_change" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
StringBuilder strhtml=new StringBuilder ();
Response.Write(WapTool.showTop(this.GetLang("转移栏目|轉移欄目|Transfer part"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    strhtml.Append("<p>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("转移成功！|轉移成功！|Transfer modified"));
        strhtml.Append("</b><br/>");
    }
    else if(this.INFO=="SELECT")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("请选择栏目！|請選擇欄目！|Please select the part!"));
        strhtml.Append("</b><br/>");
    }
    strhtml.Append(this.GetLang("原栏目ID|原欄目ID|Original classID") + ": "+classid+"<br/>");
    strhtml.Append(this.GetLang("原栏目名|原欄目名|Class Name") + ": " + classVo.classname + "<br/>");
    strhtml.Append(this.GetLang("转移后栏目|轉移後欄目|Change ClassID") + "*:<br/>");
    //下拉选择
    strhtml.Append("<select name=\"toclassid"+r+"\" value=\""+this.classid+"\">");
    for (int i = 0; (classList != null && i < classList.Count); i++)
    {
        strhtml.Append("<option value=\""+classList[i].classid + "\">" + classList[i].classid + "_"+classList[i].classname+"</option>");
    }
    strhtml.Append("</select><br/>"); 
    strhtml.Append("<anchor><go href=\""+http_start+"bbs/book_view_change.aspx\" method=\"post\" accept-charset=\"utf-8\">");
    strhtml.Append("<postfield name=\"action\" value=\"gomod\"/>");
    strhtml.Append("<postfield name=\"id\" value=\""+id+"\"/>");
    strhtml.Append("<postfield name=\"classid\" value=\""+classid+"\"/>");
    strhtml.Append("<postfield name=\"siteid\" value=\""+siteid+"\"/>");
    strhtml.Append("<postfield name=\"lpage\" value=\""+lpage+"\"/>");    	
    strhtml.Append("<postfield name=\"toclassid\" value=\"$(toclassid"+r+")\"/>");
    strhtml.Append("<postfield name=\"sid\" value=\""+sid+"\"/>");
    strhtml.Append("</go>" + this.GetLang("确 定|確 定|sure? really") + "</anchor><br/><br/>");
    strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.toclassid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回主题|返回主题|Back to subject") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
    strhtml.Append("</p>");
    Response.Write(strhtml);
}
else //2.0界面
{
    strhtml.Append("<div class=\"title\">" + this.GetLang("转移版块|轉移操作|Change operation") + "</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("转移成功！|轉移成功！|Transfer modified"));
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
        strhtml.Append("</b><br/>");
    }
    else if (this.INFO == "SELECT")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("请选择栏目！|請選擇欄目！|Please select the part!"));
        strhtml.Append("</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append(this.GetLang("原栏目ID|原欄目ID|Original classID") + "<span class=\"recolon\">:</span>" + classid + "<br/>");
    strhtml.Append(this.GetLang("原栏目名|原欄目名|Class Name") + "<span class=\"recolon\">:</span>" + classVo.classname + "<br/>");
    strhtml.Append(this.GetLang("转移后栏目|轉移後欄目|Change ClassID") + "<span class=\"recolon\">:</span><br/>");
    //下拉选择
    strhtml.Append("<form name=\"go\" action=\"book_view_change.aspx\" method=\"post\">");
    strhtml.Append("<select name=\"toclassid\" style='height:28px;' value=\"" + this.classid + "\">");
    for (int i = 0; (classList != null && i < classList.Count); i++)
    {
        strhtml.Append("<option value=\"" + classList[i].classid + "\">" + classList[i].classid + "_" + classList[i].classname + "</option>");
    }
    strhtml.Append("</select><br/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
    //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" name=\"bt\" value=\"" + this.GetLang("确 定|確 定|sure? really") + "\"/>");
    strhtml.Append("</form></div>");
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
    if (isWebHtml != "")
    {
        //string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml,wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_admin.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;id=" + this.id + "\">" + this.GetLang("返回管理|返回上級|Back to admin") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>