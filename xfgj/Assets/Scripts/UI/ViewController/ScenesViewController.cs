using UnityEngine;
using System;
using System.Collections.Generic;


public class ScenesViewController : MonoBehaviour {

    public delegate List<Scene> DataSource (int index, int count);

    private const int ITEM_COUNT = 8;

    public GameObject root;
    public GameObject scrollPanel;
    public GameObject table;
    public GameObject loadingPanel;

    public DataSource dataSource;

    private UIScrollView scrollComp;
    private UIPanel scrollPanelComp;
    private UITable tableComp;
    private UIPanel loadingComp;

    private List<SceneItemView> itemViewList;
    private List<int> sceneIdList;

    #region MonoBehaviour
    void Awake () {
        scrollComp = scrollPanel.GetComponent<UIScrollView>();
        scrollComp.onDragFinished = DragFinished;
        scrollPanelComp = scrollPanel.GetComponent<UIPanel>();
        tableComp = table.GetComponent<UITable>();
        tableComp.onReposition = OnReposition;
        loadingComp = loadingPanel.GetComponent<UIPanel>();
        itemViewList = new List<SceneItemView>();
        sceneIdList = new List<int>();
    }

    void Start () {
        SyncScenesCommand scenesCmd = new SyncScenesCommand();
        scenesCmd.Callback = AfterSyncScenes;
        scenesCmd.execute();
    }

    void OnEnable () {
        ToggleLoading(true);
        ClearView();
        GenerateView();
        ToggleLoading(false);
    }

    void OnDisable () {
        sceneIdList.Clear();
        ClearView();
    }
    #endregion

    #region private methods
    private void ToggleLoading (bool show) {
        if (loadingComp != null) {
            if (show) {
                loadingComp.depth = NGUITools.CalculateNextDepth(gameObject);
            }
            else {
                loadingComp.depth = -1;
            }
        }
    }

    private void GenerateView () {
        Debug.Log("GenerateView");
        sceneIdList.Clear();
        List<Scene> scenes = dataSource(itemViewList.Count, ITEM_COUNT);
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
        if (Math.Abs(scrollPanel.transform.localPosition.y + scrollPanelComp.clipRange.w / 2
                     + table.transform.GetChild(table.transform.childCount - 1).localPosition.y) < 5) {
            Debug.Log("scroll to the bottom");
            ToggleLoading(true);
            GenerateView();
            ToggleLoading(false);
        }
    }

    private void ItemClick (GameObject go) {
        Debug.Log("item click now");
        RootViewController.SwitchParam param = new RootViewController.SwitchParam();
        param.view = RootViewController.VIEW_SCENE;
        param.obj = Int32.Parse(go.name.Substring(SceneItemView.GO_PREFIX.Length));
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

    private void AfterSyncScenes (object obj) {
        Debug.Log("AfterSyncScenes");
        if (obj != null && (bool)obj) {
            sceneIdList.Clear();
            ClearView();
            GenerateView();
        }
        ToggleLoading(false);
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
