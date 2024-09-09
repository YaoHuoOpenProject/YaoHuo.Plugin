using System;
using KeLin.ClassManager;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{

	public class Book_List_delmy : MyPageWap
    {
		private string string_10 = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string touserid = "";

		public string reid = "";

		public string page = "";

		public string lpage = "";

		public string ot = "";

		public string INFO = "";

		public string ERROR = "";

		public string why = "";

		public string sub = "";

		public string pw = "";

		public wap_bbsre_Model bbsReVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			action = GetRequestValue("action");
			touserid = GetRequestValue("touserid").TrimStart('0'); // 去掉前导零
			reid = GetRequestValue("reid");
			page = GetRequestValue("page");
			lpage = GetRequestValue("lpage");
			ot = GetRequestValue("ot");
			why = GetRequestValue("why");
			sub = GetRequestValue("sub");
			pw = GetRequestValue("pw");
			CheckManagerLvl("04", "", GetUrlQueryString());

			// 如果ID为1000，不允许进行清空操作并跳转到首页
			if (touserid == "1000")
			{
				Response.Redirect("/");
				return;
			}
			if (action == "Del_1")
			{
				action = "godel";
				sub = "1";
			}
			else if (action == "Del_2")
			{
				action = "godel";
				sub = "2";
			}
			else if (action == "Del_3")
			{
				action = "godel";
				sub = "3";
			}
			if (!WapTool.IsNumeric(touserid))
			{
				touserid = "0";
			}
			if (!(action == "godel"))
			{
				return;
			}
			try
			{
				if (PubConstant.md5(pw).ToLower() != userVo.password.ToLower())
				{
					INFO = "PASSERROR";
					return;
				}
				MainBll.UpdateSQL("delete from wap_bbs where userid=" + siteid + " and book_pub=" + touserid);
				if (sub == "1")
				{
					MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的所有帖子被清空','删除时间：", DateTime.Now, "[br]理由：", why, "',", touserid, ",1)"));
					MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "清空用户ID:" + touserid + "的所有帖子','" + IP + "')");
				}
				else if (sub == "2")
				{
					MainBll.UpdateSQL("update [user] set money=0 where siteid=" + siteid + " and userid=" + touserid);
					MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的所有帖子被清空','删除时间：", DateTime.Now, "[br]同时清币[br]理由：", why, "',", touserid, ",1)"));
					MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "清空用户ID:" + touserid + "的所有帖子[+清币]','" + IP + "')");
				}
				else
				{
					MainBll.UpdateSQL("update [user] set money=0,expr=0 where siteid=" + siteid + " and userid=" + touserid);
					MainBll.UpdateSQL(string.Concat("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(", siteid, ",", userid, ",'", nickname, "','您的所有帖子被清空','删除时间：", DateTime.Now, "[br]同时清币，清经验值[br]理由：", why, "',", touserid, ",1)"));
					MainBll.UpdateSQL("insert into wap_log(siteid,oper_userid,oper_nickname,oper_type,log_info,oper_ip)values(" + siteid + "," + userid + ",'" + nickname + "',0,'用户ID:" + userid + "清空用户ID:" + touserid + "的所有帖子[+清币+清经验]','" + IP + "')");
				}
				INFO = "OK";
			}
			catch (Exception ex)
			{
				ERROR = ex.ToString();
			}
		}
	}
}