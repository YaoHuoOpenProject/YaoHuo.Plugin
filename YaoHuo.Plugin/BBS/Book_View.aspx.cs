using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_ShowPreNextTitle_bbs = PubConstant.GetAppString("KL_ShowPreNextTitle_bbs");

        public string id = "0";

        public sys_ad_show_Model adVo = new sys_ad_show_Model();

        public wap_bbs_Model bookVo = new wap_bbs_Model();

        public List<wap_bbsre_Model> relistVo = null;

        public List<wap_bbsre_Model> relistVoTop = null;

        public List<wap_bbs_vote_Model> vlistVo = null;

        public List<user_Model> userListVo_IDName = null;

        public string[] facelist;

        public string[] facelistImg;

        public string reShowInfo = "";

        public StringBuilder strhtml = new StringBuilder();

        public string lpage = "";

        public string content = "";

        public string view = "";

        public string viewLeave = "";

        public StringBuilder preNextTitle = new StringBuilder();

        public string ERROR = "";

        public int k = 0;

        public string stype = "";

        public string stypelink = "";

        public string threePageType = "";

        public string linkURL = "";

        public string http_start_url = "";

        public int totalPage = 0;

        public int pageSize = 1000;

        public int CurrentPage = 1;

        public bool isAdmin = false;

        public string state = "";

        public string type = "";

        public string showhead = "0";

        public user_Model toUserVo = null;

        public string downLink = "";

        public bool isNeedSecret = false;

        public string towidths = "";

        public string towidths_W = "";

        public string towidths_H = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            if (lpage == "")
            {
                lpage = "1";
            }
            view = GetRequestValue("view");
            viewLeave = GetRequestValue("viewleave");
            stype = GetRequestValue("stype");
            type = WapTool.GetSiteDefault(siteVo.Version, 27);
            showhead = WapTool.getArryString(classVo.smallimg, '|', 38);
            downLink = WapTool.getArryString(classVo.smallimg, '|', 20).Trim().Replace("[stype]", stype);
            threePageType = WapTool.getArryString(classVo.smallimg, '|', 23);
            if (!WapTool.IsNumeric(threePageType))
            {
                threePageType = "1";
            }
            if (WapTool.IsNumeric(stype))
            {
                stypelink = "&amp;stype=" + stype;
            }
            if (!WapTool.IsNumeric(id))
            {
                ShowTipInfo("帖子ID参数错误！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + stypelink);
            }
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 0)))
            {
                ShowTipInfo("此栏目已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            towidths_W = WapTool.getArryString(classVo.smallimg, '|', 45);
            towidths_H = WapTool.getArryString(classVo.smallimg, '|', 46);
            if (!WapTool.IsNumeric(towidths_W))
            {
                towidths_W = "0";
            }
            if (!WapTool.IsNumeric(towidths_H))
            {
                towidths_H = "0";
            }
            if (towidths_W == "0" && towidths_H == "0")
            {
                towidths = " width='320px' height='100%' ";
            }
            else if (towidths_W != "0" && towidths_H == "0")
            {
                towidths = " width='" + towidths_W + "px' ";
            }
            else if (towidths_W == "0" && towidths_H != "0")
            {
                towidths = "  height='" + towidths_H + "px' ";
            }
            else
            {
                towidths = " width='" + towidths_W + "px' height='" + towidths_H + "px' ";
            }
            if (classVo.topicID != "" && classVo.topicID != "0" && IsCheckManagerLvl("|00|01|03|04|", ""))
            {
                isNeedSecret = true;
            }
            isAdmin = IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername);
            CheckUserViewSubMoney("BBS" + id, GetUrlQueryString(), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + stypelink);
            pageSize = Convert.ToInt32(userVo.MaxPerPage_Content);
            if (pageSize < 100)
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Content);
            }
            if (pageSize < 100)
            {
                pageSize = 100;
            }
            if (GetRequestValue("vpage") != "")
            {
                CurrentPage = int.Parse(GetRequestValue("vpage"));
                if (CurrentPage < 1)
                {
                    CurrentPage = 1;
                }
            }
            CheckFunction("bbs", CurrentPage);
            try
            {
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                state = GetRequestValue("state");
                if (state != "")
                {
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSH" + id) < 0)
                    {
                        wap_bbs_BLL.UpdateXiNuHan(siteid, id, state);
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "BBSH" + id + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    }
                }
                if (CurrentPage == 1)
                {
                    wap_bbs_BLL.UpdateXiNuHan(siteid, id, "0");
                }
                string backurl = "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + stypelink;
                bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
                if (bookVo == null)
                {
                    ShowTipInfo("可能已删除！或不存在！或已转移至历史表中，点击此<a href='" + http_start + "bbs/book_view_bak.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;sid=" + a + "'>尝试访问历史数据</a>。", backurl);
                }
                else if (bookVo.ischeck == 1L)
                {
                    ShowTipInfo("正在审核中！", backurl);
                }
                else if (bookVo.book_classid.ToString() != classid)
                {
                    ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
                }
                else if (bookVo.ischeck == 2L)
                {
                    CheckManagerLvl("04", classVo.adminusername, "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
                }
                bookVo.book_title = WapTool.GetShowImg(bookVo.book_title, "200", "bbs");
                wmlVo.title = bookVo.book_title;
                wmlVo.id = bookVo.id;
                user_BLL user_BLL = new user_BLL(a);
                toUserVo = user_BLL.getUserInfo(bookVo.book_pub, siteid);
                if (toUserVo == null)
                {
                    toUserVo = user_BLL.getUserInfo(siteid, siteid);
                    toUserVo.nickname = "游客";
                    toUserVo.city = "火星";
                    toUserVo.userid = 0L;
                    toUserVo.remark = "";
                }
                http_start_url = http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + stypelink;
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                if (bookVo.MarkSixBetID != 0L)
                {
                    wap_bbs_MarkSix_bet_BLL wap_bbs_MarkSix_bet_BLL = new wap_bbs_MarkSix_bet_BLL(a);
                    wap_bbs_MarkSix_bet_Model wap_bbs_MarkSix_bet_Model = new wap_bbs_MarkSix_bet_Model();
                    wap_bbs_MarkSix_bet_Model = wap_bbs_MarkSix_bet_BLL.GetModel(long.Parse(siteid), bookVo.MarkSixBetID);
                    if (wap_bbs_MarkSix_bet_Model != null)
                    {
                        string text = "";
                        string text2 = "";
                        text = "【";
                        text = text + "<a href='" + http_start + "bbs/marksix/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;sid=" + a + "'>查看</a>";
                        if (bookVo.userid.ToString() == userid)
                        {
                            text = text + ".<a href='" + http_start + "bbs/marksix/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;bbsid=" + id + "&amp;id=" + wap_bbs_MarkSix_bet_Model.id + "'>修改</a>.<a href='" + http_start + "bbs/marksix/book_user_win.aspx?touserid=" + userid + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;sid=" + a + "'>兑奖</a>";
                        }
                        text += "】";
                        if (bookVo.MarkSixWin == 1L)
                        {
                            text2 = "<img src='/NetImages/zhong.gif'alt='中'> ";
                        }
                        bookVo.book_content = text2 + wap_bbs_MarkSix_bet_Model.peroid + "期投注[" + GamesClassManager.Tool.GetMarkSix_PlayName(wap_bbs_MarkSix_bet_Model.types.ToString()) + "]为" + wap_bbs_MarkSix_bet_Model.types_content + " 共" + wap_bbs_MarkSix_bet_Model.num + "注" + text + "<br/>----------<br/>" + bookVo.book_content;
                    }
                    else
                    {
                        bookVo.book_content = "投注记录已删除<br/>----------<br/>" + bookVo.book_content;
                    }
                }
                content = bookVo.book_content;
                content = content.Replace("[id]", id);
                if (view != "all")
                {
                    if (content.IndexOf("[next]") > 0)
                    {
                        content = content.Replace("[next]", "\uff3e");
                        string[] array = content.Split('\uff3e');
                        totalPage = array.Length;
                        if (array[totalPage - 1] == "")
                        {
                            totalPage--;
                        }
                        if (viewLeave != "")
                        {
                            if (int.Parse(viewLeave) < totalPage)
                            {
                                string text3 = "";
                                for (int i = int.Parse(viewLeave); i < totalPage; i++)
                                {
                                    text3 += array[i];
                                }
                                content = text3;
                            }
                        }
                        else
                        {
                            content = array[CurrentPage - 1];
                        }
                    }
                    else
                    {
                        try
                        {
                            totalPage = content.Length / pageSize;
                            if (content.Length > totalPage * pageSize)
                            {
                                totalPage++;
                            }
                            if (viewLeave != "")
                            {
                                if (Convert.ToInt32(viewLeave) * pageSize < content.Length)
                                {
                                    content = content.Substring(Convert.ToInt32(viewLeave) * pageSize, content.Length - Convert.ToInt32(viewLeave) * pageSize);
                                }
                            }
                            else if (CurrentPage > totalPage)
                            {
                                content = "";
                            }
                            else if (totalPage > 1 && CurrentPage >= totalPage)
                            {
                                CurrentPage = totalPage;
                                content = content.Substring((CurrentPage - 1) * pageSize, content.Length - (CurrentPage - 1) * pageSize);
                            }
                            else if (totalPage > 1 && CurrentPage < totalPage)
                            {
                                content = content.Substring((CurrentPage - 1) * pageSize, pageSize);
                            }
                        }
                        catch (Exception ex)
                        {
                            content += WapTool.ErrorToString(ex.ToString());
                        }
                    }
                    if (content != "")
                    {
                        content = "<!--listS-->" + content + "<!--listE-->";
                    }
                    content += "<span id='KL_show_next_list'></span>";
                    linkURL = WapTool.GetPageContentLink(ver, lang, totalPage, pageSize, CurrentPage, http_start_url + "&amp;id=" + id);
                }
                if (bookVo.isdown > 0L)
                {
                    string text4 = "";
                    string text5 = WapTool.getArryString(classVo.smallimg, '|', 17);
                    string text6 = WapTool.getArryString(classVo.smallimg, '|', 18);
                    if (!WapTool.IsNumeric(text5))
                    {
                        text5 = "0";
                    }
                    if (!WapTool.IsNumeric(text6))
                    {
                        text6 = "0";
                    }
                    if (long.Parse(text5) > 0L)
                    {
                        text4 = "扣" + text5 + "个" + siteVo.sitemoneyname;
                    }
                    if (long.Parse(text6) > 0L)
                    {
                        if (text4 != "")
                        {
                            text4 += "/";
                        }
                        text4 = text4 + "送" + text6 + "个" + siteVo.sitemoneyname;
                    }
                    if (text4 != "")
                    {
                        text4 = "(" + text4 + ")";
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    List<wap2_attachment_Model> list2 = null;
                    wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(a);
                    list2 = wap2_attachment_BLL.GetListVo(" book_type='bbs' and book_id=" + long.Parse(id));
                    if (list2 != null)
                    {
                        stringBuilder.Append("<div class='attachment'>");
                        stringBuilder.Append("<span class='attachmenSum'>");
                        stringBuilder.Append("<span class='attachmentext'>共有</span>");
                        stringBuilder.Append("<span class='attachmentlistnum'>" + list2.Count + "</span>");
                        stringBuilder.Append("<span class='attachmentext'>个附件</span>");
                        stringBuilder.Append("<span class='attachmentCharge'>" + text4 + "</span>");
                        stringBuilder.Append("</span>");
                    }
                    string text7 = WapTool.getArryString(classVo.smallimg, '|', 33);
                    if (!WapTool.IsNumeric(text7))
                    {
                        text7 = "2";
                    }
                    if (text7 == "0")
                    {
                        text7 = "1000";
                    }
                    int i = 0;
                    while (list2 != null && i < list2.Count && i <= int.Parse(text7) - 1)
                    {
                        stringBuilder.Append("<div class='attachmentinfo'><span class=\"downloadname\"><span class=\"attachmentnumber\">");
                        stringBuilder.Append(i + 1 + ".");
                        stringBuilder.Append("</span><span class='attachmentname'><span class='attachmentitle'>");
                        stringBuilder.Append(list2[i].book_title);
                        stringBuilder.Append("</span>");
                        if (list2[i].book_ext.Trim() != "" && list2[i].book_ext.Trim() != "mov")
                        {
                            stringBuilder.Append("<span class=\"FileExtension\">");
                            stringBuilder.Append("." + list2[i].book_ext);
                            stringBuilder.Append("</span></span>");
                        }
                        if (list2[i].book_size.Trim() != "")
                        {
                            stringBuilder.Append("<span class=\"attachmentsize\">");
                            stringBuilder.Append("(" + list2[i].book_size + ")");
                            stringBuilder.Append("</span></span>");
                        }
                        if (list2[i].book_ext.Trim() != "" && ".gif|.jpg|.jpeg|.png|.webp|.bmp|".IndexOf(list2[i].book_ext.ToLower()) >= 0)
                        {
                            string text8 = http_start + "bbs/" + list2[i].book_file;
                            if (list2[i].book_file.ToLower().StartsWith("http"))
                            {
                                text8 = list2[i].book_file;
                            }
                            stringBuilder.Append("<span class=\"attachmentimage\">");
                            //stringBuilder.Append("<a href='" + http_start + "bbs/picDIY.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;path=" + HttpUtility.UrlEncode("bbs\\" + list2[i].book_file) + "'><img src='" + text8 + "' " + towidths + "/></a>");
                            stringBuilder.Append("<a href='" + http_start + "bbs/" + HttpUtility.UrlDecode(list2[i].book_file) + "'>");
                            //stringBuilder.Append("<img src='" + text8 + "' " + towidths + "/></a>");
                            stringBuilder.Append("<img src='" + text8 + "' referrerpolicy='no-referrer'/></a>");
                            stringBuilder.Append("</span>");
                        }
                        else if (list2[i].book_ext.Trim() != "" && ("mov|flv|m3u8|mp4").IndexOf(list2[i].book_ext.ToLower()) >= 0)
                        {
                            string fileExt = WapTool.right(list2[i].book_file.ToLower(), 3);
                            if (("mov|flv|m3u8|mp4").IndexOf(fileExt) >= 0)
                            {
                                stringBuilder.Append("<span class=\"videoplay\"><video onclick='if(this.paused) { this.play();}else{ this.pause();}' src='" + list2[i].book_file + "' autobuffer='true' width='100%' height='100%' poster='/NetImages/play.gif' controls>{不支持在线播放，请更换浏览器}</video>");
                            }
                            else
                            {
                                stringBuilder.Append("</span><span class=\"downloadlink\"><span class=\"downloadurl\"><a class='urlbtn'  href='" + http_start + "bbs/download.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;book_id=" + id + "&amp;id=" + list2[i].ID + "&amp;RndPath=" + siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(list2[i].book_title) + "." + list2[i].book_ext + "'>点击下载</a></span><span class=\"downloadcount\">(" + list2[i].book_click + "次)</span></span>");
                            }
                        }
                        else
                        {
                            stringBuilder.Append("</span><span class=\"downloadlink\"><span class=\"downloadurl\"><a class='urlbtn'  href='" + http_start + "bbs/download.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;book_id=" + id + "&amp;id=" + list2[i].ID + "&amp;RndPath=" + siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(list2[i].book_title) + "." + list2[i].book_ext + "'>点击下载</a></span><span class=\"downloadcount\">(" + list2[i].book_click + "次)</span></span>");
                        }
                        stringBuilder.Append("<span class=\"attachmentNote\">");
                        stringBuilder.Append(list2[i].book_content + "");
                        stringBuilder.Append("</span>");
                        stringBuilder.Append("</div>");
                        i++;
                    }
                    if (list2 != null && list2.Count > int.Parse(text7) - 1)
                    {
                        stringBuilder.Append("<div class='btBox'><div class='bt1'>");
                        stringBuilder.Append("<a href='" + http_start + "bbs/book_view_showfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + stypelink + "'>{查看所有附件}</a> ");
                        stringBuilder.Append("</div></div>");
                    }
                    content += stringBuilder.ToString();
                }
                string text9 = "";
                if (content.IndexOf("[go]") > 0)
                {
                    content = content.Replace("[go]", "\uff3e");
                    text9 = content.Split('\uff3e')[0] + "<br/>";
                }
                if (bookVo.viewtype == 1L)
                {
                    if (userid == "0")
                    {
                        content = text9 + "{此处内容需要登录才能查看}";
                    }
                }
                else if (bookVo.viewtype == 2L && !isAdmin)
                {
                    if (!WapTool.isAllowUA(UA) && !WapTool.isAllowIP(IP) && userid != bookVo.book_pub)
                    {
                        content = text9 + "{此处内容需要手机访问才能查看，当前UA+IP:" + UA + " " + IP + "}";
                    }
                }
                else if (bookVo.viewtype == 3L && userid != bookVo.book_pub && !isAdmin)
                {
                    if (!WapTool.isHasReply(siteid, userid, id) && !CheckManagerLvl("04", classVo.adminusername))
                    {
                        content = text9 + "{此处内容需要回复才能查看}";
                    }
                }
                else if (bookVo.viewtype == 4L && userid != bookVo.book_pub && !isAdmin)
                {
                    if (userVo.money < bookVo.viewmoney)
                    {
                        content = text9 + "{此处内容需要您拥有" + siteVo.sitemoneyname + ":" + bookVo.viewmoney + "才能查看}";
                    }
                }
                else if (bookVo.viewtype == 5L && userid != bookVo.book_pub && !isAdmin)
                {
                    if (userVo.expr < bookVo.viewmoney)
                    {
                        content = text9 + "{此处内容需要您拥有经验:" + bookVo.viewmoney + "才能查看}";
                    }
                }
                else if (bookVo.viewtype == 6L && userid != bookVo.book_pub && !isAdmin)
                {
                    string text10 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (!WapTool.IsNumeric(text10))
                    {
                        text10 = "0";
                    }
                    if (long.Parse(text10) < 2L)
                    {
                        text10 = "1000";
                    }
                    if (bookVo.viewmoney > long.Parse(text10))
                    {
                        bookVo.viewmoney = long.Parse(text10);
                    }
                    if (bookVo.viewmoney < 0L)
                    {
                        bookVo.viewmoney = 0L;
                    }
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSNEED" + id) < 0 && GetRequestValue("buy") == "yes" && userVo.money >= bookVo.viewmoney)
                    {
                        needPassWordToAdmin();
                        fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSNEED" + id;
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                        MainBll.UpdateSQL("update [user] set money=money-" + bookVo.viewmoney + " where userid=" + userVo.userid);
                        MainBll.UpdateSQL("update [user] set money=money+" + bookVo.viewmoney + " where userid=" + bookVo.book_pub);
                        SaveBankLog(userid.ToString(), "购买帖子", "-" + bookVo.viewmoney, userid, nickname, "购买帖子[ID:" + id + "]");
                        SaveBankLog(bookVo.book_pub.ToString(), "帖子赚币", bookVo.viewmoney.ToString(), userid, nickname, "操作人购买你的帖子[ID:" + id + "]");
                    }
                    if (fcountSubMoneyFlag.IndexOf("BBSNEED" + id) < 0)
                    {
                        content = text9 + "{此处内容需要支付" + siteVo.sitemoneyname + ":" + bookVo.viewmoney + "才能查看，";
                        if (userid == "0")
                        {
                            content = content + "请先<a href='" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "'>登录网站</a>！}";
                        }
                        else if (userVo.money < bookVo.viewmoney)
                        {
                            content = content + "你只有:" + userVo.money + "，不能购买！}";
                        }
                        else
                        {
                            content = content + "<a href='" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes'>确认支付</a>}";
                        }
                    }
                }
                else if (bookVo.viewtype == 7L && userid != bookVo.book_pub && !isAdmin)
                {
                    string text10 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (!WapTool.IsNumeric(text10))
                    {
                        text10 = "0";
                    }
                    if (long.Parse(text10) < 2L)
                    {
                        text10 = "1000";
                    }
                    if (bookVo.viewmoney > long.Parse(text10))
                    {
                        bookVo.viewmoney = long.Parse(text10);
                    }
                    if (bookVo.viewmoney < 0L)
                    {
                        bookVo.viewmoney = 0L;
                    }
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSNEEDRMB" + id) < 0 && GetRequestValue("buy") == "yes" && userVo.RMB >= (decimal)bookVo.viewmoney)
                    {
                        needPassWordToAdmin();
                        fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSNEEDRMB" + id;
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                        MainBll.UpdateSQL("update [user] set rmb=rmb-" + bookVo.viewmoney + " where userid=" + userVo.userid);
                        MainBll.UpdateSQL("update [user] set rmb=rmb+" + bookVo.viewmoney + " where userid=" + bookVo.book_pub);
                        string orderID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + userid;
                        SaveRMBLog(userid, "-5", "-" + bookVo.viewmoney, userid, nickname, "购买论坛内容[" + id + "]", orderID);
                        SaveRMBLog(bookVo.book_pub, "5", bookVo.viewmoney.ToString(), userid, nickname, "操作人购买你的论坛内容[" + id + "]", orderID);
                    }
                    if (fcountSubMoneyFlag.IndexOf("BBSNEEDRMB" + id) < 0)
                    {
                        content = text9 + "{此处内容需要支付RMB￥:" + bookVo.viewmoney + "才能查看，";
                        if (userid == "0")
                        {
                            content = content + "请先<a href='" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "'>登录网站</a>！}";
                        }
                        else if (userVo.RMB < (decimal)bookVo.viewmoney)
                        {
                            content = content + "你只有￥:" + userVo.RMB.ToString("f2") + "，不能购买！<a href='" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid + "'>点击此在线充值</a>。}";
                        }
                        else
                        {
                            content = content + "<a href='" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes'>确认支付</a>}";
                        }
                    }
                }
                if (isAdmin)
                {
                    content = content.Replace("[/reply]", "［/reply(管理员查看中)］");
                    content = content.Replace("[/buy]", "［/buy(管理员查看中)］");
                    content = content.Replace("[/buyrmb]", "［/buyrmb(管理员查看中)］");
                    content = content.Replace("[/buyrmb]", "［/buyrmb(管理员查看中)］");
                }
                string text11 = "0";
                if (content.IndexOf("[/reply]") > 0)
                {
                    text11 = "1";
                }
                if (content.IndexOf("[/buy]") > 0)
                {
                    text11 = "2";
                }
                if (content.IndexOf("[/buyrmb]") > 0)
                {
                    text11 = "3";
                }
                if (text11 != "0")
                {
                    content = WapTool.ToWML(content, wmlVo);
                }
                if (text11 == "1")
                {
                    Regex regex = new Regex("(\\[reply\\])(.[^\\[]*)(\\[\\/reply\\])");
                    if (!WapTool.isHasReply(siteid, userid, id) && !CheckManagerLvl("04", classVo.adminusername) && userid != bookVo.book_pub)
                    {
                        content = regex.Replace(content, "{此处内容须回复后才能浏览}");
                    }
                    else
                    {
                        content = regex.Replace(content, "$2");
                    }
                }
                if (text11 == "2")
                {
                    Regex regex = new Regex("(\\[buy=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/buy\\])");
                    Match match = regex.Match(content);
                    string text12 = match.Groups[2].Value;
                    if (!WapTool.IsNumeric(text12))
                    {
                        text12 = "0";
                    }
                    string text10 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (!WapTool.IsNumeric(text10))
                    {
                        text10 = "0";
                    }
                    if (long.Parse(text10) < 2L)
                    {
                        text10 = "1000";
                    }
                    if (long.Parse(text12) > long.Parse(text10))
                    {
                        text12 = text10;
                    }
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSNEED" + id) < 0 && GetRequestValue("buy") == "yes" && userVo.money >= long.Parse(text12))
                    {
                        needPassWordToAdmin();
                        fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSNEED" + id;
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                        MainBll.UpdateSQL("update [user] set money=money-" + text12 + " where userid=" + userVo.userid);
                        MainBll.UpdateSQL("update [user] set money=money+" + text12 + " where userid=" + bookVo.book_pub);
                        SaveBankLog(userid.ToString(), "购买帖子", "-" + text12.ToString(), userid, nickname, "购买帖子[ID:" + id + "]");
                        SaveBankLog(bookVo.book_pub.ToString(), "帖子赚币", text12.ToString(), userid, nickname, "操作人购买你的帖子[ID:" + id + "]");
                    }
                    if (userid != bookVo.book_pub && fcountSubMoneyFlag.IndexOf("BBSNEED" + id) < 0)
                    {
                        string text13 = "{此处内容需要支付" + siteVo.sitemoneyname + ":" + text12 + "个才能浏览，";
                        content = regex.Replace(replacement: (userid == "0") ? (text13 + "请先<a href='" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "'>登录网站</a>！}") : ((userVo.money >= long.Parse(text12)) ? (text13 + "<a href='" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes'>确认支付</a>}") : (text13 + "你只有:" + userVo.money + "，不能购买！}")), input: content);
                    }
                    else
                    {
                        content = regex.Replace(content, "$3");
                    }
                }
                if (text11 == "3")
                {
                    Regex regex = new Regex("(\\[buyrmb=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/buyrmb\\])");
                    Match match = regex.Match(content);
                    string text12 = match.Groups[2].Value;
                    if (!WapTool.IsNumeric(text12))
                    {
                        text12 = "0";
                    }
                    string text10 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (!WapTool.IsNumeric(text10))
                    {
                        text10 = "0";
                    }
                    if (long.Parse(text10) < 2L)
                    {
                        text10 = "1000";
                    }
                    if (long.Parse(text12) > long.Parse(text10))
                    {
                        text12 = text10;
                    }
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSNEEDRMB" + id) < 0 && GetRequestValue("buy") == "yes" && userVo.RMB >= (decimal)long.Parse(text12))
                    {
                        needPassWordToAdmin();
                        fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSNEEDRMB" + id;
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                        MainBll.UpdateSQL("update [user] set rmb=rmb-" + text12 + " where userid=" + userVo.userid);
                        MainBll.UpdateSQL("update [user] set rmb=rmb+" + text12 + " where userid=" + bookVo.book_pub);
                        string orderID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + userid;
                        SaveRMBLog(userid, "-5", "-" + text12.ToString(), userid, nickname, "购买论坛内容[" + id + "]", orderID);
                        SaveRMBLog(bookVo.book_pub, "5", text12.ToString(), userid, nickname, "操作人购买你的论坛内容[" + id + "]", orderID);
                    }
                    if (userid != bookVo.book_pub && fcountSubMoneyFlag.IndexOf("BBSNEEDRMB" + id) < 0)
                    {
                        string text13 = "{此处内容需要支付RMB￥:" + text12 + "个才能浏览，";
                        content = regex.Replace(replacement: (userid == "0") ? (text13 + "请先<a href='" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;sid=" + a + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "'>登录网站</a>！}") : ((!(userVo.RMB < (decimal)long.Parse(text12))) ? (text13 + "<a href='" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes'>确认支付</a>}") : (text13 + "你只有:" + userVo.RMB.ToString("f2") + "，不能购买！<a href='" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid + "'>点击此在线充值</a>。}")), input: content);
                    }
                    else
                    {
                        content = regex.Replace(content, "$3");
                    }
                }
                content = content.Replace("[next]", "");
                content = content.Replace("[go]", "");
                content = content.Replace("\uff3e", "");
                KL_ShowPreNextTitle_bbs = WapTool.GetSystemAndMyConfig(KL_ShowPreNextTitle_bbs, WapTool.getArryString(classVo.smallimg, '|', 15));
                if (!"1".Equals(KL_ShowPreNextTitle_bbs))
                {
                    string text2 = stype == "" ? classid : (classid + " and topic=" + stype);
                    preNextTitle = wap_bbs_BLL.GetPreNextTitle(ver, lang, http_start_url, siteid, text2, id, "desc");
                }
                else
                {
                    preNextTitle.Append("");
                }
                if ("1".Equals(WapTool.KL_OpenCache))
                {
                    WapTool.DataBBSReArray.TryGetValue("bbsRe" + siteid + id, out relistVo);
                }
                if (relistVo == null)
                {
                    wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
                    relistVoTop = wap_bbsre_BLL.GetListTopVo("  devid='" + siteid + "' and bookid=" + id + " and ischeck=0 and book_top=1 ", 1);
                    long num = 10L;
                    num = ((classVo.ismodel >= 1L) ? Convert.ToInt32(classVo.ismodel) : Convert.ToInt32(siteVo.MaxPerPage_Default));
                    relistVo = wap_bbsre_BLL.GetListVo(num, 1L, "  devid='" + siteid + "' and bookid=" + id + " and ischeck=0 and book_top=0 ", "*", "id", bookVo.book_re, 1);
                    int i = 0;
                    while (relistVoTop != null && i < relistVoTop.Count)
                    {
                        if (relistVo != null)
                        {
                            relistVo.Insert(0, relistVoTop[i]);
                            if (i > 10)
                            {
                                break;
                            }
                            i++;
                            continue;
                        }
                        relistVo = relistVoTop;
                        break;
                    }
                    if ("1".Equals(WapTool.KL_OpenCache))
                    {
                        try
                        {
                            WapTool.DataBBSReArray.Add("bbsRe" + siteid + id, relistVo);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                try
                {
                    if (classVo.bbsFace.IndexOf('_') < 0)
                    {
                        classVo.bbsFace = "_";
                    }
                    facelist = classVo.bbsFace.Split('_')[0].Split('|');
                    facelistImg = classVo.bbsFace.Split('_')[1].Split('|');
                    if (classVo.bbsType.IndexOf('_') < 0)
                    {
                        classVo.bbsType = "_";
                    }
                    string[] array2 = classVo.bbsType.Split('_')[1].Split('|');
                    Random random = new Random();
                    reShowInfo = array2[random.Next(0, array2.Length - 1)];
                }
                catch (Exception)
                {
                }
                if (bookVo.isVote == 1L)
                {
                    wap_bbs_vote_BLL wap_bbs_vote_BLL = new wap_bbs_vote_BLL(a);
                    vlistVo = wap_bbs_vote_BLL.GetListVo(100, 1, " siteid=" + siteid + " and id=" + id, "*", "vid", 100L, 0);
                }
                string siteDefault = WapTool.GetSiteDefault(siteVo.Version, 33);
                if (siteDefault != "1" && relistVo != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("siteid=" + siteid + " and userid in(");
                    int i = 0;
                    while (relistVo != null && i < relistVo.Count && i < 5)
                    {
                        stringBuilder.Append(relistVo[i].userid);
                        stringBuilder.Append(",");
                        i++;
                    }
                    stringBuilder.Append("0)");
                    userListVo_IDName = MainBll.GetUserListVo(stringBuilder.ToString());
                }
                VisiteCount("正在浏览帖子:<a href='" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "'>" + bookVo.book_title + "</a>");
                Action_user_doit(3);
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }

        public string ShowNickName_color(long userid, string nickname)
        {
            if (userListVo_IDName == null)
            {
                return nickname;
            }
            int num = 0;
            while (userListVo_IDName != null && num < userListVo_IDName.Count)
            {
                if (userListVo_IDName[num].userid != userid)
                {
                    num++;
                    continue;
                }
                nickname = WapTool.GetColorNickName(userListVo_IDName[num].idname, nickname, lang, ver, userListVo_IDName[num].endTime);
                break;
            }
            return nickname;
        }

        public void UpdateBbsReCount(string nowid)
        {
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
            long num = wap_bbsre_BLL.GetListCount(" devid='" + siteid + "' and bookid=" + nowid + " and ischeck=0");
            MainBll.UpdateSQL("update wap_bbs set book_re=" + num + " where id=" + nowid);
        }
    }
}