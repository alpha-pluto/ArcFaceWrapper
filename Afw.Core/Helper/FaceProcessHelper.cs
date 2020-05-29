/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceProcessHelper.cs  
 * 文件功能描述：人脸检测处理类
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using Afw.Core.Exceptions;
using System;
using System.Drawing;

namespace Afw.Core.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class FaceProcessHelper
    {
        #region 人脸检测

        /// <summary>
        /// 
        /// </summary>
        private static object locks = new object();

        /// <summary>
        /// 人脸检测(PS:检测RGB图像的人脸时，必须保证图像的宽度能被4整除，否则会失败)
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <returns>人脸检测结果</returns>
        public static ASF_MultiFaceInfo DetectFace(IntPtr pEngine, ImageInfo imageInfo)
        {
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            int retCode = ASFWrapper.ASFDetectFaces(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo);
            var errCode = retCode.ToEnum<MError>();
            if (errCode == MError.MOK)
            {
                multiFaceInfo = MemoryHelper.PtrToStructure<ASF_MultiFaceInfo>(pMultiFaceInfo);
            }
            else
            {
                throw new FaceDetectException(errCode);
            }

            return multiFaceInfo;
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ASF_MultiFaceInfo DetectFace(IntPtr pEngine, Image image)
        {
            lock (locks)
            {
                ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
                if (image != null)
                {
                    image = ImageHelper.ScaleImage(image, image.Width, image.Height);
                    ImageInfo imageInfo = ImageHelper.ReadBMP(image);
                    multiFaceInfo = DetectFace(pEngine, imageInfo);
                    MemoryHelper.Free(imageInfo.imgData);
                    return multiFaceInfo;
                }
                else
                {
                    return multiFaceInfo;
                }
            }
        }

        public static ASF_MultiFaceInfo DetectFaceIR(IntPtr pEngine, Image image)
        {
            lock (locks)
            {
                ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
                if (image != null)
                {
                    image = ImageHelper.ScaleImage(image, image.Width, image.Height);
                    ImageInfo imageInfo = ImageHelper.ReadGray(image);
                    multiFaceInfo = DetectFace(pEngine, imageInfo);
                    MemoryHelper.Free(imageInfo.imgData);
                    return multiFaceInfo;
                }
                else
                {
                    return multiFaceInfo;
                }
            }
        }

        /// <summary>
        /// 人脸3D角度检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>保存人脸3D角度检测结果结构体</returns>
        public static ASF_Face3DAngle Face3DAngleDetection(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                retCode = -1;
                return new ASF_Face3DAngle();
            }

            //人脸信息处理
            retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_FACE3DANGLE);
            if (retCode == 0)
            {
                //获取人脸3D角度
                IntPtr pFace3DAngleInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_Face3DAngle>());
                retCode = ASFWrapper.ASFGetFace3DAngle(pEngine, pFace3DAngleInfo);
                //Console.WriteLine("Get Face3D Angle Result:" + retCode);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Face3D Angle Result:{retCode}");
                ASF_Face3DAngle face3DAngle = MemoryHelper.PtrToStructure<ASF_Face3DAngle>(pFace3DAngleInfo);

                //释放内存
                MemoryHelper.Free(pMultiFaceInfo);
                MemoryHelper.Free(pFace3DAngleInfo);

                return face3DAngle;
            }
            else
            {
                return new ASF_Face3DAngle();
            }
        }

        /// <summary>
        /// 获取多个人脸检测结果中面积最大的人脸
        /// </summary>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>面积最大的人脸信息</returns>
        public static ASF_SingleFaceInfo GetMaxFace(ASF_MultiFaceInfo multiFaceInfo)
        {
            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
            singleFaceInfo.faceRect = new MRECT();
            singleFaceInfo.faceOrient = 1;

            int maxArea = 0;
            int index = -1;
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                MRECT rect = MemoryHelper.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryHelper.SizeOf<MRECT>() * i);
                int area = (rect.right - rect.left) * (rect.bottom - rect.top);
                if (maxArea <= area)
                {
                    maxArea = area;
                    index = i;
                }
            }
            if (index != -1)
            {
                singleFaceInfo.faceRect = MemoryHelper.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryHelper.SizeOf<MRECT>() * index);
                singleFaceInfo.faceOrient = MemoryHelper.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryHelper.SizeOf<int>() * index);
            }
            return singleFaceInfo;
        }

        #endregion

        #region 提取人脸特征

        /// <summary>
        /// 提取人脸特征
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>保存人脸特征结构体指针</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out ASF_SingleFaceInfo singleFaceInfo)
        {
            singleFaceInfo = new ASF_SingleFaceInfo();
            singleFaceInfo.faceRect = MemoryHelper.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
            singleFaceInfo.faceOrient = MemoryHelper.PtrToStructure<int>(multiFaceInfo.faceOrients);
            IntPtr pSingleFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_SingleFaceInfo>());
            MemoryHelper.StructureToPtr(singleFaceInfo, pSingleFaceInfo);

            IntPtr pFaceFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
            int retCode = ASFWrapper.ASFFaceFeatureExtract(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pSingleFaceInfo, pFaceFeature);
            //Console.WriteLine("FR Extract Feature result:" + retCode);
            SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"FR Extract Feature (4) result:{retCode} => [{retCode.ToEnum<MError>().GetFieldDescription()}]");
            if (retCode != 0)
            {
                //释放指针
                MemoryHelper.Free(pSingleFaceInfo);
                MemoryHelper.Free(pFaceFeature);
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
                MemoryHelper.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }

            //人脸特征feature过滤
            ASF_FaceFeature faceFeature = MemoryHelper.PtrToStructure<ASF_FaceFeature>(pFaceFeature);
            byte[] feature = new byte[faceFeature.featureSize];
            MemoryHelper.Copy(faceFeature.feature, feature, 0, faceFeature.featureSize);

            ASF_FaceFeature localFeature = new ASF_FaceFeature();
            localFeature.feature = MemoryHelper.Malloc(feature.Length);
            MemoryHelper.Copy(feature, 0, localFeature.feature, feature.Length);
            localFeature.featureSize = feature.Length;
            IntPtr pLocalFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
            MemoryHelper.StructureToPtr(localFeature, pLocalFeature);

            //释放指针
            MemoryHelper.Free(pSingleFaceInfo);
            MemoryHelper.Free(pFaceFeature);

            return pLocalFeature;
        }

        /// <summary>
        /// 提取人脸特征
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="image">图像</param>
        /// <returns>保存人脸特征结构体指针</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, Image image, out ASF_SingleFaceInfo singleFaceInfo)
        {
            image = ImageHelper.ScaleImage(image, image.Width, image.Height);
            ImageInfo imageInfo = ImageHelper.ReadBMP(image);
            ASF_MultiFaceInfo multiFaceInfo = DetectFace(pEngine, imageInfo);
            singleFaceInfo = new ASF_SingleFaceInfo();
            IntPtr pFaceModel = ExtractFeature(pEngine, imageInfo, multiFaceInfo, out singleFaceInfo);
            MemoryHelper.Free(imageInfo.imgData);
            return pFaceModel;
        }

        /// <summary>
        /// 提取单人脸特征
        /// </summary>
        /// <param name="pEngine">人脸识别引擎</param>
        /// <param name="image">图片</param>
        /// <param name="singleFaceInfo">单人脸信息</param>
        /// <returns>单人脸特征</returns>
        public static IntPtr ExtractFeature(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageHelper.ReadBMP(image);
            IntPtr pSingleFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_SingleFaceInfo>());
            MemoryHelper.StructureToPtr(singleFaceInfo, pSingleFaceInfo);

            IntPtr pFaceFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
            int retCode = -1;
            try
            {
                retCode = ASFWrapper.ASFFaceFeatureExtract(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pSingleFaceInfo, pFaceFeature);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Extract Feature Exception => {e.ToString()}");
            }
            //Console.WriteLine("FR Extract Feature result:" + retCode);
            SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"FR Extract Feature result:{retCode} =>[{retCode.ToEnum<MError>().GetFieldDescription()}]");

            if (retCode != 0)
            {
                //释放指针
                MemoryHelper.Free(pSingleFaceInfo);
                MemoryHelper.Free(pFaceFeature);
                ASF_FaceFeature emptyFeature = new ASF_FaceFeature();
                IntPtr pEmptyFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
                MemoryHelper.StructureToPtr(emptyFeature, pEmptyFeature);
                return pEmptyFeature;
            }

            //人脸特征feature过滤
            ASF_FaceFeature faceFeature = MemoryHelper.PtrToStructure<ASF_FaceFeature>(pFaceFeature);
            byte[] feature = new byte[faceFeature.featureSize];
            MemoryHelper.Copy(faceFeature.feature, feature, 0, faceFeature.featureSize);

            ASF_FaceFeature localFeature = new ASF_FaceFeature();
            localFeature.feature = MemoryHelper.Malloc(feature.Length);
            MemoryHelper.Copy(feature, 0, localFeature.feature, feature.Length);
            localFeature.featureSize = feature.Length;
            IntPtr pLocalFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
            MemoryHelper.StructureToPtr(localFeature, pLocalFeature);

            //释放指针
            MemoryHelper.Free(pSingleFaceInfo);
            MemoryHelper.Free(pFaceFeature);
            MemoryHelper.Free(imageInfo.imgData);

            return pLocalFeature;
        }


        #endregion

        #region 年龄检测

        /// <summary>
        /// 年龄检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>年龄检测结构体</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            retCode = -1;
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_AgeInfo();
            }

            //人脸信息处理
            retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_AGE);
            if (retCode == 0)
            {
                //获取年龄信息
                IntPtr pAgeInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_AgeInfo>());
                retCode = ASFWrapper.ASFGetAge(pEngine, pAgeInfo);
                //Console.WriteLine("Get Age Result:" + retCode);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Age Result:{retCode}");
                ASF_AgeInfo ageInfo = MemoryHelper.PtrToStructure<ASF_AgeInfo>(pAgeInfo);

                //释放内存
                MemoryHelper.Free(pMultiFaceInfo);
                MemoryHelper.Free(pAgeInfo);
                return ageInfo;
            }
            else
            {
                return new ASF_AgeInfo();
            }
        }

        /// <summary>
        /// 年龄检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>年龄检测结构体</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_AgeInfo();
            }

            //人脸信息处理
            int retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_AGE);

            //获取年龄信息
            IntPtr pAgeInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_AgeInfo>());
            retCode = ASFWrapper.ASFGetAge(pEngine, pAgeInfo);
            //Console.WriteLine("Get Age Result:" + retCode);
            SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Age Result:{retCode}");
            ASF_AgeInfo ageInfo = MemoryHelper.PtrToStructure<ASF_AgeInfo>(pAgeInfo);

            //释放内存
            MemoryHelper.Free(pMultiFaceInfo);
            MemoryHelper.Free(pAgeInfo);

            return ageInfo;
        }

        /// <summary>
        /// 单人脸年龄检测
        /// </summary>
        /// <param name="pEngine">人脸识别引擎</param>
        /// <param name="image">图片</param>
        /// <param name="singleFaceInfo">单人脸信息</param>
        /// <returns>年龄检测结果</returns>
        public static ASF_AgeInfo AgeEstimation(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageHelper.ReadBMP(image);
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            multiFaceInfo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
            MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfo.faceRects);
            multiFaceInfo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
            MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfo.faceOrients);
            multiFaceInfo.faceNum = 1;
            ASF_AgeInfo ageInfo = AgeEstimation(pEngine, imageInfo, multiFaceInfo);
            MemoryHelper.Free(imageInfo.imgData);
            return ageInfo;
        }

        #endregion

        #region 性别检测

        /// <summary>
        /// 性别检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>保存性别检测结果结构体</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, out int retCode)
        {
            retCode = -1;
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_GenderInfo();
            }

            //人脸信息处理
            retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_GENDER);
            if (retCode == 0)
            {
                //获取性别信息
                IntPtr pGenderInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_GenderInfo>());
                retCode = ASFWrapper.ASFGetGender(pEngine, pGenderInfo);
                //Console.WriteLine("Get Gender Result:" + retCode);
                SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Gender Result:{retCode}");
                ASF_GenderInfo genderInfo = MemoryHelper.PtrToStructure<ASF_GenderInfo>(pGenderInfo);

                //释放内存
                MemoryHelper.Free(pMultiFaceInfo);
                MemoryHelper.Free(pGenderInfo);

                return genderInfo;
            }
            else
            {
                return new ASF_GenderInfo();
            }
        }

        /// <summary>
        /// 性别检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo">图像数据</param>
        /// <param name="multiFaceInfo">人脸检测结果</param>
        /// <returns>保存性别估计结果结构体</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            if (multiFaceInfo.faceNum == 0)
            {
                return new ASF_GenderInfo();
            }

            //人脸信息处理
            int retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_GENDER);

            //获取性别信息
            IntPtr pGenderInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_GenderInfo>());
            retCode = ASFWrapper.ASFGetGender(pEngine, pGenderInfo);
            //Console.WriteLine("Get Gender Result:" + retCode);
            SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Gender Result:{retCode}");
            ASF_GenderInfo genderInfo = MemoryHelper.PtrToStructure<ASF_GenderInfo>(pGenderInfo);

            //释放内存
            MemoryHelper.Free(pMultiFaceInfo);
            MemoryHelper.Free(pGenderInfo);

            return genderInfo;
        }

        /// <summary>
        /// 单人脸性别检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="image">图片</param>
        /// <param name="singleFaceInfo">单人脸信息</param>
        /// <returns>性别估计结果</returns>
        public static ASF_GenderInfo GenderEstimation(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageHelper.ReadBMP(image);
            ASF_MultiFaceInfo multiFaceInfo = new ASF_MultiFaceInfo();
            multiFaceInfo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
            MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfo.faceRects);
            multiFaceInfo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
            MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfo.faceOrients);
            multiFaceInfo.faceNum = 1;
            ASF_GenderInfo genderInfo = GenderEstimation(pEngine, imageInfo, multiFaceInfo);
            MemoryHelper.Free(imageInfo.imgData);
            return genderInfo;
        }


        #endregion

        #region 活体信息检测

        /// <summary>
        /// RGB活体检测
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public static ASF_LivenessInfo LivenessEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            try
            {
                if (multiFaceInfo.faceNum == 0)
                {
                    return new ASF_LivenessInfo();
                }

                if (multiFaceInfo.faceNum > 1)
                {
                    ASF_SingleFaceInfo singleFaceInfo = GetMaxFace(multiFaceInfo);
                    ASF_MultiFaceInfo multiFaceInfoNeo = new ASF_MultiFaceInfo();
                    multiFaceInfoNeo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
                    MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfoNeo.faceRects);
                    multiFaceInfoNeo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
                    MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfoNeo.faceOrients);
                    multiFaceInfoNeo.faceNum = 1;
                    MemoryHelper.StructureToPtr(multiFaceInfoNeo, pMultiFaceInfo);
                }
                //活体信息检测
                int retCode = ASFWrapper.ASFProcess(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_LIVENESS);
                if (retCode == 0)
                {
                    //获取活体信息
                    IntPtr pLivenessInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_LivenessInfo>());

                    retCode = ASFWrapper.ASFGetLivenessScore(pEngine, pLivenessInfo);

                    SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Liveness Result:{retCode}");
                    ASF_LivenessInfo livenessInfo = MemoryHelper.PtrToStructure<ASF_LivenessInfo>(pLivenessInfo);

                    //释放内存

                    MemoryHelper.Free(pLivenessInfo);
                    return livenessInfo;
                }
                else
                {

                    return new ASF_LivenessInfo();
                }

            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(FaceProcessHelper), $"LivenessEstimation Exception => {ex.ToString()}");
            }
            finally
            {
                //释放内存
                MemoryHelper.Free(pMultiFaceInfo);
                MemoryHelper.Free(imageInfo.imgData);
            }
            return new ASF_LivenessInfo();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="singleFaceInfo"></param>
        /// <returns></returns>
        public static ASF_LivenessInfo LivenessEstimation(IntPtr pEngine, ImageInfo imageInfo, ASF_SingleFaceInfo singleFaceInfo)
        {
            ASF_MultiFaceInfo multiFaceInfoNeo = new ASF_MultiFaceInfo();
            multiFaceInfoNeo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
            MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfoNeo.faceRects);
            multiFaceInfoNeo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
            MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfoNeo.faceOrients);
            multiFaceInfoNeo.faceNum = 1;
            ASF_LivenessInfo livenessinfo = LivenessEstimation(pEngine, imageInfo, multiFaceInfoNeo);

            return livenessinfo;
        }

        public static ASF_LivenessInfo LivenessEstimation(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageHelper.ReadBMP(image);
            return LivenessEstimation(pEngine, imageInfo, singleFaceInfo);
        }

        /// <summary>
        /// IR活体检测
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public static ASF_LivenessInfo LivenessEstimationIR(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            IntPtr pMultiFaceInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_MultiFaceInfo>());
            MemoryHelper.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            try
            {
                if (multiFaceInfo.faceNum == 0)
                {
                    return new ASF_LivenessInfo();
                }

                if (multiFaceInfo.faceNum > 1)
                {
                    ASF_SingleFaceInfo singleFaceInfo = GetMaxFace(multiFaceInfo);
                    ASF_MultiFaceInfo multiFaceInfoNeo = new ASF_MultiFaceInfo();
                    multiFaceInfoNeo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
                    MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfoNeo.faceRects);
                    multiFaceInfoNeo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
                    MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfoNeo.faceOrients);
                    multiFaceInfoNeo.faceNum = 1;
                    MemoryHelper.StructureToPtr(multiFaceInfoNeo, pMultiFaceInfo);
                }
                //活体信息检测
                int retCode = ASFWrapper.ASFProcess_IR(pEngine, imageInfo.width, imageInfo.height, imageInfo.format, imageInfo.imgData, pMultiFaceInfo, FaceEngineMask.ASF_IR_LIVENESS);
                if (retCode == 0)
                {
                    //获取活体信息
                    IntPtr pLivenessInfo = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_LivenessInfo>());

                    retCode = ASFWrapper.ASFGetLivenessScore_IR(pEngine, pLivenessInfo);

                    SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.Core.Helper.FaceProcessHelper), $"Get Liveness Result:{retCode}");
                    ASF_LivenessInfo livenessInfo = MemoryHelper.PtrToStructure<ASF_LivenessInfo>(pLivenessInfo);

                    //释放内存

                    MemoryHelper.Free(pLivenessInfo);
                    return livenessInfo;
                }
                else
                {
                    return new ASF_LivenessInfo();
                }
            }
            catch (Exception ex)
            {

                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(FaceProcessHelper), $"LivenessEstimationIR Exception => {ex.ToString()}");
            }
            finally
            {
                MemoryHelper.Free(pMultiFaceInfo);
                MemoryHelper.Free(imageInfo.imgData);
            }
            return new ASF_LivenessInfo();
        }

        public static ASF_LivenessInfo LivenessEstimationIR(IntPtr pEngine, ImageInfo imageInfo, ASF_SingleFaceInfo singleFaceInfo)
        {
            ASF_MultiFaceInfo multiFaceInfoNeo = new ASF_MultiFaceInfo();
            multiFaceInfoNeo.faceRects = MemoryHelper.Malloc(MemoryHelper.SizeOf<MRECT>());
            MemoryHelper.StructureToPtr<MRECT>(singleFaceInfo.faceRect, multiFaceInfoNeo.faceRects);
            multiFaceInfoNeo.faceOrients = MemoryHelper.Malloc(MemoryHelper.SizeOf<int>());
            MemoryHelper.StructureToPtr<int>(singleFaceInfo.faceOrient, multiFaceInfoNeo.faceOrients);
            multiFaceInfoNeo.faceNum = 1;
            ASF_LivenessInfo livenessinfo = LivenessEstimationIR(pEngine, imageInfo, multiFaceInfoNeo);

            return livenessinfo;
        }

        public static ASF_LivenessInfo LivenessEstimationIR(IntPtr pEngine, Image image, ASF_SingleFaceInfo singleFaceInfo)
        {
            ImageInfo imageInfo = ImageHelper.ReadGray(image);
            return LivenessEstimationIR(pEngine, imageInfo, singleFaceInfo);
        }

        #endregion
    }
}
