using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using KeLin.WebSite;
using System;
using System.Data;
using YaoHuo.Plugin.Tool;

public class MyFile : PageWap
{
    public string INFO = "";

    public string ERROR = "";

    public string strHtml = "";

    public string strShowHtml = "";

    public string messagecount = "0";

    public string messageAll = "0";

    public string goodfriend = "0";

    public string type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Discarded unreachable code: IL_006a
        while (true)
        {
            base.IsLogin(base.userid, "myfile.aspx?siteid=" + base.siteid);
            bool flag;
            flag = !(WapTool.getArryString(base.siteVo.Version, '|', 54) == "1");
            int num;
            num = 1;
            while (true)
            {
                switch (num)
                {
                    case 1:
                        if (true)
                        {
                        }
                        if (!flag)
                        {
                            num = 0;
                            continue;
                        }
                        goto case 3;
                    case 2:
                        try
                        {
                            while (true)
                            {
                            IL_00f1:
                                this.strHtml = base.siteVo.siterowremark;
                                string connectionString;
                                connectionString = PubConstant.GetConnectionString(PubConstant.GetAppString("InstanceName"));
                                DataSet dataSet;
                                dataSet = DbHelperSQL.ExecuteDataset(connectionString, CommandType.Text, "select count(id),(select count(id) from wap_message where siteid=" + base.siteid + " and isnew<>2 and touserid=" + base.userid + ") from wap_message where siteid=" + base.siteid + " and isnew=1 and  touserid=" + base.userid);
                                num = 10;
                                while (true)
                                {
                                    int num3;
                                    int num2;
                                    switch (num)
                                    {
                                        case 10:
                                            num = ((dataSet == null) ? 9 : 18);
                                            continue;
                                        case 8:
                                            num = 1;
                                            continue;
                                        case 1:
                                            num3 = ((dataSet.Tables[0].Rows.Count <= 0) ? 1 : 0);
                                            goto IL_0382;
                                        case 0:
                                            this.messagecount = dataSet.Tables[0].Rows[0][0].ToString();
                                            num = 7;
                                            continue;
                                        case 16:
                                            dataSet = DbHelperSQL.ExecuteDataset(connectionString, CommandType.Text, "select count(id) from wap_friends where friendtype=0 and userid=" + base.userid);
                                            num = 6;
                                            continue;
                                        case 6:
                                            num = ((dataSet == null) ? 21 : 8);
                                            continue;
                                        case 9:
                                            num2 = 1;
                                            goto IL_03c0;
                                        case 4:
                                            flag = dataSet.Tables[0].Rows[0][0] == null;
                                            num = 3;
                                            continue;
                                        case 3:
                                            if (!flag)
                                            {
                                                num = 20;
                                                continue;
                                            }
                                            goto case 23;
                                        case 13:
                                            num = 16;
                                            continue;
                                        case 7:
                                            flag = dataSet.Tables[0].Rows[0][1] == null;
                                            num = 14;
                                            continue;
                                        case 14:
                                            if (!flag)
                                            {
                                                num = 15;
                                                continue;
                                            }
                                            goto case 13;
                                        case 12:
                                            flag = dataSet.Tables[0].Rows[0][0] == null;
                                            num = 19;
                                            continue;
                                        case 19:
                                            if (!flag)
                                            {
                                                num = 0;
                                                continue;
                                            }
                                            goto case 7;
                                        case 15:
                                            this.messageAll = dataSet.Tables[0].Rows[0][1].ToString();
                                            num = 13;
                                            continue;
                                        case 11:
                                            if (!flag)
                                            {
                                                num = 4;
                                                continue;
                                            }
                                            goto case 5;
                                        case 23:
                                            num = 5;
                                            continue;
                                        case 22:
                                            if (!flag)
                                            {
                                                num = 12;
                                                continue;
                                            }
                                            goto case 16;
                                        case 21:
                                            num3 = 1;
                                            goto IL_0382;
                                        case 20:
                                            this.goodfriend = dataSet.Tables[0].Rows[0][0].ToString();
                                            num = 23;
                                            continue;
                                        case 18:
                                            num = 2;
                                            continue;
                                        case 2:
                                            num2 = ((dataSet.Tables[0].Rows.Count <= 0) ? 1 : 0);
                                            goto IL_03c0;
                                        case 5:
                                            base.addMoneyToMyBank();
                                            num = 17;
                                            continue;
                                        case 17:
                                            goto end_IL_0088;
                                        IL_0382:
                                            flag = (byte)num3 != 0;
                                            num = 11;
                                            continue;
                                        IL_03c0:
                                            flag = (byte)num2 != 0;
                                            num = 22;
                                            continue;
                                    }
                                    goto IL_00f1;
                                end_IL_0088:
                                    break;
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ERROR = WapTool.ErrorToString(ex.ToString());
                        }
                        base.VisiteCount("进入我的地盘。");
                        return;

                    case 3:
                        this.type = WapTool.GetSiteDefault(base.siteVo.Version, 27);
                        num = 2;
                        continue;
                    case 0:
                        base.needPassWordToAdmin();
                        num = 3;
                        continue;
                }
                break;
            }
        }
    }
}
