﻿using UnityEngine;

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
