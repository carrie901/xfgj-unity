using UnityEngine;
using System.Collections;

public delegate void SwitchDelegate ();

public class SingleModelController : MonoBehaviour {

    public GameObject surroundCamera;
    public int offset;

    public SwitchDelegate beforeMoveAway;
    public SwitchDelegate beforeMoveBack;
    public SwitchDelegate afterMoveAway;
    public SwitchDelegate afterMoveBack;

    void Start () {
    
    }

    public void MoveAway () {
        CustomTBOrbit ct = surroundCamera.GetComponent<CustomTBOrbit>();
        if (ct != null) {
            ct.enabled = false;
        }
        TweenPosition tp = UITweener.Begin<TweenPosition>(surroundCamera, 1.0f);
        Vector3 pos = surroundCamera.transform.localPosition;
        tp.from = pos;
        tp.to = pos + surroundCamera.transform.right * offset;
        tp.onFinished.Clear();
        EventDelegate.Add(tp.onFinished, AfterMoveAway);
        tp.PlayForward();
        if (beforeMoveAway != null) {
            beforeMoveAway();
        }
    }

    public void MoveBack () {
        TweenPosition tp = UITweener.Begin<TweenPosition>(surroundCamera, 1.0f);
        Vector3 pos = surroundCamera.transform.localPosition;
        tp.from = pos;
        tp.to = pos - surroundCamera.transform.right * offset;
        tp.onFinished.Clear();
        EventDelegate.Add(tp.onFinished, AfterMoveBack);
        tp.PlayForward();
        if (beforeMoveBack != null) {
            beforeMoveBack();
        }
    }

    private void AfterMoveAway () {
        Debug.Log("AfterMoveAway");
        if (afterMoveAway != null) {
            afterMoveAway();
        }
    }

    private void AfterMoveBack () {
        Debug.Log("AfterMoveBack");
        CustomTBOrbit ct = surroundCamera.GetComponent<CustomTBOrbit>();
        if (ct != null) {
            ct.enabled = true;
        }
        if (afterMoveBack != null) {
            afterMoveBack();
        }
    }
    
}

