using KeLin.ClassManager;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyInfo : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string INFO = "";
        public string ERROR = "";
        public string aihao = "";
        public string qq = "";
        public string ot = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
            needPassWordToAdmin();
            string text = base.Request.Form.Get("action");
            aihao = (userVo.aihao + "___").Split('_')[0];
            qq = (userVo.aihao + "___").Split('_')[1];
            ot = WapTool.GetSiteDefault(siteVo.Version, 50);
            if (!WapTool.IsNumeric(ot) || ot == "0")
            {
                ot = "50";
            }
            if (!(text == "gomod"))
            {
                return;
            }
            try
            {
                // 验证并更新用户信息
                if (!ValidateAndUpdateUserInfo())
                {
                    return;
                }

                MainBll.UpdateUser_WAP(userVo);
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }

        private bool ValidateAndUpdateUserInfo()
        {
            // 性别验证（特殊处理，因为不允许为空）
            string sexStr = GetRequestValue("sex");
            if (!long.TryParse(sexStr, out long sex) || (sex != 0 && sex != 1))
            {
                ERROR = "性别格式不正确";
                return false;
            }
            userVo.sex = sex;

            // 通用验证方法
            if (!ValidateField("age", 10L, 99L, "年龄必须为10-99之间的数字", out long age))
                return false;
            userVo.age = age;

            if (!ValidateField("shenggao", 100, 250, "身高必须为100-250之间的数字", out int shenggao))
                return false;
            userVo.shenggao = shenggao.ToString();

            if (!ValidateField("tizhong", 30, 300, "体重必须为30-300之间的数字", out int tizhong))
                return false;
            userVo.tizhong = tizhong.ToString();

            // 星座验证
            if (!ValidateZodiacSign("xingzuo", out string xingzuo))
                return false;
            userVo.xingzuo = xingzuo;

            // 汉字字段验证
            if (!ValidateChineseField("aihao", 0, 10, "爱好最多10个汉字", out string aihao))
                return false;
            this.aihao = aihao;

            if (!ValidateChineseField("fenfuo", 0, 2, "婚否最多2个汉字", out string fenfuo))
                return false;
            userVo.fenfuo = fenfuo;

            if (!ValidateChineseField("zhiye", 0, 5, "职业最多5个汉字", out string zhiye))
                return false;
            userVo.zhiye = zhiye;

            if (!ValidateChineseField("city", 0, 8, "城市最多8个汉字", out string city))
                return false;
            userVo.city = city;

            // 其他字段验证
            if (!ValidateStringField("mobile", 11, 11, @"^\d{11}$", "手机号必须为11位数字", out string mobile))
                return false;
            userVo.mobile = mobile;

            if (!ValidateStringField("email", 0, 30, @"^.+@.+\..+$", "邮箱格式不正确或超过30个字符", out string email))
                return false;
            userVo.email = email;

            if (!ValidateStringField("qq", 5, 11, @"^\d{5,11}$", "QQ号必须为5-11位数字", out string qq))
                return false;
            this.qq = qq;

            if (!ValidateStringField("remark", 0, 15, null, "签名最多15个字符", out string remark))
                return false;
            userVo.remark = remark;

            // 更新爱好和QQ
            userVo.aihao = aihao + "_" + qq;

            return true;
        }

        private bool ValidateField<T>(string fieldName, T min, T max, string errorMessage, out T value) where T : struct, IComparable<T>
        {
            string strValue = GetRequestValue(fieldName);
            if (string.IsNullOrEmpty(strValue))
            {
                value = default(T);
                return true;
            }
            if (!TryParse(strValue, out value) || value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            {
                ERROR = errorMessage;
                return false;
            }
            return true;
        }

        private bool TryParse<T>(string value, out T result)
        {
            try
            {
                result = (T)Convert.ChangeType(value, typeof(T));
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }

        private bool ValidateChineseField(string fieldName, int minLength, int maxLength, string errorMessage, out string value)
        {
            value = GetRequestValue(fieldName);
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            if (value.Length < minLength || value.Length > maxLength || !Regex.IsMatch(value, @"^[\u4e00-\u9fa5]+$"))
            {
                ERROR = errorMessage;
                return false;
            }
            return true;
        }

        private bool ValidateStringField(string fieldName, int minLength, int maxLength, string pattern, string errorMessage, out string value)
        {
            value = GetRequestValue(fieldName);
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            if (value.Length < minLength || value.Length > maxLength)
            {
                ERROR = errorMessage;
                return false;
            }
            if (pattern != null && !Regex.IsMatch(value, pattern))
            {
                ERROR = errorMessage;
                return false;
            }
            return true;
        }

        private bool ValidateZodiacSign(string fieldName, out string value)
        {
            string[] validSigns = new string[]
            {
                "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座",
                "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座"
            };

            value = GetRequestValue(fieldName);
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            if (!validSigns.Contains(value))
            {
                ERROR = "必须填入十二星座（3个字）";
                return false;
            }
            return true;
        }
    }
}