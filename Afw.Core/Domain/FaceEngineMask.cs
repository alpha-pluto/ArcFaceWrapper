/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceEngineMask.cs  
 * 文件功能描述：引擎方法类型结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 引擎方法类型结构体，在初始化时将用到的类型用|连接传入，如 ASF_NONE|ASF_FACE_DETECT|ASF_FACERECOGNITION
    /// </summary>
    public struct FaceEngineMask
    {
        /// <summary>
        /// 不做方法初始化方法类型
        /// 此处detect可以是tracking或者detection两个引擎之一，具体的选择由detect mode 确定
        /// </summary>
        public const int ASF_NONE = 0x00000000;

        /// <summary>
        /// 人脸检测方法类型常量
        /// </summary>
        public const int ASF_FACE_DETECT = 0x00000001;

        /// <summary>
        /// 人脸识别方法类型常量，包含图片feature提取和feature比对
        /// </summary>
        public const int ASF_FACERECOGNITION = 0x00000004;

        /// <summary>
        /// 年龄检测方法类型常量
        /// </summary>
        public const int ASF_AGE = 0x00000008;

        /// <summary>
        /// 性别检测方法类型常量
        /// </summary>
        public const int ASF_GENDER = 0x00000010;

        /// <summary>
        /// 3D角度检测方法类型常量
        /// </summary>
        public const int ASF_FACE3DANGLE = 0x00000020;

        /// <summary>
        /// RGB活体
        /// </summary>
        public const int ASF_LIVENESS = 0x00000080;

        /// <summary>
        /// 红外活体
        /// </summary>
        public const int ASF_IR_LIVENESS = 0x00000400;
    }
}
