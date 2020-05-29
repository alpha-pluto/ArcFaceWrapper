/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceFeature.cs  
 * 文件功能描述：人脸特征实体（用于序列化存储）
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Data.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FaceFeature : BaseEntity
    {
        public int EmployeeNo;

        public string Name;

        public int DeptId;

        public string FaceImagePath;

        public int FeatureSize;

        public byte[] Feature;

    }

}
