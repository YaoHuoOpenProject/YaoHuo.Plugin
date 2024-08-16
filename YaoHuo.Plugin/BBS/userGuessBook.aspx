<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userGuessBook.aspx.cs" Inherits="YaoHuo.Plugin.BBS.userGuessBook" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("留言板|留言板|GuessBook"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    //输出
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
}
else //2.0界面
{
    //会员可见
 	if (this.IsCheckManagerLvl("|00|01|02|03|04|","")==true)
    {
    strhtml.Append("<script type=\"text/javascript\" defer src=\"/NetCSS/JS/HyperLink.js\"></script>");
    strhtml.Append("<div class=\"title\">" + this.GetLang("留言板|留言板|GuessBook") + "</div>");
    strhtml.Append(ERROR);
    if (this.INFO != "") strhtml.Append("<div class=\"tip\">");
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>留言成功！</b><br/> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>审核后显示！</b> <br/>");
        }
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<b>留言内容不能为空！</b><br/>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<b>请不要发重复内容！</b><br/>");
    }
    else if (this.INFO == "WAITING")
    {
        strhtml.Append("<b>请再过" + this.KL_CheckIPTime + "秒后操作！</b><br/>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<b>抱歉，您已超过一天留言总数" + KL_CheckZoneReCount + "条了，请明天再来！</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
    }
    if (this.INFO != "") strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    //显示输入框
    strhtml.Append("<form id='MessageForm' name=\"f\" action=\"" + http_start + "bbs/userguessbook.aspx\" method=\"post\">");
    //strhtml.Append("<a href=\"" + this.http_start + "bbs/userguessbook_pic.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=" + this.ot + "&amp;go=" + this.r + "\"><img src=\"" + this.http_start + "bbs/face/" + face + ".gif\" alt=\"表情\"/></a>");
	strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
	strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<textarea name=\"content\" oninput=\"adjustTextareaHeight(this)\" rows=\"5\" class=\"KL_textarea\" style=\"width: 98.6%; \" required=\"required\"></textarea>");
	strhtml.Append("</div>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"face\" value=\"" + face + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"g\" id='Submitcomment' value=\"我要留言\"/>");
    strhtml.Append("</form></div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a ");
        if (this.ot != "1")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + this.http_start + "bbs/userguessbook.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=0&amp;go=" + this.r + "\">最新留言</a> ");
        strhtml.Append("<a ");
        if (this.ot == "1")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + this.http_start + "bbs/userguessbook.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=1&amp;go=" + this.r + "\">最早留言</a> ");
        strhtml.Append("</div></div>");
    //显示列表
    strhtml.Append("<!--listS-->");
    kk = kk + ((CurrentPage - 1) * pageSize) - 1;
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        if (ot == "1")
        {
            index = (kk + 1);
        }
        else
        {
            index = (total - kk);
        }
        strhtml.Append("[" + index + "楼] ");
        //strhtml.Append("[<a href=\"" + this.http_start + "bbs/userguessbook_re.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;ot=" + this.ot + "\">回</a>]");
        //--------------------管理员
        if (this.userid == listVo[i].userid.ToString() || this.IsUserManager(this.userid, userVo.managerlvl, ""))  //自己删除自己的帖子
        {
            strhtml.Append("[<a href=\"" + this.http_start + "bbs/userguessbook_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;touserid=" + this.touserid + "&amp;ot=" + this.ot + "\">删</a>]");
        }
        strhtml.Append("<span class=\"retext\">");
        strhtml.Append(listVo[i].content);
        strhtml.Append("</span></div>");
        kk = kk + 1;
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">暂无留言记录！</div>");
    }
    strhtml.Append("<!--listE-->");
    //显示导航分页
    strhtml.Append(linkURL);
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + this.touserid + "" + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\">我的地盘</a>");
    strhtml.Append("</div></div>");
    //输出
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo)); 
    //会员可见结束
}
}
//显示底部
Response.Write(WapTool.showDown(wmlVo)); %>