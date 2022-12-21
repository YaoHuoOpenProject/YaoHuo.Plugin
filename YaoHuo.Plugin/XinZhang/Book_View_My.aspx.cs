using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace YaoHuo.Plugin.XinZhang
{
    public class Book_View_My : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string ConnectionString
        {
            get
            {
                return PubConstant.GetConnectionString(a);
            }
        }

        public string id = "0";

        public XinZhang_Model bookVo = new XinZhang_Model();

        public StringBuilder strhtml = new StringBuilder();

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string action = "";

        public string backurl = "";

        public string pw = "";

        public string ordertype = "";

        public string g = "";

        public string type = "";

        /// <summary>
        /// 隐藏的勋章
        /// </summary>
        public string HideMoneyName
        {
            get
            {
                //初始化新功能表
                /*
CREATE TABLE [dbo].[XinZhang_Plugin] (
[userid] bigint  NOT NULL,
[siteid] bigint  NOT NULL,
[moneyname] ntext COLLATE Chinese_PRC_CI_AS  NULL
)
GO
                 */
                var sqlStr = "select top 1 moneyname from XinZhang_Plugin where siteid=" + base.siteid + " and userid=" + base.userVo.userid;
                var myMoneyName = DbHelperSQL.ExecuteScalar(ConnectionString, CommandType.Text, sqlStr);
                if (myMoneyName == null)
                {
                    sqlStr = "insert XinZhang_Plugin(userid,siteid,moneyname) values('" + base.userVo.userid + "','" + base.siteid + "','')";
                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                }
                return myMoneyName.ToStr();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_00ac
            while (true)
            {
                this.id = base.GetRequestValue("id");
                this.g = base.GetRequestValue("g");
                this.lpage = base.GetRequestValue("lpage");
                this.action = base.GetRequestValue("action");
                this.pw = base.GetRequestValue("pw");
                this.ordertype = base.GetRequestValue("ordertype");
                bool flag = !(this.g.Trim() != "");
                int num;
                num = 0;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 1;
                        case 2:
                            try
                            {
                                while (true)
                                {
                                    flag = !(this.action == "godel");
                                    num = 4;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 4:
                                                if (!flag)
                                                {
                                                    num = 1;
                                                    continue;
                                                }
                                                goto case 6;
                                            case 1:
                                                flag = !(PubConstant.md5(this.pw).ToLower() != base.userVo.password.ToLower());
                                                num = 0;
                                                continue;
                                            case 0:
                                                if (!flag)
                                                {
                                                    num = 2;
                                                    continue;
                                                }
                                                //删除勋章
                                                if (this.type == "删除")
                                                {
                                                    //删除显示
                                                    base.userVo.moneyname = base.userVo.moneyname.Replace(this.id, "").Replace("||", "|");
                                                    var updCount = base.MainBll.UpdateSQL("update [user] set moneyname='" + base.userVo.moneyname + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                                                }
                                                //隐藏勋章
                                                if (this.type == "隐藏")
                                                {
                                                    //删除显示
                                                    base.userVo.moneyname = base.userVo.moneyname.Replace(this.id, "").Replace("||", "|");
                                                    var updCount = base.MainBll.UpdateSQL("update [user] set moneyname='" + base.userVo.moneyname + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                                                    //添加隐藏
                                                    var setMoneyName = HideMoneyName + (!string.IsNullOrEmpty(HideMoneyName) ? "|" : "") + this.id;
                                                    var sqlStr = "update XinZhang_Plugin set moneyname='" + setMoneyName + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid;
                                                    if (updCount > 0) DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                }
                                                //还原的勋章
                                                else if (this.type == "还原")
                                                {
                                                    //删除隐藏
                                                    var myMoneyName = HideMoneyName.Replace(this.id, "").Replace("||", "|");
                                                    var sqlStr = "update XinZhang_Plugin set moneyname='" + myMoneyName + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid;
                                                    var updCount = DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //还原
                                                    var setMoneyName = base.userVo.moneyname + (!string.IsNullOrEmpty(base.userVo.moneyname) ? "|" : "") + this.id;
                                                    sqlStr = "update [user] set moneyname='" + setMoneyName + "' where siteid=" + base.siteid + " and userid=" + base.userVo.userid;
                                                    if (updCount > 0) DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = setMoneyName;
                                                }
                                                this.INFO = "OK";
                                                num = 5;
                                                continue;
                                            case 3:
                                            case 5:
                                                num = 6;
                                                continue;
                                            case 2:
                                                this.INFO = "NOPASS";
                                                num = 3;
                                                continue;
                                            case 6:
                                                num = 7;
                                                continue;
                                            case 7:
                                                return;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.ERROR = ex.ToString();
                                return;
                            }
                        case 1:
                            this.backurl = "xinzhang/book_view_my.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;lpage=" + this.lpage + "&amp;ordertype=" + this.ordertype;
                            base.IsLogin(base.userid, this.backurl);
                            num = 2;
                            continue;
                        case 3:
                            //删除
                            this.type = this.g.Contains("删除") ? "删除" : this.type;
                            this.g = this.g.Replace("删除_", "");
                            //隐藏
                            this.type = this.g.Contains("隐藏") ? "隐藏" : this.type;
                            this.g = this.g.Replace("隐藏_", "");
                            //还原
                            this.type = this.g.Contains("还原") ? "还原" : this.type;
                            this.g = this.g.Replace("还原_", "");
                            //参数传递
                            this.id = this.g;
                            num = 1;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
