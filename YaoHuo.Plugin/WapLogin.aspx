<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WapLogin.aspx.cs" Inherits="WapLogin" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {
        wmlVo.timer = "2"; //5秒后自动跳转
        wmlVo.strUrl = backurl;
    }
    StringBuilder strhtml = new StringBuilder();
    //显示头
    Response.Write(WapTool.showTop(this.GetLang("登录网站|登錄網站|LogIn"), wmlVo));//显示头
    if (ver == "1")  //显示WAP1.0
    {
        strhtml.Append("<p>");
        if (this.INFO == "OK")
        {
            strhtml.Append("<b>" + this.GetLang("登录成功！|登录成功！！|Login Sucess!") + "</b><br/>");
            strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">等不及了，马上进入</a><br/>");
        }
        else if (this.INFO == "IDNULL")
        {
            strhtml.Append("<b>" + this.GetLang("*用户ID/用户名/手机号码不能为空！|*用户ID/用户名/手机号码不能为空！|username not null!") + "</b><br/>");
        }
        else if (this.INFO == "PASSNULL")
        {
            strhtml.Append("<b>" + this.GetLang("*密码不能为空！|*密码不能为空！|password not null!") + "</b><br/>");
        }
        else if (this.INFO == "NOTEXIST")
        {
            strhtml.Append("<b>" + this.GetLang("*用户ID/用户名/手机号码不存！|*用户ID/用户名/手机号码不存！|username not exist ") + "</b><br/>");
        }
        else if (this.INFO == "PASSERR")
        {
            strhtml.Append("<b>" + this.GetLang("*密码错误！|*密码错误！|Password error!") + "</b><br/>");
        }
        else if (this.INFO == "USERLOCK")
        {
            strhtml.Append("<b>" + this.GetLang("*用户被锁定！|*用户被锁定！|user locked!") + "</b><br/>");
        }
        else if (this.INFO == "MAXLOGIN")
        {
            strhtml.Append("<b>" + this.GetLang("*登录失败次数超过" + this.KL_LoginTime + "次了，请明天再来!") + "</b><br/>");
        }

        if (errorinfo == "config")
        {
            strhtml.Append("<b>如果老是进入登录界面，请联系站长在“网站默认配制”“[55].参数保存方式”选[1]</b><br/>");
        }

        if (this.INFO != "OK")
        {
            strhtml.Append(this.GetLang("用户ID/用户名/手机号码|用户ID/用户名/手机号码|userid/username/mobile") + "*:<br/>");

            strhtml.Append("<input type=\"text\" name=\"logname\" value=\"" + this.logname + "\"/><br/>");

            strhtml.Append(this.GetLang("密码|密码|password") + "*:<br/>");

            strhtml.Append("<input type=\"password\" name=\"logpass\" value=\"" + this.logpass + "\"/><br/>");

            strhtml.Append("<select name=\"savesid\" value=\"0\">");
            //if (this.ver == "3" || this.ver == "4")
            //{
            //    strhtml.Append("<option value=\"1\">[2]安全登录(WLan;网线上网)</option>");
            //    strhtml.Append("<option value=\"0\">[1]下次自动登录(移动网络)</option>");            
            //}
            //else
            //{
            strhtml.Append("<option value=\"0\">[1]下次自动登录(移动网络)</option>");
            strhtml.Append("<option value=\"1\">[2]安全登录(WLan;网线上网)</option>");
            //}
            strhtml.Append("</select><br/>");
            strhtml.Append("<anchor><go href=\"" + http_start + "waplogin.aspx\" method=\"post\" accept-charset=\"utf-8\">");
            strhtml.Append("<postfield name=\"action\" value=\"login\"/>");
            strhtml.Append("<postfield name=\"logname\" value=\"$(logname)\"/>");
            strhtml.Append("<postfield name=\"logpass\" value=\"$(logpass)\"/>");
            strhtml.Append("<postfield name=\"savesid\" value=\"$(savesid)\"/>");
            strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\"/>");
            strhtml.Append("<postfield name=\"backurl\" value=\"" + backurl + "\"/>");
            strhtml.Append("</go>" + this.GetLang("登 录|登 录|Login") + "</anchor><br/>");

            if (this.showQQLogin == "0")
            {
                strhtml.Append("<a href=\"http://" + http_start2 + "/oauth/qq/qqlogin.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "\"><img src=\"/oauth/qq/Connect_logo.png\" alt=\"QQ\"/>QQ帐号登录</a><br/>");
            }
            strhtml.Append("<a href=\"" + http_start + "wapreg.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\">免费注册会员</a><br/>");
            strhtml.Append("<a href=\"" + http_start + "wapgetpw.aspx?siteid=" + siteid + "" + "\">取回忘记密码</a><br/>");

            if (pd == false)
            {
                strhtml.Append("<a href=\"" + http_start + "WapRegSite.aspx?siteid=" + siteid + "" + "\">申请自助建站</a><br/>");
            }
            strhtml.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "" + "\">返回上级页面</a><br/>");

            strhtml.Append("说明:如果第一次登录本网站，请选择\"免费注册会员\"，登录方式[1]后你的信息放在URL地址中的sid=xxxxx，登录后请加入书签，下次从书签中访问可自动登录。不要将带有sid的地址告诉他人！电脑版登录请选择安全登录,sid不会显示！");
            strhtml.Append(WapTool.GetVS(wmlVo));
        }
        strhtml.Append("</p>");
        Response.Write(strhtml);

    }
    else
    {
        //显示WAP2.0
        strhtml.Append("<div class=\"subtitle\">" + this.GetLang("登录网站|登录网站|Login") + "</div>");
        if (this.INFO == "OK")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("登录成功！|登录成功！！|Login Sucess!") + "</b></div>");
            strhtml.Append("<div class=\"bt1\"><a href=\"" + this.http_start + (backurl) + "" + "\">等不及了，马上进入!</a></div>");
        }
        if (this.INFO == "IDNULL")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*用户ID/用户名/手机号码不能为空！|*用户ID/用户名/手机号码不能为空！|username not null!") + "</b></div>");
        }
        else if (this.INFO == "PASSNULL")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*密码不能为空！|*密码不能为空！|password not null!") + "</b></div>");
        }
        else if (this.INFO == "NOTEXIST")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*用户ID/用户名/手机号码不存！|*用户ID/用户名/手机号码不存！|username not exist ") + "</b></div>");
        }
        else if (this.INFO == "PASSERR")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*密码错误！|*密码错误！|Password error!") + "</b></div>");
        }
        else if (this.INFO == "USERLOCK")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*用户被锁定！|*用户被锁定！|user locked!") + "</b></div>");
        }
        else if (this.INFO == "MAXLOGIN")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>" + this.GetLang("*登录失败次数超过" + this.KL_LoginTime + "次了，请明天再来!") + "</b></div>");
        }
        else if (this.INFO == "weixin")
        {
            strhtml.Append("<div class=\"tip\">");
            if (publicID != "")
            {
                strhtml.Append("请在微信加本站公共帐号:" + publicName + " 或 " + publicID + " ，关注后自动注册成为会员，更改密码在微信上发送：密码+XXXX，查看注册帐号信息发送：帐号");
            }
            else
            {
                strhtml.Append("本站管理员还没有配置微信共帐号。");
            }
            strhtml.Append("</div>");
        }

        if (errorinfo == "config")
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("<b>如果老是进入登录界面，请联系站长在“网站默认配制”“[55].参数保存方式”选[1]</b></div>");
        }

        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代

        if (this.INFO != "OK")
        {
            strhtml.Append("<form name=\"f\" action=\"" + http_start + "waplogin.aspx\" method=\"post\">");

            strhtml.Append("<div class=\"content\">");
            strhtml.Append(this.GetLang("用户ID/用户名/手机号码|用户ID/用户名/手机号码|userid/username/mobile") + "*:<br/>");

            strhtml.Append("<input type=\"text\" name=\"logname\" class=\"txt\" value=\"" + this.logname + "\"/><br/>");

            strhtml.Append(this.GetLang("密码|密码|password") + "*:<br/>");
            strhtml.Append("<input type=\"password\" name=\"logpass\" class=\"txt\" value=\"" + this.logpass + "\"/><br/>");

            strhtml.Append("<input type=\"checkbox\" name=\"savesid\" checked=\"checked\" value=\"0\" > 下次自动登录");

            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"login\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
            strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("登 录|登 录|Login") + "\"/><br/>");

            strhtml.Append("</div>");
            strhtml.Append("</form>");

            if (this.showQQLogin == "0")
            {
                strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
                strhtml.Append("<a href=\"http://" + http_start2 + "/oauth/qq/qqlogin.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\"><img src=\"/oauth/qq/Connect_logo.png\" alt=\"QQ\" width=\"16\" height=\"16\"/> QQ帐号登录</a><br/>");
                strhtml.Append("</div></div>");
            }
            if (this.showWXLogin == "0")
            {
                strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
                strhtml.Append("<a href=\"" + http_start + "waplogin.aspx?action=weixin&amp;siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\"><img src=\"/weixin/images/logo.png\" alt=\"WeiXin\" width=\"16\" height=\"16\"/> WX微信登录</a><br/>");
                strhtml.Append("</div></div>");
            }
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a href=\"" + http_start + "wapreg.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\">免费注册会员</a> ");
            strhtml.Append("</div></div>");
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a href=\"" + http_start + "wapgetpw.aspx?siteid=" + siteid + "" + "\">取回忘记密码</a><br/>");
            strhtml.Append("</div></div>");
            if (pd == false)
            {
                strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
                if (isWebHtml == "") strhtml.Append("<a href=\"" + http_start + "WapRegSite.aspx?siteid=" + siteid + "" + "\">申请站长建站</a><br/>");
                strhtml.Append("</div></div>");
            }
            strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
            strhtml.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "" + "\">返回上级页面</a><br/>");
            strhtml.Append("</div></div>");


            //strhtml.Append("<div class=\"tip\">");
            //strhtml.Append("说明:如果第一次登录本网站，请选择\"免费注册会员\"，选了下次登录后您的信息放在URL地址中的sid=xxxxx，登录后请加入书签，下次从书签中访问可自动登录。不要将带有sid的地址告诉他人！电脑版登录请选择安全登录,sid不会显示！");
            //strhtml.Append("</div>");

            //if (isWebHtml == "") strhtml.Append(WapTool.GetVS(wmlVo));
        }

        if (isWebHtml != "")
        {
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }

        Response.Write(strhtml);
    }
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>