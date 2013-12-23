using UnityEngine;
using System;

public class CameraController : MonoBehaviour {

    public GameObject camPathGroup;
    public GameObject lookPathGroup;
    public GameObject lookTarget;

    private Transform[] cameraPath;
    private Transform[] lookPath;
    private float percentage;

    private float frontPos = 0.1f;
    private float topPos = 0.9f;

    #region MonoBehaviour
    void Awake () {
        InitParams();
    }
    void OnGUI () {
        percentage = GUI.VerticalSlider(new Rect(Screen.width-20,20,15,Screen.height-40),percentage,1,0);
        iTween.PutOnPath(gameObject, cameraPath, percentage);
        iTween.PutOnPath(lookTarget, lookPath, percentage);
        transform.LookAt(iTween.PointOnPath(lookPath, percentage));
     
        if(GUI.Button(new Rect(5,Screen.height-25,50,20),"front")){
            SlideTo(frontPos);
        }
        if(GUI.Button(new Rect(60,Screen.height-25,50,20),"top")){
            SlideTo(topPos);
        }
    }

    void OnDrawGizmos () {
        InitParams();
        iTween.DrawPath(cameraPath,Color.magenta);
        iTween.DrawPath(lookPath,Color.cyan);
        Gizmos.color= Color.black;
        Gizmos.DrawLine(gameObject.transform.position, lookTarget.transform.position);
    }
    #endregion

    #region public

    public void MoveToNext () {
        Debug.Log("MoveToNext");
    }

    public void MoveToPrevious () {
        Debug.Log("MoveToPrevious");
    }

    #endregion

    private void InitParams () {
        cameraPath = new Transform[camPathGroup.transform.childCount];
        lookPath = new Transform[lookPathGroup.transform.childCount];
        for (int i = 0; i < camPathGroup.transform.childCount; ++i) {
            cameraPath[i] = camPathGroup.transform.GetChild(i);
        }
        for (int i = 0; i < lookPathGroup.transform.childCount; ++i) {
            lookPath[i] = lookPathGroup.transform.GetChild(i);
        }
    }

    void SlideTo(float position){
        iTween.Stop(gameObject);
        iTween.ValueTo(gameObject,iTween.Hash("from",percentage,"to",position,"time",2,"easetype",iTween.EaseType.easeInOutCubic,"onupdate","SlidePercentage"));
    }
 
    void SlidePercentage(float p){
        percentage=p;
    }
}

