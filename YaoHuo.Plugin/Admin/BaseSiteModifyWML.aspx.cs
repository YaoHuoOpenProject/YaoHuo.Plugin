using System;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Admin
{
    public class BaseSiteModifyWML : MyPageWap
    {
    public string strTitle2 = "";

    public string strHtml = "";

    public string strHtml1 = "";

    public string strHtml2 = "";

    public string cname = "";

    public string string_10 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        IsCheckUserManager(userid, userVo.managerlvl, classVo.adminusername, GetUrlQueryString());
    }
    }
}