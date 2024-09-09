using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Album
{
    public class AlbumList_del : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string string_11 = "";

        public string smalltypeid = "";

        public string id = "";

        public string backurl = "";

        public string INFO = "";

        public string page = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            page = GetRequestValue("page");
            smalltypeid = GetRequestValue("smalltypeid");
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
            if (!WapTool.IsNumeric(id))
            {
                id = "0";
            }
            IsLogin(userid, backurl);
            needPassWordToAdmin();
            switch (action)
            {
                case "godelall":
                    godelall();
                    break;
                case "godel":
                    godel();
                    break;
            }
        }

        public void godel()
        {
            wap_album_BLL wap_album_BLL = new wap_album_BLL(string_10);
            wap_album_Model model = wap_album_BLL.GetModel(long.Parse(id));
            if (model.userid.ToString() != siteid)
            {
                base.Response.End();
            }
            if (model.makerid.ToString() != userid)
            {
                base.Response.End();
            }
            DeleteFile("album", model.book_file + "|" + model.book_img, GetUrlQueryString().Replace("godel", "go"));
            MainBll.UpdateSQL("delete  from wap_album where userid=" + long.Parse(siteid) + " and makerid=" + long.Parse(userid) + " and id=" + long.Parse(id));
            INFO = "OK";
        }

        public void godelall()
        {
        }
    }
}