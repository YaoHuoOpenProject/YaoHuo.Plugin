using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using System;
using System.Data;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin
{
    public class Default : Page
    {
        public string _InstanceName => PubConstant.GetAppString("InstanceName");

        public string KL_HiddenQuery => PubConstant.GetAppString("KL_HiddenQuery");

        public string INFO { get; set; } = "";

        public string ERROR { get; set; } = "";

        public string domain { get; set; } = "";

        public string siteid { get; set; } = "";

        public string goURL { get; set; } = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            domain = base.Request.ServerVariables["HTTP_HOST"];
            domain = domain.ToLower();
            try
            {
                var text = base.Request.QueryString.Get("_K");
                if (text != null)
                {
                    Session["_K"] = text;
                }
                var text2 = base.Request.QueryString.Get("_QR");
                if (text2 != null)
                {
                    Session["_QR"] = text2;
                }
                var text3 = domain.Split('.')[0];
                var commandText = "select * from domainname where id=1 or domain='" + domain + "' order by id desc";
                var dataSet = DbHelperSQL.ExecuteDataset(PubConstant.GetConnectionString(_InstanceName), CommandType.Text, commandText);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    if (!(row["id"].ToString() == "1"))
                    {
                        if (row["domain"].ToString().ToLower() == domain)
                        {
                            goURL = row["realpath"].ToString().Replace("&amp;", "&");
                            break;
                        }
                        continue;
                    }
                    if (domain.ToLower().IndexOf("www.") >= 0)
                    {
                        goURL = "/index.aspx";
                        break;
                    }
                    siteid = WapTool.GetSiteid("", text3);
                    if (siteid == "")
                    {
                        siteid = "0";
                    }
                    goURL = $"/wapindex.aspx?siteid={siteid}";
                    break;
                }
                if ("0".Equals(KL_HiddenQuery) || !IsRelativeUrl(goURL))
                {
                    base.Response.Redirect(goURL);
                    return;
                }
                var openUrl = goURL;
                base.Server.Execute(openUrl);
                //base.Server.Transfer(openUrl);
                //base.Response.End();
            }
            catch (Exception ex)
            {
                var text4 = WapTool.ErrorToString(ex.ToString());
                if (text4.IndexOf("ThreadAbortException") < 0)
                {
                    base.Response.Write(text4);
                }
            }
        }

        private bool IsRelativeUrl(string url)
        {
            return !url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                   !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }
    }
}