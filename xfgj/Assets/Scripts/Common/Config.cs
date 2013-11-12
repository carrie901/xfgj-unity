using UnityEngine;

public class Config {

    public const bool RELEASE = false;

    public const int PLATFORM_IOS = 1;
    public const int PLATFORM_ANDROID = 2;
    public const int PLATFORM_UNITY = 3;
    
    public static readonly int BUILD_PLATFORM = PLATFORM_UNITY;
    public static readonly string DB_PATH;

    public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public const string APP_KEY = "T6rfDzeRmQdT9J7x";

    public static readonly string SERVER_URL;

    static Config () {
        if (BUILD_PLATFORM == PLATFORM_IOS) {
            DB_PATH = "Data Source=" + Application.persistentDataPath + "/xfgj.sqlite";
        }
        else if (BUILD_PLATFORM == PLATFORM_ANDROID) {
            DB_PATH = "URI=file:" + Application.persistentDataPath + "/xfgj.sqlite";
        }
        else {
            DB_PATH = "Data Source=xfgj.sqlite";
        }
        if (RELEASE) {
            SERVER_URL = "http://www.xingfuguanjia.com";
        }
        else {
            SERVER_URL = "http://127.0.0.1:8000";
        }
    }
    



    
}
