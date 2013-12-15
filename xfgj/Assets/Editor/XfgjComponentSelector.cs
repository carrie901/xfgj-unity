//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// EditorGUILayout.ObjectField doesn't support custom components, so a custom wizard saves the day.
/// Unfortunately this tool only shows components that are being used by the scene, so it's a "recently used" selection tool.
/// </summary>

public class XfgjComponentSelector : ScriptableWizard
{
    public delegate void OnSelectionCallback (Object obj, int? index);

    System.Type mType;
    OnSelectionCallback mCallback;
    int? index;
    Object[] mObjects;

    Vector2 scrollPosition;

    static string GetName (System.Type t)
    {
        string s = t.ToString();
        s = s.Replace("UnityEngine.", "");
        if (s.StartsWith("UI")) s = s.Substring(2);
        return s;
    }

    /// <summary>
    /// Draw a button + object selection combo filtering specified types.
    /// </summary>

    static public void Draw<T> (string buttonName, T obj, int? index, OnSelectionCallback cb, params GUILayoutOption[] options) where T : Object
    {
        GUILayout.BeginHorizontal();
        bool show = NGUIEditorTools.DrawPrefixButton(buttonName);
        T o;
        if (obj is SceneAsset) {
            SceneAsset sa = obj as SceneAsset;
            o = EditorGUILayout.ObjectField(StringUtil.GetFileNameWithoutExt(sa.assetPath), obj, typeof(T), false, options) as T;
        }
        else {
            o = EditorGUILayout.ObjectField(obj, typeof(T), false, options) as T;
        }

        GUILayout.EndHorizontal();
        if (show) Show<T>(cb, index);
        else if (o != obj) cb(o, index);
    }

    /// <summary>
    /// Draw a button + object selection combo filtering specified types.
    /// </summary>

    static public void Draw<T> (T obj, int? index, OnSelectionCallback cb, params GUILayoutOption[] options) where T : Object
    {
        Draw<T>(NGUITools.GetTypeName<T>(), obj, index, cb, options);
    }

    /// <summary>
    /// Show the selection wizard.
    /// </summary>

    static public void Show<T> (OnSelectionCallback cb, int? index) where T : Object
    {
        System.Type type = typeof(T);
        XfgjComponentSelector comp = ScriptableWizard.DisplayWizard<XfgjComponentSelector>("Select a " + GetName(type));
        comp.mType = type;
        comp.mCallback = cb;
        comp.index = index;

        if (type == typeof(UIAtlas) || type == typeof(UIFont))
        {
            BetterList<T> list = new BetterList<T>();
            string[] paths = AssetDatabase.GetAllAssetPaths();

            for (int i = 0; i < paths.Length; ++i)
            {
                string path = paths[i];
                if (path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase))
                {
                    GameObject obj = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;

                    if (obj != null && PrefabUtility.GetPrefabType(obj) == PrefabType.Prefab)
                    {
                        T t = obj.GetComponent(typeof(T)) as T;
                        if (t != null) list.Add(t);
                    }
                }
            }
            comp.mObjects = list.ToArray();
        }
        else if (type == typeof(SceneAsset)) {
            BetterList<Object> list = new BetterList<Object>();
            string[] paths = AssetDatabase.GetAllAssetPaths();

            for (int i = 0; i < paths.Length; ++i)
            {
                string path = paths[i];
                if (path.EndsWith(XfgjEditor.SCENE_EXT, System.StringComparison.OrdinalIgnoreCase)
                    && path.Contains(XfgjEditor.SCENE_FOLDER))
                {
                    SceneAsset sceneAsset = SceneAsset.CreateInstance<SceneAsset>();
                    sceneAsset.assetPath = path;
                    list.Add(sceneAsset);
                }
            }
            comp.mObjects = list.ToArray();
        }
        else comp.mObjects = Resources.FindObjectsOfTypeAll(typeof(T));
    }

    /// <summary>
    /// Draw the custom wizard.
    /// </summary>

    void OnGUI ()
    {
        NGUIEditorTools.SetLabelWidth(80f);
        GUILayout.Label("Select a " + GetName(mType), "LODLevelNotifyText");
        NGUIEditorTools.DrawSeparator();

        if (mObjects.Length == 0)
        {
            EditorGUILayout.HelpBox("No " + GetName(mType) + " components found.\nTry creating a new one.", MessageType.Info);

            bool isDone = false;

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (mType == typeof(UIFont))
            {
                if (GUILayout.Button("Open the Font Maker", GUILayout.Width(150f)))
                {
                    EditorWindow.GetWindow<UIFontMaker>(false, "Font Maker", true);
                    isDone = true;
                }
            }
            else if (mType == typeof(UIAtlas))
            {
                if (GUILayout.Button("Open the Atlas Maker", GUILayout.Width(150f)))
                {
                    EditorWindow.GetWindow<UIAtlasMaker>(false, "Atlas Maker", true);
                    isDone = true;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if (isDone) Close();
        }
        else
        {
            Object sel = null;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinHeight(300));
            foreach (Object o in mObjects)
            {
                if (o is SceneAsset) {
                    if (DrawSceneAsset(o)) {
                        if (o == null) {
                            Debug.Log("scene asset o is null");
                        }
                        sel = o;
                    }
                }
                else {
                    if (DrawObject(o)) {
                        sel = o;
                    }
                }
            }
            GUILayout.EndScrollView();

            if (sel != null)
            {
                mCallback(sel, index);
                Close();
            }
        }
    }

    /// <summary>
    /// Draw details about the specified object in column format.
    /// </summary>

    bool DrawObject (Object ob)
    {
        bool retVal = false;
        Component comp = ob as Component;

        GUILayout.BeginHorizontal();
        {
            if (comp != null && EditorUtility.IsPersistent(comp.gameObject))
                GUI.contentColor = new Color(0.6f, 0.8f, 1f);
            
            GUILayout.Label(NGUITools.GetTypeName(ob), "AS TextArea", GUILayout.Width(80f), GUILayout.Height(20f));

            if (comp != null)
            {
                GUILayout.Label(NGUITools.GetHierarchy(comp.gameObject), "AS TextArea", GUILayout.Height(20f));
            }
            else if (ob is Font)
            {
                Font fnt = ob as Font;
                GUILayout.Label(fnt.name, "AS TextArea", GUILayout.Height(20f));
            }
            GUI.contentColor = Color.white;

            retVal = GUILayout.Button("Select", "ButtonLeft", GUILayout.Width(60f), GUILayout.Height(16f));
        }
        GUILayout.EndHorizontal();
        return retVal;
    }

    bool DrawSceneAsset (Object ob) {
        bool retVal = false;
        SceneAsset sceneAsset = ob as SceneAsset;

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("SceneAsset", "AS TextArea", GUILayout.Width(80f), GUILayout.Height(20f));
            GUILayout.Label(StringUtil.GetFileName(sceneAsset.assetPath), "AS TextArea", GUILayout.Height(20f));
            GUI.contentColor = Color.white;
            retVal = GUILayout.Button("Select", "ButtonLeft", GUILayout.Width(60f), GUILayout.Height(16f));
        }
        GUILayout.EndHorizontal();
        return retVal;
    }

}
