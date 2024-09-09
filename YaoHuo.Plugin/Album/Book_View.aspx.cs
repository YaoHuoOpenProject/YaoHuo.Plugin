using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using Microsoft.VisualBasic;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Album
{
    public class Book_View : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string id = "0";

        public sys_ad_show_Model adVo = new sys_ad_show_Model();

        public wap_album_Model bookVo = new wap_album_Model();

        public List<wap_albumre_Model> relistVo = null;

        public List<wap_album_Model> filelist = null;

        public StringBuilder strhtml = new StringBuilder();

        public string lpage = "";

        public string content = "";

        public string view = "";

        public string viewLeave = "";

        public StringBuilder preNextTitle = new StringBuilder();

        public string ERROR = "";

        public string INFO = "";

        public int k = 0;

        public string fileurl = "";

        public string linkURL = "";

        public string http_start_url = "";

        public int totalPage = 0;

        public int pageSize = 1000;

        public int CurrentPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            IsLogin(userid, "");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            view = GetRequestValue("view");
            viewLeave = GetRequestValue("viewleave");
            pageSize = Convert.ToInt32(siteVo.MaxPerPage_Content);
            if (GetRequestValue("vpage") != "")
            {
                CurrentPage = int.Parse(GetRequestValue("vpage"));
                if (CurrentPage < 1)
                {
                    CurrentPage = 1;
                }
            }
            CheckUserViewSubMoney("ALB" + id, GetUrlQueryString(), "album/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            try
            {
                wap_album_BLL wap_album_BLL = new wap_album_BLL(string_10);
                if (CurrentPage == 1)
                {
                    wap_album_BLL.UpdateXiNuHan(siteid, id, "0");
                }
                string backurl = "album/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage;
                bookVo = wap_album_BLL.GetModel(long.Parse(id));
                if (bookVo == null)
                {
                    ShowTipInfo("已删除！或不存在！", backurl);
                }
                else if (bookVo.ischeck == 1L)
                {
                    ShowTipInfo("正在审核中！", backurl);
                }
                else if (bookVo.ishidden == 1L && userid != bookVo.makerid.ToString())
                {
                    ShowTipInfo("隐私相片！", backurl);
                }
                wmlVo.title = bookVo.book_title;
                wmlVo.id = bookVo.id;

                if (userid == bookVo.makerid.ToString() && GetRequestValue("action") == "addhead")
                {
                    // 获取头像路径
                    string originalImg = bookVo.book_img;

                    // 检查图片路径是否包含缩略图前缀 'S'
                    if (!string.IsNullOrEmpty(originalImg) && originalImg.Contains("/S"))
                    {
                        // 去掉文件名中的 'S'，得到原图路径
                        originalImg = originalImg.Replace("/S", "/");
                    }

                    // 确保路径前有 "album/"
                    if (!originalImg.StartsWith("album/"))
                    {
                        originalImg = "album/" + originalImg;
                    }

                    // 构建 SQL 更新语句
                    string sql = "update [user] set headimg='" + originalImg + "' where userid=" + userid;

                    // 执行 SQL 更新操作
                    long affectedRows = MainBll.UpdateSQL(sql);

                    // 判断更新是否成功
                    if (affectedRows > 0)
                    {
                        INFO = "OK";  // 更新成功
                    }
                    else
                    {
                        ERROR = "Failed to update the headimg.";  // 更新失败
                    }
                }

                http_start_url = http_start + "album/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage;
                sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(string_10);
                adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='album' and siteid=" + siteid);
                string[] array = bookVo.book_file.Split('|');
                filelist = new List<wap_album_Model>();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Trim() != "")
                    {
                        string[] array2 = array[i].Split('.');
                        wap_album_Model wap_album_Model = new wap_album_Model();
                        wap_album_Model.book_file = array[i];
                        wap_album_Model.book_ext = array2[array2.Length - 1];
                        wap_album_Model.book_size = bookVo.book_size;
                        filelist.Add(wap_album_Model);
                    }
                }
                content = bookVo.book_content;
                if (view != "all")
                {
                    if (content.IndexOf("[next]") > 0)
                    {
                        content = content.Replace("[next]", "\uff3e");
                        string[] array3 = content.Split('\uff3e');
                        totalPage = array3.Length;
                        if (array3[totalPage - 1] == "")
                        {
                            totalPage--;
                        }
                        if (viewLeave != "")
                        {
                            if (int.Parse(viewLeave) < totalPage)
                            {
                                string text = "";
                                for (int i = int.Parse(viewLeave); i < totalPage; i++)
                                {
                                    text += array3[i];
                                }
                                content = text;
                            }
                        }
                        else
                        {
                            content = array3[CurrentPage - 1];
                        }
                    }
                    else
                    {
                        totalPage = content.Length / pageSize;
                        if (content.Length > totalPage * pageSize)
                        {
                            totalPage++;
                        }
                        if (viewLeave != "")
                        {
                            if (Convert.ToInt32(viewLeave) * pageSize < content.Length)
                            {
                                content = content.Substring(Convert.ToInt32(viewLeave) * pageSize, content.Length - Convert.ToInt32(viewLeave) * pageSize);
                            }
                        }
                        else if (totalPage > 1 && CurrentPage >= totalPage)
                        {
                            CurrentPage = totalPage;
                            content = content.Substring((CurrentPage - 1) * pageSize, content.Length - (CurrentPage - 1) * pageSize);
                        }
                        else if (totalPage > 1 && CurrentPage < totalPage)
                        {
                            content = content.Substring((CurrentPage - 1) * pageSize, pageSize);
                        }
                    }
                    if (content != "")
                    {
                        content = "<!--listS-->" + content + "<!--listE-->";
                    }
                    content += "<span id=\"KL_show_next_list\"></span>";
                    linkURL = WapTool.GetPageContentLink(ver, lang, totalPage, pageSize, CurrentPage, http_start_url + "&amp;id=" + id);
                }
                content = content.Replace("[next]", "");
                preNextTitle = wap_album_BLL.GetPreNextTitle(http_start_url, siteid, classid, id, "desc");
                wap_albumre_BLL wap_albumre_BLL = new wap_albumre_BLL(string_10);
                relistVo = wap_albumre_BLL.GetListVo(5, 1, " devid='" + siteid + "' and ischeck=0 and bookid=" + id, "*", "id", 10L, 1);
                VisiteCount("正在浏览:<a href=\"" + http_start + "album/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "\">" + bookVo.book_title + "</a>");
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}