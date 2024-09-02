using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ResetVIP : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string tousername = "";

        public string topw = "";

        public string backurl = "";

        public string getuserid = "";

        public user_Model toUserVo = null;

        public string KL_Check_Repeat_Nickname = PubConstant.GetAppString("KL_Check_Repeat_Nickname");

        protected void Page_Load(object sender, EventArgs e)
        {
            string requestValue = GetRequestValue("action");
            if (!IsCheckManagerLvl("|00|", "") || siteVo.siteid != userVo.userid)
            {
                ShowTipInfo("抱歉，只有正超级管理员才有权限操作。", "");
            }
            needPassWordToAdmin();
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "admin/basesitemodifywml.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            topw = GetRequestValue("topw");
            if (!(requestValue == "godoit"))
            {
                return;
            }
            if (PubConstant.md5(topw) != userVo.password)
            {
                INFO = "NULL";
                return;
            }
            try
            {
                MainBll.UpdateSQL("update [user] set sessiontimeout=0,endtime=null where siteid=" + siteid + " and endtime is not null and datediff(day, getdate(),endtime) < 1 ");
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}