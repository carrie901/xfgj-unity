using UnityEngine;
using System.Collections;

public class NGUI309 : MonoBehaviour {

    public GameObject cameraGo;

    private int cullingMask;

    private static NGUI309 adjustor;

    void Awake () {
        adjustor = this;
        cullingMask = cameraGo.GetComponent<Camera>().cullingMask;
    }

    public static void Adjust () {
        adjustor.DeleteCamera();
    }

    private void DeleteCamera () {
        for (int i = 0; i < cameraGo.transform.parent.childCount; ++i) {
            Transform t = cameraGo.transform.parent.GetChild(i);
            if (t.childCount == 0) {
                Destroy(t.gameObject);
            }
        }
        cameraGo.GetComponent<Camera>().cullingMask = cullingMask;
    }

}

