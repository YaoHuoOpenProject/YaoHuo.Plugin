using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;
using System.Collections.Concurrent;

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

        public string searchKey = "";

        private static ConcurrentDictionary<string, (DateTime LastSearchTime, int SearchCount, string LastSearchKey)> _searchCache = new ConcurrentDictionary<string, (DateTime, int, string)>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            action = GetRequestValue("action");
            touserid = GetRequestValue("touserid").TrimStart('0');
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("ot");
            searchKey = GetRequestValue("searchKey");

            // 检查是否为管理员
            bool isAdmin = IsCheckManagerLvl("|00|01|", "");

            // 优化：合并检查逻辑
            if (!string.IsNullOrEmpty(searchKey) && touserid != userid && !isAdmin)
            {
                ERROR = "ERR_PERMISSION";
                return;
            }

            // 添加搜索关键词长度检查
            if (!string.IsNullOrEmpty(searchKey) && (searchKey.Length < 1 || searchKey.Length > 10))
            {
                ERROR = "ERR_LENGTH";
                return;
            }

            // 访问特定ID回复列表时，直接跳转到首页
            if (touserid == "3814")
            {
                Response.Redirect("/");
                return;
            }

            if (!string.IsNullOrEmpty(searchKey))
            {
                if (CanSearch() && (touserid == userid || isAdmin))
                {
                    // 执行搜索
                    showclass(isAdmin);
                }
                else
                {
                    ERROR = "<div class=\"tip\">搜索频率过高，请3秒后再试!</div>";
                }
            }
            else
            {
                showclass(isAdmin);
            }

            // 修改获取页码的逻辑
            if (GetRequestValue("page") != "")
            {
                if (long.TryParse(GetRequestValue("page"), out long pageNumber))
                {
                    CurrentPage = pageNumber;
                }
                else
                {
                    CurrentPage = 1;
                }
            }
        }

        public void showclass(bool isAdmin)
        {
            int parsedTouserid;
            if (int.TryParse(touserid, out parsedTouserid))
            {
                condition = $"userid='{touserid}'";

                // 只有当用户查看自己的回复或者是管理员时，才添加搜索条件
                if (!string.IsNullOrEmpty(searchKey) && (touserid == userid || isAdmin))
                {
                    // 添加搜索条件，包括时间和ID限制
                    condition += " AND id >= 16139565";
                    condition += " AND redate >= DATEADD(YEAR, -5, GETDATE())";
                    condition += $" AND CHARINDEX('{searchKey}', content) > 0";
                }
            }
            else
            {
                ERROR = "无效的用户ID";
                return;
            }

            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(string_10);

                // 更新总数
                total = wap_bbsre_BLL.GetListCount(condition);

                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);

                // 定义排序方式
                int orderType = (ot == "1") ? 0 : 1;

                // 使用现有的 GetListVo 方法
                listVo = wap_bbsre_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, orderType);

                // 更新链接URL，确保包含所有必要的参数
                linkURL = http_start + "bbs/book_re_my.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "&amp;lpage=" + lpage + "&amp;getTotal=" + total + "&amp;ot=" + ot;
                if (!string.IsNullOrEmpty(searchKey) && (touserid == userid || isAdmin))
                {
                    linkURL += "&amp;searchKey=" + HttpUtility.UrlEncode(searchKey);
                }
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);

                // 如果访问ID为1000的回复列表且不是管理员，隐藏分页信息
                if (touserid == "1000" && !isAdmin)
                {
                    linkTOP = "";
                    linkURL = "";
                }
            }
            catch (Exception ex)
            {
                ERROR = "SQL错误: " + ex.Message;
                if (ex is SqlException sqlEx)
                {
                    ERROR += "\n错误代码: " + sqlEx.Number;
                }
            }
        }

        private bool CanSearch()
        {
            string userKey = GetUserKey();
            var now = DateTime.Now;

            if (_searchCache.TryGetValue(userKey, out var lastSearch))
            {
                // 如果是相同的搜索关键词，允许搜索（翻页操作）
                if (lastSearch.LastSearchKey == searchKey)
                {
                    return true;
                }

                if ((now - lastSearch.LastSearchTime).TotalSeconds < 3)
                {
                    if (lastSearch.SearchCount >= 1)
                    {
                        return false;
                    }
                    _searchCache[userKey] = (lastSearch.LastSearchTime, lastSearch.SearchCount + 1, searchKey);
                }
                else
                {
                    _searchCache[userKey] = (now, 1, searchKey);
                }
            }
            else
            {
                _searchCache[userKey] = (now, 1, searchKey);
            }

            return true;
        }

        private string GetUserKey()
        {
            // 使用用户ID或IP地址作为键
            return HttpContext.Current.User.Identity.IsAuthenticated
                ? HttpContext.Current.User.Identity.Name
                : HttpContext.Current.Request.UserHostAddress;
        }
    }
}