using UnityEngine;
using UnityEditor;
using System.Collections;

static public class XfgjEditor {

    private static readonly string STREAMING_ASSETS = Application.dataPath + "/StreamingAssets/";

    [MenuItem("Custom/Create AssetBundle Main")]
    static void CreateAssetBundleMain () {
        Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            string targetUnity = STREAMING_ASSETS + "Unity/" + obj.name + ".assetbundle";
            string targetIos = STREAMING_ASSETS + "Ios/" + obj.name + ".assetbundle";
            string targetAndroid = STREAMING_ASSETS + "Android/" + obj.name + ".assetbundle";
            BuildPipeline.BuildAssetBundle(obj, null, targetIos, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
            BuildPipeline.BuildAssetBundle(obj, null, targetAndroid, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
            BuildPipeline.BuildAssetBundle(obj, null, targetUnity, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneOSXIntel64);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Custom/Create AssetBundle All")]
    static void CreateAssetBundleAll () {
        Caching.CleanCache();
        string targetUnity = STREAMING_ASSETS + "Unity/All.assetbundle";
        string targetIos = STREAMING_ASSETS + "Ios/All.assetbundle";
        string targetAndroid = STREAMING_ASSETS + "Android/All.assetbundle";
        Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            Debug.Log ("Create AssetBundles name: " + obj);
        }
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetIos, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetAndroid, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetUnity, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneOSXIntel64);
        AssetDatabase.Refresh();
    }

    [MenuItem("Custom/Create Scene AssetBundle")]
    static void CreateSceneAssetBundle () {
        Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset) {
            string targetUnity = STREAMING_ASSETS + "Unity/" + obj.name + ".unity3d";
            string targetIos = STREAMING_ASSETS + "Ios/" + obj.name + ".unity3d";
            string targetAndroid = STREAMING_ASSETS + "Android/" + obj.name + ".unity3d";
            string[] levels = new string[] {AssetDatabase.GetAssetPath(obj)};
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, targetIos, BuildTarget.iPhone);
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, targetAndroid, BuildTarget.Android);
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, targetUnity, BuildTarget.StandaloneOSXIntel64);
        }
        AssetDatabase.Refresh();
    }

}
