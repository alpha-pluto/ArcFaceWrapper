/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_FaceFeature.cs  
 * 文件功能描述：SDK人脸特征结构体
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
    /// 人脸特征结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct ASF_FaceFeature
    {
        /// <summary>
        /// 特征值 byte[]
        /// </summary>
        public IntPtr feature;

        /// <summary>
        /// 人脸特征长度
        /// 结果集大小
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int featureSize;
    }

    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct MemberWithFeature
    {
        public int Id;

        public int EmployeeNo;

        public string Name;

        public int DeptId;

        public string FaceImagePath;

        public ASF_FaceFeature AsfFaceFeature;
    }

}
