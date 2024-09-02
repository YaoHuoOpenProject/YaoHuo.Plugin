using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyUserName : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string tousername = "";

        public string touserid = "";

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
            touserid = GetRequestValue("touserid");
            if (touserid == siteid)
            {
                ShowTipInfo("抱歉，正站长对应的用户名不在此修改，请到注册会员管理中修改。", "");
            }
            if (touserid != "")
            {
                if (!WapTool.IsNumeric(touserid))
                {
                    touserid = "0";
                }
                toUserVo = MainBll.getUserInfo(touserid, siteid);
            }
            if (!(requestValue == "gomod"))
            {
                return;
            }
            tousername = GetRequestValue("tousername");
            tousername = WapTool.left(tousername, 20);
            getuserid = MainBll.isHasExistUserName(tousername);
            if (tousername.Trim() == "")
            {
                INFO = "NULL";
                return;
            }
            if (getuserid != "0")
            {
                INFO = "HASEXIST";
                return;
            }
            try
            {
                MainBll.UpdateSQL("update [user] set username='" + tousername + "' where siteid=" + siteid + " and userid=" + toUserVo.userid);
                toUserVo.username = tousername;
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}