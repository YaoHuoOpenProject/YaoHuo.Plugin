using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin
{
    public class WapLogin : PageWap
    {
        public string _InstanceName = PubConstant.GetAppString("InstanceName");

        public string KL_ModelSite_Close_Login = PubConstant.GetAppString("KL_ModelSite_Close_Login");

        public string backurl = "";

        public string sessid = "";

        public user_BLL user_BLL_0;

        public new user_Model userVo;

        public string logname = "";

        public string logpass = "";

        public string action = "";

        public string savesid = "";

        public string INFO = "";

        public bool pd = false;

        public string http_start2 = "";

        public string KL_LoginTime = PubConstant.GetAppString("KL_LoginTime");

        public string showQQLogin = "";

        public string showWXLogin = "";

        public string errorinfo = "";

        public string publicName = "";

        public string publicID = "";

        /// <summary>
        /// 前端密钥
        /// <para>谷歌人机验证</para>
        /// </summary>
        public string RecaptchaV2_Key => PubConstant.GetAppString("GoogleRecaptchaV2Key");

        /// <summary>
        /// 后端验证密钥
        /// <para>谷歌人机验证</para>
        /// </summary>
        public string RecaptchaV2_Response => PubConstant.GetAppString("GoogleRecaptchaV2Response");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!WapTool.IsNumeric(KL_LoginTime))
            {
                KL_LoginTime = "0";
            }
            errorinfo = GetRequestValue("errorinfo");
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == "")
            {
                backurl = "wapindex.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            if (KL_VERSION == "2" || KL_VERSION == "3")
            {
                pd = true;
            }
            action = GetRequestValue("action");
            savesid = GetRequestValue("savesid");
            //帐号
            logname = GetRequestValue("logname");
            logname = logname.Replace("\n", "").Trim();
            logname = logname.Replace("=", "");
            logname = logname.Replace(" ", "");
            //密码
            logpass = GetRequestValue("logpass");
            logpass = logpass.Replace("\n", "").Trim();
            logpass = logpass.Replace("=", "");
            logpass = logpass.Replace(" ", "");
            showQQLogin = WapTool.GetSiteDefault(siteVo.Version, 51);
            if (!WapTool.IsNumeric(showQQLogin))
            {
                showQQLogin = "1";
            }
            showWXLogin = WapTool.GetSiteDefault(siteVo.Version, 39);
            if (!WapTool.IsNumeric(showWXLogin))
            {
                showWXLogin = "1";
            }
            http_start2 = WapTool.GetDomain();
            http_start2 = http_start2.Split('|')[0];
            if (action == "weixin")
            {
                var wap_weixin_Config_BLL = new wap_weixin_Config_BLL(_InstanceName);
                var wap_weixin_Config_Model = wap_weixin_Config_BLL.GetModel(siteid);
                if (wap_weixin_Config_Model != null)
                {
                    publicName = wap_weixin_Config_Model.publicName;
                    publicID = wap_weixin_Config_Model.weiXinName;
                }
                INFO = "weixin";
            }
            else
            {
                if (!(action == "login")) return;
                //谷歌人机验证
                var rpToken = GetRequestValue("g-recaptcha-response");
                if (!string.IsNullOrEmpty(RecaptchaV2_Key))
                {
                    //进入登录逻辑标识
                    var goLogin = false;
                    //
                    try
                    {
                        if (!string.IsNullOrEmpty(rpToken))
                        {
                            //调用获取验证结果
                            //var postUrl = "https://www.google.com/recaptcha/api/siteverify";//服务器在国外时
                            var postUrl = "https://recaptcha.google.cn/recaptcha/api/siteverify";
                            var postData = string.Format("secret={0}&response={1}", RecaptchaV2_Response, rpToken);
                            var data = HttpTool.Post(postUrl, postData);
                            var jObject = JObject.Parse(data);
                            var rpOk = jObject["success"].ToStr().ToBool();
                            if (rpOk) goLogin = true;
                            //System.IO.File.WriteAllText(@"D://q1.txt", data + goLogin);
                        }
                    }
                    catch { }
                    //
                    if (!goLogin)
                    {
                        INFO = "NOTGOOGLERECAPTCHA";
                        return;
                    }
                }
                //帐号验证
                var fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                if (int.Parse(KL_LoginTime) > 0)
                {
                    if (WapTool.CheckStrCount(fcountSubMoneyFlag, "~") < int.Parse(KL_LoginTime))
                    {
                        MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "~,' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    }
                    else
                    {
                        INFO = "MAXLOGIN";
                    }
                }
                user_BLL_0 = new user_BLL(_InstanceName);
                if (logname == "")
                {
                    INFO = "IDNULL";
                }
                if (logpass == "")
                {
                    INFO = "PASSNULL";
                }
                if (INFO == "")
                {
                    if (WapTool.IsNumeric(logname))
                    {
                        userVo = user_BLL_0.GetPassFormID(long.Parse(siteid), long.Parse(logname));
                    }
                    if (userVo == null)
                    {
                        userVo = user_BLL_0.GetPassFormUsername(long.Parse(siteid), logname);
                    }
                    checkUser();
                }
                VisiteCount("进入网站。");
            }
        }

        /// <summary>
        /// 检查用户
        /// </summary>
        public void checkUser()
        {
            if (userVo == null)
            {
                INFO = "NOTEXIST";
                return;
            }
            if (userVo.password.ToLower() != logpass.ToLower() && userVo.password.ToLower() != PubConstant.md5(logpass).ToLower())
            {
                INFO = "PASSERR";
            }
            if (userVo.userid < 300L && KL_ModelSite_Close_Login != "1")
            {
                INFO = "USERLOCK";
            }
            if (userVo.LockUser.ToString() == "1")
            {
                INFO = "USERLOCK";
            }
            if (!(INFO == ""))
            {
                return;
            }
            Random random = new Random();
            int num = random.Next(5000, 99999);
            if (userVo.password.Length < 5)
            {
                sessid = PubConstant.md5(userVo.userid + userVo.password.Substring(0, 4));
            }
            else
            {
                sessid = PubConstant.md5(userVo.userid + userVo.password.Substring(0, 6));
            }
            string text = WapTool.EnCode_KL(siteid + "_" + num + "_" + userVo.userid + "_0_" + sessid);
            user_BLL_0.SetUserSID(long.Parse(siteid), userVo.userid, sessid);
            if (WapTool.IsNumeric(userVo.MailServerUserName))
            {
                var wap2_mobile_UA_BLL = new wap2_mobile_UA_BLL(_InstanceName);
                var wap2_mobile_UA_Model = wap2_mobile_UA_BLL.GetModel(long.Parse(userVo.MailServerUserName));
                if (wap2_mobile_UA_Model != null)
                {
                    myua = userVo.MailServerUserName;
                    width = wap2_mobile_UA_Model.widthpx.ToString();
                }
            }
            INFO = "OK";
            string text2 = base.Request.ServerVariables["HTTP_HOST"].Split('.')[0];
            sid = text + "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
            sid2 = "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
            if (savesid == "0")
            {
                base.Response.Cookies["sid" + text2].Expires = DateTime.Now.AddYears(1);
                base.Response.Cookies["sid" + text2].Value = sid;
                //帐号二次密码验证值
                base.Response.Cookies["GET" + userVo.userid].Expires = DateTime.Now.AddYears(1);
                base.Response.Cookies["GET" + userVo.userid].Value = PubConstant.md5(PubConstant.md5(PubConstant.md5($"{userVo.userid}{DateTime.Now.Day}")));
            }
            else
            {
                base.Response.Cookies["sid" + text2].Expires = DateTime.Now.AddHours(1.0);
                base.Response.Cookies["sid" + text2].Value = sid;
            }
            wmlVo.sid = sid;
            INFO = "OK";
        }
    }
}
