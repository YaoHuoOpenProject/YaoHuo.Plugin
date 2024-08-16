using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using KeLin.WebSite;
using System;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class Book_View_del : MyPageWap
    {
        public string why = "";
        private string string_10 = PubConstant.GetAppString("InstanceName");
        public string action = "";
        public string id = "";
        public string lpage = "";
        public string INFO = "";
        public string ERROR = "";
        public string ot = "";
        public wap_bbs_Model bbsVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
            {
                ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
            }

            action = GetRequestValue("action");
            id = GetRequestValue("id");
            lpage = GetRequestValue("lpage");
            ot = GetRequestValue("sub");
            why = GetRequestValue("why");

            if (action.ToLower() == "del_1")
            {
                action = "godel";
                ot = "1";
            }
            else if (action.ToLower() == "del_2")
            {
                action = "godel";
                ot = "2";
            }
            else if (action.ToLower() == "del_3")
            {
                action = "godel";
                ot = "0";
            }

            IsLogin(userid, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);

            NeedPassWordToAdminNew();

            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);

            long parsedId;
            if (!long.TryParse(id, out parsedId))
            {
                ShowTipInfo("无效的帖子ID", "");
                return;
            }

            bbsVo = wap_bbs_BLL.GetModel(parsedId);
            if (bbsVo == null)
            {
                ShowTipInfo("已删除！或不存在！", "");
            }
            else if (bbsVo.ischeck == 1L)
            {
                ShowTipInfo("正在审核中！", "");
            }
            else if (bbsVo.book_classid.ToString() != classid)
            {
                ShowTipInfo("栏目ID对不上！可能没有传classid值！", "");
            }
            else if (bbsVo.islock == 1L)
            {
                ShowTipInfo("此帖已锁！", "bbs/book_view.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
            }
            if (bbsVo == null || (bbsVo != null && bbsVo.ischeck == 2L))
            {
                ShowTipInfo(GetLang("已删除|已删除|Not Exist"), "bbs/book_list.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage);
            }
            if (userid != bbsVo.book_pub.ToString())
            {
                CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            else if (!CheckManagerLvl(userVo.managerlvl, classVo.adminusername) && WapTool.GetSiteDefault(siteVo.Version, 26) == "1")
            {
                ShowTipInfo("站长已关闭此功能！", "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
            }
            if (!(action == "godel"))
            {
                return;
            }
            try
            {
                MainBll.UpdateSQL("update wap_bbs set ischeck=2 where userid=" + siteid + " and id=" + parsedId);
                MainBll.UpdateSQL("update [class] set total=total-1 where classid=" + long.Parse(classid));
                long num = WapTool.getMoneyRegular(siteVo.moneyregular, 0);
                long num2 = WapTool.getLvLRegular(siteVo.lvlRegular, 0);

                if (userid == bbsVo.book_pub.ToString())
                {
                    num *= 2L;
                    num2 *= 2L;
                }
                else if (ot == "2")
                {
                    num *= 2L;
                    num2 *= 2L;
                }
                else if (ot == "0")
                {
                    num = 0L;
                    num2 = 0L;
                }

                string book_title = bbsVo.book_title;
                book_title = book_title.Replace("[", "［");
                book_title = book_title.Replace("]", "］");

                MainBll.UpdateSQL("update [user] set money=money -" + num + ",expR=expR-" + num2 + " where siteid=" + long.Parse(siteid) + " and userid=" + long.Parse(bbsVo.book_pub));
                SaveBankLog(bbsVo.book_pub.ToString(), "删除帖子", "-" + num, userid, nickname, "帖子[ID:" + bbsVo.id + "]");

                string text = "";
                string title = "您的一篇帖子删除";
                string deleteReason = "";

                if (userid == bbsVo.book_pub.ToString())
                {
                    title = "您删除了自己的帖子";
                }
                else
                {
                    deleteReason = !string.IsNullOrEmpty(why) ? "[br]删帖提示：" + why : "";
                }

                MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','", title, "，扣除", num, "妖晶、", num2, "经验','", text, "[br]删除时间：", DateTime.Now, "[br]帖子标题：", bbsVo.book_title, deleteReason, "',", bbsVo.book_pub, ",1)"));
                MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "删除用户ID:" + bbsVo.book_pub + "发表的ID=" + parsedId + "主题:" + bbsVo.book_title + "','" + IP + "')");

                INFO = "OK";
                WapTool.ClearDataBBS("bbs" + siteid + classid);
                WapTool.ClearDataBBS("bbsTop" + siteid + classid);
                WapTool.ClearDataTemp("bbsTotal" + siteid + classid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}