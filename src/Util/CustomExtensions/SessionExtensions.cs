using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Util.CustomExtensions
{

    /// <summary>
    /// Session 扩展
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// 设置 Session 值
        /// </summary>
        /// <typeparam name="T">设置数据类型</typeparam>
        /// <param name="session"></param>
        /// <param name="key">Session的key</param>
        /// <param name="value">值</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 从 Session 获取值
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="session"></param>
        /// <param name="key">Session的key</param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

    }
}
