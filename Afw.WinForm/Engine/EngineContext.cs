/*----------------------------------------------------------------
 *  
 * Winform demo for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：EngineContext.cs  
 * 文件功能描述：引擎上下文
 * 
----------------------------------------------------------------*/
using AForge.Video.DirectShow;
using Afw.Core;
using Afw.Core.Domain;
using Afw.Core.Helper;
using System;
using System.Drawing;
using System.Text;

namespace Afw.WinForm
{
    internal class EngineContext : IDisposable
    {

        #region parameter

        private float threshold = 0.8f;

        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;

        private VideoCaptureDevice deviceVideo;

        private VideoCaptureDevice deviceVideoIr;

        private int irCameraIdx = -1;

        private int rgbCameraIdx = -1;

        #endregion

        #region ctor

        public EngineContext()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            for (var i = 0; i < filterInfoCollection.Count; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(filterInfoCollection[i].Name, @"\bir[\s\t]+camera", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    irCameraIdx = i;
                else if (System.Text.RegularExpressions.Regex.IsMatch(filterInfoCollection[i].Name, @"\brgb[\s\t]+camera", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    rgbCameraIdx = i;
                else
                    rgbCameraIdx = i;
            }

            AttachVideoCapability();
        }

        #endregion

        #region 系统变量

        public int IrCameraIdx { get { return irCameraIdx; } }

        public float Threshold { get { return threshold; } set { threshold = value; } }

        public FilterInfoCollection FilterInfoCollection { get { return filterInfoCollection; } }

        public VideoCaptureDevice DeviceVideo { get { return deviceVideo; } set { deviceVideo = value; } }

        public VideoCaptureDevice DeviceVideoIR { get { return deviceVideoIr; } set { deviceVideoIr = value; } }

        #endregion

        #region 引擎变量

        private IntPtr ptrImageEngine = IntPtr.Zero;

        private IntPtr ptrVideoEngine = IntPtr.Zero;

        //视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        private IntPtr ptrVideoImageEngine = IntPtr.Zero;

        #endregion

        #region 引擎成员属性

        public IntPtr PtrImageEngine { get { return ptrImageEngine; } }

        public IntPtr PtrVideoEngine { get { return ptrVideoEngine; } }

        public IntPtr PtrVideoImageEngine { get { return ptrVideoImageEngine; } }

        #endregion

        #region video

        public void StartVideoCapture()
        {
            deviceVideo?.Start();
            deviceVideoIr?.Start();
        }

        public void StopVideoCapture()
        {
            deviceVideo?.SignalToStop();
            deviceVideoIr?.SignalToStop();
        }

        #endregion

        #region initial 

        private void AttachVideoCapability()
        {
            try
            {
                if (rgbCameraIdx > -1)
                {
                    deviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIdx].MonikerString);
                    deviceVideo.VideoResolution = deviceVideo.VideoCapabilities[0];//.FirstOrDefault(ss => ss.FrameSize == new System.Drawing.Size(640, 480));
                }
                if (irCameraIdx > -1)
                {
                    deviceVideoIr = new VideoCaptureDevice(filterInfoCollection[irCameraIdx].MonikerString);
                    deviceVideoIr.VideoResolution = deviceVideoIr.VideoCapabilities[0];//.FirstOrDefault(ss => ss.FrameSize == new System.Drawing.Size(640, 480));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MError ActivateEngine()
        {
            var retCode = MError.MERR_UNKNOWN;
            try
            {
                retCode = Afw.Services.Activation.ASFOnlineActivation();
                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"ActivateEngine Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"ActivateEngine Exception:{ex.ToString()}");
            }
            return retCode;
        }

        public MError InitialEngineForImage()
        {
            var retCode = MError.MERR_UNKNOWN;
            try
            {
                retCode = Afw.Services.Initialization.InitialEngineForImage(out ptrImageEngine);
                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForImage Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForImage Exception:{ex.ToString()}");
            }
            return retCode;
        }

        /// <summary>
        /// 视频模式下人脸检测引擎
        /// </summary>
        /// <returns></returns>
        public MError InitialEngineForVideo()
        {
            var retCode = MError.MERR_UNKNOWN;
            try
            {
                retCode = Afw.Services.Initialization.InitialEngineForVideo(out ptrVideoEngine);
                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForVideo Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForVideo Exception:{ex.ToString()}");
            }
            return retCode;
        }

        /// <summary>
        /// 视频专用FR引擎
        /// </summary>
        /// <returns></returns>
        public MError InitialEngineForVideoImage()
        {
            var retCode = MError.MERR_UNKNOWN;
            try
            {
                retCode = Afw.Services.Initialization.InitialEngineForImage(out ptrVideoImageEngine, detectFaceMaxNum: 1, combinedMask: FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_LIVENESS);
                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForVideoImage Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"InitialEngineForVideoImage Exception:{ex.ToString()}");
            }
            return retCode;
        }

        public MError UninitEngine(IntPtr ptrEngine)
        {
            var retCode = MError.MERR_UNKNOWN;
            try
            {
                var errCode = Afw.Services.Initialization.UnintialEngine(ptrEngine);
                if (errCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"UninitEngine Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"UninitEngine Exception:{ex.ToString()}");
            }
            return retCode;
        }

        #endregion

        #region Detect

        public IntPtr ExtractFeature(Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            IntPtr ptrFeature = IntPtr.Zero;
            var retCode = MError.MOK;
            if (ptrVideoImageEngine == IntPtr.Zero)
                retCode = InitialEngineForVideoImage();
            if (retCode == MError.MOK)
            {
                ptrFeature = FaceProcessHelper.ExtractFeature(ptrVideoImageEngine, image, singleFaceInfo);
            }
            return ptrFeature;
        }

        public ASF_MultiFaceInfo DetectFace(Bitmap bitmap)
        {
            var retCode = MError.MOK;
            if (ptrVideoEngine == IntPtr.Zero)
                retCode = InitialEngineForVideo();
            if (retCode == MError.MOK)
                return FaceProcessHelper.DetectFace(ptrVideoEngine, bitmap);
            return default(ASF_MultiFaceInfo);
        }

        public ASF_MultiFaceInfo DetectFaceIR(Image image)
        {
            var retCode = MError.MOK;
            if (ptrVideoEngine == IntPtr.Zero)
                retCode = InitialEngineForVideo();
            if (retCode == MError.MOK)
                return FaceProcessHelper.DetectFaceIR(ptrVideoEngine, image);
            return default(ASF_MultiFaceInfo);
        }

        /// <summary>
        /// 为注册会员检测人脸
        /// </summary>
        /// <param name="transImageInfo"></param>
        /// <returns></returns>
        public MError DeteceForMemberEnroll(
            out string errMsg,
            out IntPtr ptrImgFeature,
            out ASF_SingleFaceInfo singleFaceInfo,
            out int face3DStatus,
            out int faceNum,
            out string faceDesc,
            ref Image srcImage,
            int imageContainerWidth,
            int imageContainerHeight)
        {
            faceNum = 0;
            face3DStatus = -1;
            faceDesc = string.Empty;
            errMsg = string.Empty;
            ptrImgFeature = IntPtr.Zero;
            StringBuilder sbErrMsg = new StringBuilder();
            var retCode = MError.MERR_UNKNOWN;
            int retAge = -1;
            int retGender = -1;
            int ret3DAngle = -1;
            singleFaceInfo = new ASF_SingleFaceInfo();
            try
            {
                if (ptrImageEngine == IntPtr.Zero)
                    retCode = InitialEngineForImage();
                else
                    retCode = MError.MOK;

                if (retCode == MError.MOK)
                {
                    //调整图像宽度，需要宽度为4的倍数
                    if (srcImage.Width % 4 != 0)
                    {
                        //srcImage = ImageHelper.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                        srcImage = ImageHelper.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                    }
                    //调整图片数据，非常重要
                    ImageInfo transImageInfo = ImageHelper.ReadBMP(srcImage);

                    //人脸检测
                    ASF_MultiFaceInfo multiFaceInfo = FaceProcessHelper.DetectFace(ptrImageEngine, transImageInfo);
                    faceNum = multiFaceInfo.faceNum;
                    if (multiFaceInfo.faceNum != 1)
                    {
                        retCode = MError.MERR_ASF_EX_INVALID_FACE_INFO;
                        sbErrMsg.Append(multiFaceInfo.faceNum > 1 ? "检测到多张人脸" : "未检测到人脸");
                    }
                    else
                    {
                        ASF_AgeInfo ageInfo = FaceProcessHelper.AgeEstimation(ptrImageEngine, transImageInfo, multiFaceInfo, out retAge);

                        ASF_GenderInfo genderInfo = FaceProcessHelper.GenderEstimation(ptrImageEngine, transImageInfo, multiFaceInfo, out retGender);

                        ASF_Face3DAngle face3DAngleInfo = FaceProcessHelper.Face3DAngleDetection(ptrImageEngine, transImageInfo, multiFaceInfo, out ret3DAngle);

                        #region 标记出检测到的人脸

                        MRECT temp = new MRECT();
                        int ageTemp = 0;
                        int genderTemp = 0;
                        int rectTemp = 0;

                        //标记出检测到的人脸
                        for (int i = 0; i < multiFaceInfo.faceNum; i++)
                        {
                            MRECT rect = MemoryHelper.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryHelper.SizeOf<MRECT>() * i);
                            int orient = MemoryHelper.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryHelper.SizeOf<int>() * i);
                            int age = 0;

                            if (retAge != 0)
                            {
                                sbErrMsg.AppendLine($"年龄检测失败，返回{retAge}!");
                            }
                            else
                            {
                                age = MemoryHelper.PtrToStructure<int>(ageInfo.ageArray + MemoryHelper.SizeOf<int>() * i);
                            }

                            int gender = -1;
                            if (retGender != 0)
                            {
                                sbErrMsg.AppendLine($"性别检测失败，返回{retGender}!");
                            }
                            else
                            {
                                gender = MemoryHelper.PtrToStructure<int>(genderInfo.genderArray + MemoryHelper.SizeOf<int>() * i);
                            }
                            //int face3DStatus = -1;
                            float roll = 0f;
                            float pitch = 0f;
                            float yaw = 0f;
                            if (ret3DAngle != 0)
                            {
                                sbErrMsg.AppendLine($"3DAngle检测失败，返回{ret3DAngle}!");
                            }
                            else
                            {
                                //角度状态 非0表示人脸不可信
                                face3DStatus = MemoryHelper.PtrToStructure<int>(face3DAngleInfo.status + MemoryHelper.SizeOf<int>() * i);
                                //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                                roll = MemoryHelper.PtrToStructure<float>(face3DAngleInfo.roll + MemoryHelper.SizeOf<float>() * i);
                                pitch = MemoryHelper.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryHelper.SizeOf<float>() * i);
                                yaw = MemoryHelper.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryHelper.SizeOf<float>() * i);
                            }

                            int rectWidth = rect.right - rect.left;
                            int rectHeight = rect.bottom - rect.top;

                            //查找最大人脸
                            if (rectWidth * rectHeight > rectTemp)
                            {
                                rectTemp = rectWidth * rectHeight;
                                temp = rect;
                                ageTemp = age;
                                genderTemp = gender;
                            }

                            //srcImage = ImageHelper.MarkRectAndString(srcImage, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, age, gender);
                            faceDesc = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} - 人脸坐标:[left:{rect.left},top:{rect.top},right:{rect.right},bottom:{rect.bottom},orient:{orient},roll:{roll},pitch:{pitch},yaw:{yaw},status:{face3DStatus}] Age:{age} Gender:{(gender >= 0 ? gender.ToString() : "")}\n";
                        }
                        //提取人脸特征
                        ptrImgFeature = FaceProcessHelper.ExtractFeature(ptrImageEngine, srcImage, out singleFaceInfo);

                        float scaleRate = ImageHelper.GetWidthAndHeight(srcImage.Width, srcImage.Height, imageContainerWidth, imageContainerHeight);
                        srcImage = ImageHelper.ScaleImage(srcImage, imageContainerWidth, imageContainerHeight);
                        srcImage = ImageHelper.MarkRectAndString(srcImage, (int)(temp.left * scaleRate), (int)(temp.top * scaleRate), (int)(temp.right * scaleRate) - (int)(temp.left * scaleRate), (int)(temp.bottom * scaleRate) - (int)(temp.top * scaleRate), ageTemp, genderTemp, imageContainerWidth);

                        #endregion
                    }
                    MemoryHelper.Free(transImageInfo.imgData);

                }

                errMsg = sbErrMsg.ToString();
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"DeteceForMemberEnroll Exception:{ex.ToString()}");
            }

            return retCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="similarity"></param>
        /// <param name="feature"></param>
        /// <param name="feature2"></param>
        /// <returns></returns>
        public MError FaceFeatureCompare(out float similarity, IntPtr feature, IntPtr feature2)
        {
            var retCode = MError.MERR_UNKNOWN;
            similarity = 0f;
            try
            {
                if (ptrVideoImageEngine == IntPtr.Zero)
                    retCode = InitialEngineForVideoImage();
                else
                    retCode = MError.MOK;

                if (retCode == MError.MOK)
                {
                    retCode = Afw.Services.FaceRecognition.FaceFeatureCompare(out similarity, ptrVideoImageEngine, feature, feature2);
                }

                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"FaceFeatureCompare Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"FaceFeatureCompare Exception:{ex.ToString()}");
            }
            return retCode;
        }

        #endregion

        #region 活体检测

        public ASF_LivenessInfo GetLivenessScore(Bitmap bitmap, ASF_SingleFaceInfo singleFaceInfo)
        {
            var retCode = MError.MERR_UNKNOWN;
            var livenessinfo = new ASF_LivenessInfo();
            try
            {
                if (ptrVideoEngine == IntPtr.Zero)
                    retCode = InitialEngineForVideo();
                else
                    retCode = MError.MOK;

                if (retCode == MError.MOK)
                {
                    livenessinfo = FaceProcessHelper.LivenessEstimation(ptrVideoEngine, bitmap, singleFaceInfo);
                }

                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"GetLivenessScore Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"GetLivenessScore Exception:{ex.ToString()}");
            }
            return livenessinfo;
        }

        public ASF_LivenessInfo GetLivenessScoreIR(Bitmap bitmap, ASF_SingleFaceInfo singleFaceInfo)
        {
            var retCode = MError.MERR_UNKNOWN;
            var livenessinfo = new ASF_LivenessInfo();
            try
            {
                if (ptrVideoEngine == IntPtr.Zero)
                    retCode = InitialEngineForVideo();
                else
                    retCode = MError.MOK;

                if (retCode == MError.MOK)
                {
                    livenessinfo = FaceProcessHelper.LivenessEstimationIR(ptrVideoEngine, bitmap, singleFaceInfo);
                }

                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"GetLivenessScore Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"GetLivenessScore Exception:{ex.ToString()}");
            }
            return livenessinfo;
        }

        public MError SetLivenessParam(ASF_LivenessThreshold livenessThreshold)
        {
            var retCode = MError.MERR_UNKNOWN;
            IntPtr ptrLivenessThreshold = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_LivenessThreshold>());
            MemoryHelper.StructureToPtr<ASF_LivenessThreshold>(livenessThreshold, ptrLivenessThreshold);
            try
            {
                if (ptrVideoEngine == IntPtr.Zero)
                    retCode = InitialEngineForVideo();
                else
                    retCode = MError.MOK;

                if (retCode == MError.MOK)
                {
                    retCode = Afw.Services.Initialization.SetLivenessParam(ptrVideoEngine, ptrLivenessThreshold);
                }

                if (retCode != MError.MOK)
                {
                    Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"SetLivenessParam Error:{retCode.GetFieldDescription()}");
                }
            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.EngineContext), $"SetLivenessParam Exception:{ex.ToString()}");
            }
            return retCode;
        }

        #endregion

        #region disposable

        public void Dispose()
        {
            if (ptrImageEngine != IntPtr.Zero)
                ASFWrapper.ASFUninitEngine(ptrImageEngine);

            if (ptrVideoEngine != IntPtr.Zero)
                ASFWrapper.ASFUninitEngine(ptrVideoEngine);

            if (ptrVideoImageEngine != IntPtr.Zero)
                ASFWrapper.ASFUninitEngine(ptrVideoImageEngine);

            //if (deviceVideo.IsRunning)
            deviceVideo?.SignalToStop();

            //if (deviceVideoIr.IsRunning)
            deviceVideoIr?.SignalToStop();
        }

        #endregion
    }
}
