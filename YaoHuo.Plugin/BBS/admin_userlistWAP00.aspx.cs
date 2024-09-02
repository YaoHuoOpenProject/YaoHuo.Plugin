using System;
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
    public class admin_userlistWAP00 : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string linkTOP = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string ischeck = "";

        public string tositeid = "";

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
            IsCheckSuperAdmin(userid, userVo.managerlvl, GetUrlQueryString());
            switch (action)
            {
                case "gocheckall":
                    gocheckall();
                    break;
                case "gocheck":
                    gocheck();
                    break;
                case "class":
                    showclass();
                    break;
                default:
                    showclass();
                    break;
            }
        }

        public void showclass()
        {
            key = GetRequestValue("key");
            ischeck = GetRequestValue("ischeck");
            tositeid = GetRequestValue("tositeid");
            condition = " 1=1 ";
            if (tositeid != "" && tositeid != "0")
            {
                condition = condition + " and userid=" + long.Parse(tositeid);
            }
            else
            {
                tositeid = "0";
            }
            if (classid == "0")
            {
                classVo.classid = 0L;
                classVo.position = "left";
                classVo.classname = "管理 所有论坛内容:" + key;
                classVo.siteimg = "NetImages/no.gif";
            }
            else
            {
                classVo.classname = "管理 " + classVo.classname + ":" + key;
                condition = condition + "  and   book_classid=" + long.Parse(classid) + " ";
            }
            if (key.Trim() != "")
            {
                condition = condition + " and book_title like '%" + key + "%' ";
            }
            if (ischeck.Trim() != "")
            {
                condition = condition + " and ischeck=" + ischeck;
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
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
                linkURL = http_start + "bbs/admin_userlistWAP00.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;ischeck=" + ischeck + "&amp;tositeid=" + tositeid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
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
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }

        public void gocheck()
        {
            string requestValue = GetRequestValue("id");
            string requestValue2 = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue2) + " where id=" + long.Parse(requestValue));
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }

        public void gocheckall()
        {
            string requestValue = GetRequestValue("state");
            try
            {
                MainBll.UpdateSQL("update wap_bbs set ischeck=" + long.Parse(requestValue) + " where ischeck <>" + long.Parse(requestValue));
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            showclass();
        }
    }
}