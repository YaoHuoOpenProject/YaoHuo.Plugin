using System;

namespace YaoHuo.Plugin.Tool
{
    /// <summary>
    /// 转账工具
    /// </summary>
    public class SendMoneyTool
    {
        /// <summary>
        /// 是否合理
        /// </summary>
        /// <param name="moneyStr">钱</param>
        /// <returns></returns>
        public static bool IsReasonable(string moneyStr)
        {
            var money = long.Parse(moneyStr);
            //小于 101
            if (money < 101) return false;
            //大于 20202
            else if (money > 20202) return false;
            //返回不在合理的范围
            return true;
        }

        /// <summary>
        /// 获取手续费
        /// </summary>
        /// <param name="moneyStr">钱</param>
        /// <returns></returns>
        public static long GetHandlingFee(string moneyStr)
        {
            var money = double.Parse(moneyStr);
            //费率
            var rates = 0.01;
            //手续费
            var result = money * rates;
            //返回手续费
            return Convert.ToInt64(result);
        }
    }
}
