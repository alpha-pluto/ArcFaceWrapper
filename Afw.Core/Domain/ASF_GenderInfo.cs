/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_GenderInfo.cs  
 * 文件功能描述：SDK性别结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 性别结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_GenderInfo
    {
        /// <summary>
        /// 性别检测结果集合
        /// </summary>
        public IntPtr genderArray;

        /// <summary>
        /// 结果集大小
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int num;
    }
}
