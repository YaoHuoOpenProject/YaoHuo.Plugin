using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.WML
{
    public class admin_userlistWAP : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string linkTOP = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public List<wap_wml_Model> listVo = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            while (true)
            {
                action = GetRequestValue("action");
                CheckManagerLvl("03", classVo.adminusername, GetUrlQueryString());
                string text = action;
                int num = 12;
                while (true)
                {
                    switch (num)
                    {
                        case 12:
                            if (text != null)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 4;
                        case 8:
                            return;
                        case 11:
                            return;
                        case 7:
                            num = 0;
                            continue;
                        case 0:
                            num = ((text == "godel") ? 6 : 3);
                            continue;
                        case 9:
                            return;
                        case 1:
                            num = 5;
                            continue;
                        case 5:
                            if (text == "class")
                            {
                                showclass();
                                if (true)
                                {
                                }
                                num = 11;
                            }
                            else
                            {
                                num = 7;
                            }
                            continue;
                        case 6:
                            return;
                        case 4:
                            showclass();
                            num = 2;
                            continue;
                        case 2:
                            return;
                        case 14:
                            num = 10;
                            continue;
                        case 10:
                            if (text == "gocheckall")
                            {
                                gocheckall();
                                num = 9;
                            }
                            else
                            {
                                num = 13;
                            }
                            continue;
                        case 3:
                            num = 15;
                            continue;
                        case 15:
                            if (text == "gocheck")
                            {
                                gocheck();
                                num = 8;
                            }
                            else
                            {
                                num = 14;
                            }
                            continue;
                        case 13:
                            num = 4;
                            continue;
                    }
                    break;
                }
            }
        }

        public void showclass()
        {
            while (true)
            {
                key = GetRequestValue("key");
                bool flag = !(classid == "0");
                int num = 4;
                while (true)
                {
                    switch (num)
                    {
                        case 4:
                            if (!flag)
                            {
                                num = 0;
                                continue;
                            }
                            classVo.classname = "管理" + classVo.classname;
                            condition = " userid=" + siteid + " and   book_classid=" + classid + " ";
                            num = 3;
                            continue;
                        case 2:
                            try
                            {
                                while (true)
                                {
                                    pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                                    wap_wml_BLL wap_wml_BLL = new wap_wml_BLL(a);
                                    flag = !(GetRequestValue("getTotal") != "");
                                    num = 8;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 8:
                                                if (!flag)
                                                {
                                                    num = 10;
                                                    continue;
                                                }
                                                total = wap_wml_BLL.GetListCount(condition);
                                                num = 12;
                                                continue;
                                            case 13:
                                                num = 0;
                                                continue;
                                            case 7:
                                                CheckFunction("wml", CurrentPage);
                                                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                                                index = pageSize * (CurrentPage - 1);
                                                linkURL = http_start + "wml/admin_userlistWAP.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
                                                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                                                linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL);
                                                flag = CurrentPage != 1;
                                                num = 5;
                                                continue;
                                            case 5:
                                                if (!flag)
                                                {
                                                    num = 6;
                                                    continue;
                                                }
                                                goto case 0;
                                            case 9:
                                                WapTool.setTotal(siteid, classid, total);
                                                num = 13;
                                                continue;
                                            case 6:
                                                flag = classVo.total == total;
                                                num = 1;
                                                continue;
                                            case 1:
                                                if (!flag)
                                                {
                                                    num = 9;
                                                    continue;
                                                }
                                                goto case 13;
                                            case 11:
                                            case 12:
                                                flag = !(GetRequestValue("page") != "");
                                                num = 4;
                                                continue;
                                            case 4:
                                                if (!flag)
                                                {
                                                    num = 3;
                                                    continue;
                                                }
                                                goto case 7;
                                            case 10:
                                                total = long.Parse(GetRequestValue("getTotal"));
                                                if (true)
                                                {
                                                }
                                                num = 11;
                                                continue;
                                            case 3:
                                                CurrentPage = long.Parse(GetRequestValue("page"));
                                                num = 7;
                                                continue;
                                            case 0:
                                                listVo = wap_wml_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_click,hangbiaoshi,ischeck", "id", total, 1);
                                                num = 2;
                                                continue;
                                            case 2:
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
                        case 3:
                        case 5:
                            flag = !(key.Trim() != "");
                            num = 6;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 2;
                        case 0:
                            condition = " userid=" + siteid;
                            classVo.classid = 0L;
                            classVo.position = "left";
                            classVo.classname = "管理所有WML内容";
                            classVo.siteimg = "NetImages/no.gif";
                            num = 5;
                            continue;
                        case 1:
                            condition = condition + " and book_title like '%" + key + "%' ";
                            num = 2;
                            continue;
                    }
                    break;
                }
            }
        }

        public void gocheck()
        {
            string requestValue = default(string);
            string requestValue2 = default(string);
            while (true)
            {
                CheckManagerLvl("01", "", GetUrlQueryString());
                bool flag = !(userVo.managerlvl != "00");
                int num = 4;
                while (true)
                {
                    switch (num)
                    {
                        case 4:
                            if (!flag)
                            {
                                num = 0;
                                continue;
                            }
                            goto case 1;
                        case 6:
                            try
                            {
                                MainBll.UpdateSQL("update wap_wml set ischeck=" + long.Parse(requestValue) + "  where id=" + long.Parse(requestValue2) + " and userid=" + siteid);
                            }
                            catch (Exception ex)
                            {
                                ERROR = WapTool.ErrorToString(ex.ToString());
                            }
                            showclass();
                            num = 5;
                            continue;
                        case 1:
                            requestValue2 = GetRequestValue("id");
                            requestValue = GetRequestValue("state");
                            num = 6;
                            continue;
                        case 3:
                            return;
                        case 5:
                            return;
                        case 0:
                            flag = siteVo.isCheckSite != 1;
                            num = 2;
                            continue;
                        case 2:
                            num = (flag ? 1 : 7);
                            continue;
                        case 7:
                            if (true)
                            {
                            }
                            ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheck", "class"));
                            num = 3;
                            continue;
                    }
                    break;
                }
            }
        }

        public void gocheckall()
        {
            string requestValue = default(string);
            while (true)
            {
                CheckManagerLvl("01", "", GetUrlQueryString());
                bool flag = !(userVo.managerlvl != "00");
                int num = 4;
                while (true)
                {
                    switch (num)
                    {
                        case 4:
                            if (!flag)
                            {
                                num = 0;
                                continue;
                            }
                            goto case 1;
                        case 3:
                            try
                            {
                                MainBll.UpdateSQL("update wap_wml set ischeck=" + long.Parse(requestValue) + "  where ischeck <>" + long.Parse(requestValue) + " and userid=" + siteid);
                            }
                            catch (Exception ex)
                            {
                                ERROR = WapTool.ErrorToString(ex.ToString());
                            }
                            showclass();
                            num = 6;
                            continue;
                        case 1:
                            requestValue = GetRequestValue("state");
                            num = 3;
                            continue;
                        case 2:
                            ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheckall", "class"));
                            num = 5;
                            continue;
                        case 5:
                            return;
                        case 6:
                            return;
                        case 0:
                            if (true)
                            {
                            }
                            flag = siteVo.isCheckSite != 1;
                            num = 7;
                            continue;
                        case 7:
                            num = (flag ? 1 : 2);
                            continue;
                    }
                    break;
                }
            }
        }
    }
}