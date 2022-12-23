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
                var sqlStr = $"select top 1 moneyname from XinZhang_Plugin where siteid={base.siteid} and userid={base.userVo.userid}";
                var myMoneyName = DbHelperSQL.ExecuteScalar(ConnectionString, CommandType.Text, sqlStr);
                if (myMoneyName == null)
                {
                    sqlStr = $"insert XinZhang_Plugin(userid,siteid,moneyname) values('{base.userVo.userid}','{base.siteid}','')";
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
                                                //查找勋章是否存在（防止不规范的数据）
                                                //if (this.type != "隐藏全部" && this.type != "显示全部")
                                                //{
                                                //    var sqlStr = $"select count(0) from XinZhang where XinZhangTuPian = @XinZhangTuPian";
                                                //    var array = new SqlParameter[]
                                                //    {
                                                //        new SqlParameter("@XinZhangTuPian", SqlDbType.NVarChar),
                                                //    };
                                                //    array[0].Value = this.id;
                                                //    var isXinZhang = DbHelperSQL.ExecuteScalar(ConnectionString, CommandType.Text, sqlStr, array).ToInt();
                                                //    if (isXinZhang == 0)
                                                //    {
                                                //        this.INFO = "NO";
                                                //        return;
                                                //    }
                                                //}
                                                //删除勋章
                                                if (this.type == "删除")
                                                {
                                                    //删除显示
                                                    var myMoneyName = base.userVo.moneyname.Replace("||", "|").Trim('|');
                                                    var updMoneyName = myMoneyName.Replace(this.id, "");
                                                    if (myMoneyName == updMoneyName)
                                                    {
                                                        this.INFO = "NO";
                                                        return;
                                                    }
                                                    var sqlStr = $"update [user] set moneyname='{updMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = updMoneyName;
                                                }
                                                //隐藏勋章
                                                else if (this.type == "隐藏")
                                                {
                                                    //删除显示
                                                    var myMoneyName = base.userVo.moneyname.Replace("||", "|").Trim('|');
                                                    var updMoneyName = myMoneyName.Replace(this.id, "");
                                                    if (myMoneyName == updMoneyName)
                                                    {
                                                        this.INFO = "NO";
                                                        return;
                                                    }
                                                    var sqlStr = $"update [user] set moneyname='{updMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                    //添加隐藏
                                                    var myHideMoneyName = HideMoneyName.Replace("||", "|").Trim('|');
                                                    var setHideMoneyName = myHideMoneyName + "|" + this.id;
                                                    sqlStr = $"update XinZhang_Plugin set moneyname='{setHideMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = updMoneyName;
                                                }
                                                //隐藏全部勋章
                                                else if (this.type == "隐藏全部")
                                                {
                                                    var showMoneyName = base.userVo.moneyname;
                                                    //删除显示
                                                    var sqlStr = $"update [user] set moneyname='' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                    //添加隐藏
                                                    var myHideMoneyName = HideMoneyName.Replace("||", "|").Trim('|');
                                                    var setHideMoneyName = myHideMoneyName + "|" + showMoneyName;
                                                    sqlStr = $"update XinZhang_Plugin set moneyname='{setHideMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = "";
                                                }
                                                //显示勋章
                                                else if (this.type == "显示")
                                                {
                                                    //删除隐藏
                                                    var myHideMoneyName = HideMoneyName.Replace("||", "|").Trim('|');
                                                    var updHideMoneyName = myHideMoneyName.Replace(this.id, "");
                                                    if (myHideMoneyName == updHideMoneyName)
                                                    {
                                                        this.INFO = "NO";
                                                        return;
                                                    }
                                                    var sqlStr = $"update XinZhang_Plugin set moneyname='{updHideMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //还原显示
                                                    var myMoneyName = base.userVo.moneyname.Replace("||", "|").Trim('|');
                                                    var setMoneyName = myMoneyName + "|" + this.id;
                                                    sqlStr = $"update [user] set moneyname='{setMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = setMoneyName;
                                                }
                                                //显示全部勋章
                                                else if (this.type == "显示全部")
                                                {
                                                    var hideMoneyName = HideMoneyName;
                                                    //删除隐藏
                                                    var sqlStr = $"update XinZhang_Plugin set moneyname='' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //还原显示
                                                    var myMoneyName = base.userVo.moneyname.Replace("||", "|").Trim('|');
                                                    var setMoneyName = myMoneyName + "|" + hideMoneyName;
                                                    sqlStr = $"update [user] set moneyname='{setMoneyName}' where siteid={base.siteid} and userid={base.userVo.userid}";
                                                    DbHelperSQL.ExecuteQuery(ConnectionString, sqlStr);
                                                    //立刻刷新界面数据
                                                    base.userVo.moneyname = setMoneyName;
                                                }
                                                //未知的操作
                                                else
                                                {
                                                    this.INFO = "NO";
                                                    return;
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
                            //隐藏全部
                            this.type = this.g.Contains("隐藏全部") ? "隐藏全部" : this.type;
                            //显示
                            this.type = this.g.Contains("显示") ? "显示" : this.type;
                            this.g = this.g.Replace("显示_", "");
                            //显示全部
                            this.type = this.g.Contains("显示全部") ? "显示全部" : this.type;
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
