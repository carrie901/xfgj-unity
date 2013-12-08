#define XFGJ_RELEASE
#undef XFGJ_RELEASE
using UnityEngine;

public class Config {
    public static readonly string DB_PATH;
    public static readonly string STREAMING_ASSETS;
    public static readonly string ASSETBUNDLE_PATH;

    public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public const string APP_KEY = "T6rfDzeRmQdT9J7x";

    public static readonly string SERVER_URL;

    static Config () {
#if UNITY_ANDROID
        DB_PATH = "URI=file:" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = "jar:file://" + Application.dataPath + "!/assets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Android/";
#elif UNITY_IPHONE
        DB_PATH = "Data Source=" + Application.persistentDataPath + "/xfgj.sqlite";
        STREAMING_ASSETS = Application.dataPath + "/Raw/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Ios/";
#else
        DB_PATH = "Data Source=xfgj.sqlite";
        STREAMING_ASSETS = "file://" + Application.dataPath + "/StreamingAssets/";
        ASSETBUNDLE_PATH = STREAMING_ASSETS + "Unity/";
#endif
#if XFGJ_RELEASE
        SERVER_URL = "http://www.xingfuguanjia.com";
#else
        SERVER_URL = "http://127.0.0.1:8000";
#endif
    }
    



    
}
