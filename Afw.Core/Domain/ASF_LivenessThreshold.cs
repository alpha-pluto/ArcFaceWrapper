/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_LivenessThreshold.cs  
 * 文件功能描述：活体置信度
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
    /// 活体置信度
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ASF_LivenessThreshold
    {
        /// <summary>
        /// RGB 活体置信度
        /// </summary>
        public float thresholdmodel_BGR;

        /// <summary>
        /// IR 活体置信度
        /// </summary>
        public float thresholdmodel_IR;
    }
}
