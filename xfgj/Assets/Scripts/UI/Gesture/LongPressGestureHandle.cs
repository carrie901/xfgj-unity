using UnityEngine;
using System.Collections;

public class LongPressGestureHandle : MonoBehaviour {

    private SelectObjectDelegate selectDelegate;
    public SelectObjectDelegate SelectCallback {
        set {
            selectDelegate = value;
        }
    }

    #region FingerGesture
    void OnLongPress (LongPressGesture gesture) {
        Debug.Log("call OnLongPress");
        if (selectDelegate != null) {
            selectDelegate(gesture.Selection);
        }
    }
    #endregion
}

