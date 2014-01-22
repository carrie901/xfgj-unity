using UnityEngine;
using System;

public class SceneViewController : MonoBehaviour {

    private static readonly string PREFIX_PRODUCT = "p_";

    public GameObject productView;
    public GameObject backButton;
    public GameObject title;
    public GameObject contextButtons;
    public GameObject cover;

    public int sceneId;

    private ProductViewController pvc;
    private ContextButtonController cbc;
    private Scene scene;
    private AsyncOperation oper;
    private long ticks;

    #region MonoBehaviour
    void Awake () {
        pvc = productView.GetComponent<ProductViewController>();
        cbc = contextButtons.GetComponent<ContextButtonController>();
        UIEventListener.Get(backButton).onClick = BackToHome;
        pvc.hideDelegate = CloseProductView;
        cbc.productDelegate = ProductButtonClick;
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
            title.GetComponent<UILabel>().text = scene.name;
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
        scene = null;
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
            cc.roamStart = OnRoamStart;
            cc.roamComplete = OnRoamComplete;
            cc.Roam();
        }
        GameObject gesture = GameObject.FindGameObjectWithTag(Config.TAG_GESTURE);
        if (gesture != null) {
            TapGestureHandle comp = gesture.GetComponent<TapGestureHandle>();
            if (comp != null) {
                comp.SelectCallback = SelectObject;
                comp.TapInterceptDelegate = IsTapIntercepted;
            }
        }
    }

    private void BackToHome (GameObject obj) {
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

    private void SelectObject (GameObject go) {
        Debug.Log("SceneViewController SelectObject " + go.name);
        if (go.name.StartsWith(PREFIX_PRODUCT)) {
            cbc.ShowContextButton(ContextButtonController.FLAG_PRODUCT);
            pvc.productId = int.Parse(go.name.Substring(PREFIX_PRODUCT.Length));
        }
    }

    private void ProductButtonClick () {
        Debug.Log("ProductButtonClick");
        ticks = DateTime.Now.Ticks;
        productView.SetActive(true);
        SwitchFingerGesture(false);
    }

    private void CloseProductView () {
        SwitchFingerGesture(true);
    }

    private void SwitchFingerGesture (bool sw) {
        GameObject gesture = GameObject.FindGameObjectWithTag(Config.TAG_GESTURE);
        if (gesture != null) {
            TapRecognizer tr = gesture.GetComponent<TapRecognizer>();
            if (tr != null) {
                tr.UseSendMessage = sw;
            }
        }
        GameObject mainCamera = GameObject.FindGameObjectWithTag(Config.TAG_MAIN_CAMERA);
        if (mainCamera != null) {
            CustomTBOrbit ct = mainCamera.GetComponent<CustomTBOrbit>();
            if (ct != null) {
                ct.enabled = sw;
            }
            DragRecognizer dr = mainCamera.GetComponent<DragRecognizer>();
            if (dr != null) {
                dr.UseSendMessage = sw;
            }
            PinchRecognizer pr = mainCamera.GetComponent<PinchRecognizer>();
            if (pr != null) {
                pr.UseSendMessage = sw;
            }
        }
    }

    private bool IsTapIntercepted () {
        // 1 ms == 10000 ticks
        if (ticks == 0) {
            return false;
        }
        return (DateTime.Now.Ticks - ticks) / 10000 < 1000;
    }

    private void OnRoamStart () {
        cover.SetActive(true);
    }

    private void OnRoamComplete () {
        cover.SetActive(false);
    }
    #endregion

}

