using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_List_Search : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";
        public string linkURL = "";
        public string linkTOP = "";
        public string condition = "";
        public string key = "";
        public string type = "";
        public string ERROR = "";
        public List<wap_bbs_Model> listVo = null;
        public StringBuilder strhtml = new StringBuilder();
        public sys_ad_show_Model adVo = new sys_ad_show_Model();
        public List<user_Model> userListVo_IDName = null;
        public long kk = 1L;
        public long index = 0L;
        public long total = 0L;
        public long pageSize = 10L;
        public long CurrentPage = 1L;
        public string hots = "500";
        public string titlecolor = "";

        private static Dictionary<string, DateTime> lastSearchTime = new Dictionary<string, DateTime>();
        private static readonly TimeSpan searchLogInterval = TimeSpan.FromMinutes(2);

        // 多个关键词搜索开关
        private static bool EnableMultiKeywordSearch = false; // 开启true 关闭false

        public static void ToggleMultiKeywordSearch()
        {
            EnableMultiKeywordSearch = !EnableMultiKeywordSearch;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            action = GetRequestValue("action");
            showsearch();
        }

        public void showsearch()
        {
            key = GetRequestValue("key");
            type = GetRequestValue("type");
            string pub = GetRequestValue("pub");

            // 限制关键词数量和长度
            if (!string.IsNullOrEmpty(key))
            {
                string[] keywords = key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (keywords.Length > 3)
                {
                    keywords = keywords.Take(3).ToArray();
                }
                key = string.Join(" ", keywords);
                if (key.Length > 20)
                {
                    key = key.Substring(0, 20);
                }
            }

            if (WapTool.GetSiteDefault(siteVo.Version, 60) == "1")
            {
                IsLogin(userid, GetUrlQueryString());
            }

            if (type == "pub" || !string.IsNullOrEmpty(pub))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.TrimStart('0');
                }
                if (!string.IsNullOrEmpty(pub))
                {
                    pub = pub.TrimStart('0');
                }
            }

            if (key.IsNull() && pub.IsNull())
            {
                ShowTipInfo("禁止空白搜索", "");
                return;
            }

            key = HttpUtility.UrlDecode(key);
            key = key.Replace("[", "［").Replace("]", "］").Replace("%", "％").Replace("_", "——");

            string displayText = "查询内容";
            string displayValue = key;

            if (type == "pub" || !string.IsNullOrEmpty(pub))
            {
                displayText = "查询用户";
                displayValue = !string.IsNullOrEmpty(pub) ? pub : key;
            }
            else if (type == "title")
            {
                displayText = "查询标题";
            }

            if (classid == "0")
            {
                condition = " ischeck=0 and userid=" + siteid;
                classVo.classid = 0L;
                classVo.position = "left";

                // 修改这里的逻辑
                if (type == "days")
                {
                    classVo.classname = $"查询天数:{displayValue}";
                }
                else
                {
                    classVo.classname = $"{displayText}:{displayValue}";
                }

                classVo.siteimg = "NetImages/no.gif";
                classVo.introduce = "";
            }
            else
            {
                // 修改这里的逻辑
                if (type == "days")
                {
                    classVo.classname = $"{classVo.classname}>查询天数:{displayValue}";
                }
                else if (type == "pub" || !string.IsNullOrEmpty(pub))
                {
                    classVo.classname = $"{classVo.classname}>查询用户:{displayValue}";
                }
                else
                {
                    classVo.classname = $"{classVo.classname}>{displayText}:{displayValue}";
                }

                condition = "  ischeck=0 and userid=" + siteid + " and   book_classid in (select classid from [class] where childid=" + classid + " union select '" + classid + "') ";

                // 添加5年时间限制
                condition += " and book_date >= DATEADD(year, -5, GETDATE()) ";
            }

            if (!string.IsNullOrEmpty(key))
            {
                if (type == "title")
                {
                    if (EnableMultiKeywordSearch)
                    {
                        string[] keywords = key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (keywords.Length > 3)
                        {
                            keywords = keywords.Take(3).ToArray();
                        }

                        if ("1".Equals(PubConstant.GetAppString("KL_FULLSEARCH_bbs")))
                        {
                            condition += " and CONTAINS(book_title,'\"" + string.Join("\" AND \"", keywords) + "\"') ";
                        }
                        else
                        {
                            condition += " and " + string.Join(" AND ", keywords.Select(k => "CHARINDEX('" + k + "', book_title) > 0"));
                        }
                    }
                    else
                    {
                        if ("1".Equals(PubConstant.GetAppString("KL_FULLSEARCH_bbs")))
                        {
                            condition += " and CONTAINS(book_title,'\"" + key + "\"') ";
                        }
                        else
                        {
                            condition += " and book_title like '%" + key + "%' ";
                        }
                    }
                }
                else if (type == "author")
                {
                    condition += " and book_author like '%" + key + "%' ";
                }
                else if (type == "days")
                {
                    if (!WapTool.IsNumeric(key))
                    {
                        key = "0";
                    }
                    condition += " and (DATEDIFF(dd, book_date, GETDATE()) < " + key + ") ";
                }
                else if (type == "pub" && WapTool.IsNumeric(key))
                {
                    condition += " and book_pub='" + key + "'";
                }
            }

            if (!string.IsNullOrEmpty(pub) && WapTool.IsNumeric(pub))
            {
                condition += " and book_pub='" + pub + "'";
            }

            // 针对用户1000的搜索添加时间限制
            if ((type == "pub" && key == "1000") || (!string.IsNullOrEmpty(pub) && pub == "1000"))
            {
                // 检查当前用户ID是否为1000
                if (this.userid != "1000")
                {
                    condition += " and book_date >= DATEADD(year, -2, GETDATE())";
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
                linkURL = http_start + "bbs/book_list_search.aspx?action=search&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;type=" + type + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;

                if (!string.IsNullOrEmpty(pub))
                {
                    linkURL += "&amp;pub=" + pub;
                }

                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL, WapTool.getArryString(classVo.smallimg, '|', 40));
                listVo = wap_bbs_BLL.GetListVo(pageSize, CurrentPage, condition, "book_classid,classname,id,book_title,book_date,book_click,book_re,book_author,book_pub,book_top,book_good,topic,islock,ischeck,sendMoney,isvote,isdown,hangbiaoshi,freeMoney,book_img,MarkSixBetID,MarkSixWin", "id", total, 1);
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='bbs' and siteid=" + siteid);

                // 获取当前栏目名称，只取冒号前的部分
                string currentClassName = classVo.classname.Split(':')[0];

                // 修改日志记录
                string logMessage = "";
                string searchKey = "";
                if (!string.IsNullOrEmpty(pub) && type == "title")
                {
                    logMessage = $"正在论坛查询{pub}的帖子:{key}";
                    searchKey = $"title_{pub}_{key}";
                }
                else if (type == "pub" || !string.IsNullOrEmpty(pub))
                {
                    logMessage = $"正在论坛查询用户:{(!string.IsNullOrEmpty(pub) ? pub : key)}";
                    searchKey = $"user_{(!string.IsNullOrEmpty(pub) ? pub : key)}";
                }
                else if (type == "title")
                {
                    logMessage = $"正在论坛查询标题:{key}";
                    searchKey = $"title_{key}";
                }
                else if (type == "author")
                {
                    logMessage = $"正在论坛查询作者:{key}";
                    searchKey = $"author_{key}";
                }
                else if (type == "days")
                {
                    logMessage = $"正在查看{currentClassName}最近{key}天的帖子";
                    searchKey = $"days_{key}";
                }
                else
                {
                    logMessage = $"正在论坛查询关键字:{key}";
                    searchKey = $"keyword_{key}";
                }

                if (ShouldLogSearch(searchKey))
                {
                    VisiteCount(logMessage);
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }

        private bool ShouldLogSearch(string searchKey)
        {
            DateTime now = DateTime.Now;
            if (!lastSearchTime.ContainsKey(searchKey) || (now - lastSearchTime[searchKey]) > searchLogInterval)
            {
                lastSearchTime[searchKey] = now;
                return true;
            }
            return false;
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