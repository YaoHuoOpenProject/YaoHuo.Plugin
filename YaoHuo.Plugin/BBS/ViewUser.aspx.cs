using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.WebSite;
using System;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

public class ViewUser : BasePage
{
    protected HtmlForm form1;

    protected Repeater TopRepeater;

    protected Repeater SiteRepeater;

    protected AspNetPager AspNetPager1;

    protected TextBox tb_title;

    protected TextBox tb_rec;

    protected Button bt_revert;

    private string m_a = PubConstant.GetAppString("InstanceName");

    public string strCommandType = "";

    public string strCommandResult = "";

    public string strHangBiaoShi = "";

    public string domain = "";

    public string loadpagetime = "";

    public static string bookid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_00c5
        string text = default(string);
        while (true)
        {
            DateTime now = DateTime.Now;
            bool isPostBack = Page.IsPostBack;
            int num = 9;
            while (true)
            {
                switch (num)
                {
                    case 9:
                        if (!isPostBack)
                        {
                            num = 10;
                            continue;
                        }
                        goto case 6;
                    case 13:
                        num = 11;
                        continue;
                    case 8:
                        num = 12;
                        continue;
                    case 12:
                        if (true)
                        {
                        }
                        num = ((text == "edit") ? 1 : 4);
                        continue;
                    case 6:
                        strCommandType = GetRequestValue("CommandType");
                        text = strCommandType.ToLower();
                        num = 16;
                        continue;
                    case 16:
                        if (text != null)
                        {
                            num = 8;
                            continue;
                        }
                        goto case 11;
                    case 11:
                        num = 15;
                        continue;
                    case 0:
                        num = 14;
                        continue;
                    case 14:
                        num = ((text == "uncheck") ? 3 : 5);
                        continue;
                    case 10:
                        CheckManagerLvl("04");
                        bookid = GetRequestValue("bookid");
                        f();
                        this.e();
                        num = 6;
                        continue;
                    case 5:
                        num = 17;
                        continue;
                    case 17:
                        num = ((!(text == "del")) ? 13 : 7);
                        continue;
                    case 4:
                        num = 18;
                        continue;
                    case 18:
                        num = ((text == "check") ? 2 : 0);
                        continue;
                    case 1:
                    case 2:
                    case 3:
                    case 7:
                    case 15:
                        {
                            domain = base.Domain;
                            DateTime now2 = DateTime.Now;
                            loadpagetime = (now2 - now).TotalMilliseconds.ToString();
                            return;
                        }
                }
                break;
            }
        }
    }

    private void f()
    {
        //Discarded unreachable code: IL_0145
        string strWhere = default(string);
        string orderfldName = default(string);
        string value = default(string);
        while (true)
        {
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(this.m_a);
            string text = " id=" + bookid;
            bool flag = !(base.ManagerLvl != "00");
            int num = 2;
            while (true)
            {
                int num3;
                int num2;
                switch (num)
                {
                    case 2:
                        if (!flag)
                        {
                            num = 7;
                            continue;
                        }
                        goto case 16;
                    case 15:
                        strWhere = " bookid=" + bookid;
                        num = 13;
                        continue;
                    case 13:
                        orderfldName = "id";
                        value = "1";
                        num = 10;
                        continue;
                    case 10:
                        num = ((ViewState["OrderColunmName"] == null) ? 20 : 17);
                        continue;
                    case 18:
                        value = ViewState["OrderType"].ToString();
                        num = 3;
                        continue;
                    case 3:
                        if (1 == 0)
                        {
                        }
                        goto case 5;
                    case 19:
                        num3 = 1;
                        goto IL_0316;
                    case 20:
                        num2 = 1;
                        goto IL_0174;
                    case 0:
                        if (!flag)
                        {
                            num = 14;
                            continue;
                        }
                        goto case 9;
                    case 4:
                        num = 1;
                        continue;
                    case 1:
                        num3 = ((!(ViewState["OrderType"].ToString() != "")) ? 1 : 0);
                        goto IL_0316;
                    case 17:
                        num = 8;
                        continue;
                    case 8:
                        num2 = ((!(ViewState["OrderColunmName"].ToString() != "")) ? 1 : 0);
                        goto IL_0174;
                    case 16:
                        TopRepeater.DataSource = wap_bbs_BLL.GetList(text);
                        TopRepeater.DataBind();
                        strWhere = "";
                        flag = !(bookid != "");
                        num = 12;
                        continue;
                    case 12:
                        if (!flag)
                        {
                            num = 15;
                            continue;
                        }
                        goto case 13;
                    case 14:
                        orderfldName = ViewState["OrderColunmName"].ToString();
                        num = 9;
                        continue;
                    case 7:
                        text = text + " and userid=" + base.SiteId;
                        num = 16;
                        continue;
                    case 9:
                        num = 11;
                        continue;
                    case 11:
                        num = ((ViewState["OrderType"] != null) ? 4 : 19);
                        continue;
                    case 6:
                        if (flag)
                        {
                            ViewState["OrderType"] = value;
                            num = 5;
                        }
                        else
                        {
                            num = 18;
                        }
                        continue;
                    case 5:
                        {
                            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(this.m_a);
                            int listCount = wap_bbsre_BLL.GetListCount(strWhere);
                            AspNetPager1.RecordCount = listCount;
                            SiteRepeater.DataSource = wap_bbsre_BLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, strWhere, "", orderfldName, listCount, Convert.ToInt32(value));
                            SiteRepeater.DataBind();
                            return;
                        }
                    IL_0174:
                        flag = (byte)num2 != 0;
                        num = 0;
                        continue;
                    IL_0316:
                        flag = (byte)num3 != 0;
                        num = 6;
                        continue;
                }
                break;
            }
        }
    }

    private void e()
    {
        //Discarded unreachable code: IL_008e
        while (true)
        {
            bool flag = !(bookid.Trim() != "");
            int num = 1;
            while (true)
            {
                switch (num)
                {
                    case 1:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        return;

                    case 0:
                        {
                            SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(this.m_a));
                            string cmdText = "update wap_bbs set book_click=book_click+1 where id =" + bookid;
                            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            if (true)
                            {
                            }
                            num = 2;
                            continue;
                        }
                    case 2:
                        return;
                }
                break;
            }
        }
    }

    private string d()
    {
        //Discarded unreachable code: IL_0237
        string[] array = default(string[]);
        string result = default(string);
        while (true)
        {
            string text = "";
            wap_book_BLL wap_book_BLL = new wap_book_BLL(this.m_a);
            string requestValue = GetRequestValue("hidHangBiaoShis");
            bool flag = !(requestValue != "");
            int num = 1;
            while (true)
            {
                int num2;
                switch (num)
                {
                    case 1:
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        goto case 8;
                    case 9:
                        try
                        {
                            while (true)
                            {
                            IL_00c3:
                                int num3 = 0;
                                num = 2;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 1:
                                        case 2:
                                            flag = num3 < array.Length;
                                            num = 3;
                                            continue;
                                        case 3:
                                            {
                                                if (!flag)
                                                {
                                                    num = 0;
                                                    continue;
                                                }
                                                wap_book_BLL.Delete(Convert.ToInt64(array[num3].ToString()));
                                                SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(this.m_a));
                                                SqlCommand sqlCommand = new SqlCommand("delete from wap_bookre where bookid=" + array[num3].ToString(), sqlConnection);
                                                sqlConnection.Open();
                                                sqlCommand.ExecuteNonQuery();
                                                sqlConnection.Close();
                                                num3++;
                                                num = 1;
                                                continue;
                                            }
                                        case 0:
                                            num = 4;
                                            continue;
                                        case 4:
                                            goto end_IL_00a6;
                                    }
                                    goto IL_00c3;
                                end_IL_00a6:
                                    break;
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            text = ex.Message;
                        }
                        finally
                        {
                        }
                        num = 3;
                        continue;
                    case 6:
                        num = 10;
                        continue;
                    case 10:
                        num2 = ((array.Length <= 0) ? 1 : 0);
                        goto IL_020d;
                    case 3:
                        num = 8;
                        continue;
                    case 0:
                        array = requestValue.Split(',');
                        num = 7;
                        continue;
                    case 7:
                        if (array != null)
                        {
                            num = 6;
                            continue;
                        }
                        if (true)
                        {
                        }
                        num = 4;
                        continue;
                    case 8:
                        result = text;
                        num = 11;
                        continue;
                    case 2:
                        if (!flag)
                        {
                            num = 5;
                            continue;
                        }
                        goto case 3;
                    case 4:
                        num2 = 1;
                        goto IL_020d;
                    case 5:
                        num = 9;
                        continue;
                    case 11:
                        {
                            return result;
                        }
                    IL_020d:
                        flag = (byte)num2 != 0;
                        num = 2;
                        continue;
                }
                break;
            }
        }
    }

    private string c()
    {
        //Discarded unreachable code: IL_0233
        string[] array = default(string[]);
        SqlConnection sqlConnection = default(SqlConnection);
        string result = default(string);
        while (true)
        {
            string text = "";
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", "<script language=javascript>document.body.style.cursor='wait';</script>", addScriptTags: true);
            string requestValue = GetRequestValue("hidHangBiaoShis");
            bool flag = !(requestValue != "");
            int num = 4;
            while (true)
            {
                int num2;
                switch (num)
                {
                    case 4:
                        if (!flag)
                        {
                            num = 5;
                            continue;
                        }
                        goto case 8;
                    case 9:
                        try
                        {
                            while (true)
                            {
                            IL_00dc:
                                int num3 = 0;
                                num = 2;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 0:
                                        case 2:
                                            flag = num3 < array.Length;
                                            num = 1;
                                            continue;
                                        case 1:
                                            {
                                                if (!flag)
                                                {
                                                    num = 4;
                                                    continue;
                                                }
                                                SqlCommand sqlCommand = new SqlCommand("update wap_bbs set ischeck=0 where id=" + array[num3].ToString(), sqlConnection);
                                                sqlCommand.ExecuteNonQuery();
                                                num3++;
                                                num = 0;
                                                continue;
                                            }
                                        case 4:
                                            num = 3;
                                            continue;
                                        case 3:
                                            goto end_IL_00bf;
                                    }
                                    goto IL_00dc;
                                end_IL_00bf:
                                    break;
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            text = ex.Message;
                        }
                        finally
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", "<script language=javascript>document.body.style.cursor='default';</script>", addScriptTags: true);
                        }
                        num = 6;
                        continue;
                    case 3:
                        num = 7;
                        continue;
                    case 7:
                        num2 = ((array.Length <= 0) ? 1 : 0);
                        goto IL_023a;
                    case 6:
                        sqlConnection.Close();
                        num = 8;
                        continue;
                    case 5:
                        array = requestValue.Split(',');
                        sqlConnection = new SqlConnection(PubConstant.GetConnectionString(this.m_a));
                        sqlConnection.Open();
                        num = 11;
                        continue;
                    case 11:
                        num = ((array == null) ? 2 : 3);
                        continue;
                    case 8:
                        result = text;
                        num = 10;
                        continue;
                    case 10:
                        if (true)
                        {
                        }
                        return result;

                    case 0:
                        if (!flag)
                        {
                            num = 1;
                            continue;
                        }
                        goto case 6;
                    case 2:
                        num2 = 1;
                        goto IL_023a;
                    case 1:
                        {
                            num = 9;
                            continue;
                        }
                    IL_023a:
                        flag = (byte)num2 != 0;
                        num = 0;
                        continue;
                }
                break;
            }
        }
    }

    private string b()
    {
        //Discarded unreachable code: IL_014d
        string[] array = default(string[]);
        SqlConnection sqlConnection = default(SqlConnection);
        string result = default(string);
        while (true)
        {
            string text = "";
            string requestValue = GetRequestValue("hidHangBiaoShis");
            bool flag = !(requestValue != "");
            int num = 0;
            while (true)
            {
                int num2;
                switch (num)
                {
                    case 0:
                        if (!flag)
                        {
                            num = 2;
                            continue;
                        }
                        goto case 7;
                    case 5:
                        try
                        {
                            while (true)
                            {
                            IL_00b4:
                                int num3 = 0;
                                num = 3;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 1:
                                        case 3:
                                            flag = num3 < array.Length;
                                            num = 2;
                                            continue;
                                        case 2:
                                            {
                                                if (!flag)
                                                {
                                                    num = 0;
                                                    continue;
                                                }
                                                SqlCommand sqlCommand = new SqlCommand("update wap_bbs set ischeck=1 where id=" + array[num3].ToString(), sqlConnection);
                                                sqlCommand.ExecuteNonQuery();
                                                num3++;
                                                num = 1;
                                                continue;
                                            }
                                        case 0:
                                            num = 4;
                                            continue;
                                        case 4:
                                            goto end_IL_0097;
                                    }
                                    goto IL_00b4;
                                end_IL_0097:
                                    break;
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            text = ex.Message;
                        }
                        finally
                        {
                            if (1 == 0)
                            {
                            }
                        }
                        num = 9;
                        continue;
                    case 4:
                        num = 3;
                        continue;
                    case 3:
                        num2 = ((array.Length <= 0) ? 1 : 0);
                        goto IL_01f0;
                    case 9:
                        sqlConnection.Close();
                        num = 7;
                        continue;
                    case 2:
                        array = requestValue.Split(',');
                        sqlConnection = new SqlConnection(PubConstant.GetConnectionString(this.m_a));
                        sqlConnection.Open();
                        num = 8;
                        continue;
                    case 8:
                        num = ((array == null) ? 6 : 4);
                        continue;
                    case 7:
                        result = text;
                        num = 1;
                        continue;
                    case 10:
                        if (!flag)
                        {
                            num = 11;
                            continue;
                        }
                        goto case 9;
                    case 6:
                        num2 = 1;
                        goto IL_01f0;
                    case 11:
                        num = 5;
                        continue;
                    case 1:
                        {
                            return result;
                        }
                    IL_01f0:
                        flag = (byte)num2 != 0;
                        num = 10;
                        continue;
                }
                break;
            }
        }
    }

    private string a()
    {
        return "";
    }

    protected void AspNetPager1_PageChanged(object src, PageChangedEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        f();
    }
}
