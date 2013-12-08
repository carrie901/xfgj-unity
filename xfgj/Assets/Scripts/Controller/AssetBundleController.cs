using UnityEngine;
using System;
using System.Collections;

public class AssetBundleController : MonoBehaviour
{
    public delegate void LoadCallback (UnityEngine.Object[] objs);

    public class LoadParam {
        public string path;
        public string[] name;
        public LoadCallback callback;
    }

    private static GameObject obj;

    public static void LoadObject(LoadParam param) {
        if (obj == null) {
            obj = GameObject.Find("InitObj");
        }
        obj.SendMessage("LoadGameObject", param, SendMessageOptions.RequireReceiver);
    }

    private void LoadGameObject(LoadParam param) {
        if (param == null || param.path == null || param.path.Equals(string.Empty)
            || param.name == null || param.callback == null) {
            throw new Exception("param can't be null");
        }
        StartCoroutine(LoadGameObject(param.path, param.name, param.callback));
    }

    private IEnumerator LoadGameObject (string path, string[] names, LoadCallback callback) {
        WWW bundle = WWW.LoadFromCacheOrDownload(path, 1);
        yield return bundle;
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        for (int i = 0; i < names.Length; ++i) {
            if (names[i] != null) {
                objs[i] = Instantiate(bundle.assetBundle.Load(names[i]));
            }
            else {
                objs[i] = null;
            }
        }
        callback(objs);
        bundle.assetBundle.Unload(false);
    }
}

