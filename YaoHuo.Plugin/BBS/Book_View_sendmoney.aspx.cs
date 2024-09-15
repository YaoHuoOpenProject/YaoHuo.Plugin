using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_SendMoney : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string KL_CheckBBSCount = PubConstant.GetAppString("KL_CheckBBSCount");

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

        public StringBuilder strhtml = new StringBuilder();

        public string freemoney = "";

        public string freerule1 = "";

        public string freerule2 = "";

        public string maxs = "";

        public long allMoney = 0L;

        public bool isNeedSecret = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            page = GetRequestValue("page");
            needpwFlag = WapTool.getArryString(siteVo.Version, '|', 31);
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 2)) && !CheckManagerLvl("04", classVo.adminusername))
            {
                ShowTipInfo("发帖功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            if (classVo.topicID != "" && classVo.topicID != "0" && IsCheckManagerLvl("|00|01|03|04|", ""))
            {
                isNeedSecret = true;
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
            if (!"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 11)))
            {
                IsLogin(userid, "bbs/book_view_sendmoney.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
            }
            string arryString = WapTool.getArryString(classVo.smallimg, '|', 27);
            if (arryString.Trim() != "")
            {
                arryString = "|" + arryString + "|";
                if (!IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("|" + userVo.SessionTimeout + "|") < 0)
                {
                    ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang) + " 不允许发帖。<br/>允许发帖用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
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
            isadmin = IsUserManager(userid, userVo.managerlvl, classVo.adminusername);
            if (action == "friends")
            {
                book_content = getUserInfoFriends(userVo.userid, siteVo.siteid);
                if (ver == "1")
                {
                    book_content = book_content.Replace("\n", "[br]");
                }
            }
            maxs = WapTool.getArryString(siteVo.Version, '|', 22);
            if (!WapTool.IsNumeric(maxs))
            {
                maxs = "0";
            }
            if (long.Parse(maxs) < 2L)
            {
                maxs = "10000";
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
                    face = GetRequestValue("face");
                    stype = GetRequestValue("stype");
                    freemoney = GetRequestValue("freemoney");
                    freerule1 = GetRequestValue("freerule1");
                    freerule2 = GetRequestValue("freerule2");
                    if (WapTool.getArryString(classVo.smallimg, '|', 41) == "1" && stype.Trim() == "")
                    {
                        ShowTipInfo("类别不能为空！", "bbs/book_view_sendmoney.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                    }
                    if (stype.IndexOf("$") >= 0)
                    {
                        stype = "";
                    }
                    if (!WapTool.IsNumeric(freemoney))
                    {
                        freemoney = "0";
                    }
                    if (long.Parse(freemoney) > long.Parse(maxs))
                    {
                        freemoney = maxs;
                    }
                    bool flag = false;
                    bool flag2 = false;
                    string[] array = freerule2.Split('|');
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (WapTool.IsNumeric(array[i]))
                        {
                            allMoney += long.Parse(array[i]);
                            continue;
                        }
                        flag = true;
                        break;
                    }
                    string[] array2 = freerule1.Split('|');
                    for (int i = 0; i < array2.Length; i++)
                    {
                        if (!WapTool.IsNumeric(array2[i]))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    string arryString2 = WapTool.getArryString(classVo.smallimg, '|', 21);
                    if (arryString2.Trim() != "")
                    {
                        arryString2 = "|" + arryString2 + "|";
                        bool flag3 = false;
                        if (book_content.IndexOf("[/reply]") > 0 || book_content.IndexOf("[/buy]") > 0 || book_content.IndexOf("[/coin]") > 0 || book_content.IndexOf("[/grade]") > 0)
                        {
                            flag3 = true;
                        }
                        if (flag3 && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString2.IndexOf("|" + userVo.SessionTimeout + "|") < 0)
                        {
                            ShowTipInfo("我当前的用户级别：" + WapTool.GetMyID(userVo.idname, lang) + " 不允许发特殊帖。<br/>允许发特殊帖用户级别为：" + WapTool.GetCardIDNameFormID_multiple(siteid, arryString2, lang), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + page);
                        }
                    }
                    if (flag || flag2)
                    {
                        INFO = "FORMATERR";
                    }
                    else if (needpwFlag == "1" && PubConstant.md5(needpw).ToLower() != userVo.password.ToLower())
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
                    else
                    {
                        List<long> exemptUserIds = new List<long> { 1000 }; // 这里添加不受限制的用户ID
                        long userIdLong;

                        if (long.TryParse(userid, out userIdLong) && !exemptUserIds.Contains(userIdLong) && KL_CheckBBSCount != "0" && !WapTool.CheckUserBBSCount(siteid, userid, KL_CheckBBSCount, "bbs"))
                        {
                            INFO = "MAX";
                        }
                        else if (WapTool.isLockuser(siteid, userid, classid) > -1L)
                        {
                            INFO = "LOCK";
                        }
                        else if (WapTool.CheckStrCount(freerule1, "|") != WapTool.CheckStrCount(freerule2, "|"))
                        {
                            INFO = "FORMATERR";
                        }
                        else if (long.Parse(freemoney) > userVo.money || long.Parse(freemoney) < 1L)
                        {
                            INFO = "NOMONEY";
                        }
                        else if (allMoney > long.Parse(freemoney))
                        {
                            INFO = "MAXMONEY";
                        }
                        else if (array2.Length > 1 && allMoney != long.Parse(freemoney))
                        {
                            INFO = "NOEQUALMONEY";
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
                            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
                            wap_bbs_Model wap_bbs_Model = new wap_bbs_Model();
                            wap_bbs_Model.ischeck = siteVo.isCheck;
                            wap_bbs_Model.userid = long.Parse(siteid);
                            wap_bbs_Model.book_classid = long.Parse(classid);
                            wap_bbs_Model.book_title = book_title;
                            wap_bbs_Model.book_author = userVo.nickname;
                            wap_bbs_Model.book_pub = userid;
                            wap_bbs_Model.book_content = book_content;
                            wap_bbs_Model.book_date = DateTime.Now;
                            wap_bbs_Model.reShow = 0L;
                            wap_bbs_Model.sendMoney = 0L;
                            wap_bbs_Model.viewmoney = 0L;
                            wap_bbs_Model.viewtype = 0L;
                            wap_bbs_Model.reDate = DateTime.Now;
                            wap_bbs_Model.freeMoney = long.Parse(freemoney);
                            wap_bbs_Model.freeLeftMoney = wap_bbs_Model.freeMoney;
                            wap_bbs_Model.freeRule = freerule1 + "_" + freerule2;
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
                            MainBll.UpdateSQL("update [user] set [money]=([money]+" + getmoney + "-" + long.Parse(freemoney) + "),expR=expR+" + getexpr + ",bbscount=" + (userVo.bbsCount + 1L) + " where siteid=" + siteid + " and userid=" + userid);
                            SaveBankLog(userid, "论坛发帖", getmoney.ToString(), userid, nickname, "发派币帖[" + getid + "]");
                            if (long.Parse(freemoney) > 0L)
                            {
                                SaveBankLog(userid, "发派币帖", "-" + freemoney.ToString(), userid, nickname, "发布派币帖[" + getid + "]");
                            }
                            VisiteCount("发表新帖:<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + getid + "\">" + WapTool.GetShowImg(wap_bbs_Model.book_title, "200", "bbs") + "</a>");
                            INFO = "OK";
                            WapTool.ClearDataBBS("bbs" + siteid + classid);
                            WapTool.ClearDataTemp("bbsTotal" + siteid + classid);
                            Action_user_doit(1);
                        }
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

        public string getUserInfoFriends(long userid, long siteid)
        {
            user_Info_BLL user_Info_BLL = new user_Info_BLL(string_10);
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