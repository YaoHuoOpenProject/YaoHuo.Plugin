using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using YaoHuo.Plugin.Tool;

public class Default : Page
{
    public string _InstanceName = PubConstant.GetAppString("InstanceName");

    public string KL_HiddenQuery = PubConstant.GetAppString("KL_HiddenQuery");

    public string INFO = "";

    public string ERROR = "";

    public string domain = "";

    public string siteid = "";

    public string goURL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_0197
        domain = base.Request.ServerVariables["HTTP_HOST"];
        domain = domain.ToLower();
        try
        {
            string text2 = default(string);
            IEnumerator enumerator = default(IEnumerator);
            string text3 = default(string);
            DataRow dataRow = default(DataRow);
            while (true)
            {
                string text = base.Request.QueryString.Get("_K");
                bool flag = text == null;
                int num = 3;
                while (true)
                {
                    int num2;
                    switch (num)
                    {
                        case 3:
                            if (!flag)
                            {
                                num = 9;
                                continue;
                            }
                            goto case 20;
                        case 16:
                            text2 = domain.Split('.')[0];
                            flag = domain.IndexOf(".zone.") <= 0;
                            num = 21;
                            continue;
                        case 21:
                            {
                                if (!flag)
                                {
                                    num = 17;
                                    continue;
                                }
                                string commandText = "select * from domainname where id=1 or domain='" + domain + "' order by id desc";
                                DataSet dataSet = DbHelperSQL.ExecuteDataset(PubConstant.GetConnectionString(_InstanceName), CommandType.Text, commandText);
                                enumerator = dataSet.Tables[0].Rows.GetEnumerator();
                                num = 6;
                                continue;
                            }
                        case 2:
                            num2 = 0;
                            goto IL_021b;
                        case 13:
                            Session["_QR"] = text3;
                            num = 16;
                            continue;
                        case 15:
                        case 19:
                            num = 0;
                            continue;
                        case 0:
                            if ("0".Equals(KL_HiddenQuery))
                            {
                                num = 2;
                                continue;
                            }
                            if (true)
                            {
                            }
                            num = 5;
                            continue;
                        case 12:
                            base.Response.Redirect(goURL);
                            num = 18;
                            continue;
                        case 17:
                            siteid = WapTool.GetSiteid(text2, "");
                            flag = !WapTool.IsNumeric(text2);
                            num = 14;
                            continue;
                        case 14:
                            if (!flag)
                            {
                                num = 10;
                                continue;
                            }
                            goto case 8;
                        case 11:
                            if (!flag)
                            {
                                num = 12;
                                continue;
                            }
                            base.Server.Transfer(goURL.Replace("http://" + domain, ""));
                            base.Response.End();
                            num = 4;
                            continue;
                        case 8:
                            goURL = "http://" + domain + "/bbs/userinfo.aspx?siteid=" + siteid + "&touserid=" + text2 + "&TJ=" + text2;
                            num = 15;
                            continue;
                        case 9:
                            Session["_K"] = text;
                            num = 20;
                            continue;
                        case 10:
                            Session["KL_FROM_USERID"] = text2;
                            num = 8;
                            continue;
                        case 20:
                            text3 = base.Request.QueryString.Get("_QR");
                            flag = text3 == null;
                            num = 7;
                            continue;
                        case 7:
                            if (!flag)
                            {
                                num = 13;
                                continue;
                            }
                            goto case 16;
                        case 5:
                            num = 1;
                            continue;
                        case 1:
                            num2 = (goURL.StartsWith("http://" + domain) ? 1 : 0);
                            goto IL_021b;
                        case 6:
                            try
                            {
                                num = 12;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 10:
                                            if (!flag)
                                            {
                                                num = 14;
                                                continue;
                                            }
                                            goto case 1;
                                        case 16:
                                            num = (flag ? 13 : 0);
                                            continue;
                                        case 15:
                                            goURL = "http://" + domain + "/index.aspx";
                                            num = 6;
                                            continue;
                                        case 11:
                                            flag = domain.ToLower().IndexOf("www.") < 0;
                                            num = 3;
                                            continue;
                                        case 3:
                                            if (flag)
                                            {
                                                siteid = WapTool.GetSiteid("", text2);
                                                flag = !(siteid == "");
                                                num = 10;
                                            }
                                            else
                                            {
                                                num = 15;
                                            }
                                            continue;
                                        case 14:
                                            siteid = "0";
                                            num = 1;
                                            continue;
                                        case 1:
                                            goURL = "http://" + domain + "/wapindex.aspx?siteid=" + siteid;
                                            num = 7;
                                            continue;
                                        case 5:
                                            if (flag)
                                            {
                                                flag = !(dataRow["domain"].ToString().ToLower() == domain);
                                                num = 16;
                                            }
                                            else
                                            {
                                                num = 11;
                                            }
                                            continue;
                                        default:
                                            flag = enumerator.MoveNext();
                                            num = 2;
                                            continue;
                                        case 2:
                                            if (flag)
                                            {
                                                dataRow = (DataRow)enumerator.Current;
                                                flag = !(dataRow["id"].ToString() == "1");
                                                num = 5;
                                            }
                                            else
                                            {
                                                num = 9;
                                            }
                                            continue;
                                        case 0:
                                            goURL = dataRow["realpath"].ToString().Replace("&amp;", "&");
                                            num = 4;
                                            continue;
                                        case 4:
                                        case 6:
                                        case 7:
                                        case 9:
                                            num = 8;
                                            continue;
                                        case 8:
                                            break;
                                    }
                                    break;
                                }
                            }
                            finally
                            {
                                while (true)
                                {
                                IL_06d6:
                                    IDisposable disposable = enumerator as IDisposable;
                                    flag = disposable == null;
                                    num = 2;
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
                                                goto end_IL_06c1;
                                            case 1:
                                                disposable.Dispose();
                                                num = 0;
                                                continue;
                                            case 0:
                                                goto end_IL_06c1;
                                        }
                                        goto IL_06d6;
                                    end_IL_06c1:
                                        break;
                                    }
                                    break;
                                }
                            }
                            num = 19;
                            continue;
                        case 4:
                        case 18:
                            num = 22;
                            continue;
                        case 22:
                            return;
                        IL_021b:
                            flag = (byte)num2 != 0;
                            num = 11;
                            continue;
                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string text4 = WapTool.ErrorToString(ex.ToString());
            if (text4.IndexOf("ThreadAbortException") < 0)
            {
                base.Response.Write(text4);
            }
        }
    }
}
