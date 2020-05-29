/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-15  
 * daniel.zhang
 * zamen@126.com
 * 文件名：EncryptHelper.cs  
 * 文件功能描述：加密解密处理类
 * 
----------------------------------------------------------------*/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Afw.Core.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EncryptHelper
    {
        //构造一个对称算法
        private SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

        #region 加密解密函数

        /// <summary>
        /// 字符串的加密
        /// </summary>
        /// <param name="Value">要加密的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string Value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;
                mCSP.Key = Convert.FromBase64String(sKey);
                mCSP.IV = Convert.FromBase64String(sIV);
                //指定加密的运算模式
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                //获取或设置加密算法的填充模式
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
                byt = Encoding.UTF8.GetBytes(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ("Error in Encrypting " + ex.Message);
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">加密后的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string Value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;//加密转换运算
                MemoryStream ms;//内存流
                CryptoStream cs;//数据流连接到数据加密转换的流
                byte[] byt;
                //将3DES的密钥转换成byte
                mCSP.Key = Convert.FromBase64String(sKey);
                //将3DES的向量转换成byte
                mCSP.IV = Convert.FromBase64String(sIV);
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//创建对称解密对象
                byt = Convert.FromBase64String(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ("Error in Decrypting " + ex.Message);
            }
        }

        #endregion

        //public string MD5Encrypt(string strText)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
        //    return System.Text.Encoding.Default.GetString(result);
        //}

        public string MD5Encrypt(string strText)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(strText);// System.Text.Encoding.Default.GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');

            }
            return str;


        }
    }
}
