/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_LivenessInfo.cs  
 * 文件功能描述：活体信息
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
    /// 活体信息
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_LivenessInfo
    {
        /// <summary>
        /// 0:非真人； 1:真人； -1：不确定； -2:传入人脸数>1
        /// MInt32*
        /// </summary>
        public IntPtr isLive;

        /// <summary>
        /// 检测的人脸个数
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int num;
    }
}
