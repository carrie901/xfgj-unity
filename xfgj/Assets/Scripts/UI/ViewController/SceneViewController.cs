using UnityEngine;
using System.Collections;

public class SceneViewController : MonoBehaviour {

    private static readonly string PREFIX_PRODUCT = "p_";

    public GameObject productView;
    public GameObject backButton;
    public GameObject roamButton;
    public GameObject characterButton;
    public GameObject title;
    public GameObject contextButtons;
    public GameObject cover;

    public int sceneId;

    private ProductViewController pvc;
    private ContextButtonController cbc;
    private Scene scene;
    private AsyncOperation oper;
    private bool tapIntercept = false;

    #region MonoBehaviour
    void Awake () {
        pvc = productView.GetComponent<ProductViewController>();
        cbc = contextButtons.GetComponent<ContextButtonController>();
        UIEventListener.Get(backButton).onClick = BackToHome;
        UIEventListener.Get(roamButton).onClick = Roam;
        UIEventListener.Get(characterButton).onClick = SwitchToCharacter;
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
        LoadViewController.ShowLoadProgress(OnFinishShow);
    }

    void OnDisable () {
        sceneId = 0;
        scene = null;
    }

    #endregion

    #region private
    private void GetAssetCallback (object obj) {
        if (obj != null) {
            Asset asset = obj as Asset;
            LoadScene(asset);
        }
    }

    private void LoadScene (Asset asset) {
        SceneManager.LoadLevelAdditive(AppSetting.getInstance().assetUrl + asset.name,
                                       asset.version, "" + scene.sceneId, NotifyProgress);
    }

    private void TapSelectObject (GameObject go) {
        if (go == null) {
            cbc.HideContextButton();
            return;
        }
        Debug.Log("TapSelectObject " + go.name);
        if (go.animation != null) {
            Debug.Log("SelectModel anim play");
            go.animation.Play();
        }
        Light lightComp = go.GetComponentInChildren<Light>();
        if (lightComp != null) {
            lightComp.enabled = !lightComp.enabled;
        }
        //if (go.name.StartsWith(PREFIX_PRODUCT)) {
            cbc.ShowContextButton(ContextButtonController.FLAG_PRODUCT);
            //int productId = int.Parse(go.name.Substring(PREFIX_PRODUCT.Length));
            //Product product = LogicController.GetProduct(productId);
            Product product = LogicController.GetProduct(100001);
            if (product != null) {
                pvc.product = product;
            }
        //}
    }

    private void LongPressSelectObject (GameObject go) {
        Debug.Log("LongPressSelectObject");
        tapIntercept = true;
        if (go == null) {
            return;
        }
    }

    private void FingerUpCallback () {
        StartCoroutine(DelayCall(delegate() {tapIntercept = false;}, 1f));
    }

    private void SwitchFingerGesture (bool sw) {
        GameObject gesture = GameObject.FindGameObjectWithTag(Config.TAG_GESTURE);
        if (gesture != null) {
            TapRecognizer tr = gesture.GetComponent<TapRecognizer>();
            if (tr != null) {
                tr.UseSendMessage = sw;
            }
            LongPressRecognizer lpr = gesture.GetComponent<LongPressRecognizer>();
            if (lpr != null) {
                lpr.UseSendMessage = sw;
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

    private void SwitchFingerGesture (bool sw, float delay) {
        StartCoroutine(DelayCall(delegate() {SwitchFingerGesture(sw);}, delay));
    }

    private IEnumerator DelayCall (DelayCallDelegate callback, float delay) {
        yield return new WaitForSeconds(delay);
        if (callback != null) {
            callback();
        }
    }

    private bool IsTapIntercepted () {
        return tapIntercept;
    }
    #endregion

    #region Load
    private void OnFinishShow () {
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
                cmd.Callback = GetAssetCallback;
                cmd.execute();
            }*/
            //oper = Application.LoadLevelAdditiveAsync("" + scene.sceneId);
            oper = Application.LoadLevelAdditiveAsync("Apartment Scene");
        }
    }

    private void OnFinishLoad () {
        CameraSwitcher.SwitchToMain();
        GameObject gesture = GameObject.FindGameObjectWithTag(Config.TAG_GESTURE);
        if (gesture != null) {
            TapGestureHandle tapHandle = gesture.GetComponent<TapGestureHandle>();
            if (tapHandle != null) {
                tapHandle.SelectCallback = TapSelectObject;
                tapHandle.TapInterceptDelegate = IsTapIntercepted;
            }
            LongPressGestureHandle longPressHandle = gesture.GetComponent<LongPressGestureHandle>();
            if (longPressHandle != null) {
                longPressHandle.SelectCallback = LongPressSelectObject;
            }
            FingerUpHandle fingerUpHandle = gesture.GetComponent<FingerUpHandle>();
            if (fingerUpHandle != null) {
                fingerUpHandle.FingerUpCallback = FingerUpCallback;
            }
        }
        tapIntercept = false;
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
    #endregion

    #region ButtonClick
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
        arrays = GameObject.FindGameObjectsWithTag(Config.TAG_CHARACTER);
        foreach (GameObject go in arrays) {
            CharacterCache cc = go.GetComponent<CharacterCache>();
            if (cc != null && cc.character != null) {
                Destroy(cc.character);
            }
            Destroy(go);
        }
        RootViewController.ShowMainView();
    }

    private void ProductButtonClick () {
        Debug.Log("ProductButtonClick");
        productView.SetActive(true);
        SwitchFingerGesture(false);
    }

    private void CloseProductView () {
        SwitchFingerGesture(true, 1f);
    }

    private void SwitchToCharacter (GameObject obj) {
        GameObject go = GameObject.FindGameObjectWithTag(Config.TAG_CHARACTER);
        if (go == null) {return;}
        CharacterCache cc = go.GetComponent<CharacterCache>();
        if (cc == null || cc.character == null) {return;}
        if (cc.character.activeSelf) {
            CameraSwitcher.SwitchToMain();
            cc.character.SetActive(false);
            SwitchFingerGesture(true);
        }
        else {
            CameraSwitcher.SwitchToFirstPerson();
            cc.character.SetActive(true);
            SwitchFingerGesture(false);
        }
    }
    #endregion

    #region Roam
    private void Roam (GameObject obj) {
        Debug.Log("Call Roam");
        CameraSwitcher.SwitchToRoam();
        GameObject roamCamera = GameObject.FindGameObjectWithTag(Config.TAG_ROAM_CAMERA);
        if (roamCamera != null) {
            Debug.Log("Can find Roam Camera");
            CameraRoamController cc = roamCamera.GetComponent<CameraRoamController>();
            cc.roamStart = OnRoamStart;
            cc.roamComplete = OnRoamComplete;
            cc.Roam();
        }
    }

    private void OnRoamStart () {
        cover.SetActive(true);
        SwitchFingerGesture(false);
    }

    private void OnRoamComplete () {
        CameraSwitcher.SwitchToMain();
        cover.SetActive(false);
        SwitchFingerGesture(true);
    }
    #endregion

}

