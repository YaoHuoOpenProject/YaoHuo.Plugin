using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class book_view_modfile : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string KL_NotDownAndUpload = PubConstant.GetAppString("KL_NotDownAndUpload");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public List<wap2_attachment_Model> imgList = new List<wap2_attachment_Model>();

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string book_title = "";

        public string book_content = "";

        public string book_imgTrue = "";

        public string updateInfo = "";

        public string softMoney = "";

        public string softSafe = "";

        public string money = "";

        public string lpage = "";

        public string id = "";

        public long getid;

        public int num = 1;

        public StringBuilder book_img = new StringBuilder();

        public StringBuilder band = new StringBuilder();

        public StringBuilder platform = new StringBuilder();

        public StringBuilder screen = new StringBuilder();

        public StringBuilder serial = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID非论坛模块。", "");
            }
            action = GetRequestValue("action");
            page = GetRequestValue("page");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
            bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
            if (bbsVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bbsVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bbsVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bbsVo.islock == 1L)
            {
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
            }
            else if (bbsVo.islock == 2L)
            {
                ShowTipInfo("此帖已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
            }
            bbsVo.book_title = WapTool.GetShowImg(bbsVo.book_title, "200", "bbs");
            if (bbsVo.userid.ToString() != siteid)
            {
                base.Response.End();
            }
            if (bbsVo.book_classid.ToString() != classid)
            {
                base.Response.End();
            }
            if (userid != bbsVo.book_pub.ToString())
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            else
            {
                IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
                //needPassWordToAdmin();
            }
            wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(string_10);
            imgList = wap2_attachment_BLL.GetListVo(" book_type='bbs' and  book_id=" + id);
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                string[] values = base.Request.Form.GetValues("book_id");
                string[] values2 = base.Request.Form.GetValues("book_file_title");
                string[] values3 = base.Request.Form.GetValues("book_ext");
                string[] values4 = base.Request.Form.GetValues("book_size");
                string[] values5 = base.Request.Form.GetValues("book_file_info");
                string[] values6 = base.Request.Form.GetValues("book_click");
                int num = 0;
                while (imgList != null && num < imgList.Count)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (imgList[num].ID.ToString() == values[i])
                        {
                            imgList[num].book_content = ToHtm(values5[i]);
                            imgList[num].book_title = ToHtm(values2[i]);
                            imgList[num].book_ext = ToHtm(values3[i]);
                            imgList[num].book_size = values4[i];
                            try
                            {
                                imgList[num].book_click = long.Parse(values6[i]);
                            }
                            catch (Exception)
                            {
                            }
                            if (imgList[num].book_title.Trim() == "")
                            {
                                INFO = "NULL";
                            }
                            imgList[num].book_title = imgList[num].book_title.Replace("[", "［").Replace("］", "]");
                            checkGo(bbsVo.viewtype.ToString(), imgList[num].book_content);
                        }
                    }
                    num++;
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
                string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")修改附件" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                bbsVo.whylock = text + bbsVo.whylock;
                bbsVo.isdown = 1L;
                wap_bbs_BLL.Update(bbsVo);
                num = 0;
                while (imgList != null && num < imgList.Count)
                {
                    wap2_attachment_BLL.Update(imgList[num]);
                    num++;
                }
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }

        public void checkGo(string viewtype, string content)
        {
            string arryString = WapTool.getArryString(classVo.smallimg, '|', 21);
            if (arryString != "")
            {
                arryString = "_" + arryString + "_";
                bool flag = false;
                if (int.Parse(viewtype) > 2 || content.IndexOf("[/reply]") > 0 || content.IndexOf("[/buy]") > 0 || content.IndexOf("[/coin]") > 0 || content.IndexOf("[/grade]") > 0)
                {
                    flag = true;
                }
                if (flag && !IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) && arryString.IndexOf("_" + userVo.SessionTimeout + "_") < 0)
                {
                    ShowTipInfo("您当前的身份不允许发特殊帖。", "bbs/book_view_modfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
                }
            }
        }
    }
}