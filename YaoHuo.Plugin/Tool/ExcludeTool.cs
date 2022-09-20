namespace YaoHuo.Plugin.Tool
{
    /// <summary>
    /// 排除工具
    /// </summary>
    public class ExcludeTool
    {
        /// <summary>
        /// 获取排除用户脚本
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static string GetExcludeUserSql(string name = "book_pub", string userid = "")
        {
            var sqlStr = string.Empty;
            if (!string.IsNullOrEmpty(userid) && userid != "0")
            {
                sqlStr = $@" and {name} not in (select frienduserid from wap_friends where friendtype = 1 and userid = {userid})";
            }
            return sqlStr;
        }
    }
}
