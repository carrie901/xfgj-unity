using UnityEngine;

public class Config {
    
	public const int PLATFORM_IOS = 1;
	public const int PLATFORM_ANDROID = 2;
	public const int PLATFORM_UNITY = 3;
	
	public static readonly int BUILD_PLATFORM = PLATFORM_UNITY;
    public static readonly string DB_PATH;
	
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
	}
	
	public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
    
}
