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

		public string msg = "";

		public string regcode = "";

		public string isReg = "";

		public string KL_Site_INFO = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			while (true)
			{
				CheckManagerLvl("00", "", GetUrlQueryString());
				bool flag = WapTool.CheckStrCount(MyPageWap._KL_URL_INFO, "|") != 3;
				int num = 2;
				while (true)
				{
					switch (num)
					{
						case 2:
							if (!flag)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							goto case 6;
						case 1:
							isReg = isReg + "-[" + GetLang("ÒÑ×¢²á|ÒÑ×¢²á|ÒÑ×¢²á") + "]";
							num = 0;
							continue;
						case 0:
							return;
						case 6:
							isReg = WapTool.GetSystemVersion(KL_VERSION, "0");
							flag = !(KL_ISREG == "1");
							num = 4;
							continue;
						case 4:
							if (!flag)
							{
								num = 1;
								continue;
							}
							isReg = isReg + "-[" + GetLang("Î´×¢²á£¬ÇëÁªÏµ|Î´×¢²á£¬ÇëÁªÏµ|Î´×¢²á£¬ÇëÁªÏµ") + "<a href=\"http://kelink.com\">Kelink.Com</a>]";
							num = 5;
							continue;
						case 3:
							{
								string[] array = MyPageWap._KL_URL_INFO.Split('|');
								KL_Site_INFO = array[2];
								num = 6;
								continue;
							}
						case 5:
							return;
					}
					break;
				}
			}
		}
	}
}