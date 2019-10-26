using System;

namespace Util
{
    /// <summary>
    /// 序列化接口
    /// </summary>
    /// <remarks>
    /// 说明：
    ///     所有序列化类都需要继承该接口
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2018-12-29</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public interface ISerialize
    {
        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <typeparam name="T">反序列化返回的数据类型</typeparam>
        /// <param name="data">需要进行反序列化的数据</param>
        /// <returns></returns>
        T DeSerialize<T>(String data) where T : class;

        /// <summary>
        /// 序列化数据
        /// </summary>
        /// <typeparam name="T">需要序列化的数据类型</typeparam>
        /// <param name="data">需要序列化的数据</param>
        /// <returns></returns>
        String Serialize<T>(T data) where T : class;


    }
}
