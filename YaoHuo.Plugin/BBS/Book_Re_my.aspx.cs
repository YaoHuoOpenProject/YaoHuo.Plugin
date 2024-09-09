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
    public class Book_Re_My : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string linkTOP = "";

        public string condition = "";

        public string ERROR = "";

        public string INFO = "";

        public string touserid = "0";

        public string lpage = "";

        public string ot = "0";

        public List<wap_bbsre_Model> listVo = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            touserid = GetRequestValue("touserid").TrimStart('0'); // 去掉前导零
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("ot");

            // 检查是否为管理员
            bool isAdmin = IsCheckManagerLvl("|00|01|", "");

            // 如果访问ID为1000的回复列表且不是管理员
            if (touserid == "1000" && !isAdmin)
            {
                // 如果链接中带有page或ot参数，跳转回第一页
                if (!string.IsNullOrEmpty(GetRequestValue("page")) || !string.IsNullOrEmpty(ot))
                {
                    Response.Redirect("book_re_my.aspx?touserid=1000");
                    return;
                }
            }

            // 访问特定ID回复列表时，直接跳转到首页
            if (touserid == "3814")
            {
                Response.Redirect("/");
                return;
            }

            string text = action;
            if (text != null && text == "class")
            {
                showclass(isAdmin);
            }
            else
            {
                showclass(isAdmin);
            }
        }

        public void showclass(bool isAdmin)
        {
            condition = " devid='" + siteid + "' and userid=" + touserid;
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(string_10);
                if (GetRequestValue("getTotal") != "" && GetRequestValue("getTotal") != "0")
                {
                    total = Convert.ToInt64(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_bbsre_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                if (CurrentPage == 1L && WapTool.IsNumeric(touserid))
                {
                    MainBll.UpdateSQL("update [user] set bbsReCount=" + total + "  where siteid=" + siteid + " and  userid=" + touserid);
                }
                linkURL = http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "&amp;lpage=" + lpage + "&amp;getTotal=" + total + "&amp;ot=" + ot;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                if (ot == "1")
                {
                    listVo = wap_bbsre_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 0);
                }
                else
                {
                    listVo = wap_bbsre_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
                }

                // 如果访问ID为1000的回复列表且不是管理员，隐藏分页信息
                if (touserid == "1000" && !isAdmin)
                {
                    linkTOP = "";
                    linkURL = "";
                }
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}