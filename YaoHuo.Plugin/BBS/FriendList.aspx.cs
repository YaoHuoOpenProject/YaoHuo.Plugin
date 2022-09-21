using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class FriendList : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string friendtype = "";

        public string backurl = "";

        public string linkTOP = "";

        public string INFO = "";

        public string KL_ADDFriendCount = PubConstant.GetAppString("KL_ADDFriendCount");

        public List<wap_friends_Model> listVo = null;

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_01a9
            string text = default(string);
            bool flag = default(bool);
            while (true)
            {
                action = GetRequestValue("action");
                backurl = base.Request.QueryString.Get("backurl");
                int num = 13;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 13:
                            num = ((backurl == null) ? 25 : 12);
                            continue;
                        case 8:
                            num = 1;
                            continue;
                        case 1:
                            if (!(text == "class"))
                            {
                                num = 19;
                                continue;
                            }
                            showclass();
                            num = 16;
                            continue;
                        case 24:
                            backurl = ToHtm(backurl);
                            backurl = HttpUtility.UrlDecode(backurl);
                            backurl = WapTool.URLtoWAP(backurl);
                            friendtype = GetRequestValue("friendtype");
                            flag = WapTool.IsNumeric(friendtype);
                            num = 4;
                            continue;
                        case 4:
                            if (!flag)
                            {
                                num = 6;
                                continue;
                            }
                            goto case 2;
                        case 9:
                            backurl = base.Request.Form.Get("backurl");
                            num = 26;
                            continue;
                        case 16:
                            return;

                        case 25:
                            num3 = 0;
                            goto IL_02e9;
                        case 2:
                            IsLogin(userid, backurl);
                            text = action;
                            num = 0;
                            continue;
                        case 0:
                            if (text != null)
                            {
                                num = 8;
                                continue;
                            }
                            goto case 14;
                        case 22:
                            return;

                        case 17:
                            backurl = "myfile.aspx?siteid=" + siteid;
                            num = 24;
                            continue;
                        case 26:
                            num = 20;
                            continue;
                        case 20:
                            num = ((backurl == null) ? 23 : 15);
                            continue;
                        case 3:
                            num = 14;
                            continue;
                        case 14:
                            showclass();
                            num = 10;
                            continue;
                        case 10:
                            return;

                        case 23:
                            num2 = 0;
                            goto IL_0334;
                        case 12:
                            num = 7;
                            continue;
                        case 7:
                            num3 = ((!(backurl == "")) ? 1 : 0);
                            goto IL_02e9;
                        case 5:
                            if (!flag)
                            {
                                num = 9;
                                continue;
                            }
                            goto case 26;
                        case 15:
                            num = 18;
                            continue;
                        case 18:
                            num2 = ((!(backurl == "")) ? 1 : 0);
                            goto IL_0334;
                        case 11:
                            if (!flag)
                            {
                                num = 17;
                                continue;
                            }
                            goto case 24;
                        case 19:
                            num = 21;
                            continue;
                        case 21:
                            if (text == "addfriend")
                            {
                                goaddfriend();
                                num = 22;
                            }
                            else
                            {
                                num = 3;
                            }
                            continue;
                        case 6:
                            {
                                friendtype = "0";
                                num = 2;
                                continue;
                            }
                        IL_0334:
                            flag = (byte)num2 != 0;
                            num = 11;
                            continue;
                        IL_02e9:
                            flag = (byte)num3 != 0;
                            num = 5;
                            continue;
                    }
                    break;
                }
            }
        }

        public void showclass()
        {
            //Discarded unreachable code: IL_00d6
            while (true)
            {
                key = GetRequestValue("key");
                condition = " siteid= " + siteid;
                condition = condition + " and  userid=" + userid + " and friendtype=" + friendtype;
                bool flag = !(key.Trim() != "");
                int num = 7;
                while (true)
                {
                    switch (num)
                    {
                        case 7:
                            if (!flag)
                            {
                                num = 6;
                                continue;
                            }
                            goto case 4;
                        case 0:
                            num = 5;
                            continue;
                        case 1:
                            condition = condition + " and  userid = " + key + " ";
                            num = 0;
                            continue;
                        case 4:
                            flag = !(friendtype == "4");
                            num = 8;
                            continue;
                        case 8:
                            if (!flag)
                            {
                                num = 2;
                                continue;
                            }
                            goto case 5;
                        case 6:
                            condition = condition + " and  frienduserid = " + key + " ";
                            num = 4;
                            continue;
                        case 5:
                            try
                            {
                                while (true)
                                {
                                    pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                    wap_friends_BLL wap_friends_BLL = new wap_friends_BLL(a);
                                    flag = !(GetRequestValue("getTotal") != "");
                                    num = 5;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 5:
                                                if (!flag)
                                                {
                                                    num = 11;
                                                    continue;
                                                }
                                                flag = !(friendtype == "4");
                                                num = 1;
                                                continue;
                                            case 9:
                                            case 14:
                                                flag = !(GetRequestValue("page") != "");
                                                num = 7;
                                                continue;
                                            case 7:
                                                if (!flag)
                                                {
                                                    num = 8;
                                                    continue;
                                                }
                                                goto case 12;
                                            case 10:
                                                total = wap_friends_BLL.GetListCount2("select count(id) as n from wap_friends_VIEW where " + condition);
                                                num = 0;
                                                continue;
                                            case 3:
                                                listVo = wap_friends_BLL.GetListVo2(pageSize, CurrentPage, condition, "*", "id", total, 1L);
                                                num = 2;
                                                continue;
                                            case 11:
                                                total = long.Parse(GetRequestValue("getTotal"));
                                                num = 14;
                                                continue;
                                            case 1:
                                                if (flag)
                                                {
                                                    total = wap_friends_BLL.GetListCount(condition);
                                                    num = 15;
                                                }
                                                else
                                                {
                                                    num = 10;
                                                }
                                                continue;
                                            case 0:
                                            case 15:
                                                num = 9;
                                                continue;
                                            case 8:
                                                CurrentPage = long.Parse(GetRequestValue("page"));
                                                num = 12;
                                                continue;
                                            case 12:
                                                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                                index = pageSize * (CurrentPage - 1);
                                                linkURL = http_start + "bbs/FriendList.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;friendtype=" + friendtype + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                                                flag = !(friendtype == "4");
                                                num = 6;
                                                continue;
                                            case 6:
                                                if (flag)
                                                {
                                                    listVo = wap_friends_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1L);
                                                    num = 13;
                                                }
                                                else
                                                {
                                                    num = 3;
                                                }
                                                continue;
                                            case 2:
                                            case 13:
                                                num = 4;
                                                continue;
                                            case 4:
                                                return;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ERROR = ex.ToString();
                                return;
                            }
                        case 2:
                            condition = " siteid= " + siteid + " and frienduserid=" + userid;
                            flag = !(key.Trim() != "");
                            num = 3;
                            continue;
                        case 3:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 0;
                    }
                    break;
                }
            }
        }

        public void goaddfriend()
        {
            //Discarded unreachable code: IL_057e
            string title = default(string);
            long listCount = default(long);
            while (true)
            {
                string requestValue = GetRequestValue("touserid");
                user_Model user_Model = null;
                user_BLL user_BLL = new user_BLL(a);
                user_Model = user_BLL.getUserInfo(requestValue, siteid);
                bool flag = WapTool.IsNumeric(KL_ADDFriendCount);
                int num = 29;
                while (true)
                {
                    switch (num)
                    {
                        case 29:
                            if (!flag)
                            {
                                num = 19;
                                continue;
                            }
                            goto case 1;
                        case 5:
                            title = "TA的黑名单|TA的黑名单|black";
                            num = 16;
                            continue;
                        case 7:
                            INFO = "HASEXIST";
                            num = 21;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            flag = !(userid == requestValue);
                            num = 23;
                            continue;
                        case 18:
                            title = "TA的追求|TA的追求|his love";
                            num = 27;
                            continue;
                        case 15:
                            INFO = "NOTUSER";
                            num = 10;
                            continue;
                        case 22:
                            if (!flag)
                            {
                                num = 5;
                                continue;
                            }
                            flag = !(friendtype == "2");
                            num = 26;
                            continue;
                        case 13:
                            {
                                string text = nickname + GetLang("将你加入|将你加入|to") + GetLang(title);
                                string text2 = GetLang("操作时间|操作时间|Operation Time") + ":" + DateTime.Now;
                                string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                                text3 = text3 + "  values(" + siteid + "," + userid + ",'" + nickname + "','" + text + "','" + text2 + "'," + requestValue + ",1)";
                                MainBll.UpdateSQL(text3);
                                num = 4;
                                continue;
                            }
                        case 3:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            title = "";
                            flag = !(friendtype == "0");
                            num = 0;
                            continue;
                        case 12:
                            INFO = "MAX";
                            num = 30;
                            continue;
                        case 8:
                        case 16:
                        case 27:
                            {
                                string text4 = user_Model.nickname;
                                //限制黑名单上限
                                if (friendtype == "1")
                                {
                                    var a = "kelinkWAP_Check";
                                    var b = PubConstant.GetConnectionString(a);
                                    //查询用户信息
                                    var userInfo = MainBll.getUserInfo(userid, siteid);
                                    var isResult = BlackTool.AddBlackUser(userInfo, b, requestValue);
                                    if (!string.IsNullOrEmpty(isResult))
                                    {
                                        INFO = isResult;
                                        num = 21;
                                        continue;
                                    }
                                }
                                MainBll.UpdateSQL("insert into wap_friends(siteid,userid,frienduserid,friendusername,friendnickname,friendtype)values(" + siteid + "," + userid + "," + requestValue + ",'','" + text4 + "'," + friendtype + ")");
                                flag = !(friendtype != "1");
                                num = 17;
                                continue;
                            }
                        case 17:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            goto case 4;
                        case 4:
                            INFO = "OK";
                            Action_user_doit(6);
                            num = 2;
                            continue;
                        case 9:
                            if (flag)
                            {
                                flag = !WapTool.isExistFriend(siteid, userid, requestValue, friendtype);
                                num = 3;
                            }
                            else
                            {
                                num = 28;
                            }
                            continue;
                        case 19:
                            KL_ADDFriendCount = "10";
                            num = 1;
                            continue;
                        case 23:
                            if (flag)
                            {
                                flag = WapTool.isLockuser(siteid, userid, "0") <= -1;
                                num = 9;
                            }
                            else
                            {
                                num = 11;
                            }
                            continue;
                        case 1:
                            {
                                wap_friends_BLL wap_friends_BLL = new wap_friends_BLL(a);
                                listCount = wap_friends_BLL.GetListCount(" (DATEDIFF(dd, addtime, GETDATE()) < 1) and friendtype=0 and siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(userid));
                                flag = user_Model != null;
                                num = 14;
                                continue;
                            }
                        case 14:
                            if (flag)
                            {
                                flag = listCount <= long.Parse(KL_ADDFriendCount);
                                num = 6;
                            }
                            else
                            {
                                num = 15;
                            }
                            continue;
                        case 28:
                            INFO = "LOCK";
                            num = 24;
                            continue;
                        case 26:
                            if (!flag)
                            {
                                num = 18;
                                continue;
                            }
                            goto case 8;
                        case 20:
                            title = "TA的好友|TA的好友|his friend";
                            num = 8;
                            continue;
                        case 11:
                            INFO = "MY";
                            num = 25;
                            continue;
                        case 0:
                            if (flag)
                            {
                                flag = !(friendtype == "1");
                                num = 22;
                            }
                            else
                            {
                                num = 20;
                            }
                            continue;
                        case 2:
                        case 10:
                        case 21:
                        case 24:
                        case 25:
                        case 30:
                            showclass();
                            return;
                    }
                    break;
                }
            }
        }
    }
}
