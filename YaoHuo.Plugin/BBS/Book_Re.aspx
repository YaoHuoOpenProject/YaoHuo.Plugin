<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_Re.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_Re" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    if (this.INFO == "OK")
    {
        if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 1)))
        {
            wmlVo.timer = "3";
            wmlVo.strUrl = "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage;
        }
        else
        {
            wmlVo.timer = "3";
            wmlVo.strUrl = "bbs/book_re.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage;
        }
    }
    Response.Write(WapTool.showTop(this.GetLang("查看回复|查看回複|View Reply"), wmlVo));
    string isWebHtml = this.ShowWEB_view(this.classid);
    strhtml.Append("<!--web-->");
    strhtml.Append(ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>回复成功！</b> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>审核后显示！</b> ");
        }
        strhtml.Append("获得" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + ":" + allMoney + "，获得经验:" + getexpr + "<br/> ");
        strhtml.Append("跳转中...<a href=\"" + this.http_start + wmlVo.strUrl + "" + "\">返回</a><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>回复内容最少" + contentmax + "字！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "TITLEMAX")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>内容最大" + this.content_max + "字。</b></div>");
    }
    else if (this.INFO == "ERR_FORMAT")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>取到非法值:“$$(”请更换手机浏览器或重新编辑！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>请不要发重复内容！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "ERROR_Secret")
    {
        strhtml.Append("<div class=\"tip\"><b>暗号错误，如果忘记联系站长索取！</b><br/></div>");
    }
    else if (this.INFO == "WAITING")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>请再过" + this.KL_CheckIPTime + "秒后操作！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>今天你已超过回帖限制：" + this.KL_CheckBBSreCount + " 个回帖了，请明天再来！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
        strhtml.Append("</div>");
    }
    else if (this.INFO == "NOMONEY")
    {
        strhtml.Append("<div class=\"tip\"><b>你当前的只有:" + userVo.money + "个，发帖需要扣掉：" + getmoney2 + "个</b></div>");
    }
    if (this.INFO == "")
    {
        //顶部链接
        strhtml.Append("<div class=\"btBox\"><div class=\"bt3\">");
        if (this.ot == "1")
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=0&amp;go=" + this.r + "\">最新回复</a> ");
        }
        else
        {
            strhtml.Append("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=1&amp;go=" + this.r + "\">最早回复</a> ");
        }
        strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=&amp;mainuserid=" + bookVo.book_pub + "&amp;go=" + this.r + "\">楼主回复</a>");
        //strhtml.Append("<a href=\"" + this.http_start + "bbs/book_re.aspx?action=class&amp;id=" + this.id + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=" + this.ot + "&amp;go=" + this.r + "\">刷新</a> ");
        strhtml.Append("</div>");
        strhtml.Append("</div>");
        //strhtml.Append("<div class=\"subtitle\">" + this.GetLang("查看回复|查看回複|View Reply") + "</div>");
        strhtml.Append("<div class=\"content\">");
        if (bookVo.islock == 0)
        {
            strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_re.aspx\" method=\"post\">");

            if (this.reply != "")
            {
                strhtml.Append("<b>回复" + this.reply + "楼 </b>");
                strhtml.Append("<select name=\"sendmsg2\">");
                strhtml.Append("<option value=\"1\">通知对方</option>");
                strhtml.Append("<option value=\"0\">不予通知</option>");
                strhtml.Append("</select><br/>");
            }
            //显示输入框
            strhtml.Append("<select name=\"face\">");
            strhtml.Append("<option value=\"\">表情</option>");
            for (int i = 0; (facelistImg != null && i < this.facelistImg.Length); i++)
            {
                strhtml.Append("<option value=\"" + this.facelistImg[i] + "\">" + this.facelist[i] + "</option>");
            }
            strhtml.Append("</select>");
            strhtml.Append("<select name=\"sendmsg\">");
            strhtml.Append("<option value=\"\">通知楼主？</option>");
            strhtml.Append("<option value=\"0\">否</option>");
            strhtml.Append("<option value=\"1\">是</option>");
            strhtml.Append("</select><br/>");
            //strhtml.Append("<input type=\"text\" name=\"content\" value=\"" + this.reShowInfo + "\" maxlength=\"200\"/><br/>");
            strhtml.Append("<textarea class=\"retextarea\" name=\"content\" minlength=\"1\" required=\"required\" placeholder=\"请不要乱打字回复，以免被加黑。\" rows=\"5\" class=\"KL_textarea\" style=\"width:97%\">" + this.reShowInfo + "</textarea><br/>");
            if (this.isNeedSecret == true)
            {
                strhtml.Append("本版暗号*:<input type=\"text\" name=\"secret\" value=\"\" size=\"10\" /><br/>");
            }
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + lpage + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"reply\" value=\"" + reply + "\"/>");
            strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + this.GetRequestValue("touserid") + "\"/>");
            //strhtml.Append("<input type=\"hidden\" name=\"backurl\" value=\"" + HttpUtility.UrlEncode("bbs/book_re.aspx?action=class&siteid=" + siteid + "&classid=" + classid + "&id=" + id) + "\"/>");
            strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"发表回复\"/>");
            strhtml.Append(" <a href=\"" + this.http_start + "bbs/book_re_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\" style=\"font-size:14px;\">" + this.GetLang("文件回帖|文件回帖|upload file") + "</a>");
            strhtml.Append("</form>");
        }
        strhtml.Append("</div>");
        strhtml.Append("<!--listS-->");
        kk = kk + ((CurrentPage - 1) * pageSize) - 1;
        for (int i = 0; (listVo != null && i < listVo.Count); i++)
        {
            if (i % 2 == 0)
            {
                strhtml.Append("<div class=\"list-reply line1\">");
            }
            else
            {
                strhtml.Append("<div class=\"list-reply line2\">");
            }
            if (ot == "1")
            {
                index = (kk + 1);
            }
            else
            {
                index = (total - kk);
            }
            if (listVo[i].book_top == 1 && this.CurrentPage == 1)
            {
                strhtml.Append("[顶楼]");
            }
            else
            {
                if (index == 1)
                {
                    strhtml.Append("[沙发]");
                }
                else if (index == 2)
                {
                    strhtml.Append("[椅子]");
                }
                else if (index == 3)
                {
                    strhtml.Append("[板凳]");
                }
                else
                {
                    strhtml.Append("[" + index + "楼]");
                }
            }
            if (this.userid == bookVo.book_pub && bookVo.sendMoney > 0 && listVo[i].myGetMoney == 0)
            {
                strhtml.Append("[<a href=\"" + this.http_start + "bbs/SendMoney.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;reid=" + listVo[i].id + "&amp;siteid=" + this.siteid + "\">赏分</a>]");
            }
            if (listVo[i].myGetMoney > 0)
            {
                strhtml.Append("[<b>得金:" + listVo[i].myGetMoney + "</b>]");
            }
            // 管理员操作
            if (this.IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername))
            {
                strhtml.Append("[<a href=\"" + this.http_start + "bbs/SendMoney_free.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;reid=" + listVo[i].id + "&amp;touserid=" + listVo[i].userid + "&amp;siteid=" + this.siteid + "\">送</a>]");

                strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;id=" + this.id + "&amp;ot=" + this.ot + "\">删</a>]");
                strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re_mod.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;id=" + this.id + "&amp;ot=" + this.ot + "\">审</a>]");
                if (listVo[i].book_top == 1)
                {
                    strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re_top.aspx?action=go&amp;tops=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;id=" + this.id + "&amp;ot=" + this.ot + "\">消顶</a>]");
                }
                else
                {
                    strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re_top.aspx?action=go&amp;tops=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;id=" + this.id + "&amp;ot=" + this.ot + "\">顶</a>]");
                }
            }
            else if (this.userid == listVo[i].userid.ToString())  // 删除自己的回复
            {
                strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;id=" + this.id + "&amp;ot=" + this.ot + "\">删</a>]");
            }
            strhtml.Append("[<a href=\"" + this.http_start + "bbs/Book_re.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reply=" + index + "&amp;id=" + this.id + "&amp;touserid=" + listVo[i].userid + "&amp;ot=" + this.ot + "\">回</a>]");
            if (listVo[i].reply != 0)
            {
                strhtml.Append("回复" + listVo[i].reply + "楼：");
            }
            strhtml.Append(listVo[i].content);
            if (listVo[i].isdown > 0)
            {
                strhtml.Append("{<a href=\"" + this.http_start + "bbs/book_re_addfileshow.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;reid=" + listVo[i].id + "&amp;lpage=" + this.lpage + "\">查看" + listVo[i].isdown + "个附件</a>}");
            }
            strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/userinfo.aspx?touserid=" + listVo[i].userid + "\">" + ShowNickName_color(listVo[i].userid, listVo[i].nickname) + "(" + listVo[i].userid + ")</a> " + string.Format("{0:MM-dd HH:mm}", listVo[i].redate) + "</div>");
            kk = kk + 1;
        }
        if (listVo == null)
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("暂无回复记录！");
            strhtml.Append("</div>");
        }
        strhtml.Append("<!--listE-->");
        strhtml.Append(linkURL);
    }
    //显示电脑版
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        int s = strhtml_list.IndexOf("<!--web-->");
        strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs-" + id + ".html\">返回主题</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + "" + "\">返回列表</a> ");
    strhtml.Append("</div></div>");
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(WapTool.showDown(wmlVo));
%>
