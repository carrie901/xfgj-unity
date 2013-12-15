using UnityEngine;
using System;

public class ViewController : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject uiCamera;
    private GameObject table;
    private UITable tableComp;

    void Start () {
        mainCamera = GameObject.Find("MainCamera");
        uiCamera = GameObject.Find("UI Root (2D)/Camera");
        MainViewController mainController = new MainViewController();
        mainController.Show();
    }

    public static void SwitchCamera () {

    }

    void OnApplicationQuit () {
        Debug.Log("OnApplicationQuit called");
    }

}

