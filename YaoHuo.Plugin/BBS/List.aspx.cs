using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

public class List : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_01e0
        string text4 = default(string);
        string text5 = default(string);
        string text3 = default(string);
        string text6 = default(string);
        string text2 = default(string);
        while (true)
        {
            string text = base.Request.QueryString.Get("classid");
            bool flag = WapTool.IsNumeric(text);
            int num = 24;
            while (true)
            {
                int num6;
                int num5;
                int num4;
                int num3;
                int num2;
                switch (num)
                {
                    case 24:
                        if (!flag)
                        {
                            num = 10;
                            continue;
                        }
                        goto case 5;
                    case 33:
                        num = 1;
                        continue;
                    case 1:
                        num6 = ((!(text4 != "")) ? 1 : 0);
                        goto IL_01db;
                    case 32:
                        num = 9;
                        continue;
                    case 9:
                        num5 = ((!(text5 != "")) ? 1 : 0);
                        goto IL_02a5;
                    case 35:
                        num4 = 1;
                        goto IL_03eb;
                    case 6:
                        num3 = 1;
                        goto IL_0347;
                    case 16:
                        text4 = "&action=" + text4;
                        num = 30;
                        continue;
                    case 5:
                        text3 = base.Request.QueryString.Get("page");
                        num = 23;
                        continue;
                    case 23:
                        num = ((text3 == null) ? 36 : 18);
                        continue;
                    case 17:
                        num5 = 1;
                        goto IL_02a5;
                    case 2:
                        num6 = 1;
                        goto IL_01db;
                    case 22:
                        if (!flag)
                        {
                            num = 16;
                            continue;
                        }
                        goto case 30;
                    case 0:
                        text6 = "&stype=" + text6;
                        num = 27;
                        continue;
                    case 8:
                        num = 28;
                        continue;
                    case 28:
                        num3 = ((!(text2 != "")) ? 1 : 0);
                        goto IL_0347;
                    case 10:
                        text = "0";
                        num = 5;
                        continue;
                    case 36:
                        num2 = 1;
                        goto IL_027b;
                    case 26:
                        if (!flag)
                        {
                            num = 25;
                            continue;
                        }
                        goto case 4;
                    case 37:
                        if (!flag)
                        {
                            num = 14;
                            continue;
                        }
                        goto case 7;
                    case 7:
                        text6 = base.Request.QueryString.Get("stype");
                        num = 15;
                        continue;
                    case 15:
                        num = ((text6 != null) ? 21 : 35);
                        continue;
                    case 34:
                        text5 = base.Request.QueryString.Get("sitename");
                        num = 19;
                        continue;
                    case 19:
                        num = ((text5 != null) ? 32 : 17);
                        continue;
                    case 29:
                        if (!flag)
                        {
                            num = 12;
                            continue;
                        }
                        goto case 34;
                    case 21:
                        num = 13;
                        continue;
                    case 13:
                        num4 = ((!(text6 != "")) ? 1 : 0);
                        goto IL_03eb;
                    case 25:
                        text3 = "&page=" + text3;
                        num = 4;
                        continue;
                    case 30:
                        text2 = base.Request.QueryString.Get("siteurl");
                        num = 31;
                        continue;
                    case 31:
                        num = ((text2 != null) ? 8 : 6);
                        continue;
                    case 11:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        goto case 27;
                    case 14:
                        text5 = "&sitename=" + text5;
                        num = 7;
                        continue;
                    case 4:
                        text4 = base.Request.QueryString.Get("action");
                        num = 3;
                        continue;
                    case 3:
                        num = ((text4 != null) ? 33 : 2);
                        continue;
                    case 18:
                        num = 20;
                        continue;
                    case 20:
                        num2 = ((!(text3 != "")) ? 1 : 0);
                        goto IL_027b;
                    case 12:
                        text2 = "&siteurl=" + text2;
                        num = 34;
                        continue;
                    case 27:
                        {
                            string classToSiteid = WapTool.GetClassToSiteid(long.Parse(text));
                            base.Server.Transfer("/bbs/book_list.aspx?siteid=" + classToSiteid + "&classid=" + text + text3 + text4 + text2 + text5 + text6);
                            return;
                        }
                    IL_0347:
                        flag = (byte)num3 != 0;
                        num = 29;
                        continue;
                    IL_03eb:
                        flag = (byte)num4 != 0;
                        num = 11;
                        continue;
                    IL_02a5:
                        flag = (byte)num5 != 0;
                        num = 37;
                        continue;
                    IL_01db:
                        flag = (byte)num6 != 0;
                        if (true)
                        {
                        }
                        num = 22;
                        continue;
                    IL_027b:
                        flag = (byte)num2 != 0;
                        num = 26;
                        continue;
                }
                break;
            }
        }
    }
}
