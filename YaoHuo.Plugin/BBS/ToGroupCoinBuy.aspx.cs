using System;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class toGroupCoinBuy : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");
        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");
        public string action = "";
        public string backurl = "";
        public string INFO = "";
        public string ERROR = "";
        public string toid = "";
        public long toNeedMoney = 0L;
        public long STATE = 0L;
        public long changeMoney = 0L;
        public string changePW = "";
        public string num = "";
        public wap2_smallType_Model idVo = null;

        private const int RMB_TO_COIN_RATIO = 12500; // 设置RMB和妖晶比例

        // 新增公共属性，用于传递数据到前端
        public int CurrentRank { get; private set; } = 0;
        public int RemainingDays { get; private set; } = 0;
        public int TargetRank { get; private set; } = 0;
        public long CurrentId { get; private set; } = 0;
        public long ToidNum { get; private set; } = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            IsLogin(userid, backurl);
            action = GetRequestValue("action");
            changePW = GetRequestValue("changePW");
            toid = GetRequestValue("toid");
            num = GetRequestValue("num");
            if (!WapTool.IsNumeric(num))
            {
                num = "1";
            }
            if (int.Parse(num) > 100)
            {
                num = "100";
            }
            if (toid == "105" && int.Parse(num) < 12)
            {
                num = "12";
            }
            wap2_smallType_BLL wap2_smallType_BLL = new wap2_smallType_BLL(string_10);
            idVo = wap2_smallType_BLL.GetModel(long.Parse(toid));
            if (idVo == null)
            {
                ShowTipInfo("不存在此身份级别！", "");
            }
            toNeedMoney = idVo.rank * RMB_TO_COIN_RATIO * long.Parse(num);
            STATE = toNeedMoney;
            if (STATE < 1L)
            {
                INFO = "CLOSE";
            }
            if (userVo.userid.ToString() == userVo.siteid.ToString())
            {
                INFO = "MASTERNO";
            }

            // 获取当前用户的身份信息
            wap2_smallType_BLL currentTypeBLL = new wap2_smallType_BLL(string_10);
            var currentIdVo = currentTypeBLL.GetModel(userVo.SessionTimeout);
            if (currentIdVo != null)
            {
                CurrentRank = (int)currentIdVo.rank;
                CurrentId = currentIdVo.id;
                TimeSpan remainingTime = userVo.endTime - DateTime.Now;
                RemainingDays = remainingTime.Days > 0 ? remainingTime.Days : 0;
            }

            // 获取目标身份的等级和ID
            TargetRank = (int)idVo.rank;

            // 使用临时变量来解析 toid
            long tempToidNum = 0;
            long.TryParse(toid, out tempToidNum);
            ToidNum = tempToidNum;

            string text = action;
            if (text != null && text == "add")
            {
                addMoney();
            }
        }

        public void addMoney()
        {
            if (!(INFO != ""))
            {
                if (STATE < 1L)
                {
                    INFO = "CLOSE";
                }
                else if (PubConstant.md5(changePW).ToLower() != userVo.password.ToLower())
                {
                    INFO = "PWERR";
                }
                else if (isCheckIPTime(long.Parse(KL_CheckIPTime)))
                {
                    INFO = "WAITING";
                }
                else if (userVo.money < 1m || userVo.money < (decimal)toNeedMoney)
                {
                    INFO = "NOTMONEY";
                }
                else
                {
                    string text = "";
                    DateTime newEndTime;
                    // 获取当前身份的rank（RMB价格）
                    wap2_smallType_BLL wap2_smallType_BLL = new wap2_smallType_BLL(string_10);
                    var currentIdVo = wap2_smallType_BLL.GetModel(userVo.SessionTimeout);
                    if (currentIdVo != null)
                    {
                        if (long.Parse(toid) == currentIdVo.id)
                        {
                            // 如果身份ID相同，直接延长有效期
                            newEndTime = userVo.endTime.AddDays(int.Parse(num) * 30);
                        }
                        else
                        {
                            // 不同价格身份之间的转换逻辑
                            if (currentIdVo.rank > 0 && idVo.rank > 0)
                            {
                                // 计算当前身份剩余价值（RMB）
                                TimeSpan remainingTime = userVo.endTime - DateTime.Now;
                                int remainingDays = remainingTime.Days > 0 ? remainingTime.Days : 0;
                                decimal currentValueRMB = (currentIdVo.rank / 30m) * remainingDays;

                                // 将RMB转换为妖晶
                                long currentValueCoin = (long)(currentValueRMB * RMB_TO_COIN_RATIO);

                                // 计算新身份对应的天数
                                decimal newIdentityRMB = idVo.rank;
                                decimal newIdentityDaysDecimal = (currentValueRMB / idVo.rank) * 30m;
                                int newIdentityDays = (int)Math.Floor(newIdentityDaysDecimal);

                                // 计算购买的天数
                                int purchasedDays = int.Parse(num) * 30;

                                // 新有效期
                                newEndTime = DateTime.Now.AddDays(newIdentityDays + purchasedDays);
                            }
                            else
                            {
                                // 如果新身份不能购买或当前身份不能转换
                                INFO = "CLOSE";
                                return;
                            }
                        }
                    }
                    else
                    {
                        // 新身份的有效期从购买日开始计算
                        newEndTime = DateTime.Now.AddDays(int.Parse(num) * 30);
                    }

                    // 更新用户的身份和有效期
                    text = string.Concat(",endtime='", newEndTime, "',sessiontimeout=", idVo.id, " ");
                    string orderID = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + userid;
                    try
                    {
                        MainBll.UpdateSQL("UPDATE [user] SET money=money-" + toNeedMoney + ",sessiontimeout=" + idVo.id + ",endtime='" + newEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE siteid=" + siteid + " AND userid=" + userid);
                        SaveBankLog(userid, "购买身份级别", "-" + toNeedMoney, userid, nickname, "购买身份级别[" + idVo.id + "]");
                        INFO = "OK";
                    }
                    catch
                    {
                        // 记录错误日志（具体实现根据实际情况）
                        INFO = "ERROR";
                    }
                }
                VisiteCount("使用妖晶购买了身份级别");
            }
        }
    }
}