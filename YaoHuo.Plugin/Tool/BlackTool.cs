using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using System.Collections.Generic;
using System.Data;

namespace YaoHuo.Plugin.Tool
{
    /// <summary>
    /// 黑名单工具
    /// </summary>
    public class BlackTool
    {
        /// <summary>
        /// 添加黑名单用户
        /// </summary>
        /// <param name="userInfo">查询用户信息</param>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="addUserID">要拉黑的用户ID</param>
        /// <returns></returns>
        public static string AddBlackUser(user_Model userInfo, string connStr, string addUserID)
        {
            //黑名单上限
            var blackUp = 6;
            //会员用户增加黑名单上限
            if (userInfo.SessionTimeout == 105) blackUp = 15;
            //获取拉取黑名单数量
            var sqlStr = $"select count(0) from wap_friends where friendtype = 1 and userid = {userInfo.userid}";
            var friendsCount = DbHelperSQL.ExecuteScalar(connStr, CommandType.Text, sqlStr).ToInt();
            //白名单用户
            var noBlockList = new List<string>()
            {
                "1000",
                "11637",
                "36787",
            };
            //白名单不能黑
            if (noBlockList.IndexOf(addUserID) != -1)
            {
                return "NOTBLACK";
            }
            //经验未达到2000却拉了一个黑名单（准备拉第二个黑名单）
            else if (userInfo.expr <= 2000 && friendsCount >= blackUp - 6)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 5000 && friendsCount >= blackUp - 5)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 10000 && friendsCount >= blackUp - 4)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 50000 && friendsCount >= blackUp - 3)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 100000 && friendsCount >= blackUp - 2)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 200000 && friendsCount >= blackUp - 1)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 300000 && friendsCount >= blackUp - 0)
            {
                return "UPMAX";
            }
            //限制黑名单上限
            else if (friendsCount >= blackUp)
            {
                return "UPMAX";
            }
            return "";
        }
    }
}
