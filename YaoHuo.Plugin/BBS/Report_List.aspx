<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_List.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Report_List" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("�ٱ�����|�e�����|Report Management"), wmlVo));//��ʾͷ                                                                                                                                                                       
if (ver == "1")
{
}
else //2.0����
{
    strhtml.Append(ERROR);
    strhtml.Append("<div class=\"title\">");
    strhtml.Append(this.GetLang("�ٱ�����|�e�����|Report Management"));
    strhtml.Append("</div>");
    if (this.CheckManagerLvl("04", "") == true)
    {
        strhtml.Append("<div class=\"btBox\"><div class=\"bt1\">");
        strhtml.Append("<a class=\"noafter\" href=\"" + this.http_start + "bbs/Report_List.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=0\">�鿴����</a><a href=\"" + this.http_start + "bbs/Report_List_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=0&amp;id=0\">ɾ������</a> ");
        strhtml.Append("</div></div>");
    }
    //��ʾ�б�    
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
        strhtml.Append(index + "." + listVo[i].addtime + "<br/>�ٱ��ˣ�<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + listVo[i].userid + "&amp;backurl=" + "\">" + listVo[i].nickname + "(" + listVo[i].userid + ")</a><br/><a href=\"" + http_start + "bbs-" + listVo[i].bbsid + ".html\">�鿴����(ID:" + listVo[i].bbsid + " ��Ŀ:" + listVo[i].classid + ")</a><br/>");
        strhtml.Append("���ͣ�" + listVo[i].ReportType + "<br/>");
        strhtml.Append("��ע��" + listVo[i].ReportWhy + "<br/>");
        strhtml.Append("������<a href=\"" + this.http_start + "bbs/Report_List_del.aspx?action=go&amp;id=" + listVo[i].id + "&amp;siteid=" + this.siteid + "&amp;classid=" + listVo[i].classid + "&amp;page=" + this.CurrentPage + "\">ɾ���˾ٱ�</a>");
        strhtml.Append("</div>");
    }
    if (listVo == null)
    {
        strhtml.Append("<div class=\"tip\">���޼�¼��</div>");
    }
    //��ʾ������ҳ
    strhtml.Append(linkURL);
    string isWebHtml = this.ShowWEB_view(this.classid); //���Ǵ���html����   
    if (isWebHtml != "")
    {
        Response.Clear();
        Response.Write(WapTool.ToWML(isWebHtml, wmlVo).Replace("[view]", strhtml.ToString()));
        Response.End();
    }
    strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
    strhtml.Append("<a href=\"" + this.http_start + "bbs/showadmin.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + "" + "\">�����ϼ�</a> ");
    strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">������ҳ</a>");
    strhtml.Append("</div></div>");
    Response.Write(strhtml);
}
Response.Write(WapTool.showDown(wmlVo)); //��ʾ�ײ�
%>