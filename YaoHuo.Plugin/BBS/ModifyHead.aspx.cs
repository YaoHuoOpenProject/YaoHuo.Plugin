using KeLin.ClassManager;
using System;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class ModifyHead : MyPageWap
    {
    private string string_10 = PubConstant.GetAppString("InstanceName");

    public string txtPW = "";

    public string tohead = "";

    public string sysimg = "";

    public string toheadimg = "";

    public string INFO = "";

    public string ERROR = "";

    public string tonickname = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        IsLogin(userid, "bbs/modifyuserinfo.aspx?siteid=" + siteid);
        needPassWordToAdmin();
        string text = base.Request.Form.Get("action");
        sysimg = GetRequestValue("sysimg");
        toheadimg = userVo.headimg;
        if (text == "gomod")
        {
            toheadimg = GetRequestValue("toheadimg");
            if (toheadimg == "")
            {
                toheadimg = sysimg + ".gif";
            }
            MainBll.UpdateSQL("update [user] set headimg='" + toheadimg + "' where siteid=" + siteid + " and userid=" + userid);
            INFO = "OK";
        }
    }
}
}