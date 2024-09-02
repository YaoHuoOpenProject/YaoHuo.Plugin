using System;
using System.Collections.Generic;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;

using YaoHuo.Plugin.Tool;

using YaoHuo.Plugin.WebSite;
using System.Data;
using KeLin.ClassManager.ExUtility;

namespace YaoHuo.Plugin.BBS

{

    public class Report_add : MyPageWap

    {
        private string a = PubConstant.GetAppString("InstanceName");

        public List<class_Model> classList = new List<class_Model>();

        public string action = "";


        public string id = "";

        public string page = "";


        public string INFO = "";


        public string ERROR = "";


        public string toclassid = "";


        public int num = 1;

        // 数据库连接字符串
        private string ConnectionString => PubConstant.GetConnectionString(a);

        protected void Page_Load(object sender, EventArgs e)

        {

            while (true)

            {

                action = GetRequestValue("action");

                page = GetRequestValue("page");

                id = GetRequestValue("id");
                bool flag = !(action == "gomod");

                int num = 0;
                while (true)

                {

                    switch (num)

                    {

                        case 0:

                            if (true)

                            {

                            }

                            if (!flag)
                            {

                                num = 3;
                                continue;
                            }

                            return;
                        case 2:

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

                                    wap2_bbs_report_BLL wap2_bbs_report_BLL = new wap2_bbs_report_BLL(a);

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

                            num = 1;

                            continue;

                        case 3:

                            num = 2;
                            continue;

                        case 1:

                            return;

                    }
                    break;

                }
            }

        }

    }
}