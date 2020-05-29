/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_MultiFaceInfo.cs  
 * 文件功能描述：SDK多人脸检测结构体
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
    /// 多人脸检测结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_MultiFaceInfo
    {
        /// <summary>
        /// 人脸Rect结果集
        /// </summary>
        public IntPtr faceRects;

        /// <summary>
        /// 人脸角度结果集，与faceRects一一对应  对应ASF_OrientCode
        /// </summary>
        public IntPtr faceOrients;

        /// <summary>
        /// 检测到的人脸个数
        /// 结果集大小
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int faceNum;

        /// <summary>
        /// 在 VIDEO 模式下有效， IMAGE 模式下为空
        /// </summary>
        public IntPtr faceId;
    }
}
