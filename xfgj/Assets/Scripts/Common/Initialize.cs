using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Initialize : MonoBehaviour {

    // Use this for initialization
    void Start () {
        BindScripts();
        LogicController.CreateTable();
    }
    
    // Update is called once per frame
    void Update () {

    }

    private void BindScripts () {
        ApiCaller ac = gameObject.GetComponent<ApiCaller>();
        if (ac == null) {
            gameObject.AddComponent("ApiCaller");
        }
        ViewController vc = gameObject.GetComponent<ViewController>();
        if (vc == null) {
            Debug.Log("bind ViewController");
            gameObject.AddComponent("ViewController");
        }
        IosAdjustor ia = gameObject.GetComponent<IosAdjustor>();
        if (ia == null) {
            gameObject.AddComponent("IosAdjustor");
        }
        AssetBundleController abc = gameObject.GetComponent<AssetBundleController>();
        if (abc == null) {
            gameObject.AddComponent("AssetBundleController");
        }
    }

}
