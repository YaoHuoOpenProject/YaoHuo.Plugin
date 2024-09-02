using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using Microsoft.VisualBasic;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Report_List : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string string_11 = "0";

        public string lpage = "";

        public List<wap2_bbs_report_Model> listVo = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            lpage = GetRequestValue("lpage");
            CheckManagerLvl("04", classVo.adminusername, "bbs/showadmin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=1");
            switch (action)
            {
                case "class":
                    showclass();
                    break;
                case "del":
                    godel();
                    break;
                default:
                    showclass();
                    break;
            }
        }

        public void showclass()
        {
            if (CheckManagerLvl("04", "") && classid == "0")
            {
                condition = " siteid=" + siteid;
            }
            else
            {
                condition = " classid=" + classid + " and siteid=" + siteid;
            }
            condition += " and types=0 ";
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
                wap2_bbs_report_BLL wap2_bbs_report_BLL = new wap2_bbs_report_BLL(string_10);
                if (GetRequestValue("getTotal") != "" && GetRequestValue("getTotal") != "0")
                {
                    total = Convert.ToInt32(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap2_bbs_report_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/Report_List.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;getTotal=" + total;
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                listVo = wap2_bbs_report_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        public void godel()
        {
        }
    }
}