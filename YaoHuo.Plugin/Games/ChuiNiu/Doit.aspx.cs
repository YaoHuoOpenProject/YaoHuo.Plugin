using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Games.ChuiNiu
{
    public class Doit : MyPageWap
    {
        private string string_0 = PubConstant.GetAppString("InstanceName");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string mymoney = "";

        public string myanswer = "";

        public wap2_games_config_Model configVo = new wap2_games_config_Model();

        public wap2_games_chuiniu_Model bookVo = new wap2_games_chuiniu_Model();

        public long min = 0L;

        public long max = 0L;

        public long per = 0L;

        public string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            id = GetRequestValue("id");
            IsLogin(userid, GetUrlQueryString());
            needPassWordToAdmin();
            wap2_games_config_BLL wap2_games_config_BLL = new wap2_games_config_BLL(string_0);
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
            try
            {
                min = long.Parse(WapTool.getArryString(configVo.config, '|', 0));
                max = long.Parse(WapTool.getArryString(configVo.config, '|', 1));
                per = long.Parse(WapTool.getArryString(configVo.config, '|', 2));
            }
            catch (Exception)
            {
                ShowTipInfo("此游戏还没有配置，请联系站长配置！", "games/gamesindex.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            if (id.IsNull())
            {
                INFO = "NULL";
                return;
            }
            wap2_games_chuiniu_BLL wap2_games_chuiniu_BLL = new wap2_games_chuiniu_BLL(string_0);
            bookVo = wap2_games_chuiniu_BLL.GetModel(long.Parse(id));
            if (bookVo == null)
            {
                ShowTipInfo("不存在此挑战！", "games/chuiniu/index.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            else if (bookVo.state != 0L)
            {
                ShowTipInfo("此挑战已被" + bookVo.winNickname + "(ID:" + bookVo.winUserid + ")抢去！", "games/chuiniu/index.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            else if (bookVo.userid == userVo.userid)
            {
                ShowTipInfo("自己挑战的只能由其它友友应战！", "games/chuiniu/index.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            try
            {
                myanswer = GetRequestValue("myanswer");
                if (myanswer != "1" && myanswer != "2")
                {
                    myanswer = "1";
                }
                if (mymoney == "0")
                {
                    INFO = "NULL";
                    return;
                }
                if (bookVo.userid == userVo.userid)
                {
                    INFO = "ISME";
                    return;
                }
                if (mymoney == "0")
                {
                    INFO = "NUMBER";
                    return;
                }
                if (userVo.money < bookVo.myMoney)
                {
                    INFO = "NOMONEY";
                    return;
                }
                if (myanswer == bookVo.myAnswer.ToString())
                {
                    bookVo.state = 1L;
                }
                else
                {
                    bookVo.state = 2L;
                }
                bookVo.winUserid = userVo.userid;
                bookVo.winNickname = userVo.nickname;
                bookVo.winAnswer = long.Parse(myanswer);
                bookVo.winTime = DateTime.Now;
                wap2_games_chuiniu_BLL.Update(bookVo);
                if (bookVo.state != 3L)
                {
                    if (bookVo.state == 1L)
                    {
                        MainBll.UpdateSQL("update [user] set money=money+" + bookVo.myMoney * per / 100L + " where siteid=" + siteid + " and userid=" + userVo.userid);
                        SaveBankLog(userid.ToString(), "游戏赢币", (bookVo.myMoney * per / 100L).ToString(), siteid, "系统", "吹牛打赢了");
                        if (WapTool.getArryString(siteVo.Version, '|', 46) == "1")
                        {
                            SaveMessage(siteid, "系统消息", "吹牛赢了", "吹牛赢:" + bookVo.myMoney * per / 100L + "个" + siteVo.sitemoneyname, userid.ToString(), "", 1);
                        }
                        GamesClassManager.Tool.SaveGamesRank(siteVo.siteid, userVo.userid, userVo.nickname, "chuiniu", "1", bookVo.myMoney * per / 100L);
                    }
                    else if (bookVo.state == 2L)
                    {
                        MainBll.UpdateSQL("update [user] set money=money-" + bookVo.myMoney + " where siteid=" + siteid + " and userid=" + userVo.userid);
                        SaveBankLog(userid.ToString(), "游戏输币", "-" + bookVo.myMoney, siteid, "系统", "吹牛输了");
                        MainBll.UpdateSQL("update [user] set money=money+ " + (bookVo.myMoney + bookVo.myMoney * per / 100L) + " where siteid=" + siteid + " and userid=" + bookVo.userid);
                        SaveBankLog(bookVo.userid.ToString(), "游戏赢币", (bookVo.myMoney + bookVo.myMoney * per / 100L).ToString(), siteid, "系统", "吹牛打赢了");
                        GamesClassManager.Tool.SaveGamesRank(siteVo.siteid, userVo.userid, userVo.nickname, "chuiniu", "0", bookVo.myMoney);
                    }
                }
                INFO = "OK";
                Action_user_doit(21);
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}