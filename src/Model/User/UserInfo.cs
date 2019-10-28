/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：UserInfo                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.14 19:38:22                        
 * └────────────────────────────────────────────────┘
 */

using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public sealed partial class UserInfo
    {

        public UserInfo()
        {
            CreateTime = System.DateTime.UtcNow;
            LastUpdateTime = System.DateTime.UtcNow;
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// 编码，备份字段
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserState State { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String Creator { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后更新的操作人
        /// </summary>
        public String LastUpdator { get; set; }

    }
}
