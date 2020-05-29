# ArcFaceWrapper

C# 人脸识别 
基于ArcSoft Face Engine SDK 2.2 C++

配置文件 app.config
   add key="AppId" value="你申请的appid"
    add key="SdkKey32" value="你申请的sdkkey32位"
    add key="SdkKey64" value="你申请的sdkkey64位"
    add key="FileFaceDataRepository" value="Afw.Data,Afw.Data.FileFaceDataRepository`1"

FileFaceDataRepository 是人脸数据源的实现接口 （Afw.Data项目下的IFaceDataRepository.cs）
Afw.Data.FileFaceDataRepository 是基于这个接口用本地文件存储实现的人脸数据源，默认没有使用数据库存储的实现，有兴趣或是有需要的朋友可以依照接口规范自己写实现类
