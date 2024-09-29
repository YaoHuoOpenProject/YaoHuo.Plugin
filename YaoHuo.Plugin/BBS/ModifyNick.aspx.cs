using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using System;
using System.Data;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyNick : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string INFO = "";

        public string ERROR = "";

        public string tonickname = "";

        public string backurl = "";

        public string KL_Check_Repeat_Nickname = PubConstant.GetAppString("KL_Check_Repeat_Nickname");

        public string num = "0";

        private static readonly string[] ForbiddenNicknames = ConfigurationManager.AppSettings["ForbiddenNicknames"].Split(',');

        protected void Page_Load(object sender, EventArgs e)
        {
            string text = base.Request.Form.Get("action");
            IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
            needPassWordToAdmin();
            num = WapTool.GetSiteDefault(siteVo.Version, 48);
            if (!WapTool.IsNumeric(num) || num == "0")
            {
                num = "16";
            }
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "bbs/modifyuserinfo.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            tonickname = userVo.nickname;
            if (!(text == "gomod"))
            {
                return;
            }
            tonickname = GetRequestValue("tonickname");

            // 移除所有空格(包括全角空格)
            tonickname = RemoveAllSpaces(tonickname);

            if (tonickname.Length > 15)
            {
                tonickname = tonickname.Substring(0, 15);
            }
            if (string.IsNullOrEmpty(tonickname))
            {
                INFO = "NULL";
                return;
            }

            // 检查是否包含禁用昵称
            if (ContainsForbiddenNickname(tonickname))
            {
                INFO = "FORBIDDEN";
                return;
            }

            // 检查上次修改昵称的时间
            if (!CanChangeNickname())
            {
                INFO = "TOOFREQUENT";
                return;
            }
            if (KL_Check_Repeat_Nickname != "1" && MainBll.isHasExistNickname(siteid, tonickname))
            {
                INFO = "HASEXIST";
                return;
            }
            if (num != "0")
            {
                tonickname = WapTool.left(tonickname, int.Parse(num));
            }
            MainBll.UpdateSQL($"update [user] set nickname='{tonickname}', LastNickChangeDate=GETDATE() where siteid={siteid} and userid={userid}");
            INFO = "OK";
        }

        private bool CanChangeNickname()
        {
            string sqlStr = $"SELECT LastNickChangeDate FROM [user] WHERE siteid={siteid} AND userid={userid}";
            object result = DbHelperSQL.ExecuteScalar(ConnectionString, CommandType.Text, sqlStr);

            if (result != null && result != DBNull.Value)
            {
                DateTime lastChangeDate = Convert.ToDateTime(result);
                if (DateTime.Now.Year == lastChangeDate.Year && DateTime.Now.Month == lastChangeDate.Month)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ContainsForbiddenNickname(string nickname)
        {
            return ForbiddenNicknames.Any(forbidden => nickname.ToLower().Contains(forbidden.Trim().ToLower()));
        }

        private string RemoveAllSpaces(string input)
        {
            // 移除所有空格(包括全角空格)
            return Regex.Replace(input, @"\s+", "", RegexOptions.Compiled);
        }

        private string ConnectionString => PubConstant.GetConnectionString(a);
    }
}