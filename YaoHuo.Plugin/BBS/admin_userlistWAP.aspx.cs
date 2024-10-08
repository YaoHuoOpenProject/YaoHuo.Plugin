﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
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

        public List<wap_bbs_Model> listVo = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            CheckManagerLvl("03", classVo.adminusername, GetUrlQueryString());
            switch (action)
            {
                case "class":
                    showclass();
                    break;
                case "gotop":
                    gotop();
                    break;
                case "gogood":
                    gogood();
                    break;
                case "gocheck":
                    gocheck();
                    break;
                case "gocheckall":
                    gocheckall();
                    break;
                default:
                    showclass();
                    break;
                case "godel":
                    break;
            }
        }

        public void showclass()
        {
            key = GetRequestValue("key");
            if (classid == "0")
            {
                condition = " userid=" + siteid;
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "管理 所有论坛内容:" + key;
                classVo.siteimg = "NetImages/no.gif";
            }
            else
            {
                classVo.classname = "管理 " + classVo.classname + ":" + key;
                condition = " userid=" + siteid + " and   book_classid=" + classid + " ";
            }
            if (key.Trim() != "")
            {
                condition = condition + " and book_title like '%" + key + "%' ";
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
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CheckFunction("bbs", CurrentPage);
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/admin_userlistWAP.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL);
                if (CurrentPage == 1L && classVo.total != total)
                {
                    WapTool.setTotal(siteid, classid, total);
                }
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void gotop()
        {
            string requestValue = GetRequestValue("id");
            string requestValue2 = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set book_top=" + long.Parse(requestValue2) + " where userid=" + siteid + " and id=" + long.Parse(requestValue));
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }

        public void gogood()
        {
            string requestValue = GetRequestValue("id");
            string requestValue2 = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set book_good=" + long.Parse(requestValue2) + " where userid=" + siteid + " and id=" + long.Parse(requestValue));
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }

        public void gocheck()
        {
            CheckManagerLvl("01", "", GetUrlQueryString());
            if (userVo.managerlvl != "00" && siteVo.isCheckSite == 1L)
            {
                ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheck", "class"));
                return;
            }
            string requestValue = GetRequestValue("id");
            string requestValue2 = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue2) + "  where id=" + long.Parse(requestValue) + " and userid=" + siteid);
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }

        public void gocheckall()
        {
            CheckManagerLvl("01", "", GetUrlQueryString());
            if (userVo.managerlvl != "00" && siteVo.isCheckSite == 1L)
            {
                ShowTipInfo("超级管理员设置您网站内容需要审核，请联系超级管理员审核！", GetUrlQueryString().Replace("gocheckall", "class"));
                return;
            }
            string requestValue = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue) + "  where ischeck <>" + long.Parse(requestValue) + " and userid=" + siteid);
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }
    }
}