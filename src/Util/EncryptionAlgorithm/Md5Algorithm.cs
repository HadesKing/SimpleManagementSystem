using System;
using System.Security.Cryptography;
using System.Text;

namespace Util.EncryptionAlgorithm
{

    /// <summary>
    /// MD5 算法
    /// </summary>
    public class Md5Algorithm
    {

        #region 【不可用】

        /*
         * 说明：
         *      不要采用这种算法，因为当待加密字符串超出一定长度之后，加密值不变
         */

        ///// <summary>
        ///// MD5 16位加密
        ///// 
        ///// </summary>
        ///// <param name="argString">待加密字符串</param>
        ///// <returns></returns>
        //public static String Encryption(String argString)
        //{
        //    MD5 md5Hasher = MD5CryptoServiceProvider.Create();
        //    byte[] result = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(argString));

        //    String strEncryptionString = BitConverter.ToString(md5Hasher.ComputeHash(result, 4, 8));
        //    return strEncryptionString.Replace("-", "");
        //}

        //// Hash an input string and return the hash as
        //// a 32 character hexadecimal string.

        ///// <summary>
        ///// MD5 32位加密
        ///// </summary>
        ///// <param name="argString"></param>
        ///// <returns></returns>
        //public static string Encryption32(String argString)
        //{
        //    // Create a new instance of the MD5CryptoServiceProvider object.
        //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

        //    // Convert the input string to a byte array and compute the hash.
        //    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(argString));

        //    // Create a new Stringbuilder to collect the bytes
        //    // and create a string.
        //    StringBuilder sBuilder = new StringBuilder();

        //    // Loop through each byte of the hashed data 
        //    // and format each one as a hexadecimal string.
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }

        //    // Return the hexadecimal string.
        //    return sBuilder.ToString();
        //}

        /////// <summary>
        /////// 解密
        /////// </summary>
        /////// <param name="argString"></param>
        /////// <returns></returns>
        ////public String Decrypt(String argString)
        ////{

        ////} 
        #endregion


        /// <summary>
        /// md5算法 采用的加密算法
        /// 
        /// 说明：
        ///     32 位小写加密算法（如需要大写，可以将返回结果改成大写）
        ///     默认编码格式是UTF8
        /// 适用于PHP和JAVA等语言
        /// </summary>
        /// <param name="argString"></param>
        /// <returns></returns>
        public static String Encryption32(String argString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(argString);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            StringBuilder sbString = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sbString.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));        //不足16位的使用0补充
            }
            String strString = sbString.ToString();
            return strString.PadLeft(32, '0');        //不足32位的使用0补充
            //string ret = "";
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            //}
            //return ret.PadLeft(32, '0');
        }

    }
}
