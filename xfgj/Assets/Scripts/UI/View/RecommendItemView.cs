using UnityEngine;
using System;

public class RecommendItemView {
    public static readonly string GO_PREFIX = "r_";

    private static GameObject prefab;

    static RecommendItemView() {
        prefab = Resources.Load("Prefabs/recommend_item") as GameObject;
    }

    public static RecommendItemView Create (GameObject parent) {
        if (parent == null) {
            return null;
        }
        GameObject go = NGUITools.AddChild(parent, prefab);
        RecommendItemView view = new  RecommendItemView();
        view.go = go;
        view.sprite = go.GetComponent<UISprite>();
        return view;
    }

    private RecommendItemView () {
    }

    private GameObject go;
    public GameObject gameObject {
        get {
            return go;
        }
    }

    private int recommendId;
    public int RecommendId {
        get {
            return recommendId;
        }

        set {
            recommendId = value;
            go.name = GO_PREFIX + value;
        }
    }

    private UISprite sprite;

    private string pictureId;
    public String PictureId {
        get {
            return pictureId;
        }

        set {
            pictureId = value;
            ShowPicture();
        }
    }

    public int x;
    public int y;
    public int width;
    public int height;

    public void release () {
        UnityEngine.Object.Destroy(go);
        go = null;
        sprite = null;
    }

    public void MatchPictureSize () {
        sprite.width = width;
        sprite.height = height;
    }

    public bool IsPictureShowed () {
        return sprite.atlas != null && sprite.spriteName == pictureId;
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
        Debug.Log("RecommendItemView LoadCallback");
        if (objs != null && objs.Length != 0) {
            GameObject atlas = objs[0] as GameObject;
            if (atlas == null) {
                Debug.Log("atlas is null");
                return;
            }
            sprite.atlas = atlas.GetComponent<UIAtlas>();
            sprite.spriteName = pictureId;
        }
    }

}

