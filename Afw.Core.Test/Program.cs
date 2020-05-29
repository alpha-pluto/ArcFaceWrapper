/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：Program.cs  
 * 文件功能描述：
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Afw.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(MError.MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL.GetFieldDescription());
            //Console.WriteLine(MError.MERR_ASF_ACTIVATION_FAIL.GetName());
            //Console.WriteLine((0x16010).ToEnum<MError>());
            //Console.Read();

            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("appId", typeof(string));
            string sdkKey64 = (string)reader.GetValue("sdkKey64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("sdkKey32", typeof(string));

            //var e = WrapperFunctions.ASFOnlineActivation(appId, sdkKey64);

            //Console.WriteLine(e.GetFieldDescription());
            //ASF_GetVersion.TestASF_GetVersion();

            //ASFGetActiveFileInfo.TestGetActiveFileInfo();

            //ASF_Activation.TestASF_Activation();

            //MemberEnroll.TestMemberEnroll();
            //MemberEnroll.Test();


            Console.ReadLine();

        }
    }
}
