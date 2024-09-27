﻿using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Text;
using System.Text.RegularExpressions;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_add : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_CheckBBSCount = PubConstant.GetAppString("KL_CheckBBSCount");

        public string KL_BBS_Anonymous_Open = PubConstant.GetAppString("KL_BBS_Anonymous_Open");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string book_title = "";

        public string book_content = "";

        public string sendmoney = "";

        public bool isadmin = false;

        public long getid;

        public string getmoney = "0";

        public string getexpr = "0";

        public string needpwFlag = "";

        public string needpw = "";

        public string titlemax = "0";

        public string contentmax = "0";

        public string title_max = "0";

        public string content_max = "0";

        public string getmoney2 = "";

        public bool isNeedSecret = false;

        public StringBuilder strhtml = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块。", "");
            }
            action = base.Request.Form.Get("action");
            page = GetRequestValue("page");
            needpwFlag = WapTool.getArryString(siteVo.Version, '|', 31);
            if (classVo.topicID != "" && classVo.topicID != "0" && IsCheckManagerLvl("|00|01|03|04|", ""))
            {
                isNeedSecret = true;
            }
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 2)) && !CheckManagerLvl("04", classVo.adminusername))
            {
                ShowTipInfo("发帖功能已关闭！", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            string value = WapTool.getArryString(classVo.smallimg, '|', 11);
            if (KL_BBS_Anonymous_Open != "1")
            {
                value = "0";
            }
            if (!"1".Equals(value))
            {
                IsLogin(userid, "bbs/book_view_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
            }
            else
            {
                userVo.password = "";
            }
            string arryString = WapTool.getArryString(classVo.smallimg, '|', 27);
            if (arryString.Trim() != "")
            {
                arryString = arryString.Replace("_", "|");
                arryString = "|" + arryString + "|";
                if (!IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("|" + userVo.SessionTimeout + "|") < 0)
                {
                    ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + " 不允许发帖。<br/>允许发帖用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                }
            }
            string text = WapTool.GetSiteDefault(siteVo.Version, 14);
            if (!WapTool.IsNumeric(text))
            {
                text = "0";
            }
            long num = Convert.ToInt64(text);
            if (num > 0L)
            {
                long num2 = WapTool.DateDiff(DateTime.Now, userVo.RegTime, "MM");
                if (num2 < num)
                {
                    ShowTipInfo("请再过:" + (num - num2) + "分钟才能发帖！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                }
            }
            if (classid == "0")
            {
                ShowTipInfo("无此栏目ID", "");
            }
            if (userVo.managerlvl == "01" || userVo.managerlvl == "00")
            {
                isadmin = true;
            }
            if (action == "gomod")
            {
                try
                {
                    book_title = GetRequestValue("book_title").Trim(); // 移除标题前后的空格
                    book_content = GetRequestValue("book_content");
                    book_title = book_title.Replace("/", "／").Replace("[", "［").Replace("]", "］");
                    titlemax = WapTool.getArryString(classVo.smallimg, '|', 24);
                    contentmax = WapTool.getArryString(classVo.smallimg, '|', 25);
                    if (!WapTool.IsNumeric(titlemax) || titlemax == "0")
                    {
                        titlemax = "2";
                    }
                    if (!WapTool.IsNumeric(contentmax) || contentmax == "0")
                    {
                        contentmax = "2";
                    }
                    title_max = WapTool.getArryString(classVo.smallimg, '|', 30);
                    content_max = WapTool.getArryString(classVo.smallimg, '|', 31);
                    if (!WapTool.IsNumeric(title_max))
                    {
                        title_max = "0";
                    }
                    if (!WapTool.IsNumeric(content_max))
                    {
                        content_max = "0";
                    }
                    needpw = GetRequestValue("needpw");
                    sendmoney = GetRequestValue("sendmoney");
                    string text2 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (!WapTool.IsNumeric(sendmoney))
                    {
                        sendmoney = "0";
                    }
                    if (!WapTool.IsNumeric(text2))
                    {
                        text2 = "0";
                    }
                    if (long.Parse(text2) < 2L)
                    {
                        text2 = "1000";
                    }
                    if (long.Parse(sendmoney) > long.Parse(text2))
                    {
                        sendmoney = text2;
                    }
                    string arryString2 = WapTool.getArryString(classVo.smallimg, '|', 21);
                    if (arryString2.Trim() != "")
                    {
                        arryString2 = arryString2.Replace("_", "|");
                        arryString2 = "|" + arryString2 + "|";
                        bool flag = false;
                        if (book_content.IndexOf("[/reply]") > 0 || book_content.IndexOf("[/buy]") > 0 || book_content.IndexOf("[/coin]") > 0 || book_content.IndexOf("[/grade]") > 0)
                        {
                            flag = true;
                        }
                        if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString2.IndexOf("|" + userVo.SessionTimeout + "|") < 0)
                        {
                            ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + " 不允许发特殊帖。<br/>允许发特殊帖用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString2, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                        }
                    }
                    string[] array = (WapTool.getArryString(classVo.smallimg, '|', 34) + ",").Split(',');
                    getmoney2 = array[0];
                    if (needpwFlag == "1" && PubConstant.md5(needpw).ToLower() != userVo.password.ToLower())
                    {
                        INFO = "PWERROR";
                    }
                    else if (isNeedSecret && base.Request.Form.Get("secret").ToString() != classVo.topicID)
                    {
                        INFO = "ERROR_Secret";
                    }
                    else if (book_title.Trim().Length < long.Parse(titlemax) || book_content.Trim().Length < long.Parse(contentmax))
                    {
                        INFO = "NULL";
                    }
                    else if ((title_max != "0" && book_title.Trim().Length > long.Parse(title_max)) || (content_max != "0" && book_content.Trim().Length > long.Parse(content_max)))
                    {
                        INFO = "TITLEMAX";
                    }
                    //else if (book_title.IndexOf("$(") >= 0 || book_content.IndexOf("$(") >= 0)
                    //{
                    //    INFO = "ERR_FORMAT";
                    //}
                    else if (book_title.Equals(Session["content"]))
                    {
                        INFO = "REPEAT";
                    }
                    else if (KL_CheckBBSCount != "0" && !WapTool.CheckUserBBSCount(siteid, userid, KL_CheckBBSCount, "bbs"))
                    {
                        INFO = "MAX";
                    }
                    else if (userVo.money < long.Parse(sendmoney))
                    {
                        INFO = "SENDMONEY";
                    }
                    else if (WapTool.isLockuser(siteid, userid, classid) > -1L)
                    {
                        INFO = "LOCK";
                    }
                    else if (getmoney2.IndexOf('-') == 0 && userVo.money + long.Parse(getmoney2) < 0L)
                    {
                        INFO = "NOMONEY";
                    }
                    else
                    {
                        Session["content"] = book_title;
                        if (book_title.Length > 200)
                        {
                            book_title = book_title.Substring(0, 200);
                        }
                        wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                        wap_bbs_Model wap_bbs_Model = new wap_bbs_Model();
                        wap_bbs_Model.ischeck = siteVo.isCheck;
                        wap_bbs_Model.userid = long.Parse(siteid);
                        wap_bbs_Model.book_classid = long.Parse(classid);
                        wap_bbs_Model.book_title = book_title;
                        wap_bbs_Model.book_author = userVo.nickname;
                        wap_bbs_Model.book_pub = userid;
                        wap_bbs_Model.book_content = book_content;
                        wap_bbs_Model.book_date = DateTime.Now;
                        wap_bbs_Model.reDate = DateTime.Now;
                        wap_bbs_Model.sendMoney = long.Parse(sendmoney);
                        getid = wap_bbs_BLL.Add(wap_bbs_Model);
                        getmoney = WapTool.GetSiteDefault(siteVo.moneyregular, 0);
                        if (!WapTool.IsNumeric(getmoney))
                        {
                            getmoney = "0";
                        }
                        getexpr = WapTool.GetSiteDefault(siteVo.lvlRegular, 0);
                        if (!WapTool.IsNumeric(getexpr))
                        {
                            getexpr = "0";
                        }
                        string[] array2 = (WapTool.getArryString(classVo.smallimg, '|', 34) + ",").Split(',');
                        if (WapTool.IsNumeric(array2[0].Replace("-", "")))
                        {
                            getmoney = array2[0];
                        }
                        if (WapTool.IsNumeric(array2[1].Replace("-", "")))
                        {
                            getexpr = array2[1];
                        }
                        MainBll.UpdateSQL("update [user] set [money]=([money]+" + getmoney + "-" + sendmoney + "),expR=expR+" + getexpr + ",bbscount=" + (userVo.bbsCount + 1L) + " where siteid=" + siteid + " and userid=" + userid);
                        SaveBankLog(userid, "论坛发帖", getmoney.ToString(), userid, nickname, "发新帖[" + getid + "]");
                        if (long.Parse(sendmoney) > 0L)
                        {
                            SaveBankLog(userid, "发布赏帖", "-" + sendmoney.ToString(), userid, nickname, "发赏帖[" + getid + "]");
                        }
                        VisiteCount("发表新帖:<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + getid + "\">" + WapTool.GetShowImg(wap_bbs_Model.book_title, "200", "bbs") + "</a>");
                        INFO = "OK";
                        WapTool.ClearDataBBS("bbs" + siteid + classid);
                        WapTool.ClearDataTemp("bbsTotal" + siteid + classid);
                        Action_user_doit(1);
                    }
                }
                catch (Exception ex)
                {
                    ERROR = WapTool.ErrorToString(ex.ToString());
                }
            }
            if (INFO == "WAITING")
            {
                VisiteCount("发表新帖。");
            }
        }
    }
}