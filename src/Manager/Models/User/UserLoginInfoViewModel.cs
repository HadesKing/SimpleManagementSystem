using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models.User
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-10-14</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public sealed class UserLoginInfoViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [Range(6, 12, ErrorMessage = "请输入6~12个字符")]
        public String UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [Range(6, 12, ErrorMessage = "请输入6~12个字符")]
        public String Password { get; set; }

        //public String ReturnUrl { get; set; }

        public bool RememberMe { get; set; }

    }
}
