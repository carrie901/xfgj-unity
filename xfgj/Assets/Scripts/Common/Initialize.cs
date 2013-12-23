using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Initialize : MonoBehaviour {

    #region MonoBehaviour
    void Awake () {
        BindScripts();
        LogicController.CreateTable();
    }

    void Start () {
    }
    #endregion
    
    private void BindScripts () {
        ApiCaller ac = gameObject.GetComponent<ApiCaller>();
        if (ac == null) {
            gameObject.AddComponent("ApiCaller");
        }
        AssetBundleController abc = gameObject.GetComponent<AssetBundleController>();
        if (abc == null) {
            gameObject.AddComponent("AssetBundleController");
        }
        MobclickAgent ma = gameObject.GetComponent<MobclickAgent>();
        if (ma == null) {
            gameObject.AddComponent("MobclickAgent");
        }
        if (!Application.isEditor) {
            InitInEditMode comp = gameObject.GetComponent<InitInEditMode>();
            Destroy(comp);
        }
    }

}
