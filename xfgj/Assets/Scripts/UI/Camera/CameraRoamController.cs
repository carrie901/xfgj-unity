using UnityEngine;
using System;

public delegate void RoamDelegate();

public class CameraRoamController : MonoBehaviour {

    public Transform[] lookTargets;
    public Transform[] cameraPath;
    public Transform lookTarget;
    public float duration;

    public RoamDelegate roamStart;
    public RoamDelegate roamComplete;

    private int curIndex;
    private bool animating;
    private float percentage;

    #region MonoBehaviour
    void Awake () {
    }

    void Update () {
        if (!animating) {
            iTween.PutOnPath(gameObject, cameraPath, percentage);
            transform.LookAt(iTween.PointOnPath(lookTargets, percentage));
        }
    }

    void OnDrawGizmos () {
        for (int i = 0; i < cameraPath.Length; ++i) {
            if (i == cameraPath.Length - 1) {
                iTween.DrawPath(new Transform[]{cameraPath[i], cameraPath[0]}, Color.magenta);
            }
            else {
                iTween.DrawPath(new Transform[]{cameraPath[i], cameraPath[i + 1]}, Color.magenta);
            }
        }
        Gizmos.color= Color.black;
        Gizmos.DrawLine(gameObject.transform.position, lookTarget.transform.position);
    }
    #endregion

    #region public
    public void MoveToNext () {
        Debug.Log("MoveToNext");
        if (animating) {
            return;
        }
        if (curIndex == cameraPath.Length - 1) {
            MoveTo(0);
        }
        else {
            MoveTo(curIndex + 1);
        }
    }

    public void MoveToPrevious () {
        Debug.Log("MoveToPrevious");
        if (animating) {
            return;
        }
        if (curIndex == 0) {
            MoveTo(cameraPath.Length - 1);
        }
        else {
            MoveTo(curIndex - 1);
        }
    }

    public void Roam () {
        iTween.Stop(gameObject);
        /*iTween.MoveFrom(gameObject, iTween.Hash("position", cameraPath[0],
                                                "path", cameraPath,
                                                "time", 10.0f,
                                                "looktarget", lookTarget,
                                                "onstart", "RoamStart",
                                                "oncomplete", "RoamComplete"));*/
        iTween.ValueTo(gameObject, iTween.Hash("from", 0,
                                               "to", 1,
                                               "time", duration,
                                               "easetype",iTween.EaseType.linear,
                                               "onupdate", "UpdatePercentage"));
    }

    #endregion

    #region private
    private void UpdatePercentage (float p) {
        percentage = p;
    }


    private void MoveTo (int index){
        Debug.Log("SlideTo " + index + " current " + curIndex);
        iTween.Stop(gameObject);
        Transform[] path = new Transform[2];
        path[0] = cameraPath[curIndex];
        path[1] = cameraPath[index];
        iTween.MoveTo(gameObject, iTween.Hash("position", cameraPath[index],
                                              "path", path,
                                              "movetopath", false,
                                              "looktarget", lookTarget,
                                              "time", 2.0f,
                                              "onstart", "MoveAnimStart",
                                              "onstartparams", index,
                                              "oncomplete", "MoveAnimComplete",
                                              "oncompleteparams", index));
    }

    private void MoveAnimStart (int index) {
        animating = true;
    }
 
    private void MoveAnimComplete (int index){
        Debug.Log("AnimComplete called");
        curIndex = index;
        animating = false;
    }

    private void RoamStart () {
        if (roamStart != null) {
            roamStart();
        }
    }

    private void RoamComplete () {
        if (roamComplete != null) {
            roamComplete();
        }
        CameraSwitcher.SwitchToMain();
    }
    #endregion
}

