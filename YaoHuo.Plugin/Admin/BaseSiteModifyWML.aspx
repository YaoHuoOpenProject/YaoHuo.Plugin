<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseSiteModifyWML.aspx.cs" Inherits="YaoHuo.Plugin.Admin.BaseSiteModifyWML" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
Response.Write(WapTool.showTop(this.GetLang("手机管理后台|手機管理後台|Mobile Admin"), wmlVo));//显示头   
if (ver == "1")
{%>	
<%Response.Write(WapTool.GetVS(wmlVo));%>
<%}
else //2.0界面
{%>
<!--<div class="subtitle"><%=this.GetLang("电脑IE管理后台|電腦IE管理後台|WEB Admin Login")%></div>
<div class="content"><a href="login.aspx">00.<%=this.GetLang("点击登录WEB后台|點擊登錄WEB後台|Click here")%></a><br />电脑IE浏览器登录WEB管理后台更直观，功能更强大！建议电脑用户使用！</div>-->
<div class="title"><%=this.GetLang("手机管理后台|手機管理後台|Mobile WAP Admin")%></div>
<div class="content">
<a href="<%=http_start%>admin/applicationadmin.aspx?siteid=<%=siteid%>">01.网站缓存管理</a><br />
<a href="<%=http_start%>admin/webconfig.aspx?siteid=<%=siteid%>">02.系统全局配置</a><br />
<a href="<%=http_start%>admin/sitesetdefault.aspx?siteid=<%=siteid%>">03.网站默认配置</a><br/>
<a href="<%=http_start%>admin/basesitemodifywmlInfo.aspx?siteid=<%=siteid%>">04.基本信息设置</a><br/>
<a href="<%=http_start%>search/book_list.aspx?classid=0&amp;siteid=<%=siteid%>&amp;backurl=<%= HttpUtility.UrlEncode("admin/basesitemodifywml.aspx?siteid="+siteid )%>">05.注册会员管理</a><br/>
<a href="<%=http_start%>wapstyle/style_list01.aspx?siteid=<%=siteid%>">06.CSS 样式管理</a><br/>
<a href="<%=http_start%>admin/wapindexeditwap.aspx?siteid=<%=siteid%>">07.页面综合排版</a><br/>
<a href="<%=http_start%>admin/admin_waplist.aspx?siteid=<%=siteid%>">08.<%=this.GetLang("添加删除内容|添加刪除內容|Add or Remove Content")%></a><br/>
<a href="<%=http_start%>visiteCount/visitedCount.aspx?siteid=<%=siteid%>">09.网站流量统计</a><br/>
<a href="<%=http_start%>admin/backupsqlwml.aspx?siteid=<%=siteid%>">10.清理数据日志</a>
<hr>
<a href="<%=http_start%>admin/ModifyQR.aspx?siteid=<%=siteid%>">11.生成二维码</a><br/>
<a href="<%=http_start%>bbs/UserRegCode.aspx?siteid=<%=siteid%>">12.注册邀请码管理</a><br/>
<a href="<%=http_start%>bbs/ModifySMS_close.aspx?siteid=<%=siteid%>">13.网站注册开关设置</a><br/>
<a href="<%=http_start%>bbs/ModifySMS.aspx?siteid=<%=siteid%>">14.开通短信注册设置</a><br/>
<a href="<%=http_start%>OAuth/QQ/OAuth_help.htm">开通第三方QQ帐号登录</a> <br />
<a href="<%=http_start%>bbs/ModifyDomain.aspx?siteid=<%=siteid%>">15.网站域名绑定设置</a><br/>
<a href="<%=http_start%>bbs/ModifyRight.aspx?siteid=<%=siteid%>">16.角色权限访问设置</a><br/>
<a href="<%=http_start%>bbs/ModifyAllSid.aspx?siteid=<%=siteid%>">17.更新会员身份过期</a><br/>
<a href="<%=http_start%>bbs/ModifyAllUBB.aspx?siteid=<%=siteid%>">18.会员UBB过滤设置</a><br/>
<a href="<%=http_start%>bbs/ModifyDownLink.aspx?siteid=<%=siteid%>">19.防盗链接参数设置</a><br />
<a href="<%=http_start%>admin/siteshare.aspx?siteid=<%=siteid%>">20.网站资源共享设置</a>
<hr>
<a href="<%=http_start%>bbs/toMoneylvl.aspx?siteid=<%=siteid%>">21.币种经验规则设置</a><br/>
<a href="<%=http_start%>bbs/toLvlMedal.aspx?siteid=<%=siteid%>">22.经验头衔等级设置</a><br/>
<a href="<%=http_start%>bbs/toLvlFace.aspx?siteid=<%=siteid%>">23.在线时间图片设置</a><br/>
<a href="<%=http_start%>bbs/toLvlXingZhang.aspx?siteid=<%=siteid%>">24.自动奖励勋章设置</a><br/>
<a href="<%=http_start%>bbs/toMedal.aspx?siteid=<%=siteid%>">25.手动奖励勋章设置</a><br/>
<a href="<%=http_start%>bbs/smalltypelist.aspx?siteid=<%=siteid%>&amp;systype=card">26.设置会员身份属性</a><br />
<a href="<%=http_start%>bbs/modifyusername.aspx?siteid=<%=siteid%>&amp;systype=card">27.更改会员用户名</a><br />
<a href="<%=http_start%>bbs/resetvip.aspx?siteid=<%=siteid%>">28.过期VIP会员重置</a>
<hr>
<%
Response.Write("<a href=\"" + http_start + "chinabank/cardindex_chinaBank_WAP.aspx?siteid=" + siteid + "" + "\">29.充值我的网站</a><br/>");
Response.Write("<a href=\"" + http_start + "chinabank_wap/cardindex_chinaBank_WAP.aspx?siteid=" + siteid + "" + "\">30.本站会员充值设置</a><br/>");
Response.Write("<a href=\"" + http_start + "chinabank_wap/RMBAdd.aspx?siteid=" + siteid + "" + "\">31.本站会员手工入款</a><br/>");
Response.Write("<a href=\"" + http_start + "chinabank_wap/banklist.aspx?siteid=" + siteid + "&amp;tositeid=" + this.siteid + "&amp;backurl=" + HttpUtility.UrlEncode("admin/basesitemodifywml.aspx?siteid="+this.siteid) + "" + "\">32.本站会员充值日志</a><br/>");
Response.Write("<a href=\"" + http_start + "bbs/banklist.aspx?siteid=" + siteid + "&amp;classid=0&amp;key=" + this.userid +  "&amp;backurl=" + HttpUtility.UrlEncode("admin/basesitemodifywml.aspx?siteid="+this.siteid) +"" + "\">33.虚拟货币操作日志</a><br/>");
%>
<hr />
<a href="<%=http_start%>admin/ubb2.aspx">32.UBB大全</a><br/>
<a href="<%=http_start%>admin/ubb3.aspx">33.网站资源链接</a><br/>
<a href="<%=http_start%>admin/ubb4.aspx">34.用户手册</a><br/>
<a href="<%=http_start%>admin/ubb5.aspx">35.技术员手册</a><br/>
<a href="http://bbs.kelink.com">36.WAP建站知识库</a><br/>
</div>
<div class="btBox"><div class="bt2">
<a href="<%=http_start%>myfile.aspx?siteid=<%=siteid%>&amp;userid=<%=userid%>">返回上级</a> <a href="<%=http_start%>wapindex.aspx?siteid=<%=siteid%>">返回首页</a>	
</div></div>
<%}
Response.Write(WapTool.showDown(wmlVo)); //显示底部
%>