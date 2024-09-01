<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseSiteModifyWML00.aspx.cs" Inherits="YaoHuo.Plugin.Admin.BaseSiteModifyWML00" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("超级管理员后台|超级管理员后台|Mobile Admin"), wmlVo));//显示头   
if (ver == "1")
{%>	
<%Response.Write(WapTool.GetVS(wmlVo));%>
<%}
else //2.0界面
{
//for (int i=0;i< (Application.Count);i++){
//Response.Write("变量名：" + Application.GetKey(i) );
//Response.Write(" ,变数值：" + Application.Get(i));
//}
%>
<div class="subtitle"><%=this.GetLang("注册软件|注册软件|Reg")%></div>
<div class="content">●<a href="<%=http_start%>admin/ModifyDomainREG.aspx?siteid=<%=siteid%>">注册正版软件</a><br />
<b><%= this.isReg%></b><br />
●<a href="<%=http_start%>version.aspx?siteid=<%=siteid%>">系统升级</a><br />
<% Response.Write(WapTool.ToWML(KL_Site_INFO,wmlVo)+"<br/>");%>
</div>
<div class="subtitle"><%=this.GetLang("电脑IE管理后台|電腦IE管理後台|WEB Admin Login")%></div>
<div class="content"><a href="login.aspx">00.<%=this.GetLang("点击登录WEB后台|點擊登錄WEB後台|Click here")%></a><br />电脑IE浏览器登录WEB管理后台更直观，功能更强大！如:<br />
**.<a href="<%=http_start%>admin/applicationadmin.aspx?back=00&amp;siteid=<%=siteid%>">网站缓存管理</a><br />
01.服务器信息<br />
02.访问量统计<br />
03.网站排行<br />
04.<a href="<%=http_start%>admin/backupsqlwml.aspx?siteid=<%=siteid %>&amp;back=00">数据库维护</a><br />
05.<a href="<%=http_start%>admin/SysSetDefaultWEB00.aspx?siteid=<%=siteid%>">系统WEB设置</a><br />
06.<a href="<%=http_start%>admin/deletetemp00.aspx?siteid=<%=siteid%>">上传文件管理</a><br />
07.公告管理<br />
08.配置网站<br />
09.会员管理<br />
10.<a href="<%=http_start%>admin/ModifyDomain00.aspx?siteid=<%=siteid%>">主域名设置</a><br />
11.域名地址转发<br />
12.<a href="<%=http_start%>admin/webconfig.aspx?back=00&amp;siteid=<%=siteid%>">系统全局配置</a><br />
13.<a href="<%=http_start%>admin/webconfig_top.aspx?back=00&amp;siteid=<%=siteid%>">全局顶和底部内容</a><br />
14.<a href="<%=http_start%>admin/webconfig_code.aspx?back=00&amp;siteid=<%=siteid%>">伪验证码设置</a><br />
15.<a href="<%=http_start%>admin/SysSetDefault00.aspx?siteid=<%=siteid%>">系统默认配置</a><br />
16.<a href="<%=http_start%>wapstyle/style_list00WAP.aspx?siteid=<%=siteid%>">系统CSS管理</a><br />
17.<a href="<%=http_start%>wapstyle/mobileua_list00wap.aspx?siteid=<%=siteid%>">手机型号管理</a><br />
18.插件功能模块管理<br />
19.<a href="http://bbs.kelink.com">WAP建站知识库</a><br />
<hr />
<%Response.Write("<a href=\"" + http_start + "chinabank_wap/banklist.aspx?siteid=" + siteid + "&amp;tositeid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode("admin/basesitemodifywml00.aspx?siteid="+this.siteid) + "" + "\">20.会员充值明细</a><br/>");
  Response.Write("<a href=\"" + http_start + "bbs/banklist.aspx?siteid=" + siteid + "&amp;classid=0&amp;key=" + this.userid + "&amp;backurl=" + HttpUtility.UrlEncode("admin/basesitemodifywml00.aspx?siteid=" + this.siteid) + "" + "\">21.虚拟货币操作明细</a><br/>"); 
     %>
<hr />
</div>
<div class="title"><%=this.GetLang("手机管理内容|手機管理内容|Mobile WAP Admin")%></div>
<div class="subtitle">主要插件模块|<a href="<%=http_start %>admin/admin_waplistFun00.aspx?siteid=<%=siteid %>">所有&gt;&gt;</a></div>
<div class="content">
50.<a href="<%=http_start%>article/admin_userlistWAP00.aspx?siteid=<%=siteid%>">文章内容审核</a><br/>
51.<a href="<%=http_start%>bbs/admin_userlistWAP00.aspx?siteid=<%=siteid%>">论坛内容审核</a><br/>
52.<a href="<%=http_start%>picture/admin_userlistWAP00.aspx?siteid=<%=siteid%>">图片内容审核</a><br/>
53.<a href="<%=http_start%>ring/admin_userlistWAP00.aspx?siteid=<%=siteid%>">铃声内容审核</a><br/>
54.<a href="<%=http_start%>video/admin_userlistWAP00.aspx?siteid=<%=siteid%>">视频内容审核</a><br/>
55.<a href="<%=http_start%>download/admin_userlistWAP00.aspx?siteid=<%=siteid%>">下载内容审核</a><br/>
56.<a href="<%=http_start%>wml/admin_userlistWAP00.aspx?siteid=<%=siteid%>">WML内容审核</a><br/>
57.<a href="<%=http_start%>guessbook/admin_userlistWAP00.aspx?siteid=<%=siteid%>">留言内容审核</a><br/>
58.<a href="<%=http_start%>chat/admin_userlistWAP00.aspx?siteid=<%=siteid%>">聊天内容审核</a><br/>
59.<a href="<%=http_start%>airplane/admin_userlistWAP00.aspx?siteid=<%=siteid%>">航班内容审核</a><br/>
60.<a href="<%=http_start%>hotel/admin_userlistWAP00.aspx?siteid=<%=siteid%>">酒店内容审核</a><br/>
61.<a href="<%=http_start%>paimai/admin_userlistWAP00.aspx?siteid=<%=siteid%>">拍卖内容审核</a><br/>
62.<a href="<%=http_start%>shop/admin_userlistWAP00.aspx?siteid=<%=siteid%>">商店内容审核</a><br/>
63.<a href="<%=http_start%>yuehui/admin_userlistWAP00.aspx?siteid=<%=siteid%>">约会内容审核</a><br/>
64.<a href="<%=http_start%>rizhi/admin_userlistWAP00.aspx?siteid=<%=siteid%>">博客内容审核</a><br/>
65.<a href="<%=http_start%>album/admin_userlistWAP00.aspx?siteid=<%=siteid%>">相册内容审核</a><br/>
66.<a href="<%=http_start%>link/admin_userlistWAP00.aspx?siteid=<%=siteid%>">友情链接审核</a><br/>
67.<a href="<%=http_start%>gongqiu/admin_userlistWAP00.aspx?siteid=<%=siteid%>">行业供求审核</a><br/>
68.<a href="<%=http_start%>adlink/admin_userlistWAP00.aspx?siteid=<%=siteid%>">点击广告审核</a><br/>
69.<a href="<%=http_start%>class/admin_userlistWAP00.aspx?siteid=<%=siteid%>">栏目名称审核</a><br/>
70.<a href="<%=http_start%>class/admin_infolistWAP00.aspx?siteid=<%=siteid%>">排版内容审核</a><br/>
71.<a href="<%=http_start%>search/admin_userlistWAP00.aspx?siteid=<%=siteid%>">站内短信审核</a><br/>
72.<a href="<%=http_start %>bbs/Report_List.aspx?action=class&amp;siteid=<%=siteid %>&amp;classid=0">贴子举报审核</a><br/>
73.<a href="<%=http_start%>guangbo/admin_userlistWAP00.aspx?siteid=<%=siteid%>">广播内容审核</a><br/>
74.<a href="<%=http_start%>myaccount/admin_userlistWAP00.aspx?siteid=<%=siteid%>">记帐内容审核</a><br/>
75.<a href="<%=http_start%>novel/admin_userlistWAP00.aspx?siteid=<%=siteid%>">小说内容审核</a><br/>
76.<a href="<%=http_start%>games/gamesadmin00.aspx?siteid=<%=siteid%>">游戏大厅审核</a><br/>
77.<a href="<%=http_start%>xinzhang/admin_userlistWAP00.aspx?siteid=<%=siteid%>">购买勋章审核</a><br/>
78.<a href="<%=http_start%>Signin/admin_userlistWAP00.aspx?siteid=<%=siteid%>">签到插件审核</a><br/>
79.<a href="<%=http_start%>wabao/admin_userlistWAP00.aspx?siteid=<%=siteid%>">挖宝插件审核</a><br/>
80.<a href="<%=http_start%>FormEdit/admin_userlistWAP00.aspx?siteid=<%=siteid%>">表单设计审核</a><br/>
</div>
<div class="tip">注意：为了系统安全，以上没有链接的功能可以用IE浏览器访问进入WEB后台操作！</div>
<div class="btBox"><div class="bt2">
<a href="<%=http_start%>myfile.aspx?siteid=<%=siteid%>&amp;userid=<%=userid%>">返回上级</a> <a href="<%=http_start%>wapindex.aspx?siteid=<%=siteid%>">返回首页</a>	
</div></div>
<%}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>