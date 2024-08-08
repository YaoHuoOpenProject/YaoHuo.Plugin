// KeLin.WebSite.admin.BaseSiteModifyWML
using System;
using KeLin.WebSite;

public class BaseSiteModifyWML : PageWap
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
