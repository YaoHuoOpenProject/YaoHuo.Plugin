<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToMyBankMoney.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ToMyBankMoney" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang("银行取款|银行取款|bank"), wmlVo));
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>");
        strhtml.Append(this.GetLang("操作成功！|操作成功！|Successfully Update"));
        strhtml.Append("</b><br/>");
    }
    else if (this.INFO == "CLOSE")
    {
        strhtml.Append("<b>站长关闭此功能！</b><br/>");
    }
    else if (this.INFO == "NUM")
    {
        strhtml.Append("<b>金额需要数字！</b><br/>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<b>金额不能小于1！</b><br/>");
    }
    else if (this.INFO == "NOBANKMONEY")
    {
        strhtml.Append("<b>银行存款不够:" + allmoney + "</b><br/>");
    }
    else if (this.INFO == "NOMONEY")
    {
        strhtml.Append("<b>我的钱不够:" + tomoney + "</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"subtitle\">" + this.GetLang("银行取款|银行取款|bank") + "</div>");
    if (this.INFO != "OK")
    {
        strhtml.Append("<div class=\"content\">");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("上次结息时间:" + userVo.myBankTime + "<br/>");
        strhtml.Append("我的银行存款:" + userVo.myBankMoney + "<br/>");
        strhtml.Append("我的活动" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + ":" + userVo.money + "<br/>");
        strhtml.Append("存款月利率:" + lvlmoney + "%<br/>");
        strhtml.Append("取款手续费:" + mainmoney + "%<br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/tomybankmoney.aspx\" method=\"post\">");
        strhtml.Append("输入金额：<br/>");
        strhtml.Append("<input type=\"number\" style=\"width:70%\" min=\"100\" required=\"required\" class=\"txt\" name=\"tomoney\" value=\"" + this.tomoney + "\"/><br/>");
        strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"backurl\" value=\"" + backurl + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"type\" value=\"" + type + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"sid\" value=\"" + sid + "\"/>");
        if (this.type != "1")
        {
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
            strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"g\" value=\"" + this.GetLang("确定存款|确定存款|确定存款") + "\" /><br/>");
        }
        else
        {
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"sub\"/>");
            strhtml.Append("<input type=\"submit\"class=\"btn\"  name=\"g\" value=\"" + this.GetLang("确定取款|确定取款|确定取款") + "\" /><br/>");
        }
        if (this.type != "1")
        {
            strhtml.Append("<br/><div class=\"content\"><a class=\"urlbtn\" href=\"" + this.http_start + "bbs/tomybankmoney.aspx?type=1&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">" + this.GetLang("取款操作|取款操作|取款操作") + "</a></div>");
        }
        else
        {
            strhtml.Append("<br/><div class=\"content\"><a class=\"urlbtn\" href=\"" + this.http_start + "bbs/tomybankmoney.aspx?type=0&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">" + this.GetLang("存款操作|存款操作|存款操作") + "</a></div>");
        }
        strhtml.Append("</form>");
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回来源页</a> ");
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