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
            BuildPipeline.BuildAssetBundle(obj, null, targetUnity, BuildAssetBundleOptions.CollectDependencies);
            BuildPipeline.BuildAssetBundle(obj, null, targetIos, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
            BuildPipeline.BuildAssetBundle(obj, null, targetAndroid, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
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
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetUnity, BuildAssetBundleOptions.CollectDependencies);
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetIos, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
        BuildPipeline.BuildAssetBundle(null, SelectedAsset, targetAndroid, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        AssetDatabase.Refresh();
    }

}
