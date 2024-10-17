<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book_View.aspx.cs" Inherits="YaoHuo.Plugin.BBS.Book_View" %>
<%@ Import Namespace="YaoHuo.Plugin.Tool" %>
<%
    Response.Write(WapTool.showTop(bookVo.book_title, wmlVo));
    if (bookVo.islock == 1)
    {
        content = ""; //锁定后不显示内容
    }
    string isWebHtml = this.ShowWEB_view(this.classid);
    //显示广告
    if (adVo.threeShowTop != "")
    {
        strhtml.Append(adVo.threeShowTop);
    }
    strhtml.Append("<!--web-->");
    if (bookVo.ischeck == 2)
    {
        strhtml.Append("<div class=\"tip\"><b>此帖已在回收站！</b></div>");
    }
    //悬赏和派币公用CSS和HTML部分
    if (bookVo.freeMoney > 0 || bookVo.sendMoney > 0)
    {
        strhtml.Append("<link href=\"/NetCSS/BookView/Notification.css\" rel=\"stylesheet\" type=\"text/css\"/>");
        strhtml.Append("<div class=\"rectangle-container\" style=\"display:none;\"> <div class=\"rectangle\"> <div class=\"notification-text\"> <i class=\"material-icons\"><svg xmlns=\"http://www.w3.org/2000/svg\" height=\"24\" viewBox=\"0 -960 960 960\" width=\"24\"> <path d=\"M440-280h80v-240h-80v240Zm40-320q17 0 28.5-11.5T520-640q0-17-11.5-28.5T480-680q-17 0-28.5 11.5T440-640q0 17 11.5 28.5T480-600Zm0 520q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Zm0-80q134 0 227-93t93-227q0-134-93-227t-227-93q-134 0-227 93t-93 227q0 134 93 227t227 93Zm0-320Z\"/> </svg> </i> <span><span style=\"padding-left:3px;\">");

        if (bookVo.freeMoney > 0)
        {
            strhtml.Append("派币</span><span style=\"padding:2px;\">" + bookVo.freeMoney + "</span>已结束</span> </div> </div> </div>");
        }

        if (bookVo.sendMoney > 0)
        {
            strhtml.Append("悬赏</span><span style=\"padding:2px;\">" + bookVo.sendMoney + "</span>已结束</span> </div> </div> </div>");
        }
    }

    //会员可见
    if (this.IsCheckManagerLvl("02", "") == true)
    {
        strhtml.Append("<div class=\"subtitle2\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/SendMoney_freeMain.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;touserid=" + bookVo.book_pub + "&amp;siteid=" + this.siteid + "\" class=\"showReward\">打赏</a> ");
        strhtml.Append("<a href=\"/bbs/book_list.aspx?gettotal=2024&action=new" + "\">新帖</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\">管理</a> ");
        strhtml.Append("<a class=\"BBSettings\" href=\"/bbs/settings.aspx\">设置</a>");
        strhtml.Append("</div>");
    }
    //会员可见结束

    //管理员可见
    if (this.IsCheckManagerLvl("|00|01|03|04|", "") == true)
    {
        strhtml.Append("<div class=\"subtitle2\">");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/SendMoney_freeMain.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;touserid=" + bookVo.book_pub + "&amp;siteid=" + this.siteid + "\" class=\"showReward\">打赏</a> ");
        strhtml.Append("<a href=\"/bbs/book_list.aspx?gettotal=2024&action=new" + "\">新帖</a> ");
        strhtml.Append("<a class=\"BBSettings\" href=\"/bbs/settings.aspx\">设置</a> ");
        strhtml.Append("<a href=\"" + this.http_start + "bbs/Book_View_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\">管理</a> ");
        strhtml.Append("</div>");
    }
    //管理员可见结束

    strhtml.Append("<div class=\"content\">");
    if (bookVo.sendMoney > 0)
    {
        strhtml.Append("<span class=\"xuanshang\">[悬赏]<span class=\"xuanshangshuzi\" style='padding-left:3px;'>" + bookVo.sendMoney + "</span><span class=\"yishang\" style=\"padding:0 2px 0 12px;\">已赏</span><span class=\"yishangshuzi\">" + bookVo.hasMoney + "</span><br/></span>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/XuanShang.js\"></script>");
    }
    if (bookVo.freeMoney > 0)
    {
        strhtml.Append("<div class=\"paibi\" style=\"line-height:40px;\"><span class=\"lijin\" style=\"padding-right:0.2em;\">礼金</span><span class=\"lijinshuzi\" style=\"padding-right:0.7em;\">" + bookVo.freeMoney + "</span>");
        //strhtml.Append("方式：");
        string[] freerule = bookVo.freeRule.Split('_');
        string[] free1 = freerule[0].Split('|');
        string[] free2 = freerule[1].Split('|');
        if (free1.Length == 1)
        {
            strhtml.Append("<span class=\"meiren\" style=\"padding-right:0.15em;\">每人</span><span class=\"meirenshuzi\">" + free2[0]);
        }
        else
        {
            for (int y = 0; y < free1.Length; y++)
            {
                strhtml.Append("<br/>楼层:" + free1[y] + "派礼:" + free2[y]);
            }
        }
        strhtml.Append("</span><span class=\"shengyu\" style=\"padding-left:6px;\">(<span class=\"yu\" style=\"padding:.5px;\">余</span><span class=\"yushuzi\" style=\"padding:.5px;\">" + bookVo.freeLeftMoney + "</span>)</span>");
        strhtml.Append("</div>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/Paibi.js\"></script>");
    }
    strhtml.Append("<div class=\"Postinfo\"><span class=\"biaotiwenzi\" style='padding-right:3px;'>[标题]</span>" + bookVo.book_title + "<span style='padding-left: 3px;' class=\"yueduliang\">(阅" + bookVo.book_click + ")</span><br/>");
    strhtml.Append("<span class=\"Postime\">[时间]<span class=\"DateAndTime\" style='padding-left:3px;'>" + string.Format("{0:yyyy-MM-dd HH:mm}", bookVo.book_date) + "</span>");
    strhtml.Append("</span>");
    if (bookVo.myGetMoney > 0)
    {
        strhtml.Append("<span class=\"post-badge\" id=\"stamp-badge\" style=\"opacity: 0.8;\">获赏<span class=\"earnbounty\">" + bookVo.myGetMoney + "</span></span>");
    }
    //帖子内容
    strhtml.Append("</div><div class=\"dashed\"></div><div class=\"bbscontent\">");
    strhtml.Append(content + "</div></div></div>");

    //分页显示
    strhtml.Append(linkURL);
    //全局CSS样式（打赏盖章、打赏弹窗、下拉表情、搜索弹窗）
    strhtml.Append("<link href=\"/NetCSS/CSS/Setting.css\" rel=\"stylesheet\" type=\"text/css\"/>");
    strhtml.Append("<link href=\"/NetCSS/BookView/ALL.css\" rel=\"stylesheet\" type=\"text/css\"/>");
    strhtml.Append("<link href=\"/NetCSS/BookView/NewReply.css\" rel=\"stylesheet\" type=\"text/css\"/>");

    //显示竞猜模块
    if (guessingData != null)
    {
        strhtml.Append("<div class=\"Betting-widget\">");
        strhtml.Append("<div class=\"Betting-container bg-white rounded-xl shadow-md overflow-hidden\">");
        strhtml.Append("<div class=\"flex justify-between items-start mb-4\">");
        strhtml.Append("<h1 class=\"Betting-title\">猜对即可分妖晶</h1>");
        strhtml.Append("<div class=\"Betting-points\">可用妖晶 <span class=\"Betting-points-value\">" + userVo.money + "</span><a style=\"color: black;font-size: 0.7em;margin-left: -1px;\" href=\"/chinabank_wap/RMBtoMoney.aspx\"> &gt;</a></div>");
        strhtml.Append("</div>");

        // 添加奖池信息
        int totalAmount = guessingData.Options.Sum(o => o.Amount);
        strhtml.Append("<div class=\"mb-6\">");
        strhtml.Append("<div class=\"flex items-center justify-between mb-2\">");
        strhtml.Append("<span class=\"Betting-pool-label\"><span class=\"pool-text\">奖池</span><span class=\"have-text\">共有</span></span>");
        strhtml.Append("<div class=\"Betting-pool-digits\">");
        string poolAmount = totalAmount.ToString("00000000");
        foreach (char digit in poolAmount)
        {
            strhtml.Append("<span class=\"Betting-pool-digit\">" + digit + "</span> ");
        }
        strhtml.Append("</div>");
        strhtml.Append("<span class=\"Betting-pool-label righttext\"></span>");
        strhtml.Append("</div>");

        // 添加进度条
        int option1Percentage = totalAmount > 0 ? (int)((double)guessingData.Options[0].Amount / totalAmount * 100) : 50;
        int option2Percentage = 100 - option1Percentage;
        strhtml.Append("<div class=\"relative pt-1\">");
        strhtml.Append("<div class=\"flex mb-2 items-center justify-between\">");
        strhtml.Append("<div class=\"text-xs font-semibold inline-block py-1 px-2 uppercase rounded-full text-white bg-red-500\">" + option1Percentage + "%</div>");
        strhtml.Append("<div class=\"text-xs font-semibold inline-block py-1 px-2 uppercase rounded-full text-white bg-green-500\">" + option2Percentage + "%</div>");
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"overflow-hidden h-2 mb-4 text-xs flex rounded bg-gray-200\">");
        strhtml.Append("<div style=\"width:" + option1Percentage + "%\" class=\"shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-gradient-to-r from-red-500 to-red-600\"></div>");
        strhtml.Append("<div style=\"width:" + option2Percentage + "%\" class=\"shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-gradient-to-r from-green-500 to-green-600\"></div>");
        strhtml.Append("</div>");
        strhtml.Append("</div>");
        strhtml.Append("</div>");

        strhtml.Append("<div class=\"text-xl font-bold mb-4\">" + guessingData.Title + "</div>");

        if (!guessingData.IsClosed)
        {
            // 未下注，显示竞猜按钮
            if (userBet == null)
            {
                strhtml.Append("<div class=\"flex justify-between mb-8\">");
                for (int i = 0; i < guessingData.Options.Count; i++)
                {
                    var option = guessingData.Options[i];
                    string voteUrl = this.http_start + "bbs/GuessVote.aspx?id=" + guessingData.Id + "&option=" + HttpUtility.UrlEncode(option.Text);
                    strhtml.Append("<button class=\"Betting-prediction-button " + (i == 0 ? "rise" : "fall") + "\" data-vote-url=\"" + voteUrl + "\">");
                    strhtml.Append(option.Text);
                    strhtml.Append("</button>");
                }
                strhtml.Append("</div>");
            }
            // 已下注，显示参与信息
            if (userBet != null)
            {
                strhtml.Append("<div class=\"user-bet-info mb-4\">");
                strhtml.Append("<button class=\"Betting-user-bet-button " + (userBet.OptionId == 1 ? " rise" : "fall") + "\" disabled>");
                strhtml.Append(string.Format("您已竞猜<span class=\"mybet-amount\">{0}</span>{1}", userBet.Amount.ToString("F0"), HttpUtility.HtmlEncode(guessingData.Options[userBet.OptionId - 1].Text)));
                strhtml.Append("</button>");
                strhtml.Append("</div>");
            }
            // 添加倒计时
            DateTime deadline = guessingData.Deadline;
            strhtml.Append("<div class=\"Betting-countdown text-center text-gray-700\">距");
            strhtml.Append("<span class=\"Betting-date-time-group\">" + deadline.Month.ToString("00") + "</span>月");
            strhtml.Append("<span class=\"Betting-date-time-group\">" + deadline.Day.ToString("00") + "</span>日");
            strhtml.Append("<span class=\"Betting-date-time-group\">" + deadline.Hour.ToString("00") + "</span>时截止下注还有 ");
            strhtml.Append("<span class=\"Betting-countdown-digit countdown-days\">00</span> 天 ");
            strhtml.Append("<span class=\"Betting-countdown-digit countdown-hours\">00</span> 时 ");
            strhtml.Append("<span class=\"Betting-countdown-digit countdown-minutes\">00</span> 分 ");
            strhtml.Append("<span class=\"Betting-countdown-digit countdown-seconds\">00</span> 秒");
            strhtml.Append("</div>");
        }
        else
        {
            strhtml.Append("<div class=\"user-bet-info mb-4\">");
            if (guessingData != null && guessingData.IsClosed)
            {
                string winningClass = WinningOptionId == 1 ? "rise" : "fall";
                strhtml.Append("<button class=\"Betting-user-bet-button " + winningClass + "\">");
                if (WinningOptionId.HasValue && !string.IsNullOrEmpty(WinningOptionText))
                {
                    strhtml.Append("竞猜正确答案：" + WinningOptionText);
                }
                else
                {
                    strhtml.Append("竞猜已结束，请等待获胜结果");
                }
                strhtml.Append("</button>");
            }
            strhtml.Append("</div>");
            strhtml.Append("<div class=\"Betting-TipText\">奖池将按投注比例分配给预测正确的玩家</div>");
        }

        strhtml.Append("</div>");
        strhtml.Append("</div>");

        strhtml.Append("<div id=\"betting-dialog\" class=\"Betting-dialog-overlay\" style=\"display:none\">");
        strhtml.Append("<div class=\"Betting-dialog-content\">");
        strhtml.Append("<div id=\"dialogTitle\" class=\"Betting-dialog-title\"></div>");
        strhtml.Append("<div class=\"Betting-points-grid\">");
        for (int i = 0; i < 9; i++)
        {
            strhtml.Append("<button class=\"Betting-points-button\"></button>");
        }
        strhtml.Append("</div>");
        strhtml.Append("<div class=\"Betting-dialog-footer\">可用 <span id=\"available-points\">" + userVo.money + "</span> 妖晶</div>");
        strhtml.Append("<button id=\"confirmButton\" class=\"Betting-confirm-button\"></button>");
        strhtml.Append("</div>");
        strhtml.Append("</div>");

        // 添加自定义提示框
        strhtml.Append("<div id=\"custom-alert\" class=\"Betting-alert-overlay\" style=\"display:none;\">");
        strhtml.Append("<div class=\"Betting-alert-content\">");
        strhtml.Append("<div id=\"alert-message\" class=\"Betting-alert-message\"></div>");
        strhtml.Append("<button id=\"alert-close\" class=\"Betting-alert-close\">确定</button>");
        strhtml.Append("</div>");
        strhtml.Append("</div>");

        // 添加样式
        strhtml.Append("<link rel=\"stylesheet\" href=\"/netcss/css/Betting.css?69\" type=\"text/css\" />");
        // 添加脚本
        strhtml.Append("<script src=\"/NetCSS/JS/Confetti.Browser.min.js?001\"></script>");
        strhtml.Append("<script src=\"/NetCSS/JS/BookView/Celebration.js?001\"></script>");
        strhtml.Append("<script src=\"/NetCSS/JS/BookView/Betting.js?00032\"></script>");
        strhtml.Append("<script type=\"text/javascript\">");
        strhtml.Append("(function () {");
        strhtml.Append("var config = {");
        strhtml.Append("guessingTitle: '" + guessingData.Title + "',");
        strhtml.Append("option1Text: '" + guessingData.Options[0].Text + "',");
        strhtml.Append("option2Text: '" + guessingData.Options[1].Text + "',");
        strhtml.Append("deadline: new Date('" + guessingData.Deadline.ToString("yyyy-MM-ddTHH:mm:ss") + "'),");
        strhtml.Append("isClosed: " + guessingData.IsClosed.ToString().ToLower() + ",");
        strhtml.Append("userBetOption: '" + (userBet != null ? guessingData.Options[userBet.OptionId - 1].Text : "") + "',");
        strhtml.Append("winningOption: '" + (WinningOptionId.HasValue ? guessingData.Options[(int)(WinningOptionId.Value - 1)].Text : "") + "',");
        strhtml.Append("totalAmount: " + totalAmount); // 添加这行，确保 totalAmount 变量已在后端定义
        strhtml.Append("};");
        strhtml.Append("if (document.readyState === 'loading') {");
        strhtml.Append("document.addEventListener('DOMContentLoaded', function() {");
        strhtml.Append("initBetting(config);");
        strhtml.Append("});");
        strhtml.Append("} else {");
        strhtml.Append("initBetting(config);");
        strhtml.Append("}");
        strhtml.Append("})();");
        strhtml.Append("</script>");
    }

    //显示投票选项
    if (bookVo.isVote == 1)
    {
        strhtml.Append("<div class=\"toupiao\">");
        strhtml.Append("投票选项：<br/>");
        for (int i = 0; (vlistVo != null && i < vlistVo.Count); i++)
        {
            strhtml.Append("<span class='vote" + (i + 1) + "'>[<a  href=\"" + http_start + "bbs/book_view_toVote.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;vid=" + vlistVo[i].vid + "&amp;vpage=" + CurrentPage + "&amp;lpage=" + lpage + "&amp;id=" + id + "\" data-vote-index='" + (i + 1) + "'>投</a>] " + vlistVo[i].voteTitle + " (<span class='VON" + (i + 1) + "'>" + vlistVo[i].voteClick + "</span>)</span><br/>");
        }
        strhtml.Append("</div>");

        //投票免刷新脚本
        strhtml.Append("<script type=\"text/javascript\" src=\"/css/js/bbs/Vote.js\"></script>");
    }
    //显示结束或锁定
    if (bookVo.whylock != "")
    {
        strhtml.Append("<div class=\"tipmini\">");
        bookVo.whylock = bookVo.whylock.Replace("<br/>", "|");
        string[] arry = bookVo.whylock.Split('|');
        bookVo.whylock = arry[0];
        strhtml.Append(bookVo.whylock);
        strhtml.Append("");
        if (arry.Length > 2)
        {
            //strhtml.Append("<br/><a href=\"" + this.http_start + "bbs/Book_View_log.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\">更多&gt;&gt;</a>");
        }
        strhtml.Append("</div>");
    }
    //显示楼主信息
    strhtml.Append("<div class=\"louzhuxinxi subtitle\">");
    strhtml.Append("<span class='louzhu'>[楼主]<span style='padding-right:2px;'></span><span class='louzhunicheng'><a href=\"" + this.http_start + "bbs/userinfo.aspx?touserid=" + bookVo.book_pub + "\">" + WapTool.GetColorNickName(toUserVo.idname, toUserVo.nickname, lang, ver) + "</a></span><span class='louzhudengji' style='color:#3d68a8;font-size:15px;padding-right:1px;'>(<span class='dengji'>" + WapTool.GetLevl(siteVo.lvlNumer, toUserVo.expr, toUserVo.money, type) + "</span><span style='display: none' class='touxian'>" + WapTool.GetHandle(siteVo.lvlNumer, toUserVo.expr, toUserVo.money, type) + "</span>)</span><span class='online'>" + WapTool.GetOnline(http_start, toUserVo.isonline, toUserVo.sex.ToString()) + WapTool.GetOLtimePic(this.http_start, siteVo.lvlTimeImg, toUserVo.LoginTimes) + "</span><br/></span>");
    strhtml.Append("<span class='xunzhang'><a style='padding: 0; color: #000000;padding-right:5px;text-decoration: none;' class='rongyuwenzi'>[荣誉]</a><span class='xunzhangtupian'>" + WapTool.GetMedal(toUserVo.userid.ToString(), toUserVo.moneyname, WapTool.GetSiteDefault(siteVo.Version, 47), wmlVo) + "</span><br/></span>");
    //会员可见
    if (this.IsCheckManagerLvl("|00|01|02|03|04|", "") == true)
    {
        strhtml.Append("[操作]<span style='padding-right:2px;'></span><a href=\"" + this.http_start + "bbs/Book_View_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\">管理</a> <a href=\"" + this.http_start + "bbs/Report_add.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;page=" + this.lpage + "&amp;id=" + this.id + "\">举报</a> <a onclick=\"return confirm('确定要收藏吗？');\" href=\"" + this.http_start + "bbs/Share.aspx?action=fav&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "\">收藏</a> ");
        strhtml.Append("<a onclick=\"window.scrollTo(0, 0); return false;\" href=\"" + this.http_start + "bbs/SendMoney_freeMain.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;touserid=" + bookVo.book_pub + "&amp;siteid=" + this.siteid + "\" class=\"showReward\">打赏</a><br/>");
        strhtml.Append("<span class='qianming'>[签名]<span style='padding-right:4px;'></span><span class='qianmingneirong'><u>" + toUserVo.remark + "</u></span><br/></span>");

        //打赏弹窗内容
        strhtml.Append("<div id='RewardCoins' class='overlay light'><a class='cancel' href='#'></a><div class='popup'><section class='aui-flexView'><header class='aui-navBar'>打赏楼主 <a class='close'>×</a></header><div class='aui-recharge-box'><div class='aui-cell-box'><form name='send' action='/bbs/sendmoney_freeMain.aspx' method='post'><div class='tip'></div><p class='info-text'>余额<span id=\"balance\" class=\"space\">" + userVo.money + "</span><span class='space'></span>妖晶</p><div class='info-text' id='type-amount'>请选择打赏数量</div><div class='aui-grids'><button type='button' class='aui-grids-item this-card' value='101'><span>100</span></button><button type='button' class='aui-grids-item' value='303'><span>300</span></button><button type='button' class='aui-grids-item' value='525'><span>520</span></button><button type='button' class='aui-grids-item' value='673'><span>666</span></button><button type='button' class='aui-grids-item' value='2357'><span>2333</span></button><button type='button' class='aui-grids-item' value='5050'><span>5000</span></button><button type='button' class='aui-grids-item' value='6733'><span>6666</span></button><button type='button' class='aui-grids-item' value='8978'><span>8888</span></button><button type='button' class='aui-grids-item' value='10101'><span>10000</span></button></div>");
        strhtml.Append("<input type=\"hidden\" name=\"sendmoney\" value=\"101\" /><input type=\"hidden\" name=\"action\" value=\"gomod\" /><input type=\"hidden\" name=\"id\" value=\"" + id + "\" /><input type=\"hidden\" name=\"classid\" value=\"" + classid + "\" /><input type=\"hidden\" name=\"siteid\" value=\"" + siteid + "\" /><input type=\"hidden\" name=\"touserid\" value=\"" + bookVo.book_pub + "\" /><input type=\"hidden\" name=\"myuserid\" value=\"" + userVo.userid + "\" /><button type=\"submit\" name=\"s\" class=\"givebtn\">确定打赏</button>");
        strhtml.Append("</form></div></div></section></div></div></div>");

        //打赏弹窗脚本、CSS
        strhtml.Append("<script type='text/javascript'>document.addEventListener('DOMContentLoaded', function() {var buttons = document.querySelectorAll('.aui-grids-item');var typeAmount = document.getElementById('type-amount');var sendMoneyInput = document.querySelector(\"input[name='sendmoney']\");buttons.forEach(function(button) {button.addEventListener('click', function() {buttons.forEach(function(b) { b.classList.remove('this-card'); });this.classList.add('this-card');var displayAmount = this.querySelector('span').textContent;var sendAmount = this.getAttribute('value');typeAmount.innerHTML = '打赏<span id=\"bounty\" class=\"space\">' + displayAmount + '</span><span class=\"space\"></span>妖晶';sendMoneyInput.value = sendAmount;});});});</script>");
        strhtml.Append("</div>");

        //显示上一页下一页
        strhtml.Append(preNextTitle);
        //显示最新回复
        strhtml.Append("<div class=\"viewContent\">");
        if (bookVo.islock == 0)
        {
            strhtml.Append("<form name=\"f\" class=\"custom-form\" action=\"" + http_start + "bbs/book_re.aspx" + "\" method=\"post\">");
            //strhtml.Append("<hr/>");
            //表情下拉开始
            strhtml.Append("<span class='newselect'>");
            strhtml.Append("<ul id='faceselect' class='ulselect'>");
            strhtml.Append("<li>");
            strhtml.Append("<input class='select_close' type='radio' name='face' id='emotion-close' value='' />");
            strhtml.Append("<span class='select_label select_label-placeholder'>表情</span>");
            strhtml.Append("</li>");
            strhtml.Append("<li class='select_items'>");
            strhtml.Append("<input class='select_expand' type='radio' name='face' id='emotion-opener' />");
            strhtml.Append("<label class='select_closeLabel' for='emotion-close'></label>");
            strhtml.Append("<ul class='select_options'>");
            for (int i = 0; (facelistImg != null && i < facelistImg.Length); i++)
            {
                strhtml.Append("<li class='select_option'>");
                strhtml.Append("<input class='select_input' type='radio' name='face' id='emotion-" + i + "' value='" + facelistImg[i] + "' />");
                strhtml.Append("<label class='select_label' for='emotion-" + i + "'>" + facelist[i] + "</label>");
                strhtml.Append("</li>");
            }
            strhtml.Append("</ul>");
            strhtml.Append("<label class='select_expandLabel' for='emotion-opener'></label>");
            strhtml.Append("</li>");
            strhtml.Append("</ul>");
            strhtml.Append("<span class='tongzhi'><input class=\"inp-cbx\" id=\"cbx\" type=\"checkbox\" name=\"sendmsg\" value=\"1\" style=\"display: none\"/><label class=\"cbx\" for=\"cbx\"><span><svg width=\"12px\" height=\"10px\" viewbox=\"0 0 12 10\"><polyline points=\"1.5 6 4.5 9 10.5 1\"></polyline></svg></span><span>通知楼主</span></label></span>");
            strhtml.Append("<div id=\"emoticon-container\" class=\"emoticon-popup\" style=\"display: none;\"></div>");
            strhtml.Append("</span>");
            //表情下拉结束

            //回复文本框
            strhtml.Append("<div class='centered-container'>");
            strhtml.Append("<textarea class=\"retextarea\" name=\"content\" minlength=\"1\" maxlength=\"5000\" required=\"required\" placeholder=\"请不要乱打字回复，以免被加黑\" rows=\"5\" style=\"width:98.6%;margin-bottom:5px;\">" + this.reShowInfo + "</textarea>");
            strhtml.Append("</div>");
            strhtml.Append("<input type=\"hidden\" name=\"action\" value=\"add\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"id\" value=\"" + id + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"siteid\" value=\"" + siteid + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"lpage\" value=\"" + lpage + "\"/>");
            strhtml.Append("<input type=\"hidden\"  name=\"classid\" value=\"" + classid + "\"/>");
            strhtml.Append("<span class='kuaisuhuifu'><input type=\"submit\" name=\"g\"  style=\"transform: translateY(-5%); margin-right: 2px;\" value=\"快速回复\"/> <a href=\"" + this.http_start + "bbs/book_re_addfile.aspx?action=class&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "\" style=\"font-size:13px;\">" + this.GetLang("文件回帖|文件回帖|upload file") + "</a></span><br/>");
            strhtml.Append("</form>");
        }
        //显示回复列表
        strhtml.Append("<div style=\"padding: 7px;\" class=\"recontent\">");
        for (int i = 0; (relistVo != null && i < relistVo.Count && i < 30); i++)
        {
            if (relistVo[i].book_top == 1)
            {
                strhtml.Append("<div class=\"reline list-reply\">");
                strhtml.Append("<span class='dinglouwenzi'>[<span class=\"floornumber0\">顶楼</span>]</span>");
            }
            else
            {
                if (bookVo.book_re - k == 1)
                {
                    strhtml.Append("<div class=\"reline list-reply\">");
                    strhtml.Append("[<span class=\"floornumber1\">沙发</span>]");
                }
                else if (bookVo.book_re - k == 2)
                {
                    strhtml.Append("<div class=\"reline list-reply\">");
                    strhtml.Append("[<span class=\"floornumber2\">椅子</span>]");
                }
                else if (bookVo.book_re - k == 3)
                {
                    strhtml.Append("<div class=\"reline list-reply\">");
                    strhtml.Append("[<span class=\"floornumber3\">板凳</span>]");
                }
                else
                {
                    strhtml.Append("<div class=\"reline list-reply\">[<span class=\"floornumber\">" + (bookVo.book_re - k) + "");
                    strhtml.Append("</span><span>楼</span>]");
                }
            }

            if (relistVo[i].myGetMoney > 0)
            {
                strhtml.Append("<span class=\"remoney\">[<b>得金<span class=\"rewardnumber\">" + relistVo[i].myGetMoney + "</span></b>]</span>");
            }

            if (this.userid != relistVo[i].userid.ToString())
            {
                strhtml.Append("[<a class=\"replyicon\" href=\"" + this.http_start + "bbs/Book_re.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;vpage=" + this.CurrentPage + "&amp;reply=" + (bookVo.book_re - k) + "&amp;id=" + this.id + "&amp;touserid=" + relistVo[i].userid + "\">回</a>]");
            }

            if (this.userid == relistVo[i].userid.ToString())
            {
                strhtml.Append("[<a class=\"replyme\" href=\"" + this.http_start + "bbs/Book_re.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;vpage=" + this.CurrentPage + "&amp;reply=" + (bookVo.book_re - k) + "&amp;id=" + this.id + "&amp;touserid=" + relistVo[i].userid + "\">回</a>]");
            }

            if (this.userid == bookVo.book_pub && bookVo.sendMoney > 0 && bookVo.sendMoney != bookVo.hasMoney && bookVo.book_pub != relistVo[i].userid.ToString())
            {
                strhtml.Append("<span class=\"remanage\">[<a class=\"giveicon\" href=\"" + this.http_start + "bbs/SendMoney.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;reid=" + relistVo[i].id + "&amp;siteid=" + this.siteid + "\">赏分</a>]</span>");
            }


            if (this.IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername))
            {
                strhtml.Append("<span style='display:none' class=\"admin-remanage\">");
                // 判断是否帖子作者，生成不同的删除链接
                if (this.userid == relistVo[i].userid.ToString())
                {
                    strhtml.Append("[<a class='user-remanage delete-myfloor' href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">删</a>]");
                    strhtml.Append("[<a class='floordeladmin drop-down' href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">删</a>]");
                }
                else
                {
                    strhtml.Append("[<a class='floordeladmin drop-down' href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">删</a>]");
                }
                // 生成管理员的其它操作链接
                strhtml.Append("[<a class='floorgive drop-down' href=\"" + this.http_start + "bbs/SendMoney_free.aspx?action=sendmoney&amp;classid=" + classid + "&amp;id=" + id + "&amp;reid=" + relistVo[i].id + "&amp;touserid=" + relistVo[i].userid + "&amp;siteid=" + this.siteid + "\">送</a>]");
                strhtml.Append("[<a class='floorchange drop-down' href=\"" + this.http_start + "bbs/Book_re_mod.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">审</a>]");
                if (relistVo[i].book_top == 1)
                {
                    strhtml.Append("[<a class='floortop drop-down' href=\"" + this.http_start + "bbs/Book_re_top.aspx?action=go&amp;tops=0&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">消顶</a>]</span>");
                }
                else
                {
                    strhtml.Append("[<a class='floortop drop-down' href=\"" + this.http_start + "bbs/Book_re_top.aspx?action=go&amp;tops=1&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">顶</a>]</span>");
                }
            }
            else if (this.userid == relistVo[i].userid.ToString())  //删除自己的帖子
            {
                strhtml.Append("<span class=\"user-remanage\">[<a class='delete-myfloor' href=\"" + this.http_start + "bbs/Book_re_del.aspx?action=go&amp;siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;lpage=" + this.lpage + "&amp;page=" + this.CurrentPage + "&amp;reid=" + relistVo[i].id + "&amp;id=" + this.id + "\">删</a>]</span>");
            }

            strhtml.Append("<span class=\"renick\"><a href=\"" + this.http_start + "bbs/userinfo.aspx?touserid=" + relistVo[i].userid + "\">" + ShowNickName_color(relistVo[i].userid, relistVo[i].nickname) + "</a></span><span style='display:none'>(<a class=\"reidlink\" href=\"" + this.http_start + "bbs/book_re.aspx?action=class&id=" + this.id + "&classid=" + this.classid + "&mainuserid=" + relistVo[i].userid + "\"><span class=\"renickid\">" + relistVo[i].userid + "</span>)</a></span></span>");

            if (relistVo[i].reply != 0)
            {
                strhtml.Append("<span class=\"reother\">回复" + relistVo[i].reply + "楼</span>");
            }
            strhtml.Append("<span class=\"recolon\">:</span>");
            strhtml.Append("<span class=\"retext\">");
            strhtml.Append(relistVo[i].content);
            if (relistVo[i].isdown > 0)
            {
                strhtml.Append("{<a href=\"" + this.http_start + "bbs/book_re_addfileshow.aspx?siteid=" + this.siteid + "&amp;classid=" + this.classid + "&amp;id=" + this.id + "&amp;reid=" + relistVo[i].id + "&amp;lpage=" + this.lpage + "\">查看" + relistVo[i].isdown + "个附件</a>}");
            }
            strhtml.Append("</span>");
            strhtml.Append("<span style=\"color: #404040; font-size: 15px; padding-left: 5px;\"><span class=\"redate\">(<span class=\"retime\">" + string.Format("{0:MM-dd HH:mm}", relistVo[i].redate) + "</span>)</span></span></div>");
            k = k + 1;
        }
        if (relistVo == null)
        {
            strhtml.Append("<span style=\"margin-left: 5px;opacity:0.7;\">暂时木有回复，快抢沙发哦！</span></div>");
        }
        else
        {
            strhtml.Append("</div>");
            strhtml.Append("<div class=\"more\">");
            strhtml.Append("<a class=\"noafter\" href=\"" + http_start + "bbs/book_re.aspx?lpage=" + this.lpage + "&amp;getTotal=" + bookVo.book_re + "&amp;id=" + id + "&amp;classid=" + classid + "&amp;siteid=" + siteid + "\">全部回帖(" + bookVo.book_re + ")</a>");
            strhtml.Append("</div>");
        }
        strhtml.Append("</div>");
        //全部回复结束

        //搜索弹窗iframe
        strhtml.Append("<div class=\"search-popup-overlay\"></div> <div class=\"search-popup\"> <iframe src=\"/bbs/book_search_new.aspx\" scrolling=\"no\" frameborder=\"0\"></iframe> </div>");
        //补充CSS样式
        strhtml.Append("<style>.sticky b {margin-left: 8px; margin-right: -2px; }.line1,.line2{line-height:40px;padding:8px 0px 10px 0;}</style>");

        //显示电脑版
        if (isWebHtml != "")
        {
            string strhtml_list = strhtml.ToString();
            int s = strhtml_list.IndexOf("<!--web-->");
            strhtml_list = strhtml_list.Substring(s, strhtml_list.Length - s);
            Response.Clear();
            Response.Write(WapTool.ToWML(isWebHtml.Replace("[view]", strhtml_list), wmlVo));
            Response.End();
        }
        //显示广告
        if (adVo.threeShowDown != "")
        {
            strhtml.Append(adVo.threeShowDown);
        }
        if (downLink != "")
        {
            strhtml.Append(downLink);
        }
        else
        {
        }
        //页面脚本合集
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/Shad@w.js\" defer></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/HyperLink.js?L16\" defer></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/MiniSet.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/SelEmoji.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/HideInfo.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/Reward.js?X\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/FastPost.js?X\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/NewReplyUI.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/AttachSearch.js\"></script>");
        strhtml.Append("<script type=\"text/javascript\" src=\"/NetCSS/JS/BookView/HideUseless.js?C1\" defer></script>");
        //会员可见结束
    }
    strhtml.Append(WapTool.GetVS(wmlVo));
    //输出错误
    strhtml.Append(ERROR);
    //输出
    Response.Write(WapTool.ToWML(strhtml.ToString(), wmlVo));
    Response.Write(WapTool.showDown(wmlVo));
%>