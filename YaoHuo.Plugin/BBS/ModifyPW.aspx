<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPW.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyPW" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("修改我的密码|修改我的密码|Change My Password"), wmlVo));//显示头 
    StringBuilder strhtml = new StringBuilder();
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
        
        strhtml.Append("<b>更新成功！请返回重新登录！</b>");
        strhtml.Append("<br/>");
    }
    else if (INFO == "NULL")
    {
        
        strhtml.Append("<b>不能为空！</b>");
        strhtml.Append("<br/>");
    }
    else if (INFO == "OLDERR")
    {
       
        strhtml.Append("<b>原密码错误！</b>");
        strhtml.Append("<br/>");
    }
    else if (INFO == "TWOERR")
    {
       
        strhtml.Append("<b>原密码和旧密码不一样！</b>");
        strhtml.Append("<br/>");
    }

    strhtml.Append("原密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtoldPW\"  maxlength=\"50\" /><br/>");
    strhtml.Append("新密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtnewPW\"  maxlength=\"50\" /><br/>");
    strhtml.Append("确认新密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtrePW\"  maxlength=\"50\" /><br/>");
    strhtml.Append("<anchor><go href=\"" + http_start + "bbs/ModifyPW.aspx\" method=\"post\" accept-charset=\"utf-8\">");
    strhtml.Append("<postfield  name=\"action\"  value=\"gomod\" />");
    strhtml.Append("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
    strhtml.Append("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
    strhtml.Append("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
    strhtml.Append("<postfield name=\"txtoldPW\" value=\"$(txtoldPW)\"/>");
    strhtml.Append("<postfield name=\"txtnewPW\" value=\"$(txtnewPW)\"/>");
    strhtml.Append("<postfield name=\"txtrePW\" value=\"$(txtrePW)\"/>");
    strhtml.Append("</go>" + this.GetLang("修改|修改|Update") + "</anchor><br/>");

    strhtml.Append("注意：修改密码后，旧书签地址将无效，请登录后重新加入书签！<br/>");
    
       
    strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
    strhtml.Append(WapTool.GetVS(wmlVo));
    strhtml.Append("</p>");
    Response.Write(strhtml);
}
else
{
    strhtml.Append("<div class=\"subtitle\">修改我的密码</div>");
    if (ERROR != "")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append(ERROR);
        strhtml.Append("</div>");
    }
    if (INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>更新成功！请返回重新登录！</b>");
        strhtml.Append("</div>");
    }
    else if (INFO == "NULL")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>不能为空！</b>");
        strhtml.Append("</div>");
    }
    else if (INFO == "OLDERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>原密码错误！</b>");
        strhtml.Append("</div>");
    }
    else if (INFO == "TWOERR")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>原密码和旧密码不一样！</b>");
        strhtml.Append("</div>");
    }
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<form name =\"f\" action=\""+this.http_start+"bbs/ModifyPW.aspx\" method=\"post\">");
    strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"gomod\" />");
    strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
    strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
    strhtml.Append("<input name=\"sid\"  type=\"hidden\" value=\"" + this.sid + "\"  />");
    strhtml.Append("原密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtoldPW\"  class=\"txt\" maxlength=\"50\" /><br/>");
    strhtml.Append("新密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtnewPW\"  class=\"txt\" maxlength=\"50\" /><br/>");
    strhtml.Append("确认新密码：<br />");
    strhtml.Append("<input type=\"password\" name=\"txtrePW\"  class=\"txt\" maxlength=\"50\" /><br/>");
    strhtml.Append("<input type=\"submit\"  name=\"submit\" class=\"btn\"  value=\"修 改\" /><br/><br/>");
    
    strhtml.Append("注意：修改密码后，旧书签地址将无效，请登录后重新加入书签！");
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