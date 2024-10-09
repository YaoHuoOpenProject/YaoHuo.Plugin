using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.WML
{
	public class admin_WAPmodify : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

        public wap_wml_Model bookVo = new wap_wml_Model();

        public string action = "";

        public string id = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            page = GetRequestValue("page");
            CheckManagerLvl("03", classVo.adminusername, GetUrlQueryString());
            wap_wml_BLL wap_wml_BLL = new wap_wml_BLL(string_10);
            bookVo = wap_wml_BLL.GetModel(long.Parse(id));
            if (bookVo.userid.ToString() != siteid)
            {
                base.Response.End();
            }
            if (bookVo.book_classid.ToString() != classid)
            {
                base.Response.End();
            }
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                bookVo.book_title = ToFilterChar(GetRequestValue("book_title"));
                bookVo.book_pub = GetRequestValue("book_pub");
                string text = GetRequestValue("book_re");
                if (!WapTool.IsNumeric(text))
                {
                    text = "0";
                }
                bookVo.book_re = long.Parse(text);
                bookVo.book_content = base.Request.Form.Get("book_content").Replace("[getwml=", "[******=");
                bookVo.book_content2 = base.Request.Form.Get("book_content2").Replace("[getwml=", "[******=");
                bookVo.makerid = long.Parse(userid);
                if (bookVo.book_title.Trim() == "")
                {
                    INFO = "NULL";
                    return;
                }
                wap_wml_BLL.Update(bookVo);
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}