/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceTrackUnit.cs  
 * 文件功能描述：视频检测缓存实体类
 * 
----------------------------------------------------------------*/
using System;

namespace Afw.Core.Domain
{
    /// <summary>
    /// 视频检测缓存实体类
    /// </summary>
    public class FaceTrackUnit
    {

        public MRECT Rect { get; set; }
        public IntPtr Feature { get; set; }

        public string message = "";

    }
}
