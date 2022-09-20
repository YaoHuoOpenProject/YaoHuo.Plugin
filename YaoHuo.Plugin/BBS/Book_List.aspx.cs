using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;

public class Book_List : PageWap
{
    private string a = PubConstant.GetAppString("InstanceName");

    public string action = "";

    public string linkURL = "";

    public string linkTOP = "";

    public string condition = "";

    public string stype = "";

    public string stypename = "";

    public string ERROR = "";

    public string stypelink = "";

    public string type = "";

    public string key = "";

    public List<wap_bbs_Model> listVo = null;

    public List<wap_bbs_Model> listVoTop = null;

    public StringBuilder strhtml = new StringBuilder();

    public sys_ad_show_Model adVo = new sys_ad_show_Model();

    public List<user_Model> userListVo_IDName = null;

    public long kk = 1L;

    public long index = 0L;

    public long total = 0L;

    public long pageSize = 10L;

    public long CurrentPage = 1L;

    public string downLink = "";

    public string hots = "500";

    public string titlecolor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_036b
        string text = default(string);
        int num2 = default(int);
        int num3 = default(int);
        string siteDefault = default(string);
        StringBuilder stringBuilder = default(StringBuilder);
        while (true)
        {
            hots = WapTool.GetSiteDefault(siteVo.Version, 41);
            bool flag = WapTool.IsNumeric(hots);
            int num = 37;
            while (true)
            {
                int num6;
                int num4;
                int num7;
                int num8;
                int num5;
                switch (num)
                {
                    case 37:
                        if (!flag)
                        {
                            num = 49;
                            continue;
                        }
                        goto case 40;
                    case 22:
                        num = 53;
                        continue;
                    case 53:
                        if (!(text == "hot"))
                        {
                            num = 21;
                            continue;
                        }
                        showhot();
                        num = 48;
                        continue;
                    case 66:
                        num = 41;
                        continue;
                    case 41:
                        num = ((listVo != null) ? 64 : 46);
                        continue;
                    case 27:
                        classVo.introduce = "";
                        classVo.sitedowntip = "";
                        num = 55;
                        continue;
                    case 1:
                        num = 33;
                        continue;
                    case 33:
                        num6 = ((num2 < listVo.Count) ? 1 : 0);
                        goto IL_0343;
                    case 64:
                        num4 = 0;
                        goto IL_06fd;
                    case 39:
                        num = 62;
                        continue;
                    case 16:
                        num = 13;
                        continue;
                    case 13:
                        if (!(text == "good"))
                        {
                            num = 23;
                            continue;
                        }
                        showgood();
                        num = 54;
                        continue;
                    case 46:
                        num = 35;
                        continue;
                    case 35:
                        num4 = ((listVoTop == null) ? 1 : 0);
                        goto IL_06fd;
                    case 52:
                    case 59:
                        num = 12;
                        continue;
                    case 12:
                        num = ((listVoTop == null) ? 9 : 56);
                        continue;
                    case 23:
                        num = 70;
                        continue;
                    case 70:
                        if (!(text == "search"))
                        {
                            num = 39;
                            continue;
                        }
                        showsearch();
                        num = 10;
                        continue;
                    case 68:
                        num4 = 1;
                        goto IL_06fd;
                    case 28:
                        if (!flag)
                        {
                            num = 63;
                            continue;
                        }
                        flag = !(listVo[num2].book_pub != "");
                        num = 8;
                        continue;
                    case 63:
                        if (true)
                        {
                        }
                        num3 = 0;
                        num = 59;
                        continue;
                    case 50:
                        num = 44;
                        continue;
                    case 44:
                        num7 = ((!"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 0))) ? 1 : 0);
                        goto IL_095d;
                    case 0:
                        num8 = 1;
                        goto IL_0987;
                    case 30:
                        ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                        num = 69;
                        continue;
                    case 49:
                        hots = "5000";
                        num = 40;
                        continue;
                    case 55:
                        num = 38;
                        continue;
                    case 38:
                        num = (IsCheckManagerLvl("|00|01|", "") ? 31 : 50);
                        continue;
                    case 21:
                        num = 5;
                        continue;
                    case 5:
                        if (text == "class")
                        {
                            showclass();
                            num = 57;
                        }
                        else
                        {
                            num = 16;
                        }
                        continue;
                    case 3:
                    case 67:
                        num = 14;
                        continue;
                    case 14:
                        num = ((listVo != null) ? 1 : 15);
                        continue;
                    case 17:
                        num2++;
                        num = 67;
                        continue;
                    case 8:
                        if (!flag)
                        {
                            num = 20;
                            continue;
                        }
                        goto case 17;
                    case 65:
                        num = 18;
                        continue;
                    case 18:
                        if (text == "new")
                        {
                            shownew();
                            num = 24;
                        }
                        else
                        {
                            num = 22;
                        }
                        continue;
                    case 10:
                    case 24:
                    case 43:
                    case 48:
                    case 54:
                    case 57:
                        downLink = WapTool.getArryString(classVo.smallimg, '|', 19).Trim().Replace("[stype]", stype);
                        siteDefault = WapTool.GetSiteDefault(siteVo.Version, 33);
                        num = 7;
                        continue;
                    case 7:
                        num = ((siteDefault != "1") ? 66 : 68);
                        continue;
                    case 60:
                        if (!flag)
                        {
                            num = 29;
                            continue;
                        }
                        goto case 19;
                    case 40:
                        num = 36;
                        continue;
                    case 36:
                        num = ((classid != "0") ? 32 : 0);
                        continue;
                    case 32:
                        num = 4;
                        continue;
                    case 4:
                        num8 = ((!(classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                        goto IL_0987;
                    case 58:
                        stringBuilder = new StringBuilder();
                        stringBuilder.Append("siteid=" + siteid + " and userid in(");
                        num2 = 0;
                        num = 3;
                        continue;
                    case 6:
                        titlecolor = WapTool.getArryString(classVo.smallimg, '|', 42);
                        text = action;
                        num = 34;
                        continue;
                    case 34:
                        if (text != null)
                        {
                            num = 65;
                            continue;
                        }
                        goto case 62;
                    case 25:
                        if (!flag)
                        {
                            num = 58;
                            continue;
                        }
                        return;

                    case 31:
                        num7 = 1;
                        goto IL_095d;
                    case 56:
                        num = 45;
                        continue;
                    case 45:
                        num5 = ((num3 < listVoTop.Count) ? 1 : 0);
                        goto IL_08c9;
                    case 15:
                        num6 = 0;
                        goto IL_0343;
                    case 2:
                        ShowTipInfo("此栏目已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
                        num = 6;
                        continue;
                    case 9:
                        num5 = 0;
                        goto IL_08c9;
                    case 29:
                        stringBuilder.Append(listVoTop[num3].book_pub);
                        stringBuilder.Append(",");
                        num = 19;
                        continue;
                    case 62:
                        showclass();
                        num = 43;
                        continue;
                    case 19:
                        num3++;
                        num = 52;
                        continue;
                    case 69:
                        action = GetRequestValue("action");
                        flag = !(classid == "0");
                        num = 47;
                        continue;
                    case 47:
                        if (!flag)
                        {
                            num = 27;
                            continue;
                        }
                        goto case 55;
                    case 26:
                        if (flag)
                        {
                            flag = !(listVoTop[num3].book_pub != "");
                            num = 60;
                        }
                        else
                        {
                            num = 51;
                        }
                        continue;
                    case 20:
                        stringBuilder.Append(listVo[num2].book_pub);
                        stringBuilder.Append(",");
                        num = 17;
                        continue;
                    case 51:
                        stringBuilder.Append("0)");
                        userListVo_IDName = MainBll.GetUserListVo(stringBuilder.ToString());
                        num = 42;
                        continue;
                    case 42:
                        return;

                    case 61:
                        if (!flag)
                        {
                            num = 2;
                            continue;
                        }
                        goto case 6;
                    case 11:
                        {
                            if (!flag)
                            {
                                num = 30;
                                continue;
                            }
                            goto case 69;
                        }
                    IL_08c9:
                        flag = (byte)num5 != 0;
                        num = 26;
                        continue;
                    IL_06fd:
                        flag = (byte)num4 != 0;
                        num = 25;
                        continue;
                    IL_0987:
                        flag = (byte)num8 != 0;
                        num = 11;
                        continue;
                    IL_095d:
                        flag = (byte)num7 != 0;
                        num = 61;
                        continue;
                    IL_0343:
                        flag = (byte)num6 != 0;
                        num = 28;
                        continue;
                }
                break;
            }
        }
    }

    public void showclass()
    {
        //Discarded unreachable code: IL_0059
        var wapBbsBLL = default(wap_bbs_BLL);
        int orderType = default(int);
        string value = default(string);
        while (true)
        {
            bool flag = !(classid == "0");
            int num = 5;
            while (true)
            {
                switch (num)
                {
                    case 5:
                        if (!flag)
                        {
                            num = 3;
                            continue;
                        }
                        goto case 1;
                    case 3:
                        if (true)
                        {
                        }
                        ShowTipInfo("无此栏目ID", "");
                        num = 1;
                        continue;
                    case 4:
                        try
                        {
                            while (true)
                            {
                                flag = classVo.ismodel >= 1;
                                num = 8;
                                while (true)
                                {
                                    int num3;
                                    int num4;
                                    int num2;
                                    switch (num)
                                    {
                                        case 8:
                                            if (!flag)
                                            {
                                                num = 67;
                                                continue;
                                            }
                                            pageSize = classVo.ismodel;
                                            num = 61;
                                            continue;
                                        case 36:
                                            WapTool.DataTempArray.Add("bbsTotal" + siteid + classid, total.ToString());
                                            num = 29;
                                            continue;
                                        case 15:
                                            listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, orderType);
                                            num = 2;
                                            continue;
                                        case 55:
                                            num3 = 1;
                                            goto IL_0702;
                                        case 26:
                                        case 52:
                                            num = 16;
                                            continue;
                                        case 58:
                                            total = long.Parse(GetRequestValue("getTotal"));
                                            num = 43;
                                            continue;
                                        case 51:
                                            num = 75;
                                            continue;
                                        case 75:
                                            num4 = ((!(stype == "")) ? 1 : 0);
                                            goto IL_0494;
                                        case 37:
                                            num = 33;
                                            continue;
                                        case 18:
                                            num = 54;
                                            continue;
                                        case 35:
                                            orderType = 0;
                                            flag = !"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 14));
                                            num = 21;
                                            continue;
                                        case 21:
                                            if (!flag)
                                            {
                                                num = 11;
                                                continue;
                                            }
                                            goto case 45;
                                        case 45:
                                            listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, orderType);
                                            num = 26;
                                            continue;
                                        case 3:
                                            orderType = 1;
                                            num = 15;
                                            continue;
                                        case 6:
                                            flag = !(stype != "");
                                            num = 42;
                                            continue;
                                        case 42:
                                            if (!flag)
                                            {
                                                num = 24;
                                                continue;
                                            }
                                            listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition + " and book_top =0 ", "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "redate", total, 1);
                                            num = 57;
                                            continue;
                                        case 28:
                                            if (!flag)
                                            {
                                                num = 18;
                                                continue;
                                            }
                                            goto case 27;
                                        case 12:
                                            num = 68;
                                            continue;
                                        case 68:
                                            num2 = ((!(stype == "")) ? 1 : 0);
                                            goto IL_087c;
                                        case 44:
                                            num4 = 1;
                                            goto IL_0494;
                                        case 50:
                                            CheckFunction("bbs", CurrentPage);
                                            CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                            index = pageSize * (CurrentPage - 1);
                                            linkURL = http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total + stypelink;
                                            linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                                            linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                                            num = 9;
                                            continue;
                                        case 9:
                                            num = ((CurrentPage != 1) ? 23 : 12);
                                            continue;
                                        case 67:
                                            pageSize = siteVo.MaxPerPage_Default;
                                            num = 59;
                                            continue;
                                        case 30:
                                            CurrentPage = long.Parse(GetRequestValue("page"));
                                            num = 50;
                                            continue;
                                        case 22:
                                        case 43:
                                            flag = !(GetRequestValue("page") != "");
                                            num = 48;
                                            continue;
                                        case 48:
                                            if (!flag)
                                            {
                                                num = 30;
                                                continue;
                                            }
                                            goto case 50;
                                        case 71:
                                            flag = value != null;
                                            num = 13;
                                            continue;
                                        case 13:
                                            if (!flag)
                                            {
                                                num = 5;
                                                continue;
                                            }
                                            total = long.Parse(value);
                                            num = 39;
                                            continue;
                                        case 63:
                                            if (!flag)
                                            {
                                                num = 56;
                                                continue;
                                            }
                                            goto case 47;
                                        case 74:
                                            if (flag)
                                            {
                                                total = wapBbsBLL.GetListCount(condition);
                                                num = 65;
                                            }
                                            else
                                            {
                                                num = 62;
                                            }
                                            continue;
                                        case 54:
                                            try
                                            {
                                                WapTool.DataBBSArray.Add("bbs" + siteid + classid + CurrentPage, listVo);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            num = 27;
                                            continue;
                                        case 1:
                                            try
                                            {
                                                WapTool.DataBBSArray.Add("bbsTop" + siteid + classid, listVoTop);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            num = 37;
                                            continue;
                                        case 59:
                                        case 61:
                                            wapBbsBLL = new wap_bbs_BLL(a);
                                            flag = !(GetRequestValue("getTotal") != "");
                                            num = 60;
                                            continue;
                                        case 60:
                                            if (flag)
                                            {
                                                flag = !"1".Equals(WapTool.KL_OpenCache);
                                                num = 74;
                                            }
                                            else
                                            {
                                                num = 58;
                                            }
                                            continue;
                                        case 69:
                                            if (!flag)
                                            {
                                                num = 7;
                                                continue;
                                            }
                                            goto case 53;
                                        case 29:
                                            num = 34;
                                            continue;
                                        case 31:
                                            listVoTop = wapBbsBLL.GetListVoTop("userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 and book_top =1 or (userid=" + siteid + " and ischeck=0 and book_top=2)");
                                            num = 1;
                                            continue;
                                        case 14:
                                            WapTool.DataBBSArray.TryGetValue("bbsTop" + siteid + classid, out listVoTop);
                                            flag = listVoTop != null;
                                            num = 76;
                                            continue;
                                        case 76:
                                            if (!flag)
                                            {
                                                num = 31;
                                                continue;
                                            }
                                            goto case 37;
                                        case 2:
                                        case 57:
                                            num = 64;
                                            continue;
                                        case 64:
                                            num = ((CurrentPage < 30) ? 51 : 44);
                                            continue;
                                        case 33:
                                        case 73:
                                            num = 53;
                                            continue;
                                        case 10:
                                            if (flag)
                                            {
                                                var sreWhere = $@"{condition} and book_top = 0 {ExcludeTool.GetExcludeUserSql("book_pub", userid)}";//排除拉黑的用户
                                                listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, sreWhere, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "redate", total, 1);
                                                num = 52;
                                            }
                                            else
                                            {
                                                num = 35;
                                            }
                                            continue;
                                        case 34:
                                        case 39:
                                            num = 40;
                                            continue;
                                        case 47:
                                            flag = listVo != null;
                                            num = 72;
                                            continue;
                                        case 72:
                                            if (!flag)
                                            {
                                                num = 6;
                                                continue;
                                            }
                                            goto case 0;
                                        case 24:
                                            orderType = 0;
                                            flag = !"1".Equals(WapTool.getArryString(classVo.smallimg, '|', 14));
                                            num = 46;
                                            continue;
                                        case 46:
                                            if (!flag)
                                            {
                                                num = 3;
                                                continue;
                                            }
                                            goto case 15;
                                        case 53:
                                            flag = !"1".Equals(WapTool.KL_OpenCache);
                                            num = 70;
                                            continue;
                                        case 70:
                                            if (flag)
                                            {
                                                flag = !(stype != "");
                                                num = 10;
                                            }
                                            else
                                            {
                                                num = 32;
                                            }
                                            continue;
                                        case 40:
                                        case 65:
                                            num = 22;
                                            continue;
                                        case 27:
                                            num = 0;
                                            continue;
                                        case 32:
                                            num = 38;
                                            continue;
                                        case 38:
                                            num = ((CurrentPage < 30) ? 49 : 55);
                                            continue;
                                        case 62:
                                            value = null;
                                            flag = !(stype == "");
                                            num = 25;
                                            continue;
                                        case 25:
                                            if (!flag)
                                            {
                                                num = 17;
                                                continue;
                                            }
                                            goto case 71;
                                        case 7:
                                            flag = !"1".Equals(WapTool.KL_OpenCache);
                                            num = 66;
                                            continue;
                                        case 66:
                                            if (flag)
                                            {
                                                listVoTop = wapBbsBLL.GetListVoTop("userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 and book_top =1 or (userid=" + siteid + " and ischeck=0 and book_top=2)");
                                                num = 73;
                                            }
                                            else
                                            {
                                                num = 14;
                                            }
                                            continue;
                                        case 56:
                                            WapTool.DataBBSArray.TryGetValue("bbs" + siteid + classid + CurrentPage, out listVo);
                                            num = 47;
                                            continue;
                                        case 0:
                                            num = 4;
                                            continue;
                                        case 11:
                                            orderType = 1;
                                            num = 45;
                                            continue;
                                        case 49:
                                            num = 19;
                                            continue;
                                        case 19:
                                            num3 = ((!(stype == "")) ? 1 : 0);
                                            goto IL_0702;
                                        case 23:
                                            num2 = 1;
                                            goto IL_087c;
                                        case 5:
                                            total = wapBbsBLL.GetListCount(condition);
                                            flag = !(stype == "");
                                            num = 41;
                                            continue;
                                        case 41:
                                            if (!flag)
                                            {
                                                num = 36;
                                                continue;
                                            }
                                            goto case 29;
                                        case 17:
                                            WapTool.DataTempArray.TryGetValue("bbsTotal" + siteid + classid, out value);
                                            num = 71;
                                            continue;
                                        case 4:
                                        case 16:
                                            {
                                                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                VisiteCount("正在浏览:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
                                                num = 20;
                                                continue;
                                            }
                                        case 20:
                                            return;
                                        IL_087c:
                                            flag = (byte)num2 != 0;
                                            num = 69;
                                            continue;
                                        IL_0702:
                                            flag = (byte)num3 != 0;
                                            num = 63;
                                            continue;
                                        IL_0494:
                                            flag = (byte)num4 != 0;
                                            num = 28;
                                            continue;
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
                        condition = condition + " and topic=" + stype;
                        stypename = WapTool.getSmallTypeName(siteid, stype);
                        stypelink = "&amp;stype=" + stype;
                        num = 4;
                        continue;
                    case 1:
                        condition = " userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 ";
                        stype = GetRequestValue("stype");
                        flag = !WapTool.IsNumeric(stype);
                        num = 0;
                        continue;
                    case 0:
                        if (!flag)
                        {
                            num = 2;
                            continue;
                        }
                        goto case 4;
                }
                break;
            }
        }
    }

    public void shownew()
    {
        //Discarded unreachable code: IL_03fb
        while (true)
        {
            bool flag = !(classid == "0");
            int num = 2;
            while (true)
            {
                switch (num)
                {
                    case 2:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        classVo.classname += ".最新贴子";
                        condition = " userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
                        num = 1;
                        continue;
                    case 1:
                    case 3:
                        try
                        {
                            while (true)
                            {
                                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                var wapBbsBLL = new wap_bbs_BLL(a);
                                flag = !(GetRequestValue("getTotal") != "");
                                num = 3;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 3:
                                            if (!flag)
                                            {
                                                num = 7;
                                                continue;
                                            }
                                            total = wapBbsBLL.GetListCount(condition);
                                            num = 5;
                                            continue;
                                        case 7:
                                            total = long.Parse(GetRequestValue("getTotal"));
                                            num = 1;
                                            continue;
                                        case 2:
                                            CurrentPage = int.Parse(GetRequestValue("page"));
                                            num = 6;
                                            continue;
                                        case 1:
                                        case 5:
                                            flag = !(GetRequestValue("page") != "");
                                            num = 0;
                                            continue;
                                        case 0:
                                            if (!flag)
                                            {
                                                num = 2;
                                                continue;
                                            }
                                            goto case 6;
                                        case 6:
                                            {
                                                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                                index = pageSize * (CurrentPage - 1);
                                                linkURL = http_start + "bbs/book_list.aspx?action=new&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                                                listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "userid,book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                                                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                VisiteCount("正在浏览最新:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
                                                num = 4;
                                                continue;
                                            }
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
                    case 0:
                        //condition = " ischeck=0 and userid=" + siteid;
                        condition = $" ischeck = 0 and userid = {siteid} {ExcludeTool.GetExcludeUserSql("book_pub", userid)}";//排除拉黑的用户
                        classVo.classid = 0L;
                        classVo.position = "left";
                        classVo.classname = "所有最新贴子";
                        classVo.siteimg = "NetImages/no.gif";
                        classVo.introduce = "";
                        num = 3;
                        continue;
                }
                break;
            }
        }
    }

    public void showhot()
    {
        //Discarded unreachable code: IL_0465
        while (true)
        {
            bool flag = !(classid == "0");
            int num = 2;
            while (true)
            {
                switch (num)
                {
                    case 2:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        classVo.classname += ".最热内容";
                        condition = "  ischeck=0 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
                        num = 3;
                        continue;
                    case 1:
                    case 3:
                        try
                        {
                            while (true)
                            {
                                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                var wapBbsBLL = new wap_bbs_BLL(a);
                                flag = !(GetRequestValue("getTotal") != "");
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
                                            total = wapBbsBLL.GetListCount(condition);
                                            num = 2;
                                            continue;
                                        case 6:
                                            total = long.Parse(GetRequestValue("getTotal"));
                                            num = 5;
                                            continue;
                                        case 7:
                                            CurrentPage = int.Parse(GetRequestValue("page"));
                                            num = 3;
                                            continue;
                                        case 2:
                                        case 5:
                                            flag = !(GetRequestValue("page") != "");
                                            num = 0;
                                            continue;
                                        case 0:
                                            if (!flag)
                                            {
                                                num = 7;
                                                continue;
                                            }
                                            goto case 3;
                                        case 3:
                                            {
                                                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                                index = pageSize * (CurrentPage - 1);
                                                linkURL = http_start + "bbs/book_list.aspx?action=hot&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                                                listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "book_click", total, 1);
                                                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                VisiteCount("正在浏览热门:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
                                                num = 4;
                                                continue;
                                            }
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
                    case 0:
                        condition = " ischeck=0 and userid=" + siteid;
                        classVo.classid = 0L;
                        classVo.position = "left";
                        classVo.classname = "所有最热内容";
                        classVo.siteimg = "NetImages/no.gif";
                        classVo.introduce = "";
                        if (true)
                        {
                        }
                        num = 1;
                        continue;
                }
                break;
            }
        }
    }

    public void showsearch()
    {
        //Discarded unreachable code: IL_0937
        while (true)
        {
            bool flag = !(WapTool.GetSiteDefault(siteVo.Version, 60) == "1");
            int num = 7;
            while (true)
            {
                switch (num)
                {
                    case 7:
                        if (!flag)
                        {
                            num = 26;
                            continue;
                        }
                        goto case 35;
                    case 29:
                        if (!flag)
                        {
                            num = 27;
                            continue;
                        }
                        goto case 10;
                    case 6:
                        if (flag)
                        {
                            flag = !(type == "pub");
                            num = 29;
                        }
                        else
                        {
                            num = 4;
                        }
                        continue;
                    case 30:
                        flag = !(type == "title");
                        num = 28;
                        continue;
                    case 28:
                        if (!flag)
                        {
                            num = 16;
                            continue;
                        }
                        flag = !(type == "content");
                        num = 34;
                        continue;
                    case 33:
                        condition = " ischeck=0 and userid=" + siteid;
                        classVo.classid = 0L;
                        classVo.position = "left";
                        classVo.classname = "查询内容:" + key;
                        classVo.siteimg = "NetImages/no.gif";
                        classVo.introduce = "";
                        num = 9;
                        continue;
                    case 2:
                        condition = condition + " and  (DATEDIFF(dd, book_date, GETDATE()) < " + key + ") ";
                        num = 10;
                        continue;
                    case 25:
                        try
                        {
                            while (true)
                            {
                                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                var wapBbsBLL = new wap_bbs_BLL(a);
                                flag = !(GetRequestValue("getTotal") != "");
                                num = 11;
                                while (true)
                                {
                                    int num2;
                                    switch (num)
                                    {
                                        case 11:
                                            if (!flag)
                                            {
                                                num = 13;
                                                continue;
                                            }
                                            total = wapBbsBLL.GetListCount(condition);
                                            num = 8;
                                            continue;
                                        case 4:
                                        case 8:
                                            flag = !(GetRequestValue("page") != "");
                                            num = 1;
                                            continue;
                                        case 1:
                                            if (!flag)
                                            {
                                                num = 17;
                                                continue;
                                            }
                                            goto case 3;
                                        case 14:
                                            num2 = 1;
                                            goto IL_0504;
                                        case 13:
                                            total = long.Parse(GetRequestValue("getTotal"));
                                            num = 4;
                                            continue;
                                        case 7:
                                            num = 5;
                                            continue;
                                        case 5:
                                            num2 = ((!(type == "pub")) ? 1 : 0);
                                            goto IL_0504;
                                        case 3:
                                            CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                            index = pageSize * (CurrentPage - 1);
                                            num = 10;
                                            continue;
                                        case 10:
                                            num = ((CurrentPage == 1) ? 7 : 14);
                                            continue;
                                        case 2:
                                            MainBll.UpdateSQL("update [user] set bbsCount=" + total + "  where siteid=" + siteid + " and  userid=" + key);
                                            num = 0;
                                            continue;
                                        case 17:
                                            CurrentPage = int.Parse(GetRequestValue("page"));
                                            num = 3;
                                            continue;
                                        case 0:
                                            num = 16;
                                            continue;
                                        case 15:
                                            if (!flag)
                                            {
                                                num = 12;
                                                continue;
                                            }
                                            goto case 16;
                                        case 12:
                                            flag = !WapTool.IsNumeric(key);
                                            num = 6;
                                            continue;
                                        case 6:
                                            if (!flag)
                                            {
                                                num = 2;
                                                continue;
                                            }
                                            goto case 0;
                                        case 16:
                                            {
                                                linkURL = http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;type=" + type + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                                                listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                                                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                VisiteCount("正在论坛查询关键字:" + key);
                                                num = 9;
                                                continue;
                                            }
                                        case 9:
                                            return;
                                        IL_0504:
                                            flag = (byte)num2 != 0;
                                            num = 15;
                                            continue;
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
                    case 14:
                        condition = condition + " and book_author like '%" + key + "%' ";
                        num = 17;
                        continue;
                    case 16:
                        flag = !"1".Equals(PubConstant.GetAppString("KL_FULLSEARCH_bbs"));
                        num = 20;
                        continue;
                    case 20:
                        if (!flag)
                        {
                            num = 24;
                            continue;
                        }
                        condition = condition + " and book_title like '%" + key + "%' ";
                        num = 21;
                        continue;
                    case 5:
                    case 12:
                        num = 31;
                        continue;
                    case 8:
                    case 21:
                        num = 22;
                        continue;
                    case 26:
                        IsLogin(userid, GetUrlQueryString());
                        num = 35;
                        continue;
                    case 0:
                    case 9:
                        flag = !(key.Trim() != "");
                        num = 13;
                        continue;
                    case 13:
                        if (!flag)
                        {
                            num = 30;
                            continue;
                        }
                        goto case 25;
                    case 18:
                        condition = condition + " and book_pub='" + key + "'";
                        num = 12;
                        continue;
                    case 27:
                        flag = !WapTool.IsNumeric(key);
                        num = 19;
                        continue;
                    case 19:
                        if (flag)
                        {
                            condition += " and 1=2 ";
                            num = 5;
                        }
                        else
                        {
                            num = 18;
                        }
                        continue;
                    case 1:
                        if (true)
                        {
                        }
                        num = 11;
                        continue;
                    case 34:
                        if (!flag)
                        {
                            num = 1;
                            continue;
                        }
                        flag = !(type == "author");
                        num = 3;
                        continue;
                    case 4:
                        flag = WapTool.IsNumeric(key);
                        num = 15;
                        continue;
                    case 15:
                        if (!flag)
                        {
                            num = 32;
                            continue;
                        }
                        goto case 2;
                    case 24:
                        condition = condition + "and CONTAINS(book_title,'\"" + key + "\"') ";
                        num = 8;
                        continue;
                    case 35:
                        key = GetRequestValue("key");
                        type = GetRequestValue("type");
                        key = HttpUtility.UrlDecode(key);
                        key = key.Replace("[", "［");
                        key = key.Replace("]", "］");
                        key = key.Replace("%", "％");
                        key = key.Replace("_", "——");
                        flag = !(classid == "0");
                        num = 23;
                        continue;
                    case 23:
                        if (flag)
                        {
                            classVo.classname = classVo.classname + ":" + key;
                            condition = "  ischeck=0 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
                            num = 0;
                        }
                        else
                        {
                            num = 33;
                        }
                        continue;
                    case 10:
                    case 11:
                    case 17:
                    case 22:
                    case 31:
                        num = 25;
                        continue;
                    case 3:
                        if (flag)
                        {
                            flag = !(type == "days");
                            num = 6;
                        }
                        else
                        {
                            num = 14;
                        }
                        continue;
                    case 32:
                        key = "0";
                        num = 2;
                        continue;
                }
                break;
            }
        }
    }

    public void showgood()
    {
        //Discarded unreachable code: IL_003e
        var wapBbsBLL = default(wap_bbs_BLL);
        while (true)
        {
            bool flag = !(classid == "0");
            int num = 3;
            while (true)
            {
                switch (num)
                {
                    case 3:
                        if (true)
                        {
                        }
                        if (!flag)
                        {
                            num = 1;
                            continue;
                        }
                        classVo.classname += ".最新精华贴子";
                        condition = "  ischeck=0 and book_good=1 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
                        num = 0;
                        continue;
                    case 0:
                    case 2:
                        try
                        {
                            while (true)
                            {
                                flag = classVo.ismodel >= 1;
                                num = 7;
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
                                            pageSize = Convert.ToInt32(classVo.ismodel);
                                            num = 1;
                                            continue;
                                        case 6:
                                            pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                            num = 9;
                                            continue;
                                        case 8:
                                        case 11:
                                            flag = !(GetRequestValue("page") != "");
                                            num = 5;
                                            continue;
                                        case 5:
                                            if (!flag)
                                            {
                                                num = 0;
                                                continue;
                                            }
                                            goto case 3;
                                        case 0:
                                            CurrentPage = int.Parse(GetRequestValue("page"));
                                            num = 3;
                                            continue;
                                        case 10:
                                            total = long.Parse(GetRequestValue("getTotal"));
                                            num = 11;
                                            continue;
                                        case 1:
                                        case 9:
                                            wapBbsBLL = new wap_bbs_BLL(a);
                                            flag = !(GetRequestValue("getTotal") != "");
                                            num = 2;
                                            continue;
                                        case 2:
                                            if (flag)
                                            {
                                                total = wapBbsBLL.GetListCount(condition);
                                                num = 8;
                                            }
                                            else
                                            {
                                                num = 10;
                                            }
                                            continue;
                                        case 3:
                                            {
                                                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                                index = pageSize * (CurrentPage - 1);
                                                linkURL = http_start + "bbs/book_list.aspx?action=good&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                                                listVo = wapBbsBLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                                                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                                                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                                                VisiteCount("正在浏览精华:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
                                                num = 4;
                                                continue;
                                            }
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
                    case 1:
                        condition = " ischeck=0 and book_good=1 and userid=" + siteid;
                        classVo.classid = 0L;
                        classVo.position = "left";
                        classVo.classname = "所有精华贴子";
                        classVo.siteimg = "NetImages/no.gif";
                        classVo.introduce = "";
                        num = 2;
                        continue;
                }
                break;
            }
        }
    }

    public string ShowNickName_color(long userid, string nickname)
    {
        //Discarded unreachable code: IL_0097
        int num2 = default(int);
        string result = default(string);
        while (true)
        {
            bool flag = userListVo_IDName != null;
            int num = 10;
            while (true)
            {
                int num3;
                switch (num)
                {
                    case 10:
                        if (!flag)
                        {
                            num = 7;
                            continue;
                        }
                        num2 = 0;
                        num = 8;
                        continue;
                    case 4:
                        num3 = 0;
                        goto IL_00be;
                    case 2:
                    case 8:
                        num = 11;
                        continue;
                    case 11:
                        if (userListVo_IDName == null)
                        {
                            num = 4;
                            continue;
                        }
                        if (true)
                        {
                        }
                        num = 3;
                        continue;
                    case 6:
                        if (!flag)
                        {
                            num = 9;
                            continue;
                        }
                        flag = userListVo_IDName[num2].userid != userid;
                        num = 5;
                        continue;
                    case 7:
                        result = nickname;
                        num = 1;
                        continue;
                    case 3:
                        num = 14;
                        continue;
                    case 14:
                        num3 = ((num2 < userListVo_IDName.Count) ? 1 : 0);
                        goto IL_00be;
                    case 9:
                    case 12:
                        result = nickname;
                        num = 0;
                        continue;
                    case 13:
                        nickname = WapTool.GetColorNickName(userListVo_IDName[num2].idname, nickname, lang, ver, userListVo_IDName[num2].endTime);
                        num = 12;
                        continue;
                    case 5:
                        if (!flag)
                        {
                            num = 13;
                            continue;
                        }
                        num2++;
                        num = 2;
                        continue;
                    case 0:
                    case 1:
                        {
                            return result;
                        }
                    IL_00be:
                        flag = (byte)num3 != 0;
                        num = 6;
                        continue;
                }
                break;
            }
        }
    }
}
