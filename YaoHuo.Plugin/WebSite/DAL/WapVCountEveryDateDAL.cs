using KeLin.ClassManager;
using KeLin.ClassManager.DAL;
using KeLin.ClassManager.ExUtility;
using KeLin.ClassManager.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace YaoHuo.Plugin.WebSite
{
    public class WapVCountEveryDateDAL : wap_vcount_everyDate_DAL
    {
        private string selfB = null;

        public WapVCountEveryDateDAL(string InstanceName) : base(InstanceName)
        {
            selfB = PubConstant.GetConnectionString(InstanceName);
        }

        public new wap_vcount_everyDate_Model GetModel_Today(long siteid, long types)
        {
            //防止统计日期重复
            //DbHelperSQL.ExecuteDataset(selfB, CommandType.Text, "delete  a from  wap_vcount_everydate a where siteid=" + siteid + " and types=" + types + " and   exists(select 1 from wap_vcount_everydate where siteid=" + siteid + " and types=" + types + " and DateDiff(dd,everyDate,getdate()) =0 and DateDiff(dd,everyDate,getdate())=DateDiff(dd,a.everyDate,getdate()) and ID<a.ID)");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 id,siteid,types,everyDate,PV,UV,VV,IP,SH_google,SH_soso,SH_baidu,SH_sogou,SH_yahoo,SH_bing,SH_youdao,SH_gougou,CT_beijing,CT_shanghai,CT_guangzhou,CT_shenzhen,NT_ChinaMobile,NT_ChinaUnicom,NT_ChinaTelecom,BS_Safari,BS_Chrome,BS_Opera,BS_IE,BS_UC,BS_QQ,HangBiaoShi from wap_vcount_everyDate ");
            stringBuilder.Append(" where siteid=@siteid and types=@types and DateDiff(dd,everyDate,getdate())=0 ");
            SqlParameter[] array = new SqlParameter[2]
            {
            new SqlParameter("@siteid", SqlDbType.BigInt),
            new SqlParameter("@types", SqlDbType.BigInt)
            };
            array[0].Value = siteid;
            array[1].Value = types;
            wap_vcount_everyDate_Model wap_vcount_everyDate_Model = new wap_vcount_everyDate_Model();
            DataSet dataSet = DbHelperSQL.ExecuteDataset(selfB, CommandType.Text, stringBuilder.ToString(), array);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.id = long.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["siteid"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.siteid = long.Parse(dataSet.Tables[0].Rows[0]["siteid"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["types"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.types = long.Parse(dataSet.Tables[0].Rows[0]["types"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["everyDate"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.everyDate = DateTime.Parse(dataSet.Tables[0].Rows[0]["everyDate"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["PV"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.PV = long.Parse(dataSet.Tables[0].Rows[0]["PV"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["UV"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.UV = long.Parse(dataSet.Tables[0].Rows[0]["UV"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["VV"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.VV = long.Parse(dataSet.Tables[0].Rows[0]["VV"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["IP"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.IP = long.Parse(dataSet.Tables[0].Rows[0]["IP"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_google"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_google = long.Parse(dataSet.Tables[0].Rows[0]["SH_google"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_soso"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_soso = long.Parse(dataSet.Tables[0].Rows[0]["SH_soso"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_baidu"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_baidu = long.Parse(dataSet.Tables[0].Rows[0]["SH_baidu"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_sogou"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_sogou = long.Parse(dataSet.Tables[0].Rows[0]["SH_sogou"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_yahoo"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_yahoo = long.Parse(dataSet.Tables[0].Rows[0]["SH_yahoo"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_bing"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_bing = long.Parse(dataSet.Tables[0].Rows[0]["SH_bing"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_youdao"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_youdao = long.Parse(dataSet.Tables[0].Rows[0]["SH_youdao"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["SH_gougou"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.SH_gougou = long.Parse(dataSet.Tables[0].Rows[0]["SH_gougou"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["CT_beijing"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.CT_beijing = long.Parse(dataSet.Tables[0].Rows[0]["CT_beijing"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["CT_shanghai"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.CT_shanghai = long.Parse(dataSet.Tables[0].Rows[0]["CT_shanghai"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["CT_guangzhou"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.CT_guangzhou = long.Parse(dataSet.Tables[0].Rows[0]["CT_guangzhou"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["CT_shenzhen"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.CT_shenzhen = long.Parse(dataSet.Tables[0].Rows[0]["CT_shenzhen"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["NT_ChinaMobile"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.NT_ChinaMobile = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaMobile"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["NT_ChinaUnicom"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.NT_ChinaUnicom = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaUnicom"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["NT_ChinaTelecom"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.NT_ChinaTelecom = long.Parse(dataSet.Tables[0].Rows[0]["NT_ChinaTelecom"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_Safari"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_Safari = long.Parse(dataSet.Tables[0].Rows[0]["BS_Safari"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_Chrome"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_Chrome = long.Parse(dataSet.Tables[0].Rows[0]["BS_Chrome"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_Opera"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_Opera = long.Parse(dataSet.Tables[0].Rows[0]["BS_Opera"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_IE"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_IE = long.Parse(dataSet.Tables[0].Rows[0]["BS_IE"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_UC"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_UC = long.Parse(dataSet.Tables[0].Rows[0]["BS_UC"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["BS_QQ"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.BS_QQ = long.Parse(dataSet.Tables[0].Rows[0]["BS_QQ"].ToString());
                }

                if (dataSet.Tables[0].Rows[0]["HangBiaoShi"].ToString() != "")
                {
                    wap_vcount_everyDate_Model.HangBiaoShi = long.Parse(dataSet.Tables[0].Rows[0]["HangBiaoShi"].ToString());
                }

                return wap_vcount_everyDate_Model;
            }

            return null;
        }
    }
}
