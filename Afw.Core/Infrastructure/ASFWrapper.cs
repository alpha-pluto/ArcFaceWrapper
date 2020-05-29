/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：ASFFunctions.cs  
 * 文件功能描述：SDK函数调用入口声明
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using System;
using System.Runtime.InteropServices;

namespace Afw.Core
{
    public partial class ASFWrapper
    {
        /// <summary>
        /// SDK动态链接库路径
        /// </summary>
        public const string DLL_ENGINE_PATH = "libarcsoft_face_engine.dll";

        #region ASVL

        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ASVL_GetVersion();

        #endregion

        #region initialization

        /// <summary>
        /// 激活人脸识别SDK引擎函数
        /// </summary>
        /// <param name="appId">SDK对应的AppID</param>
        /// <param name="sdkKey">SDK对应的SDKKey</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFActivation(string appId, string sdkKey);

        /// <summary>
        /// 激活人脸识别SDK引擎函数
        /// </summary>
        /// <param name="appId">SDK对应的AppID</param>
        /// <param name="sdkKey">SDK对应的SDKKey</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFOnlineActivation(string appId, string sdkKey);

        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetActiveFileInfo([Out]IntPtr activeFileInfo);

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">AF_DETECT_MODE_VIDEO 视频模式 | AF_DETECT_MODE_IMAGE 图片模式</param>
        /// <param name="detectFaceOrientPriority">检测脸部的角度优先值，推荐：ASF_OrientPriority.ASF_OP_0_HIGHER_EXT</param>
        /// <param name="detectFaceScaleVal">用于数值化表示的最小人脸尺寸</param>
        /// <param name="detectFaceMaxNum">最大需要检测的人脸个数</param>
        /// <param name="combinedMask">用户选择需要检测的功能组合，可单个或多个</param>
        /// <param name="pEngine">初始化返回的引擎handle</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFInitEngine(uint detectMode, int detectFaceOrientPriority, int detectFaceScaleVal, int detectFaceMaxNum, int combinedMask, [Out]out IntPtr pEngine);

        /// <summary>
        /// 销毁引擎
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFUninitEngine(IntPtr pEngine);


        #endregion

        #region settings

        /// <summary>
        /// 修改 RGB/IR 活体阈值， SDK 默认 RGB： 0.75,IR： 0.7
        /// </summary>
        /// <param name="pEngine">引擎句柄</param>
        /// <param name="threshold">活体置信度，推荐阈值 RGB:0.75, IR:0.7</param>
        /// <returns></returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFSetLivenessParam(IntPtr pEngine, IntPtr threshold);

        #endregion

        #region detection 检测

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="format">图像颜色空间</param>
        /// <param name="imgData">图像数据</param>
        /// <param name="detectedFaces">人脸检测结果</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFDetectFaces(IntPtr pEngine, int width, int height, int format, IntPtr imgData, [Out] IntPtr detectedFaces);

        /// <summary>
        /// 人脸信息检测（年龄/性别/人脸3D角度）
        /// 最多支持 4 张人脸信息检测，超过部分返回未知（活体仅支持单张人脸检测，超出返回未知） ,接口不支持 IR 图像检测
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="format">图像颜色空间</param>
        /// <param name="imgData">图像数据</param>
        /// <param name="detectedFaces">人脸信息，用户根据待检测的功能裁减选择需要使用的人脸</param>
        /// <param name="combinedMask">只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选例如初始化的时候指定检测年龄和性别， 在process的时候可以只检测年龄， 但是不能检测除年龄和性别之外的功能 （ASF_AGE、 ASF_GENDER、ASF_FACE3DANGLE、 ASF_LIVENESS），支持多选 注：检测的属性须在引擎初始化接口的 combinedMask 参 数中启用</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr detectedFaces, int combinedMask);

        /// <summary>
        /// 单人脸特征提取
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="width">图像宽度 为 4 的倍数</param>
        /// <param name="height">图像高度 YUYV/I420/NV21/NV12 格式为 2 的倍数； BGR24/GRAY 格式无限制</param>
        /// <param name="format">图像颜色空间</param>
        /// <param name="imgData">图像数据</param>
        /// <param name="faceInfo">单张人脸位置和角度信息</param>
        /// <param name="faceFeature">人脸特征</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureExtract(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr faceInfo, [Out] IntPtr faceFeature);

        /// <summary>
        /// 人脸特征比对
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="faceFeature1">待比较人脸特征1</param>
        /// <param name="faceFeature2"> 待比较人脸特征2</param>
        /// <param name="similarity">相似度(0~1)</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureCompare(IntPtr pEngine, IntPtr faceFeature1, IntPtr faceFeature2, [Out]out float similarity);

        /// <summary>
        /// IR 活体单人脸检测
        ///  
        /// </summary>
        /// <param name="pEngine">引擎句柄</param>
        /// <param name="width">图片宽度，为 4 的倍数</param>
        /// <param name="height">图片高度， I420/NV21/NV12 格式为 2 的倍数，DEPTH_U16/GRAY 格式无限制</param>
        /// <param name="format">颜色空间格式，支持（I420/NV21/NV12/DEPTH_U16/GRAY）的检测</param>
        /// <param name="imgData">图片数据</param>
        /// <param name="detectedFaces">人脸信息，用户根据待检测的功能选择需要使用的人脸</param>
        /// <param name="combinedMask">检测的属性（ASF_IR_LIVENESS）注：检测的属性须在引擎初始化接口的 combinedMask 参数中启用</param>
        /// <returns></returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess_IR(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr detectedFaces, int combinedMask);

        /// <summary>
        /// 获取年龄信息
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="ageInfo">检测到的年龄信息</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetAge(IntPtr pEngine, [Out] IntPtr ageInfo);

        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="genderInfo">检测到的性别信息</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetGender(IntPtr pEngine, [Out] IntPtr genderInfo);

        /// <summary>
        /// 获取3D角度信息
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="p3DAngleInfo">检测到脸部3D角度信息</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetFace3DAngle(IntPtr pEngine, [Out] IntPtr p3DAngleInfo);

        /// <summary>
        /// 获取 RGB 活体信息
        /// </summary>
        /// <param name="pEngine">引擎句柄</param>
        /// <param name="livenessInfo">RGB 活体信息，详见 2.2.10 ASF_LivenessInfo</param>
        /// <returns></returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore(IntPtr pEngine, [Out] IntPtr livenessInfo);

        /// <summary>
        /// 获取 IR 活体信息
        /// </summary>
        /// <param name="pEngine">引擎句柄</param>
        /// <param name="livenessInfo">IR 活体信息，详见 2.2.10 ASF_LivenessInfo</param>
        /// <returns></returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore_IR(IntPtr pEngine, [Out] IntPtr livenessInfo);

        #endregion

        #region mesc

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <returns>调用结果</returns>
        [DllImport(DLL_ENGINE_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ASFGetVersion(IntPtr pEngine);

        #endregion
    }
}
