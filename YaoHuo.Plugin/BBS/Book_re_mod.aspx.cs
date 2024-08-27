using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_re_mod : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string id = "";

        public string reid = "";

        public string page = "";

        public string lpage = "";

        public string ot = "";

        public string INFO = "";

        public string ERROR = "";

        public wap_bbsre_Model bbsReVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            reid = GetRequestValue("reid");
            page = GetRequestValue("page");
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("ot");
            IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername, "bbs/book_re.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;page=" + page + "&amp;ot=" + ot + "&amp;id=" + id);
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(a);
            bbsReVo = wap_bbsre_BLL.GetModel(long.Parse(reid));
            if (!(action == "gomod"))
            {
                return;
            }
            WapTool.ClearDataBBSRe("bbsRe" + siteid + id);
            try
            {
                string requestValue = GetRequestValue("content");
                if (requestValue.Trim().Length < 2)
                {
                    INFO = "NULL";
                    return;
                }
                bbsReVo.content = requestValue;
                wap_bbsre_BLL.Update(bbsReVo);
                INFO = "OK";
                base.Application.Set("bbsRe" + siteid + id, null);
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
    }
}