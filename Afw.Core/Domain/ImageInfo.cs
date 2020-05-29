/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ImageInfo.cs  
 * 文件功能描述：图片信息类
 * 
----------------------------------------------------------------*/
using System;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 图片信息类
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// 图片的像素数据
        /// </summary>
        public IntPtr imgData { get; set; }

        /// <summary>
        /// 图片像素宽
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 图片像素高
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 图片格式
        /// </summary>
        public int format { get; set; }
    }
}
