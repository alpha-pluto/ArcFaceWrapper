/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_Face3DAngle.cs  
 * 文件功能描述：3D人脸角度检测结构体
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
    /// 3D人脸角度检测结构体，可参考https://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=1459&page=1&extra=&_dsign=fd9e1a7a
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_Face3DAngle
    {
        /// <summary>
        /// 横滚角
        /// MFloat*
        /// </summary>
        public IntPtr roll;

        /// <summary>
        /// 偏航角
        /// MFloat*
        /// </summary>
        public IntPtr yaw;

        /// <summary>
        /// 俯仰角
        /// MFloat*
        /// </summary>
        public IntPtr pitch;

        /// <summary>
        /// 是否检测成功，0成功，其他为失败
        /// MInt32*
        /// </summary>
        public IntPtr status;

        /// <summary>
        /// 检测的人脸个数
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int num;

    }
}
