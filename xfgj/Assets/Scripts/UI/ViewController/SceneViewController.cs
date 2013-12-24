using UnityEngine;
using System;

public class SceneViewController : MonoBehaviour {

    public GameObject root;
    public GameObject loadPanel;
    public int sceneId;

    public GameObject leftBtn;
    public GameObject rightBtn;

    private Scene scene;

    private AsyncOperation oper;

    #region MonoBehaviour
    void Awake () {
        if (sceneId == 0) {
            Debug.Log("SceneViewController sceneId not initlized");
            return;
        }
        scene = LogicController.GetScene(sceneId);
        if (scene != null) {
            /*Asset asset = LogicController.GetAsset(scene.assetId);
            if (asset != null) {
                LoadScene(asset);
            }
            else {
                GetAssetCommand cmd = new GetAssetCommand();
                cmd.AssetId = scene.assetId;
                cmd.Callback = GetAsset;
                cmd.execute();
            }*/
            oper = Application.LoadLevelAdditiveAsync("" + scene.sceneId);
        }
        UIEventListener.Get(leftBtn).onClick = TurnLeft;
        UIEventListener.Get(rightBtn).onClick = TurnRight;
    }

    void Start () {
    }

    void Update () {
        if (oper != null) {
            NotifyProgress(oper.progress);
            if (oper.isDone) {
                oper = null;
            }
        }
    }

    #endregion

    #region private
    private void GetAsset (object obj) {
        if (obj != null) {
            Asset asset = obj as Asset;
            LoadScene(asset);
        }
    }

    private void LoadScene (Asset asset) {
        AssetBundleController.LoadParam param = new AssetBundleController.LoadParam();
        param.path = AppSetting.getInstance().assetUrl + asset.name;
        param.name = new string[] {"" + scene.sceneId};
        param.version = asset.version;
        param.notify = NotifyProgress;
        AssetBundleController.LoadScene(param);
        loadPanel.SetActive(true);
    }

    private void NotifyProgress (float progress) {
        Debug.Log("Loading progress is " + progress);
        if (progress == 1.0f) {
            loadPanel.SetActive(false);
        }
    }

    private void TurnLeft (GameObject go) {
        CameraController cc = Camera.main.gameObject.GetComponent<CameraController>();
        cc.MoveToNext();
    }

    private void TurnRight (GameObject go) {
        CameraController cc = Camera.main.gameObject.GetComponent<CameraController>();
        cc.MoveToPrevious();
    }
    #endregion

}

