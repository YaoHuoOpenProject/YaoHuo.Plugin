using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Games.ChuiNiu
{
    public class Book_View : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public wap2_games_chuiniu_Model bookVo = new wap2_games_chuiniu_Model();

        public string id = "";

        public string type = "";

        public string touserid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            type = GetRequestValue("type");
            touserid = GetRequestValue("touserid");
            wap2_games_chuiniu_BLL wap2_games_chuiniu_BLL = new wap2_games_chuiniu_BLL(a);
            bookVo = wap2_games_chuiniu_BLL.GetModel(long.Parse(id));
            if (bookVo == null)
            {
                ShowTipInfo("不存在此挑战！", "games/chuiniu/index.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
        }
    }
}
