<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userGuessBook.aspx.cs" Inherits="YaoHuo.Plugin.BBS.userGuessBook" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("���԰�|���԰�|GuessBook"), wmlVo));//��ʾͷ                                                                                                                                                                       
if (ver == "1")
{
    //���
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
}
else //2.0����
{
    //��Ա�ɼ�
 	if (this.IsCheckManagerLvl("|00|01|02|03|04|","")==true)
    {
    strhtml.Append("<script type=\"text/javascript\" defer src=\"/NetCSS/JS/HyperLink.js\"></script>");
    strhtml.Append("<div class=\"title\">" + this.GetLang("���԰�|���԰�|GuessBook") + "</div>");
    strhtml.Append(ERROR);
    if (this.INFO != "") strhtml.Append("<div class=\"tip\">");
    if (this.INFO == "OK")
    {
        strhtml.Append("<b>���Գɹ���</b><br/> ");
        if (siteVo.isCheck == 1)
        {
            strhtml.Append("<b>��˺���ʾ��</b> <br/>");
        }
    }
    else if (this.INFO == "NULL")
    {
        strhtml.Append("<b>�������ݲ���Ϊ�գ�</b><br/>");
    }
    else if (this.INFO == "REPEAT")
    {
        strhtml.Append("<b>�벻Ҫ���ظ����ݣ�</b><br/>");
    }
    else if (this.INFO == "WAITING")
    {
        strhtml.Append("<b>���ٹ�" + this.KL_CheckIPTime + "��������</b><br/>");
    }
    else if (this.INFO == "MAX")
    {
        strhtml.Append("<b>��Ǹ�����ѳ���һ����������" + KL_CheckZoneReCount + "���ˣ�������������</b><br/>");
    }
    else if (this.INFO == "LOCK")
    {
        strhtml.Append("<b>��Ǹ�����Ѿ����������������ע�ⷢ������</b><br/>");
    }
    if (this.INFO != "") strhtml.Append("</div>");
    strhtml.Append("<div class=\"content\">");
    //��ʾ�����
    strhtml.Append("<form id='MessageForm' name=\"f\" action=\"" + http_start + "bbs/userguessbook.aspx\" method=\"post\">");
    //strhtml.Append("<a href=\"" + this.http_start + "bbs/userguessbook_pic.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=" + this.ot + "&amp;go=" + this.r + "\"><img src=\"" + this.http_start + "bbs/face/" + face + ".gif\" alt=\"����\"/></a>");
	strhtml.Append("<script> function adjustTextareaHeight(textarea) { if (textarea.scrollHeight > textarea.offsetHeight) { textarea.style.height = textarea.scrollHeight + 'px'; } } </script>");
	strhtml.Append("<div class='centered-container'>");
    strhtml.Append("<textarea name=\"content\" oninput=\"adjustTextareaHeight(this)\" rows=\"5\" class=\"KL_textarea\" style=\"width: 98.6%; \" required=\"required\"></textarea>");
	strhtml.Append("</div>");
    strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"touserid\" value=\"" + touserid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"face\" value=\"" + face + "\"/>");
    strhtml.Append("<input type=\"hidden\" name=\"classid\" value=\"" + classid + "\"/>");
    //strhtml.Append("<input type=\"hidden\" name=\"sid\" value=\"" + sid + "\"/>");
    strhtml.Append("<input type=\"submit\" class=\"btn\" name=\"g\" id='Submitcomment' value=\"��Ҫ����\"/>");
    strhtml.Append("</form></div>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a ");
        if (this.ot != "1")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + this.http_start + "bbs/userguessbook.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=0&amp;go=" + this.r + "\">��������</a> ");
        strhtml.Append("<a ");
        if (this.ot == "1")
        {
            strhtml.Append("class=\"btSelect\" ");
        }
        strhtml.Append("href=\"" + this.http_start + "bbs/userguessbook.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;touserid=" + this.touserid + "&amp;ot=1&amp;go=" + this.r + "\">��������</a> ");
        strhtml.Append("</div></div>");
    //��ʾ�б�
    strhtml.Append("<!--listS-->");
    kk = kk + ((CurrentPage - 1) * pageSize) - 1;
    for (int i = 0; (listVo != null && i < listVo.Count); i++)
    {
        if (i % 2 == 0)
        {
            strhtml.Append("<div class=\"line1\">");
        }
        else
        {
            strhtml.Append("<div class=\"line2\">");
        }
        if (ot == "1")
        {
            index = (kk + 1);
        }
        else
        {
            index = (total - kk);
        }
        strhtml.Append("[" + index + "¥] ");
        //strhtml.Append("[<a href=\"" + this.http_start + "bbs/userguessbook_re.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.touserid + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;ot=" + this.ot + "\">��</a>]");
        //--------------------����Ա
        if (this.userid == listVo[i].userid.ToString() || this.IsUserManager(this.userid, userVo.managerlvl, ""))  //�Լ�ɾ���Լ�������
        {
            strhtml.Append("[<a href=\"" + this.http_start + "bbs/userguessbook_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;reid=" + listVo[i].id + "&amp;touserid=" + this.touserid + "&amp;ot=" + this.ot + "\">ɾ</a>]");
        }
        strhtml.Append("<span class=\"retext\">");
        strhtml.Append(listVo[i].content);
        strhtml.Append("</span></div>");
        kk = kk + 1;
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">�������Լ�¼��</div>");
    }
    strhtml.Append("<!--listE-->");
    //��ʾ������ҳ
    strhtml.Append(linkURL);
    string isWebHtml = this.ShowWEB_view(this.classid); //���Ǵ���html����    
    if (isWebHtml != "")
    {
        string strhtml_list = strhtml.ToString();
        //int s = strhtml_list.IndexOf("<div class=\"title\">");
        //strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + this.touserid + "" + "\">�����ϼ�</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "myfile.aspx?siteid=" + siteid + "&amp;classid=" + this.classid + "" + "\">�ҵĵ���</a>");
    strhtml.Append("</div></div>");
    //���
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo)); 
    //��Ա�ɼ�����
}
}
//��ʾ�ײ�
Response.Write(WapTool.showDown(wmlVo)); %>