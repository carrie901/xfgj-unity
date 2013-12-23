using UnityEngine;
using System;

public class AppSetting
{
    private static AppSetting instance;

    public static AppSetting getInstance() {
        if (instance == null) {
            instance = new AppSetting();
        }
        return instance;
    }

    AppSetting () {
        
    }

    private const string TOKEN = "token";
    public string token {
        get {
            return PlayerPrefs.GetString(TOKEN, null);
        }

        set {
            PlayerPrefs.SetString(TOKEN, value);
        }
    }

    private const string ASSET_URL = "asset_url";
    public string assetUrl {
        get {
            return PlayerPrefs.GetString(ASSET_URL, Config.ASSET_URL);
        }

        set {
            PlayerPrefs.SetString(ASSET_URL, value);
        }
    }

    private const string SERVER_URL = "server_url";
    public string serverUrl {
        get {
            return PlayerPrefs.GetString(SERVER_URL, Config.SERVER_URL);
        }

        set {
            PlayerPrefs.SetString(SERVER_URL, value);
        }
    }

}

