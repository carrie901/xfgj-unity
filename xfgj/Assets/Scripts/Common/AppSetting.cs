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

}

