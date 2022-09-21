<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMoney_FreeMain.aspx.cs" Inherits="YaoHuo.Plugin.BBS.SendMoney_FreeMain" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("打赏送币|打赏送币|Modify Reply"), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        Response.Write("<p>");
        Response.Write(this.ERROR);
        if (this.INFO == "OK")
        {
            Response.Write("<b>" + this.GetLang("奖励送币成功！|奖励送币成功！|Modify successfully!") + "</b> ");
            Response.Write("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "\">返回</a><br/>");
        }
        else if (this.INFO == "NULL")
        {
            Response.Write("<b>" + this.GetLang("需要数字！|需要数字！|需要数字!") + "</b><br/>");
        }
        else if (this.INFO == "ERR")
        {
            Response.Write("<b>" + this.GetLang("奖励送币失败!原因: 1.我的币不够了；2.输入参数不对；3.不能自己赏自己！|ERR！|ERR!") + "</b><br/>");
        }
        Response.Write("我当前币有：" + userVo.money + "个<br/>");
        Response.Write("输入奖励给会员ID:" + touserid + "币值*:<br/>");
        Response.Write("<input type=\"text\"  name=\"sendmoney" + r + "\" value=\"\" /><br/>");
        Response.Write("理由:<br/><input type=\"text\"  name=\"remark" + r + "\" value=\"\" /><br/>");
        Response.Write("<anchor><go href=\"" + http_start + "bbs/sendmoney_freeMain.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        Response.Write("<postfield name=\"action\" value=\"gomod\"/>");
        Response.Write("<postfield name=\"id\" value=\"" + id + "\"/>");
        Response.Write("<postfield name=\"reid\" value=\"" + reid + "\"/>");
        Response.Write("<postfield name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<postfield name=\"page\" value=\"" + page + "\"/>");
        Response.Write("<postfield name=\"lpage\" value=\"" + lpage + "\"/>");
        Response.Write("<postfield name=\"touserid\" value=\"" + touserid + "\"/>");
        Response.Write("<postfield name=\"sendmoney\" value=\"$(sendmoney" + r + ")\"/>");
        Response.Write("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
        Response.Write("<postfield name=\"sid\" value=\"" + sid + "\"/>");
        Response.Write("</go>" + this.GetLang("确定|确定|submit") + "</anchor><br/>");
        Response.Write("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "\">返回主题</a>");
        Response.Write(WapTool.GetVS(wmlVo));
        Response.Write("</p>");
    }
    else //2.0界面
    {
        Response.Write("<div class=\"title\">" + this.GetLang("打赏送币给楼主|打赏送币给楼主|Modify") + "</div>");
        Response.Write(this.ERROR);
        Response.Write("<div class=\"tip\">");
        if (this.INFO == "OK")
        {
            Response.Write("<b>" + this.GetLang("打赏送币成功！|打赏送币成功！|Modify successfully!") + "</b> ");
            Response.Write("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "\">返回</a><br/>");
        }
        else if (this.INFO == "NULL")
        {
            Response.Write("<b>" + this.GetLang("需要数字！|需要数字！|需要数字!") + "</b><br/>");
        }
        else if (this.INFO == "ERR")
        {
            Response.Write("<b>" + this.GetLang("打赏送币失败，请返回帖子重试。可能原因: 1.妖晶不足；2.参数不对；3.不能赏自己|ERR！|ERR!") + "</b><br/>");
        }
        Response.Write("</div>");
        Response.Write("<div class=\"content\">");
        Response.Write("<form name=\"f\" aciont=\"" + this.http_start + "bbs/sendmoney_freeMain.aspx\" method=\"post\">");
        Response.Write("我当前有 " + userVo.money + " 个妖晶<span style=\"line-height:50px;\"></span><br/>");
        Response.Write("楼主ID号 " + touserid + "<div class=\"dashed\"></div>");
        Response.Write("选择打赏数量<span style=\"line-height:10px;\"></span><br/>");
        Response.Write("<select name=\"sendmoney\">");
        Response.Write("<option value=\"100\">100妖晶</option>");
        Response.Write("<option value=\"300\">300妖晶</option>");
        Response.Write("<option value=\"520\">520妖晶</option>");
        Response.Write("<option value=\"666\">666妖晶</option>");
        Response.Write("<option value=\"888\">888妖晶</option>");
        Response.Write("<option value=\"1000\">1000妖晶</option>");
        Response.Write("<option value=\"2333\">2333妖晶</option>");
        Response.Write("<option value=\"5000\">5000妖晶</option>");
        Response.Write("<option value=\"6666\">6666妖晶</option>");
        Response.Write("<option value=\"8888\">8888妖晶</option>");
        Response.Write("<option value=\"10000\">10000妖晶</option>");
        Response.Write("</select>");
        Response.Write("<br/>填写打赏原因<br/><input type=\"text\" placeholder=\"表达认同或感谢，可以不填写\" style=\"width:97%;\" name=\"remark\" value=\"\" /><br/>");
        Response.Write("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        Response.Write("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"reid\" value=\"" + reid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
        Response.Write("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
        Response.Write("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("确 定|确 定|submit") + "\"/><br/>");
        Response.Write("</form></div>");
        Response.Write("<div class=\"btBox\"><div class=\"bt1\">");
        Response.Write("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "\">返回主题</a>");
        Response.Write("</div></div>");
    }
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>
