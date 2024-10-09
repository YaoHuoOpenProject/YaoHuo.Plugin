using System;
using System.Collections.Generic;
using System.Web;
using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.Model;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
	public class BankList : MyPageWap
	{
		private string string_10 = PubConstant.GetAppString("InstanceName");

		public string action = "";

		public string linkURL = "";

		public string condition = "";

		public string ERROR = "";

		public string key = "";

		public string friendtype = "";

		public string backurl = "";

		public string linkTOP = "";

		public string toyear = DateTime.Now.Year.ToString();

		public string tomonth = DateTime.Now.Month.ToString();

		public bool isadmin = false;

		public string typeid = "";

		public string typekey = "";

		public List<wap_bankLog_Model> listVo = null;

		public long long_0 = 1L;

		public long index = 0L;

		public long total = 0L;

		public long pageSize = 10L;

		public long CurrentPage = 1L;

		protected void Page_Load(object sender, EventArgs e)
		{
			action = GetRequestValue("action");
			backurl = base.Request.QueryString.Get("backurl");
			if (backurl == null || backurl == "")
			{
				backurl = base.Request.Form.Get("backurl");
			}
			if (backurl == null || backurl == "")
			{
				backurl = "myfile.aspx?siteid=" + siteid;
			}
			backurl = ToHtm(backurl);
			backurl = HttpUtility.UrlDecode(backurl);
			backurl = WapTool.URLtoWAP(backurl);
			isadmin = IsUserManager(userid, userVo.managerlvl, "");
			IsLogin(userid, backurl);
			switch (action)
			{
				case "class":
					showclass();
					break;
				default:
					showclass();
					break;
				case "godel":
					break;
			}
		}

		public void showclass()
		{
			key = GetRequestValue("key");
			typeid = GetRequestValue("typeid");
			typekey = GetRequestValue("typekey");
			if (action == "search")
			{
				toyear = GetRequestValue("toyear");
				tomonth = GetRequestValue("tomonth");
			}
			if (action == "mod")
			{
				ShowTipInfo("确定删除吗？<a href=\"" + http_start + "bbs/banklist.aspx?action=gomod&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;toyear=" + toyear + "&amp;tomonth=" + tomonth + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + " \">确定</a>", "bbs/banklist.aspx?action=gomod&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;toyear=" + toyear + "&amp;tomonth=" + tomonth + "&amp;backurl=" + HttpUtility.UrlEncode(backurl));
			}
			else if (action == "gomod")
			{
				string text = DateTime.Now.Year.ToString();
				string text2 = DateTime.Now.Month.ToString();
				MainBll.UpdateSQL("delete from wap_bankLog where siteid=" + siteid + " and  addtime < '" + text + "-" + text2 + "-1 00:00:00'");
			}
			condition = " siteid=" + siteid;
			if (isadmin)
			{
				if (key.Trim() != "" && key.Trim() != "0")
				{
					condition = condition + " and userid = " + key + " ";
				}
			}
			else
			{
				key = userid;
				condition = condition + " and userid = " + userid + " ";
			}
			if (typekey.Trim() != "" && typeid.Trim() != "")
			{
				if (typeid == "1")
				{
					condition = condition + " and actionname like '%" + typekey + "%' ";
				}
				else if (typeid == "2")
				{
					if (!WapTool.IsNumeric(typekey))
					{
						typekey = "0";
					}
					condition = condition + " and opera_userid  =" + typekey + "  ";
				}
				else if (typeid == "3")
				{
					condition = condition + " and opera_nickname like '%" + typekey + "%' ";
				}
				else if (typeid == "4")
				{
					condition = condition + " and remark like '%" + typekey + "%' ";
				}
				else if (typeid == "5")
				{
					if (!WapTool.IsNumeric(typekey))
					{
						typekey = "0";
					}
					condition = condition + " and id  =" + typekey + "  ";
				}
			}
			if (WapTool.IsNumeric(toyear))
			{
				condition = condition + " and  Year(addtime) = " + toyear + " ";
			}
			if (WapTool.IsNumeric(tomonth))
			{
				condition = condition + " and  Month(addtime) = " + tomonth + " ";
			}
			try
			{
				pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
				wap_bankLog_BLL wap_bankLog_BLL = new wap_bankLog_BLL(string_10);
				if (GetRequestValue("getTotal") != "")
				{
					total = long.Parse(GetRequestValue("getTotal"));
				}
				else
				{
					total = wap_bankLog_BLL.GetListCount(condition);
				}
				if (GetRequestValue("page") != "")
				{
					CurrentPage = long.Parse(GetRequestValue("page"));
				}
				CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
				index = pageSize * (CurrentPage - 1L);
				linkURL = http_start + "bbs/banklist.aspx?action=search&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;typeid=" + HttpUtility.UrlEncode(typeid) + "&amp;typekey=" + HttpUtility.UrlEncode(typekey) + "&amp;key=" + HttpUtility.UrlEncode(key) + "&amp;toyear=" + toyear + "&amp;tomonth=" + tomonth + "&amp;backurl=" + HttpUtility.UrlEncode(backurl) + "&amp;getTotal=" + total;
				linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
				linkURL = WapTool.GetPageLink(ver, lang, Convert.ToInt32(total), pageSize, CurrentPage, linkURL);
				listVo = wap_bankLog_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1L);
			}
			catch (Exception ex)
			{
				ERROR = ex.ToString();
			}
		}
	}
}