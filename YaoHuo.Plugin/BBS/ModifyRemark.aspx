<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyRemark.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyRemark" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("修改详细资料|修改详细资料|Modify Details"), wmlVo));
    StringBuilder strhtml = new StringBuilder();
    strhtml.Append("<div class=\"title\">修改个性签名</div>");
    if (ERROR != "")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append(ERROR);
        strhtml.Append("</div>");
    }
    if (INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>更新成功！</b>");
        strhtml.Append("</div>");
    }
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<form name =\"f\" action=\"" + http_start + "bbs/ModifyRemark.aspx\" method=\"post\">");
    //strhtml.Append("我的个性签名<br/>");
    strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<input type=\"text\"  onkeyup=\"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" onpaste=\"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" oncontextmenu = \"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" maxlength=\"15\" style=\"width:98.6%;height:25px;\" name=\"remark\" class=\"txt\" value=\"" + userVo.remark + "\" />");
    strhtml.Append("</div>");
    strhtml.Append("(0-" + this.num + "字内，禁用代码)<br/>");
    strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"gomod\" />");
    strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
    strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
    strhtml.Append("<input type=\"submit\" id=\"submit\" class=\"btn\" name=\"submit\" value=\"修 改\" /><br/>");
    strhtml.Append("</form>");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
    strhtml.Append("</div></div>");
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    Response.Write(strhtml);
    Response.Write(WapTool.showDown(wmlVo));
%>