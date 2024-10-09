using System;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.WebSite;
using YaoHuo.Plugin.Tool;

namespace YaoHuo.Plugin.BBS
{
	public class book_view_modfile_del : MyPageWap
	{
		private string string_10 = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string id = "";

		public string lpage = "";

		public string INFO = "";

		public string ERROR = "";

		public string string_12 = "";

		public string delid = "";

		public wap_bbs_Model bbsVo = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (classid != "0" && classVo.typePath.ToLower() != "bbs/index.aspx")
			{
				ShowTipInfo("抱歉，当前访问的栏目ID对应非论坛模块，请联系站长处理。", "");
			}
			action = GetRequestValue("action");
			id = GetRequestValue("id");
			delid = GetRequestValue("delid");
			lpage = GetRequestValue("lpage");
			string_12 = GetRequestValue("sub");
			wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_10);
			bbsVo = wap_bbs_BLL.GetModel(long.Parse(id));
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
				ShowTipInfo("此贴已锁！", "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + bbsVo.book_classid + "&amp;id=" + bbsVo.id + "&amp;lpage=" + lpage);
			}
			if (bbsVo == null)
			{
				ShowTipInfo(GetLang("已删除|已删除|Not Exist"), "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;page=" + lpage);
			}
			if (userid != bbsVo.book_pub.ToString())
			{
				CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
			}
			else
			{
				IsLogin(userid, "bbs/book_view_modfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
				needPassWordToAdmin();
			}
			if (!(action == "godel"))
			{
				return;
			}
			try
			{
				wap2_attachment_Model wap2_attachment_Model = null;
				wap2_attachment_BLL wap2_attachment_BLL = new wap2_attachment_BLL(string_10);
				wap2_attachment_Model = wap2_attachment_BLL.GetModel(long.Parse(delid));
				if (wap2_attachment_Model == null)
				{
					ShowTipInfo("已删除！或不存在！", "");
				}
				if (wap2_attachment_Model.siteid.ToString() != siteid)
				{
					base.Response.End();
				}
				if (userid != wap2_attachment_Model.userid.ToString())
				{
					CheckManagerLvl("04", classVo.adminusername, "bbs/book_view_admin.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;lpage=" + lpage + "&amp;id=" + id);
				}
				else
				{
					IsLogin(userid, "bbs/book_view_modfile.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;id=" + id + "&amp;lpage=" + lpage);
					needPassWordToAdmin();
				}
				DeleteFile("bbs", wap2_attachment_Model.book_file, GetUrlQueryString().Replace("godel", "go"));
				wap2_attachment_BLL.Delete(long.Parse(delid));
				string text = "{" + userVo.nickname + "(ID" + userVo.userid + ")删除附件" + $"{DateTime.Now:MM-dd HH:mm}" + "}<br/>";
				bbsVo.whylock = text + bbsVo.whylock;
				wap_bbs_BLL.Update(bbsVo);
				INFO = "OK";
			}
			catch (Exception ex)
			{
				ERROR = ex.ToString();
			}
		}
	}
}