<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyInfo.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyInfo" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("修改详细资料|修改详细资料|Modify Details"), wmlVo));//显示头 
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
            strhtml.Append("<b>更新成功！</b>");
            strhtml.Append("<br/>");
        }
        strhtml.Append("用户ID：" + userVo.userid + "<br/>");
        strhtml.Append("昵称：" + userVo.nickname + "<br/>");
        strhtml.Append("性别：");
        strhtml.Append("<select name=\"sex"+r+"\" value=\"" + userVo.sex.ToString() + "\"><option value=\"1\">1_男</option><option value=\"0\">0_女</option></select><br/>");
        strhtml.Append("年龄：");
        strhtml.Append("<input type=\"text\" name=\"age" + r + "\" value=\"" + userVo.age.ToString() + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("身高：");
        strhtml.Append("<input type=\"text\" name=\"shenggao" + r + "\" value=\"" + userVo.shenggao + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("体重：");
        strhtml.Append("<input type=\"text\" name=\"tizhong" + r + "\" value=\"" + userVo.tizhong + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("星座：");
        strhtml.Append("<input type=\"text\" name=\"xingzuo" + r + "\" value=\"" + userVo.xingzuo + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("爱好：");
        strhtml.Append("<input type=\"text\" name=\"aihao" + r + "\" value=\"" + aihao + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("婚否：");
        strhtml.Append("<input type=\"text\" name=\"fenfuo" + r + "\" value=\"" + userVo.fenfuo + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("职务：");
        strhtml.Append("<input type=\"text\" name=\"zhiye" + r + "\" value=\"" + userVo.zhiye + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("城市：");
        strhtml.Append("<input type=\"text\" name=\"city" + r + "\" value=\"" + userVo.city + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("手机号：");
        strhtml.Append("<input type=\"text\" name=\"mobile" + r + "\" value=\"" + userVo.mobile + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("QQ号码：");
        strhtml.Append("<input type=\"text\" name=\"qq" + r + "\" value=\"" + qq + "\" maxlength=\"50\" /><br/>");
        strhtml.Append("E-MAIL：");
        strhtml.Append("<input type=\"text\" name=\"email" + r + "\" value=\"" + userVo.email + "\" /><br/>");
        strhtml.Append("<anchor><go href=\"" + http_start + "bbs/ModifyInfo.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        strhtml.Append("<postfield  name=\"action\"  value=\"gomod\" />");
        strhtml.Append("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
        strhtml.Append("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
        strhtml.Append("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
        strhtml.Append("<postfield name=\"sex\" value=\"$(sex" + r + ")\"/>");
        strhtml.Append("<postfield name=\"age\" value=\"$(age" + r + ")\"/>");
        strhtml.Append("<postfield name=\"shenggao\" value=\"$(shenggao" + r + ")\"/>");
        strhtml.Append("<postfield name=\"tizhong\" value=\"$(tizhong" + r + ")\"/>");
        strhtml.Append("<postfield name=\"xingzuo\" value=\"$(xingzuo" + r + ")\"/>");
        strhtml.Append("<postfield name=\"aihao\" value=\"$(aihao" + r + ")\"/>");
        strhtml.Append("<postfield name=\"fenfuo\" value=\"$(fenfuo" + r + ")\"/>");
        strhtml.Append("<postfield name=\"zhiye\" value=\"$(zhiye" + r + ")\"/>");
        strhtml.Append("<postfield name=\"city\" value=\"$(city" + r + ")\"/>");
        strhtml.Append("<postfield name=\"mobile\" value=\"$(mobile" + r + ")\"/>");
        strhtml.Append("<postfield name=\"qq\" value=\"$(qq" + r + ")\"/>");
        strhtml.Append("<postfield name=\"email\" value=\"$(email" + r + ")\"/>");
        strhtml.Append("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
        strhtml.Append("</go>" + this.GetLang("修改|修改|Update") + "</anchor><br/>");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</p>");
        Response.Write(strhtml);
    }
    else
    {
        strhtml.Append("<div class=\"title\">修改我的资料</div>");
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
        strhtml.Append("<form name =\"f\" action=\"" + http_start + "bbs/ModifyInfo.aspx\" method=\"post\">");
        //strhtml.Append("用户ID：" + userVo.userid + "<br/>");
        //strhtml.Append("昵称：" + userVo.nickname + "<br/>");
        //strhtml.Append("性别：");
        strhtml.Append("<select style=\"display:none;\" name=\"sex\"><option value=\"" + userVo.sex.ToString() + "\">" + userVo.sex.ToString() + "</option><option value=\"1\">1_男</option><option value=\"0\">0_女</option></select>");
        strhtml.Append("年龄 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"age\" value=\"" + userVo.age.ToString() + "\" maxlength=\"2\" /><br/>");
        strhtml.Append("身高 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"shenggao\" value=\"" + userVo.shenggao + "\" maxlength=\"3\" /><br/>");
        strhtml.Append("体重 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"tizhong\" value=\"" + userVo.tizhong + "\" maxlength=\"3\" /><br/>");
        strhtml.Append("星座 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"xingzuo\" value=\"" + userVo.xingzuo + "\" maxlength=\"3\" /><br/>");
        strhtml.Append("爱好 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"aihao\" value=\"" + aihao + "\" maxlength=\"10\" /><br/>");
        strhtml.Append("婚否 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"fenfuo\" value=\"" + userVo.fenfuo + "\" maxlength=\"2\" /><br/>");
        strhtml.Append("职业 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"zhiye\" value=\"" + userVo.zhiye + "\" maxlength=\"5\" /><br/>");
        strhtml.Append("城市 ");
        strhtml.Append("<input style=\"width:50%\" type=\"text\" name=\"city\" value=\"" + userVo.city + "\" maxlength=\"8\" /><br/>");
        strhtml.Append("手机 ");
        strhtml.Append("<input style=\"width:50%\" placeholder=\"非登录手机号\" oninput=\"if(value.length>11)value=value.slice(0,11)\" type=\"tel\" name=\"mobile\" value=\"" + userVo.mobile + "\" maxlength=\"11\" /><br/>");        
        strhtml.Append("邮箱 ");
        strhtml.Append("<input style=\"width:50%\" type=\"email\" name=\"email\" value=\"" + userVo.email + "\" maxlength=\"30\"/><br/>");
        //strhtml.Append("签名 ");
        strhtml.Append("<input style=\"width:50%;display:none\" type=\"text\" maxlength=\"15\" name=\"remark\" value=\"" + userVo.remark + "\" />");
        strhtml.Append("QQ号 ");
        strhtml.Append("<input style=\"width:47%\" oninput=\"if(value.length>11)value=value.slice(0,11)\" type=\"number\" name=\"qq\" value=\"" + qq + "\" maxlength=\"11\" /><br/>");
        strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"gomod\" />");
        strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
        strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
        //strhtml.Append("<input name=\"sid\"  type=\"hidden\" value=\"" + this.sid + "\"  />");  
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