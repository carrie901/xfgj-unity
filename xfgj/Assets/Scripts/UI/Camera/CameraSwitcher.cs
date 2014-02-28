using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject roamCamera;

    private static CameraSwitcher cs;

    #region MonoBehaviour
    void Awake () {
        cs = this;
    }

    void Start () {
    
    }
    #endregion

    #region static
    public static void SwitchToMain () {
        if (cs.roamCamera != null) {
            cs.roamCamera.SetActive(false);
        }
        if (cs.mainCamera != null) {
            cs.mainCamera.SetActive(true);
        }
    }

    public static void SwitchToRoam () {
        if (cs.mainCamera != null) {
            cs.mainCamera.SetActive(false);
        }
        if (cs.roamCamera != null) {
            cs.roamCamera.SetActive(true);
        }
    }

    public static void SwitchToFirstPerson () {
        if (cs.mainCamera != null) {
            cs.mainCamera.SetActive(false);
        }
        if (cs.roamCamera != null) {
            cs.roamCamera.SetActive(false);
        }
    }
    #endregion
}

