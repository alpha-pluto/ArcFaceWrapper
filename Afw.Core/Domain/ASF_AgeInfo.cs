/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_AgeInfo.cs  
 * 文件功能描述：SDK年龄信息结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 年龄信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_AgeInfo
    {
        /// <summary>
        /// 年龄检测结果集合
        /// 0:未知; >0:年龄
        /// </summary>
        public IntPtr ageArray;

        /// <summary>
        /// 结果集大小
        /// 检测的人脸个数
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int num;
    }
}
