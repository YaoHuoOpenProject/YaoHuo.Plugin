using System;
using System.Collections.Generic;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class Book_View_addfileAddURL : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string KL_CheckBBSCount = PubConstant.GetAppString("KL_CheckBBSCount");

        public string KL_NotDownAndUpload = PubConstant.GetAppString("KL_NotDownAndUpload");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public string action = "";

        public string lpage = "";

        public string INFO = "";

        public string ERROR = "";

        public string book_title = "";

        public string book_content = "";

        public string face = "";

        public string stype = "";

        public string viewtype = "";

        public string viewmoney = "";

        public string reshow = "";

        private string string_11 = "";

        private string string_12 = "";

        public string sendmoney = "";

        public string[] facelist;

        public string[] facelistImg;

        public string[] stypelist;

        public bool isadmin = false;

        public long getid;

        public int num = 2;

        public string id = "";

        public string addtext = "";

        public wap_bbs_Model bookVo = null;

        public string needmoney = "0";

        public string needexpr = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = base.Request.Form.Get("action");
            lpage = GetRequestValue("lpage");
            id = GetRequestValue("id");
            if (GetRequestValue("num") != "")
            {
                num = int.Parse(GetRequestValue("num"));
            }
            if (!IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && "1".Equals(WapTool.getArryString(classVo.smallimg, '|', 44)))
            {
                ShowTipInfo("发外站资源贴功能已关闭！【版务】→【更多栏目属性】中【44】项设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            KL_NotDownAndUpload += WapTool.KL_NotDownAndUpload_SYS;
            IsLogin(userid, "bbs/book_view_addfileadd.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            needPassWordToAdmin();
            //重命名局部变量
            long requiredMonths = Convert.ToInt64(WapTool.GetSiteDefault(siteVo.Version, 14));
            if (requiredMonths > 0L)
            {
                long userMonths = WapTool.DateDiff(DateTime.Now, userVo.RegTime, "MM");
                if (userMonths < requiredMonths)
                {
                    ShowTipInfo("请再过:" + (requiredMonths - userMonths) + "分钟才能发贴！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage);
                }
            }
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
            bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
            if (bookVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bookVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bookVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bookVo.islock == 1L)
            {
                ShowTipInfo("此贴已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            else if (bookVo.islock == 2L)
            {
                ShowTipInfo("此贴已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            if (userid != bookVo.book_pub.ToString())
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            needmoney = WapTool.GetSiteDefault(siteVo.moneyregular, 5);
            needexpr = WapTool.GetSiteDefault(siteVo.lvlRegular, 5);
            if (!WapTool.IsNumeric(needmoney))
            {
                needmoney = "0";
            }
            if (!WapTool.IsNumeric(needexpr))
            {
                needexpr = "0";
            }
            if (userVo.money < long.Parse(needmoney))
            {
                ShowTipInfo("上传文件需要" + WapTool.GetSiteMoneyName(siteVo.sitemoneyname, lang) + "大于:" + needmoney, "bbs/Book_View_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
            }
            if (userVo.expr < long.Parse(needexpr))
            {
                ShowTipInfo("上传文件需要经验大于:" + needexpr, "bbs/Book_View_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                book_title = GetRequestValue("book_title");
                book_content = GetRequestValue("book_content");
                face = GetRequestValue("face");
                stype = GetRequestValue("stype");
                viewtype = GetRequestValue("viewtype");
                viewmoney = GetRequestValue("viewmoney");
                reshow = GetRequestValue("reshow");
                sendmoney = GetRequestValue("sendmoney");
                string_11 = GetRequestValue("book_width");
                string_12 = GetRequestValue("book_height");
                base.Request.Form.GetValues("book_file_info");
                List<wap2_attachment_Model> list2 = new List<wap2_attachment_Model>();
                if (book_title.Length > 200)
                {
                    book_title = book_title.Substring(0, 200);
                }
                string text = WapTool.getArryString(siteVo.Version, '|', 22);
                if (!WapTool.IsNumeric(reshow))
                {
                    reshow = "0";
                }
                if (!WapTool.IsNumeric(sendmoney))
                {
                    sendmoney = "0";
                }
                if (!WapTool.IsNumeric(viewmoney))
                {
                    viewmoney = "0";
                }
                if (!WapTool.IsNumeric(viewtype))
                {
                    viewtype = "0";
                }
                if (!WapTool.IsNumeric(text))
                {
                    text = "0";
                }
                if (long.Parse(text) < 2L)
                {
                    text = "1000";
                }
                if (long.Parse(sendmoney) > long.Parse(text))
                {
                    sendmoney = text;
                }
                if (long.Parse(reshow) > long.Parse(text))
                {
                    reshow = text;
                }
                if (viewtype == "6" && long.Parse(viewmoney) > long.Parse(text))
                {
                    viewmoney = text;
                }
                string[] values = base.Request.Form.GetValues("file_ext");
                string[] values2 = base.Request.Form.GetValues("file_url");
                string[] values3 = base.Request.Form.GetValues("file_size");
                string[] values4 = base.Request.Form.GetValues("file_title");
                string[] values5 = base.Request.Form.GetValues("file_info");
                for (int i = 0; i < values2.Length; i++)
                {
                    if (values4[i].Trim() != "" && values2[i].Trim() != "")
                    {
                        wap2_attachment_Model wap2_attachment_Model = new wap2_attachment_Model();
                        wap2_attachment_Model.book_content = ToHtm(values5[i]);
                        wap2_attachment_Model.book_title = ToHtm(values4[i]);
                        wap2_attachment_Model.book_ext = ToHtm(values[i]);
                        wap2_attachment_Model.book_size = ToHtm(values3[i]);
                        wap2_attachment_Model.book_file = ToHtm(values2[i]);
                        list2.Add(wap2_attachment_Model);
                    }
                }
                if (INFO != "")
                {
                    return;
                }
                if (WapTool.isLockuser(siteid, userid, classid) > -1L)
                {
                    INFO = "LOCK";
                    return;
                }
                string text2 = "{" + userVo.nickname + "(ID" + userVo.userid + ")文件续贴" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                text2 += bookVo.whylock;
                MainBll.UpdateSQL("update wap_bbs set isdown=1,whylock='" + text2 + "' where id=" + long.Parse(id));
                wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(string_10);
                for (int j = 0; j < list2.Count; j++)
                {
                    list2[j].siteid = long.Parse(siteid);
                    list2[j].userid = long.Parse(userid);
                    list2[j].book_id = long.Parse(id);
                    list2[j].book_type = "bbs";
                    wap2_attachment_BLL.Add(list2[j]);
                }
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}