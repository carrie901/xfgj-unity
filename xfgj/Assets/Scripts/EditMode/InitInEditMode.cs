using UnityEngine;
using System;

[ExecuteInEditMode]
public class InitInEditMode : MonoBehaviour {

    void Start () {
        if (!(Application.isEditor && !Application.isPlaying)) {
            Destroy(this);
            return;
        }
        BindScripts();
    }

    private void BindScripts () {
        ApiCaller ac = gameObject.GetComponent<ApiCaller>();
        if (ac == null) {
            gameObject.AddComponent("ApiCaller");
        }
    }

    void callback (string res) {
        Debug.Log("NetCall res " + res);
    }

}

