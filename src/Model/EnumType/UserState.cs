/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：UserState                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.28 17:47:56                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EnumType
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {

        [Description("禁用")]
        Disable = 0,

        [Description("正常")]
        Normal = 1

    }
}
