/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：SHAAlgorithm                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.26 16:10:21                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Util.EncryptionAlgorithm
{

    /// <summary>
    /// SHA 算法
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <author>刘迪</author>
    /// <createtime>2019-05-30</createtime>
    /// <updator></updator>
    /// <updatetime></updatetime>
    /// <description></description>
    public sealed class SHAAlgorithm
    {
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="argContent">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public static string SHA1(String argContent)
        {
            return SHA1(argContent, Encoding.UTF8);
        }

        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="argContent">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(String argContent, Encoding encode)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = encode.GetBytes(argContent);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            string result = BitConverter.ToString(bytes_out);
            result = result.Replace("-", "");
            return result;
        }

        /// <summary>
        /// 使用SHA256加密算法
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string SHA256(string strData)
        {
            System.Security.Cryptography.SHA256 sha256 = new
            System.Security.Cryptography.SHA256Managed();
            byte[] sha256Bytes = System.Text.Encoding.UTF8.GetBytes(strData);
            byte[] cryString = sha256.ComputeHash(sha256Bytes);
            string sha256Str = string.Empty;
            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X2");
            }
            return sha256Str;
        }



    }
}

