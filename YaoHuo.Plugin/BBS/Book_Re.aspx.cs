using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class Book_Re : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_BBSRE_Anonymous_Open = PubConstant.GetAppString("KL_BBSRE_Anonymous_Open");

        public string KL_CheckBBSreCount = PubConstant.GetAppString("KL_CheckBBSreCount");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string KelinkWAP_Check = PubConstant.GetConnectionString("kelinkWAP_Check");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string INFO = "";

        public string id = "0";

        public string lpage = "";

        public string ot = "0";

        public string[] facelist;

        public string[] facelistImg;

        public string reShowInfo = "";

        public List<user_Model> userListVo_IDName = null;

        public List<wap_bbsre_Model> listVo = null;

        public List<wap_bbsre_Model> listVoTop = null;

        public wap_bbs_Model bookVo = null;

        public wap_bbs_BLL bbsbll = null;

        public StringBuilder strhtml = new StringBuilder();

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        public string getmoney = "0";

        public string getexpr = "0";

        public long getTodayMoney = 0L;

        public long allMoney = 0L;

        public string reply = "";

        public string needpwFlag = "";

        public string contentmax = "2";

        public string content_max = "2";

        public string sendmsg = "";

        public string sendmsg2 = "";

        public string touserid = "";

        public string mainuserid = "";

        public string showhead = "0";

        public string getmoney2 = "";

        public bool isNeedSecret = false;

        public string orderby = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.classid != "0" && base.classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                base.ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }
            this.action = base.Request.Form.Get("action");
            this.id = base.GetRequestValue("id");
            this.lpage = base.GetRequestValue("lpage");
            this.ot = base.GetRequestValue("ot");
            this.reply = base.GetRequestValue("reply");
            this.sendmsg = base.GetRequestValue("sendmsg");
            this.sendmsg2 = base.GetRequestValue("sendmsg2");
            this.touserid = base.GetRequestValue("touserid");
            this.needpwFlag = WapTool.getArryString(base.siteVo.Version, '|', 31);
            this.showhead = WapTool.getArryString(base.classVo.smallimg, '|', 38);
            this.orderby = WapTool.getArryString(base.classVo.smallimg, '|', 43);
            if (!WapTool.IsNumeric(this.orderby))
            {
                this.orderby = "0";
            }
            if (base.classVo.topicID != "" && base.classVo.topicID != "0" && base.IsCheckManagerLvl("|00|01|03|04|", ""))
            {
                this.isNeedSecret = true;
            }
            if (!WapTool.IsNumeric(this.id))
            {
                base.ShowTipInfo("贴子ID参数为非数字！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
            }
            this.bbsbll = new wap_bbs_BLL(this.a);
            this.bookVo = this.bbsbll.GetModel(long.Parse(this.id));
            if (this.bookVo == null)
            {
                base.ShowTipInfo("已删除！或不存在！", "");
            }
            switch (this.action)
            {
                case "add":
                    this.add();
                    break;

                case "class":
                    this.showclass();
                    break;

                default:
                    this.showclass();
                    break;
            }
        }

        public void showclass()
        {
            this.mainuserid = base.GetRequestValue("mainuserid");
            this.condition = "devid='" + base.siteid + "' and bookid=" + long.Parse(this.id) + " and ischeck=0 and book_top=0 ";
            if (WapTool.IsNumeric(this.mainuserid))
            {
                this.condition = this.condition + " and userid=" + this.mainuserid;
            }
            try
            {
                if (base.classVo.ismodel < 1L)
                {
                    this.pageSize = Convert.ToInt32(base.siteVo.MaxPerPage_Default);
                }
                else
                {
                    this.pageSize = Convert.ToInt32(base.classVo.ismodel);
                }
                wap_bbsre_BLL wap_bbsre_BLL;
                wap_bbsre_BLL = new wap_bbsre_BLL(this.a);
                if (this.mainuserid == "")
                {
                    this.total = this.bookVo.book_re;
                }
                else
                {
                    this.total = wap_bbsre_BLL.GetListCount(this.condition);
                }
                if (base.GetRequestValue("page") != "")
                {
                    this.CurrentPage = int.Parse(base.GetRequestValue("page"));
                }
                this.CurrentPage = WapTool.CheckCurrpage(this.total, this.pageSize, this.CurrentPage);
                this.index = this.pageSize * (this.CurrentPage - 1L);
                this.linkURL = base.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "&amp;getTotal=" + this.total + "&amp;ot=" + this.ot + "&amp;mainuserid=" + this.mainuserid;
                this.linkURL = WapTool.GetPageLink(base.ver, base.lang, Convert.ToInt32(this.total), this.pageSize, this.CurrentPage, this.linkURL, WapTool.getArryString(base.classVo.smallimg, '|', 40));
                if (this.ot == "1")
                {
                    this.listVo = wap_bbsre_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 0);
                    if (this.CurrentPage == 1L)
                    {
                        this.listVoTop = wap_bbsre_BLL.GetListTopVo(this.condition.Replace("book_top=0", "book_top=1"), 0);
                        int num;
                        num = 0;
                        while (this.listVoTop != null && num < this.listVoTop.Count)
                        {
                            this.listVo.Insert(0, this.listVoTop[num]);
                            if (num > 10)
                            {
                                break;
                            }
                            num++;
                        }
                    }
                }
                else
                {
                    if ("1".Equals(WapTool.KL_OpenCache) && this.CurrentPage == 1L)
                    {
                        WapTool.DataBBSReArray.TryGetValue("bbsRe" + base.siteid + this.id, out this.listVo);
                    }
                    if (this.listVo == null)
                    {
                        this.listVo = wap_bbsre_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 1);
                        if (this.CurrentPage == 1L)
                        {
                            this.listVoTop = wap_bbsre_BLL.GetListTopVo(this.condition.Replace("book_top=0", "book_top=1"), 1);
                            int num;
                            num = 0;
                            while (this.listVoTop != null && num < this.listVoTop.Count)
                            {
                                if (this.listVo != null)
                                {
                                    this.listVo.Insert(0, this.listVoTop[num]);
                                    if (num > 10)
                                    {
                                        break;
                                    }
                                    num++;
                                    continue;
                                }
                                this.listVo = this.listVoTop;
                                break;
                            }
                            if ("1".Equals(WapTool.KL_OpenCache))
                            {
                                try
                                {
                                    WapTool.DataBBSReArray.Add("bbsRe" + base.siteid + this.id, this.listVo);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                }
                if (base.classVo.bbsFace.IndexOf('_') < 0)
                {
                    base.classVo.bbsFace = "_";
                }
                this.facelist = base.classVo.bbsFace.Split('_')[0].Split('|');
                this.facelistImg = base.classVo.bbsFace.Split('_')[1].Split('|');
                if (base.classVo.bbsType.IndexOf('_') < 0)
                {
                    base.classVo.bbsType = "_";
                }
                string[] array;
                array = base.classVo.bbsType.Split('_')[1].Split('|');
                this.reShowInfo = array[new Random().Next(0, array.Length - 1)];
                if (WapTool.GetSiteDefault(base.siteVo.Version, 33) != "1" && this.listVo != null)
                {
                    StringBuilder stringBuilder;
                    stringBuilder = new StringBuilder();
                    stringBuilder.Append("siteid=" + base.siteid + " and userid in(");
                    int num;
                    num = 0;
                    while (this.listVo != null && num < this.listVo.Count)
                    {
                        stringBuilder.Append(this.listVo[num].userid);
                        stringBuilder.Append(",");
                        num++;
                    }
                    stringBuilder.Append("0)");
                    this.userListVo_IDName = base.MainBll.GetUserListVo(stringBuilder.ToString());
                }
            }
            catch (Exception ex2)
            {
                this.ERROR = WapTool.ErrorToString(ex2.ToString());
            }
        }

        public void add()
        {
            try
            {
                var isBlack = false;//是黑名单
                var sqlStr = string.Empty;//数据库脚本
                if ("1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 3)))
                {
                    base.ShowTipInfo("回贴功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classVo.childid);
                }
                if (this.bookVo == null)
                {
                    base.ShowTipInfo("已删除！或不存在！", "");
                }
                else if (this.bookVo.ischeck == 1L)
                {
                    base.ShowTipInfo("正在审核中！", "");
                }
                else if (this.bookVo.book_classid.ToString() != base.classid)
                {
                    base.ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
                }
                else if (this.bookVo.islock == 1L)
                {
                    base.ShowTipInfo("此贴已锁！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                }
                else if (this.bookVo.islock == 2L)
                {
                    base.ShowTipInfo("此贴已结！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                }
                string text;
                text = base.GetRequestValue("content");
                this.contentmax = WapTool.getArryString(base.classVo.smallimg, '|', 26);
                if (!WapTool.IsNumeric(this.contentmax) || this.contentmax == "0")
                {
                    this.contentmax = "0";
                }
                this.content_max = WapTool.getArryString(base.classVo.smallimg, '|', 32);
                if (!WapTool.IsNumeric(this.content_max))
                {
                    this.content_max = "0";
                }
                base.GetRequestValue("backurl");
                string requestValue;
                requestValue = base.GetRequestValue("face");
                string text2;
                text2 = base.GetRequestValue("reply");
                if (!WapTool.IsNumeric(text2))
                {
                    text2 = "0";
                }
                string value;
                value = WapTool.getArryString(base.classVo.smallimg, '|', 12);
                if (this.KL_BBSRE_Anonymous_Open != "1")
                {
                    value = "0";
                }
                if (!"1".Equals(value))
                {
                    base.IsLogin(base.userid, "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                }
                if (this.needpwFlag == "1")
                {
                    if (!"1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 12)))
                    {
                        base.IsLogin(base.userid, "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                    }
                    base.needPassWordToAdmin();
                }
                string text3 = WapTool.GetSiteDefault(base.siteVo.Version, 15);
                if (!WapTool.IsNumeric(text3))
                {
                    text3 = "0";
                }
                long num = Convert.ToInt64(text3);
                if (num > 0L)
                {
                    long num2;
                    num2 = WapTool.DateDiff(DateTime.Now, base.userVo.RegTime, "MM");
                    if (num2 < num)
                    {
                        base.ShowTipInfo("请再过:" + (num - num2) + "分钟才能回贴！", "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id);
                    }
                }
                this.getmoney2 = (WapTool.getArryString(base.classVo.smallimg, '|', 36) + ",").Split(',')[0];
                if (text.Trim().Length < long.Parse(this.contentmax))
                {
                    this.INFO = "NULL";
                }
                else if (this.content_max != "0" && text.Trim().Length > long.Parse(this.content_max))
                {
                    this.INFO = "TITLEMAX";
                }
                else if (text.IndexOf("$(") >= 0)
                {
                    this.INFO = "ERR_FORMAT";
                }
                else if (text.Equals(this.Session["content"]))
                {
                    this.INFO = "REPEAT";
                }
                else if (base.isCheckIPTime(long.Parse(this.KL_CheckIPTime)))
                {
                    this.INFO = "WAITING";
                }
                else if (this.KL_CheckBBSreCount != "0" && !WapTool.CheckUserBBSCount(base.siteid, base.userid, this.KL_CheckBBSreCount, "bbsre"))
                {
                    this.INFO = "MAX";
                }
                else if (WapTool.isLockuser(base.siteid, base.userid, base.classid) > -1L)
                {
                    this.INFO = "LOCK";
                }
                else if (this.isNeedSecret && base.Request.Form.Get("secret") != base.classVo.topicID)
                {
                    this.INFO = "ERROR_Secret";
                }
                else if (this.getmoney2.IndexOf('-') == 0 && base.userVo.money + long.Parse(this.getmoney2) < 0L)
                {
                    this.INFO = "NOMONEY";
                }
                else
                {
                    this.Session["content"] = text;
                    requestValue = requestValue.Replace("表情", "");
                    if (requestValue.Trim().Length > 3 && requestValue.Substring(requestValue.Length - 3, 3).ToLower() == "gif")
                    {
                        text = "[img]face/" + requestValue + "[/img]" + text;
                    }
                    if (this.bookVo.reShow > 0L && !WapTool.isHasReplyToday(base.siteid, base.userid, this.id))
                    {
                        this.allMoney = this.bookVo.reShow;
                    }
                    wap_bbsre_Model wap_bbsre_Model;
                    wap_bbsre_Model = new wap_bbsre_Model();
                    wap_bbsre_Model.devid = base.siteid;
                    wap_bbsre_Model.userid = long.Parse(base.userid);
                    wap_bbsre_Model.nickname = base.nickname;
                    wap_bbsre_Model.classid = long.Parse(base.classid);
                    wap_bbsre_Model.bookid = long.Parse(this.id);
                    wap_bbsre_Model.content = text;
                    wap_bbsre_Model.redate = DateTime.Now;
                    wap_bbsre_Model.ischeck = base.siteVo.isCheck;
                    wap_bbsre_Model.reply = long.Parse(text2);
                    if (this.bookVo.freeMoney > 0L)
                    {
                        string[] array;
                        array = this.bookVo.freeRule.Split('_');
                        string[] array2;
                        array2 = array[0].Split('|');
                        string[] array3;
                        array3 = array[1].Split('|');
                        if (array2.Length == 1)
                        {
                            if (this.bookVo.freeLeftMoney > 0L && !WapTool.isHasReplyToday(base.siteid, base.userid, this.id))
                            {
                                if (this.bookVo.freeLeftMoney > long.Parse(array3[0]))
                                {
                                    this.allMoney += long.Parse(array3[0]);
                                    wap_bbsre_Model.myGetMoney = long.Parse(array3[0]);
                                }
                                else
                                {
                                    this.allMoney += this.bookVo.freeLeftMoney;
                                    wap_bbsre_Model.myGetMoney = this.bookVo.freeLeftMoney;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < array2.Length; i++)
                            {
                                if (this.bookVo.book_re + 1L == long.Parse(array2[i]))
                                {
                                    this.allMoney += long.Parse(array3[i]);
                                    wap_bbsre_Model.myGetMoney = long.Parse(array3[i]);
                                }
                            }
                        }
                    }
                    new wap_bbsre_BLL(this.a).Add(wap_bbsre_Model);
                    string text4 = "";
                    if (this.bookVo.isdown != 2L && !(this.orderby == "1") && !(this.orderby == "2"))
                    {
                        text4 = string.Concat(",reDate='", DateTime.Now, "'");
                    }
                    if (this.allMoney > 0L)
                    {
                        text4 = text4 + ",freeleftmoney=freeleftmoney - " + this.allMoney + " ";
                    }
                    base.MainBll.UpdateSQL("update [wap_bbs] set book_re=book_re+1 " + text4 + " where id=" + long.Parse(this.id));
                    if (!"1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 9)) && this.bookVo.book_re == 0L)
                    {
                        //黑名单不发送消息（他的黑名单有我）
                        //this.bookVo.book_pub ==> 楼主用户ID
                        isBlack = BlackTool.IsBlackUser(KelinkWAP_Check, this.bookVo.book_pub, base.userid);
                        if (!isBlack)
                        {
                            //第一个回复通知
                            sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "第一个回复了你的贴子','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.bookVo.book_pub, ",1)");
                            base.MainBll.UpdateSQL(sqlStr);
                        }
                    }
                    else if ("1".Equals(this.sendmsg))
                    {
                        //黑名单不发送消息（他的黑名单有我）
                        //this.bookVo.book_pub ==> 楼主用户ID
                        isBlack = BlackTool.IsBlackUser(KelinkWAP_Check, this.bookVo.book_pub, base.userid);
                        if (!isBlack)
                        {
                            //回复通知楼主
                            sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "回复了你的贴子','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.bookVo.book_pub, ",1)");
                            base.MainBll.UpdateSQL(sqlStr);
                        }
                    }
                    if ("1".Equals(this.sendmsg2) && WapTool.IsNumeric(this.touserid))
                    {
                        //黑名单不发送消息（他的黑名单有我）
                        isBlack = BlackTool.IsBlackUser(KelinkWAP_Check, this.touserid, base.userid);
                        if (!isBlack)
                        {
                            //回复通知某一楼
                            sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "回复了你的回复','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.touserid, ",1)");
                            base.MainBll.UpdateSQL(sqlStr);
                        }
                    }
                    this.getmoney = WapTool.GetSiteDefault(base.siteVo.moneyregular, 1);
                    if (!WapTool.IsNumeric(this.getmoney))
                    {
                        this.getmoney = "0";
                    }
                    this.getexpr = WapTool.GetSiteDefault(base.siteVo.lvlRegular, 1);
                    if (!WapTool.IsNumeric(this.getexpr))
                    {
                        this.getexpr = "0";
                    }
                    string[] array4;
                    array4 = (WapTool.getArryString(base.classVo.smallimg, '|', 36) + ",").Split(',');
                    if (WapTool.IsNumeric(array4[0].Replace("-", "")))
                    {
                        this.getmoney = array4[0];
                    }
                    if (WapTool.IsNumeric(array4[1].Replace("-", "")))
                    {
                        this.getexpr = array4[1];
                    }
                    this.allMoney = long.Parse(this.getmoney) + this.allMoney;
                    base.MainBll.UpdateSQL("update [user] set [money]=([money]+" + this.allMoney + "),expR=expR+" + this.getexpr + ",bbsReCount=" + (base.userVo.bbsReCount + 1L) + " where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                    this.bookVo.book_re = this.bookVo.book_re + 1L;
                    WapTool.ClearDataBBSRe("bbsRe" + base.siteid + this.id);
                    this.INFO = "OK";
                    base.SaveBankLog(base.userid, "论坛回贴", this.allMoney.ToString(), base.userid, base.nickname, "回复贴子[" + this.id + "]");
                    base.Action_user_doit(2);
                }
                base.VisiteCount("回复了贴子:<a href=\"" + base.http_start + "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "\">" + WapTool.GetShowImg(this.bookVo.book_title, "200", "bbs") + "</a>");
                this.showclass();
            }
            catch (Exception ex)
            {
                this.ERROR = WapTool.ErrorToString(ex.ToString());
            }
            if (this.INFO == "WAITING")
            {
                base.VisiteCount("回复了贴子。");
            }
        }

        public string ShowNickName_color(long userid, string nickname)
        {
            //AI 解混淆的结果
            if (this.userListVo_IDName == null)
            {
                return nickname;
            }

            foreach (var item in this.userListVo_IDName)
            {
                if (item.userid == userid)
                {
                    nickname = WapTool.GetColorNickName(item.idname, nickname, base.lang, base.ver, item.endTime);
                    break;
                }
            }

            return nickname;
        }
    }
}
