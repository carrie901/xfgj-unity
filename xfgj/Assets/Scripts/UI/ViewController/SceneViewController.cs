using UnityEngine;
using System;

public class SceneViewController : MonoBehaviour {

    public GameObject root;
    public GameObject productPanel;
    public int sceneId;

    public GameObject leftBtn;
    public GameObject rightBtn;

    private Scene scene;

    private AsyncOperation oper;

    #region MonoBehaviour
    void Awake () {
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

    void OnEnable () {
        LoadViewController.ShowLoadProgress();
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
    }

    void OnDisable () {
        sceneId = 0;
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
        SceneManager.LoadLevelAdditive(AppSetting.getInstance().assetUrl + asset.name,
                                       asset.version, "" + scene.sceneId, NotifyProgress);
    }

    private void NotifyProgress (float progress) {
        Debug.Log("Loading progress is " + progress);
        if (progress == 1.0f) {
            LoadViewController.HideLoadProgress(OnFinishLoad);
        }
        else if (progress == -1.0f) {
            NotificationViewController.ShowNotification(Localization.Localize(StringKey.Msg_LoadSceneFail));
        }
        else {
            LoadViewController.NotifyProgress(progress);
        }
    }

    private void OnFinishLoad () {
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

    private void BackToHome () {
        GameObject[] arrays = GameObject.FindGameObjectsWithTag(Config.TAG_SCENE);
        foreach (GameObject go in arrays) {
            Destroy(go);
        }
        arrays = GameObject.FindGameObjectsWithTag(Config.TAG_ROAM_CAMERA);
        foreach (GameObject go in arrays) {
            Destroy(go);
        }
        arrays = GameObject.FindGameObjectsWithTag(Config.TAG_GESTURE);
        foreach (GameObject go in arrays) {
            Destroy(go);
        }
        RootViewController.ShowMainView();
    }

    private void TurnLeft (GameObject go) {
        BackToHome();
    }

    private void TurnRight (GameObject go) {
    }

    private void SelectObject (GameObject go) {
        Debug.Log("SceneViewController SelectObject " + go.name);
        productPanel.SetActive(true);
    }
    #endregion

}

