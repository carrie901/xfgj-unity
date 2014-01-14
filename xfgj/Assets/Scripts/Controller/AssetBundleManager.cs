using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour {

    public delegate void LoadCallback (UnityEngine.Object[] objs);

    class Operation {
        public string path;
        public int version;
        public string[] names;
        public LoadCallback callback;

        public Operation (string path, int version, string[] names, LoadCallback callback) {
            this.path = path;
            this.version = version;
            this.names = names;
            this.callback = callback;
        }
    }

    private Queue<Operation> operQueue;
    private bool isRunning;

    private static GameObject obj;
    private static AssetBundleManager manager;
    #region MonoBehaviour
    void Awake () {
        obj = GameObject.Find("InitObj");
        operQueue = new Queue<Operation>();
    }

    void Start () {
    }
    #endregion

    public static void GetObject (string path, int version, string[] names, LoadCallback callback) {
        if (manager == null) {
            manager = obj.GetComponent<AssetBundleManager>();
        }
        manager.LoadObject(path, version, names, callback);
    }

    public void LoadObject (string path, int version, string[] names, LoadCallback callback) {
        if (string.IsNullOrEmpty(path) || version < 0 || names == null || callback == null) {
            if (callback != null) {
                callback(null);
            }
            return;
        }
        if (!Caching.IsVersionCached(path, version)
            && (Application.internetReachability == NetworkReachability.NotReachable
                || (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork
                    && AppSetting.getInstance().wifiLimit))) {
            Debug.Log("can't download");
            callback(null);
        }
        operQueue.Enqueue(new Operation(path, version, names, callback));
        if (!isRunning) {
            StartCoroutine(LoadObjectProgram());
        }
    }

    private IEnumerator LoadObjectProgram () {
        isRunning = true;
        Operation oper;
        while (operQueue.Count != 0 && (oper = operQueue.Dequeue()) != null) {
            Debug.Log("Load object");
            WWW www = WWW.LoadFromCacheOrDownload(oper.path, oper.version);
            yield return www;
            if (www.error != null) {
                Debug.Log(www.error);
                oper.callback(null);
            }
            else {
                UnityEngine.Object[] objs = new UnityEngine.Object[oper.names.Length];
                for (int i = 0; i < oper.names.Length; ++i) {
                    if (oper.names[i] != null) {
                        objs[i] = www.assetBundle.Load(oper.names[i]);
                    }
                    else {
                        objs[i] = null;
                    }
                }
                oper.callback(objs);
                Debug.Log("unload object");
                www.assetBundle.Unload(false);
            }
        }
        isRunning = false;
    }
}

