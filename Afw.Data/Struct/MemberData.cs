/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MemberData.cs  
 * 文件功能描述：会员 结构 体
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using System;
using System.Runtime.InteropServices;

namespace Afw.Data
{
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct MemberData
    {

        public int Face3DStatus;

        public int FaceNum;

        public MemberWithFeature memberWithFeature;

    }
}
