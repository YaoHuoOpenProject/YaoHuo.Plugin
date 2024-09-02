using KeLin.ClassManager;
using KeLin.ClassManager.BLL;
using KeLin.WebSite;
using System;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace YaoHuo.Plugin.BBS
{
    public class ViewUser : BasePage
    {
        protected HtmlForm form1;

        protected Repeater TopRepeater;

        protected Repeater SiteRepeater;

        protected AspNetPager AspNetPager1;

        protected TextBox tb_title;

        protected TextBox tb_rec;

        protected Button bt_revert;

        private string string_36 = PubConstant.GetAppString("InstanceName");

        public string strCommandType = "";

        public string strCommandResult = "";

        public string strHangBiaoShi = "";

        public string domain = "";

        public string loadpagetime = "";

        public static string bookid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (!Page.IsPostBack)
            {
                CheckManagerLvl("04");
                bookid = GetRequestValue("bookid");
                method_2();
                method_3();
            }
            strCommandType = GetRequestValue("CommandType");
            switch (strCommandType.ToLower())
            {
                default:
                    // 暂无处理
                    break;
            }
            domain = base.Domain;
            DateTime now2 = DateTime.Now;
            loadpagetime = (now2 - now).TotalMilliseconds.ToString();
        }

        private void method_2()
        {
            wap_bbs_BLL wap_bbs_BLL = new wap_bbs_BLL(string_36);
            string text = " id=" + bookid;
            if (base.ManagerLvl != "00")
            {
                text = text + " and userid=" + base.SiteId;
            }
            TopRepeater.DataSource = wap_bbs_BLL.GetList(text);
            TopRepeater.DataBind();
            string strWhere = "";
            if (bookid != "")
            {
                strWhere = " bookid=" + bookid;
            }
            string orderfldName = "id";
            string value = "1";
            if (ViewState["OrderColunmName"] != null && ViewState["OrderColunmName"].ToString() != "")
            {
                orderfldName = ViewState["OrderColunmName"].ToString();
            }
            if (ViewState["OrderType"] != null && ViewState["OrderType"].ToString() != "")
            {
                value = ViewState["OrderType"].ToString();
            }
            else
            {
                ViewState["OrderType"] = value;
            }
            wap_bbsre_BLL wap_bbsre_BLL = new wap_bbsre_BLL(string_36);
            int listCount = wap_bbsre_BLL.GetListCount(strWhere);
            AspNetPager1.RecordCount = listCount;
            SiteRepeater.DataSource = wap_bbsre_BLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, strWhere, "", orderfldName, listCount, Convert.ToInt32(value));
            SiteRepeater.DataBind();
        }

        private void method_3()
        {
            if (bookid.Trim() != "")
            {
                SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(string_36));
                string cmdText = "update wap_bbs set book_click=book_click+1 where id =" + bookid;
                SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private string method_4()
        {
            string result = "";
            wap_book_BLL wap_book_BLL = new wap_book_BLL(string_36);
            string requestValue = GetRequestValue("hidHangBiaoShis");
            if (requestValue != "")
            {
                string[] array = requestValue.Split(',');
                if (array != null && array.Length > 0)
                {
                    try
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            wap_book_BLL.Delete(Convert.ToInt64(array[i].ToString()));
                            SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(string_36));
                            SqlCommand sqlCommand = new SqlCommand("delete from wap_bookre where bookid=" + array[i].ToString(), sqlConnection);
                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                    finally
                    {
                    }
                }
            }
            return result;
        }

        private string method_5()
        {
            string result = "";
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", "<script language=javascript>document.body.style.cursor='wait';</script>", addScriptTags: true);
            string requestValue = GetRequestValue("hidHangBiaoShis");
            if (requestValue != "")
            {
                string[] array = requestValue.Split(',');
                SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(string_36));
                sqlConnection.Open();
                if (array != null && array.Length > 0)
                {
                    try
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            SqlCommand sqlCommand = new SqlCommand("update wap_bbs set ischeck=0 where id=" + array[i].ToString(), sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                    finally
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "clientScript", "<script language=javascript>document.body.style.cursor='default';</script>", addScriptTags: true);
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }

        private string method_6()
        {
            string result = "";
            string requestValue = GetRequestValue("hidHangBiaoShis");
            if (requestValue != "")
            {
                string[] array = requestValue.Split(',');
                SqlConnection sqlConnection = new SqlConnection(PubConstant.GetConnectionString(string_36));
                sqlConnection.Open();
                if (array != null && array.Length > 0)
                {
                    try
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            SqlCommand sqlCommand = new SqlCommand("update wap_bbs set ischeck=1 where id=" + array[i].ToString(), sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                    finally
                    {
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }

        private string method_7()
        {
            return "";
        }

        protected void AspNetPager1_PageChanged(object sender, PageChangedEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            method_2();
        }
    }
}