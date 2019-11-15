using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models.User
{
    /// <summary>
    /// 用户查询条件
    /// </summary>
    [Serializable]
    public class UserFilterViewModel : Pagination
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public Model.EnumType.UserState? State { get; set; }

    }
}
