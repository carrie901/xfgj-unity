using UnityEngine;
using System;

public class SceneTypeItemView {

    public static readonly string GO_PREFIX = "st_";

    private static GameObject prefab;

    static SceneTypeItemView() {
        prefab = Resources.Load("Prefabs/scene_type_item") as GameObject;
    }

    public static SceneTypeItemView Create (GameObject parent, SceneType sceneType) {
        if (parent == null || sceneType == null) {
            return null;
        }
        GameObject go = NGUITools.AddChild(parent, prefab);
        SceneTypeItemView view = new  SceneTypeItemView();
        view.sceneType = sceneType;
        view.go = go;
        view.name = go.transform.Find("name").gameObject.GetComponent<UILabel>();
        view.pic = go.transform.Find("pic").gameObject.GetComponent<UISprite>();
        view.go.name = GO_PREFIX + sceneType.typeId;
        view.name.text = sceneType.name;
        view.ShowPicture();
        return view;
    }

    private GameObject go;
    private UILabel name;
    private UISprite pic;

    private SceneType sceneType;

    private SceneTypeItemView () {}

    public int SceneTypeId {
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

    public GameObject gameObject {
        get {
            return go;
        }
    }

    public SceneType SceneTypeObject {
        get {
            return sceneType;
        }
    }

    public void release () {
        UnityEngine.Object.Destroy(go);
        go = null;
        name = null;
        pic = null;
        sceneType = null;
    }

    public bool IsPictureShowed () {
        return pic.atlas != null && pic.spriteName == sceneType.pictureId;
    }

    public void ShowPicture () {
        /*if (IsPictureShowed()) {
            return;
        }
        Debug.Log("ShowPicture call");
        Picture picture = LogicController.GetPicture(sceneType.pictureId);
        if (picture == null) { return; }
        Asset asset = LogicController.GetAsset(picture.assetId);
        if (asset == null) { return; }
        AssetBundleManager.GetObject(AppSetting.getInstance().assetUrl + asset.name, asset.version,
                                     new string[]{picture.atlasName}, LoadCallback);
                                     */
        GameObject thumbnail = GameObject.Find("ThumbnailAtlasRef");
        UIAtlas atlas = thumbnail.GetComponent<UIAtlas>();
        pic.atlas = atlas;
        if (name.text.Equals(Localization.Localize("chufang"))) {
            pic.spriteName = "chufang";
        }
        else if (name.text.Equals(Localization.Localize("weishengjian"))) {
            pic.spriteName = "weishengjian";
        }
        else if (name.text.Equals(Localization.Localize("keting"))) {
            pic.spriteName = "keting";
        }
        else if (name.text.Equals(Localization.Localize("ertongfang"))) {
            pic.spriteName = "ertongfang";
        }
        else if (name.text.Equals(Localization.Localize("shufang"))) {
            pic.spriteName = "shufang";
        }
        else if (name.text.Equals(Localization.Localize("woshi"))) {
            pic.spriteName = "woshi";
        }
    }

    private void LoadCallback (UnityEngine.Object[] objs) {
        Debug.Log("SceneItemView LoadCallback");
        if (objs != null && objs.Length != 0) {
            GameObject atlas = objs[0] as GameObject;
            if (atlas == null) {
                Debug.Log("atlas is null");
                return;
            }
            Debug.Log("Pic " + pic);
            Debug.Log("Atlas " + atlas);
            //maybe will call release before enter in this func
            if (pic != null) {
                pic.atlas = atlas.GetComponent<UIAtlas>();
                pic.spriteName = sceneType.pictureId;
            }
        }
    }
}

