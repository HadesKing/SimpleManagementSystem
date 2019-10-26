/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：TokenHelper                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.16 14:21:10                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    /// <summary>
    /// 登录Token帮助类
    /// </summary>
    public class LoginTokenHelper
    {
        private static String m_key = "liu@di_123";
        private static String m_tokenTemplateSplit = "$_$";
        private static String m_tokenTmeplate = $"UserName{m_tokenTemplateSplit}ExpiredTime";

        /// <summary>
        /// 登录Token名称
        /// </summary>
        public static String LoginTokenName => "MSLoginToken";

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="argUserName">用户名</param>
        /// <param name="argExpiredTime">过期时间，UTC时间</param>
        /// <returns></returns>
        public static String GetToken(String argUserName, DateTime argExpiredTime)
        {
            String strExpiredTime = argExpiredTime.ToString("yyyy-MM-dd HH:mm:ss");
            String strData = m_tokenTmeplate.Replace("UserName", argUserName).Replace("ExpiredTime", strExpiredTime);
            String strToken = EncryptionAlgorithm.AesAlgorithm.Encrypt(strData, m_key);

            return strToken;
        }

        /// <summary>
        /// 判断是否过期
        /// true#已过期，false#未过期
        /// </summary>
        /// <param name="argToken">Token</param>
        /// <returns>true#已过期，false#未过期</returns>
        public static bool IsExpired(String argToken)
        {
            String strData = EncryptionAlgorithm.AesAlgorithm.Decrypt(argToken, m_key);
            String[] arrData = strData.Split(m_tokenTemplateSplit);
            DateTime expiredTime = new DateTime();
            if (arrData.Length == 2 && DateTime.TryParse(arrData[1], out expiredTime))
            {
                expiredTime = DateTime.Parse(arrData[1]);
                DateTime dtNow = System.DateTime.UtcNow;
                if (dtNow.CompareTo(expiredTime) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="argToken"></param>
        /// <returns></returns>
        public static String GetUserName(String argToken)
        {
            String strData = EncryptionAlgorithm.AesAlgorithm.Decrypt(argToken, m_key);
            String[] arrData = strData.Split(m_tokenTemplateSplit);
            if (arrData.Length == 2)
            {
                return arrData[0];
            }
            return null;
        }

    }
}
