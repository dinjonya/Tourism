using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DinJonYa.Plugs.Strings
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public static class EncryptionAndDecryption
    {
        // Fields
        private static byte[] _key1 = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef, 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
        private static string Key = "abcdef1234567890";

        // Methods
        public static string Decrypt(string showText)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(showText);
                SymmetricAlgorithm algorithm = Rijndael.Create();
                algorithm.Key = Encoding.UTF8.GetBytes(Key);
                algorithm.IV = _key1;
                byte[] buffer2 = new byte[buffer.Length];
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        stream2.Read(buffer2, 0, buffer2.Length);
                        stream2.Close();
                        stream.Close();
                    }
                }
                return Encoding.UTF8.GetString(buffer2).Replace("\0", "");
            }
            catch
            {
                return "";
            }
        }

        public static string Encrypt(string plainText)
        {
            try
            {
                SymmetricAlgorithm algorithm = Rijndael.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                algorithm.Key = Encoding.UTF8.GetBytes(Key);
                algorithm.IV = _key1;
                byte[] inArray = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        stream2.Write(bytes, 0, bytes.Length);
                        stream2.FlushFinalBlock();
                        inArray = stream.ToArray();
                        stream2.Close();
                        stream.Close();
                    }
                }
                return Convert.ToBase64String(inArray);
            }
            catch
            {
                return "";
            }
        }
    }
}
