using UnityEngine;
using System;

public class SettingViewController : MonoBehaviour {


    public GameObject version;
    public GameObject checkUpdate;
    public GameObject wifiLimit;
    public GameObject wifiLimitState;
    public GameObject cacheSpace;
    public GameObject clearCache;
    public GameObject appUpdate;


    #region MonoBehaviour
    void Awake () {
        UIEventListener.Get(checkUpdate).onClick = CheckUpdate;
        UIEventListener.Get(wifiLimit).onClick = ToggleWifiLimit;
        UIEventListener.Get(clearCache).onClick = ClearCache;

    }

    void Start () {
        version.GetComponent<UILabel>().text = Localization.Localize(StringKey.CurrentVersion) + ": " + MultiPlatform.GetAppVersion();
        wifiLimitState.GetComponent<UILabel>().text = Localization.Localize(AppSetting.getInstance().wifiLimit ? StringKey.On : StringKey.Off);
        cacheSpace.GetComponent<UILabel>().text = GetCacheSpace();
    }
    #endregion

    #region private
    private void CheckUpdate (GameObject go) {
        Debug.Log("CheckUpdate");
        appUpdate.GetComponent<AppUpdateController>().CheckUpdate();
    }

    private void ToggleWifiLimit (GameObject go) {
        Debug.Log("ToggleWifiLimit");
        AppSetting.getInstance().wifiLimit = !AppSetting.getInstance().wifiLimit;
        wifiLimitState.GetComponent<UILabel>().text = Localization.Localize(AppSetting.getInstance().wifiLimit ? StringKey.On : StringKey.Off);
    }

    private void ClearCache (GameObject go) {
        Debug.Log("ClearCache");
        if (Caching.CleanCache()) {
            cacheSpace.GetComponent<UILabel>().text = GetCacheSpace();
        }
        NotificationViewController.ShowNotification(Localization.Localize(StringKey.Msg_ClearCacheSuccess));
    }

    private string GetCacheSpace () {
        long space = Caching.spaceOccupied;
        if (space >= 1024 * 1024 * 1024) {
            return string.Format("{0:f} GB", space / (1024.0f * 1024.0f * 1024.0f));
        }
        else if (space >= 1024 * 1024) {
            return string.Format("{0:f} MB", space / (1024.0f * 1024.0f));
        }
        else {
            return string.Format("{0:f} KB", space / (1024.0f));
        }
    }
    #endregion

}

