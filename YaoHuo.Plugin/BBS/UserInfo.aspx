﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="YaoHuo.Plugin.BBS.UserInfo" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang(toUserVo.nickname + "的空间|" + toUserVo.nickname + "的空间|" + toUserVo.nickname + " Zome"), wmlVo));//显示头                                                                                                                                                                       
    string TA = "TA";
    if (this.userid == touserid)
    {
        TA = "我";
    }
    else
    {
        if (toUserVo.sex.ToString() == "1")
        {
            TA = "他";
        }
        else if (toUserVo.sex.ToString() == "0")
        {
            TA = "她";
        }
    }
    strhtml.Append("<div class=\"title\">" + this.GetLang(toUserVo.nickname + "的空间|" + toUserVo.nickname + "的空间|" + toUserVo.nickname + " Zome") + "</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append(WapTool.ToWML(toUserVo.remark, wmlVo) + "<br/>");
    strhtml.Append(WapTool.GetHeadImgHTML(http_start, toUserVo.headimg) + "<br/>");
    strhtml.Append("<form name=\"f6\" action=\"" + http_start + "bbs/messagelist_add.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"text\" class=\"txt\" name=\"content\" value=\"\" style=\"width:60%;height:19px;margin:5px;\"> ");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touseridlist\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"发送私信\" />");
    strhtml.Append("</form><br/>");
    strhtml.Append("<div style=\"display:none\">");
    strhtml.Append("<form name=\"f1\" action=\"" + http_start + "card/book_view.aspx\" method=\"get\">");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"" + TA + "的名片\" />");
    strhtml.Append("</form> ");
    strhtml.Append("<form name=\"f2\" action=\"" + http_start + "bbs/FriendList.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"addfriend\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"friendtype\" value=\"0\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"加为好友\" />");
    strhtml.Append("</form> ");
    strhtml.Append("<form name=\"f3\" action=\"" + http_start + "bbs/FriendList.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"addfriend\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"friendtype\" value=\"2\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"加入追求\" />");
    strhtml.Append("</form> ");
    strhtml.Append("<form name=\"f4\" action=\"" + http_start + "bbs/FriendList.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"addfriend\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"friendtype\" value=\"1\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"加入黑单\" />");
    strhtml.Append("</form> ");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"javascript:;\" onclick=\"f2.submit();\">加为好友</a> ");
    //strhtml.Append("<a href=\"javascript:;\" onclick=\"f3.submit();\">加入追求</a> ");
    strhtml.Append("<a href=\"javascript:;\" onclick=\"f4.submit();\">加黑名单</a> ");
    strhtml.Append("</div></div>");
    strhtml.Append("<b>ID号:</b>" + toUserVo.userid + WapTool.GetOLtimePic(this.http_start, siteVo.lvlTimeImg, toUserVo.LoginTimes) + "<br/>");
    strhtml.Append("<b>昵称:</b>" + WapTool.GetColorNickName(toUserVo.idname, toUserVo.nickname, lang, ver) + "<br/>");
    strhtml.Append("<b>" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, lang) + ":</b><a href=\"" + http_start + "bbs/toMoneyInfo.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.touserid) + "\">" + toUserVo.money + "</a>[<a href=\"" + this.http_start + "bbs/tomybankmoney.aspx?siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.touserid) + "\">存取</a>");
    if (this.isAdmin == true)
    {
        strhtml.Append(".<a href=\"" + this.http_start + "bbs/banklist.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;key=" + touserid + "\">明细</a>.<a href=\"" + this.http_start + "chinabank_wap/banklist.aspx?siteid=" + this.siteid + "&amp;tositeid=" + this.siteid + "&amp;touserid=" + this.touserid + "\">RMB</a>");
    }
    strhtml.Append("]<br/>");
    strhtml.Append("<b>经验:</b><a href=\"" + http_start + "bbs/tolvlInfo.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.touserid) + "\">" + toUserVo.expr + "</a><br/>");
    strhtml.Append("<b>等级:</b>" + WapTool.GetLevl(siteVo.lvlNumer, toUserVo.expr, toUserVo.money, type) + " ");
    strhtml.Append("<b>头衔:</b>" + WapTool.GetHandle(siteVo.lvlNumer, toUserVo.expr, toUserVo.money, type) + "<br/>");
    strhtml.Append("<b>勋章</b>:" + WapTool.GetMedal(toUserVo.userid.ToString(), toUserVo.moneyname, WapTool.GetSiteDefault(siteVo.Version, 47), wmlVo) + "<br/>");
    strhtml.Append("<a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=171\">" + WapTool.GetMyID(toUserVo.idname, this.lang) + "</a>/");
    if (toUserVo.sex == 1) { strhtml.Append(this.GetLang("男|男|Male")); } else { strhtml.Append(this.GetLang("女|女|Female")); }
    strhtml.Append("/" + toUserVo.age + "岁");
    strhtml.Append("/" + toUserVo.city + "<br/>");
    strhtml.Append("<a name=\"right\"></a><b>权限:</b>" + idtype + "<br/>");
    strhtml.Append(this.AdminClass);
    if (toUserVo.isonline == "1")
    {
        strhtml.Append("<b>在线</b>");
    }
    else
    {
        strhtml.Append("<b>离线</b>");
    }
    strhtml.Append(WapTool.GetOnline(http_start, toUserVo.isonline, toUserVo.sex.ToString()));
    strhtml.Append("[<a href=\"" + http_start + "bbs/messagelist_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=0&amp;touserid=" + this.touserid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.touserid) + "\">对话</a>]<br/>");
    strhtml.Append("<b>" + TA + "的</b><a href=\"" + this.http_start + "bbs/userinfomore.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.touserid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">详细资料&gt;&gt;</a><br/>");
    strhtml.Append("<b>" + TA + "的</b><a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=0&amp;key=" + this.touserid + "&amp;type=pub\">帖子(" + toUserVo.bbsCount + ")</a>.");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + this.touserid + "\">回复(" + toUserVo.bbsReCount + ")</a>");
    //strhtml.Append("<br/><b>" + TA + "的家族:</b>" + this.showClan + "");
    //strhtml.Append("<b>" + TA + "的粉丝</b>(" + this.fans + ")<a href=\"" + this.http_start + "bbs/FriendList.aspx?action=addfriend&amp;friendtype=0&amp;siteid=" + this.siteid + "&amp;touserid=" + touserid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">喜欢</a>");
    //strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/ZoneVistList.aspx?touserid=" + touserid + "&amp;type=1&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">谁看过我</a>|<a href=\"" + this.http_start + "bbs/ZoneVistList.aspx?touserid=" + touserid + "&amp;type=0&amp;siteid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "\">我看过谁</a>");
    strhtml.Append("<br/>空间人气:" + toUserVo.zoneCount + "/今天:" + this.todayZoneCount);
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"title\">");
    strhtml.Append("<b>=" + TA + "的动态=</b><span class=\"right\"><a class=\"urlbtn\" href=\"" + http_start + "bbs/book_list_log.aspx?action=my&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + this.touserid + "" + "\">更多&gt;&gt;</a></span>");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    for (int i = 0; (this.loglistVo != null && i < loglistVo.Count); i++)
    {
        strhtml.Append(WapTool.DateToString(loglistVo[i].oper_time, lang, 1) + "前" + loglistVo[i].log_info.Replace("[sid]", this.sid) + "<br/>");
    }
    if (loglistVo == null)
    {
        strhtml.Append("(暂无动态)");
    }
    strhtml.Append("</div>");
    //strhtml.Append("<div class=\"title\">");
    //strhtml.Append("<b>=" + TA + "的相册= </b><span class=\"right\"><a class=\"urlbtn\" href=\"" + this.http_start + "album/myalbum.aspx?siteid=" + this.siteid + "&amp;classid=0&amp;touserid=" + this.touserid + "\">更多&gt;&gt;</a></span>");   
    strhtml.Append("</div>");
    //strhtml.Append("<div class=\"content\">");
    //for (int i = 0; (this.albumlistVo != null && i < albumlistVo.Count); i++)
    {
        //strhtml.Append("<a href=\"" + http_start + "album/book_view.aspx?siteid=" + siteid + "&amp;classid=" + albumlistVo[i].book_classid + "&amp;id=" + albumlistVo[i].id + "" + "\"><img src=\"" + this.http_start + "album/" + albumlistVo[i].book_img + "\"/></a> ");
        //if (i == (albumlistVo.Count - 1)) strhtml.Append("<br/>");
    }
    //if (albumlistVo == null)
    {
        //strhtml.Append("(暂无相片)<br/>");
    }
    //strhtml.Append("</div>");
    strhtml.Append("<div class=\"title\">");
    strhtml.Append("<b>=" + TA + "的留言板=</b><br/>");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    strhtml.Append("<form name=\"f5\" action=\"" + http_start + "bbs/userguessbook.aspx\" method=\"post\">");
    strhtml.Append("<input type=\"text\" class=\"txt\" name=\"content\" required=\"required\" value=\"我来踩踩，记得回踩哦[img]/bbs/face/放电.gif[/img]\" style=\"width:60%;height:19px;margin:5px;\"> ");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" value=\"我要留言\" />");
    strhtml.Append("</form><br/>");
    StringBuilder gbstr = new StringBuilder();
    for (int i = 0; (gblistVo != null && i < gblistVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            gbstr.Append("<div class=\"line1\">");
        }
        else
        {
            gbstr.Append("<div class=\"line2\">");
        }
        gbstr.Append("" + gblistVo[i].content + "</div>");
    }
    if (gblistVo == null)
    {
        strhtml.Append("（暂时木有留言，快抢沙发哦！）<br/>");
    }
    else
    {
        strhtml.Append(WapTool.ToWML(gbstr.ToString(), wmlVo));
        strhtml.Append("<div class=\"more\"><a href=\"" + this.http_start + "bbs/userguessbook.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "\">更多留言内容</a></div>");
    }
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid);
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + (backurl) + "" + "\">返回上级</a> ");
    strhtml.Append("<a href=\"" + http_start + "wapindex.aspx?siteid=" + siteid + "" + "\">返回首页</a>	");
    strhtml.Append("</div></div>");
    Response.Write(strhtml.ToString());
    Response.Write(WapTool.showDown(wmlVo));
%>