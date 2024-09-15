<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFile.aspx.cs" Inherits="YaoHuo.Plugin.MyFile" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder sb = new StringBuilder();
    String welcome = "<div class=\"welcome\">";
    string line1 = "<div class=\"content\">";
    string line2 = "<div class=\"content\">";
    string title = "<div class=\"title\">";
    string div = "</div>";
    string line = "";
 //看是否有电脑版html代码
string isWebHtml = this.ShowWEB_list ("-1");
//显示头
Response.Write(WapTool.showTop(this.GetLang("我的地盘|我的地盤|my zone"), wmlVo));
//显示中间部分
if (strHtml.IndexOf("[myfile]") > -1 || isWebHtml.IndexOf("[view]") > -1)
{
    sb.Append(welcome + "欢迎您：<span class=\"nickname\">" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver) + "</span>" + div);
    sb.Append(line1 + "<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=0&amp;types=0" + "\">信箱</a>(" + messagecount + "/" + messageAll + ") <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=0" + "\">好友</a>(" + goodfriend + ")" + div);
    sb.Append(line2 + "我的ID<span class=\"recolon\">:</span>" + userVo.userid + WapTool.GetOLtimePic(http_start, siteVo.lvlTimeImg, userVo.LoginTimes) + div);
    sb.Append(line1 + "我的" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname,this.lang) + "<span class=\"recolon\">:</span>" + userVo.money + "" + div);
    sb.Append(line2 + "<a href=\"" + http_start + "chinabank_wap/RMBtoMoney.aspx?siteid=" + siteid + "" + "\">充值" + siteVo.sitemoneyname + "</a>/<a href=\"" + http_start + "bbs/banklist.aspx?siteid=" + siteid + "&amp;classid=0&amp;key=" + this.userid + "" + "\">帐目明细</a>" + div);
    //sb.Append(line2 + "<a href=\"" + http_start + "chinabank_wap/RMBtoMoney.aspx?siteid=" + siteid + "" + "\">充值" + siteVo.sitemoneyname + "</a>/<a href=\"" + this.http_start + "bbs/tomoney.aspx?siteid=" + this.siteid + "\">转账</a>/<a href=\"" + http_start + "bbs/banklist.aspx?siteid=" + siteid + "&amp;classid=0&amp;key=" + this.userid + "" + "\">明细</a>" + div);
    sb.Append(line1 + "银行账户<span class=\"recolon\">:</span><span class=\"bankmoney\">" + userVo.myBankMoney + "</span> <a href=\"/bbs/tomybankmoney.aspx?type=1\">取款</a>" + div);
    sb.Append(line2 + "我的经验<span class=\"recolon\">:</span>" + userVo.expr + div);
    sb.Append(line1 + "我的等级<span class=\"recolon\">:</span>" + WapTool.GetLevl(siteVo.lvlNumer, userVo.expr, userVo.money, type) + div);
    sb.Append(line2 + "我的头衔<span class=\"recolon\">:</span>" + WapTool.GetHandle(siteVo.lvlNumer, userVo.expr,userVo.money,type) + div);
    sb.Append(line1 + "我的身份<span class=\"recolon\">:</span><span class=\"current-identity\">" + WapTool.GetMyID(userVo.idname, lang) + "</span><br/><span class=\"ExpirationDate\"><a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=171" + "\">" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime, this.lang) + "</a></span>" + div);
    sb.Append(line1 + "管理权限<span class=\"recolon\">:</span>" + WapTool.GetIDName(siteid, this.userid, userVo.managerlvl, this.lang)   + div);
    sb.Append(line2 + "<a href=\"" + http_start + "xinzhang/book_view_my.aspx?siteid=" + siteid + "&amp;classid=0" + "\">我的勋章</a><span class=\"recolon\">:</span>" + WapTool.GetMedal(userVo.userid.ToString(), userVo.moneyname, WapTool.GetSiteDefault(siteVo.Version, 47), wmlVo) + div);
    sb.Append(line2 + "<a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=224" + "\">申请勋章</a>/<a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=226" + "\">购买勋章</a>" + div);
    //"<br/><a href=\"" + this.http_start + "XinZhang/XinZhangLieBiao.asp?siteid="+this.siteid+"&amp;classid=0&amp;sid="+this.sid+"\">购买勋章</a>" +
    sb.Append(line);
    //sb.Append(line2 + "我的RMB<span class=\"recolon\">:</span>￥" + userVo.RMB.ToString("f2") + "<br/><a href=\"" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid +"\">在线充值</a>/<a href=\"" + http_start + "chinabank_wap/banklist.aspx?siteid=" + siteid + "&amp;tositeid=" + this.siteid + "&amp;touserid=" + this.userid + "" + "\">明细</a>" + div);
   //sb.Append(line2 + "电子邮箱 通讯地址" + div);
    sb.Append(title + "我的设置" + div);
    //sb.Append(line1 + "<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid="+this.userid+"\">我的空间</a> <a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + siteid + "" + "\">修改资料</a>" + div);
    sb.Append(line1 + "<a href=\"/bbs/ModifyPW.aspx?siteid=1000\">更改密码</a> <a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + siteid + "" + "\">修改资料</a>" + div);
    sb.Append(line2 + "<a href=\"" + http_start + "bbs/favlist.aspx?siteid=" + siteid + "&amp;classid=0" + "\">我的收藏</a> <a href=\"/album/albumlist.aspx?siteid=1000&classid=0&smalltypeid=0&touserid="+this.userid+"" + "\">我的相册</a>" + div);
    //sb.Append(line1 + "<a href=\"/album/albumlist.aspx?siteid=1000&classid=0&smalltypeid=0&touserid="+this.userid+"" + "\">我的相册</a>" + div);
    sb.Append(line);
    sb.Append(title + "相关信息" + div);
    sb.Append(line2 + "<a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=0&amp;key=" + this.userid + "&amp;type=pub\">我的帖子</a> <a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + userid + "&amp;" + "\">回复(" + userVo.bbsReCount + ")</a>" + div);
    //sb.Append(line2 + "<a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=0&amp;key=" + this.userid + "&amp;type=pub\">我的帖子(" + userVo.bbsCount + ")</a> <a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + userid + "&amp;" + "\">回复(" + userVo.bbsReCount + ")</a>" + div);
    //sb.Append(line1 + "<a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=2" + "\">我的追求</a> <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=4" + "\">追求我的人</a>" + div);
    sb.Append(line2 + "<a href=\"" + http_start + "clan/main.aspx?siteid=" + this.siteid + "&amp;classid=0\">我的家族</a> <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=1" + "\">我的黑名单</a>" + div);
    sb.Append(title + "网站规则" + div);
    sb.Append(line1 + "<a href=\"" + this.http_start + "bbs/tomoneyinfo.aspx?siteid=" + this.siteid + "\">" + siteVo.sitemoneyname + "获取消费规则</a><br/><a href=\"" + this.http_start + "bbs/tolvlinfo.aspx?siteid=" + this.siteid + "\">经验头衔等级规则</a><br/><a href=\"" + this.http_start + "bbs/totimeinfo.aspx?siteid=" + this.siteid + "" + "\">在线时间图标规则</a>" + div);
	sb.Append("<script>");
	sb.Append("var bankMoneyElement = document.querySelector('.bankmoney'); var parentDivElement = bankMoneyElement.closest('.content'); var bankMoneyValue = parseInt(bankMoneyElement.textContent); if (bankMoneyValue === 0) { parentDivElement.style.display = 'none'; }");
	sb.Append("</script>");
    strHtml = strHtml.Replace("[myfile]", sb.ToString());
    isWebHtml = isWebHtml.Replace("[view]", sb.ToString());
}
//显示电脑效果
if (isWebHtml != "")
{
    Response.Clear();
    Response.Write(WapTool.ToWML(isWebHtml, wmlVo));
    Response.End();
}
//解析UBB方法:
if (strHtml.Trim() == "")
{
    Response.Write("<b>请在WEB/WAP后台---页面综合排版---编辑我的地盘---[顶] 录入[myfile]或自己排版</b><br/>");
}
else
{
    Response.Write(WapTool.ToWML(strHtml, wmlVo));
}
bool isclassadm = WapTool.isClassAdmin(siteid, userid);
//if ("|00|01|03|".IndexOf(userVo.managerlvl) > 0 || isclassadm==true) Response.Write(title + "网站管理后台<a href=\"" + this.http_start + "admin/loginwap.aspx?siteid=" + this.siteid + "\">&gt;&gt;</a>" + div); 
if (this.IsCheckManagerLvl("|00|","")==true) Response.Write(title + "管理后台" + div); 
if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
{
    Response.Write(line1 + "<a href=\"" + this.http_start + "admin/basesitemodifywml.aspx?siteid=" + siteid + "" + "\">站长管理后台</a><br/>");
}
if (userVo.managerlvl == "00")
{
    Response.Write("<a href=\"" + this.http_start + "admin/basesitemodifywml00.aspx?siteid=" + siteid + "" + "\">超级管理后台</a>" + div);
}
if (userVo.managerlvl == "03")
{
    //Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/admin_waplist.aspx?siteid=" + siteid + "" + "\">总编辑管理后台</a>]" + div);
}
if (isclassadm==true)
{
    //Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/admin_waplist.aspx?siteid=" + siteid + "" + "\">栏目管理员后台</a>]" + div);
}
Response.Write(line);
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"/\">返回上级</a> ");
    //Response.Write("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "\">返回上级</a> ");
    Response.Write("<a href=\"" + this.http_start + "waplogout.aspx?siteid=" + siteid + "" + "\">安全退出</a> " );
    Response.Write("</div></div>");
Response.Write(WapTool.showDown(wmlVo));
%>
<script>
    document.addEventListener("DOMContentLoaded", function() {
        const colorMap = {
            '绿色昵称': '#25a444',
            '红色昵称': '#FF0000',
            '蓝色昵称': '#228aff',
            '紫色昵称': '#c000ff',
            '粉色昵称': '#ff6363',
            '粉色昵称': '#ff00c0',
            '灰色昵称': '#BDBDBD',
            '/netimages/vip.gif': { name: '红名VIP', color: '#FF0000' },
            '/netimages/年费vip.gif': { name: '年费VIP', color: '#c000ff' },
            '/netimages/靓号.gif': { name: '靓', color: '#FF7F00' },
            '/netimages/帅.gif': { name: '帅', color: '#228aff' },
            '/netimages/newvip.gif': { name: '金名VIP', color: '#fa6700' }
        };

        function processIdentityElement(identityElement) {
            if (identityElement) {
                const identityText = identityElement.innerHTML;
                const regex = /^(绿色昵称|红色昵称|蓝色昵称|紫色昵称|粉色昵称|灰色昵称)/;
                const match = identityText.match(regex);
                
                if (match && colorMap[match[0]]) {
                    const coloredText = `<span style="color: ${colorMap[match[0]]};">${match[0]}</span>`;
                    identityElement.innerHTML = identityText.replace(match[0], coloredText);
                }

                const imgElement = identityElement.querySelector('img');
                if (imgElement) {
                    const imgSrc = imgElement.getAttribute('src').toLowerCase();
                    if (colorMap[imgSrc]) {
                        const { name, color } = colorMap[imgSrc];
                        imgElement.insertAdjacentHTML('afterend', `<span style="color: ${color}; margin-left: 3px; font-weight: bold;">${name}</span>`);
                    }
                }
            }
        }

        function processExpirationDate(expirationElement) {
            if (expirationElement) {
                const expirationText = expirationElement.innerHTML;
                const updatedText = expirationText.replace('有效期至:', '有效期至<span class="recolon">:</span>');
                if (!expirationText.includes('无期限')) {
                    const dateMatch = expirationText.match(/\d{4}-\d{2}-\d{2}/);
                    if (dateMatch) {
                        const formattedDate = dateMatch[0].replace(/-/g, '/');
                        expirationElement.innerHTML = `有效期至<span class="recolon">:</span>${formattedDate} [<a href="/wapindex.aspx?classid=171">续费</a>]`;
                    }
                } else {
                    expirationElement.innerHTML = `有效期至<span class="recolon">:</span>无期限 [<a href="/wapindex.aspx?classid=171">购买VIP</a>]`;
                }
            }
        }

        function processNicknameElement(nicknameElement) {
            if (nicknameElement) {
                const fontElement = nicknameElement.querySelector('font');
                if (fontElement) {
                    fontElement.removeAttribute('color');
                }
            }
        }

        function processContentElement(contentElement) {
            const identityElement = contentElement.querySelector('.current-identity');
            const expirationElement = contentElement.querySelector('.ExpirationDate');
            if (identityElement && identityElement.textContent.trim() === '普通会员' && expirationElement) {
                contentElement.innerHTML = `我的身份<span class="recolon">:</span><span style="padding-right: 3px;">普通会员</span>[<a href="/wapindex.aspx?siteid=1000&amp;classid=171">购买VIP</a>]`;
            }
        }

        const currentIdentityElements = document.querySelectorAll('.current-identity');
        currentIdentityElements.forEach(processIdentityElement);

        const expirationDateElements = document.querySelectorAll('.ExpirationDate a');
        expirationDateElements.forEach(expirationElement => {
            const parentElement = expirationElement.parentElement;
            processExpirationDate(expirationElement);
            parentElement.innerHTML = expirationElement.innerHTML;
        });

        const nicknameElements = document.querySelectorAll('.nickname');
        nicknameElements.forEach(processNicknameElement);

        const contentElements = document.querySelectorAll('.content');
        contentElements.forEach(processContentElement);
    });
</script>