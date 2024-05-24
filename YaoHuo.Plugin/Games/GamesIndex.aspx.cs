using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;

namespace YaoHuo.Plugin.Games
{
    public class GamesIndex : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public sys_ad_show_Model adVo = new sys_ad_show_Model();

        public StringBuilder strhtml = new StringBuilder();

        public List<wap2_games_config_Model> configList = new List<wap2_games_config_Model>();

        public string ERROR = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            var sysAdShowBLL = new sys_ad_show_BLL(a);
            adVo = sysAdShowBLL.GetModelBySQL(" and systype='games' and siteid=" + siteid);
            wap2_games_config_BLL wap2_games_config_BLL = new wap2_games_config_BLL(a);
            configList = wap2_games_config_BLL.GetListVo(100L, 1L, " siteid=" + siteid, "*", "id", 100L, 1);
            if (classid == "0")
            {
                classVo.classname = "游戏大厅";
                classVo.introduce = "";
                classVo.sitedowntip = "";
            }
        }

        public string GetTodayState(string gameEn, string showtype)
        {
            int num = 0;
            while (true)
            {
                if (configList != null && num < configList.Count)
                {
                    if (configList[num].gameEn == gameEn)
                    {
                        if (showtype == "1")
                        {
                            return configList[num].todayTimes.ToString();
                        }
                        if (showtype == "2")
                        {
                            break;
                        }
                    }
                    num++;
                    continue;
                }
                return "0";
            }
            return configList[num].todayMoney.ToString();
        }
    }
}
