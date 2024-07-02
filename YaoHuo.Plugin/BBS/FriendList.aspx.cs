using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Web;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class FriendList : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_ADDFriendCount = PubConstant.GetAppString("KL_ADDFriendCount");

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string KelinkWAP_Check = PubConstant.GetConnectionString("kelinkWAP_Check");

        public string action = "";

        public string linkURL = "";

        public string condition = "";

        public string ERROR = "";

        public string key = "";

        public string friendtype = "";

        public string backurl = "";

        public string linkTOP = "";

        public string INFO = "";

        public List<wap_friends_Model> listVo = null;

        public long kk = 1L;

        public long index = 0L;

        public long total = 0L;

        public long pageSize = 10L;

        public long CurrentPage = 1L;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.action = base.GetRequestValue("action");
            this.backurl = base.Request.QueryString.Get("backurl");
            if (this.backurl == null || this.backurl == "")
            {
                this.backurl = base.Request.Form.Get("backurl");
            }
            if (this.backurl == null || this.backurl == "")
            {
                this.backurl = "myfile.aspx?siteid=" + base.siteid;
            }
            this.backurl = base.ToHtm(this.backurl);
            this.backurl = HttpUtility.UrlDecode(this.backurl);
            this.backurl = WapTool.URLtoWAP(this.backurl);
            this.friendtype = base.GetRequestValue("friendtype");
            if (!WapTool.IsNumeric(this.friendtype))
            {
                this.friendtype = "0";
            }
            base.IsLogin(base.userid, this.backurl);
            switch (this.action)
            {
                case "addfriend":
                    this.goaddfriend();
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
            this.key = base.GetRequestValue("key");
            this.condition = " siteid= " + base.siteid;
            this.condition = this.condition + " and  userid=" + base.userid + " and friendtype=" + this.friendtype;
            if (this.key.Trim() != "")
            {
                this.condition = this.condition + " and  frienduserid = " + this.key + " ";
            }
            if (this.friendtype == "4")
            {
                this.condition = " siteid= " + base.siteid + " and frienduserid=" + base.userid;
                if (this.key.Trim() != "")
                {
                    this.condition = this.condition + " and  userid = " + this.key + " ";
                }
            }
            try
            {
                this.pageSize = Convert.ToInt32(base.siteVo.MaxPerPage_Default);
                wap_friends_BLL wap_friends_BLL;
                wap_friends_BLL = new wap_friends_BLL(this.a);
                if (base.GetRequestValue("getTotal") != "")
                {
                    this.total = long.Parse(base.GetRequestValue("getTotal"));
                }
                else if (this.friendtype == "4")
                {
                    this.total = wap_friends_BLL.GetListCount2("select count(id) as n from wap_friends_VIEW where " + this.condition);
                }
                else
                {
                    this.total = wap_friends_BLL.GetListCount(this.condition);
                }
                if (base.GetRequestValue("page") != "")
                {
                    this.CurrentPage = long.Parse(base.GetRequestValue("page"));
                }
                this.CurrentPage = WapTool.CheckCurrpage(this.total, this.pageSize, this.CurrentPage);
                this.index = this.pageSize * (this.CurrentPage - 1L);
                this.linkURL = base.http_start + "bbs/FriendList.aspx?action=class&amp;siteid=" + base.siteid + "&amp;classid=" + base.classid + "&amp;friendtype=" + this.friendtype + "&amp;key=" + HttpUtility.UrlEncode(this.key) + "&amp;backurl=" + HttpUtility.UrlEncode(this.backurl) + "&amp;getTotal=" + this.total;
                this.linkTOP = WapTool.GetPageLinkShowTOP(base.ver, base.lang, this.total, this.pageSize, this.CurrentPage, this.linkURL);
                this.linkURL = WapTool.GetPageLink(base.ver, base.lang, Convert.ToInt32(this.total), this.pageSize, this.CurrentPage, this.linkURL);
                if (this.friendtype == "4")
                {
                    this.listVo = wap_friends_BLL.GetListVo2(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 1L);
                }
                else
                {
                    this.listVo = wap_friends_BLL.GetListVo(this.pageSize, this.CurrentPage, this.condition, "*", "id", this.total, 1L);
                }
            }
            catch (Exception ex)
            {
                this.ERROR = ex.ToString();
            }
        }

        public void goaddfriend()
        {
            string requestValue = base.GetRequestValue("touserid");
            user_Model user_Model = new user_BLL(this.a).getUserInfo(requestValue, base.siteid);
            if (!WapTool.IsNumeric(this.KL_ADDFriendCount))
            {
                this.KL_ADDFriendCount = "10";
            }
            long listCount;
            listCount = new wap_friends_BLL(this.a).GetListCount(" (DATEDIFF(dd, addtime, GETDATE()) < 1) and friendtype=0 and siteid=" + long.Parse(base.siteid) + " and userid=" + long.Parse(base.userid));
            if (user_Model == null)
            {
                this.INFO = "NOTUSER";
            }
            else if (listCount > long.Parse(this.KL_ADDFriendCount))
            {
                this.INFO = "MAX";
            }
            else if (base.userid == requestValue)
            {
                this.INFO = "MY";
            }
            else if (WapTool.isLockuser(base.siteid, base.userid, "0") > -1L)
            {
                this.INFO = "LOCK";
            }
            else if (WapTool.isExistFriend(base.siteid, base.userid, requestValue, this.friendtype))
            {
                this.INFO = "HASEXIST";
            }
            else
            {
                string title = "";
                if (this.friendtype == "0")
                {
                    title = "TA的好友|TA的好友|his friend";
                }
                else if (this.friendtype == "1")
                {
                    title = "TA的黑名单|TA的黑名单|black";
                }
                else if (this.friendtype == "2")
                {
                    title = "TA的追求|TA的追求|his love";
                }
                string text = user_Model.nickname;
                //限制黑名单上限
                if (friendtype == "1")
                {
                    //查询用户信息
                    var userInfo = MainBll.getUserInfo(userid, siteid);
                    var isResult = BlackTool.AddBlackUser(userInfo, KelinkWAP_Check, requestValue);
                    if (!string.IsNullOrEmpty(isResult))
                    {
                        INFO = isResult;
                        this.showclass();
                        return;
                    }
                }
                base.MainBll.UpdateSQL("insert into wap_friends(siteid,userid,frienduserid,friendusername,friendnickname,friendtype)values(" + base.siteid + "," + base.userid + "," + requestValue + ",'','" + text + "'," + this.friendtype + ")");
                if (this.friendtype != "1")
                {
                    string text2 = base.nickname + base.GetLang("将你加入|将你加入|to") + base.GetLang(title);
                    string text3 = base.GetLang("操作时间|操作时间|Operation Time") + ":" + DateTime.Now;
                    string text4 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                    string strSQL = text4 + "  values(" + base.siteid + "," + base.userid + ",'" + base.nickname + "','" + text2 + "','" + text3 + "'," + requestValue + ",1)";
                    base.MainBll.UpdateSQL(strSQL);
                }
                this.INFO = "OK";
                base.Action_user_doit(6);
            }
            this.showclass();
        }
    }
}
