using System;
using System.Collections.Generic;
using System.Text;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using KeLin.WebSite.bbs;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class Book_Re : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_BBSRE_Anonymous_Open = PubConstant.GetAppString("KL_BBSRE_Anonymous_Open");

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

        public string KL_CheckBBSreCount = PubConstant.GetAppString("KL_CheckBBSreCount");

        public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

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
            //Discarded unreachable code: IL_01ca
            bool flag = default(bool);
            string text = default(string);
            while (true)
            {
                int num;
                num = 7;
                while (true)
                {
                    int num2;
                    int num3;
                    switch (num)
                    {
                        case 7:
                            num = ((!(base.classid != "0")) ? 29 : 19);
                            continue;
                        case 28:
                            num = 0;
                            continue;
                        case 0:
                            num = ((!(base.classVo.topicID != "")) ? 17 : 20);
                            continue;
                        case 22:
                            this.orderby = "0";
                            num = 28;
                            continue;
                        case 20:
                            num = 25;
                            continue;
                        case 25:
                            num2 = ((!(base.classVo.topicID != "0")) ? 1 : 0);
                            goto IL_01c6;
                        case 2:
                            flag = WapTool.IsNumeric(this.id);
                            num = 33;
                            continue;
                        case 33:
                            if (!flag)
                            {
                                num = 27;
                                continue;
                            }
                            goto case 30;
                        case 19:
                            num = 18;
                            continue;
                        case 18:
                            num3 = ((!(base.classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                            goto IL_0405;
                        case 12:
                            num = 2;
                            continue;
                        case 13:
                            if (!flag)
                            {
                                num = 15;
                                continue;
                            }
                            goto case 2;
                        case 8:
                            num = 24;
                            continue;
                        case 24:
                            if (!(text == "add"))
                            {
                                num = 32;
                                continue;
                            }
                            this.add();
                            num = 21;
                            continue;
                        case 15:
                            flag = !base.IsCheckManagerLvl("|00|01|03|04|", "");
                            num = 1;
                            continue;
                        case 1:
                            if (!flag)
                            {
                                num = 5;
                                continue;
                            }
                            goto case 12;
                        case 31:
                            return;
                        case 14:
                            base.ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                            num = 6;
                            continue;
                        case 29:
                            num3 = 1;
                            goto IL_0405;
                        case 21:
                            return;
                        case 17:
                            num2 = 1;
                            goto IL_01c6;
                        case 6:
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
                            flag = WapTool.IsNumeric(this.orderby);
                            num = 34;
                            continue;
                        case 34:
                            if (!flag)
                            {
                                num = 22;
                                continue;
                            }
                            goto case 28;
                        case 32:
                            num = 35;
                            continue;
                        case 26:
                            if (!flag)
                            {
                                num = 14;
                                continue;
                            }
                            goto case 6;
                        case 27:
                            base.ShowTipInfo("贴子ID参数为非数字！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                            num = 30;
                            continue;
                        case 10:
                            text = this.action;
                            num = 4;
                            continue;
                        case 4:
                            if (text != null)
                            {
                                num = 16;
                                continue;
                            }
                            goto case 35;
                        case 30:
                            this.bbsbll = new wap_bbs_BLL(this.a);
                            this.bookVo = this.bbsbll.GetModel(long.Parse(this.id));
                            flag = this.bookVo != null;
                            num = 11;
                            continue;
                        case 11:
                            if (!flag)
                            {
                                num = 3;
                                continue;
                            }
                            goto case 10;
                        case 35:
                            this.showclass();
                            num = 9;
                            continue;
                        case 9:
                            return;
                        case 16:
                            num = 23;
                            continue;
                        case 23:
                            if (text == "class")
                            {
                                this.showclass();
                                num = 31;
                            }
                            else
                            {
                                num = 8;
                            }
                            continue;
                        case 3:
                            base.ShowTipInfo("已删除！或不存在！", "");
                            num = 10;
                            continue;
                        case 5:
                            {
                                this.isNeedSecret = true;
                                num = 12;
                                continue;
                            }
                        IL_01c6:
                            flag = (byte)num2 != 0;
                            if (true)
                            {
                            }
                            num = 13;
                            continue;
                        IL_0405:
                            flag = (byte)num3 != 0;
                            num = 26;
                            continue;
                    }
                    break;
                }
            }
        }

        public void showclass()
        {
            //Discarded unreachable code: IL_008a
            int num2 = default(int);
            wap_bbsre_BLL wap_bbsre_BLL = default(wap_bbsre_BLL);
            string siteDefault = default(string);
            StringBuilder stringBuilder = default(StringBuilder);
            while (true)
            {
                this.mainuserid = base.GetRequestValue("mainuserid");
                this.condition = "devid='" + base.siteid + "' and bookid=" + long.Parse(this.id) + " and ischeck=0 and book_top=0 ";
                bool flag;
                flag = !WapTool.IsNumeric(this.mainuserid);
                if (true)
                {
                }
                int num;
                num = 0;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (!flag)
                            {
                                num = 2;
                                continue;
                            }
                            goto case 1;
                        case 1:
                            try
                            {
                                while (true)
                                {
                                    flag = base.classVo.ismodel >= 1;
                                    num = 15;
                                    while (true)
                                    {
                                        int num4;
                                        int num6;
                                        int num5;
                                        int num3;
                                        switch (num)
                                        {
                                            case 15:
                                                if (!flag)
                                                {
                                                    num = 49;
                                                    continue;
                                                }
                                                this.pageSize = Convert.ToInt32(base.classVo.ismodel);
                                                num = 32;
                                                continue;
                                            case 65:
                                                num = 4;
                                                continue;
                                            case 4:
                                                num4 = ((num2 < this.listVoTop.Count) ? 1 : 0);
                                                goto IL_09ac;
                                            case 2:
                                                this.facelist = base.classVo.bbsFace.Split('_')[0].Split('|');
                                                this.facelistImg = base.classVo.bbsFace.Split('_')[1].Split('|');
                                                flag = base.classVo.bbsType.IndexOf('_') >= 0;
                                                num = 0;
                                                continue;
                                            case 0:
                                                if (!flag)
                                                {
                                                    num = 73;
                                                    continue;
                                                }
                                                goto case 40;
                                            case 19:
                                                num2++;
                                                num = 54;
                                                continue;
                                            case 51:
                                            case 74:
                                                num = 57;
                                                continue;
                                            case 57:
                                                num = ((this.listVoTop == null) ? 66 : 55);
                                                continue;
                                            case 71:
                                                this.CurrentPage = int.Parse(base.GetRequestValue("page"));
                                                num = 1;
                                                continue;
                                            case 27:
                                                this.listVoTop = wap_bbsre_BLL.GetListTopVo(this.condition.Replace("book_top=0", "book_top=1"), 0);
                                                num2 = 0;
                                                num = 74;
                                                continue;
                                            case 68:
                                                if (!flag)
                                                {
                                                    num = 42;
                                                    continue;
                                                }
                                                num2++;
                                                num = 51;
                                                continue;
                                            case 59:
                                                WapTool.DataBBSReArray.TryGetValue("bbsRe" + base.siteid + this.id, out this.listVo);
                                                num = 20;
                                                continue;
                                            case 39:
                                                num = 30;
                                                continue;
                                            case 81:
                                                flag = this.CurrentPage != 1;
                                                num = 77;
                                                continue;
                                            case 77:
                                                if (!flag)
                                                {
                                                    num = 59;
                                                    continue;
                                                }
                                                goto case 20;
                                            case 8:
                                                base.classVo.bbsFace = "_";
                                                num = 2;
                                                continue;
                                            case 49:
                                                this.pageSize = Convert.ToInt32(base.siteVo.MaxPerPage_Default);
                                                num = 58;
                                                continue;
                                            case 32:
                                            case 58:
                                                wap_bbsre_BLL = new wap_bbsre_BLL(this.a);
                                                flag = !(this.mainuserid == "");
                                                num = 43;
                                                continue;
                                            case 43:
                                                if (!flag)
                                                {
                                                    num = 33;
                                                    continue;
                                                }
                                                this.total = wap_bbsre_BLL.GetListCount(this.condition);
                                                num = 47;
                                                continue;
                                            case 76:
                                                num4 = 0;
                                                goto IL_09ac;
                                            case 70:
                                                if (!flag)
                                                {
                                                    num = 78;
                                                    continue;
                                                }
                                                this.listVo = this.listVoTop;
                                                num = 22;
                                                continue;
                                            case 45:
                                                this.listVo = wap_bbsre_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 1);
                                                flag = this.CurrentPage != 1;
                                                num = 67;
                                                continue;
                                            case 67:
                                                if (!flag)
                                                {
                                                    num = 35;
                                                    continue;
                                                }
                                                goto case 31;
                                            case 72:
                                                if (!flag)
                                                {
                                                    num = 81;
                                                    continue;
                                                }
                                                goto case 62;
                                            case 48:
                                                num = 17;
                                                continue;
                                            case 42:
                                                num = 53;
                                                continue;
                                            case 21:
                                                num6 = 1;
                                                goto IL_06e4;
                                            case 11:
                                                num = 31;
                                                continue;
                                            case 31:
                                                num = 69;
                                                continue;
                                            case 53:
                                            case 60:
                                                num = 18;
                                                continue;
                                            case 10:
                                                if (!flag)
                                                {
                                                    num = 13;
                                                    continue;
                                                }
                                                goto case 38;
                                            case 41:
                                            case 79:
                                                flag = base.classVo.bbsFace.IndexOf('_') >= 0;
                                                num = 7;
                                                continue;
                                            case 7:
                                                if (!flag)
                                                {
                                                    num = 8;
                                                    continue;
                                                }
                                                goto case 2;
                                            case 50:
                                            case 54:
                                                num = 16;
                                                continue;
                                            case 16:
                                                num = ((this.listVoTop != null) ? 65 : 76);
                                                continue;
                                            case 40:
                                                {
                                                    string[] array;
                                                    array = base.classVo.bbsType.Split('_')[1].Split('|');
                                                    this.reShowInfo = array[new Random().Next(0, array.Length - 1)];
                                                    siteDefault = WapTool.GetSiteDefault(base.siteVo.Version, 33);
                                                    num = 64;
                                                    continue;
                                                }
                                            case 64:
                                                num = ((siteDefault != "1") ? 37 : 21);
                                                continue;
                                            case 66:
                                                num5 = 0;
                                                goto IL_0a36;
                                            case 20:
                                                num = 62;
                                                continue;
                                            case 73:
                                                base.classVo.bbsType = "_";
                                                num = 40;
                                                continue;
                                            case 17:
                                            case 22:
                                            case 44:
                                                flag = !"1".Equals(WapTool.KL_OpenCache);
                                                num = 24;
                                                continue;
                                            case 24:
                                                if (!flag)
                                                {
                                                    num = 39;
                                                    continue;
                                                }
                                                goto case 11;
                                            case 29:
                                            case 75:
                                                num = 9;
                                                continue;
                                            case 9:
                                                num = ((this.listVo == null) ? 52 : 28);
                                                continue;
                                            case 69:
                                                num = 41;
                                                continue;
                                            case 30:
                                                try
                                                {
                                                    WapTool.DataBBSReArray.Add("bbsRe" + base.siteid + this.id, this.listVo);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                                num = 11;
                                                continue;
                                            case 37:
                                                num = 61;
                                                continue;
                                            case 61:
                                                num6 = ((this.listVo == null) ? 1 : 0);
                                                goto IL_06e4;
                                            case 28:
                                                num = 26;
                                                continue;
                                            case 26:
                                                num3 = ((num2 < this.listVo.Count) ? 1 : 0);
                                                goto IL_09d6;
                                            case 33:
                                                this.total = this.bookVo.book_re;
                                                num = 23;
                                                continue;
                                            case 36:
                                                if (flag)
                                                {
                                                    flag = this.listVo == null;
                                                    num = 70;
                                                }
                                                else
                                                {
                                                    num = 44;
                                                }
                                                continue;
                                            case 80:
                                                if (!flag)
                                                {
                                                    num = 5;
                                                    continue;
                                                }
                                                stringBuilder.Append(this.listVo[num2].userid);
                                                stringBuilder.Append(",");
                                                num2++;
                                                num = 29;
                                                continue;
                                            case 62:
                                                flag = this.listVo != null;
                                                num = 34;
                                                continue;
                                            case 34:
                                                if (!flag)
                                                {
                                                    num = 45;
                                                    continue;
                                                }
                                                goto case 69;
                                            case 46:
                                                if (flag)
                                                {
                                                    this.listVo.Insert(0, this.listVoTop[num2]);
                                                    flag = num2 <= 10;
                                                    num = 68;
                                                }
                                                else
                                                {
                                                    num = 60;
                                                }
                                                continue;
                                            case 13:
                                                stringBuilder = new StringBuilder();
                                                stringBuilder.Append("siteid=" + base.siteid + " and userid in(");
                                                num2 = 0;
                                                num = 75;
                                                continue;
                                            case 55:
                                                num = 56;
                                                continue;
                                            case 56:
                                                num5 = ((num2 < this.listVoTop.Count) ? 1 : 0);
                                                goto IL_0a36;
                                            case 18:
                                                num = 79;
                                                continue;
                                            case 5:
                                                stringBuilder.Append("0)");
                                                this.userListVo_IDName = base.MainBll.GetUserListVo(stringBuilder.ToString());
                                                num = 38;
                                                continue;
                                            case 35:
                                                this.listVoTop = wap_bbsre_BLL.GetListTopVo(this.condition.Replace("book_top=0", "book_top=1"), 1);
                                                num2 = 0;
                                                num = 50;
                                                continue;
                                            case 23:
                                            case 47:
                                                flag = !(base.GetRequestValue("page") != "");
                                                num = 12;
                                                continue;
                                            case 12:
                                                if (!flag)
                                                {
                                                    num = 71;
                                                    continue;
                                                }
                                                goto case 1;
                                            case 1:
                                                this.CurrentPage = WapTool.CheckCurrpage(this.total, this.pageSize, this.CurrentPage);
                                                this.index = this.pageSize * (this.CurrentPage - 1);
                                                this.linkURL = base.http_start + "bbs/book_re.aspx?action=class&amp;siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "&amp;lpage=" + this.lpage + "&amp;getTotal=" + this.total + "&amp;ot=" + this.ot + "&amp;mainuserid=" + this.mainuserid;
                                                this.linkURL = WapTool.GetPageLink(base.ver, base.lang, Convert.ToInt32(this.total), this.pageSize, this.CurrentPage, this.linkURL, WapTool.getArryString(base.classVo.smallimg, '|', 40));
                                                flag = !(this.ot == "1");
                                                num = 63;
                                                continue;
                                            case 63:
                                                if (flag)
                                                {
                                                    flag = !"1".Equals(WapTool.KL_OpenCache);
                                                    num = 72;
                                                }
                                                else
                                                {
                                                    num = 6;
                                                }
                                                continue;
                                            case 6:
                                                this.listVo = wap_bbsre_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 0);
                                                flag = this.CurrentPage != 1;
                                                num = 14;
                                                continue;
                                            case 14:
                                                if (!flag)
                                                {
                                                    num = 27;
                                                    continue;
                                                }
                                                goto case 18;
                                            case 52:
                                                num3 = 0;
                                                goto IL_09d6;
                                            case 78:
                                                this.listVo.Insert(0, this.listVoTop[num2]);
                                                flag = num2 <= 10;
                                                num = 3;
                                                continue;
                                            case 3:
                                                num = ((!flag) ? 48 : 19);
                                                continue;
                                            case 38:
                                                num = 25;
                                                continue;
                                            case 25:
                                                return;
                                            IL_09d6:
                                                flag = (byte)num3 != 0;
                                                num = 80;
                                                continue;
                                            IL_0a36:
                                                flag = (byte)num5 != 0;
                                                num = 46;
                                                continue;
                                            IL_06e4:
                                                flag = (byte)num6 != 0;
                                                num = 10;
                                                continue;
                                            IL_09ac:
                                                flag = (byte)num4 != 0;
                                                num = 36;
                                                continue;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex2)
                            {
                                this.ERROR = WapTool.ErrorToString(ex2.ToString());
                                return;
                            }
                        case 2:
                            this.condition = this.condition + " and userid=" + this.mainuserid;
                            num = 1;
                            continue;
                    }
                    break;
                }
            }
        }

        public void add()
        {
            //Discarded unreachable code: IL_2205
            bool flag = default(bool);
            string value = default(string);
            int num4 = default(int);
            string text = default(string);
            string[] array2 = default(string[]);
            wap_bbsre_Model wap_bbsre_Model = default(wap_bbsre_Model);
            string text4 = default(string);
            string text3 = default(string);
            string text2 = default(string);
            string[] array = default(string[]);
            string[] array4 = default(string[]);
            long num11 = default(long);
            long num5 = default(long);
            string text5 = default(string);
            var sqlStr = string.Empty;
            var connName = "kelinkWAP_Check";//配置文件
            var connStr = PubConstant.GetConnectionString(connName);//数据库连接字符串
            var isBlack = false;//是黑名单
            while (true)
            {
                int num;
                num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 3:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            return;
                        case 2:
                            try
                            {
                                while (true)
                                {
                                IL_032b:
                                    flag = !"1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 3));
                                    num = 32;
                                    while (true)
                                    {
                                        int num2;
                                        int num3;
                                        int num8;
                                        int num7;
                                        int num6;
                                        int num10;
                                        int num9;
                                        switch (num)
                                        {
                                            case 32:
                                                if (!flag)
                                                {
                                                    num = 160;
                                                    continue;
                                                }
                                                goto case 15;
                                            case 92:
                                                if (!flag)
                                                {
                                                    num = 98;
                                                    continue;
                                                }
                                                flag = !base.isCheckIPTime(long.Parse(this.KL_CheckIPTime));
                                                num = 56;
                                                continue;
                                            case 106:
                                                value = WapTool.getArryString(base.classVo.smallimg, '|', 12);
                                                flag = !(this.KL_BBSRE_Anonymous_Open != "1");
                                                num = 61;
                                                continue;
                                            case 61:
                                                if (!flag)
                                                {
                                                    num = 104;
                                                    continue;
                                                }
                                                goto case 90;
                                            case 66:
                                                num4++;
                                                num = 114;
                                                continue;
                                            case 115:
                                                base.ShowTipInfo("正在审核中！", "");
                                                num = 107;
                                                continue;
                                            case 24:
                                                if (!flag)
                                                {
                                                    num = 91;
                                                    continue;
                                                }
                                                goto case 141;
                                            case 48:
                                                num = 127;
                                                continue;
                                            case 79:
                                                text = WapTool.GetSiteDefault(base.siteVo.Version, 15);
                                                flag = WapTool.IsNumeric(text);
                                                num = 163;
                                                continue;
                                            case 163:
                                                if (!flag)
                                                {
                                                    num = 82;
                                                    continue;
                                                }
                                                goto case 150;
                                            case 149:
                                                num = 76;
                                                continue;
                                            case 14:
                                                flag = WapTool.isHasReplyToday(base.siteid, base.userid, this.id);
                                                num = 173;
                                                continue;
                                            case 173:
                                                if (!flag)
                                                {
                                                    num = 89;
                                                    continue;
                                                }
                                                goto case 67;
                                            case 23:
                                                this.allMoney += long.Parse(array2[0]);
                                                wap_bbsre_Model.myGetMoney = long.Parse(array2[0]);
                                                num = 110;
                                                continue;
                                            case 38:
                                                this.INFO = "NOMONEY";
                                                num = 130;
                                                continue;
                                            case 43:
                                                num2 = 1;
                                                goto IL_0851;
                                            case 28:
                                                this.INFO = "ERROR_Secret";
                                                num = 40;
                                                continue;
                                            case 37:
                                            case 93:
                                                flag = !"1".Equals(this.sendmsg2);
                                                num = 75;
                                                continue;
                                            case 75:
                                                if (!flag)
                                                {
                                                    num = 3;
                                                    continue;
                                                }
                                                goto case 76;
                                            case 148:
                                                base.ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
                                                num = 152;
                                                continue;
                                            case 103:
                                                this.INFO = "NULL";
                                                num = 19;
                                                continue;
                                            case 132:
                                                wap_bbsre_Model = new wap_bbsre_Model();
                                                wap_bbsre_Model.devid = base.siteid;
                                                wap_bbsre_Model.userid = long.Parse(base.userid);
                                                wap_bbsre_Model.nickname = base.nickname;
                                                wap_bbsre_Model.classid = long.Parse(base.classid);
                                                wap_bbsre_Model.bookid = long.Parse(this.id);
                                                wap_bbsre_Model.content = text4;
                                                wap_bbsre_Model.redate = DateTime.Now;
                                                wap_bbsre_Model.ischeck = base.siteVo.isCheck;
                                                wap_bbsre_Model.reply = long.Parse(text3);
                                                flag = this.bookVo.freeMoney <= 0;
                                                num = 111;
                                                continue;
                                            case 111:
                                                if (!flag)
                                                {
                                                    num = 36;
                                                    continue;
                                                }
                                                goto case 171;
                                            case 27:
                                                base.IsLogin(base.userid, "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                                                num = 126;
                                                continue;
                                            case 110:
                                            case 131:
                                                num = 67;
                                                continue;
                                            case 165:
                                                if (flag)
                                                {
                                                    this.Session["content"] = text4;
                                                    text2 = text2.Replace("表情", "");
                                                    flag = text2.Trim().Length <= 3;
                                                    num = 24;
                                                }
                                                else
                                                {
                                                    num = 38;
                                                }
                                                continue;
                                            case 151:
                                                this.getmoney = "0";
                                                num = 74;
                                                continue;
                                            case 36:
                                                {
                                                    string[] array3;
                                                    array3 = this.bookVo.freeRule.Split('_');
                                                    array = array3[0].Split('|');
                                                    array2 = array3[1].Split('|');
                                                    flag = array.Length != 1;
                                                    num = 143;
                                                    continue;
                                                }
                                            case 143:
                                                if (!flag)
                                                {
                                                    num = 62;
                                                    continue;
                                                }
                                                num4 = 0;
                                                num = 18;
                                                continue;
                                            case 167:
                                                num = 132;
                                                continue;
                                            case 133:
                                                flag = this.allMoney <= 0;
                                                num = 140;
                                                continue;
                                            case 140:
                                                if (!flag)
                                                {
                                                    num = 84;
                                                    continue;
                                                }
                                                goto case 100;
                                            case 44:
                                                num3 = 0;
                                                goto IL_17bb;
                                            case 55:
                                                if (flag)
                                                {
                                                    flag = !text4.Equals(this.Session["content"]);
                                                    num = 92;
                                                }
                                                else
                                                {
                                                    num = 30;
                                                }
                                                continue;
                                            case 80:
                                                if (!flag)
                                                {
                                                    num = 115;
                                                    continue;
                                                }
                                                flag = !(this.bookVo.book_classid.ToString() != base.classid);
                                                num = 168;
                                                continue;
                                            case 96:
                                                if (!flag)
                                                {
                                                    num = 162;
                                                    continue;
                                                }
                                                goto case 78;
                                            case 9:
                                                base.ShowTipInfo("已删除！或不存在！", "");
                                                num = 153;
                                                continue;
                                            case 135:
                                                num = (flag ? 129 : 73);
                                                continue;
                                            case 59:
                                                text4 = "[img]face/" + text2 + "[/img]" + text4;
                                                num = 169;
                                                continue;
                                            case 113:
                                                num = 123;
                                                continue;
                                            case 123:
                                                num3 = ((!(this.orderby == "2")) ? 1 : 0);
                                                goto IL_17bb;
                                            case 10:
                                                this.content_max = WapTool.getArryString(base.classVo.smallimg, '|', 32);
                                                flag = WapTool.IsNumeric(this.content_max);
                                                num = 83;
                                                continue;
                                            case 83:
                                                if (!flag)
                                                {
                                                    num = 86;
                                                    continue;
                                                }
                                                goto case 12;
                                            case 162:
                                                base.ShowTipInfo("此贴已结！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                                                num = 78;
                                                continue;
                                            case 47:
                                                this.getexpr = "0";
                                                num = 144;
                                                continue;
                                            case 12:
                                                {
                                                    string requestValue;
                                                    requestValue = base.GetRequestValue("backurl");
                                                    text2 = base.GetRequestValue("face");
                                                    text3 = base.GetRequestValue("reply");
                                                    flag = WapTool.IsNumeric(text3);
                                                    num = 17;
                                                    continue;
                                                }
                                            case 17:
                                                if (!flag)
                                                {
                                                    num = 164;
                                                    continue;
                                                }
                                                goto case 106;
                                            case 125:
                                            case 166:
                                                num = 133;
                                                continue;
                                            case 7:
                                                this.getexpr = array4[1];
                                                num = 39;
                                                continue;
                                            case 30:
                                                this.INFO = "ERR_FORMAT";
                                                num = 31;
                                                continue;
                                            case 112:
                                                num = 46;
                                                continue;
                                            case 46:
                                                num8 = ((!(base.Request.Form.Get("secret") != base.classVo.topicID)) ? 1 : 0);
                                                goto IL_1582;
                                            case 126:
                                                flag = !(this.needpwFlag == "1");
                                                num = 138;
                                                continue;
                                            case 138:
                                                if (!flag)
                                                {
                                                    num = 102;
                                                    continue;
                                                }
                                                goto case 79;
                                            case 3:
                                                flag = !WapTool.IsNumeric(this.touserid);
                                                num = 174;
                                                continue;
                                            case 174:
                                                if (!flag)
                                                {
                                                    num = 170;
                                                    continue;
                                                }
                                                goto case 149;
                                            case 144:
                                                array4 = (WapTool.getArryString(base.classVo.smallimg, '|', 36) + ",").Split(',');
                                                flag = !WapTool.IsNumeric(array4[0].Replace("-", ""));
                                                num = 4;
                                                continue;
                                            case 4:
                                                if (!flag)
                                                {
                                                    num = 49;
                                                    continue;
                                                }
                                                goto case 161;
                                            case 90:
                                                flag = "1".Equals(value);
                                                num = 94;
                                                continue;
                                            case 94:
                                                if (!flag)
                                                {
                                                    num = 27;
                                                    continue;
                                                }
                                                goto case 126;
                                            case 120:
                                                num = ((!(this.content_max != "0")) ? 20 : 85);
                                                continue;
                                            case 86:
                                                this.content_max = "0";
                                                num = 12;
                                                continue;
                                            case 146:
                                                num11 = WapTool.DateDiff(DateTime.Now, base.userVo.RegTime, "MM");
                                                flag = num11 >= num5;
                                                num = 5;
                                                continue;
                                            case 5:
                                                if (!flag)
                                                {
                                                    num = 117;
                                                    continue;
                                                }
                                                goto case 35;
                                            case 108:
                                            case 127:
                                                num = 171;
                                                continue;
                                            case 85:
                                                num = 81;
                                                continue;
                                            case 81:
                                                num7 = ((text4.Trim().Length <= long.Parse(this.content_max)) ? 1 : 0);
                                                goto IL_1895;
                                            case 136:
                                                this.INFO = "WAITING";
                                                num = 122;
                                                continue;
                                            case 6:
                                                this.contentmax = "0";
                                                num = 10;
                                                continue;
                                            case 160:
                                                base.ShowTipInfo("回贴功能已关闭！【版务】→【更多栏目属性】中设置。", "wapindex.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classVo.childid);
                                                num = 15;
                                                continue;
                                            case 41:
                                                if (!flag)
                                                {
                                                    num = 42;
                                                    continue;
                                                }
                                                goto case 66;
                                            case 77:
                                                num = 51;
                                                continue;
                                            case 51:
                                                num6 = ((this.bookVo.book_re != 0) ? 1 : 0);
                                                goto IL_1100;
                                            case 117:
                                                base.ShowTipInfo("请再过:" + (num5 - num11) + "分钟才能回贴！", "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id);
                                                num = 35;
                                                continue;
                                            case 109:
                                                num = 34;
                                                continue;
                                            case 34:
                                                num10 = ((!(this.contentmax == "0")) ? 1 : 0);
                                                goto IL_1b75;
                                            case 53:
                                                this.INFO = "MAX";
                                                num = 65;
                                                continue;
                                            case 158:
                                                num = 45;
                                                continue;
                                            case 45:
                                                num = ((!(this.orderby == "1")) ? 113 : 44);
                                                continue;
                                            case 49:
                                                this.getmoney = array4[0];
                                                num = 161;
                                                continue;
                                            case 128:
                                                if (!flag)
                                                {
                                                    num = 156;
                                                    continue;
                                                }
                                                flag = !"1".Equals(this.sendmsg);
                                                num = 16;
                                                continue;
                                            case 156:
                                                //黑名单不发送消息（他的黑名单有我）
                                                isBlack = BlackTool.IsBlackUser(connStr, this.touserid, base.userid);
                                                if (!isBlack)
                                                {
                                                    //第一个回复通知
                                                    sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "第一个回复了你的贴子','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text4, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.bookVo.book_pub, ",1)");
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                }
                                                num = 93;
                                                continue;
                                            case 141:
                                                flag = this.bookVo.reShow <= 0;
                                                num = 172;
                                                continue;
                                            case 172:
                                                if (!flag)
                                                {
                                                    num = 124;
                                                    continue;
                                                }
                                                goto case 132;
                                            case 145:
                                                base.IsLogin(base.userid, "bbs/book_re.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                                                num = 29;
                                                continue;
                                            case 105:
                                                num10 = 0;
                                                goto IL_1b75;
                                            case 84:
                                                text5 = text5 + ",freeleftmoney=freeleftmoney - " + this.allMoney + " ";
                                                num = 100;
                                                continue;
                                            case 57:
                                                num = 121;
                                                continue;
                                            case 121:
                                                num9 = (WapTool.CheckUserBBSCount(base.siteid, base.userid, this.KL_CheckBBSreCount, "bbsre") ? 1 : 0);
                                                goto IL_1a84;
                                            case 129:
                                                num = ((!this.isNeedSecret) ? 119 : 112);
                                                continue;
                                            case 161:
                                                flag = !WapTool.IsNumeric(array4[1].Replace("-", ""));
                                                num = 88;
                                                continue;
                                            case 88:
                                                if (!flag)
                                                {
                                                    num = 7;
                                                    continue;
                                                }
                                                goto case 39;
                                            case 71:
                                                //黑名单不发送消息（他的黑名单有我）
                                                isBlack = BlackTool.IsBlackUser(connStr, this.touserid, base.userid);
                                                if (!isBlack)
                                                {
                                                    //回复通知楼主
                                                    sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "回复了你的贴子','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text4, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.bookVo.book_pub, ",1)");
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                }
                                                num = 37;
                                                continue;
                                            case 142:
                                                num = (flag ? 155 : 28);
                                                continue;
                                            case 52:
                                                this.allMoney = this.bookVo.reShow;
                                                num = 167;
                                                continue;
                                            case 137:
                                                num = 166;
                                                continue;
                                            case 164:
                                                text3 = "0";
                                                num = 106;
                                                continue;
                                            case 50:
                                                this.INFO = "TITLEMAX";
                                                num = 11;
                                                continue;
                                            case 168:
                                                if (!flag)
                                                {
                                                    num = 148;
                                                    continue;
                                                }
                                                flag = this.bookVo.islock != 1;
                                                num = 70;
                                                continue;
                                            case 89:
                                                flag = this.bookVo.freeLeftMoney <= long.Parse(array2[0]);
                                                num = 21;
                                                continue;
                                            case 21:
                                                if (flag)
                                                {
                                                    this.allMoney += this.bookVo.freeLeftMoney;
                                                    wap_bbsre_Model.myGetMoney = this.bookVo.freeLeftMoney;
                                                    num = 131;
                                                }
                                                else
                                                {
                                                    num = 23;
                                                }
                                                continue;
                                            case 64:
                                                num9 = 1;
                                                goto IL_1a84;
                                            case 119:
                                                num8 = 1;
                                                goto IL_1582;
                                            case 124:
                                                flag = WapTool.isHasReplyToday(base.siteid, base.userid, this.id);
                                                num = 159;
                                                continue;
                                            case 159:
                                                if (!flag)
                                                {
                                                    num = 52;
                                                    continue;
                                                }
                                                goto case 167;
                                            case 104:
                                                value = "0";
                                                num = 90;
                                                continue;
                                            case 8:
                                                this.getmoney2 = (WapTool.getArryString(base.classVo.smallimg, '|', 36) + ",").Split(',')[0];
                                                flag = text4.Trim().Length >= long.Parse(this.contentmax);
                                                num = 58;
                                                continue;
                                            case 58:
                                                num = ((!flag) ? 103 : 120);
                                                continue;
                                            case 25:
                                                if (flag)
                                                {
                                                    text5 = string.Concat(",reDate='", DateTime.Now, "'");
                                                    num = 125;
                                                }
                                                else
                                                {
                                                    num = 137;
                                                }
                                                continue;
                                            case 67:
                                                num = 157;
                                                continue;
                                            case 20:
                                                num7 = 1;
                                                goto IL_1895;
                                            case 91:
                                                flag = !(text2.Substring(text2.Length - 3, 3).ToLower() == "gif");
                                                num = 54;
                                                continue;
                                            case 54:
                                                if (!flag)
                                                {
                                                    num = 59;
                                                    continue;
                                                }
                                                goto case 169;
                                            case 70:
                                                if (flag)
                                                {
                                                    flag = this.bookVo.islock != 2;
                                                    num = 96;
                                                }
                                                else
                                                {
                                                    num = 2;
                                                }
                                                continue;
                                            case 87:
                                                if (flag)
                                                {
                                                    flag = text4.IndexOf("$(") < 0;
                                                    num = 55;
                                                }
                                                else
                                                {
                                                    num = 50;
                                                }
                                                continue;
                                            case 33:
                                                num6 = 1;
                                                goto IL_1100;
                                            case 16:
                                                if (!flag)
                                                {
                                                    num = 71;
                                                    continue;
                                                }
                                                goto case 37;
                                            case 42:
                                                this.allMoney += long.Parse(array2[num4]);
                                                wap_bbsre_Model.myGetMoney = long.Parse(array2[num4]);
                                                num = 66;
                                                continue;
                                            case 2:
                                                base.ShowTipInfo("此贴已锁！", "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + this.bookVo.book_classid + "&amp;id=" + this.bookVo.id + "&amp;lpage=" + this.lpage);
                                                num = 95;
                                                continue;
                                            case 15:
                                                flag = this.bookVo != null;
                                                num = 60;
                                                continue;
                                            case 60:
                                                if (flag)
                                                {
                                                    flag = this.bookVo.ischeck != 1;
                                                    num = 80;
                                                }
                                                else
                                                {
                                                    num = 9;
                                                }
                                                continue;
                                            case 155:
                                                num = ((this.getmoney2.IndexOf('-') == 0) ? 26 : 43);
                                                continue;
                                            case 62:
                                                flag = this.bookVo.freeLeftMoney <= 0;
                                                num = 68;
                                                continue;
                                            case 68:
                                                if (!flag)
                                                {
                                                    num = 14;
                                                    continue;
                                                }
                                                goto case 157;
                                            case 101:
                                                if (flag)
                                                {
                                                    flag = WapTool.isLockuser(base.siteid, base.userid, base.classid) <= -1;
                                                    num = 135;
                                                }
                                                else
                                                {
                                                    num = 53;
                                                }
                                                continue;
                                            case 150:
                                                num5 = Convert.ToInt64(text);
                                                flag = num5 <= 0;
                                                num = 0;
                                                continue;
                                            case 0:
                                                if (!flag)
                                                {
                                                    num = 146;
                                                    continue;
                                                }
                                                goto case 8;
                                            case 100:
                                                base.MainBll.UpdateSQL("update [wap_bbs] set book_re=book_re+1 " + text5 + " where id=" + long.Parse(this.id));
                                                num = 118;
                                                continue;
                                            case 118:
                                                num = ((!"1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 9))) ? 77 : 33);
                                                continue;
                                            case 97:
                                                if (!flag)
                                                {
                                                    num = 6;
                                                    continue;
                                                }
                                                goto case 10;
                                            case 73:
                                                this.INFO = "LOCK";
                                                num = 139;
                                                continue;
                                            case 171:
                                                new wap_bbsre_BLL(this.a).Add(wap_bbsre_Model);
                                                text5 = "";
                                                flag = this.bookVo.isdown == 2;
                                                num = 99;
                                                continue;
                                            case 99:
                                                if (!flag)
                                                {
                                                    num = 158;
                                                    continue;
                                                }
                                                goto case 133;
                                            case 170:
                                                //黑名单不发送消息（他的黑名单有我）
                                                isBlack = BlackTool.IsBlackUser(connStr, this.touserid, base.userid);
                                                if (!isBlack)
                                                {
                                                    //回复通知某一楼
                                                    sqlStr = string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", base.siteid, ",", base.userid, ",'", base.nickname, "','", base.nickname, "回复了你的回复','回复时间：", DateTime.Now, "<br/>回复内容：", WapTool.left(text4, 200), " <br/><a href=\"", base.http_start, "bbs/book_view.aspx?id=", this.bookVo.id, "&amp;siteid=", base.siteid, "&amp;classid=", this.bookVo.book_classid, "\">到帖子中查看</a>',", this.touserid, ",1)");
                                                    base.MainBll.UpdateSQL(sqlStr);
                                                }
                                                num = 149;
                                                continue;
                                            case 18:
                                            case 114:
                                                flag = num4 < array.Length;
                                                num = 22;
                                                continue;
                                            case 22:
                                                if (flag)
                                                {
                                                    flag = this.bookVo.book_re + 1 != long.Parse(array[num4]);
                                                    num = 41;
                                                }
                                                else
                                                {
                                                    num = 48;
                                                }
                                                continue;
                                            case 98:
                                                this.INFO = "REPEAT";
                                                num = 72;
                                                continue;
                                            case 74:
                                                this.getexpr = WapTool.GetSiteDefault(base.siteVo.lvlRegular, 1);
                                                flag = WapTool.IsNumeric(this.getexpr);
                                                num = 63;
                                                continue;
                                            case 63:
                                                if (!flag)
                                                {
                                                    num = 47;
                                                    continue;
                                                }
                                                goto case 144;
                                            case 69:
                                                num = ((this.KL_CheckBBSreCount != "0") ? 57 : 64);
                                                continue;
                                            case 102:
                                                flag = "1".Equals(WapTool.getArryString(base.classVo.smallimg, '|', 12));
                                                num = 154;
                                                continue;
                                            case 154:
                                                if (!flag)
                                                {
                                                    num = 145;
                                                    continue;
                                                }
                                                goto case 29;
                                            case 26:
                                                num = 116;
                                                continue;
                                            case 116:
                                                num2 = ((base.userVo.money + long.Parse(this.getmoney2) >= 0) ? 1 : 0);
                                                goto IL_0851;
                                            case 56:
                                                num = ((!flag) ? 136 : 69);
                                                continue;
                                            case 29:
                                                base.needPassWordToAdmin();
                                                num = 79;
                                                continue;
                                            case 35:
                                                num = 8;
                                                continue;
                                            case 39:
                                                this.allMoney = long.Parse(this.getmoney) + this.allMoney;
                                                base.MainBll.UpdateSQL("update [user] set [money]=([money]+" + this.allMoney + "),expR=expR+" + this.getexpr + ",bbsReCount=" + (base.userVo.bbsReCount + 1) + " where siteid=" + base.siteid + " and userid=" + base.userVo.userid);
                                                this.bookVo.book_re = this.bookVo.book_re + 1;
                                                WapTool.ClearDataBBSRe("bbsRe" + base.siteid + this.id);
                                                this.INFO = "OK";
                                                base.SaveBankLog(base.userid, "论坛回贴", this.allMoney.ToString(), base.userid, base.nickname, "回复贴子[" + this.id + "]");
                                                base.Action_user_doit(2);
                                                num = 1;
                                                continue;
                                            case 82:
                                                text = "0";
                                                num = 150;
                                                continue;
                                            case 169:
                                                num = 141;
                                                continue;
                                            case 76:
                                                this.getmoney = WapTool.GetSiteDefault(base.siteVo.moneyregular, 1);
                                                flag = WapTool.IsNumeric(this.getmoney);
                                                num = 147;
                                                continue;
                                            case 147:
                                                if (!flag)
                                                {
                                                    num = 151;
                                                    continue;
                                                }
                                                goto case 74;
                                            case 157:
                                                num = 108;
                                                continue;
                                            case 78:
                                            case 95:
                                            case 107:
                                            case 152:
                                            case 153:
                                                text4 = base.GetRequestValue("content");
                                                this.contentmax = WapTool.getArryString(base.classVo.smallimg, '|', 26);
                                                num = 134;
                                                continue;
                                            case 134:
                                                num = (WapTool.IsNumeric(this.contentmax) ? 109 : 105);
                                                continue;
                                            case 1:
                                            case 11:
                                            case 19:
                                            case 31:
                                            case 40:
                                            case 65:
                                            case 72:
                                            case 122:
                                            case 130:
                                            case 139:
                                                base.VisiteCount("回复了贴子:<a href=\"" + base.http_start + "bbs/book_view.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;id=" + this.id + "\">" + WapTool.GetShowImg(this.bookVo.book_title, "200", "bbs") + "</a>");
                                                this.showclass();
                                                num = 13;
                                                continue;
                                            case 13:
                                                goto end_IL_0066;
                                            IL_17bb:
                                                flag = (byte)num3 != 0;
                                                num = 25;
                                                continue;
                                            IL_1a84:
                                                flag = (byte)num9 != 0;
                                                num = 101;
                                                continue;
                                            IL_1b75:
                                                flag = (byte)num10 != 0;
                                                num = 97;
                                                continue;
                                            IL_1100:
                                                flag = (byte)num6 != 0;
                                                num = 128;
                                                continue;
                                            IL_1895:
                                                flag = (byte)num7 != 0;
                                                num = 87;
                                                continue;
                                            IL_0851:
                                                flag = (byte)num2 != 0;
                                                num = 165;
                                                continue;
                                            IL_1582:
                                                flag = (byte)num8 != 0;
                                                num = 142;
                                                continue;
                                        }
                                        goto IL_032b;
                                        continue;
                                    end_IL_0066:
                                        break;
                                    }
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.ERROR = WapTool.ErrorToString(ex.ToString());
                            }
                            flag = !(this.INFO == "WAITING");
                            num = 3;
                            continue;
                        case 1:
                            if (true)
                            {
                            }
                            base.VisiteCount("回复了贴子。");
                            num = 0;
                            continue;
                        case 0:
                            return;
                    }
                    break;
                }
            }
        }

        public string ShowNickName_color(long userid, string nickname)
        {
            //Discarded unreachable code: IL_0195
            int num2 = default(int);
            string result = default(string);
            while (true)
            {
                bool flag;
                flag = this.userListVo_IDName != null;
                int num;
                num = 1;
                while (true)
                {
                    int num3;
                    switch (num)
                    {
                        case 1:
                            if (!flag)
                            {
                                num = 4;
                                continue;
                            }
                            num2 = 0;
                            num = 6;
                            continue;
                        case 5:
                            num3 = 0;
                            goto IL_00b6;
                        case 6:
                        case 9:
                            num = 13;
                            continue;
                        case 13:
                            num = ((this.userListVo_IDName == null) ? 5 : 0);
                            continue;
                        case 10:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            flag = this.userListVo_IDName[num2].userid != userid;
                            num = 14;
                            continue;
                        case 4:
                            result = nickname;
                            num = 7;
                            continue;
                        case 0:
                            num = 2;
                            continue;
                        case 2:
                            num3 = ((num2 < this.userListVo_IDName.Count) ? 1 : 0);
                            goto IL_00b6;
                        case 8:
                        case 12:
                            result = nickname;
                            num = 3;
                            continue;
                        case 11:
                            nickname = WapTool.GetColorNickName(this.userListVo_IDName[num2].idname, nickname, base.lang, base.ver, this.userListVo_IDName[num2].endTime);
                            num = 8;
                            continue;
                        case 14:
                            if (true)
                            {
                            }
                            if (!flag)
                            {
                                num = 11;
                                continue;
                            }
                            num2++;
                            num = 9;
                            continue;
                        case 3:
                        case 7:
                            {
                                return result;
                            }
                        IL_00b6:
                            flag = (byte)num3 != 0;
                            num = 10;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
