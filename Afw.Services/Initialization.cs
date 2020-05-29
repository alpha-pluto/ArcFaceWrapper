/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-17  
 * daniel.zhang
 * zamen@126.com
 * 文件名：Initialization.cs  
 * 文件功能描述：初始化引擎
 * 
----------------------------------------------------------------*/
using Afw.Core;
using Afw.Core.Domain;
using System;

namespace Afw.Services
{
    /// <summary>
    /// 初始化引擎
    /// </summary>
    public class Initialization
    {
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">AF_DETECT_MODE_VIDEO 视频模式 | AF_DETECT_MODE_IMAGE 图片模式</param>
        /// <param name="detectFaceOrientPriority">检测脸部的角度优先值，推荐：ASF_OrientPriority.ASF_OP_0_HIGHER_EXT</param>
        /// <param name="detectFaceScaleVal">用于数值化表示的最小人脸尺寸</param>
        /// <param name="detectFaceMaxNum">最大需要检测的人脸个数</param>
        /// <param name="combinedMask">用户选择需要检测的功能组合，可单个或多个</param>
        /// <param name="ptrEngine">初始化返回的引擎handle</param>
        /// <returns>调用结果</returns>
        public static MError InitialEngine(
            uint detectMode,
            int detectFaceOrientPriority,
            int detectFaceScaleVal,
            int detectFaceMaxNum,
            int combinedMask,
            out IntPtr ptrEngine)
        {
            ptrEngine = IntPtr.Zero;
            var retCode = MError.MERR_UNKNOWN.ToInt();
            try
            {
                retCode = ASFWrapper.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, out ptrEngine);
            }
            catch (Exception ex)
            {
                retCode = MError.MERR_UNKNOWN.ToInt();
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"ASFOnlineActivation Exception : {ex.ToString()}");
            }
            return retCode.ToEnum<MError>();
        }

        /// <summary>
        /// 初始化图像引擎
        /// </summary>
        /// <param name="ptrEngine"></param>
        /// <param name="detectFaceOrientPriority"></param>
        /// <param name="detectFaceScaleVal"></param>
        /// <param name="detectFaceMaxNum"></param>
        /// <param name="combinedMask"></param>
        /// <returns></returns>
        public static MError InitialEngineForImage(
            out IntPtr ptrEngine,
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY,
            int detectFaceScaleVal = 30,
            int detectFaceMaxNum = 5,
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_LIVENESS | FaceEngineMask.ASF_IR_LIVENESS)
        {
            ptrEngine = IntPtr.Zero;
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            return InitialEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, out ptrEngine);
        }

        /// <summary>
        /// 初始化视频引擎
        /// </summary>
        /// <param name="ptrEngine"></param>
        /// <param name="detectFaceOrientPriority"></param>
        /// <param name="detectFaceScaleVal"></param>
        /// <param name="detectFaceMaxNum"></param>
        /// <param name="combinedMask"></param>
        /// <returns></returns>
        public static MError InitialEngineForVideo(
            out IntPtr ptrEngine,
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT,
            int detectFaceScaleVal = 16,
            int detectFaceMaxNum = 5,
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS | FaceEngineMask.ASF_IR_LIVENESS)
        {
            ptrEngine = IntPtr.Zero;
            uint detectMode = DetectionMode.ASF_DETECT_MODE_VIDEO;
            return InitialEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, out ptrEngine);
        }

        public static MError UnintialEngine(IntPtr ptrEngine)
        {
            var retCode = MError.MERR_UNKNOWN.ToInt();
            try
            {
                retCode = ASFWrapper.ASFUninitEngine(ptrEngine);
            }
            catch (Exception ex)
            {
                retCode = MError.MERR_UNKNOWN.ToInt();
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"ASFUninitEngine Exception : {ex.ToString()}");
            }
            return retCode.ToEnum<MError>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptrEngine"></param>
        /// <param name="ptrLivenessThreshold"></param>
        /// <returns></returns>
        public static MError SetLivenessParam(IntPtr ptrEngine, IntPtr ptrLivenessThreshold)
        {
            var retCode = MError.MERR_UNKNOWN.ToInt();
            try
            {
                retCode = Afw.Core.ASFWrapper.ASFSetLivenessParam(ptrEngine, ptrLivenessThreshold);
            }
            catch (Exception ex)
            {
                retCode = MError.MERR_UNKNOWN.ToInt();
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"SetLivenessParam Exception : {ex.ToString()}");
            }
            return retCode.ToEnum<MError>();

        }
    }

}
