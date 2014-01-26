using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using LitJson;

public class XfgjEditor {

    public static readonly string ASSETS_PATH = Application.dataPath;
    public static readonly string BUNDLE_PATH_UNITY = "/StreamingAssets/Unity/";
    public static readonly string BUNDLE_PATH_IPHONE = "/StreamingAssets/Ios/";
    public static readonly string BUNDLE_PATH_ANDROID = "/StreamingAssets/Android/";
    public static readonly string SCENE_FOLDER = "Assets/Scenes/";
    public static readonly string ASSETBUNDLE_EXT = ".assetbundle";
    public static readonly string SCENE_EXT = ".unity";
    public static readonly string SCENE_BUNDLE_EXT = ".unity3d";


    [MenuItem ("Custom/Build Scene Bundle", false, 0)]
    static void ShowSceneBundleWindow () {
        EditorWindow.GetWindow(typeof(SceneBundleWindow));
    }

    [MenuItem ("Custom/Build Atlas Bundle", false, 1)]
    static void ShowAtlasBundleWindow () {
        EditorWindow.GetWindow(typeof(AtlasBundleWindow));
    }

    [MenuItem ("Custom/Save Scene Metadata", false, 3)]
    static void SaveSceneMetadata () {
        string scenePath = Application.dataPath + EditorApplication.currentScene.Substring("Assets".Length);
        string metaPath = scenePath.Substring(0, scenePath.Length - "unity".Length) + "txt";
        StreamWriter sw = new StreamWriter(metaPath, false);
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos) {
            TraverseGo(go, sw);
        }
        sw.Close();
        Debug.Log("Save scene Metadata success");
    }

    [MenuItem ("Custom/Init Scene", false, 4)]
    static void InitializeScene () {
        string scenePath = Application.dataPath + EditorApplication.currentScene.Substring("Assets".Length);
        string metaPath = scenePath.Substring(0, scenePath.Length - "unity".Length) + "txt";
        FileInfo file = new FileInfo(metaPath);
        if (!file.Exists) {
            Debug.Log("The scene " + StringUtil.GetFileName(scenePath) + " don't have meta file");
            return;
        }
        StreamReader sr = new StreamReader(metaPath);
        string line;
        while ((line = sr.ReadLine()) != null) {
            JsonData jd = JsonMapper.ToObject(line);
            GameObject go = GameObject.Find((string)jd["name"]);
            if (go == null) {
                continue;
            }
            go.SetActive((bool)jd["active"]);
            jd = jd["scripts"];
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts) {
                for (int i = 0; i < jd.Count; ++i) {
                    if (script.GetType().ToString().Equals((string)jd[i]["name"])) {
                        script.enabled = (bool)jd[i]["enable"];
                    }
                }
            }
        }
        sr.Close();
        Debug.Log("Init Scene success");
    }

    private static void TraverseGo(GameObject go, StreamWriter sw) {
        if (go == null) {
            return;
        }
        JsonWriter jw = new JsonWriter(sw);
        jw.WriteObjectStart();
        jw.WritePropertyName("name");
        jw.Write(GetFullName(go));
        jw.WritePropertyName("active");
        jw.Write(go.activeSelf);
        jw.WritePropertyName("scripts");
        jw.WriteArrayStart();
        MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
        for (int i = 0; i < scripts.Length; i++) {
            if (scripts[i] != null) {
                jw.WriteObjectStart();
                jw.WritePropertyName("name");
                jw.Write(scripts[i].GetType().ToString());
                jw.WritePropertyName("enable");
                jw.Write(scripts[i].enabled);
                jw.WriteObjectEnd();
            }
        }
        jw.WriteArrayEnd();
        jw.WriteObjectEnd();
        sw.WriteLine();
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in go.transform) {
            TraverseGo(childT.gameObject, sw);
        }
    }

    private static string GetFullName (GameObject go) {
        if (go == null) {
            return null;
        }
        string name = go.name;
        Transform t = go.transform;
        while (t.parent != null) {
            name = t.parent.name + "/" + name;
            t = t.parent;
        }
        return name;
    }
}
