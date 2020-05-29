/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-11  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MRECT.cs  
 * 文件功能描述：人脸框信息结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 人脸框信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MRECT
    {
        public int left;

        public int top;

        public int right;

        public int bottom;
    }
}
