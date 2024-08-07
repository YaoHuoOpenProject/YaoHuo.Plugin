﻿using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Text;
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

        public string face = "";

        public string stype = "";

        public string viewtype = "";

        public string viewmoney = "";

        public string reshow = "";

        public string sendmoney = "";

        public string[] facelist;

        public string[] facelistImg;

        public string[] stypelist;

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

        public string book_img = "";

        public bool isNeedSecret = false;

        public StringBuilder strhtml = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
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
                ShowTipInfo("发贴功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
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
                stypelist = classVo.bbsType.Split('_')[0].Split('|');
            }
            catch (Exception)
            {
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
                    ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + " 不允许发贴。<br/>允许发贴用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
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
                    ShowTipInfo("请再过:" + (num - num2) + "分钟才能发贴！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
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
            if (action == "friends")
            {
                book_content = getUserInfoFriends(userVo.userid, siteVo.siteid);
                if (ver == "1")
                {
                    book_content = book_content.Replace("\n", "[br]");
                }
            }
            if (action == "gomod")
            {
                try
                {
                    book_title = GetRequestValue("book_title");
                    book_content = GetRequestValue("book_content");
                    book_img = GetRequestValue("book_img");
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
                    face = GetRequestValue("face");
                    stype = GetRequestValue("stype");
                    viewtype = GetRequestValue("viewtype");
                    viewmoney = GetRequestValue("viewmoney");
                    reshow = GetRequestValue("reshow");
                    sendmoney = GetRequestValue("sendmoney");
                    if (WapTool.getArryString(classVo.smallimg, '|', 41) == "1" && stype.Trim() == "")
                    {
                        ShowTipInfo("类别不能为空！", "bbs/book_view_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                    }
                    if (!isadmin)
                    {
                        reshow = "0";
                    }
                    string text2 = WapTool.getArryString(siteVo.Version, '|', 22);
                    if (stype.IndexOf("$") >= 0)
                    {
                        stype = "";
                    }
                    if (!WapTool.IsNumeric(reshow))
                    {
                        reshow = "0";
                    }
                    if (!WapTool.IsNumeric(sendmoney))
                    {
                        sendmoney = "0";
                    }
                    if (!WapTool.IsNumeric(viewmoney))
                    {
                        viewmoney = "0";
                    }
                    if (!WapTool.IsNumeric(viewtype))
                    {
                        viewtype = "0";
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
                    if (long.Parse(reshow) > long.Parse(text2))
                    {
                        reshow = text2;
                    }
                    if (viewtype == "6" && long.Parse(viewmoney) > long.Parse(text2))
                    {
                        viewmoney = text2;
                    }
                    if (long.Parse(reshow) > 0L && !"1".Equals(WapTool.getArryString(siteVo.Version, '|', 18)) && "|00|01|".IndexOf(userVo.managerlvl) < 0)
                    {
                        ShowTipInfo("站长关闭此版主发签到贴权限。<br/><br/>【网站默认设置】中配置。", "bbs/book_view_add.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                    }
                    string arryString2 = WapTool.getArryString(classVo.smallimg, '|', 21);
                    if (arryString2.Trim() != "")
                    {
                        arryString2 = arryString2.Replace("_", "|");
                        arryString2 = "|" + arryString2 + "|";
                        bool flag = false;
                        if (int.Parse(viewtype) > 2 || book_content.IndexOf("[/reply]") > 0 || book_content.IndexOf("[/buy]") > 0 || book_content.IndexOf("[/coin]") > 0 || book_content.IndexOf("[/grade]") > 0)
                        {
                            flag = true;
                        }
                        if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString2.IndexOf("|" + userVo.SessionTimeout + "|") < 0)
                        {
                            ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang, userVo.endTime) + " 不允许发特殊贴。<br/>允许发特殊贴用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString2, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
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
                    else if (book_title.IndexOf("$(") >= 0 || book_content.IndexOf("$(") >= 0)
                    {
                        INFO = "ERR_FORMAT";
                    }
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
                        stype = stype.Replace("类别", "");
                        face = face.Replace("表情", "");
                        if (stype != "")
                        {
                            book_title = "[" + stype + "]" + book_title;
                        }
                        if (face.Trim().Length > 3 && face.Substring(face.Length - 3, 3).ToLower() == "gif")
                        {
                            book_title = "[img]face/" + face + "[/img]" + book_title;
                        }
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
                        wap_bbs_Model.reShow = long.Parse(reshow);
                        wap_bbs_Model.sendMoney = long.Parse(sendmoney);
                        wap_bbs_Model.viewmoney = long.Parse(viewmoney);
                        wap_bbs_Model.viewtype = long.Parse(viewtype);
                        wap_bbs_Model.reDate = DateTime.Now;
                        wap_bbs_Model.book_img = book_img;
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
                        SaveBankLog(userid, "论坛发贴", getmoney.ToString(), userid, nickname, "发新贴[" + getid + "]");
                        if (long.Parse(sendmoney) > 0L)
                        {
                            SaveBankLog(userid, "发布赏贴", "-" + sendmoney.ToString(), userid, nickname, "发赏贴[" + getid + "]");
                        }
                        VisiteCount("发表新贴:<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + getid + "\">" + WapTool.GetShowImg(wap_bbs_Model.book_title, "200", "bbs") + "</a>");
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
                VisiteCount("发表新贴。");
            }
        }

        public string getUserInfoFriends(long userid, long siteid)
        {
            user_Info_BLL user_Info_BLL = new user_Info_BLL(a);
            user_Info_Model model = user_Info_BLL.GetModel(userVo.userid, siteVo.siteid);
            StringBuilder stringBuilder = new StringBuilder();
            if (model != null)
            {
                book_title = model.purpost + "［" + model.name + "/" + model.age + "/" + model.sex + "/" + model.marriage + "］";
                if (userVo.headimg.IndexOf("/") >= 0)
                {
                    stringBuilder.Append("[img]" + http_start + userVo.headimg + "[/img]\n");
                }
                else
                {
                    stringBuilder.Append("[img]" + http_start + "bbs/head/" + userVo.headimg + "[/img]\n");
                }
                stringBuilder.Append("【交友目的】:" + model.purpost + "\n");
                stringBuilder.Append("【姓名】:" + model.name + "\n");
                stringBuilder.Append("【性别】:" + model.sex + "\n");
                stringBuilder.Append("【年龄】:" + model.age + "\n");
                stringBuilder.Append("【身高】:" + model.CM + "CM\n");
                stringBuilder.Append("【体重】:" + model.KG + "KG\n");
                stringBuilder.Append("【城市】:" + model.city + "\n");
                string text = model.birthday + "---";
                string text2 = text.Split('-')[0];
                string text3 = text.Split('-')[1];
                string text4 = text.Split('-')[2];
                stringBuilder.Append("【生日】:" + text2 + "-" + text3 + "-" + text4 + "\n");
                stringBuilder.Append("【民族】:" + model.nation + "\n");
                stringBuilder.Append("【星座】:" + model.star + "\n");
                stringBuilder.Append("【血型】:" + model.blood + "型\n");
                stringBuilder.Append("【学历】:" + model.education + "\n");
                stringBuilder.Append("【职业】:" + model.profession + "\n");
                stringBuilder.Append("【月薪】:" + model.monthpay + "\n");
                stringBuilder.Append("【宗教】:" + model.religion + "\n");
                stringBuilder.Append("【爱好】:" + model.love + "\n");
                stringBuilder.Append("【性格】:" + model.nature + "\n");
                stringBuilder.Append("【外貌】:" + model.looks + "\n");
                stringBuilder.Append("【婚姻】:" + model.marriage + "\n");
                stringBuilder.Append("【QQ】:" + model.QQ + "\n");
                stringBuilder.Append("【邮箱】:" + model.Email + "\n");
                stringBuilder.Append("【特长】:" + model.speciality + "\n");
                stringBuilder.Append("【择友条件】:" + model.condition + "\n");
                stringBuilder.Append("【人生格言】:" + model.aphorism + "\n");
                stringBuilder.Append("【喜欢服饰】:" + model.loveClothes + "\n");
                stringBuilder.Append("【喜欢明星】:" + model.loveStar + "\n");
                stringBuilder.Append("【喜欢动物】:" + model.loveAnimal + "\n");
                stringBuilder.Append("【喜欢食物】:" + model.loveFood + "\n");
                stringBuilder.Append("【喜欢颜色】:" + model.loveColor + "\n");
                stringBuilder.Append("【喜欢音乐】:" + model.loveMusic + "\n");
                stringBuilder.Append("【其它】:" + model.other + "\n");
            }
            else
            {
                stringBuilder.Append("请先完善交友资料！");
            }
            return stringBuilder.ToString();
        }
    }
}