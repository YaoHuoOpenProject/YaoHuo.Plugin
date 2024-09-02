using System;
using System.Data;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Share : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        public string id = "";

        public string lpage = "";

        public string vpage = "";

        public string action = "";

        public wap_bbs_Model bookVo = null;

        public string INFO = "";

        public string ERROR = "";

        public string downloadpath = "";

        public string filename = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            vpage = GetRequestValue("vpage");
            http_start = "//" + base.Request.ServerVariables["HTTP_HOST"] + "/";
            if (WapTool.getArryString(classVo.smallimg, '|', 13) != "0" && action != "fav" && action != "")
            {
                ShowTipInfo("վ���ѹرմ˹��ܣ�", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
            }
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(a);
            bookVo = wap_bbs_BLL.GetModel(long.Parse(id));
            bookVo.book_title = bookVo.book_title.Replace("/", "_");
            bookVo.book_title = bookVo.book_title.Replace("\\", "_");
            if (bookVo == null)
            {
                ShowTipInfo("��ɾ�����򲻴��ڣ�", "");
            }
            else if (bookVo.ischeck == 1L)
            {
                ShowTipInfo("��������У�", "");
            }
            else if (bookVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("��ĿID�Բ��ϣ�����û�д�classidֵ��", "");
            }
            if (bookVo.viewtype > 0L && action != "" && action != "fav")
            {
                ShowTipInfo("���������������أ�", "");
            }
            if (action != "" && action != "fav" && (bookVo.book_content.IndexOf("[/login]") > 0 || bookVo.book_content.IndexOf("[/mobi]") > 0 || bookVo.book_content.IndexOf("[/reply]") > 0 || bookVo.book_content.IndexOf("[/coin]") > 0 || bookVo.book_content.IndexOf("[/grade]") > 0 || bookVo.book_content.IndexOf("[/buy]") > 0))
            {
                ShowTipInfo("���������������أ�", "");
            }
            bookVo.book_title = WapTool.GetShowImg(bookVo.book_title, "100", "bbs");
            bookVo.book_content = bookVo.book_title + "\n\n" + bookVo.book_content + "\n\n (" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&classid=" + classid + "&id=" + id + ")";
            bookVo.book_content = WapTool.ToWML(bookVo.book_content, wmlVo);
            bookVo.book_content = bookVo.book_content.Replace("<br/>", "\n");
            bookVo.book_content = bookVo.book_content.Replace("<", "��");
            bookVo.book_content = bookVo.book_content.Replace(">", "��");
            if (!(action != ""))
            {
                return;
            }
            if (isCheckIPTime(long.Parse(KL_CheckIPTime)))
            {
                INFO = "WAITING";
                return;
            }
            switch (action)
            {
                case "fav":
                    goFAV();
                    break;
            }
            VisiteCount("����̳�������˵����飬<a href=\"" + http_start + "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "\">ȥ����</a>��");
        }
        public void goFAV()
        {
            string text = "bbs-" + id + ".html";
            // ����Ƿ��Ѿ��ղ�
            string checkSql = $"SELECT COUNT(*) FROM favdetail WHERE userid={userid} AND url='{text}'";
            int count = (int)DbHelperSQL.ExecuteScalar(PubConstant.GetConnectionString(a), CommandType.Text, checkSql);
            if (count > 0)
            {
                // �Ѿ��ղع�
                INFO = "REPEAT";
                return;
            }
            // �����ղؼ�¼
            MainBll.UpdateSQL($"insert into favdetail (siteid,userid,favtypeid,title,url)values({siteid},{userid},0,'{bookVo.book_title}','{text}')");
            base.Response.Redirect("/bbs/favlist.aspx");
            //base.Response.Redirect(http_start + "bbs/myfav.aspx?siteid=" + siteid + "&classid=" + classid + "&backurl=" + HttpUtility.UrlEncode(text));
        }
    }
}