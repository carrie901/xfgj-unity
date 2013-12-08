using UnityEngine;
using System;

public class ViewController : MonoBehaviour {

    private GameObject table;
    private UITable tableComp;

    void Start () {
        /*GameObject sceneItem = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/scene_item");
        GameObject button = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/scene_item/Image Button");
        UIEventListener.Get(sceneItem).onClick = PanelClick;
        UIEventListener.Get(button).onClick = ButtonClick;*/

        //GameObject panel = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel");
        MainViewController mainController = new MainViewController();
        mainController.Show();
    }

    void ButtonClick (GameObject gameObject) {
        Debug.Log("button click");
    }

    void PanelClick (GameObject gameObject) {
        Debug.Log("panel click");
    }


}

