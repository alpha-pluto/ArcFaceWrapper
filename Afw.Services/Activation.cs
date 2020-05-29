/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-15  
 * daniel.zhang
 * zamen@126.com
 * 文件名：Activation.cs  
 * 文件功能描述：字长检测
 * 
----------------------------------------------------------------*/
using Afw.Core;
using System;

namespace Afw.Services
{
    public sealed class Activation
    {
        #region initilization

        public static MError ASFActivation()
        {
            var retCode = MError.MERR_UNKNOWN.ToInt();
            var appId = AppSettingManager.AppId;
            var sdkKey64 = AppSettingManager.SdkKey64;
            var sdkKey32 = AppSettingManager.SdkKey32;
            var is64CPU = Afw.Core.PlatformProb.Is64BitOperatingSystem;
            var sdkKey = is64CPU ? sdkKey64 : sdkKey32;
            try
            {
                retCode = ASFWrapper.ASFActivation(appId, sdkKey);
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("无法加载 DLL") > -1)
                {
                    retCode = MError.MERR_COMPONENT_NOT_EXIST.ToInt();
                }
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"ASFActivation Exception : {ex.ToString()}");
            }

            return retCode.ToEnum<MError>();
        }

        public static MError ASFOnlineActivation()
        {
            var retCode = MError.MERR_UNKNOWN.ToInt();
            var appId = AppSettingManager.AppId;
            var sdkKey64 = AppSettingManager.SdkKey64;
            var sdkKey32 = AppSettingManager.SdkKey32;
            var is64CPU = Afw.Core.PlatformProb.Is64BitOperatingSystem;
            var sdkKey = is64CPU ? sdkKey64 : sdkKey32;
            try
            {
                retCode = ASFWrapper.ASFOnlineActivation(appId, sdkKey);
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("无法加载 DLL") > -1)
                {
                    retCode = MError.MERR_COMPONENT_NOT_EXIST.ToInt();
                }
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Activation), $"ASFOnlineActivation Exception : {ex.ToString()}");
            }

            return retCode.ToEnum<MError>();
        }

        #endregion
    }
}
