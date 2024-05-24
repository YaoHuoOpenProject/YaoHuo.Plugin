using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.Games.ChuiNiu
{
    public class Book_List : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string INFO = "";

        public string lpage = "";

        public string touserid = "";

        public string type = "";

        public List<wap2_games_chuiniu_Model> listVo = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            touserid = GetRequestValue("touserid");
            lpage = GetRequestValue("lpage");
            type = GetRequestValue("type");
            string text = action;
            if (text != null && text == "class")
            {
                showclass();
            }
            else
            {
                showclass();
            }
        }

        public void showclass()
        {
            condition = " siteid=" + siteid;
            if (touserid != "")
            {
                if (type == "0")
                {
                    condition = condition + " and userid=" + long.Parse(touserid);
                }
                else
                {
                    condition = condition + " and winuserid=" + long.Parse(touserid);
                }
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
                wap2_games_chuiniu_BLL wap2_games_chuiniu_BLL = new wap2_games_chuiniu_BLL(a);
                if (GetRequestValue("getTotal") != "" && GetRequestValue("getTotal") != "0")
                {
                    total = Convert.ToInt32(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap2_games_chuiniu_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "games/chuiniu/book_list.aspx?type=" + type + "&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "&amp;lpage=" + lpage + "&amp;getTotal=" + total;
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                listVo = wap2_games_chuiniu_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}
