/*----------------------------------------------------------------
 *  
 * Winform demo for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-13  
 * daniel.zhang
 * zamen@126.com
 * 文件名：WinMessageHelper.cs  
 * 文件功能描述：windows消息发送
 * 
----------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Afw.WinForm
{
    public sealed class WinMessageHelper
    {
        #region extern
        //声明:
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        internal static extern int SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        internal static extern int SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, ref Rectangle lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        internal static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        internal static extern int SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
