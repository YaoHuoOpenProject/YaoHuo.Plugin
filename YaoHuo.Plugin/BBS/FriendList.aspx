<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendList.aspx.cs" Inherits="YaoHuo.Plugin.BBS.FriendList" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    string msgbox = "";
    if (this.friendtype == "0")
    {
        msgbox = "我的好友|我的好友|my friend";
    }
    else if (this.friendtype == "1")
    {
        msgbox = "我的黑名单|我的黑名单|black";
    }
    else if (this.friendtype == "2")
    {
        msgbox = "我的追求|我的追求|my love";
    }
    else if (this.friendtype == "4")
    {
        msgbox = "追求我的人|追求我的人|who love me";
    }
    else if (this.friendtype == "5")
    {
        msgbox = "我推荐的人|我推荐的人|我推荐的人";
    }
    StringBuilder strhtml = new StringBuilder();
    Response.Write(WapTool.showTop(this.GetLang(msgbox), wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p align=\"left\">");

        if (this.INFO == "OK")
        {
            strhtml.Append("<b>" + this.GetLang("操作成功！||") + "</b><br/>");
        }
        else if (this.INFO == "MY")
        {
            strhtml.Append("<b>" + this.GetLang("不能加自己！||") + "</b><br/>");
        }
        else if (this.INFO == "NOTUSER")
        {
            strhtml.Append("<b>" + this.GetLang("对方不存！||") + "</b><br/>");
        }
        else if (this.INFO == "HASEXIST")
        {
            strhtml.Append("<b>" + this.GetLang("已经存在了！||") + "</b><br/>");
        }
        else if (this.INFO == "MAX")
        {
            strhtml.Append("<b>抱歉，一天只能加" + KL_ADDFriendCount + "个好友，请明天再来！</b><br/>");
        }
        else if (this.INFO == "LOCK")
        {
            strhtml.Append("<b>" + this.GetLang("抱歉，您已经被加入黑名单，请注意发贴规则！||") + "</b><br/>");
        }
        else if (this.INFO == "NOTBLACK")
        {
            strhtml.Append("<b>" + this.GetLang("他这么帅，怎么能拉取黑名单呢！||") + "</b><br/>");
        }
        else if (this.INFO == "UPMAX")
        {
            strhtml.Append("<b>" + this.GetLang("已经达到拉取上限了！||") + "</b><br/>");
        }
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">[返回源来页]</a><br/>");

        strhtml.Append("输入会员ID：<input type=\"text\" name=\"key\" value=\"" + key + "\" size=\"8\"/>");
        strhtml.Append("<anchor><go href=\"" + http_start + "bbs/friendlist.aspx\" method=\"get\" accept-charset=\"utf-8\">");
        strhtml.Append("<postfield name=\"action\" value=\"class\" />");
        strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<postfield name=\"key\" value=\"$(key)\" />");
        strhtml.Append("<postfield name=\"backurl\" value=\"" + (backurl) + "\" />");
        strhtml.Append("<postfield name=\"friendtype\" value=\"" + (this.friendtype) + "\" />");
        strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\" />");
        strhtml.Append("</go>" + this.GetLang("搜索|搜索|Search") + "</anchor><br/>");
        if (this.friendtype == "1")
        {
            strhtml.Append("<b>以下用户将不能给你发站内信息！</b><br/>");
        }
        strhtml.Append("---------<br/>");
        strhtml.Append(linkTOP);
        //显示列表
        string YesOrNo = "";
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            index = index + kk;
            strhtml.Append(index + ".");

            if (this.friendtype == "4")
            {

                strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + WapTool.GetNickNameFromID(this.siteid, listVo[i].userid.ToString()) + "</a>");
                strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");
            }
            else
            {
                strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + listVo[i].friendnickname + "</a>");
                strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");

                strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>]");
                strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_mod.aspx?action=&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">备注</a>]");
                if (listVo[i].friendusername != "")
                {
                    strhtml.Append("<br/>备注:" + listVo[i].friendusername + "");
                }
            }

            strhtml.Append("<br/>(" + listVo[i].addtime + ")<br/>");
        }
        if (listVo == null)
        {
            strhtml.Append("没有" + this.GetLang(msgbox) + "<br/>");
        }
        //显示导航分页
        strhtml.Append(linkURL);

        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a><br/>");
        strhtml.Append("<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空" + this.GetLang(msgbox) + "</a><br/>");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        strhtml.Append("</p>");

        //输出
        Response.Write(strhtml);

    }
    else //2.0界面
    {
        strhtml.Append("<div class=\"subtitle\">" + this.GetLang(msgbox) + "</div>");
        if (this.INFO == "OK")
        {
            strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("操作成功！||") + "</b></div>");
        }
        else if (this.INFO == "MY")
        {
            strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("不能加自己！||") + "</b></div>");
        }
        else if (this.INFO == "NOTUSER")
        {
            strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("对方不存！||") + "</b></div>");
        }
        else if (this.INFO == "HASEXIST")
        {
            strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("已经存在了！||") + "</b></div>");
        }
        else if (this.INFO == "MAX")
        {
            strhtml.Append("<div class=\"tip\"><b>抱歉，一天只能加" + KL_ADDFriendCount + "个好友，请明天再来！</b></div>");
        }
        else if (this.INFO == "LOCK")
        {
            strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("抱歉，您已经被加入黑名单，请注意发贴规则！||") + "</b></div>");
        }
        else if (this.INFO == "NOTBLACK")
        {
            strhtml.Append("<b>" + this.GetLang("他这么帅，怎么能拉取黑名单呢！||") + "</b><br/>");
        }
        else if (this.INFO == "UPMAX")
        {
            strhtml.Append("<b>" + this.GetLang("已经达到拉取上限了！||") + "</b><br/>");
        }

        strhtml.Append("<div class=\"content\">");
        //strhtml.Append("<a class=\"urlbtn\" href=\"" + this.http_start + (backurl) + "" + "\">[返回源来页]</a><br/>");

        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/friendlist.aspx\" method=\"post\">");
        strhtml.Append("输入会员ID：<input type=\"text\" name=\"key\" value=\"" + key + "\" size=\"8\"/>");

        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"friendtype\" value=\"" + (this.friendtype) + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
        strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("搜索|搜索|Search") + "\"/>");
        if (this.friendtype == "1")
        {
            strhtml.Append("<br/><b>以下用户将不能给你发站内信息！</b><br/>");
        }
        strhtml.Append("</form></div>");
        strhtml.Append(linkTOP);
        //显示列表
        string YesOrNo = "";
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            index = index + kk;
            if (i % 2 == 0)
            {
                strhtml.Append("<div class=\"line1\">");
            }
            else
            {
                strhtml.Append("<div class=\"line2\">");
            }
            strhtml.Append(index + ".");

            if (this.friendtype == "4")
            {

                strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + WapTool.GetNickNameFromID(this.siteid, listVo[i].userid.ToString()) + "</a>");
                strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");


            }
            else
            {
                strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + listVo[i].friendnickname + "</a>");
                strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");

                strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>]");
                strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_mod.aspx?action=&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">备注</a>]");
                if (listVo[i].friendusername != "")
                {
                    strhtml.Append("<br/>备注:" + listVo[i].friendusername + "");

                }
            }

            strhtml.Append("<br/>(" + listVo[i].addtime + ")</div>");
        }
        if (listVo == null)
        {
            strhtml.Append("<div class=\"tip\">没有" + this.GetLang(msgbox) + "</div>");
        }
        //显示导航分页
        strhtml.Append(linkURL);

        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        if (isWebHtml != "")
        {
            //string strhtml_list = strhtml.ToString();
            //int s = strhtml_list.IndexOf("<div class=\"title\">");
            //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);

            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
            Response.End();
        }
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回源来页</a> ");
        if (this.friendtype != "4")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空" + this.GetLang(msgbox) + "</a> ");
        }
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");

        strhtml.Append("</div></div>");
        Response.Write(strhtml);
    }

    Response.Write(ERROR);
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>
