using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using YaoHuo.Plugin.Tool;
using YaoHuo.Plugin.WebSite;

namespace YaoHuo.Plugin.BBS
{
    public class userGuessBook : MyPageWap
    {

    private string string_10 = PubConstant.GetAppString("InstanceName");

    public string action = "";

    public string linkURL = "";

    public string condition = "";

    public string ERROR = "";

    public string INFO = "";

    public string string_11 = "0";

    public string lpage = "";

    public string ot = "0";

    public string touserid = "";

    public string face = "";

    public string linkTOP = "";

    public List<wap2_userGuessBook_Model> listVo = null;

    public StringBuilder strhtml = new StringBuilder();

    public long kk = 1L;

    public long index = 0L;

    public long total = 0L;

    public long pageSize = 10L;

    public long CurrentPage = 1L;

    public string KL_CheckIPTime = PubConstant.GetAppString("KL_CheckIPTime");

    public string KL_CheckZoneReCount = PubConstant.GetAppString("KL_CheckZoneReCount");

    protected void Page_Load(object sender, EventArgs e)
    {
        action = base.Request.Form.Get("action");
        lpage = GetRequestValue("lpage");
        ot = GetRequestValue("ot");
        touserid = GetRequestValue("touserid");
        face = GetRequestValue("face");
        if (face.Trim() == "")
        {
            face = "face";
        }
        switch (action)
        {
            case "add":
                method_2();
                break;
            case "class":
                showclass();
                break;
            default:
                showclass();
                break;
        }
    }

    public void showclass()
    {
        condition = " ischeck=0 and siteid=" + siteid + " and userid=" + touserid;
        try
        {
            pageSize = Convert.ToInt32(siteVo.MaxPerPage_Default);
            wap2_userGuessBook_BLL wap2_userGuessBook_BLL = new wap2_userGuessBook_BLL(string_10);
            if (GetRequestValue("getTotal") != "" && GetRequestValue("getTotal") != "0")
            {
                total = Convert.ToInt32(GetRequestValue("getTotal"));
            }
            else
            {
                total = wap2_userGuessBook_BLL.GetListCount(condition);
            }
            if (GetRequestValue("page") != "")
            {
                CurrentPage = long.Parse(GetRequestValue("page"));
            }
            CurrentPage = WapTool.CheckCurrpage(total, pageSize, CurrentPage);
            index = pageSize * (CurrentPage - 1L);
            linkURL = http_start + "bbs/userGuessBook.aspx?action=class&amp;siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "&amp;ot=" + ot + "&amp;getTotal=" + total;
            linkTOP = WapTool.GetPageLinkShowTOP(ver, lang, total, pageSize, CurrentPage, linkURL);
            linkURL = WapTool.GetPageLink(ver, lang, total, pageSize, CurrentPage, linkURL, "1");
            if (ot == "1")
            {
                listVo = wap2_userGuessBook_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 0);
            }
            else
            {
                listVo = wap2_userGuessBook_BLL.GetListVo(pageSize, CurrentPage, condition, "*", "id", total, 1);
            }
        }
        catch (Exception ex)
        {
            ERROR = ex.ToString();
        }
    }

    public void method_2()
    {
        IsLogin(userid, GetUrlQueryString());
        string text = GetRequestValue("content");
        string backurl = base.Request.Form.Get("backurl");
        string requestValue = GetRequestValue("face");
        IsLogin(userid, backurl);
        if (!WapTool.IsNumeric(KL_CheckZoneReCount))
        {
            KL_CheckZoneReCount = "20";
        }
        wap2_userGuessBook_BLL wap2_userGuessBook_BLL = new wap2_userGuessBook_BLL(string_10);
        long num = wap2_userGuessBook_BLL.GetListCount(" (DATEDIFF(dd, addtime, GETDATE()) < 1) and siteid=" + long.Parse(siteid) + " and fromuserid=" + long.Parse(userid));
        if (text.Trim().Length < 2)
        {
            INFO = "NULL";
        }
        else if (num > long.Parse(KL_CheckZoneReCount))
        {
            INFO = "MAX";
        }
        else if (text.Equals(Session["content"]))
        {
            INFO = "REPEAT";
        }
        else if (isCheckIPTime(long.Parse(KL_CheckIPTime)))
        {
            INFO = "WAITING";
        }
        else if (WapTool.isLockuser(siteid, userid, classid) > -1L)
        {
            INFO = "LOCK";
        }
        else
        {
            try
            {
                Session["content"] = text;
                if (text.ToLower().IndexOf("[sid]") > 0 && !IsUserManager(userid, userVo.managerlvl, ""))
                {
                    text = text.ToLower().Replace("[sid]", "[sid2]");
                    text = text.ToLower().Replace("[sid1]", "[sid2]");
                }
                if (requestValue.Trim() != "" && requestValue.Trim() != "face")
                {
                    text = "[img]face/" + requestValue + ".gif[/img]" + text;
                }
                text = "<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + userid + "\">" + nickname + "</a> <span class=\"right\">" + $"{DateTime.Now:MM-dd HH:mm}" + "</span><br/>" + text;
                wap2_userGuessBook_Model wap2_userGuessBook_Model = new wap2_userGuessBook_Model();
                wap2_userGuessBook_Model.siteid = long.Parse(siteid);
                wap2_userGuessBook_Model.userid = long.Parse(touserid);
                wap2_userGuessBook_Model.fromuserid = long.Parse(userid);
                wap2_userGuessBook_Model.fromnickname = nickname;
                wap2_userGuessBook_Model.content = text;
                wap2_userGuessBook_Model.addtime = DateTime.Now;
                wap2_userGuessBook_Model.ischeck = siteVo.isCheck;
                wap2_userGuessBook_BLL.Add(wap2_userGuessBook_Model);
                if (userid != touserid)
                {
                    MainBll.UpdateSQL("insert into wap_message(siteid,userid,nickname,title,content,touserid,issystem)values(" + siteid + "," + siteid + ",'系统','" + nickname + "在您的空间留言了','[url=/bbs/userinfo.aspx?siteid=" + siteid + "&amp;touserid=" + touserid + "]快去看看吧！[/url]'," + touserid + ",1)");
                }
                INFO = "OK";
            }
            catch (Exception ex)
            {
                ERROR = ex.ToString();
            }
        }
        VisiteCount("在<a href=\"" + http_start + "bbs/userinfo.aspx?siteid=" + siteid + "&amp;classid=" + classid + "&amp;touserid=" + touserid + "\">个人空间</a>留言了。");
        Action_user_doit(5);
        showclass();
    }
}
}