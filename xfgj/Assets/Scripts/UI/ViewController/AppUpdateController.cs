using UnityEngine;
using System.Collections;
using LitJson;

public class AppUpdateController : MonoBehaviour {

    public GameObject updateView;
    public GameObject content;
    public GameObject okButton;
    public GameObject cancelButton;

    private UILabel label;

    #region MonoBehaviour
    void Awake () {
        UIEventListener.Get(okButton).onClick = Confirm;
        UIEventListener.Get(cancelButton).onClick = Cancel;
        label = content.GetComponent<UILabel>();
    }

    void Start () {
        CheckUpdate();
    }

    #endregion

    #region public
    public void CheckUpdate () {
        ApiController.CheckUpdate(Config.APP_NAME, Config.APP_VERSION, CheckUpdateCallback);
    }
    #endregion

    #region delegate
    private void CheckUpdateCallback(string str) {
        Debug.Log("CheckUpdateCallback " + str);
        if (string.IsNullOrEmpty(str)) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        //string version = (string)jd[Param.APP_VERSION];
        string details = (string)jd[Param.UPDATE_DETAILS];
        label.text = details;
        updateView.SetActive(true);
    }

    private void Confirm (GameObject go) {

    }

    private void Cancel (GameObject go) {
        updateView.SetActive(false);
    }
    #endregion
}

