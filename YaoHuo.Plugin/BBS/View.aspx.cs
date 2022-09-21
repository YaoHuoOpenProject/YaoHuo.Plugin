using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using System;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class View : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_005b
            wap_bbs_Model model = default(wap_bbs_Model);
            string text2 = default(string);
            string text3 = default(string);
            while (true)
            {
                string text = base.Request.QueryString.Get("id");
                bool flag = WapTool.IsNumeric(text);
                int num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 2:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 3;
                        case 6:
                            base.Server.Transfer("/bbs/book_view.aspx?siteid=" + model.userid + "&classid=" + model.book_classid + "&id=" + text + "&lpage=" + text2 + "&stype=" + text3);
                            num = 4;
                            continue;
                        case 4:
                            return;

                        case 3:
                            {
                                text2 = base.Request.QueryString.Get("lpage");
                                text3 = base.Request.QueryString.Get("stype");
                                wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(WapTool._InstanceName);
                                model = wap_bbs_BLL.GetModel(long.Parse(text));
                                flag = model == null;
                                num = 5;
                                continue;
                            }
                        case 5:
                            if (!flag)
                            {
                                num = 6;
                                continue;
                            }
                            base.Response.Write("<html><title>提示</title><body>抱歉，找不到ID=" + text + "此记录！<br/><br/><a href=\"javascript:;\" onClick=\"javascript:history.back(-1);\">返回上级</a>-<a href=\"/\">返回首页</a></body></html>");
                            num = 0;
                            continue;
                        case 1:
                            text = "0";
                            num = 3;
                            continue;
                        case 0:
                            return;
                    }
                    break;
                }
            }
        }
    }
}
