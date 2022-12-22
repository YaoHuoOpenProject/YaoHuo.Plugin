<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_My.aspx.cs" Inherits="YaoHuo.Plugin.XinZhang.Book_View_My" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop("我的勋章", wmlVo));//显示头  
    bool isexit = false;
    if (ver == "1")
    {
        strhtml.Append("<p align=\"" + classVo.position + "\">");
        //显示广告
        //if(adVo.threeShowTop!=""){
        //   strhtml.Append(adVo.threeShowTop+"<br/>");
        //}

        if (this.INFO == "OK")
        {
            strhtml.Append("<b>*操作成功!</b><br/>");
        }
        else if (this.INFO == "NOMONEY")
        {
            strhtml.Append("<b>*我的币不够啦，当前我的币有" + userVo.money + "个。</b><br/>");
        }
        else if (this.INFO == "NOPASS")
        {
            strhtml.Append("<b>*密码错误，请重新录入。</b><br/>");
        }

        if (1 == 1)
        {
            strhtml.Append("<b>我当前拥有勋章：</b><br/>");
            strhtml.Append("我的密码：<input type=\"password\" name=\"pw\" value=\"\" size=\"8\"/><br/>");

            string[] arry = userVo.moneyname.Split('|');
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i].Trim() != "")
                {
                    if (arry[i].Trim().IndexOf("XinZhang") >= 0)
                    {
                        strhtml.Append("<img src=\"" + this.http_start + arry[i] + "\" alt=\".\"/><br/>");
                    }
                    else if (arry[i].Trim().StartsWith("/") || arry[i].Trim().StartsWith("http://"))
                    {
                        strhtml.Append("<img src=\"" + arry[i].Trim() + "\" alt=\".\"/><br/>");
                    }
                    else
                    {
                        strhtml.Append("<img src=\"" + this.http_start + "bbs/medal/" + arry[i] + "\" alt=\".\"/><br/>");
                    }
                    isexit = true;
                    strhtml.Append("<anchor><go href=\"" + http_start + "xinzhang/book_view_my.aspx\" method=\"post\" accept-charset=\"utf-8\">");
                    strhtml.Append("<postfield name=\"action\" value=\"godel\" />");
                    strhtml.Append("<postfield name=\"siteid\" value=\"" + siteid + "\" />");
                    strhtml.Append("<postfield name=\"classid\" value=\"" + classid + "\" />");
                    strhtml.Append("<postfield name=\"pw\" value=\"$(pw)\" />");
                    strhtml.Append("<postfield name=\"g\" value=\"" + arry[i] + "\" />");
                    strhtml.Append("<postfield name=\"lpage\" value=\"" + (this.lpage) + "\" />");
                    strhtml.Append("<postfield name=\"ordertype\" value=\"" + (this.ordertype) + "\" />");
                    strhtml.Append("<postfield name=\"sid\" value=\"" + sid + "\" />");
                    strhtml.Append("</go>确定删除</anchor> <br/>");
                }
            }
        }
        if (isexit == false)
        {
            strhtml.Append("暂时没有我的勋章！<br/>");
        }
        strhtml.Append("<a href=\"" + this.http_start + "XinZhang/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + this.lpage + "&amp;ordertype=" + this.ordertype + "" + "\">返回上级</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        //输出错误
        strhtml.Append(ERROR);
        strhtml.Append("</p>");

        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    }
    else//2.0
    {
        //显示广告
        //if (adVo.threeShowTop != "")
        //{
        //    strhtml.Append(adVo.threeShowTop );
        //}
        strhtml.Append("<div class=\"subtitle\">管理我的勋章</div>");

        if (this.INFO == "OK")
        {
            strhtml.Append("<div class=\"tip\"><b>*操作成功！</b></div>");
        }
        else if (this.INFO == "NO")
        {
            strhtml.Append("<div class=\"tip\"><b>*操作失败！</b></div>");
        }
        else if (this.INFO == "NOPASS")
        {
            strhtml.Append("<div class=\"tip\"><b>*密码错误，请重新录入。</b></div>");
        }

        if (1 == 1)
        {
            strhtml.Append("<div class=\"content\">");

            strhtml.Append("<form name=\"f\" action=\"" + http_start + "xinzhang/book_view_my.aspx\" method=\"post\">");
            strhtml.Append("<span style='color:red'>操作存在风险，丢失无法找回！</span>");
            strhtml.Append("<br/>");
            strhtml.Append("请输入操作密码：<input type=\"password\" name=\"pw\" style=\"width: 66%; \" value=\"" + pw + "\" size=\"15\"/>");
            strhtml.Append("<br/><br/>");
            strhtml.Append("<input class=\"btn\" type=\"submit\" name=\"g\" value=\"隐藏全部\"/>");
            strhtml.Append("<input class=\"btn\" type=\"submit\" name=\"g\" value=\"显示全部\"/>");
            strhtml.Append("<br/><br/>");

            //显示的勋章
            string[] arry = userVo.moneyname.Split('|');
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i].Trim() != "")
                {
                    if (arry[i].Trim().IndexOf("XinZhang") >= 0)
                    {
                        strhtml.Append("<img src=\"" + this.http_start + arry[i] + "\" alt=\".\"/><br/>");
                    }
                    else if (arry[i].Trim().StartsWith("/") || arry[i].Trim().StartsWith("http://"))
                    {
                        strhtml.Append("<img src=\"" + arry[i].Trim() + "\" alt=\".\"/><br/>");
                    }
                    else
                    {
                        strhtml.Append("<img src=\"" + this.http_start + "bbs/medal/" + arry[i] + "\" alt=\".\"/><br/>");
                    }
                    isexit = true;
                    //strhtml.Append("<input type=\"hidden\" name=\"id\" value=\"" + arry[i] + "\" />");
                    //strhtml.Append("<input class=\"btn\" type=\"submit\" name=\"g\" value=\"删除_" + arry[i] + "\"/>");
                    strhtml.Append("<input class=\"btn\" type=\"submit\" name=\"g\" value=\"隐藏_" + arry[i] + "\"/>");
                    strhtml.Append("<br/><br/>");
                }
            }
            //隐藏的勋章
            string[] hideArry = this.HideMoneyName.Split('|');
            for (int i = 0; i < hideArry.Length; i++)
            {
                if (hideArry[i].Trim() != "")
                {
                    if (hideArry[i].Trim().IndexOf("XinZhang") >= 0)
                    {
                        strhtml.Append("<img src=\"" + this.http_start + hideArry[i] + "\" alt=\".\"/><br/>");
                    }
                    else if (hideArry[i].Trim().StartsWith("/") || hideArry[i].Trim().StartsWith("http://"))
                    {
                        strhtml.Append("<img src=\"" + hideArry[i].Trim() + "\" alt=\".\"/><br/>");
                    }
                    else
                    {
                        strhtml.Append("<img src=\"" + this.http_start + "bbs/medal/" + hideArry[i] + "\" alt=\".\"/><br/>");
                    }
                    isexit = true;
                    strhtml.Append("<input class=\"btn\" type=\"submit\" name=\"g\" value=\"显示_" + hideArry[i] + "\"/>");
                    strhtml.Append("<br/><br/>");
                }
            }

            strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" />");

            strhtml.Append("<input type=\"hidden\" name=\"lpage\" value=\"" + (this.lpage) + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"ordertype\" value=\"" + ordertype + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\" />");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"godel\" />");

            strhtml.Append("</form>");
            strhtml.Append("</div>");
        }
        if (isexit == false)
        {
            strhtml.Append("<div class=\"tip\">暂时没有我的勋章！</div>");
        }
        string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码   
        strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a href=\"" + this.http_start + "XinZhang/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + this.lpage + "&amp;ordertype=" + this.ordertype + "" + "\">返回上级</a> ");

        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");

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
        //输出错误
        strhtml.Append(ERROR);
        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    }
    Response.Write(WapTool.showDown(wmlVo));
%>
