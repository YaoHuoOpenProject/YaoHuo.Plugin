using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class LockUser_List_add : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";
        public string id = "";
        public string toclassid = "";
        public string touserid = "";
        public string backurlid = "";
        public string lockdate = "";
        public string INFO = "";
        public string ERROR = "";

        public wap_bbsre_Model bbsReVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            toclassid = GetRequestValue("toclassid");
            touserid = GetRequestValue("touserid");
            backurlid = GetRequestValue("backurlid");
            lockdate = GetRequestValue("lockdate");
            if (lockdate == "")
            {
                lockdate = "0";
            }
            CheckManagerLvl("04", classVo.adminusername, "bbs/book_list.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid);
            needPassWordToAdmin();
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                bool flag = true;
                user_Model user_Model = null;
                if (!WapTool.IsNumeric(touserid))
                {
                    touserid = "0";
                }
                user_Model = MainBll.getUserInfo(touserid, siteid);
                if (user_Model == null)
                {
                    flag = false;
                }
                else if (IsUserManager(touserid, user_Model.managerlvl, classVo.adminusername))
                {
                    flag = false;
                }
                if (!WapTool.IsNumeric(lockdate) || !WapTool.IsNumeric(touserid) || !WapTool.IsNumeric(toclassid))
                {
                    INFO = "NOTNUM";
                    return;
                }
                if (!flag)
                {
                    INFO = "NOTALLOW";
                    return;
                }

                // 初始化 user_lock_BLL
                user_lock_BLL user_lock_BLL = new user_lock_BLL(string_10);

                // 删除之前的加黑记录（仅当存在时）
                try
                {
                    // 删除该用户的所有加黑记录，并获取受影响的行数
                    string deleteSql = "DELETE FROM user_lock WHERE siteid = " + siteid + " AND lockuserid = " + touserid;
                    long rowsAffected = MainBll.UpdateSQL(deleteSql); // 确认 UpdateSQL 返回受影响的行数

                    if (rowsAffected > 0)
                    {
                        // 记录解除加黑的日志
                        string logSql = "INSERT INTO wap_log(siteid, oper_userid, oper_nickname, oper_type, log_info, oper_ip) " +
                                        "VALUES (" + siteid + "," + userid + ",'" + nickname + "',0," +
                                        "'清除用户ID" + touserid + "的所有加黑记录','" + IP + "')";
                        MainBll.UpdateSQL(logSql);
                    }
                    // 如果没有加黑记录，则不执行删除和日志记录
                }
                catch (Exception ex)
                {
                    // 记录删除旧加黑记录时的错误，但不阻止新加黑的进行
                    ERROR += "删除旧加黑记录时出错: " + ex.Message + "\n";
                }

                // 添加新的加黑记录
                string insertSql = "INSERT INTO user_lock (siteid, lockuserid, lockdate, operdate, operuserid, classid) VALUES (" +
                                   siteid + "," +
                                   touserid + "," +
                                   lockdate + "," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   userid + "," +
                                   toclassid + ")";
                MainBll.UpdateSQL(insertSql);

                // 记录加黑日志
                string logInsertSql = "INSERT INTO wap_log(siteid, oper_userid, oper_nickname, oper_type, log_info, oper_ip) " +
                                      "VALUES (" + siteid + "," + userid + ",'" + nickname + "',0," +
                                      "'加黑用户ID" + touserid + "','" + IP + "')";
                MainBll.UpdateSQL(logInsertSql);

                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}