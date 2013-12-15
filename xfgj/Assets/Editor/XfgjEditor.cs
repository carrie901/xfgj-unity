using UnityEngine;
using UnityEditor;
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


    [MenuItem ("Custom/Save Scene")]
    static void SaveScene () {
        EditorApplication.SaveScene("", false);
    }

    [MenuItem ("Custom/Scene Bundle")]
    static void ShowSceneBundleWindow () {
        SceneBundleWindow window = (SceneBundleWindow)EditorWindow.GetWindow(typeof(SceneBundleWindow));
    }

    [MenuItem ("Custom/Atlas Bundle")]
    static void ShowAtlasBundleWindow () {
        AtlasBundleWindow window = (AtlasBundleWindow)EditorWindow.GetWindow(typeof(AtlasBundleWindow));
    }

}
