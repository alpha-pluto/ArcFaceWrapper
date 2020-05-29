/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-17  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceRecognition.cs  
 * 文件功能描述：人脸比对相关
 * 
----------------------------------------------------------------*/
using Afw.Core;
using System;

namespace Afw.Services
{
    public class FaceRecognition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="similarity"></param>
        /// <param name="ptrVideoImageEngine"></param>
        /// <param name="feature"></param>
        /// <param name="feature2"></param>
        /// <returns></returns>
        public static MError FaceFeatureCompare(
            out float similarity,
            IntPtr ptrVideoImageEngine,
            IntPtr feature,
            IntPtr feature2)
        {
            similarity = 0f;

            var retCode = MError.MERR_UNKNOWN.ToInt();
            try
            {
                retCode = ASFWrapper.ASFFaceFeatureCompare(ptrVideoImageEngine, feature, feature2, out similarity);
            }
            catch (Exception ex)
            {
                retCode = MError.MERR_UNKNOWN.ToInt();
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"FaceFeatureCompare Exception : {ex.ToString()}");
            }
            return retCode.ToEnum<MError>();
        }
    }
}
