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
        ApiController ac = gameObject.GetComponent<ApiController>();
        if (ac == null) {
            gameObject.AddComponent("ApiController");
        }
    }

}
