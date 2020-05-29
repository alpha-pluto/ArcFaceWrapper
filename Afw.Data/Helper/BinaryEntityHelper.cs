/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-12  
 * daniel.zhang
 * zamen@126.com
 * 文件名：BinaryEntityHelper.cs  
 * 文件功能描述：实体<->文件流 帮助类
 * 
----------------------------------------------------------------*/
using Afw.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Data.Helper
{
    public class BinaryEntityHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fullPath"></param>
        public static void WriteEntityIntoFile<T>(T entity, string fullPath) where T : BaseEntity
        {
            System.Runtime.Serialization.IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //序列化person对象newPerson，先转化为二进制再存为文件
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            binaryFormatter.Serialize(ms, entity);
            byte[] buffer = ms.GetBuffer();
            System.IO.Stream st = new System.IO.FileStream(fullPath, System.IO.FileMode.Create);
            st.Write(buffer, 0, buffer.Length);
            st.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static T BinaryFileToEntity<T>(string fullPath) where T : BaseEntity
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //指向数据文件的二进制流
            System.IO.Stream st = new System.IO.FileStream(fullPath, System.IO.FileMode.Open);
            //反序列化，得到的是一个object对象，需要强制转换
            T entity = (T)binaryFormatter.Deserialize(st);
            st.Close();
            return entity;
        }
    }
}
