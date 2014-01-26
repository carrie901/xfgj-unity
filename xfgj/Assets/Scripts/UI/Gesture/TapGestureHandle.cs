using UnityEngine;
using System.Collections;

public class TapGestureHandle : MonoBehaviour {

    private SelectObjectDelegate selectDelegate;
    public SelectObjectDelegate SelectCallback {
        set {
            selectDelegate = value;
        }
    }

    private TapIntercept tapIntercept;
    public TapIntercept TapInterceptDelegate {
        set {
            tapIntercept = value;
        }
    }

    #region MonoBehaviour
    void Start () {
    }
    #endregion

    #region FingerGesture
    void OnTap (TapGesture gesture) {
        if (tapIntercept()) {
            return;
        }
        if (selectDelegate != null) {
            selectDelegate(gesture.Selection);
        }
    }
    #endregion
}
