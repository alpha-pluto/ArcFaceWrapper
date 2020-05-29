/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_VERSION.cs  
 * 文件功能描述：SDK版本信息结构体
 * 
----------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;


namespace Afw.Core.Domain
{
    /// <summary>
    /// SDK版本信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_VERSION
    {
        // 版本号
        public string Version;

        // 构建日期
        public string BuildDate;

        // Copyright
        public string CopyRight;
    }
}
