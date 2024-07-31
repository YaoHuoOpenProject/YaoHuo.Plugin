﻿# YaoHuo.Plugin

## 插件功能

1. 黑名单功能
2. 对贴子奖赏功能
3. 显示与隐藏勋章，重复购买过滤

## 模块

1. 首页
2. 登录
3. 我的地盘
4. 我的好友（黑名单）
5. 个人空间
6. 帖子详情
7. 帖子回复
8. 货币转账（转账，打赏，手续费）
9. 消息中心（一键阅读）
10. 我的勋章
11. 银行功能
12. 游戏功能（吹牛）

## 开发

<details>
<summary><strong>zgcwkj.aspx 示例</strong></summary>
<div>

```
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zgcwkj.aspx.cs" Inherits="YaoHuo.Plugin.zgcwkj" %>

<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%
    switch (Action)
    {
        case "del":

            break;
        default:

            break;
    }

%>
<%--输出到页面--%>
<%= strhtml %>
```

</div>
</details>

<details>
<summary><strong>zgcwkj.aspx.cs 示例</strong></summary>
<div>

```
using KeLin.ClassManager;
using KeLin.ClassManager.ExUtility;
using KeLin.WebSite;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace YaoHuo.Plugin
{
	public partial class zgcwkj : BasePage
	{
		public string InstanceNameKey = "InstanceName";

		/// <summary>
		/// 实例名称
		/// </summary>
		private string InstanceName
		{
			get
			{
				return PubConstant.GetAppString(InstanceNameKey);
			}
		}

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		private string ConnectionString
		{
			get
			{
				return PubConstant.GetConnectionString(InstanceName);
			}
		}

		/// <summary>
		/// 路由（区分操作类型）
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// 输出到页面的字符串
		/// </summary>
		public StringBuilder strhtml { get; set; }

		/// <summary>
		/// 加载时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			this.Action = GetRequestValue("action");
			var id = 0;//ID
			var count = 0;//数量
			switch (Action)
			{
				case "add"://新增操作
					id = GetRequestValue("action").ToInt();
					count = Delete(id);
					strhtml.Append("新增成功");
					break;

				case "del"://删除操作
					id = GetRequestValue("action").ToInt();
					count = Delete(id);
					strhtml.Append("删除成功");
					break;

				default://默认操作
					var dataTable = GetDataTable();
					foreach (DataRow row in dataTable.Rows)
					{
						string d = row["oper_nickname"].ToString();
						strhtml.Append(d);
					}
					break;
			}
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <returns></returns>
		public DataTable GetDataTable()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 10 oper_nickname from wap_log");
			var dataSet = DbHelperSQL.ExecuteDataset(ConnectionString, CommandType.Text, stringBuilder.ToString());
			if (dataSet.Tables.Count > 0)
			{
				var dataTable = dataSet.Tables[0];
				return dataTable;
			}
			return default(DataTable);
		}

		/// <summary>
		/// 新增数据
		/// </summary>
		/// <returns></returns>
		public int Install()
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
			return 0;
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int Delete(int id)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("delete wap_log where id = @ID");
			var array = new SqlParameter[]
			{
					new SqlParameter("@ID", SqlDbType.Int),
			};
			array[0].Value = id;
			var edCount = DbHelperSQL.ExecuteNonQuery(ConnectionString, CommandType.Text, stringBuilder.ToString(), array);
			return edCount;
		}
	}
}
```

</div>
</details>

## 地址

1. /admin/addtopwapALL.aspx?siteid=1000&classid=0
2. /bbs/admin_WAPadvertise.aspx?action=go&siteid=1000&classid=0
3. /bbs/toMoneylvl.aspx?siteid=1000
4. /bbs/book_list.aspx?action=new
5. /bbs/book_view_add.aspx?classid=305

## 其他

> 逛逛妖火，是我为数不多的乐趣啦！
>
> by zgcwkj
>
> https://yaohuo.me/
