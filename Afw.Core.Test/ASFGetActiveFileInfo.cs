/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASFGetActiveFileInfo.cs  
 * 文件功能描述： 
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using Afw.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Afw.Core.Test
{
    internal class ASFGetActiveFileInfo
    {
        internal static void TestGetActiveFileInfo()
        {
            IntPtr fileInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_ActiveFileInfo>());

            var ret = ASFWrapper.ASFGetActiveFileInfo(fileInfo);

            if (ret.ToEnum<MError>() == MError.MOK && IntPtr.Zero != fileInfo)
            {
                var s = (Core.Domain.ASF_ActiveFileInfo)Marshal.PtrToStructure(fileInfo, typeof(Core.Domain.ASF_ActiveFileInfo));
                var t = typeof(Core.Domain.ASF_ActiveFileInfo);
                System.Reflection.FieldInfo[] mis = t.GetFields();

                foreach (var mi in mis)
                {
                    Console.WriteLine($"{mi.Name} => {mi.GetValue(s)}");
                }
            }
            else
            {
                Console.WriteLine($"ERROR:{ret.ToEnum<MError>()},fileInfo={fileInfo}");
            }
        }
    }
}