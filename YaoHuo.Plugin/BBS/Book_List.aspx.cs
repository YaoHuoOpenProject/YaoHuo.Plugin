using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_List : MyPageWap
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
            hots = WapTool.GetSiteDefault(siteVo.Version, 41);
            if (!WapTool.IsNumeric(hots))
            {
                hots = "5000";
            }
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            if (classid == "0")
            {
                classVo.introduce = "";
                classVo.sitedowntip = "";
            }
            if (!IsCheckManagerLvl("|00|01|", "") && "1".Equals(WapTool.getArryString(classVo.smallimg, '|', 0)))
            {
                ShowTipInfo("此栏目已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            titlecolor = WapTool.getArryString(classVo.smallimg, '|', 42);
            switch (action)
            {
                case "search":
                    showsearch();
                    break;

                case "good":
                    showgood();
                    break;

                case "class":
                    showclass();
                    break;

                case "hot":
                    showhot();
                    break;

                case "new":
                    shownew();
                    break;

                default:
                    showclass();
                    break;
            }
            downLink = WapTool.getArryString(classVo.smallimg, '|', 19).Trim().Replace("[stype]", stype);
            string siteDefault = WapTool.GetSiteDefault(siteVo.Version, 33);
            if (!(siteDefault != "1") || (listVo == null && listVoTop == null))
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("siteid=" + siteid + " and userid in(");
            int num = 0;
            while (listVo != null && num < listVo.Count)
            {
                if (listVo[num].book_pub != "")
                {
                    stringBuilder.Append(listVo[num].book_pub);
                    stringBuilder.Append(",");
                }
                num++;
            }
            int num2 = 0;
            while (listVoTop != null && num2 < listVoTop.Count)
            {
                if (listVoTop[num2].book_pub != "")
                {
                    stringBuilder.Append(listVoTop[num2].book_pub);
                    stringBuilder.Append(",");
                }
                num2++;
            }
            stringBuilder.Append("0)");
            userListVo_IDName = MainBll.GetUserListVo(stringBuilder.ToString());
        }

        public void showclass()
        {
            if (classid == "0")
            {
                ShowTipInfo("无此栏目ID", "");
            }
            condition = " userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 ";
            stype = GetRequestValue("stype");
            if (WapTool.IsNumeric(stype))
            {
                condition = condition + " and topic=" + stype;
                stypename = WapTool.getSmallTypeName(siteid, stype);
                stypelink = "&amp;stype=" + stype;
            }
            try
            {
                if (classVo.ismodel < 1L)
                {
                    pageSize = siteVo.MaxPerPage_Default;
                }
                else
                {
                    pageSize = classVo.ismodel;
                }
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else if ("1".Equals(WapTool.KL_OpenCache))
                {
                    string value = null;
                    if (stype == "")
                        linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                    {
                        WapTool.DataTempArray.TryGetValue("bbsTotal" + siteid + classid, out value);
                    }
                    if (value == null)
                    {
                        total = wap_bbs_BLL.GetListCount(condition);
                        if (stype == "")
                        {
                            WapTool.DataTempArray.Add("bbsTotal" + siteid + classid, total.ToString());
                        }
                    }
                    else
                    {
                        total = long.Parse(value);
                    }
                }
                else
                {
                    total = wap_bbs_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CheckFunction("bbs", CurrentPage);
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/book_list.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total + stypelink;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                if (CurrentPage == 1L && stype == "")
                {
                    if ("1".Equals(WapTool.KL_OpenCache))
                    {
                        WapTool.DataBBSArray.TryGetValue("bbsTop" + siteid + classid, out listVoTop);
                        if (listVoTop == null)
                        {
                            listVoTop = wap_bbs_BLL.GetListVoTop("userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 and book_top =1 or (userid=" + siteid + " and ischeck=0 and book_top=2)");
                            try
                            {
                                WapTool.DataBBSArray.Add("bbsTop" + siteid + classid, listVoTop);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else
                    {
                        listVoTop = wap_bbs_BLL.GetListVoTop("userid=" + siteid + " and book_classid=" + classid + " and ischeck=0 and book_top =1 or (userid=" + siteid + " and ischeck=0 and book_top=2)");
                    }
                }
                if ("1".Equals(WapTool.KL_OpenCache))
                {
                    if (CurrentPage < 30L && stype == "")
                    {
                        WapTool.DataBBSArray.TryGetValue("bbs" + siteid + classid + CurrentPage, out listVo);
                    }
                    if (listVo == null)
                    {
                        if (stype != "")
                        {
                            int orderType = 0;
                            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 14)))
                            {
                                orderType = 1;
                            }
                            listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, orderType);
                        }
                        else
                        {
                            listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition + " and book_top =0 ", "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "redate", total, 1);
                        }
                        if (CurrentPage < 30L && stype == "")
                        {
                            try
                            {
                                WapTool.DataBBSArray.Add("bbs" + siteid + classid + CurrentPage, listVo);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                else if (stype != "")
                {
                    int orderType = 0;
                    if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 14)))
                    {
                        orderType = 1;
                    }
                    listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, orderType);
                }
                else
                {
                    var sreWhere = $@"{condition} and book_top = 0";
                    sreWhere += BlackTool.GetExcludeUserSql(userid, "book_pub");//排除拉黑的用户
                    listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, sreWhere, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,redate,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "redate", total, 1);
                }
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                VisiteCount("正在浏览:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void shownew()
        {
            if (classid == "0")
            {
                condition = " ischeck=0 and userid=" + siteid;
                condition += BlackTool.GetExcludeUserSql(userid, "book_pub");//排除拉黑的用户
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "所有最新帖子";
                classVo.siteimg = "NetImages/no.gif";
                classVo.introduce = "";
            }
            else
            {
                classVo.classname += ".最新帖子";
                condition = $" userid={siteid} and book_classid in (select classid from [class] where childid={classid} union select '{classid}') ";
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_bbs_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = int.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/book_list.aspx?action=new&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                VisiteCount("正在浏览最新:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void showhot()
        {
            if (classid == "0")
            {
                condition = " ischeck=0 and userid=" + siteid;
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "所有最热内容";
                classVo.siteimg = "NetImages/no.gif";
                classVo.introduce = "";
            }
            else
            {
                classVo.classname += ".最热内容";
                condition = "  ischeck=0 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_bbs_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = int.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/book_list.aspx?action=hot&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "book_click", total, 1);
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                VisiteCount("正在浏览热门:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void showsearch()
        {
            if (WapTool.GetSiteDefault(siteVo.Version, 60) == "1")
            {
                IsLogin(userid, GetUrlQueryString());
            }
            key = GetRequestValue("key");
            if (key.IsNull())
            {
                ShowTipInfo("禁止空白搜索", "");
            }
            type = GetRequestValue("type");
            key = HttpUtility.UrlDecode(key);
            key = key.Replace("[", "［");
            key = key.Replace("]", "］");
            key = key.Replace("%", "％");
            key = key.Replace("_", "——");
            if (classid == "0")
            {
                condition = " ischeck=0 and userid=" + siteid;
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "查询内容:" + key;
                classVo.siteimg = "NetImages/no.gif";
                classVo.introduce = "";
            }
            else
            {
                classVo.classname = classVo.classname + ":" + key;
                condition = "  ischeck=0 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
            }
            if (key.Trim() != "")
            {
                if (type == "title")
                {
                    if ("1".Equals(PubConstant.GetAppString("KL_FULLSEARCH_bbs")))
                    {
                        condition = condition + "and CONTAINS(book_title,'\"" + key + "\"') ";
                    }
                    else
                    {
                        condition = condition + " and book_title like '%" + key + "%' ";
                    }
                }
                else if (!(type == "content"))
                {
                    if (type == "author")
                    {
                        condition = condition + " and book_author like '%" + key + "%' ";
                    }
                    else if (type == "days")
                    {
                        if (!WapTool.IsNumeric(key))
                        {
                            key = "0";
                        }
                        condition = condition + " and  (DATEDIFF(dd, book_date, GETDATE()) < " + key + ") ";
                    }
                    else if (type == "pub")
                    {
                        if (WapTool.IsNumeric(key))
                        {
                            condition = condition + " and book_pub='" + key + "'";
                        }
                        else
                        {
                            condition += " and 1=2 ";
                        }
                    }
                }
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_bbs_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = int.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                if (CurrentPage == 1L && type == "pub" && WapTool.IsNumeric(key))
                {
                    MainBll.UpdateSQL("update [user] set bbsCount=" + total + "  where siteid=" + siteid + " and  userid=" + key);
                }
                linkURL = http_start + "bbs/book_list.aspx?action=search&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;type=" + type + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                VisiteCount("正在论坛查询关键字:" + key);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void showgood()
        {
            if (classid == "0")
            {
                condition = " ischeck=0 and book_good=1 and userid=" + siteid;
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "所有精华帖子";
                classVo.siteimg = "NetImages/no.gif";
                classVo.introduce = "";
            }
            else
            {
                classVo.classname += ".最新精华帖子";
                condition = "  ischeck=0 and book_good=1 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";
            }
            try
            {
                if (classVo.ismodel < 1L)
                {
                    pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                }
                else
                {
                    pageSize = Convert.ToInt32(classVo.ismodel);
                }
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_bbs_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = int.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/book_list.aspx?action=good&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);
                VisiteCount("正在浏览精华:<a href=\"" + http_start + GetUrlQueryString() + "\">" + classVo.classname + "</a>");
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public string ShowNickName_color(long userid, string nickname)
        {
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
