/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_ActiveFileInfo.cs  
 * 文件功能描述：SDK激活文件信息结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;

namespace Afw.Core.Domain
{
    /// <summary>
    /// SDK激活文件信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_ActiveFileInfo
    {
        /// <summary>
        /// SDK 开始时间
        /// </summary>
        public string startTime;

        /// <summary>
        /// SDK 截止时间
        /// </summary>
        public string endTime;

        /// <summary>
        /// 平台版本
        /// </summary>
        public string platform;

        /// <summary>
        /// SDK 类型
        /// </summary>
        public string sdkType;

        /// <summary>
        /// APPID
        /// </summary>
        public string appId;

        /// <summary>
        /// SDKKEY
        /// </summary>
        public string sdkKey;

        /// <summary>
        /// SDK 版本号
        /// </summary>
        public string sdkVersion;

        /// <summary>
        /// 激活文件版本号
        /// </summary>
        public string fileVersion;
    }
}
