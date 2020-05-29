/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MemberEnroll.cs  
 * 文件功能描述： 
 * 
----------------------------------------------------------------*/
using Afw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Core.Test
{
    public class MemberEnroll
    {
        internal static void TestMemberEnroll()
        {
            try
            {
                IntPtr ptrImageEngine = IntPtr.Zero;
                var errCode = Afw.Services.Initialization.InitialEngineForImage(out ptrImageEngine);
                if (errCode == MError.MOK)
                {
                    MemberWithFeature m = new MemberWithFeature();
                    m.Id = 85465;
                    m.Name = "张华辉";
                    m.DeptId = 17;
                    m.EmployeeNo = 85465;
                    m.FaceImagePath = @"D:\document\my-private-doc\head.jpg";

                    var retCode = Afw.Services.MemberEnroll.Register(ptrImageEngine, ref m);

                    if (retCode == MError.MOK)
                    {
                        byte[] ys = new byte[m.AsfFaceFeature.featureSize];
                        Marshal.Copy(m.AsfFaceFeature.feature, ys, 0, m.AsfFaceFeature.featureSize);
                        string text = string.Empty;
                        for (int i = 0; i < ys.Length; i++)
                        {
                            text += ys[i].ToString("X2");
                        }

                        var e = new Afw.Data.Domain.FaceFeature
                        {
                            DeptId = m.DeptId,
                            EmployeeNo = m.EmployeeNo,
                            FaceImagePath = m.FaceImagePath,
                            Name = m.Name,
                            Id = m.Id,
                            Feature = new byte[m.AsfFaceFeature.featureSize],
                            FeatureSize = m.AsfFaceFeature.featureSize

                        };

                        Marshal.Copy(m.AsfFaceFeature.feature, e.Feature, 0, m.AsfFaceFeature.featureSize);

                        var fullpath = $"e:\\{m.Id}.dat";

                        Afw.Data.Helper.BinaryEntityHelper.WriteEntityIntoFile<Data.Domain.FaceFeature>(e, fullpath);

                        Console.WriteLine(text);

                    }
                    else
                    {
                        Console.WriteLine($"Register{retCode.GetFieldDescription()}");
                    }
                }
                else
                {
                    Console.WriteLine($"InitialEngineForImage{errCode.GetFieldDescription()}");
                }

                var r = Afw.Core.ASFWrapper.ASFUninitEngine(ptrImageEngine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        public static void Test()
        {
            var fullpath = $"e:\\85465.dat";

            var p = Afw.Data.Helper.BinaryEntityHelper.BinaryFileToEntity<Data.Domain.FaceFeature>(fullpath);

            var t = typeof(Data.Domain.FaceFeature);

            System.Reflection.FieldInfo[] mis = t.GetFields();

            foreach (var mi in mis)
            {
                if (mi.Name.Equals("Feature", StringComparison.InvariantCultureIgnoreCase))
                    Console.WriteLine($"{mi.Name} => {mi.GetValue(p)}");
            }

            var text = string.Empty;

            for (int i = 0; i < p.Feature.Length; i++)
            {
                text += p.Feature[i].ToString("X2");
            }

            Console.WriteLine(text);
        }
    }
}
