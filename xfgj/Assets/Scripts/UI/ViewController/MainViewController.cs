using UnityEngine;
using System;
using System.Collections.Generic;

public class MainViewController : MonoBehaviour {

    private const string SCENE_ITEM_PREFIX = "s_";

    public GameObject root;
    public GameObject scrollPanel;
    public GameObject table;
    public GameObject loadingPanel;
    private UIScrollView scrollComp;
    private UITable tableComp;
    private UIPanel loadingComp;

    private List<SceneItemView> itemViewList;
    private List<int> sceneIdList;

    #region MonoBehaviour
    void Awake () {
        scrollComp = scrollPanel.GetComponent<UIScrollView>();
        scrollComp.onDragFinished = DragFinished;
        tableComp = table.GetComponent<UITable>();
        tableComp.onReposition = OnReposition;
        loadingComp = loadingPanel.GetComponent<UIPanel>();
        itemViewList = new List<SceneItemView>();
        sceneIdList = new List<int>();
    }

    void Start () {
        Show();
    }

    #endregion

    #region public methods
    /*
     * item size 350*350
     * pos (-46, 191), (318, 191), (-46, -172), (318, -172)
     */
    public void Show () {
        ToggleLoading();
        GenerateView();
        AuthorizeCommand authCmd = new AuthorizeCommand();
        authCmd.Callback = AfterAuthorize;
        authCmd.execute();
    }
    #endregion

    #region private methods
    private void ToggleLoading () {
        if (loadingComp != null) {
            if (loadingComp.depth == -1) {
                loadingComp.depth = NGUITools.CalculateNextDepth(gameObject);
            }
            else {
                loadingComp.depth = -1;
            }
        }
    }

    private void GenerateView () {
        Debug.Log("GenerateView");
        ClearView();
        sceneIdList.Clear();
        List<Scene> scenes = LogicController.GetScenesBySceneType(100001);
        for (int i = 0; i < scenes.Count; ++i) {
            SceneItemView itemView = SceneItemView.Create(table);
            itemView.SceneId = scenes[i].sceneId;
            itemView.Name = scenes[i].name;
            itemView.PictureId = scenes[i].pictureId;
            UIEventListener.Get(itemView.gameObject).onClick = ItemClick;
            itemViewList.Add(itemView);

            Picture picture = LogicController.GetPicture(scenes[i].pictureId);
            if (picture == null) {
                sceneIdList.Add(scenes[i].sceneId);
            }
            else {
                Asset asset = LogicController.GetAsset(picture.assetId);
                if (asset == null) {
                    sceneIdList.Add(scenes[i].sceneId);
                }
            }
        }
        tableComp.Reposition();
        if (sceneIdList.Count != 0) {
            GetSceneSnapshotCommand cmd = new GetSceneSnapshotCommand();
            cmd.SceneIds = sceneIdList;
            cmd.Callback = AfterGetSnapshot;
            cmd.execute();
        }
    }

    private void ClearView () {
        table.transform.DetachChildren();
        if (itemViewList != null && itemViewList.Count != 0) {
            foreach (SceneItemView itemView in itemViewList) {
                if (itemView != null) {
                    itemView.release();
                }
            }
            itemViewList.Clear();
        }
    }
    #endregion

    #region View delegate
    private void DragFinished () {
        Debug.Log("DragFinished function called");
        if (Math.Abs(scrollPanel.transform.localPosition.y - scrollComp.bounds.extents.y) < 5) {
            Debug.Log("scroll to the bottom");
            ToggleLoading();
        }
    }

    private void ItemClick (GameObject go) {
        Debug.Log("item click now");
        RootViewController.SwitchParam param = new RootViewController.SwitchParam();
        param.view = RootViewController.VIEW_SCENE;
        param.obj = Int32.Parse(go.name.Substring(SCENE_ITEM_PREFIX.Length));
        root.SendMessage("SwitchView", param, SendMessageOptions.RequireReceiver);
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

    private void AfterAuthorize (object obj) {
        Debug.Log("AfterAuthorize");
        if (obj != null && !(bool)obj) {
            ToggleLoading();
            return;
        }
        SyncScenesCommand scenesCmd = new SyncScenesCommand();
        scenesCmd.Callback = AfterSyncScenes;
        scenesCmd.execute();
    }

    private void AfterSyncScenes (object obj) {
        Debug.Log("AfterSyncScenes");
        ToggleLoading();
        if (obj != null && (bool)obj) {
            GenerateView();
            return;
        }
    }

    private void AfterGetSnapshot (object obj) {
        Debug.Log("AfterGetSnapshot");
        if (obj != null && !(bool)obj) {
            return;
        }
        foreach (SceneItemView itemView in itemViewList) {
            itemView.ShowPicture();
        }
        sceneIdList.Clear();
    }

    #endregion
}

