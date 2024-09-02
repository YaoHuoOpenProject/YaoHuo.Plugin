using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Admin
{
    public class BaseSiteModifyWML00 : MyPageWap
    {
        public string strTitle2 = "";

        public string strHtml = "";

        public string strHtml1 = "";

        public string strHtml2 = "";

        public string cname = "";

        public string string_10 = "";

        public string regcode = "";

        public string isReg = "";

        public string KL_Site_INFO = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckManagerLvl("00", "", GetUrlQueryString());
            if (WapTool.CheckStrCount(PageWap._KL_URL_INFO, "|") == 3)
            {
                string[] array = PageWap._KL_URL_INFO.Split('|');
                KL_Site_INFO = array[2];
            }
            isReg = WapTool.GetSystemVersion(KL_VERSION, "0");
            if (KL_ISREG == "1")
            {
                isReg = isReg + "-[" + GetLang("��ע��|��ע��|��ע��") + "]";
            }
            else
            {
                isReg = isReg + "-[" + GetLang("δע�ᣬ����ϵ|δע�ᣬ����ϵ|δע�ᣬ����ϵ") + "<a href=\"http://kelink.com\">Kelink.Com</a>]";
            }
        }
    }
}