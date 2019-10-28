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
using System.Text;

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

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected String DbConnectString { get; set; }

        /// <summary>
        /// 需要操作的表名称
        /// </summary>
        protected abstract String TableName { get; }

        //protected Int32 ExcuteNonQuery()
        //{

        //}


    }
}
