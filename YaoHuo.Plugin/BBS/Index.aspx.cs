using KeLin.ClassManager;
using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

public class Index : Page
{
    public string KL_HiddenQuery = PubConstant.GetAppString("KL_HiddenQuery");

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_0156
        while (true)
        {
            string text = base.Request.QueryString.Get("siteid");
            string text2 = base.Request.QueryString.Get("classid");
            string text3 = base.Request.QueryString.Get("sid");
            string text4 = base.Request.QueryString.Get("action");
            string text5 = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/";
            bool flag = !(text4 == "webAdmin");
            int num = 10;
            while (true)
            {
                switch (num)
                {
                    case 10:
                        if (!flag)
                        {
                            num = 16;
                            continue;
                        }
                        flag = !(text4 == "webAdmin00");
                        num = 8;
                        continue;
                    case 11:
                        if (!flag)
                        {
                            num = 5;
                            continue;
                        }
                        base.Response.Redirect(text5 + "bbs/list.aspx?classid=" + text2);
                        num = 13;
                        continue;
                    case 9:
                        if (flag)
                        {
                            flag = !(WapTool.ISAPI_Rewrite3_Open == "1");
                            num = 11;
                        }
                        else
                        {
                            num = 6;
                        }
                        continue;
                    case 8:
                        if (!flag)
                        {
                            num = 14;
                            continue;
                        }
                        flag = !(text4 == "wapAdmin");
                        num = 15;
                        continue;
                    case 16:
                        base.Response.Redirect("userlist.aspx?classid=" + text2);
                        num = 7;
                        continue;
                    case 7:
                        return;

                    case 5:
                        base.Response.Redirect(text5 + "bbslist-" + text2 + ".html");
                        num = 2;
                        continue;
                    case 2:
                    case 13:
                        num = 3;
                        continue;
                    case 3:
                        return;

                    case 12:
                        base.Response.Redirect(text5 + "bbs/admin_userlistWAP.aspx?siteid=" + text + "&classid=" + text2);
                        num = 4;
                        continue;
                    case 4:
                        return;

                    case 14:
                        base.Response.Redirect("admin_userlist.aspx?classid=" + text2);
                        num = 1;
                        continue;
                    case 1:
                        return;

                    case 6:
                        base.Response.Redirect(text5 + "bbs/admin_userlistWAP00.aspx?siteid=" + text + "&classid=" + text2);
                        num = 0;
                        continue;
                    case 0:
                        return;

                    case 15:
                        if (flag)
                        {
                            flag = !(text4 == "wapAdmin00");
                            if (true)
                            {
                            }
                            num = 9;
                        }
                        else
                        {
                            num = 12;
                        }
                        continue;
                }
                break;
            }
        }
    }
}
