using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class MessageList_Del : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

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

        public string issystem = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            backurl = base.Request.QueryString.Get("backurl");
            id = base.Request.QueryString.Get("id");
            page = base.Request.QueryString.Get("page");
            types = base.Request.QueryString.Get("types");
            backurl = base.Request.QueryString.Get("backurl");
            issystem = base.Request.QueryString.Get("issystem");
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
            switch (action)
            {
                case "godelall":
                    godelall();
                    break;
                case "godelother":
                    godelother();
                    break;
                case "godel":
                    godel();
                    break;
            }
        }

        public void godelother()
        {
            wap_message_BLL wap_message_BLL = new wap_message_BLL(string_10);
            wap_message_Model model = wap_message_BLL.GetModel(long.Parse(id));
            MainBll.UpdateSQL("delete  from wap_message where siteid=" + siteid + " and isnew < 2 and issystem<> 2 and   ((touserid=" + userid + " and userid=" + model.userid + ") or (touserid=" + model.userid + " and userid=" + userid + ") )");
            INFO = "OK";
        }

        public void godel()
        {
            MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and touserid=" + long.Parse(userid) + " and id=" + long.Parse(id));
            INFO = "OK";
        }

        public void godelall()
        {
            if (types == "2")
            {
                MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and isnew=2 and issystem<>2 and touserid=" + long.Parse(userid));
            }
            else if (issystem == "")
            {
                MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and isnew<2 and issystem<>2 and touserid=" + long.Parse(userid));
            }
            else if (issystem == "0")
            {
                MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and isnew<2 and issystem=0 and touserid=" + long.Parse(userid));
            }
            else if (issystem == "1")
            {
                MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and isnew<2 and issystem=1 and touserid=" + long.Parse(userid));
            }
            else if (issystem == "2")
            {
                MainBll.UpdateSQL("delete  from wap_message where siteid=" + long.Parse(siteid) + " and isnew<2 and issystem=2 and touserid=" + long.Parse(userid));
            }
            INFO = "OK";
        }
    }
}