<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyNick.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyNick" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("修改我的昵称|修改我的昵称|Change My NickName"), wmlVo));
    StringBuilder strhtml = new StringBuilder();
    strhtml.Append("<div class=\"title\">修改我的昵称</div>");
    if (!string.IsNullOrEmpty(ERROR))
    {
        strhtml.Append("<div class=\"tip\">" + ERROR + "</div>");
    }
    switch (INFO)
    {
        case "OK":
            strhtml.Append("<div class=\"tip\"><b>更新成功！</b></div>");
            break;
        case "NULL":
            strhtml.Append("<div class=\"tip\"><b>昵称不能为空！</b></div>");
            break;
        case "HASEXIST":
            strhtml.Append("<div class=\"tip\"><b>本网站中此昵称已存在！</b></div>");
            break;
        case "TOOFREQUENT":
            strhtml.Append("<div class=\"tip\"><b>每自然月只能修改1次昵称，请下个月再试！</b></div>");
            break;
        case "FORBIDDEN":
            strhtml.Append("<div class=\"tip\"><b>昵称包含禁用词，请重新输入！</b></div>");
            break;
    }
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<Form name=\"f\" action=\"" + http_start + "bbs/ModifyNick.aspx\" method=\"post\">");
    strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"gomod\" />");
    strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
    strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
    strhtml.Append("<input name=\"backurl\"  type=\"hidden\" value=\"" + this.backurl + "\"  />");
    strhtml.Append("<input type=\"text\" maxlength=\"9\" name=\"tonickname\" class=\"txt\" value=\"" + tonickname + "\"/><br/>");
    strhtml.Append("(1-" + this.num + "字内，禁用符号)<br/>");
    strhtml.Append("<input type=\"submit\" id=\"submit\" name=\"submit\" class=\"btn\" value=\"修 改\" /><br/>");
    strhtml.Append("</Form>");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
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