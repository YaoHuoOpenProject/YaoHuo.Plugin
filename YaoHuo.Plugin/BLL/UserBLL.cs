using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace YaoHuo.Plugin.BLL
{
	/// <summary>
	/// UserBLL
	/// </summary>
	public class UserBLL
    {
		/// <summary>
		/// 实例名称
		/// </summary>
        private string InstanceName { get; set; }

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
        private string ConnectionString { get; set; }

		/// <summary>
		/// UserBLL
		/// </summary>
		/// <param name="InstanceName"></param>
		public UserBLL(string InstanceName)
        {
			this.InstanceName = InstanceName;
			this.ConnectionString = PubConstant.GetConnectionString(InstanceName);
		}

		public DataSet GetList()
        {
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 10 oper_nickname from wap_log");
			var result = DbHelperSQL.ExecuteDataset(ConnectionString, CommandType.Text, stringBuilder.ToString());

			return result;
		}

        public void Add()
        {
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into XinZhang(");
			stringBuilder.Append("ID,XinZhangMingChen,XinZhangTuPian,XinZhangJiaGe,ChuangJianShiJian,siteid,ShiFouMoRen,HangBiaoShi)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@ID,@XinZhangMingChen,@XinZhangTuPian,@XinZhangJiaGe,@ChuangJianShiJian,@siteid,@ShiFouMoRen,@HangBiaoShi)");
			var array = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int),
				new SqlParameter("@XinZhangMingChen", SqlDbType.NVarChar),
				new SqlParameter("@XinZhangTuPian", SqlDbType.NVarChar),
				new SqlParameter("@XinZhangJiaGe", SqlDbType.Int),
				new SqlParameter("@ChuangJianShiJian", SqlDbType.DateTime),
				new SqlParameter("@siteid", SqlDbType.Int),
				new SqlParameter("@ShiFouMoRen", SqlDbType.Bit),
				new SqlParameter("@HangBiaoShi", SqlDbType.Int)
			};
			//array[0].Value = model.ID;
			//array[1].Value = model.XinZhangMingChen;
			//array[2].Value = model.XinZhangTuPian;
			//array[3].Value = model.XinZhangJiaGe;
			//array[4].Value = model.ChuangJianShiJian;
			//array[5].Value = model.siteid;
			//array[6].Value = model.ShiFouMoRen;
			//array[7].Value = model.HangBiaoShi;
			//DbHelperSQL.ExecuteNonQuery(ConnectionString, CommandType.Text, stringBuilder.ToString(), array);
		}
	}
}