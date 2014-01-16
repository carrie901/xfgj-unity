using UnityEngine;
using System;

public class RootViewController : MonoBehaviour {

    private static RootViewController rvc;

    public class SwitchParam {
        public string view;
        public object obj;
    }

    public GameObject mainView;
    public GameObject sceneView;

    private SceneViewController svc;

    #region MonoBehaviour
    void Awake () {
        rvc = this;
    }

    void Start () {
        svc = sceneView.GetComponent<SceneViewController>();
    }

    #endregion

    #region static
    public static void ShowSceneView (int sceneId) {
        rvc.SwitchToScene(sceneId);
    }

    public static void ShowMainView () {
        rvc.SwitchToMain();
    }
    #endregion

    #region private
    private void SwitchToScene (int sceneId) {
        svc.sceneId = sceneId;
        mainView.SetActive(false);
        sceneView.SetActive(true);
    }

    private void SwitchToMain () {
        mainView.SetActive(true);
        sceneView.SetActive(false);
    }
    #endregion

}

