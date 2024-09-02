using System;
using System.Collections.Generic;
using System.Data;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Report_add : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public List<class_Model> classList = new List<class_Model>();

        public string action = "";

        public string id = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string toclassid = "";

        public int int_0 = 1;

        // 数据库连接字符串
        private string ConnectionString => PubConstant.GetConnectionString(string_10);

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            page = GetRequestValue("page");
            id = GetRequestValue("id");
            if (action == "gomod")
            {
                try
                {
                    // 检查是否已经举报过
                    string sqlStr = $"SELECT COUNT(1) FROM [wap2_bbs_report] WHERE [bbsid]={id} AND [userid]={userid}";
                    int reportCount = (int)DbHelperSQL.ExecuteScalar(ConnectionString, CommandType.Text, sqlStr);

                    if (reportCount > 0)
                    {
                        INFO = "ALREADY_REPORTED";
                    }
                    else
                    {
                        wap2_bbs_report_Model wap2_bbs_report_Model = new wap2_bbs_report_Model();
                        wap2_bbs_report_BLL wap2_bbs_report_BLL = new wap2_bbs_report_BLL(string_10);
                        string requestValue = GetRequestValue("reporttype");
                        string requestValue2 = GetRequestValue("reportwhy");
                        wap2_bbs_report_Model.siteid = long.Parse(siteid);
                        wap2_bbs_report_Model.classid = long.Parse(classid);
                        wap2_bbs_report_Model.bbsid = long.Parse(id);
                        wap2_bbs_report_Model.userid = long.Parse(userid);
                        wap2_bbs_report_Model.nickname = nickname;
                        wap2_bbs_report_Model.ReportType = requestValue;
                        wap2_bbs_report_Model.ReportWhy = requestValue2;
                        wap2_bbs_report_Model.addtime = DateTime.Now;
                        wap2_bbs_report_Model.types = 0L;
                        INFO = "OK";
                        wap2_bbs_report_BLL.Add(wap2_bbs_report_Model);
                    }
                }
                catch (Exception ex)
                {
                    ERROR = ex.ToString();
                }
            }
        }
    }
}