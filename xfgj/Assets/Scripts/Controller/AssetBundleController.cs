using UnityEngine;
using System;
using System.Collections;

public class AssetBundleController : MonoBehaviour
{
    public delegate void LoadCallback (UnityEngine.Object[] objs);

    public class LoadParam {
        public string path;
        public int version;
        public string[] name;
        public LoadCallback callback;
    }

    private static GameObject obj;

    public static void LoadObject (LoadParam param) {
        if (obj == null) {
            obj = GameObject.Find("InitObj");
        }
        obj.SendMessage("StartLoadObject", param, SendMessageOptions.RequireReceiver);
    }

    public static void LoadScene (LoadParam param) {
        if (obj == null) {
            obj = GameObject.Find("InitObj");
        }
        obj.SendMessage("StartLoadScene", param, SendMessageOptions.RequireReceiver);
    }

    private void StartLoadScene (LoadParam param) {
        StartCoroutine(LoadSceneProgram(param.path, param.version, param.name, param.callback));
    }

    private IEnumerator LoadSceneProgram (string path, int version, string[] names, LoadCallback callback) {
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
        }
        AssetBundle bundle = www.assetBundle;
        AsyncOperation oper = Application.LoadLevelAdditiveAsync(names[0]);
        yield return oper;
        bundle.Unload(false);
    }

    private void StartLoadObject (LoadParam param) {
        if (param == null || param.path == null || param.path.Equals(string.Empty)
            || param.name == null || param.callback == null) {
            throw new Exception("param can't be null");
        }
        StartCoroutine(LoadObjectProgram(param.path, param.version, param.name, param.callback));
    }

    private IEnumerator LoadObjectProgram (string path, int version, string[] names, LoadCallback callback) {
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        for (int i = 0; i < names.Length; ++i) {
            if (names[i] != null) {
                objs[i] = Instantiate(www.assetBundle.Load(names[i]));
            }
            else {
                objs[i] = null;
            }
        }
        callback(objs);
        www.assetBundle.Unload(false);
    }
}

