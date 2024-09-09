using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class DownLoad : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string id = "";

        public string book_id = "";

        public string RndPath = "";

        public wap2_attachment_Model bookVo = new wap2_attachment_Model();

        public string KL_NotDownAndUpload = PubConstant.GetAppString("KL_DownCheck");

        public string KL_DownCheckReferrer = PubConstant.GetAppString("KL_DownCheckReferrer");

        public string KL_JUMPURL_bbs = PubConstant.GetAppString("KL_JUMPURL_bbs");

        public string RealPath = "";

        public string refer = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = GetRequestValue("id");
            book_id = GetRequestValue("book_id");
            RndPath = GetRequestValue("RndPath");
            RealPath = base.Server.MapPath("/");
            string classid = GetRequestValue("classid");
            // 登录检查
            IsLogin(userid, "bbs/book_view.aspx?siteid=" + siteid + "&classid=" + classid + "&id=" + book_id);
            // 参数检查
            if (string.IsNullOrEmpty(classid) || !System.Text.RegularExpressions.Regex.IsMatch(classid, @"^[1-9]\d*$"))
            {
                ShowTipInfo("无效的参数", "bbs/book_view.aspx?siteid=" + siteid + "&classid=" + classid + "&id=" + book_id);
                return;
            }
            wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(string_10);
            bookVo = wap2_attachment_BLL.GetModel(long.Parse(id));
            string text = WapTool.getArryString(classVo.smallimg, '|', 16);
            string text2 = WapTool.getArryString(classVo.smallimg, '|', 17);
            string text3 = WapTool.getArryString(classVo.smallimg, '|', 18);
            string text4 = WapTool.getArryString(classVo.smallimg, '|', 22);
            if (!WapTool.IsNumeric(text))
            {
                text = "0";
            }
            if (!WapTool.IsNumeric(text2))
            {
                text2 = "0";
            }
            if (!WapTool.IsNumeric(text3))
            {
                text3 = "0";
            }
            if (!WapTool.IsNumeric(text4))
            {
                text4 = "0";
            }
            if (IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername))
            {
                text3 = "0";
            }
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 10)) && !WapTool.isAllowUA(UA) && !WapTool.isAllowIP(IP))
            {
                string error = "此网站设置了只允许手机下载[UA+IP]，如果你是手机访问请将以下信息发给站长：" + UA + "  " + IP;
                ShowTipInfo(error, "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + book_id);
            }
            if (text == "1")
            {
                IsLogin(userid, "bbs/book_view.aspx?siteid=" + siteid + "&classid=" + classid + "&id=" + book_id);
            }
            if (long.Parse(text2) > 0L)
            {
                if (userVo.money < long.Parse(text2))
                {
                    ShowTipInfo("抱歉，下载需要币:" + text2 + "个，你只有:" + userVo.money + "个。请先赚币或充值。", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + book_id);
                }
                string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                if (fcountSubMoneyFlag.IndexOf("BBSDLFL" + id) < 0 && userVo.userid != bookVo.userid)
                {
                    fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSDLFL" + id;
                    MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    MainBll.UpdateSQL("update [user] set money=money-" + text2 + " where userid=" + userVo.userid);
                    SaveBankLog(userid, "论坛下载", "-" + text2.ToString(), userid, nickname, "下载扣币[" + id + "]");
                    if (text4 == "1")
                    {
                        MainBll.UpdateSQL("update [user] set money=money+" + text2 + " where userid=" + bookVo.userid);
                        SaveBankLog(bookVo.userid.ToString(), "论坛赚币", text2.ToString(), userid, nickname, "操作人下载您的文件[" + id + "]");
                    }
                }
            }
            if (long.Parse(text3) > 0L)
            {
                string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
                if (fcountSubMoneyFlag.IndexOf("BBSDLAD" + id) < 0)
                {
                    fcountSubMoneyFlag = fcountSubMoneyFlag + "BBSDLAD" + id;
                    MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
                    MainBll.UpdateSQL("update [user] set money=money+" + text3 + " where userid=" + userVo.userid);
                    SaveBankLog(userid.ToString(), "论坛赚币", text2.ToString(), userid, nickname, "下载文件[" + id + "]送币");
                }
            }
            if (base.Request.UrlReferrer != null)
            {
                refer = base.Request.UrlReferrer.ToString().ToLower();
            }
            MainBll.UpdateSQL("update wap2_attachment set book_click=book_click+1 where id=" + id);
        }
    }
}