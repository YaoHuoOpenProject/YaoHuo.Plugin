using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.Games.ChuiNiu
{
    public class Add : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        public List<class_Model> classList = new List<class_Model>();

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string mymoney = "";

        public string question = "";

        public string answer1 = "";

        public string answer2 = "";

        public string myanswer = "";

        public wap2_games_config_Model configVo = new wap2_games_config_Model();

        public long min = 0L;

        public long max = 0L;

        public long per = 0L;

        public long cou = 0L;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = GetRequestValue("action");
            page = GetRequestValue("page");
            IsLogin(userid, GetUrlQueryString());
            needPassWordToAdmin();
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
            try
            {
                min = long.Parse(WapTool.getArryString(configVo.config, '|', 0));
                max = long.Parse(WapTool.getArryString(configVo.config, '|', 1));
                per = long.Parse(WapTool.getArryString(configVo.config, '|', 2));
                cou = long.Parse(WapTool.getArryString(configVo.config, '|', 3));
            }
            catch (Exception)
            {
                ShowTipInfo("此游戏还没有配置，请联系站长配置！", "games/gamesindex.aspx?siteid=" + siteid + "&amp;classid=" + classid);
            }
            if (!(action == "gomod"))
            {
                return;
            }
            // 添加请求方式限制
            if (HttpContext.Current.Request.HttpMethod != "POST")
            {
                INFO = "ERR";
                return;
            }
            try
            {
                long num = 0L;
                wap2_games_chuiniu_Model wap2_games_chuiniu_Model = new wap2_games_chuiniu_Model();
                wap2_games_chuiniu_BLL wap2_games_chuiniu_BLL = new wap2_games_chuiniu_BLL(a);
                mymoney = GetRequestValue("mymoney");
                if (!WapTool.IsNumeric(mymoney))
                {
                    mymoney = "0";
                }
                question = GetRequestValue("question");
                answer1 = GetRequestValue("answer1");
                answer2 = GetRequestValue("answer2");
                myanswer = GetRequestValue("myanswer");

                // 替换 question 和 answer 字段中的 [] 符号为空格
                question = question.Replace("[", " ").Replace("]", " ");
                answer1 = answer1.Replace("[", " ").Replace("]", " ");
                answer2 = answer2.Replace("[", " ").Replace("]", " ");

                if (question.Length > 20)
                {
                    question = question.Substring(0, 20);
                }
                if (answer1.Length > 20)
                {
                    answer1 = answer1.Substring(0, 20);
                }
                if (answer2.Length > 20)
                {
                    answer2 = answer2.Substring(0, 20);
                }
                if (myanswer != "1" && myanswer != "2")
                {
                    myanswer = "1";
                }
                num = wap2_games_chuiniu_BLL.GetListCount("siteid=" + siteid + " and userid=" + userid + " and  DATEDIFF(dd, addtime, GETDATE()) <1");
                if (mymoney == "0" || question.Trim() == "" || answer1.Trim() == "" || answer2.Trim() == "")
                {
                    INFO = "NULL";
                    return;
                }
                if (num > cou)
                {
                    INFO = "WAITING";
                    return;
                }
                if (mymoney == "0")
                {
                    INFO = "NUMBER";
                    return;
                }
                if (long.Parse(mymoney) < min)
                {
                    INFO = "NUMBER";
                    return;
                }
                if (long.Parse(mymoney) > max)
                {
                    INFO = "NUMBER";
                    return;
                }
                if (userVo.money < long.Parse(mymoney))
                {
                    INFO = "NOMONEY";
                    return;
                }
                wap2_games_chuiniu_Model.siteid = siteVo.siteid;
                wap2_games_chuiniu_Model.userid = userVo.userid;
                wap2_games_chuiniu_Model.nickName = userVo.nickname;
                wap2_games_chuiniu_Model.myMoney = long.Parse(mymoney);
                wap2_games_chuiniu_Model.Question = question;
                wap2_games_chuiniu_Model.Answer1 = answer1;
                wap2_games_chuiniu_Model.Answer2 = answer2;
                wap2_games_chuiniu_Model.myAnswer = long.Parse(myanswer);
                wap2_games_chuiniu_Model.state = 0L;
                wap2_games_chuiniu_Model.winUserid = 0L;
                wap2_games_chuiniu_Model.winNickname = "";
                wap2_games_chuiniu_Model.winAnswer = 0L;
                wap2_games_chuiniu_Model.winTime = DateTime.Now;
                wap2_games_chuiniu_Model.addtime = DateTime.Now;
                wap2_games_chuiniu_BLL.Add(wap2_games_chuiniu_Model);
                if (configVo.updateTime.Day == DateTime.Now.Day)
                {
                    configVo.todayTimes += 1L;
                    configVo.todayMoney += long.Parse(mymoney);
                }
                else
                {
                    configVo.todayTimes = 1L;
                    configVo.todayMoney = long.Parse(mymoney);
                    configVo.updateTime = DateTime.Now;
                }
                wap2_games_config_BLL.Update(configVo);
                MainBll.UpdateSQL("update [user] set money=money-" + mymoney + " where siteid=" + siteid + " and userid=" + userid);
                SaveBankLog(userid, "游戏下注", "-" + mymoney.ToString(), userid, nickname, "吹牛下注");
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