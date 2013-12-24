using UnityEngine;
using System;

public class CameraController : MonoBehaviour {

    public GameObject lookTarget;
    public Transform[] cameraPath;
    public Transform[] lookPath; //lookPath must have the same length

    private int curIndex;
    private bool animating;

    #region MonoBehaviour
    void Awake () {
    }

    void Update () {
        if (!animating) {
            lookTarget.transform.position = lookPath[curIndex].position;
            transform.LookAt(lookPath[curIndex]);
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
        iTween.DrawPath(lookPath,Color.cyan);
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

    #endregion

    #region private
    private void MoveTo(int index){
        Debug.Log("SlideTo " + index + " current " + curIndex);
        iTween.Stop(gameObject);
        Transform[] path = new Transform[2];
        path[0] = cameraPath[curIndex];
        path[1] = cameraPath[index];
        iTween.MoveTo(gameObject, iTween.Hash("position", cameraPath[index],
                                              "path", path,
                                              "movetopath", false,
                                              "looktarget", lookPath[index],
                                              "time", 2.0f,
                                              "onstart", "AnimStart",
                                              "onstartparams", index,
                                              "oncomplete", "AnimComplete",
                                              "oncompleteparams", index));
    }

    private void AnimStart(int index) {
        animating = true;
        lookTarget.transform.position = lookPath[index].position;
    }
 
    private void AnimComplete(int index){
        Debug.Log("AnimComplete called");
        curIndex = index;
        animating = false;
    }
    #endregion
}

