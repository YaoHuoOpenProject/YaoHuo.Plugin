using KeLin.ClassManager;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyInfo : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string aihao = "";
        public string qq = "";
        public string string_12 = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
            needPassWordToAdmin();
            string text = base.Request.Form.Get("action");
            aihao = (userVo.aihao + "___").Split('_')[0];
            qq = (userVo.aihao + "___").Split('_')[1];
            string_12 = WapTool.GetSiteDefault(siteVo.Version, 50);
            if (!WapTool.IsNumeric(string_12) || string_12 == "0")
            {
                string_12 = "50";
            }
            if (!(text == "gomod"))
            {
                return;
            }
            try
            {
                userVo.sex = long.Parse(GetRequestValue("sex"));
                userVo.age = long.Parse(GetRequestValue("age"));
                userVo.mobile = GetRequestValue("mobile");
                userVo.email = GetRequestValue("email");
                userVo.remark = GetRequestValue("remark");
                userVo.shenggao = GetRequestValue("shenggao");
                userVo.tizhong = GetRequestValue("tizhong");
                userVo.xingzuo = GetRequestValue("xingzuo");
                userVo.fenfuo = GetRequestValue("fenfuo");
                userVo.zhiye = GetRequestValue("zhiye");
                userVo.city = WapTool.left(GetRequestValue("city"), int.Parse(string_12));
                aihao = GetRequestValue("aihao");
                qq = GetRequestValue("qq");
                userVo.aihao = aihao + "_" + qq;
                if (userVo.remark.Length > 200)
                {
                    userVo.remark = userVo.remark.Substring(0, 200);
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