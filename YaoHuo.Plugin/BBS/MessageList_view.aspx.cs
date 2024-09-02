using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class MessageList_view : MyPageWap
    {

        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string types = "";

        public string id = "";

        public string backurl = "";

        public string INFO = "";

        public string page = "";

        public string needpwFlag = "";

        public string needpw = "";

        public string issystem = "";

        public string isclose = "0";

        public wap_message_Model bookVo = new wap_message_Model();

        public List<wap_message_Model> listVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            issystem = GetRequestValue("issystem");
            isclose = GetRequestValue("isclose");
            backurl = base.Request.QueryString.Get("backurl");
            id = base.Request.QueryString.Get("id");
            page = base.Request.QueryString.Get("page");
            types = base.Request.QueryString.Get("types");
            backurl = base.Request.QueryString.Get("backurl");
            needpw = GetRequestValue("needpw");
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
            if (!WapTool.IsNumeric(id))
            {
                id = "0";
            }
            IsLogin(userid, backurl);
            if (WapTool.getArryString(siteVo.Version, '|', 53) == "1")
            {
                needPassWordToAdmin();
            }
            if (isclose == "")
            {
                if (base.Request.Cookies["KL_MESSAGE_TIMES"] != null)
                {
                    isclose = base.Request.Cookies["KL_MESSAGE_TIMES"].Value;
                }
            }
            else
            {
                base.Response.Cookies["KL_MESSAGE_TIMES"].Expires = DateTime.Now.AddYears(1);
                base.Response.Cookies["KL_MESSAGE_TIMES"].Value = isclose;
            }
            wap_message_BLL wap_message_BLL = new wap_message_BLL(a);
            bookVo = wap_message_BLL.GetModel(long.Parse(id));
            if (bookVo.userid.ToString() != userid && bookVo.touserid.ToString() != userid)
            {
                ShowTipInfo(GetLang("你没有权限！|你沒有權限|You do not have permission"), "");
            }
            needpwFlag = WapTool.getArryString(siteVo.Version, '|', 31);
            if (bookVo.isnew == 1L)
            {
                MainBll.UpdateSQL("update wap_message set isnew=0 where id=" + long.Parse(id));
            }
            condition = " siteid=" + siteid + " and isnew < 2 and issystem<> 2 and   ((touserid=" + userid + " and userid=" + bookVo.userid + ") or (touserid=" + bookVo.userid + " and userid=" + userid + ") ) ";
            if (isclose != "1")
            {
                listVo = wap_message_BLL.GetListVo(20L, 1L, condition, "*", "id", 1000L, 1L);
            }
        }
    }
}