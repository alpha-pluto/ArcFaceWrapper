/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASF_GetVersion.cs  
 * 文件功能描述：
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using System;
using System.Runtime.InteropServices;

namespace Afw.Core.Test
{
    internal class ASF_GetVersion
    {
        internal static void TestASF_GetVersion()
        {
            IntPtr pImageEngine = IntPtr.Zero;

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_VIDEO;
            //检测脸部的角度优先值
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            var retCode = ASFWrapper.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, out pImageEngine);

            if (pImageEngine != IntPtr.Zero)
            {
                IntPtr v = ASFWrapper.ASFGetVersion(pImageEngine);

                var stuv = (Core.Domain.ASF_VERSION)Marshal.PtrToStructure(v, typeof(Core.Domain.ASF_VERSION));

                Console.WriteLine(stuv.CopyRight);
            }

            Console.WriteLine(retCode.ToEnum<MError>().GetFieldDescription());
        }
    }
}
