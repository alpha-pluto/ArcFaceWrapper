/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-15  
 * daniel.zhang
 * zamen@126.com
 * 文件名：AppSettingManager.cs  
 * 文件功能描述：App settings 处理类
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afw.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AppSettingManager
    {

        public static readonly Afw.Core.Helper.EncryptAgent agtEncrypt = new Core.Helper.EncryptAgent();

        public static readonly string AppId;

        public static readonly string SdkKey64;

        public static readonly string SdkKey32;

        public static readonly string FileFaceDataRepository;

        static AppSettingManager()
        {
            AppSettingsReader reader = new AppSettingsReader();
            AppId = (string)reader.GetValue("AppId", typeof(string));
            SdkKey64 = agtEncrypt.Decrypt((string)reader.GetValue("SdkKey64", typeof(string)));
            SdkKey32 = agtEncrypt.Decrypt((string)reader.GetValue("SdkKey32", typeof(string)));
            FileFaceDataRepository = (string)reader.GetValue("FileFaceDataRepository", typeof(string));
        }
    }

}
