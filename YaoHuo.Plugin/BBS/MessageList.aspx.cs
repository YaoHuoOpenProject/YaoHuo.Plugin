using System;
using System.Collections.Generic;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class MessageList : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string types = "";

        public string backurl = "";

        public string linkTOP = "";

        public string issystem = "";

        public List<wap_message_Model> listVo = null;

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            issystem = GetRequestValue("issystem");
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "myfile.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            IsLogin(userid, backurl);
            if (WapTool.getArryString(siteVo.Version, '|', 53) == "1")
            {
                needPassWordToAdmin();
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
            types = GetRequestValue("types");
            if (types == "")
            {
                types = "0";
            }
            if (action == "save")
            {
                string requestValue = GetRequestValue("id");
                if (WapTool.IsNumeric(requestValue))
                {
                    MainBll.UpdateSQL("update wap_message set issystem=2 where siteid=" + siteid + " and touserid=" + userid + " and id=" + requestValue);
                }
            }
            condition = " siteid= " + siteid + " and touserid=" + userid;
            if (types == "2")
            {
                condition += " and isnew=2 ";
            }
            else
            {
                condition += " and isnew < 2 ";
            }
            if (WapTool.IsNumeric(issystem))
            {
                condition = condition + " and issystem = " + issystem;
            }
            else
            {
                condition += " and issystem <>2 ";
            }
            if (key.Trim() != "")
            {
                condition = condition + " and  title like '%" + key + "%' ";
            }
            try
            {
                pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
                wap_message_BLL wap_message_BLL = new wap_message_BLL(string_10);
                if (GetRequestValue("getTotal") != "")
                {
                    total = long.Parse(GetRequestValue("getTotal"));
                }
                else
                {
                    total = wap_message_BLL.GetListCount(condition);
                }
                if (GetRequestValue("page") != "")
                {
                    CurrentPage = long.Parse(GetRequestValue("page"));
                }
                CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
                index = pageSize * (CurrentPage - 1L);
                linkURL = http_start + "bbs/messagelist.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;types=" + types + "&amp;issystem=" + issystem + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;getTotal=" + total;
                linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
                linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
                listVo = wap_message_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1L);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}