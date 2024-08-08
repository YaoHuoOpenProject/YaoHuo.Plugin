<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyRemark.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyRemark" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("修改详细资料|修改详细资料|Modify Details"), wmlVo));//显示头 
StringBuilder strhtml=new StringBuilder ();
    if (ver == "1")
    {
        strhtml.Append("<p>");
        if (ERROR != "")
        {
            strhtml.Append(ERROR);
            strhtml.Append("<br/>");
        }
        if (INFO == "OK")
        {
            strhtml.Append("<b>更新成功！</b>");
            strhtml.Append("<br/>");
        }
        strhtml.Append("我的个性签名：<br/>");
        strhtml.Append("<input type=\"text\"  onkeyup=\"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" onpaste=\"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" oncontextmenu = \"value=value.replace(/[^\\a-\\z\\A-\\Z0-9\\u4E00-\\u9FA5]/g,'')\" maxlength=\"15\" style=\"width:96%;height:25px;\" name=\"remark\" class=\"txt\" value=\"" + userVo.remark + "\" /><br/>");
        strhtml.Append("(0-" + this.num + "字内，禁用代码)<br/>");
        strhtml.Append("<anchor><go href=\"" + http_start + "bbs/ModifyRemark.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        strhtml.Append("<postfield  name=\"action\"  value=\"gomod\" />");
        strhtml.Append("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
        strhtml.Append("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
        strhtml.Append("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
        strhtml.Append("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
        strhtml.Append("</go>" + this.GetLang("修改|修改|Update") + "</anchor><br/>");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</p>");
        Response.Write(strhtml);
    }
    else
    {
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
        strhtml.Append("<input name=\"sid\"  type=\"hidden\" value=\"" + this.sid + "\"  />");  
        strhtml.Append("<input type=\"submit\" id=\"submit\" class=\"btn\" name=\"submit\" value=\"修 改\" /><br/>");
        strhtml.Append("</form>");
        strhtml.Append("</div>");
        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码  
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
        strhtml.Append("</div></div>");
        if (isWebHtml != "")
        {
            //string strhtml_list = strhtml.ToString();
            //int s = strhtml_list.IndexOf("<div class=\"title\">");
            //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }
        Response.Write(strhtml);
    }
Response.Write(WapTool.showDown(wmlVo));   //显示底部
 %>