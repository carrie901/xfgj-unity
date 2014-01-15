using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public delegate void NotifyProgress (float progress);

    public static readonly float LOAD_FAIL = -1.0f;

    private NotifyProgress notifyCallback;
    private AsyncOperation oper;

    private static SceneManager manager;
    #region MonoBehaviour
    void Awake () {
        manager = this;
    }
    
    void Update () {
        if (notifyCallback != null && oper != null) {
            notifyCallback(oper.progress);
            if (oper.isDone) {
                notifyCallback = null;
                oper = null;
            }
        }
    }
    #endregion

    public static void LoadLevelAdditive(string path, int version, string name, NotifyProgress callback) {
        if (manager == null) {
            if (callback != null) {
                callback(LOAD_FAIL);
            }
            return;
        }
        manager.LoadScene(path, version, name, callback);
    }

    public void LoadScene (string path, int version, string name, NotifyProgress callback) {
        if (string.IsNullOrEmpty(path) || version < 0 || string.IsNullOrEmpty(name) || callback == null) {
            if (callback != null) {
                callback(LOAD_FAIL);
            }
            return;
        }
        if (!Caching.IsVersionCached(path, version)
            && (Application.internetReachability == NetworkReachability.NotReachable
                || (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork
                    && AppSetting.getInstance().wifiLimit))) {
            Debug.Log("can't download");
            callback(LOAD_FAIL);
        }
        StartCoroutine(LoadSceneProgram(path, version, name, callback));
    }

    private IEnumerator LoadSceneProgram (string path, int version, string name, NotifyProgress callback) {
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
            callback(LOAD_FAIL);
        }
        else {
            AssetBundle bundle = www.assetBundle;
            AsyncOperation oper = Application.LoadLevelAdditiveAsync(name);
            this.oper = oper;
            this.notifyCallback = callback;
            yield return oper;
            bundle.Unload(false);
        }
    }
}

