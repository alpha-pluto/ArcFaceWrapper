/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_SingleFaceInfo.cs  
 * 文件功能描述：SDK单人脸信息结构体
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
    /// 单人脸检测结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_SingleFaceInfo
    {
        /// <summary>
        /// 人脸坐标Rect结果
        /// </summary>
        public MRECT faceRect;

        /// <summary>
        /// 人脸角度
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int faceOrient;
    }
}
