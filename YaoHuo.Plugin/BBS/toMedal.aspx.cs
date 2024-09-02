using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class toMedal : MyPageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string action = "";

        public string INFO = "";

        public string ERROR = "";

        public string touserid = "";

        public string smedal = "";

        public string remark = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            IsCheckUserManager(userid, userVo.managerlvl, "", "admin/basesitemodifywml.aspx?siteid=" + siteid);
            action = GetRequestValue("action");
            if (action == "search")
            {
                touserid = GetRequestValue("touserid");
                if (!WapTool.IsNumeric(touserid))
                {
                    touserid = "0";
                }
                user_BLL user_BLL = new user_BLL(a);
                user_Model userInfo = user_BLL.getUserInfo(touserid, siteid);
                if (userInfo != null)
                {
                    smedal = userInfo.moneyname;
                }
            }
            if (!(action == "gomod"))
            {
                return;
            }
            smedal = GetRequestValue("smedal");
            touserid = GetRequestValue("touserid");
            remark = GetRequestValue("remark");
            try
            {
                if (WapTool.isExistUser(siteid, touserid))
                {
                    MainBll.UpdateSQL("update [user] set moneyname='" + smedal + "' where  userid=" + touserid);
                    INFO = "OK";
                    string text = "恭喜您，" + userVo.nickname + "奖励勋章给您！";
                    string text2 = "原因:" + remark;
                    string text3 = "insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)";
                    text3 = text3 + "  values(" + siteid + "," + userid + ",'" + userVo.nickname + "','" + text + "','" + text2 + "'," + touserid + ",1)";
                    MainBll.UpdateSQL(text3);
                }
                else
                {
                    INFO = "NOTEXIST";
                }
            }
            catch (Exception ex)
            {
                ERROR = WapTool.ErrorToString(ex.ToString());
            }
        }
    }
}