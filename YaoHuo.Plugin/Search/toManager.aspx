<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToManager.aspx.cs" Inherits="YaoHuo.Plugin.Search.toManager" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("管理会员|管理会员|user admin"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0界面
{
    Response.Write("<div class=\"title\">" + this.GetLang("更新会员操作|更新操作|Update operation") + "</div>");
    Response.Write("<div class=\"tip\">");
    Response.Write(this.ERROR);
    if (this.INFO == "OK")
    {
        Response.Write("<b>");
        Response.Write(this.GetLang("修改成功！|修改成功！|Successfully Update"));
        Response.Write("</b><br/>");
        Response.Write("<a href=\"" + this.http_start + "search/book_list.aspx?siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "\">" + this.GetLang("返回|返回|Back") + "</a><br/>");
    }
    else if (this.INFO == "PWERROR")
    {
        Response.Write("<b>密码错误，请重新录入我的密码！</b><br/>");
    }
    else if (this.INFO == "NUM")
    {
        Response.Write("<b>");
        Response.Write(this.GetLang("需要输入数字！|需要输入数字！|need num"));
        Response.Write("</b><br/>");
    }
    Response.Write("</div>");
    if (this.INFO != "OK")
    {
        Response.Write("<div class=\"content\">");
        Response.Write("用户ID：<a href=\"" + http_start + "bbs/userinfomore.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + touserid + "\">" + touserVo.userid + "</a><br/>");
        Response.Write("用户名：" + touserVo.username + "<br/>");
        if (touserVo.userid == 1000 || touserVo.userid == 956)
        {
            Response.Write("<a href=\"" + http_start + "admin/modifyusername00.aspx?siteid=" + this.siteid + "\">修改用户名</a><br/>");
        }
        Response.Write("昵称：" + touserVo.nickname + "<br/>");
        Response.Write(siteVo.sitemoneyname + " " + touserVo.money + "【<a href=\"" + this.http_start + "bbs/tomoney.aspx?siteid=" + this.siteid + "&amp;touserid="+touserid+"&amp;backurl=" + HttpUtility.UrlEncode("search/toManager.aspx?siteid=" + this.siteid + "&amp;touserid=" + touserid) + "\">手工增减</a>/<a href=\"" + this.http_start + "bbs/banklist.aspx?siteid=" + this.siteid + "&amp;key="+touserid+"&amp;backurl=" + HttpUtility.UrlEncode("search/toManager.aspx?siteid=" + this.siteid + "&amp;touserid=" + touserid) + "\">日志</a>】<br/>");
        Response.Write("RMB ¥" + touserVo.RMB.ToString("f2") + "【<a href=\"" + this.http_start + "chinabank_wap/RMBAdd.aspx?siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode("search/toManager.aspx?siteid=" + this.siteid + "&amp;touserid=" + touserid) + "&amp;action=search&amp;touserid=" + touserid + "\">手工增减</a>/<a href=\"" + this.http_start + "chinabank_wap/banklist.aspx?siteid=" + this.siteid + "&amp;tositeid="+this.siteid+"&amp;touserid=" + touserid + "&amp;backurl=" + HttpUtility.UrlEncode("search/toManager.aspx?siteid=" + this.siteid + "&amp;touserid=" + touserid) + "\">日志</a>】<br/>");
        Response.Write("注册时间 " + string.Format("{0:yy-MM-dd HH:mm}", touserVo.RegTime) + "<br/>");
        Response.Write("最后登录 " + string.Format("{0:yy-MM-dd HH:mm}", touserVo.LastLoginTime) + "<br/>");
        Response.Write("<form name=\"f\" action=\"" + http_start + "search/toManager.aspx\" method=\"post\">");
        Response.Write(this.GetLang("重置密码|重置密码|change his password") + "<br/>");
        Response.Write("<input type=\"text\" size=\"36\" name=\"topassword\" value=\"\"/><br/>");
        //Response.Write("(不重置密码留空)<br/>");
        Response.Write(this.GetLang("银行存款|银行存款|change his bank money") + "<br/>");
        Response.Write("<input type=\"text\" size=\"36\" name=\"tobankmoney\" value=\"" + tobankmoney + "\"/><br/>");
        Response.Write(this.GetLang("重置经验|重置经验|change his expR") + "<br/>");
        Response.Write("<input type=\"text\" size=\"36\" name=\"toexpR\" value=\"" + toexpR + "\"/><br/>");
        Response.Write(this.GetLang("管理权限|管理权限|管理权限 ") + " ");
        Response.Write("<select name=\"tomanagerlvl\">");
        Response.Write("<option value=\"" + touserVo.managerlvl + "\">" + touserVo.managerlvl + "</option>");
        Response.Write("<option value=\"00\">00_超级管理员</option>");
        Response.Write("<option value=\"01\">01_站长</option>");
        Response.Write("<option value=\"03\">03_总编辑</option>");
        Response.Write("<option value=\"04\">04_总版主</option>");
        Response.Write("<option value=\"02\">02_普通</option>");
        Response.Write("</select><br/>");
        Response.Write(this.GetLang("会员身份|会员身份|his ID") + " ");
        Response.Write("<select name=\"tosessiontimeout\">");
        if (touserVo.SessionTimeout.ToString() != "0")
        {
            Response.Write("<option value=\"" + touserVo.SessionTimeout + "\">" + WapTool.GetUrlFileName(touserVo.idname) + "</option>");
        }
        Response.Write("<option value=\"0\">0—普通会员</option>");
        for (int i = 0; (idlistVo != null && i < idlistVo.Count); i++)
        {
            Response.Write("<option value=\"" + idlistVo[i].id + "\">" + idlistVo[i].id + "—"  +WapTool.GetUrlFileName(idlistVo[i].subclassName) + "</option>");
        }
        Response.Write("</select><br/>");
        Response.Write(WapTool.showIDEndTime(touserVo.siteid, touserVo.userid, touserVo.endTime, this.lang) + "<br/>");
        Response.Write(this.GetLang("更改时间|更改时间|change date") + " ");
        Response.Write("<input type=\"text\" size=\"21\" name=\"tochangedate\" value=\"" + tochangedate + "\"/><br/>时间格式:2023-01-01<br/>");
        Response.Write(this.GetLang("锁定用户|锁定用户|Lock ") + " ");
        Response.Write("<select name=\"tolockuser\">");
        if (touserVo.LockUser.ToString() == "1")
        {
            Response.Write("<option value=\"1\">是</option>");
        }
        Response.Write("<option value=\"0\">否</option>");
        Response.Write("<option value=\"1\">是</option>");
        Response.Write("</select><br/>");
        Response.Write("我的密码 <input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" style=\"max-width:202px;width:63%\"/><br/>");
        Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"touserid\" value=\"" + (touserid) + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
        Response.Write("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("保 存|保 存|Save") + "\"/>");
        Response.Write("</form>");
        Response.Write("</div>");
    }
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"" + this.http_start + "search/book_list.aspx?siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.page + "\">" + this.GetLang("返回上级|返回上级|Back to list") + "</a> ");
    Response.Write("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a>");
    Response.Write("</div></div>");
}

Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>