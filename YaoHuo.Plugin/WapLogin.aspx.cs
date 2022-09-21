using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
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

        public user_BLL bll;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_0323
            string fcountSubMoneyFlag = default(string);
            wap_weixin_Config_Model wap_weixin_Config_Model = default(wap_weixin_Config_Model);
            while (true)
            {
                bool flag = WapTool.IsNumeric(KL_LoginTime);
                int num = 24;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 24:
                            if (!flag)
                            {
                                num = 14;
                                continue;
                            }
                            goto case 33;
                        case 3:
                            INFO = "PASSNULL";
                            num = 39;
                            continue;
                        case 9:
                            num = 15;
                            continue;
                        case 15:
                            num3 = ((!(backurl == "")) ? 1 : 0);
                            goto IL_0387;
                        case 28:
                            flag = !(backurl == "");
                            num = 16;
                            continue;
                        case 16:
                            if (!flag)
                            {
                                num = 23;
                                continue;
                            }
                            goto case 31;
                        case 26:
                            flag = WapTool.CheckStrCount(fcountSubMoneyFlag, "~") >= int.Parse(KL_LoginTime);
                            num = 21;
                            continue;
                        case 21:
                            if (!flag)
                            {
                                num = 34;
                                continue;
                            }
                            INFO = "MAXLOGIN";
                            num = 40;
                            continue;
                        case 56:
                            num = 46;
                            continue;
                        case 46:
                            num2 = ((!(KL_VERSION == "3")) ? 1 : 0);
                            goto IL_0649;
                        case 6:
                            flag = !WapTool.IsNumeric(logname);
                            num = 43;
                            continue;
                        case 43:
                            if (!flag)
                            {
                                num = 38;
                                continue;
                            }
                            goto case 10;
                        case 35:
                            INFO = "IDNULL";
                            num = 51;
                            continue;
                        case 38:
                            userVo = bll.GetPassFormID(long.Parse(siteid), long.Parse(logname));
                            num = 10;
                            continue;
                        case 10:
                            flag = userVo != null;
                            num = 13;
                            continue;
                        case 13:
                            if (!flag)
                            {
                                num = 19;
                                continue;
                            }
                            goto case 22;
                        case 23:
                            backurl = "wapindex.aspx?siteid=" + siteid;
                            num = 31;
                            continue;
                        case 0:
                            num = (flag ? 32 : 2);
                            continue;
                        case 17:
                            pd = true;
                            num = 42;
                            continue;
                        case 18:
                            publicName = wap_weixin_Config_Model.publicName;
                            publicID = wap_weixin_Config_Model.weiXinName;
                            num = 45;
                            continue;
                        case 48:
                            if (!flag)
                            {
                                num = 11;
                                continue;
                            }
                            goto case 28;
                        case 30:
                            bll = new user_BLL(_InstanceName);
                            flag = !(logname == "");
                            num = 50;
                            continue;
                        case 50:
                            if (!flag)
                            {
                                num = 35;
                                continue;
                            }
                            goto case 51;
                        case 45:
                            INFO = "weixin";
                            num = 55;
                            continue;
                        case 55:
                            return;

                        case 37:
                            showQQLogin = "1";
                            num = 41;
                            continue;
                        case 42:
                            logname = GetRequestValue("logname");
                            logpass = GetRequestValue("logpass");
                            action = GetRequestValue("action");
                            savesid = GetRequestValue("savesid");
                            logname = logname.Replace("\n", "").Trim();
                            logname = logname.Replace("=", "");
                            logname = logname.Replace(" ", "");
                            logpass = logpass.Replace("\n", "").Trim();
                            logpass = logpass.Replace("=", "");
                            logpass = logpass.Replace(" ", "");
                            showQQLogin = WapTool.GetSiteDefault(siteVo.Version, 51);
                            flag = WapTool.IsNumeric(showQQLogin);
                            num = 12;
                            continue;
                        case 12:
                            if (!flag)
                            {
                                num = 37;
                                continue;
                            }
                            goto case 41;
                        case 20:
                            showWXLogin = "1";
                            num = 7;
                            continue;
                        case 2:
                            fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                            flag = int.Parse(KL_LoginTime) <= 0;
                            num = 1;
                            continue;
                        case 1:
                            if (!flag)
                            {
                                num = 26;
                                continue;
                            }
                            goto case 30;
                        case 22:
                            checkUser();
                            num = 47;
                            continue;
                        case 41:
                            showWXLogin = WapTool.GetSiteDefault(siteVo.Version, 39);
                            flag = WapTool.IsNumeric(showWXLogin);
                            num = 54;
                            continue;
                        case 54:
                            if (!flag)
                            {
                                num = 20;
                                continue;
                            }
                            goto case 7;
                        case 4:
                            if (!flag)
                            {
                                num = 17;
                                continue;
                            }
                            goto case 42;
                        case 39:
                            flag = !(INFO == "");
                            num = 49;
                            continue;
                        case 49:
                            if (!flag)
                            {
                                num = 6;
                                continue;
                            }
                            goto case 47;
                        case 25:
                            {
                                wap_weixin_Config_BLL wap_weixin_Config_BLL = new wap_weixin_Config_BLL(_InstanceName);
                                wap_weixin_Config_Model = new wap_weixin_Config_Model();
                                wap_weixin_Config_Model = wap_weixin_Config_BLL.GetModel(siteid);
                                flag = wap_weixin_Config_Model == null;
                                num = 27;
                                continue;
                            }
                        case 27:
                            if (!flag)
                            {
                                num = 18;
                                continue;
                            }
                            goto case 45;
                        case 11:
                            backurl = base.Request.Form.Get("backurl");
                            num = 28;
                            continue;
                        case 33:
                            errorinfo = GetRequestValue("errorinfo");
                            backurl = base.Request.QueryString.Get("backurl");
                            num = 44;
                            continue;
                        case 44:
                            num = ((backurl == null) ? 53 : 9);
                            continue;
                        case 53:
                            num3 = 0;
                            goto IL_0387;
                        case 14:
                            KL_LoginTime = "0";
                            num = 33;
                            continue;
                        case 5:
                            num2 = 0;
                            goto IL_0649;
                        case 34:
                            MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "~,' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                            num = 52;
                            continue;
                        case 19:
                            userVo = bll.GetPassFormUsername(long.Parse(siteid), logname);
                            num = 22;
                            continue;
                        case 51:
                            flag = !(logpass == "");
                            num = 57;
                            continue;
                        case 57:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 39;
                        case 47:
                            VisiteCount("进入网站。");
                            num = 29;
                            continue;
                        case 29:
                            return;

                        case 7:
                            http_start2 = WapTool.GetDomain();
                            http_start2 = http_start2.Split('|')[0];
                            flag = !(action == "weixin");
                            num = 8;
                            continue;
                        case 8:
                            if (flag)
                            {
                                flag = !(action == "login");
                                num = 0;
                            }
                            else
                            {
                                num = 25;
                            }
                            continue;
                        case 40:
                        case 52:
                            num = 30;
                            continue;
                        case 32:
                            return;

                        case 31:
                            backurl = ToHtm(backurl);
                            backurl = HttpUtility.UrlDecode(backurl);
                            backurl = WapTool.URLtoWAP(backurl);
                            num = 36;
                            continue;
                        case 36:
                            {
                                num = ((!(KL_VERSION == "2")) ? 56 : 5);
                                continue;
                            }
                        IL_0649:
                            flag = (byte)num2 != 0;
                            num = 4;
                            continue;
                        IL_0387:
                            flag = (byte)num3 != 0;
                            num = 48;
                            continue;
                    }
                    break;
                }
            }
        }

        public void checkUser()
        {
            //Discarded unreachable code: IL_0315
            string text2 = default(string);
            int num4 = default(int);
            wap2_mobile_UA_Model model = default(wap2_mobile_UA_Model);
            string text = default(string);
            while (true)
            {
                bool flag = userVo != null;
                int num = 0;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 0:
                            num = (flag ? 6 : 13);
                            continue;
                        case 27:
                            num = 2;
                            continue;
                        case 2:
                            num3 = ((!(KL_ModelSite_Close_Login != "1")) ? 1 : 0);
                            goto IL_0353;
                        case 1:
                        case 36:
                            text2 = WapTool.EnCode_KL(siteid + "_" + num4 + "_" + userVo.userid + "_0_" + sessid);
                            bll.SetUserSID(long.Parse(siteid), userVo.userid, sessid);
                            flag = !WapTool.IsNumeric(userVo.MailServerUserName);
                            num = 11;
                            continue;
                        case 11:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 28;
                        case 37:
                            flag = !(INFO == "");
                            num = 14;
                            continue;
                        case 14:
                            if (!flag)
                            {
                                num = 19;
                                continue;
                            }
                            goto case 10;
                        case 34:
                            INFO = "USERLOCK";
                            num = 18;
                            continue;
                        case 6:
                            num = ((!(userVo.password.ToLower() != logpass.ToLower())) ? 26 : 9);
                            continue;
                        case 3:
                            {
                                model = null;
                                wap2_mobile_UA_BLL wap2_mobile_UA_BLL = new wap2_mobile_UA_BLL(_InstanceName);
                                model = wap2_mobile_UA_BLL.GetModel(long.Parse(userVo.MailServerUserName));
                                flag = model == null;
                                num = 8;
                                continue;
                            }
                        case 8:
                            if (!flag)
                            {
                                num = 21;
                                continue;
                            }
                            goto case 30;
                        case 22:
                            num3 = 1;
                            goto IL_0353;
                        case 25:
                            if (!flag)
                            {
                                num = 34;
                                continue;
                            }
                            goto case 18;
                        case 10:
                            num = 33;
                            continue;
                        case 33:
                            return;

                        case 35:
                            INFO = "USERLOCK";
                            num = 37;
                            continue;
                        case 13:
                            INFO = "NOTEXIST";
                            num = 7;
                            continue;
                        case 7:
                            return;

                        case 26:
                            num2 = 1;
                            goto IL_03e2;
                        case 31:
                            if (!flag)
                            {
                                num = 32;
                                continue;
                            }
                            goto case 20;
                        case 21:
                            myua = userVo.MailServerUserName;
                            width = model.widthpx.ToString();
                            num = 30;
                            continue;
                        case 28:
                            INFO = "OK";
                            text = base.Request.ServerVariables["HTTP_HOST"].Split('.')[0];
                            sid = text2 + "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
                            sid2 = "-" + ver + "-" + cs + "-" + lang + "-" + myua + "-" + width;
                            flag = !(savesid == "0");
                            num = 15;
                            continue;
                        case 15:
                            if (flag)
                            {
                                base.Response.Cookies["sid" + text].Expires = DateTime.Now.AddHours(1.0);
                                base.Response.Cookies["sid" + text].Value = sid;
                                num = 24;
                            }
                            else
                            {
                                num = 23;
                            }
                            continue;
                        case 19:
                            {
                                Random random = new Random();
                                num4 = random.Next(5000, 99999);
                                flag = userVo.password.Length >= 5;
                                num = 29;
                                continue;
                            }
                        case 29:
                            if (flag)
                            {
                                sessid = PubConstant.md5(userVo.userid + userVo.password.Substring(0, 6));
                                num = 1;
                            }
                            else
                            {
                                num = 17;
                            }
                            continue;
                        case 23:
                            base.Response.Cookies["sid" + text].Expires = DateTime.Now.AddYears(1);
                            base.Response.Cookies["sid" + text].Value = sid;
                            num = 5;
                            continue;
                        case 32:
                            INFO = "PASSERR";
                            num = 20;
                            continue;
                        case 18:
                            flag = !(userVo.LockUser.ToString() == "1");
                            num = 4;
                            continue;
                        case 4:
                            if (!flag)
                            {
                                num = 35;
                                continue;
                            }
                            goto case 37;
                        case 5:
                        case 24:
                            wmlVo.sid = sid;
                            INFO = "OK";
                            num = 10;
                            continue;
                        case 30:
                            num = 28;
                            continue;
                        case 20:
                            num = 12;
                            continue;
                        case 12:
                            num = ((userVo.userid < 300) ? 27 : 22);
                            continue;
                        case 9:
                            num = 16;
                            continue;
                        case 16:
                            num2 = ((!(userVo.password.ToLower() != PubConstant.md5(logpass).ToLower())) ? 1 : 0);
                            goto IL_03e2;
                        case 17:
                            {
                                sessid = PubConstant.md5(userVo.userid + userVo.password.Substring(0, 4));
                                num = 36;
                                continue;
                            }
                        IL_0353:
                            flag = (byte)num3 != 0;
                            num = 25;
                            continue;
                        IL_03e2:
                            flag = (byte)num2 != 0;
                            num = 31;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
