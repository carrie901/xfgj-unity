using UnityEngine;
using System.Collections;

public class TapGestureHandle : MonoBehaviour {


    private GestureDelegate.SelectObject selectObject;
    public GestureDelegate.SelectObject SelectCallback {
        set {
            selectObject = value;
        }
    }

    #region MonoBehaviour
    void Start () {
    
    }
    
    void Update () {
    
    }
    #endregion

    #region FingerGesture
    void OnTap (TapGesture gesture) {
        if (gesture.Selection != null) {
            if (selectObject != null) {
                selectObject(gesture.Selection);
            }
            else {
                Debug.Log("TapGesturehandle selectObject is null");
            }
        }
        else {
            Debug.Log("No object was tapped at " + gesture.Position);
        }
    }
    #endregion
}
