/*
 * ┌────────────────────────────────────────────────┐
 * │　描    述：AesAlgorithm                                                    
 * │　作    者：刘迪                                             
 * │　版    本：1.0                                              
 * │　创建时间：2019.10.16 14:15:23                        
 * └────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Util.EncryptionAlgorithm
{
    /// <summary>
    /// AES 加密算法
    /// 对称加密
    /// </summary>
    public sealed class AesAlgorithm
    {
        private static byte[] _aesKeyByte = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string _aesKeyStr = Encoding.UTF8.GetString(_aesKeyByte);

        /// <summary>
        /// 随机生成密钥
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static byte[] GetIv(Int32 n)
        {
            char[] arrChar = new char[]{
               'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
               '0','1','2','3','4','5','6','7','8','9',
               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
            };

            StringBuilder sbKey = new StringBuilder();

            Random random = new Random(DateTime.Now.Millisecond);
            Int32 tmpRandom;
            for (Int32 i = 0; i < n; i++)
            {
                tmpRandom = random.Next(0, arrChar.Length);
                sbKey.Append(arrChar[tmpRandom].ToString());
            }

            //这里不应该影响现有的加密
            //_aesKetByte = Encoding.UTF8.GetBytes(sbKey.ToString());
            //return _aesKetByte;
            return Encoding.UTF8.GetBytes(sbKey.ToString());
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>密文</returns>
        public static String Encrypt(String Data, String Key, String Vector)
        {
            Byte[] plainBytes = Encoding.UTF8.GetBytes(Data);

            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] Cryptograph = null; // 加密后的密文

            //RijndaelManaged
            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流
                using (MemoryStream Memory = new MemoryStream())
                {
                    // 把内存流对象包装成加密流对象
                    using (CryptoStream Encryptor = new CryptoStream(
                        Memory, Aes.CreateEncryptor(bKey, bVector), CryptoStreamMode.Write))
                    {
                        // 明文数据写入加密流
                        Encryptor.Write(plainBytes, 0, plainBytes.Length);
                        Encryptor.FlushFinalBlock();

                        Cryptograph = Memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }

            return Convert.ToBase64String(Cryptograph);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="Data">被解密的密文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>明文</returns>
        public static String Decrypt(String Data, String Key, String Vector)
        {
            Byte[] encryptedBytes = Convert.FromBase64String(Data);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] original = null; // 解密后的明文

            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流，存储密文
                using (MemoryStream Memory = new MemoryStream(encryptedBytes))
                {
                    // 把内存流对象包装成加密流对象
                    using (CryptoStream Decryptor = new CryptoStream(Memory,
                    Aes.CreateDecryptor(bKey, bVector),
                    CryptoStreamMode.Read))
                    {
                        // 明文存储区
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            Byte[] Buffer = new Byte[1024];
                            Int32 readBytes = 0;
                            while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                            {
                                originalMemory.Write(Buffer, 0, readBytes);
                            }

                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch
            {
                original = null;
            }
            return Encoding.UTF8.GetString(original);
        }

        /// <summary>
        /// AES加密(无向量)
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(String Data, String Key)
        {
            return Encrypt(Data, Key, _aesKeyStr);
        }
        /// <summary>
        /// AES解密(无向量)
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(String Data, String Key)
        {
            return Decrypt(Data, Key, _aesKeyStr);
        }

    }
}

