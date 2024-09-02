using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Search
{
    public class toManager : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string touserid = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string backurl = "";

        public user_Model touserVo = null;

        public string tobankmoney = "";

        public string topassword = "";

        public string toexpR = "";

        public string tosessiontimeout = "";

        public string tomanagerlvl = "";

        public string tolockuser = "";

        public string tochangedate = "";

        public string needpw = "";

        public List<wap2_smallType_Model> idlistVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            backurl = base.Request.QueryString.Get("backurl");
            if (backurl == null || backurl == "")
            {
                backurl = base.Request.Form.Get("backurl");
            }
            if (backurl == null || backurl == "")
            {
                backurl = "myfile.aspx?siteid=" + siteid;
            }
            backurl = ToHtm(backurl);
            backurl = HttpUtility.UrlDecode(backurl);
            backurl = WapTool.URLtoWAP(backurl);
            touserid = GetRequestValue("touserid");
            user_BLL user_BLL = new user_BLL(string_10);
            touserVo = user_BLL.getUserInfo(touserid, siteid);
            IsCheckManagerLvl("|00|01", "", GetUrlQueryString());
            tomanagerlvl = GetRequestValue("tomanagerlvl");
            if (userVo.managerlvl == "01")
            {
                if (userVo.userid != userVo.siteid)
                {
                    if (userVo.userid == touserVo.userid)
                    {
                        if (tomanagerlvl != "" && tomanagerlvl != userVo.managerlvl)
                        {
                            ShowTipInfo("抱歉，你权限选错了！", "");
                        }
                    }
                    else
                    {
                        if (touserVo.managerlvl != "02")
                        {
                            ShowTipInfo("抱歉，你没有权限管理此用户！", "");
                        }
                        if (tomanagerlvl != "" && tomanagerlvl != "02")
                        {
                            ShowTipInfo("抱歉，你权限选错了！", "");
                        }
                    }
                }
                else if (userVo.userid == touserVo.userid)
                {
                    if (tomanagerlvl != "" && "|01|".IndexOf(tomanagerlvl) < 0)
                    {
                        ShowTipInfo("抱歉，你权限选错了！", "");
                    }
                }
                else if (tomanagerlvl != "" && "|01|02|03|04|".IndexOf(tomanagerlvl) < 0)
                {
                    ShowTipInfo("抱歉，你权限选错了！", "");
                }
            }
            if (userVo.managerlvl == "00")
            {
                if (userVo.userid != userVo.siteid)
                {
                    if (userVo.userid == touserVo.userid)
                    {
                        if (tomanagerlvl != "" && tomanagerlvl != userVo.managerlvl)
                        {
                            ShowTipInfo("抱歉，你权限选错了！", "");
                        }
                    }
                    else
                    {
                        if ("|01|02|03|04|".IndexOf(touserVo.managerlvl) < 0)
                        {
                            ShowTipInfo("抱歉，你没有权限管理此用户！", "");
                        }
                        if (tomanagerlvl != "" && "|01|02|03|04|".IndexOf(tomanagerlvl) < 0)
                        {
                            ShowTipInfo("抱歉，你权限选错了！", "");
                        }
                    }
                }
                else if (userVo.userid == touserVo.userid && tomanagerlvl != "" && "|00|".IndexOf(tomanagerlvl) < 0)
                {
                    ShowTipInfo("抱歉，你权限选错了！", "");
                }
            }
            if (action == "gomod")
            {
                tobankmoney = GetRequestValue("tobankmoney");
                topassword = GetRequestValue("topassword");
                toexpR = GetRequestValue("toexpR");
                tosessiontimeout = GetRequestValue("tosessiontimeout");
                tolockuser = GetRequestValue("tolockuser");
                tomanagerlvl = GetRequestValue("tomanagerlvl");
                tochangedate = GetRequestValue("tochangedate");
                needpw = GetRequestValue("needpw");
                try
                {
                    if (PubConstant.md5(needpw).ToLower() != userVo.password.ToLower())
                    {
                        INFO = "PWERROR";
                    }
                    else if (!WapTool.IsNumeric(tobankmoney) || !WapTool.IsNumeric(toexpR))
                    {
                        INFO = "NUM";
                    }
                    else
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("update [user] set managerlvl='" + tomanagerlvl + "' , expR=" + toexpR + ",lockuser=" + tolockuser + ",myBankMoney=" + tobankmoney + ",sessiontimeout=" + tosessiontimeout);
                        if (topassword != "")
                        {
                            stringBuilder.Append(",password='" + PubConstant.md5(topassword) + "'");
                        }
                        if (siteid.ToString() != touserid.ToString() && tochangedate.Trim() != "")
                        {
                            try
                            {
                                DateTime endTime = DateTime.Parse(tochangedate.Trim());
                                stringBuilder.Append(",endtime='" + tochangedate + "'");
                                touserVo.endTime = endTime;
                            }
                            catch (Exception)
                            {
                            }
                        }
                        stringBuilder.Append(" where siteid=" + siteid + " and  userid=" + touserid);
                        MainBll.UpdateSQL(stringBuilder.ToString());
                        INFO = "OK";
                    }
                }
                catch (Exception ex)
                {
                    ERROR = ex.ToString();
                }
            }
            tobankmoney = touserVo.myBankMoney.ToString();
            toexpR = touserVo.expr.ToString();
            tosessiontimeout = touserVo.SessionTimeout.ToString();
            tolockuser = touserVo.LockUser.ToString();
            string strWhere = "siteid=" + siteid + " and systype='card'";
            wap2_smallType_BLL wap2_smallType_BLL = new wap2_smallType_BLL(string_10);
            idlistVo = wap2_smallType_BLL.GetListVo(100L, 1L, strWhere, "*", "id", 100L, 1);
        }
    }
}