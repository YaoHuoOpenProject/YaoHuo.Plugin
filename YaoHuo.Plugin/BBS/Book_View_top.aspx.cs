﻿using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_top : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string tops = "";

        public string topType = ""; // 新增：用于区分普通置顶和全局置顶

        public wap_bbs_Model bookVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            tops = GetRequestValue("tops");
            topType = GetRequestValue("topType"); // 获取置顶类型

            // 根据置顶类型检查权限
            if (topType == "all")
            {
                CheckManagerLvl("04", "", "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            else
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }

            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
            bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
            if (bookVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bookVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bookVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bookVo.islock == 1L)
            {
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            else if (bookVo.islock == 2L)
            {
                ShowTipInfo("此帖已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            if (!(action == "gomod"))
            {
                return;
            }

            // 根据置顶类型清理缓存
            if (topType == "all" || bookVo.book_top == 2)
            {
                WapTool.ClearDataBBS("bbs" + siteid);
                WapTool.ClearDataBBS("bbsTop" + siteid);
            }
            else
            {
                WapTool.ClearDataBBS("bbs" + siteid + classid);
                WapTool.ClearDataTemp("bbsTop" + siteid + classid);
                WapTool.ClearDataBBS("bbsTop" + siteid + classid);
            }

            try
            {
                if (tops == "1")
                {
                    long targetTopValue = (topType == "all") ? 2L : 1L;
                    if (bookVo.book_top == targetTopValue)
                    {
                        INFO = "ERR";
                        return;
                    }
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSTOP" + id) < 0)
                    {
                        string topTypeText = (topType == "all") ? "全区置顶" : "置顶";
                        string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")" + topTypeText + "此帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                        text += bookVo.whylock;
                        MainBll.UpdateSQL("update wap_bbs set book_top=" + targetTopValue + ",whylock='" + text + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                        long moneyRegular = WapTool.getMoneyRegular(siteVo.moneyregular, 3);
                        long lvLRegular = WapTool.getLvLRegular(siteVo.lvlRegular, 3);
                        string book_title = bookVo.book_title;
                        book_title = book_title.Replace("[", "［");
                        book_title = book_title.Replace("]", "］");
                        if (moneyRegular > 0L || lvLRegular > 0L)
                        {
                            MainBll.UpdateSQL("update [user] set money=money+" + moneyRegular + ",expR=expR+" + lvLRegular + " where userid=" + bookVo.book_pub);
                            SaveBankLog(bookVo.book_pub.ToString(), "帖子" + topTypeText, moneyRegular.ToString(), userid, nickname, "帖子[ID:" + bookVo.id + "]");
                            MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题设为" + topTypeText + "，奖励:", moneyRegular, "个", siteVo.sitemoneyname, "，奖励经验：", lvLRegular, "','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                        }
                        MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + topTypeText + "用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "','" + IP + "')");
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "BBSTOP" + id + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                        INFO = "OK";
                    }
                    else
                    {
                        ShowTipInfo("今天您已操作过一次，请明天再来！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
                    }
                }
                else if (bookVo.book_top == 0L)
                {
                    INFO = "ERR";
                }
                else
                {
                    // 取消置顶操作
                    string topTypeText = (bookVo.book_top == 2L) ? "全区置顶" : "置顶";
                    string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")取消" + topTypeText + "此帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                    text += bookVo.whylock;
                    MainBll.UpdateSQL("update wap_bbs set book_top=0,whylock='" + text + "' where userid=" + siteid + " and  id=" + long.Parse(id));
                    string book_title = bookVo.book_title;
                    book_title = book_title.Replace("[", "［");
                    book_title = book_title.Replace("]", "］");
                    MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的一个主题取消" + topTypeText + "!','设置时间：", DateTime.Now, "[br]论坛主题:[url=/bbs/book_view.aspx?siteid=", siteid, "&amp;classid=", bookVo.book_classid, "&amp;id=", id, "]", book_title, "[/url]',", bookVo.book_pub, ",1)"));
                    MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "取消" + topTypeText + "用户ID:" + bookVo.book_pub + "发表的ID=" + id + "主题:" + book_title + "','" + IP + "')");
                    INFO = "OK";
                    if (topType == "all")
                    {
                        base.Application.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}