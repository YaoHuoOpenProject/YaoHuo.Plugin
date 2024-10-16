using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public partial class Book_View : MyPageWap
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

        //public string threePageType = "";

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

        protected GuessData guessingData;

        protected BetInfo userBet;

        // 新增属性
        public long? WinningOptionId { get; set; }
        public string WinningOptionText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
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
            //threePageType = WapTool.getArryString(classVo.smallimg, '|', 23);
            //if (!WapTool.IsNumeric(threePageType))
            //{
            //    threePageType = "1";
            //}
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
                ShowTipInfo("此栏目已关闭！", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
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
                    ShowTipInfo("帖子已删除，或不存在。", backurl);
                }
                else if (bookVo.ischeck == 1L)
                {
                    ShowTipInfo("正在审核中！", backurl);
                }
                else if (bookVo.book_classid.ToString() != classid)
                {
                    ShowTipInfo("栏目ID不正确！", "");
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
                content = bookVo.book_content;
                content = content.Replace("[id]", id);
                content = ProcessCodeTags(content);
                if (view != "all")
                {
                    content = BookViewHelper.ProcessContent(content, ref totalPage, CurrentPage, pageSize, viewLeave);
                    if (content != "")
                    {
                        content = "<!--listS-->" + content + "<!--listE-->";
                    }
                    linkURL = WapTool.GetPageContentLink(ver, lang, totalPage, pageSize, CurrentPage, http_start_url + "&amp;id=" + id);
                }
                content += "<span id='KL_show_next_list'></span>";

                wap2_attachment_BLL attachmentBLL = new wap2_attachment_BLL(a);
                string attachmentContent = BBSAttachment.ProcessAttachments(
                    bookVo.isdown,
                    classVo.smallimg,
                    siteid,
                    classid,
                    id,
                    lpage,
                    stypelink,
                    siteVo.SaveUpFilesPath,
                    siteVo.sitemoneyname,
                    http_start,
                    attachmentBLL
                );
                content += attachmentContent;

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
                    while (relistVo != null && i < relistVo.Count && i < 30)
                    {
                        stringBuilder.Append(relistVo[i].userid);
                        stringBuilder.Append(",");
                        i++;
                    }
                    stringBuilder.Append("0)");
                    userListVo_IDName = MainBll.GetUserListVo(stringBuilder.ToString());
                }
                VisiteCount("正在浏览帖子:<a href='" + http_start + "bbs-" + id + ".html'>" + bookVo.book_title + "</a>");
                Action_user_doit(3);

                // 获取竞猜数据
                guessingData = GetGuessingData(long.Parse(id));
                if (guessingData != null)
                {
                    if (guessingData.IsClosed)
                    {
                        // 设置新的属性
                        WinningOptionId = guessingData.WinningOptionId;
                        WinningOptionText = guessingData.WinningOptionText;
                    }

                    // 获取用户投注信息
                    long userIdLong;
                    if (long.TryParse(userid, out userIdLong))
                    {
                        GuessManager guessManager = new GuessManager(PubConstant.GetAppString("InstanceName"));
                        userBet = guessManager.GetUserBet(guessingData.Id, userIdLong);
                    }
                }

                if (state == "1")
                {
                    string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                    if (fcountSubMoneyFlag.IndexOf("BBSF" + id) < 0)
                    {
                        if (userVo.money >= 5)
                        {
                            // 扣除当前用户5个币
                            MainBll.UpdateSQL("update [user] set money=money - 5 where userid=" + userid);
                            // 给楼主加10个币
                            MainBll.UpdateSQL("update [user] set money=money + 10 where userid=" + bookVo.book_pub);
                            // 更新帖子的点赞数
                            wap_bbs_BLL.UpdateXiNuHan(siteid, id, state);
                            // 更新用户操作记录
                            MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "BBSF" + id + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);

                            // 记录交易日志
                            SaveBankLog(userid.ToString(), "帖子点赞", "-5", bookVo.book_pub.ToString(), userVo.nickname, "给帖子ID(" + id + ")点赞");
                            SaveBankLog(bookVo.book_pub.ToString(), "收到点赞", "10", userid.ToString(), userVo.nickname, "帖子ID(" + id + ")收到点赞");
                        }
                        else
                        {
                            strhtml.Append("<div class='tip'>您的妖晶不足5个，无法点赞。</div>");
                        }
                    }
                    else
                    {
                        strhtml.Append("<div class='tip'>您已经给这篇帖子点过赞了。</div>");
                    }
                }
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }

        public string ShowNickName_color(long userid, string nickname)
        {
            return BookViewHelper.ShowNickName_color(userid, nickname, userListVo_IDName, lang, ver);
        }

        public void UpdateBbsReCount(string nowid)
        {
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
            long num = wap_bbsre_BLL.GetListCount(" devid='" + siteid + "' and bookid=" + nowid + " and ischeck=0");
            MainBll.UpdateSQL("update wap_bbs set book_re=" + num + " where id=" + nowid);
        }

        private GuessData GetGuessingData(long bbsId)
        {
            string instanceName = PubConstant.GetAppString("InstanceName");
            if (string.IsNullOrEmpty(instanceName))
            {
                return null;
            }

            GuessManager guessManager = new GuessManager(instanceName);
            return guessManager.GetGuessingByBbsId(bbsId);
        }

    }

}