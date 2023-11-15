using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using System.Collections.Generic;
using System.Text;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
    public class book_view_modfile : PageWap
    {
        private string a = PubConstant.GetAppString("InstanceName");

        public string KL_NotDownAndUpload = PubConstant.GetAppString("KL_NotDownAndUpload");

        public wap_bbs_Model bbsVo = new wap_bbs_Model();

        public List<wap2_attachment_Model> imgList = new List<wap2_attachment_Model>();

        public string action = "";

        public string page = "";

        public string INFO = "";

        public string ERROR = "";

        public string book_title = "";

        public string book_content = "";

        public string book_imgTrue = "";

        public string updateInfo = "";

        public string softMoney = "";

        public string softSafe = "";

        public string money = "";

        public string lpage = "";

        public string id = "";

        public long getid;

        public int num = 1;

        public StringBuilder book_img = new StringBuilder();

        public StringBuilder band = new StringBuilder();

        public StringBuilder platform = new StringBuilder();

        public StringBuilder screen = new StringBuilder();

        public StringBuilder serial = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Discarded unreachable code: IL_02cd
            bool flag = default(bool);
            wap_bbs_BLL wap_bbs_BLL = default(wap_bbs_BLL);
            int num3 = default(int);
            wap2_attachment_BLL wap2_attachment_BLL = default(wap2_attachment_BLL);
            while (true)
            {
                int num = 1;
                while (true)
                {
                    int num6;
                    switch (num)
                    {
                        case 1:
                            num = ((!(classid != "0")) ? 20 : 13);
                            continue;
                        case 23:
                            if (!flag)
                            {
                                num = 2;
                                continue;
                            }
                            flag = !(bbsVo.book_classid.ToString() != classid);
                            num = 14;
                            continue;
                        case 30:
                            ShowTipInfo("此贴已结！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
                            num = 33;
                            continue;
                        case 13:
                            num = 5;
                            continue;
                        case 5:
                            num6 = ((!(classVo.typePath.ToLower() != "bbs/index.aspx")) ? 1 : 0);
                            goto IL_0b16;
                        case 28:
                            if (!flag)
                            {
                                num = 30;
                                continue;
                            }
                            goto case 9;
                        case 14:
                            if (!flag)
                            {
                                num = 7;
                                continue;
                            }
                            flag = bbsVo.islock != 1;
                            num = 27;
                            continue;
                        case 7:
                            ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
                            num = 15;
                            continue;
                        case 0:
                            try
                            {
                                while (true)
                                {
                                IL_03a5:
                                    string[] values = base.Request.Form.GetValues("book_id");
                                    string[] values2 = base.Request.Form.GetValues("book_file_title");
                                    string[] values3 = base.Request.Form.GetValues("book_ext");
                                    string[] values4 = base.Request.Form.GetValues("book_size");
                                    string[] values5 = base.Request.Form.GetValues("book_file_info");
                                    string[] values6 = base.Request.Form.GetValues("book_click");
                                    int num2 = 0;
                                    num = 27;
                                    while (true)
                                    {
                                        int num4;
                                        int num5;
                                        switch (num)
                                        {
                                            case 32:
                                                imgList[num2].book_title = imgList[num2].book_title.Replace("[", "［").Replace("］", "]");
                                                checkGo(bbsVo.viewtype.ToString(), imgList[num2].book_content);
                                                num = 6;
                                                continue;
                                            case 30:
                                                {
                                                    if (!flag)
                                                    {
                                                        num = 3;
                                                        continue;
                                                    }
                                                    string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")修改附件" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
                                                    bbsVo.whylock = text + bbsVo.whylock;
                                                    bbsVo.isdown = 1L;
                                                    wap_bbs_BLL.Update(bbsVo);
                                                    num2 = 0;
                                                    num = 15;
                                                    continue;
                                                }
                                            case 24:
                                                num4 = 0;
                                                goto IL_0850;
                                            case 2:
                                                num5 = 0;
                                                goto IL_078c;
                                            case 1:
                                                num2++;
                                                num = 19;
                                                continue;
                                            case 23:
                                                if (!flag)
                                                {
                                                    num = 20;
                                                    continue;
                                                }
                                                goto case 6;
                                            case 3:
                                                INFO = "LOCK";
                                                num = 9;
                                                continue;
                                            case 6:
                                                num3++;
                                                num = 4;
                                                continue;
                                            case 4:
                                            case 25:
                                                flag = num3 < values.Length;
                                                num = 33;
                                                continue;
                                            case 33:
                                                if (flag)
                                                {
                                                    flag = !(imgList[num2].ID.ToString() == values[num3]);
                                                    num = 23;
                                                }
                                                else
                                                {
                                                    num = 1;
                                                }
                                                continue;
                                            case 7:
                                                INFO = "OK";
                                                num = 8;
                                                continue;
                                            case 28:
                                                num = 12;
                                                continue;
                                            case 12:
                                                num5 = ((num2 < imgList.Count) ? 1 : 0);
                                                goto IL_078c;
                                            case 0:
                                                try
                                                {
                                                    imgList[num2].book_click = long.Parse(values6[num3]);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                                flag = !(imgList[num2].book_title.Trim() == "");
                                                num = 34;
                                                continue;
                                            case 15:
                                            case 17:
                                                num = 21;
                                                continue;
                                            case 21:
                                                num = ((imgList != null) ? 14 : 24);
                                                continue;
                                            case 10:
                                                num = 29;
                                                continue;
                                            case 22:
                                                if (flag)
                                                {
                                                    num3 = 0;
                                                    num = 25;
                                                }
                                                else
                                                {
                                                    num = 31;
                                                }
                                                continue;
                                            case 14:
                                                num = 18;
                                                continue;
                                            case 18:
                                                num4 = ((num2 < imgList.Count) ? 1 : 0);
                                                goto IL_0850;
                                            case 34:
                                                if (!flag)
                                                {
                                                    num = 11;
                                                    continue;
                                                }
                                                goto case 32;
                                            case 19:
                                            case 27:
                                                num = 26;
                                                continue;
                                            case 26:
                                                num = ((imgList != null) ? 28 : 2);
                                                continue;
                                            case 13:
                                                if (!flag)
                                                {
                                                    num = 7;
                                                    continue;
                                                }
                                                wap2_attachment_BLL.Update(imgList[num2]);
                                                num2++;
                                                num = 17;
                                                continue;
                                            case 11:
                                                INFO = "NULL";
                                                num = 32;
                                                continue;
                                            case 20:
                                                imgList[num2].book_content = ToHtm(values5[num3]);
                                                imgList[num2].book_title = ToHtm(values2[num3]);
                                                imgList[num2].book_ext = ToHtm(values3[num3]);
                                                imgList[num2].book_size = values4[num3];
                                                num = 0;
                                                continue;
                                            case 31:
                                                flag = !(INFO != "");
                                                num = 5;
                                                continue;
                                            case 5:
                                                if (flag)
                                                {
                                                    flag = WapTool.isLockuser(siteid, userid, classid) <= -1;
                                                    num = 30;
                                                }
                                                else
                                                {
                                                    num = 10;
                                                }
                                                continue;
                                            case 8:
                                            case 9:
                                            case 29:
                                                num = 16;
                                                continue;
                                            case 16:
                                                goto end_IL_0310;
                                            IL_0850:
                                                flag = (byte)num4 != 0;
                                                num = 13;
                                                continue;
                                            IL_078c:
                                                flag = (byte)num5 != 0;
                                                num = 22;
                                                continue;
                                        }
                                        goto IL_03a5;
                                    end_IL_0310:
                                        break;
                                    }
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                ERROR = WapTool.ErrorToString(ex.ToString());
                            }
                            num = 34;
                            continue;
                        case 25:
                            action = GetRequestValue("action");
                            page = GetRequestValue("page");
                            id = GetRequestValue("id");
                            lpage = GetRequestValue("lpage");
                            wap_bbs_BLL = new wap_bbs_BLL(a);
                            bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
                            flag = bbsVo != null;
                            num = 6;
                            continue;
                        case 6:
                            if (flag)
                            {
                                flag = bbsVo.ischeck != 1;
                                num = 23;
                            }
                            else
                            {
                                num = 3;
                            }
                            continue;
                        case 20:
                            num6 = 1;
                            goto IL_0b16;
                        case 2:
                            ShowTipInfo("正在审核中！", "");
                            num = 29;
                            continue;
                        case 3:
                            ShowTipInfo("已删除！或不存在！", "");
                            num = 17;
                            continue;
                        case 16:
                        case 24:
                            wap2_attachment_BLL = new wap2_attachment_BLL(a);
                            imgList = wap2_attachment_BLL.GetListVo(" book_type='bbs' and  book_id=" + id);
                            flag = !(action == "gomod");
                            num = 4;
                            continue;
                        case 4:
                            if (!flag)
                            {
                                num = 22;
                                continue;
                            }
                            return;
                        case 35:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            goto case 25;
                        case 27:
                            if (flag)
                            {
                                flag = bbsVo.islock != 2;
                                num = 28;
                            }
                            else
                            {
                                num = 8;
                            }
                            continue;
                        case 32:
                            base.Response.End();
                            num = 18;
                            continue;
                        case 8:
                            ShowTipInfo("此贴已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
                            num = 9;
                            continue;
                        case 22:
                            num = 0;
                            continue;
                        case 10:
                            CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
                            num = 24;
                            continue;
                        case 18:
                            flag = !(userid != bbsVo.book_pub.ToString());
                            num = 19;
                            continue;
                        case 19:
                            if (flag)
                            {
                                IsLogin(userid, "bbs/book_view_mod.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
                                //needPassWordToAdmin();
                                if (true)
                                {
                                }
                                num = 16;
                            }
                            else
                            {
                                num = 10;
                            }
                            continue;
                        case 34:
                            return;
                        case 12:
                            ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
                            num = 25;
                            continue;
                        case 31:
                            base.Response.End();
                            num = 11;
                            continue;
                        case 9:
                        case 15:
                        case 17:
                        case 29:
                        case 33:
                            bbsVo.book_title = WapTool.GetShowImg(bbsVo.book_title, "200", "bbs");
                            flag = !(bbsVo.userid.ToString() != siteid);
                            num = 26;
                            continue;
                        case 26:
                            if (!flag)
                            {
                                num = 31;
                                continue;
                            }
                            goto case 11;
                        case 11:
                            flag = !(bbsVo.book_classid.ToString() != classid);
                            num = 21;
                            continue;
                        case 21:
                            {
                                if (!flag)
                                {
                                    num = 32;
                                    continue;
                                }
                                goto case 18;
                            }
                        IL_0b16:
                            flag = (byte)num6 != 0;
                            num = 35;
                            continue;
                    }
                    break;
                }
            }
        }

        public void checkGo(string viewtype, string content)
        {
            //Discarded unreachable code: IL_014a
            bool flag2 = default(bool);
            while (true)
            {
                string text = WapTool.getArryString(classVo.smallimg, '|', 21);
                bool flag = !(text != "");
                int num = 19;
                while (true)
                {
                    int num2;
                    int num3;
                    switch (num)
                    {
                        case 19:
                            if (!flag)
                            {
                                num = 25;
                                continue;
                            }
                            return;
                        case 16:
                            num = 20;
                            continue;
                        case 1:
                            flag = text.IndexOf("_" + userVo.SessionTimeout + "_") >= 0;
                            num = 12;
                            continue;
                        case 12:
                            if (!flag)
                            {
                                num = 15;
                                continue;
                            }
                            goto case 16;
                        case 21:
                            num2 = 0;
                            goto IL_0138;
                        case 2:
                            if (true)
                            {
                            }
                            if (!flag)
                            {
                                num = 23;
                                continue;
                            }
                            goto case 17;
                        case 18:
                            num = 0;
                            continue;
                        case 0:
                            num3 = (IsCheckManagerLvl("|00|01|03|04|", classVo.adminusername) ? 1 : 0);
                            goto IL_0317;
                        case 6:
                            num = 7;
                            continue;
                        case 7:
                            if (content.IndexOf("[/coin]") <= 0)
                            {
                                num = 22;
                                continue;
                            }
                            goto IL_0127;
                        case 10:
                            num = 24;
                            continue;
                        case 24:
                            if (content.IndexOf("[/buy]") <= 0)
                            {
                                num = 6;
                                continue;
                            }
                            goto IL_0127;
                        case 15:
                            ShowTipInfo("您当前的身份不允许发特殊贴。", "bbs/book_view_modfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id);
                            num = 16;
                            continue;
                        case 22:
                            num = 9;
                            continue;
                        case 9:
                            num2 = ((content.IndexOf("[/grade]") <= 0) ? 1 : 0);
                            goto IL_0138;
                        case 5:
                            num = 13;
                            continue;
                        case 13:
                            if (content.IndexOf("[/reply]") <= 0)
                            {
                                num = 10;
                                continue;
                            }
                            goto IL_0127;
                        case 23:
                            flag2 = true;
                            num = 17;
                            continue;
                        case 8:
                            num3 = 1;
                            goto IL_0317;
                        case 25:
                            text = "_" + text + "_";
                            flag2 = false;
                            num = 4;
                            continue;
                        case 4:
                            if (int.Parse(viewtype) <= 2)
                            {
                                num = 5;
                                continue;
                            }
                            goto IL_0127;
                        case 14:
                            if (!flag)
                            {
                                num = 1;
                                continue;
                            }
                            goto case 20;
                        case 17:
                            num = 11;
                            continue;
                        case 11:
                            num = (flag2 ? 18 : 8);
                            continue;
                        case 20:
                            num = 3;
                            continue;
                        case 3:
                            return;
                        IL_0138:
                            flag = (byte)num2 != 0;
                            num = 2;
                            continue;
                        IL_0127:
                            num = 21;
                            continue;
                        IL_0317:
                            flag = (byte)num3 != 0;
                            num = 14;
                            continue;
                    }
                    break;
                }
            }
        }
    }
}
