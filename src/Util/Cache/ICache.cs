/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：ICache                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.28 11:30:26                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Cache
{

    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据的类型</typeparam>
        /// <param name="argKey">缓存数据的key</param>
        /// <returns>返回 缓存的数据</returns>
        T Get<T>(String argKey);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存数据的类型</typeparam>
        /// <param name="argKey">缓存数据的key</param>
        /// <param name="argValue">缓存数据的value</param>
        /// <param name="argIsRandomExpireTime">是否随机过期时间，随机的过期时间为180秒至300秒之间</param>
        /// <param name="expireTime">过期时间，默认为300秒，单位：秒</param>
        void Set<T>(String argKey, T argValue, bool argIsRandomExpireTime = true, Int32 expireTime = 300);

    }
}
