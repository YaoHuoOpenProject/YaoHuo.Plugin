<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_Re_my.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_Re_My" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("�鿴" + this.touserid + "�ظ�|�鿴+this.touserid+���}|View Reply"), wmlVo));//��ʾͷ
if (ver == "1")
{
    //���
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
}
else //2.0����
{
    strhtml.Append(ERROR);
    //��Ա�ɼ�
 	if (this.IsCheckManagerLvl("|00|01|02|03|04|","")==true)
    {
	strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/HyperLink.js\" defer></script>");
    strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/Shad@w.js?l\" defer></script>");
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    //��������
    strhtml.Append("<a ");
    if (this.ot != "1")
    {
        strhtml.Append("class=\"btSelect\" ");
    }
    strhtml.Append("href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;touserid=" + this.touserid + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=0&amp;go=" + this.r + "\">�����»ظ�</a> ");

    strhtml.Append("<a ");
    if (this.ot == "1")
    {
        strhtml.Append("class=\"btSelect\" ");
    }
    strhtml.Append("href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;touserid=" + this.touserid + "&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.CurrentPage + "&amp;lpage=" + this.lpage + "&amp;ot=1&amp;go=" + this.r + "\">������ظ�</a> ");
    strhtml.Append("</div></div>");
    //����Ա����
    if (this.CheckManagerLvl("04", "") == true && this.touserid != "1000") // ����Ƿ�Ϊ����Ա��ID��Ϊ1000
    {
        strhtml.Append("<div class=\"tip\">");
        strhtml.Append("<a class=\"urlbtn\" onclick=\"return confirm('��ջظ�ǰ����ȷ�ϲ���');\" href=\"" +
            this.http_start + "bbs/Book_re_delmy.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" +
            this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;touserid=" +
            this.touserid + "&amp;ot=" + this.ot + "\">���(" + this.touserid + ")�����лظ�</a><br />");
        strhtml.Append("</div>");
    }
    //��ʾ������ҳ
    strhtml.Append(linkTOP);
    //��ʾ�б�
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
        strhtml.Append("" + index + ".<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a>��<span class=\"retext\">");
        strhtml.Append(listVo[i].content + "</span><br/> " + string.Format("{0:MM-dd HH:mm}", listVo[i].redate) + " <a href=\"" + this.http_start + "bbs-" + listVo[i].bookid + "" + ".html\">�鿴</a></div>");
        kk = kk + 1;
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">���޻ظ���¼��</div>");
    }
    //��ʾ������ҳ
    strhtml.Append(linkURL);
    //��Ա�ɼ�����
}
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "myfile.aspx\">�ҵĵ���</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "\">������ҳ</a>");
    strhtml.Append("</div></div>");
    //���
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
}
Response.Write(WapTool.showDown(wmlVo)); //��ʾ�ײ�
%>