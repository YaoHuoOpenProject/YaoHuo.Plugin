<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="YaoHuo.Plugin.BBS.ViewUser" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看帖子回复</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="/NetCSS/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function bt_return_onclick() {
            history.go(-1)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td colspan="4">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="12">
                                    <img src="/NetImages/main_title_01.gif" width="12" height="24"></td>
                                <td width="100%" background="/NetImages/main_title_02.gif">
                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="2%">
                                                <div align="center">
                                                    <img src="/NetImages/i08.gif" width="16" height="16">
                                                </div>
                                            </td>
                                            <td width="98%" height="24" valign="middle">内容审核 &gt;&gt; 论坛内容 &gt;&gt;查看帖子
                                                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="13">
                                    <img src="/NetImages/main_title_03.gif" width="13" height="24">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">

                        <div class="txt4">
                            <table id="Table1" width="100%" class="TABLE6">
                                <tbody>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Repeater ID="TopRepeater" runat="server">
                                                <ItemTemplate>
                                                    <table class="tableBorder" id="Table1" cellspacing="1" cellpadding="3" width="100%">
                                                        <tr>
                                                            <td class="column" colspan="2">
                                                                <table id="Table2" cellspacing="0" cellpadding="2" width="100%">
                                                                    <tr>
                                                                        <td class="dateText" valign="middle" align="left">
                                                                            <img src="/NetImages/icon_post_show.gif" align="absMiddle" border="0">&nbsp; <b><span
                                                                                id="_ctPostSubject">
                                                                                <%# DataBinder.Eval(Container.DataItem,"book_title")%>
                                                                            </span></b>
                                                                        </td>
                                                                        <td class="dateText" align="right"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="f" colspan="2">
                                                                <span class="forumThread" id="_ct">发帖人 <b>
                                                                    <%# DataBinder.Eval(Container.DataItem,"book_author")%>
                                                                </b>&nbsp;&nbsp;&nbsp; 出处: <b>
                                                                    <%# DataBinder.Eval(Container.DataItem,"book_pub")%>
                                                                </b>&nbsp;&nbsp;&nbsp; 发帖时间:<b><%# DataBinder.Eval(Container.DataItem,"book_date")%></b>&nbsp;&nbsp;&nbsp;
                                                                    回复数: <b>
                                                                        <%# DataBinder.Eval(Container.DataItem,"book_re")%>
                                                                    </b>&nbsp;&nbsp;&nbsp; 签到送分:<b><%# DataBinder.Eval(Container.DataItem,"reshow")%></b>&nbsp;&nbsp;&nbsp;
                                                                    悬赏分:<b><%# DataBinder.Eval(Container.DataItem,"sendmoney")%></b>&nbsp;&nbsp;&nbsp;
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="fh3a" width="10%">
                                                                <span class="forumThread" id="_ThreadStats">内容:</span></td>
                                                            <td class="fh3a">&nbsp;&nbsp;&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"book_content")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <!-- Repeater Begin -->
                                            <asp:Repeater ID="SiteRepeater" runat="server">
                                                <ItemTemplate>
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td valign="top" align="center" width="100%">
                                                                <div style="padding-bottom: 3px">
                                                                    <table class="tableBorder" cellspacing="0" cellpadding="0" width="100%">
                                                                        <tr>
                                                                            <td colsan="2">
                                                                                <table cellspacing="0" cellpadding="0" width="100%" align="left">
                                                                                    <tr>
                                                                                        <td class="threadSeparator" valign="middle" align="left" colspan="2">
                                                                                            <span class="dateText" align="left">
                                                                                                <img src="/NetImages/icon_post_show.gif" border="0" />
                                                                                                <%#(Container.ItemIndex + 1) + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1)%>
                                                                                                楼
                                                                                                <!--标头-->
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="fh" valign="top" nowrap align="center" width="210" rowspan="2">
                                                                                            <div style="padding-top: 4px">
                                                                                                <img alt="" src="/NetImages/user_IsOffline.gif" border="0" />
                                                                                                <b>
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "nickname")%>
                                                                                                </b><span class="txt2">
                                                                                                    <br>
                                                                                                    <table align="center">
                                                                                                        <tr>
                                                                                                            <td class="txt2" align="left" width="90%">
                                                                                                                <br>
                                                                                                                <b>
                                                                                                                    <br />
                                                                                                                </b>
                                                                                                                <br />
                                                                                                                回复时间：<%# DataBinder.Eval(Container.DataItem, "redate")%><br />
                                                                                                                审核状态：<img src="/NetImages/<%#DataBinder.Eval(Container.DataItem, "ischeck")%>.gif"
                                                                                                                    alt="审核状态" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td class="f" valign="top" width="100%">
                                                                                            <table cellspacing="0" cellpadding="2" width="100%">
                                                                                                <tr align="right">
                                                                                                    <td>&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="fh3a" valign="top">
                                                                                            <table cellspacing="2" cellpadding="0" width="100%" align="left" border="0">
                                                                                                <tr>
                                                                                                    <td width="1" rowspan="2">
                                                                                                        <img height="50px" src="/NetImages/spacer.gif" width="1" />
                                                                                                    </td>
                                                                                                    <td class="txt4" style="padding-top: 4px" valign="top">
                                                                                                        <table cellspacing="0" cellpadding="4" width="90%" align="left">
                                                                                                            <tr>
                                                                                                                <td class="txt4" valign="top" align="left">
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "content")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="30" OnPageChanged="AspNetPager1_PageChanged">
                                            </webdiyer:AspNetPager>
                                            <br />
                                            <!-- Repeater End-->
                                            <!--快捷编辑 by Saufu
                                            <table id="Table10" cellspacing="0" cellpadding="0"border="0" width="100%" style="height: 225px">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="center" width="100%">
                                                            <div id="k" align="center">
                                                                <table class="tableBorder" id="Table11" cellspacing="1" cellpadding="3" width="100%"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="column" colspan="2" align="left">
                                                                                <span class="dateText">&nbsp;<img src="/NetImages/icon_post_show.gif" border="0">&nbsp;
                                                                                    快速编辑 </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="fh" style="width: 90px" valign="top" rowspan="2">
                                                                                UBB语法支持<br>
                                                                                <br>
                                                                                示例:<br>
                                                                                <b>粗体文字</b><br>
                                                                                [b]粗体文字[/b]
                                                                                <p>
                                                                                    &nbsp;</p>
                                                                                <br>
                                                                                <br>
                                                                                <br>
                                                                            </td>
                                                                            <td class="f" valign="top" align="left">
                                                                                <span class="txt5Bold">标题:&nbsp; </span>
                                                                                <asp:TextBox ID="tb_title" runat="server" Width="521px"></asp:TextBox><span class="txt5Bold">&nbsp;
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="fh3b" valign="top" align="left">
                                                                                <asp:TextBox ID="tb_rec" runat="server" Width="606px" Height="193px" TextMode="MultiLine">
                                                                                </asp:TextBox>
                                                                                <br>
                                                                                <asp:Button ID="bt_revert" runat="server" Text=" 保 存 "></asp:Button>&nbsp;&nbsp;
                                                                                <span class="QuickPostText">&nbsp;&nbsp;</span>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            快捷回复 结束-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" valign="top" align="center">
                                            <!--Repeater-->
                                            <input id="bt_return" type="button" value=" 返 回 " onclick="return bt_return_onclick()" class="bt" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr align="right">
                    <td></td>
                </tr>
                <tr align="center">
                    <td>页面执行时间：<%=loadpagetime %>&nbsp;毫秒！</td>
                </tr>
            </tbody>
        </table>
        <input type="hidden" id="CommandType" name="CommandType" value="<%=strCommandType%>" />
        <input type="hidden" id="hidHangBiaoShis" name="hidHangBiaoShis" />
    </form>
</body>
</html>
