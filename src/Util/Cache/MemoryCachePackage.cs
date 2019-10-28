/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：MemoryCache                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.28 10:59:15                        
 * └────────────────────────────────────────────────┘
 */

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Cache
{

    /// <summary>
    /// 内存缓存
    /// </summary>
    public sealed class MemoryCachePackage
    {

        private static IMemoryCache m_memoryCache;

        static MemoryCachePackage()
        {
            MemoryCacheOptions memoryCacheOptions = new MemoryCacheOptions();
            /*
             * https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/memory?view=aspnetcore-3.0
             * 使用 SetSize、Size 和 SizeLimit 限制缓存大小 说明
             * 实例可以选择指定并强制实施大小限制。 
             * 缓存大小限制没有定义的度量单位，因为缓存没有度量条目大小的机制。 
             * 如果设置了缓存大小限制，则所有条目都必须指定 size。 
             * ASP.NET Core 运行时不会根据内存压力限制缓存大小。 
             * 开发人员需要限制缓存大小。 指定的大小以开发人员选择的单位为单位。
             * 
             * 
             * SizeLimit 说明
             * 1. 如果未设置 SizeLimit，则缓存将不受限制 
             * 2. 当系统内存不足时，ASP.NET Core 运行时不会剪裁缓存。
             * 3. 没有单位。如果已设置缓存大小限制，则缓存条目必须以其认为最适合的任何单位指定大小。
             * 4. 如果缓存条目大小的总和超出 SizeLimit 指定的值，则不会缓存条目。
             * 5. 如果未设置任何缓存大小限制，则将忽略在该项上设置的缓存大小。
             * 6. 如果可用内存有限，请调用 Compact 或 Remove：
             */
            //memoryCacheOptions.SizeLimit = 1024;     //获取或设置缓存的最大大小
            //memoryCacheOptions.ExpirationScanFrequency = 200;       //获取或设置连续扫描过期项之间的最小时间长度
            //memoryCacheOptions.CompactionPercentage = 200;      //获取或设置在超出最大大小时要压缩的缓存量
            m_memoryCache = new MemoryCache(memoryCacheOptions);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据的类型</typeparam>
        /// <param name="argKey">缓存数据的key</param>
        /// <returns>返回 缓存的数据</returns>
        public static T Get<T>(String argKey)
        {
            return m_memoryCache.Get<T>(argKey);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存数据的类型</typeparam>
        /// <param name="argKey">缓存数据的key</param>
        /// <param name="argValue">缓存数据的value</param>
        /// <param name="argIsRandomExpireTime">是否随机过期时间，随机的过期时间为180秒至300秒之间</param>
        /// <param name="expireTime">过期时间，默认为300秒，单位：秒</param>
        public static void Set<T>(String argKey, T argValue, bool argIsRandomExpireTime = true, Int32 expireTime = 300)
        {
            if (argIsRandomExpireTime)
            {
                expireTime = new Random().Next(180, 300);       //3-5分钟之间进行随机
            }
            m_memoryCache.Set(argKey, argValue, TimeSpan.FromSeconds(expireTime));
        }

    }
}
