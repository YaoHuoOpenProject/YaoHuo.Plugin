using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class UserInfo : MyPageWap
    {
        private string string_10 = PubConstant.GetAppString("InstanceName");

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
            type = WapTool.GetSiteDefault(siteVo.Version, 27);
            touserid = GetRequestValue("touserid");
            if (!WapTool.IsNumeric(touserid))
            {
                touserid = "0";
            }
            isAdmin = IsCheckManagerLvl("|00|01|", "");
            user_BLL user_BLL = new user_BLL(string_10);
            toUserVo = user_BLL.getUserInfo(touserid, siteid);
            if (toUserVo == null)
            {
                ShowTipInfo(GetLang("无资料记录，当前您查看的是匿名或游客！|無資料記錄，當前您查看的是匿名或游客！|No data records, may be anonymous, may be deleted!"), backurl);
            }
            idtype = WapTool.GetIDName(siteid, touserid, toUserVo.managerlvl, lang);
            wap_log_BLL wap_log_BLL = new wap_log_BLL(string_10);
            loglistVo = wap_log_BLL.GetListVo(3L, 1L, "  siteid=" + siteid + " and oper_userid=" + touserid + " and oper_type=1", "*", "id", 3L, 1);
            wap_rizhi_BLL wap_rizhi_BLL = new wap_rizhi_BLL(string_10);
            sRZlistVo = wap_rizhi_BLL.GetListVo(3L, 1L, " ischeck=0 and ishidden=0 and book_type=0 and userid=" + siteid + " and makerid=" + touserid, "*", "id", 3L, 1);
            RZlistVo = wap_rizhi_BLL.GetListVo(3L, 1L, " ischeck=0 and ishidden=0 and book_type=1 and userid=" + siteid + " and makerid=" + touserid, "*", "id", 3L, 1);
            wap_album_BLL wap_album_BLL = new wap_album_BLL(string_10);
            albumlistVo = wap_album_BLL.GetListVo(4L, 1L, " ischeck=0 and ishidden=0  and userid=" + siteid + " and makerid=" + touserid, "*", "id", 4L, 1);
            wap2_userGuessBook_BLL wap2_userGuessBook_BLL = new wap2_userGuessBook_BLL(string_10);
            gblistVo = wap2_userGuessBook_BLL.GetListVo(3L, 1L, " ischeck=0 and siteid=" + siteid + " and userid=" + touserid, "*", "id", 3L, 1);
            AdminClass = WapTool.GetClassAdmin(http_start, sid, siteid, touserid);
            wap_friends_BLL wap_friends_BLL = new wap_friends_BLL(string_10);
            fans = wap_friends_BLL.GetListCount("siteid=" + siteid + " and friendtype=0 and frienduserid=" + touserid);
            wap2_visitZone_BLL wap2_visitZone_BLL = new wap2_visitZone_BLL(string_10);
            if (userid != touserid)
            {
                int listCount = wap2_visitZone_BLL.GetListCount("userid=" + userid + " and touserid=" + touserid);
                if (listCount > 0)
                {
                    MainBll.UpdateSQL("update wap2_visitZone set addtime=getdate() where userid=" + userid + " and touserid=" + touserid);
                }
                else
                {
                    MainBll.UpdateSQL("insert into wap2_visitZone(siteid,userid,nickname,touserid,tonickname,addtime)values(" + siteid + "," + userid + ",'" + WapTool.left(nickname, 16) + "'," + touserid + ",'" + WapTool.left(toUserVo.nickname, 16) + "',getdate())");
                }
            }
            todayZoneCount = wap2_visitZone_BLL.GetListCount(" touserid=" + touserid + " and DATEDIFF(dd, addtime, GETDATE()) < 1");
            string fcountSubMoneyFlag = WapTool.getFcountSubMoneyFlag(siteid, userid, IP);
            if (fcountSubMoneyFlag.IndexOf("ZONE" + touserid) < 0)
            {
                MainBll.UpdateSQL("update [user] set ZoneCount=ZoneCount+1 where userid=" + touserid);
                MainBll.UpdateSQL("update [fcount] set SubMoneyFlag='" + fcountSubMoneyFlag + "ZONE" + touserid + ",' where fip='" + IP + "' and fuserid=" + siteid + " and userid=" + userid);
            }
            DataSet dataSet = DbHelperSQL.ExecuteDataset(WapTool._ConnStr, CommandType.Text, "select   a.id,a.clan_name from [wap_clan_list] a ,[wap_clan_user] b where a.id=b.clan_id  and b.userid=" + touserid);
            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    showClan = showClan + "<a href=\"" + http_start + "clan/my.aspx?siteid=" + siteid + "&amp;classid=0&amp;action=clanInfo&amp;id=" + row["id"].ToString() + "\">" + row["clan_name"].ToString() + "</a><br/>";
                }
            }
            else
            {
                showClan = "<a href=\"" + http_start + "clan/main.aspx?siteid=" + siteid + "&amp;classid=0\">还没有(去看看)</a><br/>";
            }
            if (userid != "0")
            {
                wap_friends_BLL wap_friends_BLL2 = new wap_friends_BLL(string_10);
                RemarkVo = wap_friends_BLL2.GetModel(" siteid=" + siteid + " and userid=" + userid + " and friendtype=0 and frienduserid=" + long.Parse(touserid));
                if (RemarkVo != null)
                {
                    touseridRemark = RemarkVo.friendusername;
                }
            }
            VisiteCount("浏览<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "\">" + WapTool.left(toUserVo.nickname, 20) + "空间</a>");
            Action_user_doit(4);
        }
    }
}