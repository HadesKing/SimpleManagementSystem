using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Util.JsonSerialize
{

    /// <summary>
    /// 使用 Newtonsoft.Json 进行数据序列化
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2018-12-29</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public sealed class NewtonsoftJsonSerialize : IJsonSerialize
    {

        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <typeparam name="T">反序列化返回的数据类型</typeparam>
        /// <param name="data">需要进行反序列化的数据</param>
        /// <returns></returns>
        public T DeSerialize<T>(String data) where T : class
        {
            Object obj = JsonConvert.DeserializeObject<T>(data);
            return obj as T;
        }


        /// <summary>
        /// 序列化数据
        /// </summary>
        /// <typeparam name="T">需要序列化的数据类型</typeparam>
        /// <param name="data">需要序列化的数据</param>
        /// <returns></returns>
        public string Serialize<T>(T data) where T : class
        {
            return JsonConvert.SerializeObject(data);
        }

    }
}
