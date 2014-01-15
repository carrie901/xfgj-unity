using UnityEngine;
using System;
using System.Collections.Generic;

public class ShowRoomViewController : MonoBehaviour {

    public GameObject root;
    public GameObject table;

    private UITable tableComp;
    private ScenesViewController scenesViewController;

    private List<SceneTypeItemView> itemViewList;
    private List<string> picIdList;
    private List<int> assetIdList;

    private int selectedTypeId;

    #region MonoBehaviour
    void Awake () {
        tableComp = table.GetComponent<UITable>();
        tableComp.onReposition = OnReposition;
        scenesViewController = gameObject.GetComponent<ScenesViewController>();
        scenesViewController.dataSource = DataSource;
        itemViewList = new List<SceneTypeItemView>();
        picIdList = new List<string>();
        assetIdList = new List<int>();
    }

    void Start () {
        LoadViewController.ShowLoadIndicator();
        AuthorizeCommand authCmd = new AuthorizeCommand();
        authCmd.Callback = AfterAuthorize;
        authCmd.execute();
    }

    void OnEnable () {
        LoadViewController.ShowLoadIndicator();
        GenerateView();
        LoadViewController.HideLoadIndicator();
    }

    void OnDisable () {
        scenesViewController.enabled = false;
    }
    #endregion

    #region private methods
    private void GenerateView () {
        Debug.Log("GenerateView");
        ClearView();
        picIdList.Clear();
        assetIdList.Clear();
        List<SceneType> sceneTypes = LogicController.GetSceneTypes();
        for (int i = 0; i < sceneTypes.Count; ++i) {
            SceneTypeItemView itemView = SceneTypeItemView.Create(table, sceneTypes[i]);
            UIEventListener.Get(itemView.gameObject).onClick = ItemClick;
            itemViewList.Add(itemView);

            Picture picture = LogicController.GetPicture(sceneTypes[i].pictureId);
            if (picture == null) {
                picIdList.Add(sceneTypes[i].pictureId);
            }
            else {
                Asset asset = LogicController.GetAsset(picture.assetId);
                if (asset == null) {
                    assetIdList.Add(picture.assetId);
                }
            }
        }
        tableComp.Reposition();
        if (picIdList.Count != 0) {
            GetPictureCommand cmd = new GetPictureCommand();
            cmd.Callback = AfterGetPicture;
            cmd.PictureIds = picIdList;
            cmd.execute();
        }
        if (assetIdList.Count != 0) {
            GetAssetCommand cmd = new GetAssetCommand();
            cmd.Callback = AfterGetAsset;
            cmd.AssetIds = assetIdList;
            cmd.execute();
        }
        picIdList.Clear();
        assetIdList.Clear();
    }

    private void ClearView () {
        table.transform.DetachChildren();
        if (itemViewList != null && itemViewList.Count != 0) {
            foreach (SceneTypeItemView itemView in itemViewList) {
                if (itemView != null) {
                    itemView.release();
                }
            }
            itemViewList.Clear();
        }
    }
    #endregion

    #region View delegate
    private void ItemClick (GameObject go) {
        Debug.Log("item click now");
        selectedTypeId = Int32.Parse(go.name.Substring(SceneTypeItemView.GO_PREFIX.Length));
        if (scenesViewController != null) {
            ClearView();
            scenesViewController.enabled = true;
        }
    }

    private void OnReposition () {
        for (int i = 0; i < table.transform.childCount; ++i) {
            Transform t = table.transform.GetChild(i);
            int row = i / 2;
            if (i % 2 != 0) {
                t.localPosition = new Vector3(182, -row * 364, 0);
            }
            else {
                t.localPosition = new Vector3(-182, -row * 364, 0);
            }
        }
    }

    private List<Scene> DataSource (int index, int count) {
        return LogicController.GetScenesBySceneType(selectedTypeId, index, count);
    }

    private void AfterAuthorize (object obj) {
        Debug.Log("AfterAuthorize");
        if (obj != null && !(bool)obj) {
            LoadViewController.HideLoadIndicator();
            return;
        }
        SyncSceneTypeCommand cmd = new SyncSceneTypeCommand();
        cmd.Callback = AfterSyncSceneType;
        cmd.execute();
    }

    private void AfterSyncSceneType (object obj) {
        Debug.Log("AfterSyncSceneType");
        if (obj != null && !(bool)obj) {
            LoadViewController.HideLoadIndicator();
            return;
        }
        GenerateView();
        LoadViewController.HideLoadIndicator();
    }

    private void AfterGetPicture (object obj) {
        Debug.Log("AfterGetPicture");
        if (obj != null && !(bool)obj) {
            return;
        }
        foreach (SceneTypeItemView itemView in itemViewList) {
            if (itemView != null) {
                itemView.ShowPicture();
            }
        }
    }

    private void AfterGetAsset (object obj) {
        Debug.Log("AfterGetAsset");
        if (obj != null && !(bool)obj) {
            return;
        }
        foreach (SceneTypeItemView itemView in itemViewList) {
            if (itemView != null) {
                itemView.ShowPicture();
            }
        }
    }
    #endregion
}

