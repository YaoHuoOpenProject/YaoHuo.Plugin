<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendList.aspx.cs" Inherits="YaoHuo.Plugin.BBS.FriendList" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    string msgbox = "";
    if (this.friendtype == "0")
    {
        msgbox = "好友|我的好友|my friend";
    }
    else if (this.friendtype == "1")
    {
        msgbox = "黑名单|我的黑名单|black";
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
    Response.Write(WapTool.showTop(this.GetLang(msgbox), wmlVo));
    strhtml.Append("<div class=\"title\"><a href=\"/\">首页</a>><a href=\"/myfile.aspx\">我的地盘</a>>" + this.GetLang(msgbox) + "</div>");
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
        strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("禁止拉黑管理员，请不要调皮哦！||") + "</b></div>");
    }
    else if (this.INFO == "UPMAX")
    {
        strhtml.Append("<div class=\"tip\"><b>" + this.GetLang("拉黑失败，已达到当前用户等级黑名单上限||") + "</b></div>");
    }
    if (this.friendtype == "0")
    {
        strhtml.Append("<div class=\"content\">");
        //strhtml.Append("<a class=\"urlbtn\" href=\"" + this.http_start + (backurl) + "" + "\">[返回来源页]</a><br/>");
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/friendlist.aspx\" method=\"post\">");
        strhtml.Append("<input type=\"number\" style=\"width:50%;max-width: 200px;height:19px;\" required=\"required\" name=\"key\" value=\"" + key + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\" />");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + (backurl) + "\" />");
        strhtml.Append("<input type=\"hidden\" name=\"friendtype\" value=\"" + (this.friendtype) + "\" />");
        strhtml.Append("<input type=\"submit\" name=\"g\" value=\"" + this.GetLang("搜索ID号|搜索|Search") + "\"/>");
        strhtml.Append("</form></div>");
    }
    //strhtml.Append(linkTOP);
    //显示列表
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        //index = index + kk;
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line2\">");
        }
        else
        {
            strhtml.Append("<div class=\"line1\">");
        }
        //strhtml.Append(index + ".");
        if (this.friendtype == "4")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + WapTool.GetNickNameFromID(this.siteid, listVo[i].userid.ToString()) + "</a>");
            strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");
        }
        if (this.friendtype == "0")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + listVo[i].friendnickname + "</a>");
            //strhtml.Append(" [<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&friendtype=" + this.friendtype) + "" + "\">发信</a>]");
            strhtml.Append(" [<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>]");
            strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_mod.aspx?action=&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">备注</a>]");
            if (listVo[i].friendusername != "")
            {
                strhtml.Append("<br/>备注:" + listVo[i].friendusername + "");
            }
        }
        if (this.friendtype == "1")
        {
            strhtml.Append("<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + listVo[i].frienduserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/friendlist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + (key)) + "" + "\">" + listVo[i].friendnickname + "</a>");
            strhtml.Append(" [<a href=\"" + http_start + "bbs/friendlist_del.aspx?action=del&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">删除</a>]");
            //strhtml.Append("[<a href=\"" + http_start + "bbs/friendlist_mod.aspx?action=&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + listVo[i].id + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">备注</a>]");
            if (listVo[i].friendusername != "")
            {
                strhtml.Append("<br/>备注:" + listVo[i].friendusername + "");
            }
        }
        //strhtml.Append("<br/>(" + listVo[i].addtime + ")</div>");
        strhtml.Append("<br/><div style=\"opacity: 0.65;\">(" + listVo[i].addtime + ")</div></div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"line1\"><br/>当前" + this.GetLang(msgbox) + "列表为空</div><br/>");
    }
    if (this.friendtype == "1")
    {
        strhtml.Append("<div class=\"tip\">禁止列表用户给你发私信，并屏蔽对方帖子</div>");
    }
    //显示导航分页
    strhtml.Append(linkURL);
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
    if (this.friendtype != "4")
    {
        strhtml.Append("<a class=\"noafter\" href=\"" + http_start + "bbs/friendlist_del.aspx?action=delall&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;friendtype=" + this.friendtype + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;page=" + this.CurrentPage + "" + "\">清空" + this.GetLang(msgbox) + "</a> ");
    }
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
    Response.Write(ERROR);
    Response.Write(WapTool.showDown(wmlVo));
%>