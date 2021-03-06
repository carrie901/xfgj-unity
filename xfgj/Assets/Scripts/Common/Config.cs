﻿#define XFGJ_RELEASE
#undef XFGJ_RELEASE
using UnityEngine;

public class Config {

    #region app info
    public const string APP_KEY = "T6rfDzeRmQdT9J7x";
    public static readonly string APP_NAME;
    public static readonly string APP_VERSION;
    #endregion

    #region app local path
    public static readonly string DB_PATH;
    public static readonly string STREAMING_ASSETS;
    public static readonly string ASSETBUNDLE_PATH;
    #endregion

    #region server info
    public static readonly string SERVER_URL;
    public static readonly string ASSET_URL;
    #endregion

    #region other
    public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public const string TAG_UI = "UI";
    public const string TAG_SCENE = "Scene";
    public const string TAG_MAIN_CAMERA = "MainCamera";
    public const string TAG_ROAM_CAMERA = "RoamCamera";
    public const string TAG_GESTURE = "Gesture";
    public const string TAG_CHARACTER = "Character";
    #endregion


    static Config () {
#if UNITY_ANDROID
        DB_PATH = "URI=file:" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = "jar:file://" + Application.dataPath + "!/assets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Android/";
        APP_NAME = "wanzhuangxiu_android";
        APP_VERSION = "1.0";
        ASSET_URL = "http://xfgj-asset-android.u.qiniudn.com/";
#elif UNITY_IPHONE
        DB_PATH = "Data Source=" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = Application.dataPath + "/Raw/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Ios/";
        APP_NAME = "wanzhuangxiu_iphone";
        APP_VERSION = IosPlugin.GetBundleVersion();
        ASSET_URL = "http://xfgj-asset-iphone.u.qiniudn.com/";
#else
        DB_PATH = "Data Source=xfgj.sqlite";
        STREAMING_ASSETS = "file://" + Application.dataPath + "/StreamingAssets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Unity/";
        APP_NAME = "wanzhuangxiu_unity";
        APP_VERSION = "1.0";
        ASSET_URL = "http://xfgj-asset-unity.u.qiniudn.com/";
#endif
#if XFGJ_RELEASE
        SERVER_URL = "http://www.xingfuguanjia.com";
#else
        SERVER_URL = "http://192.168.1.101:8000";
#endif
    }
    



    
}
