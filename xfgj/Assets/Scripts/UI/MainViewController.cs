using UnityEngine;
using System;
using System.Collections.Generic;

public class MainViewController {

    private GameObject root;
    private GameObject scrollPanel;
    private UIScrollView scrollComp;
    private GameObject table;
    private UITable tableComp;
    private UIPanel loadingComp;

    public MainViewController () {
        root = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel");
        scrollPanel = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/ScrollPanel");
        scrollComp = scrollPanel.GetComponent<UIScrollView>();
        table = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/ScrollPanel/Table");
        tableComp = table.GetComponent<UITable>();
        scrollComp.onDragFinished = DragFinished;
        tableComp.onReposition = OnReposition;
        GameObject loadingPanel = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/Loading");
        if (loadingPanel != null) {
            loadingComp = loadingPanel.GetComponent<UIPanel>();
        }
    }

    #region public methods
    /*
     * item size 350*350
     * pos (-46, 191), (318, 191), (-46, -172), (318, -172)
     */
    public void Show () {
        Debug.Log("generate view");
        ToggleLoading();
        GenerateView();
        SyncScenesCommand cmd = new SyncScenesCommand(new UIDelegate.Update(RefreshList));
        cmd.execute();
    }
    #endregion

    #region private methods
    private void ToggleLoading () {
        if (loadingComp != null) {
            if (loadingComp.depth == -1) {
                loadingComp.depth = NGUITools.CalculateNextDepth(root);
            }
            else {
                loadingComp.depth = -1;
            }
        }
    }

    private void GenerateView () {
        table.transform.DetachChildren();
        List<Scene> scenes = LogicController.GetScenesBySceneType(100001);
        GameObject sceneItem = Resources.Load("Prefabs/scene_item") as GameObject;
        for (int i = 0; i < scenes.Count; ++i) {
            NGUITools.AddChild(table, sceneItem);
        }
        tableComp.Reposition();
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

    private void RefreshList (bool result) {
        if (result) {
            GenerateView();
        }
        ToggleLoading();
    }

    #endregion

    #region api callback

    #endregion
}

