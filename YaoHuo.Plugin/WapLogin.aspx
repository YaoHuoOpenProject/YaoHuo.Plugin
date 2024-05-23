<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WapLogin.aspx.cs" Inherits="YaoHuo.Plugin.WapLogin" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {
        wmlVo.timer = "0"; //5秒后自动跳转
        wmlVo.strUrl = backurl;
    }
    StringBuilder strhtml = new StringBuilder();
    //显示头
    if (ver == "1")
    {
        //显示WAP1.0
        Response.Redirect("/waplogin.aspx?sid=-2");
    }
    else
    {
        //显示WAP2.0
        strhtml.Append("<!DOCTYPE html><html>");
        strhtml.Append("<head>");
        strhtml.Append("<meta charset='utf-8'>");
        strhtml.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>");
        strhtml.Append("<meta name='keywords' content='妖火,妖火网,妖火论坛'/>");
        strhtml.Append("<meta name='viewport' content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'>");
        strhtml.Append("<title>登录 - 妖火网</title>");
        strhtml.Append("<meta http-equiv='Content-type' content='text/html;charset=UTF-8'>");
        strhtml.Append("<link rel='stylesheet' href='/CSS/img/login/000.css'>");
        strhtml.Append("<script src='/CSS/img/login/bundle-home.js'></script>");
        strhtml.Append("<script src='/css/img/login/zero.js'></script>");
        //底部引入资源文件
        if (!string.IsNullOrEmpty(this.RecaptchaV2_Key))
        {
            //strhtml.Append("<script src='https://www.google.com/recaptcha/api.js'></script>");
            strhtml.Append("<script src='https://recaptcha.google.cn/recaptcha/api.js'></script>");
        }
        strhtml.Append("</head>");
        if (this.INFO == "OK")
        {
            strhtml.Append("<meta http-equiv='refresh' content='0;url=wapindex.aspx?sid=-2'><div class='wrap'><div style='text-align: center; color: #FFFFFF; font-weight: 500; padding-bottom: 10px; letter-spacing: 2px; margin-top: 120px;'><h1>登录成功</h1></div></div>");
        }
        if (this.INFO == "IDNULL")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("请输入账号|请输入账号|username not null!") + "</div>");
        }
        else if (this.INFO == "PASSNULL")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("请输入密码|请输入密码|password not null!") + "</div>");
        }
        else if (this.INFO == "NOTEXIST")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("请确认账号正确|请确认账号正确|username not exist ") + "</div>");
        }
        else if (this.INFO == "PASSERR")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("请确认密码正确|请确认密码正确|Password error!") + "</div>");
        }
        else if (this.INFO == "USERLOCK")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("用户被锁定|用户被锁定|user locked!") + "</div>");
        }
        else if (this.INFO == "MAXLOGIN")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("登录失败超过 " + this.KL_LoginTime + " 次了，请明天再来") + "</div>");
        }
        else if (this.INFO == "NOTGOOGLERECAPTCHA")
        {
            strhtml.Append("<div class='tip'>");
            strhtml.Append("" + this.GetLang("人机验证失败，请重试") + "</div>");
        }
        else if (this.INFO == "weixin")
        {
            strhtml.Append("<div class='tip'>");
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
            strhtml.Append("<div class='tip'>");
            strhtml.Append("<b>如果总是进入登录界面，请联系站长在“网站默认配制”“[55].参数保存方式”选[1]</b></div>");
        }
        //看是存在html代
        string isWebHtml = this.ShowWEB_view(this.classid);
        if (this.INFO != "OK")
        {
            strhtml.Append("<body id='login' class='unloaded'><div class='wrapper'><div class='zero'></div><div class='login'> <form action='/waplogin.aspx' method='post' name='login' class='container offset1 loginform'> <div id='owl-login'> <div class='eyes'></div> <div class='arm-up-right'></div> <div class='arm-up-left'></div> <div class='arm-down-left'></div> <div class='arm-down-right'></div> </div><div class='pad'> <div class='control-group'> <div class='controls'> <label for='email' class='control-label fa fa-envelope'></label> <input type='text' name='logname' id='logname' placeholder='手机或ID号' tabindex='1' autofocus='autofocus' required class='form-control input-medium' value='" + this.logname + "'/> </div> </div> <div class='control-group'> <div class='controls'> <label for='password' class='control-label fa fa-asterisk'> </label> <input id='password' type='password' name='logpass' placeholder='请输入密码' tabindex='2' required class='form-control input-medium' value='" + this.logpass + "'/> <input type='hidden' name='action' value='login'><input type='hidden' name='classid' value='0'><input type='hidden' name='siteid' value='1000'><input type='hidden' name='backurl' value='" + backurl + "'/><input type='hidden' name='savesid' value='0' ></div></div>");
            //谷歌人机验证配置
            if (!string.IsNullOrEmpty(this.RecaptchaV2_Key))
            {
                strhtml.Append("<div style='margin-bottom: -20px;' class='g-recaptcha' data-sitekey='" + this.RecaptchaV2_Key + "'></div>");
            }
            strhtml.Append("</div><div class='form-actions'> <a tabindex='5' href='/bbs-138352.html' class='btn pull-left btn-link text-muted'>注册</a> <button type='submit' tabindex='4' class='btn btn-primary'>登录</button> </div></form></div></div></html>");
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
