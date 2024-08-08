using KeLin.ClassManager;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyNick : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string tonickname = "";

        public string backurl = "";

        public string KL_Check_Repeat_Nickname = PubConstant.GetAppString("KL_Check_Repeat_Nickname");

        public string num = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            string text = base.Request.Form.Get("action");
            IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
            needPassWordToAdmin();
            num = WapTool.GetSiteDefault(siteVo.Version, 48);
            if (!WapTool.IsNumeric(num) || num == "0")
            {
                num = "16";
            }
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "bbs/modifyuserinfo.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            tonickname = userVo.nickname;
            if (!(text == "gomod"))
            {
                return;
            }
            tonickname = GetRequestValue("tonickname");
            if (tonickname.Length > 15)
            {
                tonickname = tonickname.Substring(0, 15);
            }
            if (tonickname.Trim() == "")
            {
                INFO = "NULL";
                return;
            }
            if (KL_Check_Repeat_Nickname != "1" && MainBll.isHasExistNickname(siteid, tonickname))
            {
                INFO = "HASEXIST";
                return;
            }
            if (num != "0")
            {
                tonickname = WapTool.left(tonickname, int.Parse(num));
            }
            MainBll.UpdateSQL("update [user] set nickname='" + tonickname + "' where siteid=" + siteid + " and userid=" + userid);
            INFO = "OK";
        }
    }
}