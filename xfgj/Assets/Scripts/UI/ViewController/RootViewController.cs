using UnityEngine;
using System;

public class RootViewController : MonoBehaviour {

    public static readonly string VIEW_MAIN = "view_main";
    public static readonly string VIEW_SCENE = "view_scene";

    public class SwitchParam {
        public string view;
        public object obj;
    }

    public GameObject mainView;
    public GameObject sceneView;

    private SceneViewController svc;

    #region MonoBehaviour
    void Awake () {

    }

    void Start () {
        svc = sceneView.GetComponent<SceneViewController>();
    }

    #endregion

    #region public
    public void SwitchView (SwitchParam param) {
        if (VIEW_MAIN.Equals(param.view)) {
            Debug.Log("SwitchView mainView");
            mainView.SetActive(true);
            sceneView.SetActive(false);
        }
        else if (VIEW_SCENE.Equals(param.view)) {
            Debug.Log("SwitchView sceneView");
            svc.sceneId = (int)param.obj;
            mainView.SetActive(false);
            sceneView.SetActive(true);
        }
    }

    #endregion

}

