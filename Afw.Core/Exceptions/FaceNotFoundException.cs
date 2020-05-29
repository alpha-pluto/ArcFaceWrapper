/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceNotFoundException.cs  
 * 文件功能描述：未检测到人脸 异常
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afw.Core.Exceptions
{
    /// <summary>
    /// 未检测到人脸
    /// </summary>
    public class FaceNotFoundException : Exception
    {
        public FaceNotFoundException() : base("Face not found")
        {

        }
    }
}
