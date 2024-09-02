using KeLin.ClassManager;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyPW : MyPageWap
    {

    private string a = PubConstant.GetAppString("InstanceName");

    public string INFO = "";

    public string ERROR = "";

    public string oldpassword = "";

    public string newpassword = "";

    public string newrepassword = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string text = base.Request.Form.Get("action");
        IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
        //needPassWordToAdmin();
        if (text == "gomod")
        {
            oldpassword = GetRequestValue("txtoldPW");
            newpassword = GetRequestValue("txtnewPW");
            newrepassword = GetRequestValue("txtrePW");
            if (newpassword.Trim() == "")
            {
                INFO = "NULL";
                return;
            }
            if (newpassword.Trim() != newrepassword.Trim())
            {
                INFO = "TWOERR";
                return;
            }
            if (userVo.password != oldpassword && userVo.password.ToLower() != PubConstant.md5(oldpassword).ToLower())
            {
                INFO = "OLDERR";
                return;
            }
            MainBll.UpdateSQL("update [user] set password='" + PubConstant.md5(newpassword) + "',sidtimeout=null where siteid=" + siteid + " and userid=" + userid);
            INFO = "OK";
        }
    }
}
}