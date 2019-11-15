using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models.User
{
    public sealed partial class UserInfoViewModel
    {


        /// <summary>
        /// 唯一标识
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 编码，备份字段
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public String State { get; set; }

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
        public String CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String Creator { get; set; }

        ///// <summary>
        ///// 最后更新时间
        ///// </summary>
        //public DateTime LastUpdateTime { get; set; }

        ///// <summary>
        ///// 最后更新的操作人
        ///// </summary>
        //public String LastUpdator { get; set; }

    }
}
