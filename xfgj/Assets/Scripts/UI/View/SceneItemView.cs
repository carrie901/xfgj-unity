using UnityEngine;
using System;

public class SceneItemView {

    private static readonly string GO_PREFIX = "s_";

    private static GameObject prefab;

    static SceneItemView() {
        prefab = Resources.Load("Prefabs/scene_item") as GameObject;
    }

    public static SceneItemView Add (GameObject parent) {
        if (parent == null) {
            return null;
        }
        GameObject go = NGUITools.AddChild(parent, prefab);
        SceneItemView view = new  SceneItemView();
        view.go = go;
        view.name = go.transform.Find("name").gameObject.GetComponent<UILabel>();
        view.thumbnail = go.transform.Find("thumbnail").gameObject.GetComponent<UISprite>();
        return view;
    }

    private GameObject go;
    private UILabel name;
    private UISprite thumbnail;

    private SceneItemView () {
    }

    public int SceneId {
        get {
            if (!go.name.StartsWith(GO_PREFIX)) {
                return -1;
            }
            return Int32.Parse(go.name.Substring(GO_PREFIX.Length));
        }

        set {
            go.name = GO_PREFIX + value;
        }
    }

    public string Name {
        get {
            return name.name;
        }

        set {
            name.name = value;
        }
    }

}

