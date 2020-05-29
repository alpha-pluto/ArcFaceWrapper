/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-11  
 * daniel.zhang
 * zamen@126.com
 * 文件名：CoreExtension.cs  
 * 文件功能描述：SDK 错误码 * 
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Afw.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class CoreExtension
    {
        /// <summary>
        /// 获得变量名
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetVarName<T>(System.Linq.Expressions.Expression<Func<string, T>> exp)
        {
            return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取类型中整型成员的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetFieldDescription(this MError merror)
        {
            FieldInfo fi = merror.GetType().GetField(merror.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return merror.ToString();
        }

        /// <summary>
        /// 将枚举值 转化为 整型值 
        /// </summary>
        /// <param name="merror"></param>
        /// <returns></returns>
        public static int ToInt(this MError merror)
        {
            return (int)merror;
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="merror"></param>
        /// <returns></returns>
        public static string GetName(this MError merror)
        {
            return merror.ToString();
        }

        /// <summary>
        /// 将整型值转化为枚举值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int enumValue)
        {
            return (T)Enum.ToObject(typeof(T), enumValue);
        }
    }
}
