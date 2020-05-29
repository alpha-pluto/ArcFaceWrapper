/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_Activation.cs  
 * 文件功能描述：unit test 
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Core.Test
{
    internal class ASF_Activation
    {
        internal static void TestASF_Activation()
        {
            ///MERR_ASF_ALREADY_ACTIVATED MOK
            var errCode = Afw.Services.Activation.ASFActivation();
            Console.WriteLine($"{errCode} => {errCode.GetFieldDescription()}");

            var err = Afw.Services.Activation.ASFOnlineActivation();
            Console.WriteLine($"{err} => {err.GetFieldDescription()}");
        }
    }
}
