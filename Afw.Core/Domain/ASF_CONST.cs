/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_CONST.cs  
 * 文件功能描述：ASF常量
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 人脸检测方向
    /// 推荐ASF_OP_0_HIGHER_EXT
    /// 注：IMAGE 模式下为了提高检测识别率，不支持 ASF_OP_0_HIGHER_EXT 检测
    /// </summary>
    public struct ASF_OrientPriority
    {
        /// <summary>
        /// 仅检测 0 度
        /// </summary>
        [Description("仅检测 0 度")]
        public const int ASF_OP_0_ONLY = 0x1;

        /// <summary>
        /// 仅检测 90 度
        /// </summary>
        [Description("仅检测 90 度")]
        public const int ASF_OP_90_ONLY = 0x2;

        /// <summary>
        /// 仅检测 270 度
        /// </summary>
        [Description("仅检测 270 度")]
        public const int ASF_OP_270_ONLY = 0x3;

        /// <summary>
        /// 仅检测 180 度
        /// </summary>
        [Description("仅检测 180 度")]
        public const int ASF_OP_180_ONLY = 0x4;

        /// <summary>
        /// 全角度检测
        /// </summary>
        [Description("全角度检测")]
        public const int ASF_OP_0_HIGHER_EXT = 0x5;
    }

    /// <summary>
    /// 检测到的人脸角度（按逆时针方向）
    /// </summary>
    public struct ASF_OrientCode
    {
        /// <summary>
        /// 0 度
        /// </summary>
        [Description("0 度")]
        public const int ASF_OC_0 = 0x1;

        /// <summary>
        /// 90 度
        /// </summary>
        [Description("90 度")]
        public const int ASF_OC_90 = 0x2;

        /// <summary>
        /// 270 度
        /// </summary>
        [Description("270 度")]
        public const int ASF_OC_270 = 0x3;

        /// <summary>
        /// 180 度
        /// </summary>
        [Description("180 度")]
        public const int ASF_OC_180 = 0x4;

        /// <summary>
        /// 30 度
        /// </summary>
        [Description("30 度")]
        public const int ASF_OC_30 = 0x5;

        /// <summary>
        /// 60 度
        /// </summary>
        [Description("60 度")]
        public const int ASF_OC_60 = 0x6;

        /// <summary>
        /// 120 度
        /// </summary>
        [Description("120 度")]
        public const int ASF_OC_120 = 0x7;

        /// <summary>
        /// 150 度
        /// </summary>
        [Description("150 度")]
        public const int ASF_OC_150 = 0x8;

        /// <summary>
        /// 210 度
        /// </summary>
        [Description("210 度")]
        public const int ASF_OC_210 = 0x9;

        /// <summary>
        /// 240 度
        /// </summary>
        [Description("240 度")]
        public const int ASF_OC_240 = 0xa;

        /// <summary>
        /// 300 度
        /// </summary>
        [Description("300 度")]
        public const int ASF_OC_300 = 0xb;

        /// <summary>
        /// 330 度
        /// </summary>
        [Description("330 度")]
        public const int ASF_OC_330 = 0xc;
    }

    /// <summary>
    /// 图片检测模式
    /// </summary>
    public struct DetectionMode
    {
        /// <summary>
        /// Video模式，一般用于多帧连续检测
        /// </summary>
        public const uint ASF_DETECT_MODE_VIDEO = 0x00000000;

        /// <summary>
        /// Image模式，一般用于静态图的单次检测
        /// </summary>
        public const uint ASF_DETECT_MODE_IMAGE = 0xFFFFFFFF;
    }
}
