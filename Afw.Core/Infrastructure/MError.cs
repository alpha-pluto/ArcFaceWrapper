/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-11  
 * daniel.zhang
 * zamen@126.com
 * 文件名：MError.cs  
 * 文件功能描述：SDK 错误码 * 
 * Originated from merror.h 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Afw.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum MError
    {

        MERR_NONE = 0,

        MOK = 0,

        #region 通用错误类型

        /// <summary>
        /// 通用错误类型
        /// </summary>
        [Description("通用错误类型")]
        MERR_BASIC_BASE = 0X0001,

        /// <summary>
        /// 错误原因不明
        /// </summary>
        [Description("错误原因不明")]
        MERR_UNKNOWN = MERR_BASIC_BASE,

        /// <summary>
        /// 无效的参数
        /// </summary>
        [Description("无效的参数")]
        MERR_INVALID_PARAM = MERR_BASIC_BASE + 1,

        /// <summary>
        /// 引擎不支持
        /// </summary>
        [Description("引擎不支持")]
        MERR_UNSUPPORTED = MERR_BASIC_BASE + 2,

        /// <summary>
        /// 内存不足
        /// </summary>
        [Description("内存不足")]
        MERR_NO_MEMORY = MERR_BASIC_BASE + 3,

        /// <summary>
        /// 状态错误
        /// </summary>
        [Description("状态错误")]
        MERR_BAD_STATE = MERR_BASIC_BASE + 4,

        /// <summary>
        /// 用户取消相关操作
        /// </summary>
        [Description("用户取消相关操作")]
        MERR_USER_CANCEL = MERR_BASIC_BASE + 5,

        /// <summary>
        /// 操作时间过期
        /// </summary>
        [Description("操作时间过期")]
        MERR_EXPIRED = MERR_BASIC_BASE + 6,

        /// <summary>
        /// 用户暂停操作
        /// </summary>
        [Description("用户暂停操作")]
        MERR_USER_PAUSE = MERR_BASIC_BASE + 7,

        /// <summary>
        /// 缓冲上溢
        /// </summary>
        [Description("缓冲上溢")]
        MERR_BUFFER_OVERFLOW = MERR_BASIC_BASE + 8,

        /// <summary>
        /// 缓冲下溢
        /// </summary>
        [Description("缓冲下溢")]
        MERR_BUFFER_UNDERFLOW = MERR_BASIC_BASE + 9,

        /// <summary>
        /// 存贮空间不足
        /// </summary>
        [Description("存贮空间不足")]
        MERR_NO_DISKSPACE = MERR_BASIC_BASE + 10,

        /// <summary>
        /// 组件不存在
        /// </summary>
        [Description("组件不存在")]
        MERR_COMPONENT_NOT_EXIST = MERR_BASIC_BASE + 11,

        /// <summary>
        /// 全局数据不存在
        /// </summary>
        [Description("全局数据不存在")]
        MERR_GLOBAL_DATA_NOT_EXIST = MERR_BASIC_BASE + 12,



        #endregion

        #region Free SDK通用错误类型

        /// <summary>
        /// Free SDK通用错误类型
        /// </summary>
        [Description("Free SDK通用错误类型")]
        MERR_FSDK_BASE = 0X7000,

        /// <summary>
        /// 无效的App Id
        /// </summary>
        [Description("无效的App Id")]
        MERR_FSDK_INVALID_APP_ID = MERR_FSDK_BASE + 1,

        /// <summary>
        /// 无效的SDK key
        /// </summary>
        [Description("无效的SDK key")]
        MERR_FSDK_INVALID_SDK_ID = MERR_FSDK_BASE + 2,

        /// <summary>
        /// AppId和SDKKey不匹配
        /// </summary>
        [Description("AppId和SDKKey不匹配")]
        MERR_FSDK_INVALID_ID_PAIR = MERR_FSDK_BASE + 3,

        /// <summary>
        /// SDKKey 和使用的SDK 不匹配
        /// </summary>
        [Description("SDKKey 和使用的SDK 不匹配")]
        MERR_FSDK_MISMATCH_ID_AND_SDK = MERR_FSDK_BASE + 4,

        /// <summary>
        /// 系统版本不被当前SDK所支持
        /// </summary>
        [Description("系统版本不被当前SDK所支持")]
        MERR_FSDK_SYSTEM_VERSION_UNSUPPORTED = MERR_FSDK_BASE + 5,

        /// <summary>
        /// SDK有效期过期，需要重新下载更新
        /// </summary>
        [Description("SDK有效期过期，需要重新下载更新")]
        MERR_FSDK_LICENCE_EXPIRED = MERR_FSDK_BASE + 6,

        #endregion

        #region Face Recognition错误类型

        /// <summary>
        /// Face Recognition错误类型
        /// </summary>
        [Description("Face Recognition错误类型")]
        MERR_FSDK_FR_ERROR_BASE = 0x12000,

        /// <summary>
        /// 无效的输入内存
        /// </summary>
        [Description("无效的输入内存")]
        MERR_FSDK_FR_INVALID_MEMORY_INFO = MERR_FSDK_FR_ERROR_BASE + 1,

        /// <summary>
        /// 无效的输入图像参数
        /// </summary>
        [Description("无效的输入图像参数")]
        MERR_FSDK_FR_INVALID_IMAGE_INFO = MERR_FSDK_FR_ERROR_BASE + 2,

        /// <summary>
        /// 无效的脸部信息
        /// </summary>
        [Description("无效的脸部信息")]
        MERR_FSDK_FR_INVALID_FACE_INFO = MERR_FSDK_FR_ERROR_BASE + 3,

        /// <summary>
        /// 当前设备无GPU可用
        /// </summary>
        [Description("当前设备无GPU可用")]
        MERR_FSDK_FR_NO_GPU_AVAILABLE = MERR_FSDK_FR_ERROR_BASE + 4,

        /// <summary>
        /// 待比较的两个人脸特征的版本不一致
        /// </summary>
        [Description("待比较的两个人脸特征的版本不一致")]
        MERR_FSDK_FR_MISMATCHED_FEATURE_LEVEL = MERR_FSDK_FR_ERROR_BASE + 5,

        #endregion

        #region 人脸特征检测错误类型

        /// <summary>
        /// 人脸特征检测错误类型
        /// </summary>
        [Description("人脸特征检测错误类型")]
        MERR_FSDK_FACEFEATURE_ERROR_BASE = 0x14000,

        /// <summary>
        /// 人脸特征检测错误未知
        /// </summary>
        [Description("人脸特征检测错误未知")]
        MERR_FSDK_FACEFEATURE_UNKNOWN = MERR_FSDK_FACEFEATURE_ERROR_BASE + 1,

        /// <summary>
        /// 人脸特征检测内存错误
        /// </summary>
        [Description("人脸特征检测内存错误")]
        MERR_FSDK_FACEFEATURE_MEMORY = MERR_FSDK_FACEFEATURE_ERROR_BASE + 2,

        /// <summary>
        /// 人脸特征检测格式错误
        /// </summary>
        [Description("人脸特征检测格式错误")]
        MERR_FSDK_FACEFEATURE_INVALID_FORMAT = MERR_FSDK_FACEFEATURE_ERROR_BASE + 3,

        /// <summary>
        /// 人脸特征检测参数错误
        /// </summary>
        [Description("人脸特征检测参数错误")]
        MERR_FSDK_FACEFEATURE_INVALID_PARAM = MERR_FSDK_FACEFEATURE_ERROR_BASE + 4,

        /// <summary>
        /// 人脸特征检测结果置信度低
        /// </summary>
        [Description("人脸特征检测结果置信度低")]
        MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL = MERR_FSDK_FACEFEATURE_ERROR_BASE + 5,

        #endregion

        #region  ASF错误类型

        /// <summary>
        /// ASF错误类型
        /// </summary>
        [Description("ASF错误类型")]
        MERR_ASF_EX_BASE = 0x15000,

        /// <summary>
        /// Engine不支持的检测属性
        /// </summary>
        [Description("Engine不支持的检测属性")]
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_INIT = MERR_ASF_EX_BASE + 1,

        /// <summary>
        /// 需要检测的属性未初始化
        /// </summary>
        [Description("需要检测的属性未初始化")]
        MERR_ASF_EX_FEATURE_UNINITED = MERR_ASF_EX_BASE + 2,

        /// <summary>
        /// 待获取的属性未在process中处理过
        /// </summary>
        [Description("待获取的属性未在process中处理过")]
        MERR_ASF_EX_FEATURE_UNPROCESSED = MERR_ASF_EX_BASE + 3,

        /// <summary>
        /// PROCESS不支持的检测属性组合，例如FR，有自己独立的处理函数
        /// </summary>
        [Description("PROCESS不支持的检测属性组合，例如FR，有自己独立的处理函数")]
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_PROCESS = MERR_ASF_EX_BASE + 4,

        /// <summary>
        /// 无效的输入图像
        /// </summary>
        [Description("无效的输入图像")]
        MERR_ASF_EX_INVALID_IMAGE_INFO = MERR_ASF_EX_BASE + 5,

        /// <summary>
        /// 无效的脸部信息
        /// </summary>
        [Description("无效的脸部信息")]
        MERR_ASF_EX_INVALID_FACE_INFO = MERR_ASF_EX_BASE + 6,


        #endregion

        #region 人脸比对基础错误类型

        /// <summary>
        /// 人脸比对基础错误类型
        /// </summary>
        [Description("人脸比对基础错误类型")]
        MERR_ASF_BASE = 0x16000,

        /// <summary>
        /// SDK激活失败,请打开读写权限
        /// </summary>
        [Description("SDK激活失败,请打开读写权限")]
        MERR_ASF_ACTIVATION_FAIL = MERR_ASF_BASE + 1,

        /// <summary>
        /// SDK已激活
        /// </summary>
        [Description("SDK已激活")]
        MERR_ASF_ALREADY_ACTIVATED = MERR_ASF_BASE + 2,

        /// <summary>
        /// SDK未激活
        /// </summary>
        [Description("SDK未激活")]
        MERR_ASF_NOT_ACTIVATED = MERR_ASF_BASE + 3,

        /// <summary>
        /// detectFaceScaleVal 不支持
        /// </summary>
        [Description("detectFaceScaleVal 不支持")]
        MERR_ASF_SCALE_NOT_SUPPORT = MERR_ASF_BASE + 4,

        /// <summary>
        /// 激活文件与SDK类型不匹配，请确认使用的sdk
        /// </summary>
        [Description("激活文件与SDK类型不匹配，请确认使用的sdk")]
        MERR_ASF_ACTIVEFILE_SDKTYPE_MISMATCH = MERR_ASF_BASE + 5,

        /// <summary>
        /// 设备不匹配
        /// </summary>
        [Description("设备不匹配")]
        MERR_ASF_DEVICE_MISMATCH = MERR_ASF_BASE + 6,

        /// <summary>
        /// 唯一标识不合法
        /// </summary>
        [Description("唯一标识不合法")]
        MERR_ASF_UNIQUE_IDENTIFIER_ILLEGAL = MERR_ASF_BASE + 7,

        /// <summary>
        /// 参数为空
        /// </summary>
        [Description("参数为空")]
        MERR_ASF_PARAM_NULL = MERR_ASF_BASE + 8,

        /// <summary>
        /// 活体已过期
        /// </summary>
        [Description("活体已过期")]
        MERR_ASF_LIVENESS_EXPIRED = MERR_ASF_BASE + 9,

        /// <summary>
        /// 版本不支持
        /// </summary>
        [Description("版本不支持")]
        MERR_ASF_VERSION_NOT_SUPPORT = MERR_ASF_BASE + 10,

        /// <summary>
        /// 签名错误
        /// </summary>
        [Description("签名错误")]
        MERR_ASF_SIGN_ERROR = MERR_ASF_BASE + 11,

        /// <summary>
        /// 激活信息保存异常
        /// </summary>
        [Description("激活信息保存异常")]
        MERR_ASF_DATABASE_ERROR = MERR_ASF_BASE + 12,

        /// <summary>
        /// 唯一标识符校验失败
        /// </summary>
        [Description("唯一标识符校验失败")]
        MERR_ASF_UNIQUE_CHECKOUT_FAIL = MERR_ASF_BASE + 13,

        /// <summary>
        /// 颜色空间不支持
        /// </summary>
        [Description("颜色空间不支持")]
        MERR_ASF_COLOR_SPACE_NOT_SUPPORT = MERR_ASF_BASE + 14,

        /// <summary>
        /// 图片宽高不支持，宽度需四字节对齐
        /// </summary>
        [Description("图片宽高不支持，宽度需四字节对齐")]
        MERR_ASF_IMAGE_WIDTH_HEIGHT_NOT_SUPPORT = MERR_ASF_BASE + 15,


        #endregion

        #region 人脸比对基础错误类型 扩展

        /// <summary>
        /// 人脸比对基础错误类型
        /// </summary>
        [Description("人脸比对基础错误类型扩展")]
        MERR_ASF_BASE_EXTEND = 0x16010,

        /// <summary>
        /// android.permission.READ_PHONE_STATE权限被拒绝
        /// </summary>
        [Description("android.permission.READ_PHONE_STATE权限被拒绝")]
        MERR_ASF_READ_PHONE_STATE_DENIED = MERR_ASF_BASE_EXTEND,

        /// <summary>
        /// 激活数据被破坏,请删除激活文件，重新进行激活
        /// </summary>
        [Description("激活数据被破坏,请删除激活文件，重新进行激活")]
        MERR_ASF_ACTIVATION_DATA_DESTROYED = MERR_ASF_BASE_EXTEND + 1,

        /// <summary>
        /// 服务端未知错误
        /// </summary>
        [Description("服务端未知错误")]
        MERR_ASF_SERVER_UNKNOWN_ERROR = MERR_ASF_BASE_EXTEND + 2,

        /// <summary>
        /// INTERNET权限被拒绝
        /// </summary>
        [Description("INTERNET权限被拒绝")]
        MERR_ASF_INTERNET_DENIED = MERR_ASF_BASE_EXTEND + 3,

        /// <summary>
        /// 激活文件与SDK版本不匹配,请重新激活
        /// </summary>
        [Description("激活文件与SDK版本不匹配,请重新激活")]
        MERR_ASF_ACTIVEFILE_SDK_MISMATCH = MERR_ASF_BASE_EXTEND + 4,

        /// <summary>
        /// 设备信息太少，不足以生成设备指纹
        /// </summary>
        [Description("设备信息太少，不足以生成设备指纹")]
        MERR_ASF_DEVICEINFO_LESS = MERR_ASF_BASE_EXTEND + 5,

        /// <summary>
        /// 客户端时间与服务器时间（即北京时间）前后相差在30分钟以上
        /// </summary>
        [Description("客户端时间与服务器时间（即北京时间）前后相差在30分钟以上")]
        MERR_ASF_REQUEST_TIMEOUT = MERR_ASF_BASE_EXTEND + 6,

        /// <summary>
        /// 数据校验异常
        /// </summary>
        [Description("数据校验异常")]
        MERR_ASF_APPID_DATA_DECRYPT = MERR_ASF_BASE_EXTEND + 7,

        /// <summary>
        /// 传入的AppId和AppKey与使用的SDK版本不一致
        /// </summary>
        [Description("传入的AppId和AppKey与使用的SDK版本不一致")]
        MERR_ASF_APPID_APPKEY_SDK_MISMATCH = MERR_ASF_BASE_EXTEND + 8,

        /// <summary>
        /// 短时间大量请求会被禁止请求,30分钟之后解封
        /// </summary>
        [Description("短时间大量请求会被禁止请求,30分钟之后解封")]
        MERR_ASF_NO_REQUEST = MERR_ASF_BASE_EXTEND + 9,

        /// <summary>
        /// 激活文件不存在
        /// </summary>
        [Description("激活文件不存在")]
        MERR_ASF_ACTIVE_FILE_NO_EXIST = MERR_ASF_BASE_EXTEND + 10,

        /// <summary>
        /// IMAGE模式下不支持全角度(ASF_OP_0_HIGHER_EXT)检测
        /// </summary>
        [Description("IMAGE模式下不支持全角度(ASF_OP_0_HIGHER_EXT)检测")]
        MERR_ASF_IMAGEMODE_0_HIGHER_EXT_UNSUPPORT = MERR_ASF_BASE_EXTEND + 11,


        #endregion

        #region 网络错误类型

        /// <summary>
        /// 网络错误类型
        /// </summary>
        [Description("网络错误类型")]
        MERR_ASF_NETWORK_BASE = 0x17000,

        /// <summary>
        /// 无法解析主机地址
        /// </summary>
        [Description("无法解析主机地址")]
        MERR_ASF_NETWORK_COULDNT_RESOLVE_HOST = MERR_ASF_NETWORK_BASE + 1,

        /// <summary>
        /// 无法连接服务器
        /// </summary>
        [Description("无法连接服务器")]
        MERR_ASF_NETWORK_COULDNT_CONNECT_SERVER = MERR_ASF_NETWORK_BASE + 2,

        /// <summary>
        /// 网络连接超时
        /// </summary>
        [Description("网络连接超时")]
        MERR_ASF_NETWORK_CONNECT_TIMEOUT = MERR_ASF_NETWORK_BASE + 3,

        /// <summary>
        /// 网络未知错误
        /// </summary>
        [Description("网络未知错误")]
        MERR_ASF_NETWORK_UNKNOWN_ERROR = MERR_ASF_NETWORK_BASE + 4,


        #endregion

    }
}
