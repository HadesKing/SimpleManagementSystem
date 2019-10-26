/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：UserInfo                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.14 19:38:22                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Model.User
{
    public sealed partial class UserInfo
    {

        public String Id { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }

    }
}
