/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MemberEnroll.cs  
 * 文件功能描述：会员注册
 * 
----------------------------------------------------------------*/
using System;
using System.Drawing;
using Afw.Core;
using Afw.Core.Domain;
using Afw.Core.Helper;
namespace Afw.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberEnroll
    {
        public static MError Register(IntPtr ptrImageEngine, ref MemberWithFeature member)
        {
            var retCode = MError.MERR_UNKNOWN.ToInt();
            IntPtr feature = IntPtr.Zero;
            Image image = null;
            try
            {
                image = Image.FromFile(member.FaceImagePath);

                if (image.Width % 4 != 0)
                {
                    image = ImageHelper.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                }

                ASF_MultiFaceInfo multiFaceInfo = FaceProcessHelper.DetectFace(ptrImageEngine, image);

                if (multiFaceInfo.faceNum > 0)
                {
                    MRECT rect = MemoryHelper.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                    image = ImageHelper.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);

                    //提取人脸特征
                    ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                    feature = FaceProcessHelper.ExtractFeature(ptrImageEngine, Image.FromFile(member.FaceImagePath), out singleFaceInfo);

                    if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                    {
                        return MError.MERR_FSDK_FR_INVALID_FACE_INFO;
                    }
                    else
                    {
                        member.AsfFaceFeature = MemoryHelper.PtrToStructure<ASF_FaceFeature>(feature);
                    }
                }
                else
                {
                    return MError.MERR_FSDK_FR_INVALID_FACE_INFO;
                }
                retCode = MError.MOK.ToInt();

            }
            catch (Exception ex)
            {
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(MemberEnroll), $"Register Exception:{ex.ToString()}");
            }
            finally
            {
                MemoryHelper.Free(feature);
                image.Dispose();

            }
            return retCode.ToEnum<MError>();
        }
    }
}
