/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceDetectException.cs  
 * 文件功能描述：人脸检测异常
 * 
----------------------------------------------------------------*/
using System;

namespace Afw.Core.Exceptions
{
    /// <summary>
    /// 人脸检测异常
    /// </summary>
    public class FaceDetectException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public MError ErrCode;

        public FaceDetectException(MError errCode) : base(errCode.GetName())
        {
            ErrCode = errCode;
        }
    }
}
