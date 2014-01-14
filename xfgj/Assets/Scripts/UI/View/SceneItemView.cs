using UnityEngine;
using System;

public class SceneItemView {

    public static readonly string GO_PREFIX = "s_";

    private static GameObject prefab;

    static SceneItemView() {
        prefab = Resources.Load("Prefabs/scene_item") as GameObject;
    }

    public static SceneItemView Create (GameObject parent, Scene scene) {
        if (parent == null || scene == null) {
            return null;
        }
        GameObject go = NGUITools.AddChild(parent, prefab);
        SceneItemView view = new  SceneItemView();
        view.scene = scene;
        view.go = go;
        view.name = go.transform.Find("name").gameObject.GetComponent<UILabel>();
        view.thumbnail = go.transform.Find("thumbnail").gameObject.GetComponent<UISprite>();
        GameObject grayHeart = go.transform.Find("favourite/Gray").gameObject;
        GameObject redHeart = go.transform.Find("favourite/Red").gameObject;
        view.grayHeart = grayHeart.GetComponent<UISprite>();
        view.redHeart = redHeart.GetComponent<UISprite>();
        UIEventListener.Get(go.transform.Find("favourite").gameObject).onClick = view.ClickFavourite;
        view.go.name = GO_PREFIX + scene.sceneId;
        view.name.text = scene.name;
        view.ShowPicture();
        view.ShowFavouriteState();
        return view;
    }

    private GameObject go;
    private UILabel name;
    private UISprite thumbnail;
    private UISprite grayHeart;
    private UISprite redHeart;

    private Scene scene;

    private SceneItemView () {}

    public GameObject gameObject {
        get {
            return go;
        }
    }

    public Scene SceneObject {
        get {
            return scene;
        }
    }

    #region public
    public void release () {
        UnityEngine.Object.Destroy(go);
        go = null;
        name = null;
        thumbnail = null;
        scene = null;
    }

    public bool IsPictureShowed () {
        return thumbnail.atlas != null && thumbnail.spriteName == scene.pictureId;
    }

    public void ShowPicture () {
        if (IsPictureShowed()) {
            return;
        }
        Debug.Log("ShowPicture call");
        Picture picture = LogicController.GetPicture(scene.pictureId);
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
    #endregion

    #region private
    private void LoadCallback (UnityEngine.Object[] objs) {
        Debug.Log("SceneItemView LoadCallback");
        if (objs != null && objs.Length != 0) {
            GameObject atlas = objs[0] as GameObject;
            if (atlas == null) {
                Debug.Log("atlas is null");
                return;
            }
            thumbnail.atlas = atlas.GetComponent<UIAtlas>();
            thumbnail.spriteName = scene.pictureId;
        }
    }

    private void ClickFavourite (GameObject go) {
        Debug.Log("ClickFavourite");
        scene.favourite = !scene.favourite;
        LogicController.UpdateScene(scene);
        ShowFavouriteState();
    }

    private void ShowFavouriteState() {
        int maxDepth = Math.Max(grayHeart.depth, redHeart.depth);
        int minDepth = Math.Min(grayHeart.depth, redHeart.depth);
        if (scene.favourite) {
            redHeart.depth = maxDepth;
            grayHeart.depth = minDepth;
        }
        else {
            redHeart.depth = minDepth;
            grayHeart.depth = maxDepth;
        }
    }
    #endregion
}

