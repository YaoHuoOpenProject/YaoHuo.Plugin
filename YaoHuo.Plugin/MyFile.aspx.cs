using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using KeLin.WebSite;
using System;
using System.Data;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin
{
    public class MyFile : PageWap
    {
        public string INFO = "";

        public string ERROR = "";

        public string strHtml = "";

        public string strShowHtml = "";

        public string messagecount = "0";

        public string messageAll = "0";

        public string goodfriend = "0";

        public string type = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "myfile.aspx?siteid=" + siteid);
            if (WapTool.getArryString(siteVo.Version, '|', 54) == "1")
            {
                needPassWordToAdmin();
            }
            type = WapTool.GetSiteDefault(siteVo.Version, 27);
            try
            {
                strHtml = siteVo.siterowremark;
                var appString = PubConstant.GetAppString("InstanceName");
                var connectionString = PubConstant.GetConnectionString(appString);
                var commandText = "select count(id),(select count(id) from wap_message where siteid=" + siteid + " and isnew<>2 and touserid=" + userid + ") from wap_message where siteid=" + siteid + " and isnew=1 and  touserid=" + userid;
                var dataSet = DbHelperSQL.ExecuteDataset(connectionString, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0][0] != null)
                    {
                        messagecount = dataSet.Tables[0].Rows[0][0].ToString();
                    }
                    if (dataSet.Tables[0].Rows[0][1] != null)
                    {
                        messageAll = dataSet.Tables[0].Rows[0][1].ToString();
                    }
                }
                commandText = "select count(id) from wap_friends where friendtype=0 and userid=" + userid;
                dataSet = DbHelperSQL.ExecuteDataset(connectionString, CommandType.Text, commandText);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0][0] != null)
                {
                    goodfriend = dataSet.Tables[0].Rows[0][0].ToString();
                }
                addMoneyToMyBank();
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
            VisiteCount("进入我的地盘。");
        }
    }
}
