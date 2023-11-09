using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View : PageWap
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
            //Discarded unreachable code: IL_5e51
            string text5 = default(string);
            string backurl = default(string);
            user_BLL user_BLL = default(user_BLL);
            wap_bbs_MarkSix_bet_Model model = default(wap_bbs_MarkSix_bet_Model);
            string text7 = default(string);
            string text9 = default(string);
            string[] array = default(string[]);
            string text15 = default(string);
            int num2 = default(int);
            string text12 = default(string);
            string text14 = default(string);
            string text13 = default(string);
            StringBuilder stringBuilder2 = default(StringBuilder);
            List<wap2_attachment_Model> listVo = default(List<wap2_attachment_Model>);
            string text = default(string);
            string text6 = default(string);
            string text11 = default(string);
            string text8 = default(string);
            string text10 = default(string);
            Regex regex = default(Regex);
            string text4 = default(string);
            string text3 = default(string);
            string text2 = default(string);
            wap_bbsre_BLL wap_bbsre_BLL = default(wap_bbsre_BLL);
            long num7 = default(long);
            bool flag = default(bool);
            string siteDefault = default(string);
            StringBuilder stringBuilder = default(StringBuilder);
            while (true)
            {
                int num = 34;
                while (true)
                {
                    int num32;
                    int num31;
                    int num29;
                    int num30;
                    int num33;
                    switch (num)
                    {
                        case 34:
                            num = ((!(classid != "0")) ? 26 : 21);
                            continue;
                        case 54:
                            pageSize = Convert.ToInt32(siteVo.MaxPerPage_Content);
                            num = 67;
                            continue;
                        case 27:
                            num = 35;
                            continue;
                        case 35:
                            num32 = ((!(towidths_H != "0")) ? 1 : 0);
                            goto IL_62b5;
                        case 13:
                            num = ((!(towidths_W == "0")) ? 49 : 27);
                            continue;
                        case 60:
                            towidths = " width=\"320px\" height=\"100%\" ";
                            num = 42;
                            continue;
                        case 58:
                            num31 = 1;
                            goto IL_5e16;
                        case 41:
                            num29 = 1;
                            goto IL_615b;
                        case 32:
                            ShowTipInfo("贴子ID参数错误！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + stypelink);
                            num = 8;
                            continue;
                        case 19:
                            isAdmin = IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername);
                            CheckUserViewSubMoney("BBS" + id, GetUrlQueryString(), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + stypelink);
                            pageSize = Convert.ToInt32(userVo.MaxPerPage_Content);
                            flag = pageSize >= 100;
                            num = 1;
                            continue;
                        case 1:
                            if (!flag)
                            {
                                num = 54;
                                continue;
                            }
                            goto case 67;
                        case 17:
                            flag = !WapTool.IsNumeric(stype);
                            num = 56;
                            continue;
                        case 56:
                            if (!flag)
                            {
                                num = 14;
                                continue;
                            }
                            goto case 68;
                        case 24:
                            num = 19;
                            continue;
                        case 21:
                            num = 45;
                            continue;
                        case 45:
                            num30 = ((!(classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                            goto IL_61ab;
                        case 0:
                            try
                            {
                                while (true)
                                {
                                    wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                                    state = GetRequestValue("state");
                                    flag = !(state != "");
                                    num = 80;
                                    while (true)
                                    {
                                        int num14;
                                        int num11;
                                        int num8;
                                        int num5;
                                        int num28;
                                        int num27;
                                        int num26;
                                        int num25;
                                        int num22;
                                        int num21;
                                        int num20;
                                        int num19;
                                        int num18;
                                        int num17;
                                        int num16;
                                        int num15;
                                        int num13;
                                        int num12;
                                        int num10;
                                        int num9;
                                        int num6;
                                        int num4;
                                        int num3;
                                        switch (num)
                                        {
                                            case 80:
                                                if (!flag)
                                                {
                                                    num = 105;
                                                    continue;
                                                }
                                                goto case 10;
                                            case 105:
                                                text5 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                                                flag = text5.IndexOf("BBSH" + id) >= 0;
                                                num = 192;
                                                continue;
                                            case 192:
                                                if (!flag)
                                                {
                                                    num = 394;
                                                    continue;
                                                }
                                                goto case 264;
                                            case 394:
                                                wap_bbs_BLL.UpdateXiNuHan(siteid, id, state);
                                                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + text5 + "BBSH" + id + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                                                num = 264;
                                                continue;
                                            case 264:
                                                num = 10;
                                                continue;
                                            case 10:
                                                flag = CurrentPage != 1;
                                                num = 67;
                                                continue;
                                            case 67:
                                                if (!flag)
                                                {
                                                    num = 333;
                                                    continue;
                                                }
                                                goto case 375;
                                            case 333:
                                                wap_bbs_BLL.UpdateXiNuHan(siteid, id, "0");
                                                num = 375;
                                                continue;
                                            case 375:
                                                backurl = "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage + stypelink;
                                                bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
                                                flag = bookVo != null;
                                                num = 422;
                                                continue;
                                            case 422:
                                                if (!flag)
                                                {
                                                    num = 110;
                                                    continue;
                                                }
                                                flag = bookVo.ischeck != 1;
                                                num = 178;
                                                continue;
                                            case 110:
                                                ShowTipInfo("可能已删除！或不存在！或已转移至历史表中，点击此<a href=\"" + http_start + "bbs/book_view_bak.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;sid=" + sid + "\">尝试访问历史数据</a>。", backurl);
                                                num = 6;
                                                continue;
                                            case 178:
                                                if (!flag)
                                                {
                                                    num = 58;
                                                    continue;
                                                }
                                                flag = !(bookVo.book_classid.ToString() != classid);
                                                num = 200;
                                                continue;
                                            case 58:
                                                ShowTipInfo("正在审核中！", backurl);
                                                num = 75;
                                                continue;
                                            case 200:
                                                if (!flag)
                                                {
                                                    num = 11;
                                                    continue;
                                                }
                                                flag = bookVo.ischeck != 2;
                                                num = 131;
                                                continue;
                                            case 11:
                                                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
                                                num = 224;
                                                continue;
                                            case 131:
                                                if (!flag)
                                                {
                                                    num = 367;
                                                    continue;
                                                }
                                                goto case 6;
                                            case 367:
                                                CheckManagerLvl("04", classVo.adminusername, "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
                                                num = 48;
                                                continue;
                                            case 6:
                                            case 48:
                                            case 75:
                                            case 224:
                                                bookVo.book_title = WapTool.GetShowImg(bookVo.book_title, "200", "bbs");
                                                wmlVo.title = bookVo.book_title;
                                                wmlVo.id = bookVo.id;
                                                user_BLL = new user_BLL(a);
                                                toUserVo = user_BLL.getUserInfo(bookVo.book_pub, siteid);
                                                flag = toUserVo != null;
                                                num = 449;
                                                continue;
                                            case 449:
                                                if (!flag)
                                                {
                                                    num = 145;
                                                    continue;
                                                }
                                                goto case 409;
                                            case 145:
                                                toUserVo = user_BLL.getUserInfo(siteid, siteid);
                                                toUserVo.nickname = "游客";
                                                toUserVo.city = "火星";
                                                toUserVo.userid = 0L;
                                                toUserVo.remark = "";
                                                num = 409;
                                                continue;
                                            case 409:
                                                {
                                                    http_start_url = http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + stypelink;
                                                    sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                    adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                    flag = bookVo.MarkSixBetID == 0;
                                                    num = 17;
                                                    continue;
                                                }
                                            case 17:
                                                if (!flag)
                                                {
                                                    num = 377;
                                                    continue;
                                                }
                                                goto case 338;
                                            case 377:
                                                {
                                                    wap_bbs_MarkSix_bet_BLL wap_bbs_MarkSix_bet_BLL = new wap_bbs_MarkSix_bet_BLL(a);
                                                    model = new wap_bbs_MarkSix_bet_Model();
                                                    model = wap_bbs_MarkSix_bet_BLL.GetModel(long.Parse(siteid), bookVo.MarkSixBetID);
                                                    flag = model == null;
                                                    num = 314;
                                                    continue;
                                                }
                                            case 314:
                                                if (!flag)
                                                {
                                                    num = 78;
                                                    continue;
                                                }
                                                bookVo.book_content = "投注记录已删除<br/>----------<br/>" + bookVo.book_content;
                                                num = 241;
                                                continue;
                                            case 78:
                                                text9 = "";
                                                text7 = "";
                                                text9 = "【";
                                                text9 = text9 + "<a href=\"" + http_start + "bbs/marksix/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;sid=" + sid + "\">查看</a>";
                                                flag = !(bookVo.userid.ToString() == userid);
                                                num = 392;
                                                continue;
                                            case 392:
                                                if (!flag)
                                                {
                                                    num = 211;
                                                    continue;
                                                }
                                                goto case 90;
                                            case 211:
                                                text9 = text9 + ".<a href=\"" + http_start + "bbs/marksix/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;bbsid=" + id + "&amp;id=" + model.id + "\">修改</a>.<a href=\"" + http_start + "bbs/marksix/book_user_win.aspx?touserid=" + userid + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;sid=" + sid + "\">兑奖</a>";
                                                num = 90;
                                                continue;
                                            case 90:
                                                text9 += "】";
                                                flag = bookVo.MarkSixWin != 1;
                                                num = 72;
                                                continue;
                                            case 72:
                                                if (!flag)
                                                {
                                                    num = 130;
                                                    continue;
                                                }
                                                goto case 434;
                                            case 130:
                                                text7 = "<img src=\"/NetImages/zhong.gif\"alt=\"中\"> ";
                                                num = 434;
                                                continue;
                                            case 434:
                                                bookVo.book_content = text7 + model.peroid + "期投注[" + GamesClassManager.Tool.GetMarkSix_PlayName(model.types.ToString()) + "]为" + model.types_content + " 共" + model.num + "注" + text9 + "<br/>----------<br/>" + bookVo.book_content;
                                                num = 305;
                                                continue;
                                            case 241:
                                            case 305:
                                                num = 338;
                                                continue;
                                            case 338:
                                                content = bookVo.book_content;
                                                content = content.Replace("[id]", id);
                                                flag = !(view != "all");
                                                num = 369;
                                                continue;
                                            case 369:
                                                if (!flag)
                                                {
                                                    num = 74;
                                                    continue;
                                                }
                                                goto case 430;
                                            case 74:
                                                flag = content.IndexOf("[next]") <= 0;
                                                num = 25;
                                                continue;
                                            case 25:
                                                num = (flag ? 265 : 386);
                                                continue;
                                            case 386:
                                                content = content.Replace("[next]", "\uff3e");
                                                array = content.Split('\uff3e');
                                                totalPage = array.Length;
                                                flag = !(array[totalPage - 1] == "");
                                                num = 400;
                                                continue;
                                            case 400:
                                                if (!flag)
                                                {
                                                    num = 405;
                                                    continue;
                                                }
                                                goto case 438;
                                            case 405:
                                                totalPage--;
                                                num = 438;
                                                continue;
                                            case 438:
                                                flag = !(viewLeave != "");
                                                num = 163;
                                                continue;
                                            case 163:
                                                if (!flag)
                                                {
                                                    num = 425;
                                                    continue;
                                                }
                                                content = array[CurrentPage - 1];
                                                num = 350;
                                                continue;
                                            case 425:
                                                flag = int.Parse(viewLeave) >= totalPage;
                                                num = 94;
                                                continue;
                                            case 94:
                                                if (!flag)
                                                {
                                                    num = 437;
                                                    continue;
                                                }
                                                goto case 1;
                                            case 437:
                                                text15 = "";
                                                num2 = int.Parse(viewLeave);
                                                num = 35;
                                                continue;
                                            case 35:
                                            case 326:
                                                flag = num2 < totalPage;
                                                num = 129;
                                                continue;
                                            case 129:
                                                if (flag)
                                                {
                                                    text15 += array[num2];
                                                    num2++;
                                                    num = 326;
                                                }
                                                else
                                                {
                                                    num = 330;
                                                }
                                                continue;
                                            case 330:
                                                content = text15;
                                                num = 1;
                                                continue;
                                            case 1:
                                                num = 379;
                                                continue;
                                            case 350:
                                            case 379:
                                                num = 196;
                                                continue;
                                            case 265:
                                                try
                                                {
                                                    while (true)
                                                    {
                                                    IL_17ef:
                                                        totalPage = content.Length / pageSize;
                                                        flag = content.Length <= totalPage * pageSize;
                                                        num = 18;
                                                        while (true)
                                                        {
                                                            int num23;
                                                            int num24;
                                                            switch (num)
                                                            {
                                                                case 18:
                                                                    if (!flag)
                                                                    {
                                                                        num = 5;
                                                                        continue;
                                                                    }
                                                                    goto case 16;
                                                                case 23:
                                                                    num = ((totalPage <= 1) ? 20 : 15);
                                                                    continue;
                                                                case 11:
                                                                    content = content.Substring(Convert.ToInt32(viewLeave) * pageSize, content.Length - Convert.ToInt32(viewLeave) * pageSize);
                                                                    num = 3;
                                                                    continue;
                                                                case 14:
                                                                    CurrentPage = totalPage;
                                                                    content = content.Substring((CurrentPage - 1) * pageSize, content.Length - (CurrentPage - 1) * pageSize);
                                                                    num = 21;
                                                                    continue;
                                                                case 19:
                                                                    num = ((totalPage <= 1) ? 8 : 0);
                                                                    continue;
                                                                case 7:
                                                                case 21:
                                                                case 27:
                                                                    num = 13;
                                                                    continue;
                                                                case 2:
                                                                    content = content.Substring((CurrentPage - 1) * pageSize, pageSize);
                                                                    num = 7;
                                                                    continue;
                                                                case 8:
                                                                    num23 = 1;
                                                                    goto IL_19b2;
                                                                case 10:
                                                                    num = ((!flag) ? 14 : 23);
                                                                    continue;
                                                                case 22:
                                                                    num = ((!flag) ? 17 : 19);
                                                                    continue;
                                                                case 5:
                                                                    totalPage++;
                                                                    num = 16;
                                                                    continue;
                                                                case 20:
                                                                    num24 = 1;
                                                                    goto IL_1aa9;
                                                                case 15:
                                                                    num = 12;
                                                                    continue;
                                                                case 12:
                                                                    num24 = ((CurrentPage >= totalPage) ? 1 : 0);
                                                                    goto IL_1aa9;
                                                                case 0:
                                                                    num = 25;
                                                                    continue;
                                                                case 25:
                                                                    num23 = ((CurrentPage < totalPage) ? 1 : 0);
                                                                    goto IL_19b2;
                                                                case 17:
                                                                    content = "";
                                                                    num = 27;
                                                                    continue;
                                                                case 6:
                                                                    if (!flag)
                                                                    {
                                                                        num = 2;
                                                                        continue;
                                                                    }
                                                                    goto case 7;
                                                                case 16:
                                                                    flag = !(viewLeave != "");
                                                                    num = 24;
                                                                    continue;
                                                                case 24:
                                                                    if (flag)
                                                                    {
                                                                        flag = CurrentPage <= totalPage;
                                                                        num = 22;
                                                                    }
                                                                    else
                                                                    {
                                                                        num = 26;
                                                                    }
                                                                    continue;
                                                                case 3:
                                                                    num = 1;
                                                                    continue;
                                                                case 26:
                                                                    flag = Convert.ToInt32(viewLeave) * pageSize >= content.Length;
                                                                    num = 4;
                                                                    continue;
                                                                case 4:
                                                                    if (!flag)
                                                                    {
                                                                        num = 11;
                                                                        continue;
                                                                    }
                                                                    goto case 3;
                                                                case 1:
                                                                case 13:
                                                                    num = 9;
                                                                    continue;
                                                                case 9:
                                                                    goto end_IL_1776;
                                                                IL_1aa9:
                                                                    flag = (byte)num24 != 0;
                                                                    num = 6;
                                                                    continue;
                                                                IL_19b2:
                                                                    flag = (byte)num23 != 0;
                                                                    num = 10;
                                                                    continue;
                                                            }
                                                            goto IL_17ef;
                                                        end_IL_1776:
                                                            break;
                                                        }
                                                        break;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    content += WapTool.ErrorToString(ex.ToString());
                                                }
                                                num = 113;
                                                continue;
                                            case 113:
                                            case 196:
                                                flag = !(content != "");
                                                num = 417;
                                                continue;
                                            case 417:
                                                if (!flag)
                                                {
                                                    num = 214;
                                                    continue;
                                                }
                                                goto case 361;
                                            case 214:
                                                content = "<!--listS-->" + content + "<!--listE-->";
                                                num = 361;
                                                continue;
                                            case 361:
                                                content += "<span id=\"KL_show_next_list\"></span>";
                                                linkURL = WapTool.GetPageContentLink(ver, lang, totalPage, pageSize, CurrentPage, http_start_url + "&amp;id=" + id);
                                                num = 430;
                                                continue;
                                            case 430:
                                                flag = bookVo.isdown <= 0;
                                                num = 343;
                                                continue;
                                            case 343:
                                                if (!flag)
                                                {
                                                    num = 378;
                                                    continue;
                                                }
                                                goto case 32;
                                            case 378:
                                                text12 = "";
                                                text14 = WapTool.getArryString(classVo.smallimg, '|', 17);
                                                text13 = WapTool.getArryString(classVo.smallimg, '|', 18);
                                                flag = WapTool.IsNumeric(text14);
                                                num = 313;
                                                continue;
                                            case 313:
                                                if (!flag)
                                                {
                                                    num = 443;
                                                    continue;
                                                }
                                                goto case 218;
                                            case 443:
                                                text14 = "0";
                                                num = 218;
                                                continue;
                                            case 218:
                                                flag = WapTool.IsNumeric(text13);
                                                num = 127;
                                                continue;
                                            case 127:
                                                if (!flag)
                                                {
                                                    num = 181;
                                                    continue;
                                                }
                                                goto case 364;
                                            case 181:
                                                text13 = "0";
                                                num = 364;
                                                continue;
                                            case 364:
                                                flag = long.Parse(text14) <= 0;
                                                num = 122;
                                                continue;
                                            case 122:
                                                if (!flag)
                                                {
                                                    num = 226;
                                                    continue;
                                                }
                                                goto case 57;
                                            case 226:
                                                text12 = "扣" + text14 + "个" + siteVo.sitemoneyname;
                                                num = 57;
                                                continue;
                                            case 57:
                                                flag = long.Parse(text13) <= 0;
                                                num = 77;
                                                continue;
                                            case 77:
                                                if (!flag)
                                                {
                                                    num = 36;
                                                    continue;
                                                }
                                                goto case 287;
                                            case 36:
                                                flag = !(text12 != "");
                                                num = 18;
                                                continue;
                                            case 18:
                                                if (!flag)
                                                {
                                                    num = 76;
                                                    continue;
                                                }
                                                goto case 387;
                                            case 76:
                                                text12 += "/";
                                                num = 387;
                                                continue;
                                            case 387:
                                                text12 = text12 + "送" + text13 + "个" + siteVo.sitemoneyname;
                                                num = 287;
                                                continue;
                                            case 287:
                                                flag = !(text12 != "");
                                                num = 44;
                                                continue;
                                            case 44:
                                                if (!flag)
                                                {
                                                    num = 320;
                                                    continue;
                                                }
                                                goto case 174;
                                            case 320:
                                                text12 = "(" + text12 + ")";
                                                num = 174;
                                                continue;
                                            case 174:
                                                {
                                                    stringBuilder2 = new StringBuilder();
                                                    listVo = null;
                                                    wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(a);
                                                    listVo = wap2_attachment_BLL.GetListVo(" book_type='bbs' and book_id=" + long.Parse(id));
                                                    flag = listVo == null;
                                                    num = 159;
                                                    continue;
                                                }
                                            case 159:
                                                if (!flag)
                                                {
                                                    num = 245;
                                                    continue;
                                                }
                                                goto case 315;
                                            case 245:
                                                stringBuilder2.Append("<div class=\"attachment\"><span class=\"attachmenSum\"><span class=\"attachmentext\">共有</span><span class=\"attachmentlistnum\">" + listVo.Count + "</span><span class=\"attachmentext\">个附件</span><span class=\"attachmentCharge\">");
                                                stringBuilder2.Append(text12);
                                                stringBuilder2.Append("</span></span>");
                                                num = 315;
                                                continue;
                                            case 315:
                                                text = WapTool.getArryString(classVo.smallimg, '|', 33);
                                                flag = WapTool.IsNumeric(text);
                                                num = 444;
                                                continue;
                                            case 444:
                                                if (!flag)
                                                {
                                                    num = 402;
                                                    continue;
                                                }
                                                goto case 24;
                                            case 402:
                                                text = "2";
                                                num = 24;
                                                continue;
                                            case 24:
                                                flag = !(text == "0");
                                                num = 355;
                                                continue;
                                            case 355:
                                                if (!flag)
                                                {
                                                    num = 341;
                                                    continue;
                                                }
                                                goto case 198;
                                            case 341:
                                                text = "1000";
                                                num = 198;
                                                continue;
                                            case 198:
                                                num2 = 0;
                                                num = 168;
                                                continue;
                                            case 272:
                                                if (!flag)
                                                {
                                                    num = 427;
                                                    continue;
                                                }
                                                stringBuilder2.Append("<div class=\"attachmentinfo\">");
                                                stringBuilder2.Append("<span class=\"downloadname\">");
                                                //stringBuilder2.Append(num2 + 1 + "." + listVo[num2].book_title);
                                                stringBuilder2.Append("<span class='attachmentnumber'>" + (num2 + 1) + ".</span>");
                                                stringBuilder2.Append("<span class='attachmentname'><span class='attachmentitle'>" + listVo[num2].book_title + "</span>");
                                                num = 71;
                                                continue;
                                            case 427:
                                                num = 92;
                                                continue;
                                            case 71:
                                                num = ((!(listVo[num2].book_ext.Trim() != "")) ? 202 : 418);
                                                continue;
                                            case 418:
                                                num = 426;
                                                continue;
                                            case 426:
                                                num14 = ((!(listVo[num2].book_ext.Trim() != "mov")) ? 1 : 0);
                                                goto IL_2162;
                                            case 202:
                                                num14 = 1;
                                                goto IL_2162;
                                            case 302:
                                                if (!flag)
                                                {
                                                    num = 266;
                                                    continue;
                                                }
                                                goto case 227;
                                            case 266:
                                                stringBuilder2.Append("<span class=\"FileExtension\">");
                                                stringBuilder2.Append("." + listVo[num2].book_ext);
                                                stringBuilder2.Append("</span></span>");
                                                num = 227;
                                                continue;
                                            case 227:
                                                flag = !(listVo[num2].book_size.Trim() != "");
                                                num = 248;
                                                continue;
                                            case 248:
                                                if (!flag)
                                                {
                                                    num = 318;
                                                    continue;
                                                }
                                                goto case 249;
                                            case 318:
                                                stringBuilder2.Append("<span class=\"attachmentsize\">");
                                                stringBuilder2.Append("(" + listVo[num2].book_size + ")");
                                                num = 249;
                                                continue;
                                            case 249:
                                                stringBuilder2.Append("</span></span>");
                                                num = 161;
                                                continue;
                                            case 161:
                                                num = ((!(listVo[num2].book_ext.Trim() != "")) ? 116 : 335);
                                                continue;
                                            case 335:
                                                num = 360;
                                                continue;
                                            case 360:
                                                num11 = ((".gif|.jpg|.jpeg|.png|.webp|.bmp|".IndexOf(listVo[num2].book_ext.ToLower()) < 0) ? 1 : 0);
                                                goto IL_22be;
                                            case 116:
                                                num11 = 1;
                                                goto IL_22be;
                                            case 390:
                                                num = (flag ? 407 : 406);
                                                continue;
                                            case 406:
                                                text6 = http_start + "bbs/" + listVo[num2].book_file;
                                                flag = !listVo[num2].book_file.ToLower().StartsWith("http");
                                                num = 62;
                                                continue;
                                            case 62:
                                                if (!flag)
                                                {
                                                    num = 319;
                                                    continue;
                                                }
                                                goto case 52;
                                            case 319:
                                                text6 = listVo[num2].book_file;
                                                num = 52;
                                                continue;
                                            case 52:
                                                //stringBuilder2.Append("<a href=\"" + http_start + "bbs/picDIY.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;path=" + HttpUtility.UrlEncode("bbs\\" + listVo[num2].book_file) + "\"><img src=\"" + text6 + "\" " + towidths + " alt=\"" + listVo[num2].book_title + "\"/></a><br/>");
                                                stringBuilder2.Append("<span class=\"attachmentimage\"><a href=\"" + http_start + "" + HttpUtility.UrlEncode("bbs\\" + listVo[num2].book_file) + "\"><img src=\"" + text6 + "\" " + towidths + "/></a></span>");
                                                num = 9;
                                                continue;
                                            case 407:
                                                num = ((!(listVo[num2].book_ext.Trim() != "")) ? 293 : 362);
                                                continue;
                                            case 362:
                                                num = 150;
                                                continue;
                                            case 150:
                                                num8 = ((".mov".IndexOf(listVo[num2].book_ext.ToLower()) < 0) ? 1 : 0);
                                                goto IL_24da;
                                            case 293:
                                                num8 = 1;
                                                goto IL_24da;
                                            case 270:
                                                if (!flag)
                                                {
                                                    num = 232;
                                                    continue;
                                                }
                                                stringBuilder2.Append("<span class=\"downloadlink\"><span class=\"downloadurl\"><a class=\"urlbtn\"  href=\"" + http_start + "bbs/download.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;book_id=" + id + "&amp;id=" + listVo[num2].ID + "&amp;RndPath=" + siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(listVo[num2].book_title) + "." + listVo[num2].book_ext + "\">点击下载</a></span><span class=\"downloadcount\">(" + listVo[num2].book_click + "次)</span></span>");
                                                num = 60;
                                                continue;
                                            case 232:
                                                flag = ".mov|.m3u8|.mp4".IndexOf(WapTool.right(listVo[num2].book_file.ToLower(), 3)) < 0;
                                                num = 84;
                                                continue;
                                            case 84:
                                                if (!flag)
                                                {
                                                    num = 87;
                                                    continue;
                                                }
                                                //stringBuilder2.Append("<iframe width=\"300\" height=\"200\" src=\"" + listVo[num2].book_file + "\" frameborder=\"0\" allowfullscreen=\"\" qbiframeattached=\"true\" style=\"line-height: 2em; font-size: 16px; z-index: 1;\"></iframe><br/><br/>");
                                                num = 88;
                                                continue;
                                            case 87:
                                                stringBuilder2.Append("<span class=\"videoplay\"><video onclick=\"if(this.paused) { this.play();}else{ this.pause();}\" src=\"" + listVo[num2].book_file + "\" width=\"100%\" height=\"100%\" poster=\"/NetImages/play.gif\" controls>{不支持在线播放，请更换浏览器}</video></span>");
                                                stringBuilder2.Append("");
                                                stringBuilder2.Append("<span class=\"downloadurl\"><a class=\"urlbtn\" href=\"" + http_start + "bbs/download.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;book_id=" + id + "&amp;id=" + listVo[num2].ID + "&amp;RndPath=" + siteVo.SaveUpFilesPath + "&amp;n=" + HttpUtility.UrlEncode(listVo[num2].book_title) + "." + listVo[num2].book_ext + "\">点击下载</a></span><span class=\"downloadcount\">(" + listVo[num2].book_click + "次)</span>");
                                                num = 86;
                                                continue;
                                            case 86:
                                            case 88:
                                                num = 380;
                                                continue;
                                            case 9:
                                            case 60:
                                            case 380:
                                                stringBuilder2.Append("<span class=\"attachmentNote\">");
                                                stringBuilder2.Append(listVo[num2].book_content + "");
                                                stringBuilder2.Append("</span>");
                                                stringBuilder2.Append("</div>");
                                                num2++;
                                                num = 157;
                                                continue;
                                            case 157:
                                            case 168:
                                                num = 298;
                                                continue;
                                            case 298:
                                                num = ((listVo == null) ? 123 : 151);
                                                continue;
                                            case 151:
                                                num = 297;
                                                continue;
                                            case 297:
                                                num5 = ((num2 < listVo.Count) ? 1 : 0);
                                                goto IL_2896;
                                            case 123:
                                                num5 = 0;
                                                goto IL_2896;
                                            case 170:
                                                if (flag)
                                                {
                                                    flag = num2 <= int.Parse(text) - 1;
                                                    num = 272;
                                                }
                                                else
                                                {
                                                    num = 51;
                                                }
                                                continue;
                                            case 51:
                                            case 92:
                                                num = 134;
                                                continue;
                                            case 134:
                                                num = ((listVo == null) ? 43 : 172);
                                                continue;
                                            case 172:
                                                num = 277;
                                                continue;
                                            case 277:
                                                num28 = ((listVo.Count <= int.Parse(text) - 1) ? 1 : 0);
                                                goto IL_2915;
                                            case 43:
                                                num28 = 1;
                                                goto IL_2915;
                                            case 397:
                                                if (!flag)
                                                {
                                                    num = 2;
                                                    continue;
                                                }
                                                goto case 391;
                                            case 2:
                                                stringBuilder2.Append("<div class=\"btBox\"><div class=\"bt1\">");
                                                stringBuilder2.Append("<a href=\"" + http_start + "bbs/book_view_showfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + stypelink + "\">{查看所有附件}</a> ");
                                                stringBuilder2.Append("</div></div></div>");
                                                num = 391;
                                                continue;
                                            case 391:
                                                content += stringBuilder2.ToString();
                                                num = 32;
                                                continue;
                                            case 32:
                                                text11 = "";
                                                flag = content.IndexOf("[go]") <= 0;
                                                num = 112;
                                                continue;
                                            case 112:
                                                if (!flag)
                                                {
                                                    num = 274;
                                                    continue;
                                                }
                                                goto case 54;
                                            case 274:
                                                content = content.Replace("[go]", "\uff3e");
                                                text11 = content.Split('\uff3e')[0] + "<br/>";
                                                num = 54;
                                                continue;
                                            case 54:
                                                flag = bookVo.viewtype != 1;
                                                num = 370;
                                                continue;
                                            case 370:
                                                num = (flag ? 144 : 139);
                                                continue;
                                            case 139:
                                                flag = !(userid == "0");
                                                num = 185;
                                                continue;
                                            case 185:
                                                if (!flag)
                                                {
                                                    num = 0;
                                                    continue;
                                                }
                                                goto case 164;
                                            case 0:
                                                content = text11 + "{此处内容需要登录才能查看}";
                                                num = 164;
                                                continue;
                                            case 164:
                                                num = 133;
                                                continue;
                                            case 144:
                                                num = ((bookVo.viewtype != 2) ? 253 : 260);
                                                continue;
                                            case 260:
                                                num = 396;
                                                continue;
                                            case 396:
                                                num27 = (isAdmin ? 1 : 0);
                                                goto IL_2bb5;
                                            case 253:
                                                num27 = 1;
                                                goto IL_2bb5;
                                            case 13:
                                                num = (flag ? 4 : 284);
                                                continue;
                                            case 284:
                                                num = 242;
                                                continue;
                                            case 242:
                                                if (!WapTool.isAllowUA(UA))
                                                {
                                                    num = 222;
                                                    continue;
                                                }
                                                goto IL_2c5c;
                                            case 222:
                                                num = 381;
                                                continue;
                                            case 381:
                                                if (!WapTool.isAllowIP(IP))
                                                {
                                                    num = 66;
                                                    continue;
                                                }
                                                goto IL_2c5c;
                                            case 66:
                                                num = 291;
                                                continue;
                                            case 291:
                                                num26 = ((!(userid != bookVo.book_pub)) ? 1 : 0);
                                                goto IL_2c6d;
                                            case 162:
                                                num26 = 1;
                                                goto IL_2c6d;
                                            case 85:
                                                if (!flag)
                                                {
                                                    num = 15;
                                                    continue;
                                                }
                                                goto case 182;
                                            case 15:
                                                content = text11 + "{此处内容需要手机访问才能查看，当前UA+IP:" + UA + " " + IP + "}";
                                                num = 182;
                                                continue;
                                            case 182:
                                                num = 21;
                                                continue;
                                            case 4:
                                                if (bookVo.viewtype == 3)
                                                {
                                                    num = 140;
                                                    continue;
                                                }
                                                goto IL_2d7a;
                                            case 140:
                                                num = 183;
                                                continue;
                                            case 183:
                                                if (userid != bookVo.book_pub)
                                                {
                                                    num = 285;
                                                    continue;
                                                }
                                                goto IL_2d7a;
                                            case 285:
                                                num = 177;
                                                continue;
                                            case 177:
                                                num25 = (isAdmin ? 1 : 0);
                                                goto IL_2d8b;
                                            case 213:
                                                num25 = 1;
                                                goto IL_2d8b;
                                            case 70:
                                                num = (flag ? 412 : 429);
                                                continue;
                                            case 429:
                                                num = 120;
                                                continue;
                                            case 120:
                                                num = (WapTool.isHasReply(siteid, userid, id) ? 256 : 19);
                                                continue;
                                            case 19:
                                                num = 354;
                                                continue;
                                            case 354:
                                                num22 = (CheckManagerLvl("04", classVo.adminusername) ? 1 : 0);
                                                goto IL_2e21;
                                            case 256:
                                                num22 = 1;
                                                goto IL_2e21;
                                            case 121:
                                                if (!flag)
                                                {
                                                    num = 251;
                                                    continue;
                                                }
                                                goto case 230;
                                            case 251:
                                                content = text11 + "{此处内容需要回复才能查看}";
                                                num = 230;
                                                continue;
                                            case 230:
                                                num = 307;
                                                continue;
                                            case 412:
                                                if (bookVo.viewtype == 4)
                                                {
                                                    num = 98;
                                                    continue;
                                                }
                                                goto IL_2ef6;
                                            case 98:
                                                num = 246;
                                                continue;
                                            case 246:
                                                if (userid != bookVo.book_pub)
                                                {
                                                    num = 189;
                                                    continue;
                                                }
                                                goto IL_2ef6;
                                            case 189:
                                                num = 389;
                                                continue;
                                            case 389:
                                                num21 = (isAdmin ? 1 : 0);
                                                goto IL_2f07;
                                            case 187:
                                                num21 = 1;
                                                goto IL_2f07;
                                            case 221:
                                                num = (flag ? 132 : 332);
                                                continue;
                                            case 332:
                                                flag = userVo.money >= bookVo.viewmoney;
                                                num = 46;
                                                continue;
                                            case 46:
                                                if (!flag)
                                                {
                                                    num = 275;
                                                    continue;
                                                }
                                                goto case 385;
                                            case 275:
                                                content = text11 + "{此处内容需要您拥有" + siteVo.sitemoneyname + ":" + bookVo.viewmoney + "才能查看}";
                                                num = 385;
                                                continue;
                                            case 385:
                                                num = 65;
                                                continue;
                                            case 132:
                                                if (bookVo.viewtype == 5)
                                                {
                                                    num = 432;
                                                    continue;
                                                }
                                                goto IL_3066;
                                            case 432:
                                                num = 209;
                                                continue;
                                            case 209:
                                                if (userid != bookVo.book_pub)
                                                {
                                                    num = 206;
                                                    continue;
                                                }
                                                goto IL_3066;
                                            case 206:
                                                num = 282;
                                                continue;
                                            case 282:
                                                num20 = (isAdmin ? 1 : 0);
                                                goto IL_3077;
                                            case 334:
                                                num20 = 1;
                                                goto IL_3077;
                                            case 136:
                                                num = (flag ? 401 : 229);
                                                continue;
                                            case 229:
                                                flag = userVo.expr >= bookVo.viewmoney;
                                                num = 3;
                                                continue;
                                            case 3:
                                                if (!flag)
                                                {
                                                    num = 126;
                                                    continue;
                                                }
                                                goto case 446;
                                            case 126:
                                                content = text11 + "{此处内容需要您拥有经验:" + bookVo.viewmoney + "才能查看}";
                                                num = 446;
                                                continue;
                                            case 446:
                                                num = 288;
                                                continue;
                                            case 401:
                                                if (bookVo.viewtype == 6)
                                                {
                                                    num = 289;
                                                    continue;
                                                }
                                                goto IL_31be;
                                            case 289:
                                                num = 63;
                                                continue;
                                            case 63:
                                                if (userid != bookVo.book_pub)
                                                {
                                                    num = 441;
                                                    continue;
                                                }
                                                goto IL_31be;
                                            case 441:
                                                num = 225;
                                                continue;
                                            case 225:
                                                num19 = (isAdmin ? 1 : 0);
                                                goto IL_31cf;
                                            case 208:
                                                num19 = 1;
                                                goto IL_31cf;
                                            case 49:
                                                num = (flag ? 39 : 102);
                                                continue;
                                            case 102:
                                                text8 = WapTool.getArryString(siteVo.Version, '|', 22);
                                                flag = WapTool.IsNumeric(text8);
                                                num = 14;
                                                continue;
                                            case 14:
                                                if (!flag)
                                                {
                                                    num = 107;
                                                    continue;
                                                }
                                                goto case 408;
                                            case 107:
                                                text8 = "0";
                                                num = 408;
                                                continue;
                                            case 408:
                                                flag = long.Parse(text8) >= 2;
                                                num = 271;
                                                continue;
                                            case 271:
                                                if (!flag)
                                                {
                                                    num = 382;
                                                    continue;
                                                }
                                                goto case 191;
                                            case 382:
                                                text8 = "1000";
                                                num = 191;
                                                continue;
                                            case 191:
                                                flag = bookVo.viewmoney <= long.Parse(text8);
                                                num = 349;
                                                continue;
                                            case 349:
                                                if (!flag)
                                                {
                                                    num = 215;
                                                    continue;
                                                }
                                                goto case 101;
                                            case 215:
                                                bookVo.viewmoney = long.Parse(text8);
                                                num = 101;
                                                continue;
                                            case 101:
                                                flag = bookVo.viewmoney >= 0;
                                                num = 47;
                                                continue;
                                            case 47:
                                                if (!flag)
                                                {
                                                    num = 301;
                                                    continue;
                                                }
                                                goto case 146;
                                            case 301:
                                                bookVo.viewmoney = 0L;
                                                num = 146;
                                                continue;
                                            case 146:
                                                text5 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                                                num = 340;
                                                continue;
                                            case 340:
                                                if (text5.IndexOf("BBSNEED" + id) < 0)
                                                {
                                                    num = 153;
                                                    continue;
                                                }
                                                goto IL_33f8;
                                            case 153:
                                                num = 22;
                                                continue;
                                            case 22:
                                                if (GetRequestValue("buy") == "yes")
                                                {
                                                    num = 179;
                                                    continue;
                                                }
                                                goto IL_33f8;
                                            case 179:
                                                num = 59;
                                                continue;
                                            case 59:
                                                num18 = ((userVo.money < bookVo.viewmoney) ? 1 : 0);
                                                goto IL_3409;
                                            case 347:
                                                num18 = 1;
                                                goto IL_3409;
                                            case 376:
                                                if (!flag)
                                                {
                                                    num = 64;
                                                    continue;
                                                }
                                                goto case 290;
                                            case 64:
                                                needPassWordToAdmin();
                                                text5 = text5 + "BBSNEED" + id;
                                                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + text5 + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                                                MainBll.UpdateSQL("update [user] set money=money-" + bookVo.viewmoney + " where userid=" + userVo.userid);
                                                MainBll.UpdateSQL("update [user] set money=money+" + bookVo.viewmoney + " where userid=" + bookVo.book_pub);
                                                SaveBankLog(userid.ToString(), "购买帖子", "-" + bookVo.viewmoney, userid, nickname, "购买贴子[ID:" + id + "]");
                                                SaveBankLog(bookVo.book_pub.ToString(), "帖子赚币", bookVo.viewmoney.ToString(), userid, nickname, "操作人购买你的贴子[ID:" + id + "]");
                                                num = 290;
                                                continue;
                                            case 290:
                                                flag = text5.IndexOf("BBSNEED" + id) >= 0;
                                                num = 83;
                                                continue;
                                            case 83:
                                                if (!flag)
                                                {
                                                    num = 329;
                                                    continue;
                                                }
                                                goto case 81;
                                            case 329:
                                                content = text11 + "{此处内容需要支付" + siteVo.sitemoneyname + ":" + bookVo.viewmoney + "才能查看，";
                                                flag = !(userid == "0");
                                                num = 148;
                                                continue;
                                            case 148:
                                                if (!flag)
                                                {
                                                    num = 371;
                                                    continue;
                                                }
                                                flag = userVo.money >= bookVo.viewmoney;
                                                num = 31;
                                                continue;
                                            case 371:
                                                content = content + "请先<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "\">登录网站</a>！}";
                                                num = 239;
                                                continue;
                                            case 31:
                                                if (!flag)
                                                {
                                                    num = 115;
                                                    continue;
                                                }
                                                content = content + "<a href=\"" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes\">确认支付</a>}";
                                                num = 247;
                                                continue;
                                            case 115:
                                                content = content + "你只有:" + userVo.money + "，不能购买！}";
                                                num = 276;
                                                continue;
                                            case 239:
                                            case 247:
                                            case 276:
                                                num = 81;
                                                continue;
                                            case 81:
                                                num = 56;
                                                continue;
                                            case 39:
                                                if (bookVo.viewtype == 7)
                                                {
                                                    num = 138;
                                                    continue;
                                                }
                                                goto IL_3971;
                                            case 138:
                                                num = 141;
                                                continue;
                                            case 141:
                                                if (userid != bookVo.book_pub)
                                                {
                                                    num = 12;
                                                    continue;
                                                }
                                                goto IL_3971;
                                            case 12:
                                                num = 268;
                                                continue;
                                            case 268:
                                                num17 = (isAdmin ? 1 : 0);
                                                goto IL_3982;
                                            case 180:
                                                num17 = 1;
                                                goto IL_3982;
                                            case 93:
                                                if (!flag)
                                                {
                                                    num = 258;
                                                    continue;
                                                }
                                                goto case 21;
                                            case 258:
                                                text8 = WapTool.getArryString(siteVo.Version, '|', 22);
                                                flag = WapTool.IsNumeric(text8);
                                                num = 296;
                                                continue;
                                            case 296:
                                                if (!flag)
                                                {
                                                    num = 440;
                                                    continue;
                                                }
                                                goto case 308;
                                            case 440:
                                                text8 = "0";
                                                num = 308;
                                                continue;
                                            case 308:
                                                flag = long.Parse(text8) >= 2;
                                                num = 388;
                                                continue;
                                            case 388:
                                                if (!flag)
                                                {
                                                    num = 103;
                                                    continue;
                                                }
                                                goto case 309;
                                            case 103:
                                                text8 = "1000";
                                                num = 309;
                                                continue;
                                            case 309:
                                                flag = bookVo.viewmoney <= long.Parse(text8);
                                                num = 244;
                                                continue;
                                            case 244:
                                                if (!flag)
                                                {
                                                    num = 193;
                                                    continue;
                                                }
                                                goto case 279;
                                            case 193:
                                                bookVo.viewmoney = long.Parse(text8);
                                                num = 279;
                                                continue;
                                            case 279:
                                                flag = bookVo.viewmoney >= 0;
                                                num = 393;
                                                continue;
                                            case 393:
                                                if (!flag)
                                                {
                                                    num = 395;
                                                    continue;
                                                }
                                                goto case 50;
                                            case 395:
                                                bookVo.viewmoney = 0L;
                                                num = 50;
                                                continue;
                                            case 50:
                                                text5 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                                                num = 34;
                                                continue;
                                            case 34:
                                                if (text5.IndexOf("BBSNEEDRMB" + id) < 0)
                                                {
                                                    num = 156;
                                                    continue;
                                                }
                                                goto IL_3bb6;
                                            case 156:
                                                num = 28;
                                                continue;
                                            case 28:
                                                if (GetRequestValue("buy") == "yes")
                                                {
                                                    num = 142;
                                                    continue;
                                                }
                                                goto IL_3bb6;
                                            case 142:
                                                num = 374;
                                                continue;
                                            case 374:
                                                num16 = ((!(userVo.RMB >= (decimal)bookVo.viewmoney)) ? 1 : 0);
                                                goto IL_3bc7;
                                            case 416:
                                                num16 = 1;
                                                goto IL_3bc7;
                                            case 237:
                                                if (!flag)
                                                {
                                                    num = 311;
                                                    continue;
                                                }
                                                goto case 368;
                                            case 311:
                                                {
                                                    needPassWordToAdmin();
                                                    text5 = text5 + "BBSNEEDRMB" + id;
                                                    MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + text5 + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                                                    MainBll.UpdateSQL("update [user] set rmb=rmb-" + bookVo.viewmoney + " where userid=" + userVo.userid);
                                                    MainBll.UpdateSQL("update [user] set rmb=rmb+" + bookVo.viewmoney + " where userid=" + bookVo.book_pub);
                                                    string orderID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + userid;
                                                    SaveRMBLog(userid, "-5", "-" + bookVo.viewmoney, userid, nickname, "购买论坛内容[" + id + "]", orderID);
                                                    SaveRMBLog(bookVo.book_pub, "5", bookVo.viewmoney.ToString(), userid, nickname, "操作人购买你的论坛内容[" + id + "]", orderID);
                                                    num = 368;
                                                    continue;
                                                }
                                            case 368:
                                                flag = text5.IndexOf("BBSNEEDRMB" + id) >= 0;
                                                num = 207;
                                                continue;
                                            case 207:
                                                if (!flag)
                                                {
                                                    num = 317;
                                                    continue;
                                                }
                                                goto case 254;
                                            case 317:
                                                content = text11 + "{此处内容需要支付RMB￥:" + bookVo.viewmoney + "才能查看，";
                                                flag = !(userid == "0");
                                                num = 310;
                                                continue;
                                            case 310:
                                                if (!flag)
                                                {
                                                    num = 233;
                                                    continue;
                                                }
                                                flag = !(userVo.RMB < (decimal)bookVo.viewmoney);
                                                num = 108;
                                                continue;
                                            case 233:
                                                content = content + "请先<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "\">登录网站</a>！}";
                                                num = 68;
                                                continue;
                                            case 108:
                                                if (!flag)
                                                {
                                                    num = 359;
                                                    continue;
                                                }
                                                content = content + "<a href=\"" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes\">确认支付</a>}";
                                                num = 99;
                                                continue;
                                            case 359:
                                                content = content + "你只有￥:" + userVo.RMB.ToString("f2") + "，不能购买！<a href=\"" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid + "\">点击此在线充值</a>。}";
                                                num = 262;
                                                continue;
                                            case 68:
                                            case 99:
                                            case 262:
                                                num = 254;
                                                continue;
                                            case 254:
                                                num = 243;
                                                continue;
                                            case 21:
                                            case 56:
                                            case 65:
                                            case 133:
                                            case 243:
                                            case 288:
                                            case 307:
                                                flag = !isAdmin;
                                                num = 356;
                                                continue;
                                            case 356:
                                                if (!flag)
                                                {
                                                    num = 398;
                                                    continue;
                                                }
                                                goto case 428;
                                            case 398:
                                                content = content.Replace("[/reply]", "［/reply(管理员查看中)］");
                                                content = content.Replace("[/buy]", "［/buy(管理员查看中)］");
                                                content = content.Replace("[/buyrmb]", "［/buyrmb(管理员查看中)］");
                                                content = content.Replace("[/buyrmb]", "［/buyrmb(管理员查看中)］");
                                                num = 428;
                                                continue;
                                            case 428:
                                                text10 = "0";
                                                flag = content.IndexOf("[/reply]") <= 0;
                                                num = 166;
                                                continue;
                                            case 166:
                                                if (!flag)
                                                {
                                                    num = 292;
                                                    continue;
                                                }
                                                goto case 439;
                                            case 292:
                                                text10 = "1";
                                                num = 439;
                                                continue;
                                            case 439:
                                                flag = content.IndexOf("[/buy]") <= 0;
                                                num = 45;
                                                continue;
                                            case 45:
                                                if (!flag)
                                                {
                                                    num = 339;
                                                    continue;
                                                }
                                                goto case 176;
                                            case 339:
                                                text10 = "2";
                                                num = 176;
                                                continue;
                                            case 176:
                                                flag = content.IndexOf("[/buyrmb]") <= 0;
                                                num = 345;
                                                continue;
                                            case 345:
                                                if (!flag)
                                                {
                                                    num = 415;
                                                    continue;
                                                }
                                                goto case 143;
                                            case 415:
                                                text10 = "3";
                                                num = 143;
                                                continue;
                                            case 143:
                                                flag = !(text10 != "0");
                                                num = 278;
                                                continue;
                                            case 278:
                                                if (!flag)
                                                {
                                                    num = 286;
                                                    continue;
                                                }
                                                goto case 327;
                                            case 286:
                                                content = WapTool.ToWML(content, wmlVo);
                                                num = 327;
                                                continue;
                                            case 327:
                                                flag = !(text10 == "1");
                                                num = 294;
                                                continue;
                                            case 294:
                                                if (!flag)
                                                {
                                                    num = 413;
                                                    continue;
                                                }
                                                goto case 300;
                                            case 413:
                                                regex = new Regex("(\\[reply\\])(.[^\\[]*)(\\[\\/reply\\])");
                                                num = 160;
                                                continue;
                                            case 160:
                                                if (!WapTool.isHasReply(siteid, userid, id))
                                                {
                                                    num = 435;
                                                    continue;
                                                }
                                                goto IL_43d7;
                                            case 435:
                                                num = 447;
                                                continue;
                                            case 447:
                                                if (!CheckManagerLvl("04", classVo.adminusername))
                                                {
                                                    num = 363;
                                                    continue;
                                                }
                                                goto IL_43d7;
                                            case 363:
                                                num = 96;
                                                continue;
                                            case 96:
                                                num15 = ((!(userid != bookVo.book_pub)) ? 1 : 0);
                                                goto IL_43e8;
                                            case 331:
                                                num15 = 1;
                                                goto IL_43e8;
                                            case 410:
                                                if (!flag)
                                                {
                                                    num = 372;
                                                    continue;
                                                }
                                                content = regex.Replace(content, "$2");
                                                num = 273;
                                                continue;
                                            case 372:
                                                content = regex.Replace(content, "{此处内容须回复后才能浏览}");
                                                num = 119;
                                                continue;
                                            case 119:
                                            case 273:
                                                num = 300;
                                                continue;
                                            case 300:
                                                flag = !(text10 == "2");
                                                num = 199;
                                                continue;
                                            case 199:
                                                if (!flag)
                                                {
                                                    num = 137;
                                                    continue;
                                                }
                                                goto case 82;
                                            case 137:
                                                {
                                                    regex = new Regex("(\\[buy=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/buy\\])");
                                                    Match match = regex.Match(content);
                                                    text4 = match.Groups[2].Value;
                                                    flag = WapTool.IsNumeric(text4);
                                                    num = 267;
                                                    continue;
                                                }
                                            case 267:
                                                if (!flag)
                                                {
                                                    num = 42;
                                                    continue;
                                                }
                                                goto case 373;
                                            case 42:
                                                text4 = "0";
                                                num = 373;
                                                continue;
                                            case 373:
                                                text8 = WapTool.getArryString(siteVo.Version, '|', 22);
                                                flag = WapTool.IsNumeric(text8);
                                                num = 89;
                                                continue;
                                            case 89:
                                                if (!flag)
                                                {
                                                    num = 240;
                                                    continue;
                                                }
                                                goto case 95;
                                            case 240:
                                                text8 = "0";
                                                num = 95;
                                                continue;
                                            case 95:
                                                flag = long.Parse(text8) >= 2;
                                                num = 27;
                                                continue;
                                            case 27:
                                                if (!flag)
                                                {
                                                    num = 104;
                                                    continue;
                                                }
                                                goto case 79;
                                            case 104:
                                                text8 = "1000";
                                                num = 79;
                                                continue;
                                            case 79:
                                                flag = long.Parse(text4) <= long.Parse(text8);
                                                num = 321;
                                                continue;
                                            case 321:
                                                if (!flag)
                                                {
                                                    num = 235;
                                                    continue;
                                                }
                                                goto case 299;
                                            case 235:
                                                text4 = text8;
                                                num = 299;
                                                continue;
                                            case 299:
                                                text5 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                                                num = 165;
                                                continue;
                                            case 165:
                                                if (text5.IndexOf("BBSNEED" + id) < 0)
                                                {
                                                    num = 118;
                                                    continue;
                                                }
                                                goto IL_46ad;
                                            case 118:
                                                num = 184;
                                                continue;
                                            case 184:
                                                if (GetRequestValue("buy") == "yes")
                                                {
                                                    num = 38;
                                                    continue;
                                                }
                                                goto IL_46ad;
                                            case 38:
                                                num = 403;
                                                continue;
                                            case 403:
                                                num13 = ((userVo.money < long.Parse(text4)) ? 1 : 0);
                                                goto IL_46be;
                                            case 204:
                                                num13 = 1;
                                                goto IL_46be;
                                            case 312:
                                                if (!flag)
                                                {
                                                    num = 20;
                                                    continue;
                                                }
                                                goto case 135;
                                            case 20:
                                                needPassWordToAdmin();
                                                text5 = text5 + "BBSNEED" + id;
                                                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + text5 + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                                                MainBll.UpdateSQL("update [user] set money=money-" + text4 + " where userid=" + userVo.userid);
                                                MainBll.UpdateSQL("update [user] set money=money+" + text4 + " where userid=" + bookVo.book_pub);
                                                SaveBankLog(userid.ToString(), "购买帖子", "-" + text4.ToString(), userid, nickname, "购买贴子[ID:" + id + "]");
                                                SaveBankLog(bookVo.book_pub.ToString(), "帖子赚币", text4.ToString(), userid, nickname, "操作人购买你的贴子[ID:" + id + "]");
                                                num = 135;
                                                continue;
                                            case 135:
                                                num = 203;
                                                continue;
                                            case 203:
                                                num = ((!(userid != bookVo.book_pub)) ? 169 : 404);
                                                continue;
                                            case 404:
                                                num = 348;
                                                continue;
                                            case 348:
                                                num12 = ((text5.IndexOf("BBSNEED" + id) >= 0) ? 1 : 0);
                                                goto IL_48e1;
                                            case 169:
                                                num12 = 1;
                                                goto IL_48e1;
                                            case 324:
                                                if (!flag)
                                                {
                                                    num = 117;
                                                    continue;
                                                }
                                                content = regex.Replace(content, "$3");
                                                num = 357;
                                                continue;
                                            case 117:
                                                text3 = "{此处内容需要支付" + siteVo.sitemoneyname + ":" + text4 + "个才能浏览，";
                                                flag = !(userid == "0");
                                                num = 23;
                                                continue;
                                            case 23:
                                                if (!flag)
                                                {
                                                    num = 147;
                                                    continue;
                                                }
                                                flag = userVo.money >= long.Parse(text4);
                                                num = 188;
                                                continue;
                                            case 147:
                                                text3 = text3 + "请先<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "\">登录网站</a>！}";
                                                num = 201;
                                                continue;
                                            case 188:
                                                if (!flag)
                                                {
                                                    num = 212;
                                                    continue;
                                                }
                                                text3 = text3 + "<a href=\"" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes\">确认支付</a>}";
                                                num = 316;
                                                continue;
                                            case 212:
                                                text3 = text3 + "你只有:" + userVo.money + "，不能购买！}";
                                                num = 337;
                                                continue;
                                            case 201:
                                            case 316:
                                            case 337:
                                                content = regex.Replace(content, text3);
                                                num = 216;
                                                continue;
                                            case 216:
                                            case 357:
                                                num = 82;
                                                continue;
                                            case 82:
                                                flag = !(text10 == "3");
                                                num = 7;
                                                continue;
                                            case 7:
                                                if (!flag)
                                                {
                                                    num = 194;
                                                    continue;
                                                }
                                                goto case 325;
                                            case 194:
                                                {
                                                    regex = new Regex("(\\[buyrmb=(.[^\\]]*)\\])(.[^\\[]*)(\\[\\/buyrmb\\])");
                                                    Match match = regex.Match(content);
                                                    text4 = match.Groups[2].Value;
                                                    flag = WapTool.IsNumeric(text4);
                                                    num = 414;
                                                    continue;
                                                }
                                            case 414:
                                                if (!flag)
                                                {
                                                    num = 195;
                                                    continue;
                                                }
                                                goto case 154;
                                            case 195:
                                                text4 = "0";
                                                num = 154;
                                                continue;
                                            case 154:
                                                text8 = WapTool.getArryString(siteVo.Version, '|', 22);
                                                flag = WapTool.IsNumeric(text8);
                                                num = 55;
                                                continue;
                                            case 55:
                                                if (!flag)
                                                {
                                                    num = 328;
                                                    continue;
                                                }
                                                goto case 69;
                                            case 328:
                                                text8 = "0";
                                                num = 69;
                                                continue;
                                            case 69:
                                                flag = long.Parse(text8) >= 2;
                                                num = 175;
                                                continue;
                                            case 175:
                                                if (!flag)
                                                {
                                                    num = 280;
                                                    continue;
                                                }
                                                goto case 352;
                                            case 280:
                                                text8 = "1000";
                                                num = 352;
                                                continue;
                                            case 352:
                                                flag = long.Parse(text4) <= long.Parse(text8);
                                                num = 125;
                                                continue;
                                            case 125:
                                                if (!flag)
                                                {
                                                    num = 124;
                                                    continue;
                                                }
                                                goto case 257;
                                            case 124:
                                                text4 = text8;
                                                num = 257;
                                                continue;
                                            case 257:
                                                text5 = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                                                num = 411;
                                                continue;
                                            case 411:
                                                if (text5.IndexOf("BBSNEEDRMB" + id) < 0)
                                                {
                                                    num = 186;
                                                    continue;
                                                }
                                                goto IL_4e03;
                                            case 186:
                                                num = 217;
                                                continue;
                                            case 217:
                                                if (GetRequestValue("buy") == "yes")
                                                {
                                                    num = 448;
                                                    continue;
                                                }
                                                goto IL_4e03;
                                            case 448:
                                                num = 445;
                                                continue;
                                            case 445:
                                                num10 = ((!(userVo.RMB >= (decimal)long.Parse(text4))) ? 1 : 0);
                                                goto IL_4e14;
                                            case 419:
                                                num10 = 1;
                                                goto IL_4e14;
                                            case 167:
                                                if (!flag)
                                                {
                                                    num = 346;
                                                    continue;
                                                }
                                                goto case 91;
                                            case 346:
                                                {
                                                    needPassWordToAdmin();
                                                    text5 = text5 + "BBSNEEDRMB" + id;
                                                    MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + text5 + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                                                    MainBll.UpdateSQL("update [user] set rmb=rmb-" + text4 + " where userid=" + userVo.userid);
                                                    MainBll.UpdateSQL("update [user] set rmb=rmb+" + text4 + " where userid=" + bookVo.book_pub);
                                                    string orderID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + userid;
                                                    SaveRMBLog(userid, "-5", "-" + text4.ToString(), userid, nickname, "购买论坛内容[" + id + "]", orderID);
                                                    SaveRMBLog(bookVo.book_pub, "5", text4.ToString(), userid, nickname, "操作人购买你的论坛内容[" + id + "]", orderID);
                                                    num = 91;
                                                    continue;
                                                }
                                            case 91:
                                                num = 424;
                                                continue;
                                            case 424:
                                                num = ((!(userid != bookVo.book_pub)) ? 41 : 283);
                                                continue;
                                            case 283:
                                                num = 269;
                                                continue;
                                            case 269:
                                                num9 = ((text5.IndexOf("BBSNEEDRMB" + id) >= 0) ? 1 : 0);
                                                goto IL_5056;
                                            case 41:
                                                num9 = 1;
                                                goto IL_5056;
                                            case 259:
                                                if (!flag)
                                                {
                                                    num = 29;
                                                    continue;
                                                }
                                                content = regex.Replace(content, "$3");
                                                num = 263;
                                                continue;
                                            case 29:
                                                text3 = "{此处内容需要支付RMB￥:" + text4 + "个才能浏览，";
                                                flag = !(userid == "0");
                                                num = 250;
                                                continue;
                                            case 250:
                                                if (!flag)
                                                {
                                                    num = 423;
                                                    continue;
                                                }
                                                flag = !(userVo.RMB < (decimal)long.Parse(text4));
                                                num = 106;
                                                continue;
                                            case 423:
                                                text3 = text3 + "请先<a href=\"" + http_start + "waplogin.aspx?siteid=" + siteid + "&amp;sid=" + sid + "&amp;backurl=" + HttpUtility.UrlEncode("bbs/book_view.aspx?sitid=" + siteid + "&classid=" + classid + "&id=" + id) + "\">登录网站</a>！}";
                                                num = 149;
                                                continue;
                                            case 106:
                                                if (!flag)
                                                {
                                                    num = 358;
                                                    continue;
                                                }
                                                text3 = text3 + "<a href=\"" + http_start + "bbs/book_view.aspx?sitid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage + "&amp;buy=yes\">确认支付</a>}";
                                                num = 383;
                                                continue;
                                            case 358:
                                                text3 = text3 + "你只有:" + userVo.RMB.ToString("f2") + "，不能购买！<a href=\"" + http_start + "chinabank_wap/selbank_wap.aspx?siteid=" + siteid + "\">点击此在线充值</a>。}";
                                                num = 37;
                                                continue;
                                            case 37:
                                            case 149:
                                            case 383:
                                                content = regex.Replace(content, text3);
                                                num = 344;
                                                continue;
                                            case 263:
                                            case 344:
                                                num = 325;
                                                continue;
                                            case 325:
                                                content = content.Replace("[next]", "");
                                                content = content.Replace("[go]", "");
                                                content = content.Replace("\uff3e", "");
                                                KL_ShowPreNextTitle_bbs = WapTool.GetSystemAndMyConfig(KL_ShowPreNextTitle_bbs, WapTool.getArryString(classVo.smallimg, '|', 15));
                                                flag = "1".Equals(KL_ShowPreNextTitle_bbs);
                                                num = 323;
                                                continue;
                                            case 323:
                                                if (!flag)
                                                {
                                                    num = 171;
                                                    continue;
                                                }
                                                preNextTitle.Append("");
                                                num = 238;
                                                continue;
                                            case 171:
                                                text2 = "";
                                                flag = !(stype != "");
                                                num = 336;
                                                continue;
                                            case 336:
                                                if (!flag)
                                                {
                                                    num = 155;
                                                    continue;
                                                }
                                                text2 = classid;
                                                num = 433;
                                                continue;
                                            case 155:
                                                text2 = classid + " and topic=" + stype;
                                                num = 100;
                                                continue;
                                            case 100:
                                            case 433:
                                                preNextTitle = wap_bbs_BLL.GetPreNextTitle(ver, lang, http_start_url, siteid, text2, id, "desc");
                                                num = 281;
                                                continue;
                                            case 238:
                                            case 281:
                                                flag = !"1".Equals(WapTool.KL_OpenCache);
                                                num = 197;
                                                continue;
                                            case 197:
                                                if (!flag)
                                                {
                                                    num = 111;
                                                    continue;
                                                }
                                                goto case 295;
                                            case 111:
                                                WapTool.DataBBSReArray.TryGetValue("bbsRe" + siteid + id, out relistVo);
                                                num = 295;
                                                continue;
                                            case 295:
                                                flag = relistVo != null;
                                                num = 442;
                                                continue;
                                            case 442:
                                                if (!flag)
                                                {
                                                    num = 450;
                                                    continue;
                                                }
                                                goto case 236;
                                            case 450:
                                                wap_bbsre_BLL = new wap_bbsre_BLL(a);
                                                relistVoTop = wap_bbsre_BLL.GetListTopVo("  devid='" + siteid + "' and bookid=" + id + " and ischeck=0 and book_top=1 ", 1);
                                                num7 = 10L;
                                                flag = classVo.ismodel >= 1;
                                                num = 255;
                                                continue;
                                            case 255:
                                                if (!flag)
                                                {
                                                    num = 384;
                                                    continue;
                                                }
                                                num7 = Convert.ToInt32(classVo.ismodel);
                                                num = 261;
                                                continue;
                                            case 384:
                                                num7 = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                                num = 53;
                                                continue;
                                            case 53:
                                            case 261:
                                                relistVo = wap_bbsre_BLL.GetListVo(num7, 1L, "  devid='" + siteid + "' and bookid=" + id + " and ischeck=0 and book_top=0 ", "*", "id", bookVo.book_re, 1);
                                                num2 = 0;
                                                num = 128;
                                                continue;
                                            case 351:
                                                if (!flag)
                                                {
                                                    num = 173;
                                                    continue;
                                                }
                                                relistVo = relistVoTop;
                                                num = 252;
                                                continue;
                                            case 173:
                                                relistVo.Insert(0, relistVoTop[num2]);
                                                flag = num2 <= 10;
                                                num = 158;
                                                continue;
                                            case 158:
                                                num = (flag ? 205 : 190);
                                                continue;
                                            case 190:
                                                num = 33;
                                                continue;
                                            case 205:
                                                num2++;
                                                num = 210;
                                                continue;
                                            case 128:
                                            case 210:
                                                num = 16;
                                                continue;
                                            case 16:
                                                num = ((relistVoTop == null) ? 231 : 353);
                                                continue;
                                            case 353:
                                                num = 366;
                                                continue;
                                            case 366:
                                                num6 = ((num2 < relistVoTop.Count) ? 1 : 0);
                                                goto IL_580a;
                                            case 231:
                                                num6 = 0;
                                                goto IL_580a;
                                            case 61:
                                                if (flag)
                                                {
                                                    flag = relistVo == null;
                                                    num = 351;
                                                }
                                                else
                                                {
                                                    num = 5;
                                                }
                                                continue;
                                            case 5:
                                            case 33:
                                            case 252:
                                                flag = !"1".Equals(WapTool.KL_OpenCache);
                                                num = 109;
                                                continue;
                                            case 109:
                                                if (!flag)
                                                {
                                                    num = 97;
                                                    continue;
                                                }
                                                goto case 219;
                                            case 97:
                                                num = 30;
                                                continue;
                                            case 30:
                                                try
                                                {
                                                    WapTool.DataBBSReArray.Add("bbsRe" + siteid + id, relistVo);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                                num = 219;
                                                continue;
                                            case 219:
                                                num = 236;
                                                continue;
                                            case 236:
                                                try
                                                {
                                                    while (true)
                                                    {
                                                    IL_58f3:
                                                        flag = classVo.bbsFace.IndexOf('_') >= 0;
                                                        num = 1;
                                                        while (true)
                                                        {
                                                            switch (num)
                                                            {
                                                                case 1:
                                                                    if (!flag)
                                                                    {
                                                                        num = 6;
                                                                        continue;
                                                                    }
                                                                    goto case 3;
                                                                case 6:
                                                                    classVo.bbsFace = "_";
                                                                    num = 3;
                                                                    continue;
                                                                case 3:
                                                                    facelist = classVo.bbsFace.Split('_')[0].Split('|');
                                                                    facelistImg = classVo.bbsFace.Split('_')[1].Split('|');
                                                                    flag = classVo.bbsType.IndexOf('_') >= 0;
                                                                    num = 0;
                                                                    continue;
                                                                case 0:
                                                                    if (!flag)
                                                                    {
                                                                        num = 5;
                                                                        continue;
                                                                    }
                                                                    goto case 2;
                                                                case 5:
                                                                    classVo.bbsType = "_";
                                                                    num = 2;
                                                                    continue;
                                                                case 2:
                                                                    {
                                                                        string[] array2 = classVo.bbsType.Split('_')[1].Split('|');
                                                                        Random random = new Random();
                                                                        reShowInfo = array2[random.Next(0, array2.Length - 1)];
                                                                        num = 4;
                                                                        continue;
                                                                    }
                                                                case 4:
                                                                    goto end_IL_58ce;
                                                            }
                                                            goto IL_58f3;
                                                        end_IL_58ce:
                                                            break;
                                                        }
                                                        break;
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                }
                                                flag = bookVo.isVote != 1;
                                                num = 114;
                                                continue;
                                            case 114:
                                                if (!flag)
                                                {
                                                    num = 342;
                                                    continue;
                                                }
                                                goto case 431;
                                            case 342:
                                                {
                                                    wap_bbs_vote_BLL wap_bbs_vote_BLL = new wap_bbs_vote_BLL(a);
                                                    vlistVo = wap_bbs_vote_BLL.GetListVo(100, 1, " siteid=" + siteid + " and id=" + id, "*", "vid", 100L, 0);
                                                    num = 431;
                                                    continue;
                                                }
                                            case 431:
                                                siteDefault = WapTool.GetSiteDefault(siteVo.Version, 33);
                                                num = 40;
                                                continue;
                                            case 40:
                                                num = ((!(siteDefault != "1")) ? 322 : 220);
                                                continue;
                                            case 220:
                                                num = 304;
                                                continue;
                                            case 304:
                                                num4 = ((relistVo == null) ? 1 : 0);
                                                goto IL_5b86;
                                            case 322:
                                                num4 = 1;
                                                goto IL_5b86;
                                            case 8:
                                                if (!flag)
                                                {
                                                    num = 399;
                                                    continue;
                                                }
                                                goto case 223;
                                            case 399:
                                                stringBuilder = new StringBuilder();
                                                stringBuilder.Append("siteid=" + siteid + " and userid in(");
                                                num2 = 0;
                                                num = 436;
                                                continue;
                                            case 421:
                                            case 436:
                                                num = 73;
                                                continue;
                                            case 73:
                                                if (relistVo != null)
                                                {
                                                    num = 152;
                                                    continue;
                                                }
                                                goto IL_5c8c;
                                            case 152:
                                                num = 228;
                                                continue;
                                            case 228:
                                                if (num2 < relistVo.Count)
                                                {
                                                    num = 420;
                                                    continue;
                                                }
                                                goto IL_5c8c;
                                            case 420:
                                                num = 365;
                                                continue;
                                            case 365:
                                                num3 = ((num2 < 5) ? 1 : 0);
                                                goto IL_5c9d;
                                            case 234:
                                                num3 = 0;
                                                goto IL_5c9d;
                                            case 306:
                                                if (flag)
                                                {
                                                    stringBuilder.Append(relistVo[num2].userid);
                                                    stringBuilder.Append(",");
                                                    num2++;
                                                    num = 421;
                                                }
                                                else
                                                {
                                                    num = 303;
                                                }
                                                continue;
                                            case 303:
                                                stringBuilder.Append("0)");
                                                userListVo_IDName = MainBll.GetUserListVo(stringBuilder.ToString());
                                                num = 223;
                                                continue;
                                            case 223:
                                                VisiteCount("正在浏览贴子:<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "\">" + bookVo.book_title + "</a>");
                                                Action_user_doit(3);
                                                num = 26;
                                                continue;
                                            case 26:
                                                return;
                                            IL_5c9d:
                                                flag = (byte)num3 != 0;
                                                num = 306;
                                                continue;
                                            IL_5c8c:
                                                num = 234;
                                                continue;
                                            IL_5b86:
                                                flag = (byte)num4 != 0;
                                                num = 8;
                                                continue;
                                            IL_2896:
                                                flag = (byte)num5 != 0;
                                                num = 170;
                                                continue;
                                            IL_580a:
                                                flag = (byte)num6 != 0;
                                                num = 61;
                                                continue;
                                            IL_24da:
                                                flag = (byte)num8 != 0;
                                                num = 270;
                                                continue;
                                            IL_5056:
                                                flag = (byte)num9 != 0;
                                                num = 259;
                                                continue;
                                            IL_4e14:
                                                flag = (byte)num10 != 0;
                                                num = 167;
                                                continue;
                                            IL_4e03:
                                                num = 419;
                                                continue;
                                            IL_22be:
                                                flag = (byte)num11 != 0;
                                                num = 390;
                                                continue;
                                            IL_48e1:
                                                flag = (byte)num12 != 0;
                                                num = 324;
                                                continue;
                                            IL_46be:
                                                flag = (byte)num13 != 0;
                                                num = 312;
                                                continue;
                                            IL_46ad:
                                                num = 204;
                                                continue;
                                            IL_2162:
                                                flag = (byte)num14 != 0;
                                                num = 302;
                                                continue;
                                            IL_43e8:
                                                flag = (byte)num15 != 0;
                                                num = 410;
                                                continue;
                                            IL_43d7:
                                                num = 331;
                                                continue;
                                            IL_3bc7:
                                                flag = (byte)num16 != 0;
                                                num = 237;
                                                continue;
                                            IL_3bb6:
                                                num = 416;
                                                continue;
                                            IL_3982:
                                                flag = (byte)num17 != 0;
                                                num = 93;
                                                continue;
                                            IL_3971:
                                                num = 180;
                                                continue;
                                            IL_3409:
                                                flag = (byte)num18 != 0;
                                                num = 376;
                                                continue;
                                            IL_33f8:
                                                num = 347;
                                                continue;
                                            IL_31cf:
                                                flag = (byte)num19 != 0;
                                                num = 49;
                                                continue;
                                            IL_31be:
                                                num = 208;
                                                continue;
                                            IL_3077:
                                                flag = (byte)num20 != 0;
                                                num = 136;
                                                continue;
                                            IL_3066:
                                                num = 334;
                                                continue;
                                            IL_2f07:
                                                flag = (byte)num21 != 0;
                                                num = 221;
                                                continue;
                                            IL_2ef6:
                                                num = 187;
                                                continue;
                                            IL_2e21:
                                                flag = (byte)num22 != 0;
                                                num = 121;
                                                continue;
                                            IL_2d8b:
                                                flag = (byte)num25 != 0;
                                                num = 70;
                                                continue;
                                            IL_2d7a:
                                                num = 213;
                                                continue;
                                            IL_2c6d:
                                                flag = (byte)num26 != 0;
                                                num = 85;
                                                continue;
                                            IL_2c5c:
                                                num = 162;
                                                continue;
                                            IL_2bb5:
                                                flag = (byte)num27 != 0;
                                                num = 13;
                                                continue;
                                            IL_2915:
                                                flag = (byte)num28 != 0;
                                                num = 397;
                                                continue;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ERROR = WapTool.ErrorToString(ex.ToString());
                                return;
                            }
                        case 59:
                            id = GetRequestValue("id");
                            lpage = GetRequestValue("lpage");
                            flag = !(lpage == "");
                            num = 30;
                            continue;
                        case 30:
                            if (!flag)
                            {
                                num = 72;
                                continue;
                            }
                            goto case 29;
                        case 25:
                            if (!flag)
                            {
                                num = 63;
                                continue;
                            }
                            goto case 19;
                        case 57:
                            num = ((towidths_W != "0") ? 43 : 41);
                            continue;
                        case 67:
                            flag = pageSize >= 100;
                            num = 39;
                            continue;
                        case 39:
                            if (!flag)
                            {
                                num = 4;
                                continue;
                            }
                            goto case 10;
                        case 29:
                            view = GetRequestValue("view");
                            viewLeave = GetRequestValue("viewleave");
                            stype = GetRequestValue("stype");
                            type = WapTool.GetSiteDefault(siteVo.Version, 27);
                            showhead = WapTool.getArryString(classVo.smallimg, '|', 38);
                            downLink = WapTool.getArryString(classVo.smallimg, '|', 20).Trim().Replace("[stype]", stype);
                            threePageType = WapTool.getArryString(classVo.smallimg, '|', 23);
                            flag = WapTool.IsNumeric(threePageType);
                            num = 33;
                            continue;
                        case 33:
                            if (!flag)
                            {
                                num = 50;
                                continue;
                            }
                            goto case 17;
                        case 69:
                            CurrentPage = int.Parse(GetRequestValue("vpage"));
                            flag = CurrentPage >= 1;
                            num = 61;
                            continue;
                        case 61:
                            if (!flag)
                            {
                                num = 53;
                                continue;
                            }
                            goto case 66;
                        case 16:
                            towidths = " width=\"" + towidths_W + "px\" ";
                            num = 40;
                            continue;
                        case 37:
                            flag = WapTool.IsNumeric(towidths_H);
                            num = 9;
                            continue;
                        case 9:
                            if (!flag)
                            {
                                num = 2;
                                continue;
                            }
                            goto case 36;
                        case 15:
                            towidths = "  height=\"" + towidths_H + "px\" ";
                            num = 65;
                            continue;
                        case 50:
                            threePageType = "1";
                            num = 17;
                            continue;
                        case 14:
                            stypelink = "&amp;stype=" + stype;
                            num = 68;
                            continue;
                        case 40:
                        case 42:
                        case 65:
                        case 71:
                            num = 52;
                            continue;
                        case 52:
                            num = ((classVo.topicID != "") ? 22 : 58);
                            continue;
                        case 20:
                            ShowTipInfo("此栏目已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                            num = 48;
                            continue;
                        case 51:
                            num = ((!flag) ? 16 : 13);
                            continue;
                        case 23:
                            CheckFunction("bbs", CurrentPage);
                            num = 0;
                            continue;
                        case 64:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            goto case 59;
                        case 62:
                            num33 = 1;
                            goto IL_6221;
                        case 63:
                            flag = !IsCheckManagerLvl("|00|01|03|04|", "");
                            num = 31;
                            continue;
                        case 31:
                            if (!flag)
                            {
                                num = 11;
                                continue;
                            }
                            goto case 24;
                        case 18:
                            num = ((!flag) ? 60 : 57);
                            continue;
                        case 46:
                            num = 5;
                            continue;
                        case 5:
                            num33 = ((!(towidths_H == "0")) ? 1 : 0);
                            goto IL_6221;
                        case 11:
                            isNeedSecret = true;
                            num = 24;
                            continue;
                        case 53:
                            CurrentPage = 1;
                            num = 66;
                            continue;
                        case 66:
                            num = 23;
                            continue;
                        case 3:
                            if (!flag)
                            {
                                num = 15;
                                continue;
                            }
                            towidths = " width=\"" + towidths_W + "px\" height=\"" + towidths_H + "px\" ";
                            num = 71;
                            continue;
                        case 49:
                            num32 = 1;
                            goto IL_62b5;
                        case 22:
                            num = 47;
                            continue;
                        case 47:
                            num31 = ((!(classVo.topicID != "0")) ? 1 : 0);
                            goto IL_5e16;
                        case 8:
                            flag = !"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 0));
                            num = 44;
                            continue;
                        case 44:
                            if (!flag)
                            {
                                num = 20;
                                continue;
                            }
                            goto case 48;
                        case 48:
                            towidths_W = WapTool.getArryString(classVo.smallimg, '|', 45);
                            towidths_H = WapTool.getArryString(classVo.smallimg, '|', 46);
                            flag = WapTool.IsNumeric(towidths_W);
                            num = 28;
                            continue;
                        case 28:
                            if (!flag)
                            {
                                num = 38;
                                continue;
                            }
                            goto case 37;
                        case 26:
                            num30 = 1;
                            goto IL_61ab;
                        case 38:
                            towidths_W = "0";
                            num = 37;
                            continue;
                        case 68:
                            flag = WapTool.IsNumeric(id);
                            num = 70;
                            continue;
                        case 70:
                            if (!flag)
                            {
                                num = 32;
                                continue;
                            }
                            goto case 8;
                        case 72:
                            lpage = "1";
                            num = 29;
                            continue;
                        case 43:
                            num = 55;
                            continue;
                        case 55:
                            num29 = ((!(towidths_H == "0")) ? 1 : 0);
                            goto IL_615b;
                        case 7:
                            ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                            num = 59;
                            continue;
                        case 10:
                            flag = !(GetRequestValue("vpage") != "");
                            num = 6;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 69;
                                continue;
                            }
                            goto case 23;
                        case 4:
                            pageSize = 100;
                            num = 10;
                            continue;
                        case 2:
                            towidths_H = "0";
                            num = 36;
                            continue;
                        case 36:
                            num = 12;
                            continue;
                        case 12:
                            {
                                num = ((towidths_W == "0") ? 46 : 62);
                                continue;
                            }
                        IL_61ab:
                            flag = (byte)num30 != 0;
                            num = 64;
                            continue;
                        IL_62b5:
                            flag = (byte)num32 != 0;
                            num = 3;
                            continue;
                        IL_6221:
                            flag = (byte)num33 != 0;
                            num = 18;
                            continue;
                        IL_615b:
                            flag = (byte)num29 != 0;
                            num = 51;
                            continue;
                        IL_5e16:
                            flag = (byte)num31 != 0;
                            num = 25;
                            continue;
                    }
                    break;
                }
            }
        }

        public void UpdateBbsReCount(string nowid)
        {
            //Discarded unreachable code: IL_0003
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
            long num = wap_bbsre_BLL.GetListCount(" devid='" + siteid + "' and bookid=" + nowid + " and ischeck=0");
            MainBll.UpdateSQL("update wap_bbs set book_re=" + num + " where id=" + nowid);
        }

        public string ShowNickName_color(long userid, string nickname)
        {
            //AI 解混淆的结果
            if (this.userListVo_IDName == null)
            {
                return nickname;
            }

            foreach (var item in this.userListVo_IDName)
            {
                if (item.userid == userid)
                {
                    nickname = WapTool.GetColorNickName(item.idname, nickname, base.lang, base.ver, item.endTime);
                    break;
                }
            }

            return nickname;
        }
    }
}
