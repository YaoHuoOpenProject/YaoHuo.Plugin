using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.Games.ChuiNiu
{
    public class Index : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public StringBuilder strhtml = new StringBuilder();

        public string ERROR = "";

        public long showRe = 5L;

        public List<wap2_games_chuiniu_Model> listVo = new List<wap2_games_chuiniu_Model>();

        public wap2_games_config_Model configVo = new wap2_games_config_Model();

        public sys_ad_show_Model adVo = new sys_ad_show_Model();

        public List<wap2_games_chat_Model> relistVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GetRequestValue("action") == "del")
            {
                IsCheckManagerLvl("|00|01|", classVo.adminusername, GetUrlQueryString());
                string requestValue = GetRequestValue("id");
                MainBll.UpdateSQL("delete from wap2_games_chuiniu where siteid=" + siteid + " and id=" + long.Parse(requestValue));
            }
            sys_ad_show_BLL sys_ad_show_BLL = new sys_ad_show_BLL(a);
            adVo = sys_ad_show_BLL.GetModelBySQL(" and systype='games' and siteid=" + siteid);
            wap2_games_config_BLL wap2_games_config_BLL = new wap2_games_config_BLL(a);
            configVo = wap2_games_config_BLL.GetModel("gameen='chuiniu' and siteid=" + siteid);
            if (configVo == null)
            {
                configVo = new wap2_games_config_Model();
                configVo.siteid = siteVo.siteid;
                configVo.gameEn = "chuiniu";
                configVo.gameCn = "吹牛";
                configVo.config = "100|20000|95|10|5";
                configVo.todayTimes = 0L;
                configVo.todayMoney = 0L;
                configVo.updateTime = DateTime.Now;
                configVo.addtime = DateTime.Now;
                wap2_games_config_BLL.Add(configVo);
            }
            wap2_games_chuiniu_BLL wap2_games_chuiniu_BLL = new wap2_games_chuiniu_BLL(a);
            listVo = wap2_games_chuiniu_BLL.GetListVo(100L, 1L, " state=0 and siteid=" + siteid + " ", "*", "id", 100L, 1);
            if (WapTool.getArryString(configVo.config, '|', 4) != "")
            {
                showRe = long.Parse(WapTool.getArryString(configVo.config, '|', 4));
            }
            if (showRe > 0L)
            {
                wap2_games_chat_BLL wap2_games_chat_BLL = new wap2_games_chat_BLL(a);
                relistVo = wap2_games_chat_BLL.GetListVo(showRe, 1L, " siteid=" + siteid + " ", "*", "id", 100L, 1);
            }
            string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
            if (fcountSubMoneyFlag.IndexOf("chuiniu") < 0)
            {
                VisiteCount("在玩<a href=\"" + http_start + "games/chuiniu/index.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;sid=[sid]\">吹牛</a>");
                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "chuiniu,' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
            }
        }
    }
}
