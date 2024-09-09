using System;
using System.Collections.Generic;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using Microsoft.VisualBasic;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Album
{
    public class AlbumList : MyPageWap

    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string friendtype = "";

        public string backurl = "";

        public string linkTOP = "";

        public string smalltypeid = "";

        public string touserid = "";

        public List<wap_album_Model> listVo = null;

        public wap_albumSubject_Model bookVo = null;

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, backurl);
            action = GetRequestValue("action");
            smalltypeid = GetRequestValue("smalltypeid");
            if (smalltypeid == "")
            {
                smalltypeid = "0";
            }
            touserid = GetRequestValue("touserid");
            if (touserid == "")
            {
                touserid = userid;
            }
            // 新增跳转逻辑，如果不是自己的相册
            if (touserid != userid)
            {
                Response.Redirect("/album/albumlist.aspx");
                return;
            }
            switch (action)
            {
                case "class":
                    showclass();
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
            condition = " userid = " + siteid + " and makerid=" + touserid;
            if (userid == "0" || touserid != userid)
            {
                condition += " and ishidden =0 ";
            }
            if (smalltypeid != "" && smalltypeid != "0")
            {
                condition = condition + " and smalltype = " + smalltypeid;
            }
            if (key.Trim() != "")
            {
                condition = condition + " and book_title like '%" + key + "%'";
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_album_BLL wap_album_BLL = new wap_album_BLL(string_10);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_album_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "album/albumlist.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "&amp;smalltypeid=" + smalltypeid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                listVo = wap_album_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
                if (smalltypeid != "0")
                {
                    wap_albumSubject_BLL wap_albumSubject_BLL = new wap_albumSubject_BLL(string_10);
                    bookVo = wap_albumSubject_BLL.GetModel(long.Parse(smalltypeid));
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}