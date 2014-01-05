using UnityEngine;
using System;

public class SceneItemView {

    public static readonly string GO_PREFIX = "s_";

    private static GameObject prefab;

    static SceneItemView() {
        prefab = Resources.Load("Prefabs/scene_item") as GameObject;
    }

    public static SceneItemView Create (GameObject parent) {
        if (parent == null) {
            return null;
        }
        GameObject go = NGUITools.AddChild(parent, prefab);
        SceneItemView view = new  SceneItemView();
        view.go = go;
        view.name = go.transform.Find("name").gameObject.GetComponent<UILabel>();
        view.thumbnail = go.transform.Find("thumbnail").gameObject.GetComponent<UISprite>();
        view.Favourite = go.transform.Find("favourite").gameObject;
        return view;
    }

    private GameObject go;
    private UILabel name;
    private UISprite thumbnail;

    private int sceneId;
    private string pictureId;

    private SceneItemView () {
    }

    public GameObject gameObject {
        get {
            return go;
        }
    }

    public int SceneId {
        get {
            return sceneId;
        }

        set {
            sceneId = value;
            go.name = GO_PREFIX + value;
        }
    }

    public string Name {
        get {
            return name.text;
        }

        set {
            name.text = value;
        }
    }

    public string PictureId {
        get {
            return pictureId;
        }

        set {
            pictureId = value;
            ShowPicture();
        }
    }

    private GameObject Favourite {
        set {
            UIEventListener.Get(value).onClick = ClickFavourite;
        }
    }

    public void release () {
        UnityEngine.Object.Destroy(go);
        go = null;
        name = null;
        thumbnail = null;
    }

    public bool IsPictureShowed () {
        return thumbnail.atlas != null && thumbnail.spriteName == pictureId;
    }

    public void ShowPicture () {
        if (IsPictureShowed()) {
            return;
        }
        Debug.Log("ShowPicture call");
        Picture picture = LogicController.GetPicture(pictureId);
        if (picture == null) { return; }
        Asset asset = LogicController.GetAsset(picture.assetId);
        if (asset == null) { return; }
        AssetBundleController.LoadParam param = new AssetBundleController.LoadParam();
        param.path = Config.ASSET_URL + asset.name;
        param.version = asset.version;
        param.name = new string[]{picture.atlasName};
        param.callback = LoadCallback;
        AssetBundleController.LoadObject(param);
    }

    private void LoadCallback (UnityEngine.Object[] objs) {
        Debug.Log("SceneItemView LoadCallback");
        if (objs != null && objs.Length != 0) {
            GameObject atlas = objs[0] as GameObject;
            if (atlas == null) {
                Debug.Log("atlas is null");
                return;
            }
            thumbnail.atlas = atlas.GetComponent<UIAtlas>();
            thumbnail.spriteName = pictureId;
        }
    }

    private void ClickFavourite (GameObject go) {
        Debug.Log("ClickFavourite");
        Scene scene = LogicController.GetScene(sceneId);
        scene.favourite = !scene.favourite;
        LogicController.UpdateScene(scene);
    }

}

