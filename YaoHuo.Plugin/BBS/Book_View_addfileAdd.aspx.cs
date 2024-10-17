using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_addfileAdd : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

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

        private string b = "";

        private string c = "";

        public string sendmoney = "";

        public string[] facelist;

        public string[] facelistImg;

        public string[] stypelist;

        public bool isadmin = false;

        public long getid;

        public int num = 1;

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
            action = GetRequestValue("action");
            lpage = GetRequestValue("lpage");
            id = GetRequestValue("id");
            if (GetRequestValue("num") != "")
            {
                this.num = int.Parse(GetRequestValue("num"));
            }
            if ("1".Equals(WapTool.getArryString(classVo.smallimg, '|', 7)))
            {
                ShowTipInfo("续文件帖功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + siteid + "&amp;classid=" + classVo.childid);
            }
            KL_NotDownAndUpload += WapTool.KL_NotDownAndUpload_SYS;
            IsLogin(userid, "bbs/book_view_addfileadd.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
            //needPassWordToAdmin();
            long num = Convert.ToInt64(WapTool.GetSiteDefault(siteVo.Version, 14));
            if (num > 0L)
            {
                long num2 = WapTool.DateDiff(DateTime.Now, userVo.RegTime, "MM");
                if (num2 < num)
                {
                    ShowTipInfo("请再过:" + (num - num2) + "分钟才能发帖！", "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage);
                }
            }
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
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
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
            }
            else if (bookVo.islock == 2L)
            {
                ShowTipInfo("此帖已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bookVo.book_classid + "&amp;id=" + bookVo.id + "&amp;lpage=" + lpage);
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
                b = GetRequestValue("book_width");
                c = GetRequestValue("book_height");
                string[] values = base.Request.Form.GetValues("book_file_info");
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
                string text2 = "";
                string text3 = "";
                string text4 = "";
                long num3 = 0L;
                long num4 = 0L;
                string text5 = "";
                text2 = base.Server.MapPath("upload/" + siteid + "/" + WapTool.GetDatePathString());
                if (!Directory.Exists(text2))
                {
                    Directory.CreateDirectory(text2);
                }
                HttpFileCollection files = HttpContext.Current.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    wap2_attachment_Model wap2_attachment_Model = new wap2_attachment_Model();
                    HttpPostedFile httpPostedFile = files[i];
                    FileInfo fileInfo = new FileInfo(httpPostedFile.FileName.ToLower());
                    text4 = fileInfo.Extension;
                    num4 = httpPostedFile.InputStream.Length;
                    num3 = num4 / 1024L;
                    long num5 = Convert.ToInt64(siteVo.myspace) + num3;
                    text3 = userid + "_" + DateTime.Now.ToString("HHmmss") + i + fileInfo.Name;
                    text3 = WapTool.filePathFilter(text3);
                    text5 = "upload/" + siteid + "/" + WapTool.GetDatePathString() + text3;
                    if (siteVo.UpFileType.ToLower().IndexOf(text4.Replace(".", "").ToLower()) >= 0)
                    {
                        if (WapTool.isNotChinese(text4.Replace(".", "")))
                        {
                            if (KL_NotDownAndUpload.IndexOf(text4.Replace(".", "").ToLower()) < 0)
                            {
                                if (num5 > Convert.ToInt64(siteVo.sitespace) * 1024L)
                                {
                                    INFO = "NOTSPACE";
                                }
                                else if (num3 > Convert.ToInt64(siteVo.MaxFileSize))
                                {
                                    INFO = "MAXFILE";
                                }
                                MainBll.UpdateSQL("update [user] set myspace=" + $"{num5}" + " where userid=" + siteid);
                                WapTool.SaveUploadFileToLog(siteid, userid, "1", "论坛续传", text4, num3.ToString(), "bbs/" + text5, siteVo.isCheck.ToString());
                                httpPostedFile.SaveAs(text2 + text3);
                                if (".gif|.jpg|.jpeg|.png|.bmp".IndexOf(text4.ToLower()) >= 0)
                                {
                                    if (b != "" && c != "")
                                    {
                                        WapTool.MakeThumbnail(text2 + text3, text2 + text3, int.Parse(b), int.Parse(c), "HW");
                                    }
                                    else if (b != "" && c == "")
                                    {
                                        WapTool.MakeThumbnail(text2 + text3, text2 + text3, int.Parse(b), 0, "W");
                                    }
                                    else if (b == "" && c != "")
                                    {
                                        WapTool.MakeThumbnail(text2 + text3, text2 + text3, 0, int.Parse(c), "H");
                                    }
                                    addtext = WapTool.GetSiteDefault(siteVo.Version, 4);
                                    if (addtext == "1" && ".jpg|.jpeg|.png|.bmp".IndexOf(text4.ToLower()) >= 0)
                                    {
                                        string text6 = WapTool.GetSiteDefault(siteVo.Version, 13);
                                        if (!WapTool.IsNumeric(text6))
                                        {
                                            text6 = "0";
                                        }
                                        long fontsize = Convert.ToInt64(text6);
                                        string text7 = WapTool.GetSiteDefault(siteVo.Version, 12);
                                        string siteDefault = WapTool.GetSiteDefault(siteVo.Version, 16);
                                        if (text7.StartsWith("/"))
                                        {
                                            text7 = base.Server.MapPath(text7);
                                        }
                                        WapTool.AddWater(text2 + text3, text2 + text3, base.Request.ServerVariables["HTTP_HOST"], text7, fontsize, siteDefault);
                                    }
                                }
                                wap2_attachment_Model.book_content = ToHtm(values[i]);
                                wap2_attachment_Model.book_title = ToHtm(fileInfo.Name.Replace(text4, ""));
                                wap2_attachment_Model.book_ext = text4.Replace(".", "");
                                wap2_attachment_Model.book_size = WapTool.ShowSizeInfo(num4);
                                wap2_attachment_Model.book_file = text5;
                                list2.Add(wap2_attachment_Model);
                                continue;
                            }
                            INFO = "EXTERR";
                            break;
                        }
                        INFO = "EXTERR";
                        break;
                    }
                    INFO = "EXTERR";
                    break;
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
                string text8 = "{" + userVo.nickname + "(ID" + userVo.userid + ")文件续帖" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                text8 += bookVo.whylock;
                MainBll.UpdateSQL("update wap_bbs set isdown=1,whylock='" + text8 + "' where id=" + long.Parse(id));
                wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(a);
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