<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyHead.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ModifyHead" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(this.GetLang("修改我的头像|修改我的头像|Change My Avatar"), wmlVo));//显示头 
    StringBuilder strhtml = new StringBuilder();
 if (ver == "1")
   {
       strhtml.Append("<p>");
       if (ERROR != "")
       {
           strhtml.Append(ERROR);
           strhtml.Append("<br/>");
       }
       if (INFO == "OK")
       {
           strhtml.Append("<b>更新成功！</b>");
           strhtml.Append("<br/>");
       }
       else if (INFO == "NULL")
       {
           strhtml.Append("<b>不能为空！</b>");
           strhtml.Append("<br/>");
       }
       strhtml.Append("我现在的头像：<br/>");
       strhtml.Append(WapTool.GetHeadImgHTML(http_start, toheadimg) + "<br/>");
       strhtml.Append("<b>A.选择系统头像：</b><br/>");
       if (sysimg == "")
       {
           sysimg = "1";
       }
       strhtml.Append("<img src=\"" + this.http_start + "bbs/head/" + sysimg + ".gif\" alt=\"头像\" /><br/>");
       strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=1&amp;siteid=" + siteid + "" + "\">男</a> ");
       strhtml.Append("&nbsp;&nbsp;");
       if (Convert.ToInt64(sysimg) > 1 && Convert.ToInt64(sysimg) < 64)
       {
           strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=" + Convert.ToString(Convert.ToInt64(sysimg) - 1) + "&amp;siteid=" + siteid + "" + "\">上一张</a> &nbsp;&nbsp;");
       }
       if (Convert.ToInt64(sysimg) < 63)
       {
           strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=" + Convert.ToString(Convert.ToInt64(sysimg) + 1) + "&amp;siteid=" + siteid + "" + "\">下一张</a> &nbsp;&nbsp;");
       }
       strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=63&amp;siteid=" + siteid + "" + "\">女</a> <br />");
       strhtml.Append("<b>B.自定义照片地址：</b><br/>");
       strhtml.Append(this.http_start + "<input type=\"text\" name=\"toheadimg" + r + "\" /><br/>");
       strhtml.Append("(方法一:论坛上传相片例输入:bbs/upload/123.gif)<br/>");
       strhtml.Append("(方法二:相册上传后，直接设为头像)<br/>");
       strhtml.Append("<anchor><go href=\"" + http_start + "bbs/ModifyHead.aspx\" method=\"post\" accept-charset=\"utf-8\">");
       strhtml.Append("<postfield  name=\"action\"  value=\"gomod\" />");
       strhtml.Append("<postfield  name=\"siteid\" value=\"" + this.siteid + "\"  />");
       strhtml.Append("<postfield  name=\"classid\"  value=\"" + this.classid + "\"  />");
       strhtml.Append("<postfield  name=\"sid\"  value=\"" + this.sid + "\"  />");
       strhtml.Append("<postfield  name=\"sysimg\"  value=\"" + this.sysimg + "\"  />");
       strhtml.Append("<postfield name=\"toheadimg\" value=\"$(toheadimg" + r + ")\"/>");
       strhtml.Append("</go>" + this.GetLang("修改|修改|Update") + "</anchor><br/>");
       strhtml.Append("<br />");
       strhtml.Append("注意：优先级最高为B(7个字符以上)。<br/>");
       strhtml.Append("如果要选A中头像要保证B中的输入框是空的！<br/>");
       strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + this.siteid + "\">" + this.GetLang("返回上级|返回上级|Back to set") + "</a> ");
       strhtml.Append(WapTool.GetVS(wmlVo));
       strhtml.Append("</p>");
       Response.Write(strhtml);
   }
   else
   {
       strhtml.Append("<div class=\"subtitle\">我的头像</div>");
       if (ERROR != "")
       {
           strhtml.Append("<div class=\"tip\">");
           strhtml.Append(ERROR);
           strhtml.Append("</div>");
       }
       if (INFO == "OK")
       {
           strhtml.Append("<div class=\"tip\">");
           strhtml.Append("<b>更新成功！</b>");
           strhtml.Append("</div>");
       }
       else if (INFO == "NULL")
       {
           strhtml.Append("<div class=\"tip\">");
           strhtml.Append("<b>不能为空！</b>");
           strhtml.Append("</div>");
       }  
       strhtml.Append("<div class=\"content\">");
       strhtml.Append("<form name =\"f\" action=\"" + http_start + "bbs/ModifyHead.aspx\" method=\"post\">");
       //strhtml.Append("现在的头像:<br/>");
       strhtml.Append(WapTool.GetHeadImgHTML(http_start, toheadimg) + "<br/>");
       strhtml.Append("<div class=\"linkbtn\">");
       strhtml.Append("<br/><b>A.选择系统头像</b><br/>");
       if (sysimg == "")
       {
           sysimg = "1";
       }
       strhtml.Append("<img src=\"" + this.http_start + "bbs/head/" + sysimg + ".gif\" alt=\"头像\" /><br/>");
       strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=1&amp;siteid=" + siteid + "" + "\">男</a> ");
       strhtml.Append("&nbsp;&nbsp;");
       if (Convert.ToInt64(sysimg) > 1 && Convert.ToInt64(sysimg) < 64)
       {
           strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=" + Convert.ToString(Convert.ToInt64(sysimg) - 1) + "&amp;siteid=" + siteid + "" + "\">上一张</a> &nbsp;&nbsp;");
       }
       if (Convert.ToInt64(sysimg) < 63)
       {
           strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=" + Convert.ToString(Convert.ToInt64(sysimg) + 1) + "&amp;siteid=" + siteid + "" + "\">下一张</a> &nbsp;&nbsp;");
       }
       strhtml.Append("<a href=\"" + this.http_start + "bbs/modifyhead.aspx?sysimg=63&amp;siteid=" + siteid + "" + "\">女</a> <br />");
       strhtml.Append("</div>");
       strhtml.Append("<br/><b>B.自定义站内图片地址</b><br/>");
       strhtml.Append(this.http_start + "<input type=\"text\" style=\"width:50%\" name=\"toheadimg\" /><br/>");
       strhtml.Append("（示例：bbs/face/放电.gif）<br/>");
       strhtml.Append("<input name=\"action\"  type=\"hidden\" value=\"gomod\" />");
       strhtml.Append("<input name=\"siteid\"  type=\"hidden\" value=\"" + this.siteid + "\"  />");
       strhtml.Append("<input name=\"classid\"  type=\"hidden\" value=\"" + this.classid + "\"  />");
       strhtml.Append("<input name=\"sysimg\"  type=\"hidden\" value=\"" + this.sysimg + "\"  />");
       strhtml.Append("<input name=\"sid\"  type=\"hidden\" value=\"" + this.sid + "\"  />");
       strhtml.Append("<input type=\"submit\" id=\"submit\" class=\"btn\" name=\"submit\" value=\"修 改\" /><br/>");
       strhtml.Append("</form>");
       //strhtml.Append("<br/>");
       //strhtml.Append("注意：优先级最高为B(7个字符以上)。<br/>");
       //strhtml.Append("如果要选A中头像要保证B中的输入框是空的！<br/>");
       strhtml.Append("<br/><b>C.相册上传后，直接设为头像</b><br/>");
       strhtml.Append("[<a href=\"" + this.http_start + "album/admin_WAPadd.aspx\">点此上传相片</a>]");
       strhtml.Append("</div><br/>");
       string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码  
       strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
       strhtml.Append("<a href=\"javascript:history.go(-1)\">返回上级</a>");
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
 Response.Write(WapTool.showDown(wmlVo)); //显示底部
 %>