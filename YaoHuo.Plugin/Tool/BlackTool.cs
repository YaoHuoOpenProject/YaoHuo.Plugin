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
            //获取拉取黑名单数量
            var sqlStr = $"select count(0) from wap_friends where friendtype = 1 and userid = {userInfo.userid}";
            var friendsCount = DbHelperSQL.ExecuteScalar(connStr, CommandType.Text, sqlStr).ToInt();
            //不能拉取黑名单的ID
            var noBlockList = new List<string>()
            {
                "1000",
                "37567",
            };
            if (noBlockList.IndexOf(addUserID) != -1)
            {
                return "NOTBLACK";
            }
            //经验未达到十 却 拉了一个黑名单（准备拉第二个黑名单）
            else if (userInfo.expr <= 10 && friendsCount >= 1)
            {
                return "UPMAX";
            }
            //限制黑名单上限
            else if (friendsCount >= 1)
            {
                return "UPMAX";
            }
            return "";
        }
    }
}
