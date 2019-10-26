using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Return
{

    /// <summary>
    /// 返回值
    /// </summary>
    /// <typeparam name="T">返回数据类型</typeparam>
    public sealed class ReturnValue<T> : BaseReturn
    {

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

    }
}
