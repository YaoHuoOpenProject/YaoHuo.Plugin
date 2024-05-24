<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamesIndex.aspx.cs" Inherits="YaoHuo.Plugin.Games.GamesIndex" %>

<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(classVo.classname, wmlVo));//显示头                                                                                                                                                                       
    if (ver == "1")
    {
        strhtml.Append("<p align=\"" + classVo.position + "\">");

        if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
        {
            strhtml.Append("<img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/><br/>");
        }
        if (classVo.introduce != "")
        {
            strhtml.Append(classVo.introduce + "<br/>");
        }

        //显示广告
        if (adVo.secondShowTop != "")
        {
            strhtml.Append(adVo.secondShowTop + "<br/>");
        }

        //显示内容开始
        strhtml.Append("<img src=\"wabao/wabao.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/waBao/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">挖宝竟猜</a>.<a href=\"" + this.http_start + "games/wabao/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">历史开奖</a><br/>");
        strhtml.Append("今日下注总数:" + this.GetTodayState("waBao", "1") + "次 <br/>");
        strhtml.Append("今日下注总量:" + this.GetTodayState("waBao", "2") + siteVo.sitemoneyname + "<br/>");

        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"happyten/10.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/happyTen/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">快乐十分</a>.<a href=\"" + this.http_start + "games/happyTen/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">历史开奖</a><br/>");
        strhtml.Append("今日下注总数:" + this.GetTodayState("happyTen", "1") + "次 <br/>");
        strhtml.Append("今日下注总量:" + this.GetTodayState("happyTen", "2") + siteVo.sitemoneyname + "<br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"marksix/zhong.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/marksix/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">博彩游戏</a>.<a href=\"" + this.http_start + "games/marksix/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">历史开奖</a><br/>");
        strhtml.Append("今日下注总数:" + this.GetTodayState("MarkSix", "1") + "次 <br/>");
        strhtml.Append("今日下注总量:" + this.GetTodayState("MarkSix", "2") + siteVo.sitemoneyname + "<br/>");
        //strhtml.Append("--------<br/>");
        //strhtml.Append("<img src=\"ball/ball.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/ball/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">球彩竟猜</a>.<a href=\"" + this.http_start + "games/ball/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">我要下注</a><br/>");
        // strhtml.Append("今日下注总数:" + this.GetTodayState("ballLottery", "1") + "次 <br/>");
        // strhtml.Append("今日下注总量:" + this.GetTodayState("ballLottery", "2") + siteVo.sitemoneyname + "<br/>");


        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"stone/st.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/stone/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">石头剪刀布</a>.<a href=\"" + this.http_start + "games/stone/add.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">公开叫板</a><br/>");
        strhtml.Append("今日疯狂总数:" + this.GetTodayState("stone", "1") + "次 <br/>");
        strhtml.Append("今日石头总量:" + this.GetTodayState("stone", "2") + siteVo.sitemoneyname + "<br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"touzi/sz.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/touzi/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">激情骰子</a>.<a href=\"" + this.http_start + "games/touzi/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.userid + "\">激情的岁月</a><br/>");
        strhtml.Append("今日掷骰总次数:" + this.GetTodayState("touzi", "1") + "次 <br/>");
        strhtml.Append("今日掷骰总金额:" + this.GetTodayState("touzi", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"apple/apple.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/apple/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">苹果机</a>.<a href=\"" + this.http_start + "games/apple/info.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">玩法</a> <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("apple", "1") + "次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("apple", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"quankun/qkb.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/quankun/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">乾坤宝</a>.<a href=\"" + this.http_start + "games/quankun/info.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">规则</a><br/>");
        strhtml.Append("今日压注总次数:" + this.GetTodayState("quankun", "1") + " 次 <br/>");
        strhtml.Append("今日压注总金额:" + this.GetTodayState("quankun", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"chuiniu/dh.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/chuiniu/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">疯狂吹牛</a>.<a href=\"" + this.http_start + "games/chuiniu/add.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">放狠话</a> <br/>");
        strhtml.Append("今日传播大话总数:" + this.GetTodayState("chuiniu", "1") + " 次 <br/>");
        strhtml.Append("今日传播大话金额:" + this.GetTodayState("chuiniu", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"lucky28/luck28.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/lucky28/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">幸运28！</a> <br/>");
        strhtml.Append("看看今天谁是幸运大赢家！ <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("lucky28", "1") + " 次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("lucky28", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"shoot/ball.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/shoot/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">疯狂射门</a> <br/>");
        strhtml.Append("超级刺激的疯狂射门，让您体会足球的乐趣，赚币多多！ <br/>");
        strhtml.Append("今日疯狂射门总次数:" + this.GetTodayState("shoot", "1") + " 次 <br/>");
        strhtml.Append("今日疯狂射门总金额:" + this.GetTodayState("shoot", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"horse/2.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/horse/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">赛马场</a><br/>");
        strhtml.Append("疯狂刺激，爽，赚币多多！ <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("horse", "1") + " 次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("horse", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"egg/egg.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/egg/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">砸金蛋</a> <br/>");
        strhtml.Append("高赔率，快速致富的好途径！<br/>");
        strhtml.Append("今日玩家总次数:" + this.GetTodayState("egg", "1") + " 次 <br/>");
        strhtml.Append("今日砸金总金额:" + this.GetTodayState("egg", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"/netImages/nc.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "nongchang/index.asp?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;sid=" + this.sid + "\">开心农场</a> <br/>");
        strhtml.Append("在自己农场里开垦土地、种植各种蔬菜和水果，快去偷菜吧。<br/>");
        strhtml.Append("--------<br/>");
        strhtml.Append("<img src=\"/netImages/jz.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "clan/main.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">家族争霸</a> <br/>");
        strhtml.Append("不同家族之间争霸，可让自己的家族雄起来。<br/>");

        //strhtml.Append("</div>");

        //显示广告
        if (adVo.secondShowDown != "")
        {
            strhtml.Append(adVo.secondShowDown + "<br/>");
        }

        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid + "" + "\">返回上级</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");
        strhtml.Append(WapTool.GetVS(wmlVo));
        if (classVo.sitedowntip != "")
        {
            strhtml.Append("<br/>" + classVo.sitedowntip);
        }
        strhtml.Append("</p>");
        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));

    }
    else //2.0界面
    {
        if (classVo.siteimg != "UploadFiles/no.gif" && classVo.siteimg != "NetImages/no.gif")
        {
            strhtml.Append("<div class=\"logo\"><img src=\"" + http_start + classVo.siteimg + "\" alt=\"LOGO\"/></div>");
        }
        if (classVo.introduce != "")
        {
            strhtml.Append(classVo.introduce);
        }

        //显示广告
        if (adVo.secondShowTop != "")
        {
            strhtml.Append(adVo.secondShowTop);
        }
        //显示内容
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"chuiniu/dh.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/chuiniu/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">疯狂吹牛</a>.<a href=\"" + this.http_start + "games/chuiniu/add.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">放狠话</a> <br/>");
        strhtml.Append("今日传播大话总数:" + this.GetTodayState("chuiniu", "1") + " 次 <br/>");
        strhtml.Append("今日传播大话金额:" + this.GetTodayState("chuiniu", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line2\">");
        strhtml.Append("<img src=\"wabao/wabao.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/waBao/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">挖宝竟猜</a>.<a href=\"" + this.http_start + "games/wabao/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">历史开奖</a><br/>");
        strhtml.Append("今日下注总数:" + this.GetTodayState("waBao", "1") + "次 <br/>");
        strhtml.Append("今日下注总量:" + this.GetTodayState("waBao", "2") + siteVo.sitemoneyname + "<br/>");
        strhtml.Append("</div>");

        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"stone/st.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/stone/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">石头剪刀布</a>.<a href=\"" + this.http_start + "games/stone/add.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">公开叫板</a><br/>");
        strhtml.Append("今日疯狂总数:" + this.GetTodayState("stone", "1") + "次 <br/>");
        strhtml.Append("今日石头总量:" + this.GetTodayState("stone", "2") + siteVo.sitemoneyname + "<br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line2\">");
        strhtml.Append("<img src=\"touzi/sz.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/touzi/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">激情骰子</a>.<a href=\"" + this.http_start + "games/touzi/book_list.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;touserid=" + this.userid + "\">激情的岁月</a><br/>");
        strhtml.Append("今日掷骰总次数:" + this.GetTodayState("touzi", "1") + "次 <br/>");
        strhtml.Append("今日掷骰总金额:" + this.GetTodayState("touzi", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"apple/apple.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/apple/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">苹果机</a>.<a href=\"" + this.http_start + "games/apple/info.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">玩法</a> <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("apple", "1") + "次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("apple", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line2\">");
        strhtml.Append("<img src=\"quankun/qkb.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/quankun/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">乾坤宝</a>.<a href=\"" + this.http_start + "games/quankun/info.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">规则</a><br/>");
        strhtml.Append("今日压注总次数:" + this.GetTodayState("quankun", "1") + " 次 <br/>");
        strhtml.Append("今日压注总金额:" + this.GetTodayState("quankun", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"lucky28/luck28.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/lucky28/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">幸运28！</a> <br/>");
        strhtml.Append("看看今天谁是幸运大赢家！ <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("lucky28", "1") + " 次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("lucky28", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line2\">");
        strhtml.Append("<img src=\"shoot/ball.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/shoot/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">疯狂射门</a> <br/>");
        strhtml.Append("超级刺激的疯狂射门，让您体会足球的乐趣，赚币多多！ <br/>");
        strhtml.Append("今日疯狂射门总次数:" + this.GetTodayState("shoot", "1") + " 次 <br/>");
        strhtml.Append("今日疯狂射门总金额:" + this.GetTodayState("shoot", "2") + siteVo.sitemoneyname + "  <br/>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"horse/2.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/horse/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">赛马场</a><br/>");
        strhtml.Append("疯狂刺激，爽，赚币多多！ <br/>");
        strhtml.Append("今日投注总次数:" + this.GetTodayState("horse", "1") + " 次 <br/>");
        strhtml.Append("今日投注总金额:" + this.GetTodayState("horse", "2") + siteVo.sitemoneyname + " <br/>");
        strhtml.Append("</div>");
        //strhtml.Append("<div class=\"line2\">");
        //strhtml.Append("<img src=\"egg/egg.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "games/egg/index.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">砸金蛋</a> <br/>");
        //strhtml.Append("高赔率，快速致富的好途径！<br/>");
        //strhtml.Append("今日玩家总次数:" + this.GetTodayState("egg", "1") + " 次 <br/>");
        //strhtml.Append("今日砸金总金额:" + this.GetTodayState("egg", "2") + siteVo.sitemoneyname + " <br/>");
        //strhtml.Append("</div>");
        strhtml.Append("<div class=\"line1\">");
        strhtml.Append("<img src=\"/netImages/jz.gif\" alt=\"PIC\"/><a href=\"" + this.http_start + "clan/main.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "\">家族争霸</a> <br/>");
        strhtml.Append("不同家族之间争霸，可让自己的家族雄起来。");
        strhtml.Append("</div>");

        //显示广告
        if (adVo.secondShowDown != "")
        {
            strhtml.Append(adVo.secondShowDown);
        }
        //导航按钮
        strhtml.Append("<div class=\"btBox\"><div class=\"bt2\">");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid + "" + "\">返回上级</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "wapindex.aspx?siteid=" + siteid + "&amp;classid=0" + "\">返回首页</a>");

        strhtml.Append("</div></div>");
        if (classVo.sitedowntip != "")
        {
            strhtml.Append(classVo.sitedowntip);
        }

        if (base.IsCheckManagerLvl("|00|01|", ""))
        {
            strhtml.Append("<div class=\"tip\">");
            strhtml.Append("提示:在WEB/WAP【页面综合排版】，点击此栏目进去，点击【顶】【底】可录入UBB内容！【插入广告】也可以。有专门的游戏UBB调用。");
            strhtml.Append("</div>");
        }
        //输出
        Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));

    }

    Response.Write(ERROR);
    //显示底部
    Response.Write(WapTool.showDown(wmlVo));
%>