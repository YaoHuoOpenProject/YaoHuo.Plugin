﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace YaoHuo.Plugin
{
    /// <summary>
    /// 数据转换拓展包（zgcwkj.Util）
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// 转换成 Int 类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static int ToInt(this object value, int def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成 Double 类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static double ToDouble(this object value, double def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToDouble(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 截断 Double 类型的小数位
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="length">保留长度</param>
        /// <returns></returns>
        public static double ToTruncate(this double value, int length = 2)
        {
            var pow = Math.Pow(10, length);
            return Math.Truncate(value * pow) / pow;
        }

        /// <summary>
        /// 截断 Float 类型的小数位
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="length">保留长度</param>
        /// <returns></returns>
        public static double ToTruncate(this float value, int length = 2)
        {
            var pow = Math.Pow(10, length);
            return Math.Truncate(value * pow) / pow;
        }

        /// <summary>
        /// 转换成字符串类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static string ToStr(this object value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToString(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成字符串类型
        /// 去掉前后的空格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static string ToTrim(this object value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().Trim();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成字符串类型
        /// 去掉前面的空格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static string ToTrimStart(this object value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().TrimStart();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成字符串类型
        /// 去掉后面的空格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static string ToTrimEnd(this object value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().TrimEnd();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成布尔类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static bool ToBool(this object value, bool def = false)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToBoolean(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        /// <summary>
        /// 转换成时间类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static DateTime ToDate(this object value, DateTime def = default)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return def;
        }

        /// <summary>
        /// 转换成时间类型并格式化
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="dateMode">时间格式</param>
        /// <param name="def">失败值</param>
        /// <returns></returns>
        public static string ToDate(this object value, string dateMode, string def = "")
        {
            try
            {
                DateTime dateTime = value.ToDate();
                return dateTime.ToString(dateMode);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return def;
        }

        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime ToUnixByDate(this double timeStamp)
        {
            DateTime nowTime = new DateTime(1970, 1, 1, 0, 0, 0);
            if (timeStamp.ToString().Length == 13)
            {
                nowTime = nowTime.AddMilliseconds(timeStamp);
            }
            else
            {
                nowTime = nowTime.AddSeconds(timeStamp);
            }
            return TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local);
        }

        /// <summary>
        /// 时间转时间戳
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static double ToDateByUnix(this DateTime dateTime)
        {
            DateTime nowTime = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan nowSpan = dateTime - TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local);
            return nowSpan.TotalSeconds;
        }

        /// <summary>
        /// 时间转时间戳字符
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static string ToDateByUnixStr(this DateTime dateTime)
        {
            return dateTime.ToDateByUnix().ToStr();
        }

        /// <summary>
        /// 编码 Base64
        /// </summary>
        /// <param name="value">Data</param>
        /// <returns></returns>
        public static string ToBase64(this string value)
        {
            try
            {
                return Encoding.UTF8.GetBytes(value).ToBase64();
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 编码 Base64
        /// </summary>
        /// <param name="value">Data</param>
        /// <returns></returns>
        public static string ToBase64(this byte[] value)
        {
            try
            {
                return Convert.ToBase64String(value);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 解码 Base64
        /// </summary>
        /// <param name="value">Base64</param>
        /// <returns></returns>
        public static string ToUnBase64(this string value)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNull(this object value)
        {
            try
            {
                if (value == null)
                {
                    return true;
                }
                else if (string.IsNullOrEmpty(value.ToString()))
                {
                    return true;
                }
                else if (value.ToString().Length > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return true;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        /// <summary>
        /// 判断是否为空或零
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNullOrZero(this object value)
        {
            if (IsNull(value))
            {
                return true;
            }
            else if (value.ToStr() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
