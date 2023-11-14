<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFile.aspx.cs" Inherits="YaoHuo.Plugin.MyFile" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    StringBuilder sb = new StringBuilder();
    String welcome = "<div class=\"welcome\">";
    string line1 = "<div class=\"content\">";
    string line2 = "<div class=\"content\">";
    string title = "<div class=\"title\">";
    string mylink = "<div class=\"mylink\">";
    string div = "</div>";
    string line = "";

    if (this.ver == "1")
    {
        welcome = "";
        line1 = "";
        line2 = "";
        title = "";
        mylink = "";
        line = "----------<br/>";
        div = "<br/>";
    }
    //看是否有电脑版html代码
    string isWebHtml = this.ShowWEB_list("-1");
    //显示头
    Response.Write(WapTool.showTop(this.GetLang("我的地盘|我的地盤|my zone"), wmlVo));

    if (this.ver == "1")
    {
        Response.Write("<p>");
    }

    //显示中间部分
    if (strHtml.IndexOf("[myfile]") > -1 || isWebHtml.IndexOf("[view]") > -1)
    {
        sb.Append(welcome + "欢迎您：" + WapTool.GetColorNickName(userVo.idname, userVo.nickname, lang, ver) + div);
        sb.Append(line1 + "<a href=\"" + http_start + "bbs/messagelist.aspx?siteid=" + siteid + "&amp;classid=0&amp;types=0" + "\">信箱</a>:(" + messagecount + "/" + messageAll + ") <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=0" + "\">好友</a>(" + goodfriend + ")" + div);
        sb.Append(line2 + "我的ID: " + userVo.userid + WapTool.GetOLtimePic(http_start, siteVo.lvlTimeImg, userVo.LoginTimes) + div);
        sb.Append(line1 + "我的" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, this.lang) + ":" + userVo.money + "" + div);
        //==> 钱相关操作
        string moneyHtml = line2;
        moneyHtml += "<a href=\"" + http_start + "chinabank_wap/RMBtoMoney.aspx?siteid=" + siteid + "" + "\">充值" + siteVo.sitemoneyname + "</a>";
        moneyHtml += "/";
        moneyHtml += "<a href=\"" + http_start + "/bbs/tomoney.aspx?siteid=" + siteid + "\">转账</a>";
        moneyHtml += "/";
        moneyHtml += "<a href=\"" + http_start + "bbs/banklist.aspx?siteid=" + siteid + "&amp;classid=0&amp;key=" + this.userid + "" + "\">明细</a>";
        moneyHtml += div;
        sb.Append(moneyHtml);
        //==> 钱相关操作
        sb.Append(line1 + "银行账户:" + userVo.myBankMoney + " <a href=\"/bbs/tomybankmoney.aspx?type=1\">取款</a>" + div);
        sb.Append(line2 + "我的经验:" + userVo.expr + div);
        sb.Append(line1 + "我的等级:" + WapTool.GetLevl(siteVo.lvlNumer, userVo.expr, userVo.money, type) + div);
        sb.Append(line2 + "我的头衔:" + WapTool.GetHandle(siteVo.lvlNumer, userVo.expr, userVo.money, type) + div);
        sb.Append(line1 + "我的身份:" + WapTool.GetMyID(userVo.idname, lang) + "<br/><a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=171" + "\">" + WapTool.showIDEndTime(userVo.siteid, userVo.userid, userVo.endTime, this.lang) + "</a>" + div);
        sb.Append(line1 + "管理权限:" + WapTool.GetIDName(siteid, this.userid, userVo.managerlvl, this.lang) + div);
        sb.Append(line2 + "<a href=\"" + http_start + "xinzhang/book_view_my.aspx?siteid=" + siteid + "&amp;classid=0" + "\">我的勋章</a>:" + WapTool.GetMedal(userVo.userid.ToString(), userVo.moneyname, WapTool.GetSiteDefault(siteVo.Version, 47), wmlVo) + div);
        sb.Append(line2 + "<a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=224" + "\">申请勋章</a>/<a href=\"/wapindex.aspx?siteid=" + siteid + "&amp;classid=226" + "\">购买勋章</a>" + div);
        //"<br/><a href=\"" + this.http_start + "XinZhang/XinZhangLieBiao.asp?siteid="+this.siteid+"&amp;classid=0&amp;sid="+this.sid+"\">购买勋章</a>" +
        sb.Append(line);
        //sb.Append(line2 + "我的RMB:￥" + userVo.RMB.ToString("f2") + "<br/><a href=\"" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid +"\">在线充值</a>/<a href=\"" + http_start + "chinabank_wap/banklist.aspx?siteid=" + siteid + "&amp;tositeid=" + this.siteid + "&amp;touserid=" + this.userid + "" + "\">明细</a>" + div);
        //sb.Append(line2 + "电子邮箱 通讯地址" + div);
        sb.Append(title + "我的设置" + div);
        sb.Append(line1 + "<a href=\"" + this.http_start + "bbs/userinfo.aspx?siteid=" + this.siteid + "&amp;touserid=" + this.userid + "\">我的空间</a> <a href=\"" + this.http_start + "bbs/modifyuserinfo.aspx?siteid=" + siteid + "" + "\">修改资料</a>" + div);
        sb.Append(line2 + "<a href=\"" + http_start + "bbs/favlist.aspx?siteid=" + siteid + "&amp;classid=0" + "\">我的收藏</a> <a href=\"/album/albumlist.aspx?siteid=1000&classid=0&smalltypeid=0&touserid=" + this.userid + "" + "\">我的相册</a>" + div);
        //sb.Append(line1 + "<a href=\"/album/albumlist.aspx?siteid=1000&classid=0&smalltypeid=0&touserid="+this.userid+"" + "\">我的相册</a>" + div);
        sb.Append(line);
        sb.Append(title + "相关信息" + div);
        sb.Append(line2 + "<a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=0&amp;key=" + this.userid + "&amp;type=pub\">我的帖子</a> <a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + userid + "&amp;" + "\">回复(" + userVo.bbsReCount + ")</a>" + div);
        //sb.Append(line2 + "<a href=\"" + this.http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + this.siteid + "&amp;classid=0&amp;key=" + this.userid + "&amp;type=pub\">我的帖子(" + userVo.bbsCount + ")</a> <a href=\"" + this.http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=0&amp;touserid=" + userid + "&amp;" + "\">回复(" + userVo.bbsReCount + ")</a>" + div);
        //sb.Append(line1 + "<a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=2" + "\">我的追求</a> <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=4" + "\">追求我的人</a>" + div);
        sb.Append(line2 + "<a href=\"" + http_start + "clan/main.aspx?siteid=" + this.siteid + "&amp;classid=0\">我的家族</a> <a href=\"" + http_start + "bbs/FriendList.aspx?siteid=" + siteid + "&amp;classid=0&amp;friendtype=1" + "\">我的黑名单</a>" + div);
        sb.Append(title + "网站规则" + div);
        sb.Append(line1 + "<a href=\"" + this.http_start + "bbs/tomoneyinfo.aspx?siteid=" + this.siteid + "\">" + siteVo.sitemoneyname + "获取消费规则</a><br/><a href=\"" + this.http_start + "bbs/tolvlinfo.aspx?siteid=" + this.siteid + "\">经验头衔等级规则</a><br/><a href=\"" + this.http_start + "bbs/totimeinfo.aspx?siteid=" + this.siteid + "" + "\">在线时间图标规则</a>" + div);
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
    if (this.ver == "1") Response.Write(line);
    bool isclassadm = WapTool.isClassAdmin(siteid, userid);
    if ("|00|01|03|".IndexOf(userVo.managerlvl) > 0 || isclassadm == true) Response.Write(title + "网站管理后台<a href=\"" + this.http_start + "admin/loginwap.aspx?siteid=" + this.siteid + "\">&gt;&gt;</a>" + div);
    if (userVo.managerlvl == "00" || userVo.managerlvl == "01")
    {
        Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/basesitemodifywml.aspx?siteid=" + siteid + "" + "\">站长管理后台</a>]" + div);
    }
    if (userVo.managerlvl == "00")
    {
        Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/basesitemodifywml00.aspx?siteid=" + siteid + "" + "\">超级管理员后台</a>]" + div);
    }
    if (userVo.managerlvl == "03")
    {
        Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/admin_waplist.aspx?siteid=" + siteid + "" + "\">总编辑管理后台</a>]" + div);
    }
    if (isclassadm == true)
    {
        Response.Write(line1 + "[<a href=\"" + this.http_start + "admin/admin_waplist.aspx?siteid=" + siteid + "" + "\">栏目管理员后台</a>]" + div);
    }
    Response.Write(line);
    Response.Write("<div class=\"btBox\"><div class=\"bt2\">");
    Response.Write("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classid + "" + "\">返回上级</a> ");
    Response.Write("<a href=\"" + this.http_start + "waplogout.aspx?siteid=" + siteid + "" + "\">安全退出</a> ");
    Response.Write("</div></div>");
    if (this.ver == "1")
    {
        Response.Write("</p>");
    }
    //显示尾
    Response.Write(WapTool.showDown(wmlVo));
%>
