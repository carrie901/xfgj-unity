using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using LitJson;

public class SceneBundleWindow : EditorWindow {

    private string windowName = "Scene Bundle";
    private int sceneCount = 0;
    private const int ARRAY_DELTA = 5;
    private SceneAsset[] sceneArr = new SceneAsset[ARRAY_DELTA];

    void OnGUI () {
        this.title = windowName;
        GUILayout.BeginVertical();
        GUILayout.Label("Make Scene bundle");
        for (int i = 0; i < sceneCount; ++i) {
            XfgjComponentSelector.Draw<SceneAsset>("Select", sceneArr[i], i, OnSelectScene);
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add", GUILayout.MinWidth(50))) {
            sceneCount++;
            if (sceneCount > sceneArr.Length) {
                SceneAsset[] tmpArr = sceneArr;
                sceneArr = new SceneAsset[tmpArr.Length + ARRAY_DELTA];
                for (int i = 0; i < tmpArr.Length; ++i) {
                    sceneArr[i] = tmpArr[i];
                }
            }
        }
        if (GUILayout.Button("Build", GUILayout.MinWidth(50))) {
            CreateBundle();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    void OnSelectScene (Object obj, int? index) {
        if (index != null) {
            sceneArr[(int)index] = obj as SceneAsset;
        }
        Repaint();
    }

    void CreateBundle () {
        Caching.CleanCache();
        string fileName = StringUtil.GetGuid() + XfgjEditor.SCENE_BUNDLE_EXT;
        string targetUnity = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_UNITY + fileName;
        string targetIos = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_IPHONE + fileName;
        string targetAndroid = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_ANDROID + fileName;
        List<string> levels = new List<string>();
        for (int i = 0; i < sceneArr.Length; ++i) {
            if (sceneArr[i] != null) {
                levels.Add(sceneArr[i].assetPath);
            }
        }
        BuildPipeline.BuildStreamedSceneAssetBundle(levels.ToArray(), targetIos, BuildTarget.iPhone);
        BuildPipeline.BuildStreamedSceneAssetBundle(levels.ToArray(), targetAndroid, BuildTarget.Android);
        BuildPipeline.BuildStreamedSceneAssetBundle(levels.ToArray(), targetUnity, BuildTarget.StandaloneOSXIntel64);
        AssetDatabase.Refresh();
        string metaData = BuildMetaData(fileName, levels);
        ApiController.UploadScene(AppSetting.getInstance().token, metaData, targetUnity,
                                  targetIos, targetAndroid, callback);
    }

    string BuildMetaData (string fileName, List<string> scenes) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter jw = new JsonWriter(sb);
        jw.WriteObjectStart();
        jw.WritePropertyName(Param.ASSET_NAME);
        jw.Write(fileName);
        jw.WritePropertyName(Param.SCENES);
        jw.WriteArrayStart();
        foreach (string path in scenes) {
            int sceneId = int.Parse(StringUtil.GetFileNameWithoutExt(path));
            Scene scene = LogicController.GetScene(sceneId);
            jw.WriteObjectStart();
            jw.WritePropertyName(Param.SCENE_ID);
            jw.Write(sceneId);
            jw.WritePropertyName(Param.PRODUCTS);
            jw.Write(scene.products);
            jw.WriteObjectEnd();
        }
        jw.WriteArrayEnd();
        jw.WriteObjectEnd();
        return sb.ToString();
    }

    void callback (string res) {
        Debug.Log("upload scene " + res);
    }
}

