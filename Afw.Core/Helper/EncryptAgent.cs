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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Core.Helper
{
    public class EncryptAgent
    {
        #region parameter

        //密钥,必须32位
        protected string sKey = "qJzGXi6hOVLWVJeCnFPGzamenB7NLQM5";
        //向量，必须是12个字符
        protected string sIV = "zamen0X7811=";

        protected EncryptHelper ecrypt;

        #endregion

        #region constructor

        public EncryptAgent()
        {
            ecrypt = new EncryptHelper();
        }

        public EncryptAgent(string key, string vector)
        {
            this.sKey = key;
            this.sIV = vector;
            this.ecrypt = new EncryptHelper();
        }

        #endregion

        #region Encrypt

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        public virtual string Encrypt(string inString)
        {
            return ecrypt.EncryptString(inString ?? "", sKey, sIV);
        }

        #endregion

        #region Decrypt

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        public virtual string Decrypt(string inString)
        {
            return ecrypt.DecryptString(inString ?? "", sKey, sIV);
        }

        #endregion
    }
}
