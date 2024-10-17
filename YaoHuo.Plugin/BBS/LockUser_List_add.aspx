<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockUser_List_add.aspx.cs" Inherits="YaoHuo.Plugin.BBS.LockUser_List_add" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("加黑操作|加黑操作|To Lock"), wmlVo));
    strhtml.Append("<div class=\"title\">" + this.GetLang("加黑操作|加黑操作|To Lock") + "</div>");
    strhtml.Append(this.ERROR);
    strhtml.Append("<div class=\"tip\">");
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>" + this.GetLang("添加成功！|添加成功！|Submit successfully!") + "</b> <a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid=" + this.backurlid + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "&amp;toclassid=" + this.toclassid + "\">" + this.GetLang("返回|返回|Back") + "</a><br/>");
    }
    else if (this.INFO == "NOTNUM")
    {
        strhtml.Append("<b>" + this.GetLang("要输入数字类型！|要输入数字类型！|Please input Number!") + "</b><br/>");
    }
    else if (this.INFO == "NOTALLOW")
    {
        strhtml.Append("<b>" + this.GetLang("你没有权限对此用户操作！|你沒有權限對此用戶操作！|You do not have permission to Lock this user!") + "</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/lockuser_list_add.aspx\" method=\"post\">");
    strhtml.Append(this.GetLang("加黑ID号|被锁会员ID|To Lock User ID") + "<br/>");
    strhtml.Append("<input type=\"text\" size=\"35\" name=\"touserid\" value=\"" + this.touserid + "\" /><br/>");
    strhtml.Append("<input type=\"hidden\"  name=\"toclassid\" value=\"0" + this.toclassid + "\" />");
    strhtml.Append(this.GetLang("加黑天数|加黑天数|Lock Days") + "<br/>");
    strhtml.Append("<input type=\"text\" size=\"35\" name=\"lockdate\" value=\"3" + "\" /><br/>");
    strhtml.Append("(初犯3天，二次7-15天)<br/>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"backurlid\" value=\"" + backurlid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("提 交|提 交|Submit") + "\"/>");
    strhtml.Append("</form></div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/lockuser_list.aspx?action=class&amp;backurlid=" + backurlid + "&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "&amp;toclassid=" + this.toclassid + "\">返回上级</a>");
    strhtml.Append("</div></div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>