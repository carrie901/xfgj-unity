#define XFGJ_RELEASE
#undef XFGJ_RELEASE
using UnityEngine;

public class Config {

    #region app info
    public const string APP_KEY = "T6rfDzeRmQdT9J7x";
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
    #endregion


    static Config () {
#if UNITY_ANDROID
        DB_PATH = "URI=file:" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = "jar:file://" + Application.dataPath + "!/assets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Android/";
        APP_VERSION = "Android";
#elif UNITY_IPHONE
        DB_PATH = "Data Source=" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = Application.dataPath + "/Raw/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Ios/";
        APP_VERSION = "iPhone"
#else
        DB_PATH = "Data Source=xfgj.sqlite";
        STREAMING_ASSETS = "file://" + Application.dataPath + "/StreamingAssets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Unity/";
        APP_VERSION = "Unity";
#endif
#if XFGJ_RELEASE
        SERVER_URL = "http://www.xingfuguanjia.com";
#else
        SERVER_URL = "http://127.0.0.1:8000";
#endif
        ASSET_URL = "http://asset-20131210.u.qiniudn.com/";
    }
    



    
}
