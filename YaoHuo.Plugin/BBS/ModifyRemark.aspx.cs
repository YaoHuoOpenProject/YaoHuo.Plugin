using KeLin.ClassManager;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyRemark : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string num = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
            needPassWordToAdmin();
            string text = base.Request.Form.Get("action");
            num = WapTool.GetSiteDefault(siteVo.Version, 49);
            if (!WapTool.IsNumeric(num) || num == "0")
            {
                num = "50";
            }
            if (!(text == "gomod"))
            {
                return;
            }
            try
            {
                userVo.remark = GetRequestValue("remark");
                if (num != "0")
                {
                    userVo.remark = WapTool.left(userVo.remark, int.Parse(num));
                }
                MainBll.UpdateUser_WAP(userVo);
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}