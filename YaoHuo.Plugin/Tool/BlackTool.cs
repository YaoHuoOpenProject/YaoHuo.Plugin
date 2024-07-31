using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

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
        /// <param name="userInfo">用户信息</param>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="addUserID">要拉黑的用户ID</param>
        /// <returns></returns>
        public static string AddBlackUser(user_Model userInfo, string connStr, string addUserID)
        {
            //黑名单上限
            var blackUp = 20;
            //会员用户增加黑名单上限
            switch (userInfo.SessionTimeout)
            {
                case 101:
                case 358:
                    blackUp = 30;
                    break;

                case 105:
                case 140:
                case 180:
                    blackUp = 40;
                    break;

                default:
                    break;
            }
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
            // 移除前导零并检查
            var processedUserID = Regex.Replace(addUserID, @"^0+", "");
            //白名单不能黑
            if (noBlockList.Contains(processedUserID))
            {
                return "NOTBLACK";
            }
            //经验未达到1000却拉了一个黑名单（准备拉第二个黑名单）
            else if (userInfo.expr <= 1000 && friendsCount >= blackUp - 10)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 2000 && friendsCount >= blackUp - 9)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 5000 && friendsCount >= blackUp - 8)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 10000 && friendsCount >= blackUp - 7)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 50000 && friendsCount >= blackUp - 6)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 100000 && friendsCount >= blackUp - 5)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 200000 && friendsCount >= blackUp - 4)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 300000 && friendsCount >= blackUp - 3)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 500000 && friendsCount >= blackUp - 2)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 800000 && friendsCount >= blackUp - 1)
            {
                return "UPMAX";
            }
            else if (userInfo.expr <= 1000000 && friendsCount >= blackUp - 0)
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

        /// <summary>
        /// 是否是黑名单用户
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="muUserID">用户ID</param>
        /// <param name="isUserID">验证的用户ID</param>
        /// <returns></returns>
        public static bool IsBlackUser(string connStr, string muUserID, string isUserID)
        {
            var sqlStr = $"select count(0) from wap_friends where friendtype = 1 and userid = '{muUserID}' and frienduserid = '{isUserID}'";
            var isCount = DbHelperSQL.ExecuteScalar(connStr, CommandType.Text, sqlStr).ToInt();
            return isCount > 0;
        }

        /// <summary>
        /// 获取排除用户脚本
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public static string GetExcludeUserSql(string userId, string name = "book_pub")
        {
            //会员角色ID
            var vipRoleIDs = new string[]
            {
                "101",
                "105",
                "140",
                "180",
                "358",
            };
            //排除黑名单用户脚本
            var sqlStr = string.Empty;
            if (!userId.IsNullOrZero())
            {
                //普通用户，不显示黑名单的帖子
                sqlStr = $@" and {name} not in (
    select frienduserid
    from wap_friends
    where friendtype = 1
    and userid = {userId}
)";
                //普通用户被会员拉黑时，不显示会员的帖子
                sqlStr += $@" and  {name} not in (
    select t1.userid
    from wap_friends t1
    inner join UserVO_View t2 on t1.userid = t2.userid
    where friendtype = 1
    and t1.frienduserid = {userId}
    and t2.SessionTimeout in ({string.Join(",", vipRoleIDs)})
)";
            }
            return sqlStr;
        }
    }
}
