using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using LitJson;

public class AtlasBundleWindow : EditorWindow {

    private string windowName = "Atlas Bundle";
    private int atlasCount = 0;
    private const int ARRAY_DELTA = 5;
    private UIAtlas[] atlasArr = new UIAtlas[ARRAY_DELTA];

    void OnGUI () {
        this.title = windowName;
        GUILayout.BeginVertical();
        GUILayout.Label("Make Atlas bundle");
        for (int i = 0; i < atlasCount; ++i) {
            XfgjComponentSelector.Draw<UIAtlas>("Select", atlasArr[i], i, OnSelectAtlas);
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add", GUILayout.MinWidth(50))) {
            atlasCount++;
            if (atlasCount > atlasArr.Length) {
                UIAtlas[] tmpArr = atlasArr;
                atlasArr = new UIAtlas[tmpArr.Length + ARRAY_DELTA];
                for (int i = 0; i < tmpArr.Length; ++i) {
                    atlasArr[i] = tmpArr[i];
                }
            }
        }
        if (GUILayout.Button("Build", GUILayout.MinWidth(50))) {
            CreateBundle();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    void OnSelectAtlas (Object obj, int? index) {
        if (index != null) {
            atlasArr[(int)index] = obj as UIAtlas;
        }
        Repaint();
    }

    void CreateBundle () {
        Caching.CleanCache();
        string fileName = StringUtil.GetGuid() + XfgjEditor.ASSETBUNDLE_EXT;
        string targetUnity = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_UNITY + fileName;
        string targetIos = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_IPHONE + fileName;
        string targetAndroid = XfgjEditor.ASSETS_PATH + XfgjEditor.BUNDLE_PATH_ANDROID + fileName;
        BuildPipeline.BuildAssetBundle(null, atlasArr, targetIos, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
        BuildPipeline.BuildAssetBundle(null, atlasArr, targetAndroid, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        BuildPipeline.BuildAssetBundle(null, atlasArr, targetUnity, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneOSXIntel64);
        AssetDatabase.Refresh();
        string metaData = BuildMetaData(fileName);
        ApiController.UploadAtlas(AppSetting.getInstance().token, metaData, targetUnity,
                                  targetIos, targetAndroid, callback);
    }

    string BuildMetaData (string fileName) {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter jw = new JsonWriter(sb);
        jw.WriteObjectStart();
        jw.WritePropertyName(Param.ASSET_NAME);
        jw.Write(fileName);
        jw.WritePropertyName(Param.ATLASES);
        jw.WriteArrayStart();
        foreach (UIAtlas atlas in atlasArr) {
            if (atlas == null) {
                continue;
            }
            jw.WriteObjectStart();
            jw.WritePropertyName(Param.ATLAS_NAME);
            jw.Write(atlas.name);
            jw.WritePropertyName(Param.PICTURES);
            jw.WriteArrayStart();
            BetterList<string> sprites = atlas.GetListOfSprites();
            foreach (string pictureId in sprites) {
                jw.Write(pictureId);
            }
            jw.WriteArrayEnd();
            jw.WriteObjectEnd();
        }
        jw.WriteArrayEnd();
        jw.WriteObjectEnd();
        return sb.ToString();
    }

    void callback (string res) {
        Debug.Log("upload atlas res " + res);
    }

}

