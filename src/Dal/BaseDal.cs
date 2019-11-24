/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：BaseDal                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.22 17:21:06                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;

namespace Dal
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-22</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public abstract class BaseDal
    {
        public BaseDal()
        {
            DbConnectString = Util.ConfigurationHelper.GetValue(DbConnectStringName);
        }

        /// <summary>
        /// 数据库连接字符串配置节点名称
        /// </summary>
        private String DbConnectStringName
        {
            get
            {
                return "DbConnectString:manager";
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// <remarks></remarks>
        /// <author>liudi</author>
        /// <createtime>2018-12-13</createtime>
        /// <updator></updator>
        /// <updatetime></updatetime>
        /// <description></description>
        protected String DbConnectString { get; set; }

        /// <summary>
        /// SQL语句操作命令执行超时时间
        /// 说明：
        ///     默认5分钟
        /// </summary>
        /// <remarks></remarks>
        /// <author>liudi</author>
        /// <createtime>2018-12-13</createtime>
        /// <updator></updator>
        /// <updatetime></updatetime>
        /// <description></description>
        protected virtual Int32 CommandTimeOut
        {
            get { return 300; }
        }

        /// <summary>
        /// 需要操作的表名称
        /// </summary>
        /// <remarks></remarks>
        /// <author>liudi</author>
        /// <createtime>2018-12-13</createtime>
        /// <updator></updator>
        /// <updatetime></updatetime>
        /// <description></description>
        protected abstract String TableName { get; }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="argIsOpen">是否打开连接</param>
        /// <returns></returns>
        protected IDbConnection GetDbConnection(bool argIsOpen = true)
        {
            IDbConnection dbConnection = null;
            try
            {
                if (String.IsNullOrWhiteSpace(DbConnectString))
                    throw new Exception($"The {DbConnectStringName} database connectstring is null or empty!");

                dbConnection = new MySqlConnection(DbConnectString);
                //打开连接
                if (argIsOpen) dbConnection.Open();
            }
            catch (Exception ex)
            {
                //判定连接是否是打开的状态，如是则关闭
                if (null != dbConnection && dbConnection.State != ConnectionState.Closed)
                {
                    dbConnection.Close();
                }
                //抛出错误
                throw ex;
            }
            return dbConnection;
        }

        protected void Dispose(IDbConnection dbConnection)
        {
            if (null != dbConnection && dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
        }

        /// <summary>
        /// 生成获取行数的SQL
        /// </summary>
        /// <param name="argWhereSql"></param>
        /// <returns></returns>
        protected String GenerateGetRowNumberSql(String argWhereSql)
        {
            StringBuilder sbGetRowNumberSql = new StringBuilder();
            sbGetRowNumberSql.Append("SELECT COUNT(1) AS 'RowNumber' ");
            sbGetRowNumberSql.Append($" FROM {TableName} ");
            sbGetRowNumberSql.Append(" WHERE 1= 1");
            if (!String.IsNullOrWhiteSpace(argWhereSql))
            {
                sbGetRowNumberSql.Append($" AND {argWhereSql} ");
            }
            sbGetRowNumberSql.Append(";");

            return sbGetRowNumberSql.ToString();
        }

        /// <summary>
        /// 生成分页SQL
        /// </summary>
        /// <param name="argPrimaryKeyField">主键字段</param>
        /// <param name="argWhereSql">WHERE 条件SQL 语句</param>
        /// <param name="argSortSql">排序的SQL 语句</param>
        /// <param name="argPageIndex">获取第几页的数据</param>
        /// <param name="argPageSize">一页多少条</param>
        /// <returns></returns>
        protected String GeneratePagingSql(
            String argPrimaryKeyField
            , String argWhereSql, String argSortSql
            , Int32 argPageIndex, Int32 argPageSize
            )
        {
            Int32 startRowNumber = (argPageIndex - 1) * argPageSize;
            Int32 endRowNumber = argPageIndex * argPageSize;

            StringBuilder sbSql = new StringBuilder();
            #region 【SQL SERVER】
            /* SQL SERVER */
            //sbSql.Append("SELECT * ");
            //sbSql.Append("FROM ");
            //sbSql.Append("	( ");
            //sbSql.Append($"		SELECT ROW_NUMBER() OVER(ORDER BY {argSortSql}) AS 'RowNumber', {argPrimaryKeyField} ");
            //sbSql.Append("		FROM ");
            //sbSql.Append($"			{TableName} WITH(NOLOCK) ");
            //sbSql.Append(" WHERE 1 = 1 ");
            //if (!String.IsNullOrWhiteSpace(argWhereSql))
            //{
            //    sbSql.Append($" AND {argWhereSql} ");
            //}
            //sbSql.Append("	) AS A ");
            //sbSql.Append($"	INNER JOIN {TableName} AS B WITH(NOLOCK) ON B.{argPrimaryKeyField} = A.{argPrimaryKeyField} ");
            //sbSql.Append(" WHERE ");
            //sbSql.Append($"	A.RowNumber > {startRowNumber}");
            //sbSql.Append(" AND ");
            //sbSql.Append($"	A.RowNumber <= {endRowNumber}");
            //sbSql.Append(""); 
            #endregion

            #region 【MYSQL】
            /* MYSQL */
            sbSql.Append(" SELECT * ");
            sbSql.Append($" FROM {TableName} AS A ");
            sbSql.Append($" INNER JOIN ( ");
            sbSql.Append($"          SELECT {argPrimaryKeyField} ");
            sbSql.Append($"          FROM {TableName} ");
            sbSql.Append("           WHERE 1 = 1 ");
            if (!String.IsNullOrWhiteSpace(argWhereSql))
            {
                sbSql.Append($"         AND {argWhereSql} ");
            }
            sbSql.Append($"          ORDER BY {argSortSql} ");
            sbSql.Append($"          LIMIT {(argPageIndex - 1) * argPageSize }, {argPageSize}");
            sbSql.Append("  ");
            sbSql.Append($"  ) AS B ON A.{argPrimaryKeyField} = B.{argPrimaryKeyField} ");

            #endregion

            return sbSql.ToString();
        }

        public (Int32, IList<T>) Query<T>(
            String argPrimaryKeyField
            , String argWhereSql, Object argWhereParameters
            , String argSortSql
            , Int32 argPageIndex, Int32 argPageSize)
        {
            String strGetRowNumberSql = GenerateGetRowNumberSql(argWhereSql);
            String strSql = GeneratePagingSql(argPrimaryKeyField, argWhereSql, argSortSql, argPageIndex, argPageSize);

            IList<T> users = null;
            Int32 totalRow = 0;
            using (IDbConnection dbConnection = GetDbConnection())
            {
                IDbTransaction dbTransaction = null;
                try
                {
                    dbTransaction = dbConnection.BeginTransaction();
                    totalRow = dbConnection.ExecuteScalar<Int32>(strGetRowNumberSql, argWhereParameters, dbTransaction, CommandTimeOut, CommandType.Text);
                    if (totalRow > 0)
                    {
                        users = dbConnection.Query<T>(strSql, argWhereParameters, dbTransaction, false, CommandTimeOut, CommandType.Text).AsList();
                    }
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (null != dbTransaction) dbTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    Dispose(dbConnection);
                }
            }
            return (totalRow, users);
        }

        /// <summary>
        /// 执行SQL，返回受影响的行数
        /// </summary>
        /// <param name="argSql">sql 语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">执行SQL命令类型</param>
        /// <returns>返回受影响的行数</returns>
        protected Int32 ExecuteNonQuery(String argSql, object param = null, CommandType? commandType = null)
        {
            Int32 result = 0;
            using (IDbConnection dbConnection = GetDbConnection())
            {
                result = dbConnection.Execute(argSql, param, null, CommandTimeOut, commandType);

                Dispose(dbConnection);
            }
            return result;
        }

        /// <summary>
        /// 执行SQL，返回受影响的行数
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="argSql">sql 语句</param>
        /// <param name="param">参数</param>
        /// <param name="dbTransaction">事务</param>
        /// <param name="commandType">执行SQL命令类型</param>
        /// <returns>返回受影响的行数</returns>
        protected Int32 ExecuteNonQuery(
            IDbConnection dbConnection
            , String argSql, object param = null, IDbTransaction dbTransaction = null, CommandType? commandType = null)
        {
            return dbConnection.Execute(argSql, param, dbTransaction, CommandTimeOut, commandType);
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">事务</param>
        /// <param name="argSql">sql 语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType">执行SQL命令类型</param>
        /// <returns></returns>
        protected Int32 ExecuteTransaction(
            IDbConnection dbConnection, IDbTransaction dbTransaction
            , String argSql, object param = null, CommandType? commandType = null)
        {
            return dbConnection.Execute(argSql, param, dbTransaction, CommandTimeOut, commandType);
        }
        
        /// <summary>
        /// 验证 SQL 语句
        /// 验证逻辑：
        ///     修改、删除操作必须有where条件
        /// </summary>
        protected void ValidateSql(String argSql)
        {
            if(!String.IsNullOrWhiteSpace(argSql))
            {
                String strSql = argSql.ToLower();
                if (
                    (argSql.IndexOf("update") > -1
                    || argSql.IndexOf("delete") > -1
                    ) && argSql.IndexOf("where") <= 0
                    )
                {
                    throw new Exception($"The where condition is not included in the T-sql sentence.{argSql}");
                }
            }

        }

    }
}
