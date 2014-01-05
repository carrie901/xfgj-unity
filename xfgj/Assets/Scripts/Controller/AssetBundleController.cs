using UnityEngine;
using System.Collections;

public class AssetBundleController : MonoBehaviour
{
    public delegate void LoadCallback (UnityEngine.Object[] objs);
    public delegate void NotifyProgress (float progress);

    public class LoadParam {
        public string path;
        public int version;
        public string[] name;
        public LoadCallback callback;
        public NotifyProgress notify;
    }

    #region members
    private static GameObject obj;
    private NotifyProgress notifyCallback;
    private AsyncOperation oper;
    #endregion

    #region MonoBehaviour
    void Awake () {
        obj = GameObject.Find("InitObj");
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

    #region public
    public static void LoadObject (LoadParam param) {
        obj.SendMessage("StartLoadObject", param, SendMessageOptions.RequireReceiver);
    }

    public static void LoadScene (LoadParam param) {
        obj.SendMessage("StartLoadScene", param, SendMessageOptions.RequireReceiver);
    }
    #endregion

    #region private
    private void StartLoadScene (LoadParam param) {
        StartCoroutine(LoadSceneProgram(param.path, param.version, param.name, param.notify));
    }

    private IEnumerator LoadSceneProgram (string path, int version, string[] names, NotifyProgress notify) {
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
            if (notify != null) {
                notify(-1.0f);
            }
        }
        else {
            AssetBundle bundle = www.assetBundle;
            AsyncOperation oper = Application.LoadLevelAdditiveAsync(names[0]);
            this.oper = oper;
            this.notifyCallback = notify;
            yield return oper;
            bundle.Unload(false);
        }
    }

    private void StartLoadObject (LoadParam param) {
        if (param == null || param.path == null || param.path.Equals(string.Empty)
            || param.name == null || param.callback == null) {
            throw new System.ArgumentNullException("param can't be null");
        }
        StartCoroutine(LoadObjectProgram(param.path, param.version, param.name, param.callback));
    }

    private IEnumerator LoadObjectProgram (string path, int version, string[] names, LoadCallback callback) {
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
            if (callback != null) {
                callback(null);
            }
        }
        else {
            UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
            for (int i = 0; i < names.Length; ++i) {
                if (names[i] != null) {
                    Object obj = FindInLoadedAssets(path, version, names[i]);
                    if (obj != null) {
                        objs[i] = obj;
                    }
                    else {
                        objs[i] = Instantiate(www.assetBundle.Load(names[i]));
                        AddInLoadedAssets(path, version, names[i], objs[i]);
                    }
                }
                else {
                    objs[i] = null;
                }
            }
            if (callback != null) {
                callback(objs);
            }
            www.assetBundle.Unload(false);
        }
    }
    #endregion

    #region LoadedAsset
    private class LoadedAsset {

        public string path;
        public int version;
        public string name;

        public Object obj;
    }

    private BetterList<LoadedAsset> list = new BetterList<LoadedAsset>();

    private Object FindInLoadedAssets (string path, int version, string name) {
        foreach (LoadedAsset asset in list) {
            if (asset != null && asset.path.Equals(path) && asset.version == version
                && asset.name.Equals(name)) {
                return asset.obj;
            }
        }
        return null;
    }

    private void AddInLoadedAssets (string path, int version, string name, Object obj) {
        LoadedAsset loadedAsset = new LoadedAsset();
        loadedAsset.path = path;
        loadedAsset.version = version;
        loadedAsset.name = name;
        loadedAsset.obj = obj;
        list.Add(loadedAsset);
    }
    #endregion

}

