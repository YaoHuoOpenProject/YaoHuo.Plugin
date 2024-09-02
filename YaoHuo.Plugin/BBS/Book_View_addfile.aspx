<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View_addfile.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View_addfile" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
StringBuilder strhtml=new StringBuilder ();
Response.Write(WapTool.showTop(this.GetLang("发表文件帖|发表文件帖|add subject"), wmlVo));//显示头                                                                                                                                                                       
if (ver == "1")
{
    strhtml.Append("<p>");
    strhtml.Append(this.ERROR);
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;sid=" + this.sid1 + "-2-" + this.cs + "-" + this.lang + "-" + this.myua + "-" + this.width + "\">" + this.GetLang("进入WAP2.0界面发表文件帖|进入WAP2.0界面发表文件帖|wap2.0 add upfile") + "</a><br/>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_add.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表普通帖|发表普通帖|add content") + "</a><br/>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addvote.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表投票帖|发表投票帖|add vote") + "</a><br/>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_ubb.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view_addfile.aspx?siteid=" + this.siteid + "&classid=" + this.classid) + "\">" + this.GetLang("查看UBB方法|查看UBB方法|view UBB fuction") + "</a><br/><br/>");

    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
    strhtml.Append(WapTool.GetVS(wmlVo));
    strhtml.Append("</p>");
    Response.Write(strhtml);
}
else //2.0界面
{
    if (num > 9) num = 9;
    if (num < 1) num = 1;
    //strhtml.Append("<script type=\"text/javascript\" src=\"/css/js/bbs/tuchuang.js?Q2\"></script>");
    strhtml.Append("<link href=\"/netcss/css/save-notification.css\" rel=\"stylesheet\" type=\"text/css\"/>");
    strhtml.Append("<div class=\"title\">发表文件帖(");
    strhtml.Append(classVo.classname+"");
    strhtml.Append(")</div>");
    strhtml.Append("<div style=\"display:none\" class=\"notification-container\"><div style=\"top: 55px;\" class=\"custom-notification\"> <div class=\"custom-notification-container\"> <p class=\"custom-notification-content\">草稿保存成功!</p> </div> </div></div>");
    strhtml.Append("<div class=\"btBox\">");
    strhtml.Append("<div class=\"bt2\">");
    strhtml.Append("<a  class=\"btSelect\" href=\"" + this.http_start + "bbs/book_view_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">本地上传资源</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addURL.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;num=1\">发外站资源帖</a> ");
    strhtml.Append("</div></div>");
    strhtml.Append(this.ERROR);
    if (this.INFO == "OK")
    {
        strhtml.Append("<div class=\"content\"><b>发表成功！</b> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>审核后显示！</b> ");
        }
        strhtml.Append("获得" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + "" + getmoney + "，经验" + getexpr + "<br/> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.getid + "" + "\">查看主题</a></div>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/netcss/js/ClearDraft.js\"></script>");
    }
    else if (this.INFO == "EXTERR")
    {
        strhtml.Append("<b>上传文件格式错误，只允许上传："+siteVo.UpFileType+"</b><br/>");
    }
    else if (this.INFO == "NOTSPACE")
    {
        strhtml.Append("<b>网站总空间已经大于系统分配给此网站的最大空间了，网站空间：" + siteVo.sitespace  + "M；此网站已使用：" + (siteVo.myspace) + "KB</b><br/>");
    }
    else if (this.INFO == "MAXFILE")
    {
        strhtml.Append("<b>你上传的单个文件总大小超出了最大限制" + siteVo.MaxFileSize + "KB</b><br/>");
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<b>标题最少" + this.titlemax + "字，内容最少" + this.contentmax + "字！</b><br/>");
    }
    else if (this.INFO == "TITLEMAX")
    {
        if (title_max != "0")
        {
            strhtml.Append("<b>标题最大" + this.title_max + "字。</b><br/>");
        }
        if (content_max != "0")
        {
            strhtml.Append("<b>内容最大" + this.content_max + "字。</b><br/>");
        }
    } 
    else if (this.INFO == "ERR_FORMAT")
    {
        strhtml.Append("<b>取到非法值:“$$(”请更换手机浏览器或重新编辑！</b><br/>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<b>请不要发重复内容！</b><br/>");
    }
    else if (this.INFO == "PWERROR")
    {
        strhtml.Append("<b>密码错误，请重新录入我的密码！</b><br/>");
    }
    else if (this.INFO == "ERROR_Secret")
    {
        strhtml.Append("<b>暗号错误，如果忘记联系站长索取！</b><br/>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<b>今天你已超过发帖限制：" + this.KL_CheckBBSCount + " 个帖子了，请明天再来！</b><br/>");
    }
    else if (this.INFO == "SENDMONEY")
    {
        strhtml.Append("<b>你当前的只有:" + userVo.money + "个，所以你悬赏值只能小于或等于" + userVo.money + "个</b><br/>");
    }
    else if (this.INFO == "NOMONEY")
    {
        strhtml.Append("<b>你当前的只有:" + userVo.money + "个，发帖需要扣掉：" + getmoney2 + "个</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>抱歉，您已经被加入黑名单，请注意发帖规则！</b><br/>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    if (this.INFO != "OK")
    {
        //选多少个
        strhtml.Append("<form name=\"g1\" action=\"" + http_start + "bbs/book_view_addfile.aspx\" method=\"get\">");
        strhtml.Append(this.GetLang("上传数量|上传数量|Upload Number") + " <input type=\"number\" oninput=\"if(value.length>1)value=value.slice(0,1)\" name=\"num\" style=\"width:30px;text-align:center\" value=\"" + this.num + "\" size=\"2\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"class\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"page\" value=\"" + page + "\"/>");        
        //strhtml.Append("<input type=\"hidden\"  name=\"sid\" value=\"" + sid + "\"/>");
        strhtml.Append(" <input type=\"submit\"  name=\"bt\" value=\"" + this.GetLang("确定|确定|GO") + "\"/></form>");
        strhtml.Append("<br/>");
        //显示表情
        strhtml.Append("<form name=\"f\" action=\"" + http_start + "bbs/book_view_addfile.aspx\" enctype=\"multipart/form-data\" method=\"post\">");
        strhtml.Append("<select name=\"face\" style=\"display:none;\">");
        strhtml.Append("<option value=\"\">表情</option>");
        for (int i = 0; (facelist != null && i < this.facelist.Length); i++)
        {
        }
        strhtml.Append("</select>");
        //显示类别
        strhtml.Append("<select name=\"stype\" style=\"display:none;\">");
        strhtml.Append("<option value=\"\">类别</option>");
        for (int i = 0; (stypelist != null && i < this.stypelist.Length); i++)
        {
        }
        strhtml.Append("</select>");
        strhtml.Append("<div class='book_view_add_height'>");
        strhtml.Append(this.GetLang("标题|標題|Title") + "");
        strhtml.Append("</div>");
        strhtml.Append("<div class='centered-container'>");
        strhtml.Append("<input type=\"text\" minlength=\"5\" maxlength=\"25\" required=\"required\" name=\"book_title\" class=\"txt\" style=\"width:98.6%;\" value=\"" + book_title + "\"/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class='book_view_add_height'>");
        strhtml.Append(this.GetLang("内容|內容|Content") + "<button type=\"button\" class=\"saveDraft\" id=\"saveDraftButton\">保存草稿</button><button type=\"button\" class=\"saveDraft\" id=\"clearDraftButton\" style=\"display: none;\">清除草稿</button>");
        strhtml.Append("</div>");
        strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
        strhtml.Append("<div class='centered-container'>");
        strhtml.Append("<textarea name=\"book_content\" oninput=\"adjustTextareaHeight(this)\" minlength=\"15\" required=\"required\" rows=\"3\" class=\"KL_bbs_textarea\" style=\"width:98.6%;margin-bottom:5px;min-height:25vh;\">" + book_content + "</textarea>");
        strhtml.Append("</div>");
        if (isadmin == true)
        {
            //strhtml.Append("签到送币 <input type=\"text\" format=\"*N\" name=\"reshow\" value=\"" + reshow + "\" size=\"20\"/><br/>");
        }
        strhtml.Append("悬赏妖晶<input type=\"number\" format=\"*N\" name=\"sendmoney\" value=\"" + sendmoney + "\" style=\"margin:0 10px 0 6px;width:80px;max-width:50%\"/><br/>");
        for (int i = 0; i < this.num; i++)
        {
            strhtml.Append("----- 上传文件" + (i + 1) + " -----<br/>");
            strhtml.Append("<input style=\"border:0px;\" type=\"file\" required=\"required\" name=\"book_file\" onchange=\"fileChange(this);\" accept=\".py,.txt,.zip,.rar,.7z,.apk,.jpg,.jpeg,.png,.gif,.webp,.torrent,.mp3,.wav,.pdf,.xls,.docx\" value=\"\"/><br/>");
            strhtml.Append("<script charset=\"utf-8\" type=\"text/javascript\" src=\"/NetCSS/JS/UploadFile.js\"></script>");
            strhtml.Append(this.GetLang("文件说明|文件说明|Source") + "<br/>");
            strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<textarea name=\"book_file_info\" oninput=\"adjustTextareaHeight(this)\" rows=\"3\" style=\"width:98.6%;margin-bottom:5px;\"></textarea><br/>");
            strhtml.Append("</div>");
            if (i == (this.num - 1))
            {
                strhtml.Append("<input type=\"number\" style=\"display:none;\" name=\"book_width\" min=\"300\" style=\"width:60px;text-align:center;\" value=\"\" \"\"/>");
                //strhtml.Append(this.GetLang("图片宽度|图片宽度|Width") + " <input type=\"number\" name=\"book_width\" min=\"300\" style=\"width:60px;text-align:center;\" value=\"\" \"\"/> PX<br/>");
            }
        }
        if (this.needpwFlag == "1")
        {
            strhtml.Append("我的密码*:<input type=\"text\" name=\"needpw\" value=\"" + needpw + "\" size=\"10\" /><br/>");
        }
        if (this.isNeedSecret == true)
        {
            strhtml.Append("本版暗号*:<input type=\"text\" name=\"secret\" value=\"\" size=\"10\" /><br/>");
        }
        //strhtml.Append("<anchor><go href=\"" + http_start + "bbs/book_view_add.aspx\" method=\"post\" accept-charset=\"utf-8\">");
        strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"gomod\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
        strhtml.Append("<input type=\"hidden\" name=\"page\" value=\"" + page + "\"/>");
        strhtml.Append("<input type=\"hidden\"  name=\"num\" value=\"" + num + "\"/>");     
        //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
        strhtml.Append("<input type=\"submit\" name=\"g\" class=\"btn\" value=\"" + this.GetLang("发表新帖子|发表新帖子|submit new subject") + "\"/>");
        strhtml.Append("</form>");
    }
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"tip\">");
    strhtml.Append("提示：严禁上传色情文件、病毒文件和恶意软件；<b>附件大小上限为1MB</b>，大文件建议传至网盘。");
    strhtml.Append("</div><script type=\"text/javascript\" src=\"/netcss/js/SaveDraft.js?Z7\"></script>");
    strhtml.Append("<div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_add.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表普通帖|发表普通帖|add bbs") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_addvote.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("发表投票帖|发表投票帖|add vote") + "</a> ");
    strhtml.Append("</div>");
    strhtml.Append("<div class=\"btBox\">");
    strhtml.Append("<div class=\"bt2\">");    
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_sendmoney.aspx?action=class&amp;classid=" + this.classid + "&amp;siteid=" + this.siteid + "\">发表派币帖</a>");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_view_ubb.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view_addfile.aspx?siteid=" + this.siteid + "&classid=" + this.classid) + "\">" + this.GetLang("UBB方法|查看UBB方法|view UBB fuction") + "</a>");
    strhtml.Append("</div>");
    strhtml.Append("</div>");
    string isWebHtml = this.ShowWEB_view(this.classid); //看是存在html代码    
    if (isWebHtml != "")
    {
        //string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml,wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\">");
    strhtml.Append("<div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.page + "\">" + this.GetLang("返回列表|返回列表|Back to list") + "</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + this.siteid + "&amp;classid=0\">" + this.GetLang("返回首页|返回首页|Back to index") + "</a> ");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>