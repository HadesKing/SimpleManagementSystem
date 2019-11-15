/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：UserInfoDal                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.28 18:01:01                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Model.User;

namespace Dal.User
{
    /* Copyright: liudi
     *
     * Autor: xx
     * Create Time: xx
     *
     * Rewriter: liudi
     * Rewriter Time: 2019.10.28
     * Description: xx
     *
     * Rewriter: xx
     * Rewriter Time: xx
     * Description: xx
     * 
     * 
     * **/

    /// <summary>
    /// UserInfo 数据库表操作类
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-28</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public sealed class UserInfoDal : BaseDal
    {
        protected override string TableName => "UserInfo";

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="argId">用户id</param>
        /// <returns></returns>
        public UserInfo Get(Int32 argId)
        {
            String strSql = $"SELECT * FROM {TableName} WHERE ID = @ID";
            UserInfo user = null;
            using (IDbConnection dbConnection = GetDbConnection())
            {
                user = dbConnection.QueryFirstOrDefault<UserInfo>(strSql, new { ID = argId }, null, CommandTimeOut, CommandType.Text);

                Dispose(dbConnection);
            }
            return user;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="argUserName">用户名</param>
        /// <returns></returns>
        public UserInfo Get(String argUserName)
        {
            String strSql = $"SELECT * FROM {TableName} WHERE UserName = @UserName";
            UserInfo user = null;
            using (IDbConnection dbConnection = GetDbConnection())
            {
                user = dbConnection.QueryFirstOrDefault<UserInfo>(strSql, new { UserName = argUserName }, null, CommandTimeOut, CommandType.Text);

                Dispose(dbConnection);
            }
            return user;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="argUserName">用户名</param>
        /// <param name="argState">用户状态</param>
        /// <param name="argPageIndex">获取第几页的数据</param>
        /// <param name="argPageSize">一页多少条</param>
        /// <returns></returns>
        public (Int32, IList<UserInfo>) Query(String argUserName, Int32 argState, Int32 argPageIndex, Int32 argPageSize)
        {
            IList<String> whereSqlList = new List<String>();
            if (!String.IsNullOrWhiteSpace(argUserName))
            {
                //whereSqlList.Add($" CHARINDEX(@UserName, UserName) > 0 ");        // MS Sql Server
                whereSqlList.Add($" INSTR(UserName, @UserName) > 0 ");      //MYSQL
            }
            if (argState > 0)
            {
                whereSqlList.Add($" State = @State ");
            }
            String strWhereSql = String.Join(" AND ", whereSqlList);

            IList<UserInfo> users = null;
            Int32 totalRow;
            (totalRow, users) = Query<UserInfo>(
                "ID", strWhereSql, new { UserName = argUserName, State = argState }
                , "ID DESC", argPageIndex, argPageSize);
            return (totalRow, users);
        }

        #region 【修改】

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="argUserInfo"></param>
        /// <returns></returns>
        public bool Update(UserInfo argUserInfo)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append($"UPDATE {TableName} ");
            sbSql.Append(@"   
   SET Code = @Code
      ,UserName = @UserName
      ,Password = @Password
      ,State = @State
      ,Description = @Description
      ,Remark = @Remark
      ,LastUpdateTime = @LastUpdateTime
      ,LastUpdator = @LastUpdator
        WHERE
            ID = @ID
            ");
            sbSql.Append("");
            ValidateSql(sbSql.ToString());
            Int32 result = ExecuteNonQuery(sbSql.ToString(), argUserInfo, CommandType.Text);

            return result > 0;
        }

        #endregion


    }
}
