/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：DomainTransform.cs  
 * 文件功能描述：扩展功能
 * 
----------------------------------------------------------------*/
using Afw.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Data
{
    public static class DomainTransform
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="faceFeature"></param>
        /// <returns></returns>
        public static MemberData CreateStruct(this Afw.Data.Domain.FaceFeature faceFeature)
        {
            var retMemberData = new MemberData();
            retMemberData.Face3DStatus = 0;
            retMemberData.FaceNum = 1;
            retMemberData.memberWithFeature.Id = faceFeature.Id;
            retMemberData.memberWithFeature.Name = faceFeature.Name;
            retMemberData.memberWithFeature.DeptId = faceFeature.DeptId;
            retMemberData.memberWithFeature.EmployeeNo = faceFeature.EmployeeNo;
            retMemberData.memberWithFeature.FaceImagePath = faceFeature.FaceImagePath;
            retMemberData.memberWithFeature.AsfFaceFeature.featureSize = faceFeature.FeatureSize;
            retMemberData.memberWithFeature.AsfFaceFeature.feature = MemoryHelper.Malloc(faceFeature.FeatureSize);
            MemoryHelper.Copy(faceFeature.Feature, 0, retMemberData.memberWithFeature.AsfFaceFeature.feature, faceFeature.FeatureSize);
            return retMemberData;
        }
    }
}
