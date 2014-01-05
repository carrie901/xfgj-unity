using UnityEngine;
using System;

public class SceneViewController : MonoBehaviour {

    public GameObject root;
    public GameObject loadPanel;
    public GameObject productPanel;
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

    #region delegate
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
            GameObject roamCamera = GameObject.FindGameObjectWithTag(Config.TAG_ROAM_CAMERA);
            if (roamCamera != null) {
                CameraRoamController cc = roamCamera.GetComponent<CameraRoamController>();
                cc.Roam();
            }
            GameObject gesture = GameObject.FindGameObjectWithTag(Config.TAG_GESTURE);
            if (gesture != null) {
                TapGestureHandle comp = gesture.GetComponent<TapGestureHandle>();
                if (comp != null) {
                    comp.SelectCallback = SelectObject;
                    Debug.Log("set SelectCallback");
                }
            }
        }
        else if (progress == -1.0f) {
            //TODO
        }
    }

    private void TurnLeft (GameObject go) {
    }

    private void TurnRight (GameObject go) {
    }

    private void SelectObject (GameObject go) {
        Debug.Log("SceneViewController SelectObject " + go.name);
        productPanel.SetActive(true);
    }
    #endregion

}

