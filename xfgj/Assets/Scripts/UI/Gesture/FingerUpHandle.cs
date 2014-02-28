using UnityEngine;
using System.Collections;

public class FingerUpHandle : MonoBehaviour {

    private FingerUpDelegate fingerUpDelegate;
    public FingerUpDelegate FingerUpCallback {
        set {
            fingerUpDelegate = value;
        }
    }

    void OnFingerUp (FingerUpEvent e) {
        Debug.Log("Call OnFingerUp");
        if (fingerUpDelegate != null) {
            fingerUpDelegate();
        }
    }

}

