/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MemoryHelper.cs  
 * 文件功能描述：非托管内存操作方法
 * 
----------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;

namespace Afw.Core.Helper
{
    /// <summary>
    /// 非托管内存操作帮助类
    /// </summary>
    public class MemoryHelper
    {
        /// <summary>
        /// 申请内存
        /// </summary>
        /// <param name="len">内存长度(单位:字节)</param>
        /// <returns>内存首地址</returns>
        public static IntPtr Malloc(int len)
        {
            return Marshal.AllocHGlobal(len);
        }

        /// <summary>
        /// 释放ptr托管的内存
        /// </summary>
        /// <param name="ptr">托管指针</param>
        public static void Free(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// 将字节数组的内容拷贝到托管内存中
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="startIndex">元数据拷贝起始位置</param>
        /// <param name="destination">托管内存</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 将托管内存的内容拷贝到字节数组中
        /// </summary>
        /// <param name="source">托管内存</param>
        /// <param name="destination">目标字节数组</param>
        /// <param name="startIndex">拷贝起始位置</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 将ptr托管的内存转化为结构体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="ptr">托管指针</param>
        /// <returns>转化后的对象</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// 将结构体对象复制到ptr托管的内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ptr"></param>
        public static void StructureToPtr<T>(T t, IntPtr ptr)
        {
            Marshal.StructureToPtr(t, ptr, false);
        }

        /// <summary>
        /// 获取类型的大小
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>类型的大小</returns>
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }

        /// <summary>
        /// 将结构体序列化为二进制流
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytes<TStruct>(TStruct data) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            byte[] buffer = new byte[structSize];
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return buffer;
        }

        public static IntPtr StringToIntPtr(string str)
        {
            return Marshal.StringToHGlobalAnsi(str);
        }

        public static string PtrToString(IntPtr ptrString)
        {
            return Marshal.PtrToStringAnsi(ptrString);
        }

    }
}
