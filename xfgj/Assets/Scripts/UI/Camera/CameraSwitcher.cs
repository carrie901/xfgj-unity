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
    public static void SwitchToMain() {
        cs.roamCamera.SetActive(false);
        cs.mainCamera.SetActive(true);
    }

    public static void SwitchToRoam() {
        cs.mainCamera.SetActive(false);
        cs.mainCamera.SetActive(true);
    }
    #endregion
}

