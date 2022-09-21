using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class UserInfo : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string touserid = "";

        public string INFO = "";

        public string ERROR = "";

        public string backurl = "";

        public string idtype = "";

        public string myMobile = "";

        public long fans = 0L;

        public long todayZoneCount = 0L;

        public user_Model toUserVo = new user_Model();

        public string AdminClass = "";

        public string type = "";

        public string showClan = "";

        public StringBuilder strhtml = new StringBuilder();

        public List<wap_log_Model> loglistVo = null;

        public List<wap2_userGuessBook_Model> gblistVo = null;

        public List<wap_rizhi_Model> sRZlistVo = null;

        public List<wap_rizhi_Model> RZlistVo = null;

        public List<wap_album_Model> albumlistVo = null;

        public wap_friends_Model RemarkVo = null;

        public string touseridRemark = "";

        public bool isAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_0347
            IEnumerator enumerator = default(IEnumerator);
            DataSet dataSet = default(DataSet);
            bool flag = default(bool);
            wap2_visitZone_BLL wap2_visitZone_BLL = default(wap2_visitZone_BLL);
            string fcountSubMoneyFlag = default(string);
            while (true)
            {
                this.backurl = base.Request.QueryString.Get("backurl");
                int num;
                num = 29;
                while (true)
                {
                    int num3;
                    int num2;
                    switch (num)
                    {
                        case 29:
                            num = ((this.backurl == null) ? 31 : 22);
                            continue;
                        case 13:
                            enumerator = dataSet.Tables[0].Rows.GetEnumerator();
                            num = 30;
                            continue;
                        case 5:
                            num3 = 0;
                            goto IL_035f;
                        case 33:
                            this.backurl = base.Request.Form.Get("backurl");
                            num = 19;
                            continue;
                        case 28:
                            dataSet = DbHelperSQL.ExecuteDataset(WapTool._ConnStr, CommandType.Text, "select   a.id,a.clan_name from [wap_clan_list] a ,[wap_clan_user] b where a.id=b.clan_id  and b.userid=" + this.touserid);
                            flag = dataSet == null;
                            num = 6;
                            continue;
                        case 6:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            this.showClan = "<a href=\"" + base.http_start + "clan/main.aspx?siteid=" + base.siteid + "&amp;classid=0\">还没有(去看看)</a><br/>";
                            num = 3;
                            continue;
                        case 35:
                            this.RemarkVo = new wap_friends_BLL(this.a).GetModel(" siteid=" + base.siteid + " and userid=" + base.userid + " and friendtype=0 and frienduserid=" + long.Parse(this.touserid));
                            flag = this.RemarkVo == null;
                            num = 16;
                            continue;
                        case 16:
                            if (!flag)
                            {
                                num = 38;
                                continue;
                            }
                            goto case 8;
                        case 1:
                            base.MainBll.UpdateSQL("update wap2_visitZone set addtime=getdate() where userid=" + base.userid + " and touserid=" + this.touserid);
                            num = 24;
                            continue;
                        case 10:
                            if (!flag)
                            {
                                num = 34;
                                continue;
                            }
                            goto case 9;
                        case 3:
                        case 14:
                            flag = !(base.userid != "0");
                            num = 0;
                            continue;
                        case 0:
                            if (!flag)
                            {
                                num = 35;
                                continue;
                            }
                            goto case 21;
                        case 8:
                            num = 21;
                            continue;
                        case 4:
                            this.isAdmin = base.IsCheckManagerLvl("|00|01|", "");
                            this.toUserVo = new user_BLL(this.a).getUserInfo(this.touserid, base.siteid);
                            flag = this.toUserVo != null;
                            num = 27;
                            continue;
                        case 27:
                            if (!flag)
                            {
                                num = 23;
                                continue;
                            }
                            goto case 40;
                        case 22:
                            num = 2;
                            continue;
                        case 2:
                            num2 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_0a2f;
                        case 39:
                            num = 17;
                            continue;
                        case 17:
                            num3 = ((!(this.backurl == "")) ? 1 : 0);
                            goto IL_035f;
                        case 19:
                            num = 12;
                            continue;
                        case 12:
                            num = ((this.backurl != null) ? 39 : 5);
                            continue;
                        case 9:
                            this.backurl = base.ToHtm(this.backurl);
                            this.backurl = HttpUtility.UrlDecode(this.backurl);
                            this.backurl = WapTool.URLtoWAP(this.backurl);
                            this.type = WapTool.GetSiteDefault(base.siteVo.Version, 27);
                            this.touserid = base.GetRequestValue("touserid");
                            flag = WapTool.IsNumeric(this.touserid);
                            num = 18;
                            continue;
                        case 18:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            goto case 4;
                        case 30:
                            try
                            {
                                num = 0;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        default:
                                            flag = enumerator.MoveNext();
                                            num = 1;
                                            continue;
                                        case 1:
                                            if (flag)
                                            {
                                                DataRow dataRow;
                                                dataRow = (DataRow)enumerator.Current;
                                                this.showClan = this.showClan + "<a href=\"" + base.http_start + "clan/my.aspx?siteid=" + base.siteid + "&amp;classid=0&amp;action=clanInfo&amp;id=" + dataRow["id"].ToString() + "\">" + dataRow["clan_name"].ToString() + "</a><br/>";
                                                num = 3;
                                            }
                                            else
                                            {
                                                num = 4;
                                            }
                                            continue;
                                        case 4:
                                            num = 2;
                                            continue;
                                        case 2:
                                            break;
                                    }
                                    break;
                                }
                            }
                            finally
                            {
                                while (true)
                                {
                                IL_06dd:
                                    IDisposable disposable;
                                    disposable = enumerator as IDisposable;
                                    flag = disposable == null;
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
                                                goto end_IL_06c8;
                                            case 2:
                                                disposable.Dispose();
                                                num = 1;
                                                continue;
                                            case 1:
                                                goto end_IL_06c8;
                                        }
                                        goto IL_06dd;
                                    end_IL_06c8:
                                        break;
                                    }
                                    break;
                                }
                            }
                            num = 14;
                            continue;
                        case 23:
                            base.ShowTipInfo(base.GetLang("无资料记录，当前您查看的是匿名或游客！|無資料記錄，當前您查看的是匿名或游客！|No data records, may be anonymous, may be deleted!"), this.backurl);
                            num = 40;
                            continue;
                        case 40:
                            {
                                this.idtype = WapTool.GetIDName(base.siteid, this.touserid, this.toUserVo.managerlvl, base.lang);
                                this.loglistVo = new wap_log_BLL(this.a).GetListVo(3L, 1L, "  siteid=" + base.siteid + " and oper_userid=" + this.touserid + " and oper_type=1", "*", "id", 3L, 1);
                                wap_rizhi_BLL wap_rizhi_BLL;
                                wap_rizhi_BLL = new wap_rizhi_BLL(this.a);
                                this.sRZlistVo = wap_rizhi_BLL.GetListVo(3L, 1L, " ischeck=0 and ishidden=0 and book_type=0 and userid=" + base.siteid + " and makerid=" + this.touserid, "*", "id", 3L, 1);
                                this.RZlistVo = wap_rizhi_BLL.GetListVo(3L, 1L, " ischeck=0 and ishidden=0 and book_type=1 and userid=" + base.siteid + " and makerid=" + this.touserid, "*", "id", 3L, 1);
                                this.albumlistVo = new wap_album_BLL(this.a).GetListVo(4L, 1L, " ischeck=0 and ishidden=0  and userid=" + base.siteid + " and makerid=" + this.touserid, "*", "id", 4L, 1);
                                this.gblistVo = new wap2_userGuessBook_BLL(this.a).GetListVo(3L, 1L, " ischeck=0 and siteid=" + base.siteid + " and userid=" + this.touserid, "*", "id", 3L, 1);
                                this.AdminClass = WapTool.GetClassAdmin(base.http_start, base.sid, base.siteid, this.touserid);
                                this.fans = new wap_friends_BLL(this.a).GetListCount("siteid=" + base.siteid + " and friendtype=0 and frienduserid=" + this.touserid);
                                wap2_visitZone_BLL = new wap2_visitZone_BLL(this.a);
                                flag = !(base.userid != this.touserid);
                                num = 15;
                                continue;
                            }
                        case 15:
                            if (!flag)
                            {
                                num = 20;
                                continue;
                            }
                            goto case 37;
                        case 37:
                            this.todayZoneCount = wap2_visitZone_BLL.GetListCount(" touserid=" + this.touserid + " and DATEDIFF(dd, addtime, GETDATE()) < 1");
                            fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(base.siteid, base.userid, base.IP);
                            flag = fcountSubMoneyFlag.IndexOf("ZONE" + this.touserid) >= 0;
                            num = 32;
                            continue;
                        case 32:
                            if (!flag)
                            {
                                num = 26;
                                continue;
                            }
                            goto case 28;
                        case 34:
                            this.backurl = "myfile.aspx?siteid=" + base.siteid;
                            num = 9;
                            continue;
                        case 25:
                            if (!flag)
                            {
                                num = 33;
                                continue;
                            }
                            goto case 19;
                        case 7:
                            this.touserid = "0";
                            num = 4;
                            continue;
                        case 20:
                            flag = wap2_visitZone_BLL.GetListCount("userid=" + base.userid + " and touserid=" + this.touserid) <= 0;
                            num = 36;
                            continue;
                        case 36:
                            if (flag)
                            {
                                base.MainBll.UpdateSQL("insert into wap2_visitZone(siteid,userid,nickname,touserid,tonickname,addtime)values(" + base.siteid + "," + base.userid + ",'" + WapTool.left(base.nickname, 16) + "'," + this.touserid + ",'" + WapTool.left(this.toUserVo.nickname, 16) + "',getdate())");
                                num = 11;
                            }
                            else
                            {
                                num = 1;
                            }
                            continue;
                        case 31:
                            num2 = 0;
                            goto IL_0a2f;
                        case 26:
                            base.MainBll.UpdateSQL("update [user] set ZoneCount=ZoneCount+1 where userid=" + this.touserid);
                            base.MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "ZONE" + this.touserid + ",' where fip='" + base.IP + "' and fuserid=" + base.siteid + " and userid=" + base.userid);
                            num = 28;
                            continue;
                        case 11:
                        case 24:
                            num = 37;
                            continue;
                        case 38:
                            this.touseridRemark = this.RemarkVo.friendusername;
                            num = 8;
                            continue;
                        case 21:
                            {
                                base.VisiteCount("浏览<a href=\"" + base.http_start + "bbs/userinfo.aspx?siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;touserid=" + this.touserid + "\">" + WapTool.left(this.toUserVo.nickname, 20) + "空间</a>");
                                base.Action_user_doit(4);
                                return;
                            }
                        IL_035f:
                            flag = (byte)num3 != 0;
                            num = 10;
                            continue;
                        IL_0a2f:
                            flag = (byte)num2 != 0;
                            num = 25;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
