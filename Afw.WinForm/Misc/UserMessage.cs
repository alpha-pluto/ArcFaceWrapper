/*----------------------------------------------------------------
 *  
 * Winform demo for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：UserMessage.cs  
 * 文件功能描述：自定义windows消息
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.WinForm
{
    public sealed class UserMessage
    {
        public const int USER = 0x0400;

        /// <summary>
        /// 来自线程的文本消息
        /// </summary>
        public const int WM_MESSAGE = USER + 101;

        /// <summary>
        /// 消息:人脸数据导入中
        /// </summary>
        public const int WM_FACE_DATA_LOADING = USER + 200;

        /// <summary>
        /// 消息:人脸数据导入完毕
        /// </summary>
        public const int WM_FACE_DATA_LOADED = USER + 201;

        public const int WM_FACE_DATA_REGISTER = USER + 205;

        public const int WM_FACE_DATA_UPDATE = USER + 206;

        public const int WM_FACE_DATA_DELETE = USER + 207;

        public const int WM_FACE_DATA_READ = USER + 208;

        public const int WM_FACE_DATA_REFRESH = USER + 209;

        /// <summary>
        /// 请求锁定父窗口
        /// </summary>
        public const int WM_PARENT_FORM_LOCK = USER + 400;

        /// <summary>
        /// 请求解锁父窗口
        /// </summary>
        public const int WM_PARENT_FORM_UNLOCK = USER + 401;

        public const int WM_QUERY_FACE_FEATURE_VIA_ID = USER + 500;

        public const int WM_RESPONSE_FACE_FEATURE_VIA_ID = USER + 501;

        public const int WM_QUERY_FACE_FEATURE_VIA_EMPLOYEE_NO = USER + 510;

        public const int WM_RESPONSE_FACE_FEATURE_VIA_EMPLOYEE_NO = USER + 511;

        public const int WM_FACE_FORM_LOCK = USER + 700;

        public const int WM_FACE_FORM_UNLOCK = USER + 701;

        public const int WM_FACE_FORM_HIDE = USER + 705;

        public const int WM_FACE_FORM_SHOW = USER + 706;


    }
}
