/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：UserInfoDataBll                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.14 19:36:42                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.User;

namespace DataBll.User
{
    /// <summary>
    /// 用户信息 数据业务逻辑处理类
    /// </summary>
    public sealed class UserInfoDataBll
    {
        private readonly Dal.User.UserInfoDal dal;

        public UserInfoDataBll()
        {
            dal = new Dal.User.UserInfoDal();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="argId">用户id</param>
        /// <returns></returns>
        public UserInfo Get(Int32 argId)
        {
            UserInfo user = null;
            if (argId > 0)
            {
                user = dal.Get(argId);
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
            if (String.IsNullOrWhiteSpace(argUserName)) return null;
            return dal.Get(argUserName);
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
            if (argPageIndex < 1) argPageIndex = 1;
            return dal.Query(argUserName, argState, argPageIndex, argPageSize);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="argUserInfo"></param>
        /// <returns></returns>
        public bool Update(UserInfo argUserInfo)
        {
            if (null == argUserInfo || argUserInfo.ID < 1) return true;
            return dal.Update(argUserInfo);
        }

    }
}
