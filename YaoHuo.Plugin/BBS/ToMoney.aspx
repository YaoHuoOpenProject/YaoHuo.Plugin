<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToMoney.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ToMoney" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang(siteVo.sitemoneyname + "转帐|" + siteVo.sitemoneyname + "转帐|Money to change"), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p>");
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
            strhtml.Append("<b>会员ID和金额需要数字！</b><br/>");
        }
        else if (this.INFO == "NOTUSER")
        {
            strhtml.Append("<b>用户不存！</b><br/>");
        }

        else if (this.INFO == "NOTMONEY")
        {
            strhtml.Append("<b>你的币只有" + userVo.money + "，需要大于500以上才可以转！</b><br/>");
        }
        else if (this.INFO == "MAXMONEY")
        {
            strhtml.Append("<b>每次不能大于" + maxs + "个！</b><br/>");
        }

        if (this.INFO != "OK")
        {
            strhtml.Append("会员ID：<br/>");
            strhtml.Append("<input type=\"text\" name=\"touserid" + r + "\" value=\"" + this.touserid + "\"/><br/>");

            strhtml.Append("输入金额：<br/>");
            strhtml.Append("<input type=\"text\" name=\"tomoney" + r + "\" value=\"" + this.tomoney + "\"/><br/>");

            strhtml.Append("说明原因：<br/>");
            strhtml.Append("<input type=\"text\" name=\"remark" + r + "\" value=\"" + this.remark + "\"/><br/>");

            strhtml.Append("<anchor><go href=\"" + http_start + "bbs/toMoney.aspx\" method=\"post\" accept-charset=\"utf-8\">");
            strhtml.Append("<postfield name=\"action\" value=\"add\"/>");
            strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<postfield name=\"backurl\" value=\"" + backurl + "\"/>");
            strhtml.Append("<postfield name=\"touserid\" value=\"$(touserid" + r + ")\"/>");
            strhtml.Append("<postfield name=\"tomoney\" value=\"$(tomoney" + r + ")\"/>");
            strhtml.Append("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
            strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\"/>");
            strhtml.Append("</go>" + this.GetLang("执行赠送|执行赠送|Send") + "</anchor><br/><br/>");

            if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
            {
                strhtml.Append("<anchor><go href=\"" + http_start + "bbs/toMoney.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                strhtml.Append("<postfield name=\"action\" value=\"sub\"/>");
                strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
                strhtml.Append("<postfield name=\"touserid\" value=\"$(touserid" + r + ")\"/>");
                strhtml.Append("<postfield name=\"tomoney\" value=\"$(tomoney" + r + ")\"/>");
                strhtml.Append("<postfield name=\"remark\" value=\"$(remark" + r + ")\"/>");
                strhtml.Append("<postfield name=\"backurl\" value=\"" + backurl + "\"/>");
                strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\"/>");
                strhtml.Append("</go>" + this.GetLang("执行冻结|执行冻结|sub") + "</anchor><br/><br/>");
            }
        }

        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</p>");
        Response.Write(strhtml);
    }
    else //2.0界面
    {
        strhtml.Append("<div class=\"title\">" + this.GetLang(siteVo.sitemoneyname + "转帐|" + siteVo.sitemoneyname + "转帐|Money to change") + "</div>");
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
            strhtml.Append("<b>会员ID和金额需要数字！</b><br/>");
        }
        else if (this.INFO == "NOTUSER")
        {
            strhtml.Append("<b>用户不存！</b><br/>");
        }

        else if (this.INFO == "NOTMONEY")
        {
            strhtml.Append("<b>你的币只有" + userVo.money + "，需要大于500以上才可以转！</b><br/>");
        }
        else if (this.INFO == "MAXMONEY")
        {
            strhtml.Append("<b>每次不能大于" + maxs + "个！</b><br/>");
        }
        strhtml.Append("</div>");
        if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
        {
            strhtml.Append("<div class=\"subtitle\">");
            if (this.type != "1")
            {
                strhtml.Append("转让｜<a class=\"urlbtn\" href=\"" + this.http_start + "bbs/tomoney.aspx?type=1&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">冻结</a>");
            }
            else
            {
                strhtml.Append("<a class=\"urlbtn\" href=\"" + this.http_start + "bbs/tomoney.aspx?siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">转让</a>｜冻结");
            }

            strhtml.Append("</div>");
        }
        if (this.INFO != "OK")
        {
            strhtml.Append("<div class=\"content\">");

            strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/toMoney.aspx\" method=\"post\">");
            strhtml.Append("会员ID号<br/>");
            strhtml.Append("<input type=\"text\" size=\"44\" class=\"txt\" name=\"touserid\" value=\"" + this.touserid + "\"/><br/>");

            strhtml.Append("转账金额<br/>");
            strhtml.Append("<input type=\"text\" size=\"44\" class=\"txt\" name=\"tomoney\" value=\"" + this.tomoney + "\"/><br/>");

            strhtml.Append("备注原因<br/>");
            strhtml.Append("<input type=\"text\" size=\"44\" class=\"txt\" name=\"remark\" value=\"感谢支持" + this.remark + "\"/><br/>");
            strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"backurl\" value=\"" + backurl + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"type\" value=\"" + type + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"sid\" value=\"" + sid + "\"/>");

            if (this.type != "1")
            {
                strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
                strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"g\" value=\"" + this.GetLang("执行赠送|执行赠送|Send") + "\" /><br/>");
            }
            else
            {
                if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
                {
                    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"sub\"/>");
                    strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"g\" value=\"" + this.GetLang("执行冻结|执行冻结|go sub") + "\" /><br/>");
                }
            }

            if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
            {
                if (this.type != "1")
                {
                    //strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/tomoney.aspx?type=1&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">" + this.GetLang("冻结操作|冻结操作|sub") + "</a>");
                }
            }

            strhtml.Append("</form>");
            strhtml.Append("</div>");
        }
        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a> ");
        if (isWebHtml == "")
        {
            strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
            strhtml.Append(WapTool.GetVS(wmlVo));
        }
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

    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>


