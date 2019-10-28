using System;
using System.Collections.Generic;
using System.Text;

namespace Log
{
    public class LogFactory
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static ILog GetDefaultInstance()
        {
            return new NLogLogger();
        }

    }
}
