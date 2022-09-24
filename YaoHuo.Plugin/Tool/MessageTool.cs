using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaoHuo.Plugin.Tool
{
    /// <summary>
    /// 消息工具
    /// </summary>
    public class MessageTool
    {
        /// <summary>
        /// 获取清空新消息脚本
        /// </summary>
        /// <param name="siteid">系统模块</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static string GetClearNewSql(string siteid = "1000", string userid = "")
        {
            var sqlStr = string.Empty;
            if (!string.IsNullOrEmpty(userid) && userid != "0")
            {
                sqlStr = $@" update wap_message set isnew = 0 where isnew = 1 and siteid = {siteid} and touserid = {userid}";
            }
            return sqlStr;
        }
    }
}