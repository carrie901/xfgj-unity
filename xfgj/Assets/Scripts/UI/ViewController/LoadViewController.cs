using UnityEngine;
using System.Collections;

public class LoadViewController : MonoBehaviour {

    private static readonly int SHOW_DEPTH = 5;
    private static readonly int HIDE_DEPTH = -1;

    private static LoadViewController lvc;

    public GameObject indicatorView;
    public GameObject progressView;

    private UIPanel indicatorPanel;
    private UIPanel progressPanel;
    private UISlider progressSlider;
    private UILabel tipLabel;

    #region MonoBehaviour
    void Awake () {
        lvc = this;
        indicatorPanel = indicatorView.GetComponent<UIPanel>();
        progressPanel = progressView.GetComponent<UIPanel>();
        progressSlider = progressView.transform.Find("Progress Bar").gameObject.GetComponent<UISlider>();
        tipLabel = progressView.transform.Find("Tip").gameObject.GetComponent<UILabel>();
    }

    void Start () {
        
    }
    #endregion

    #region static
    public static void ShowLoadIndicator () {
        lvc.ShowIndicatorView();
    }

    public static void HideLoadIndicator () {
        lvc.StartCoroutine(lvc.HideIndicatorView());
    }

    public static void ShowLoadProgress () {
        lvc.ShowProgressView();
    }

    public static void HideLoadProgress () {
        lvc.HideProgressView();
    }

    public static void NotifyProgress (float progress) {
        lvc.UpdateProgress(progress);
    }
    #endregion

    #region private
    private void ShowIndicatorView () {
        indicatorView.SetActive(true);
        indicatorPanel.depth = SHOW_DEPTH;
    }

    private IEnumerator HideIndicatorView () {
        yield return new WaitForSeconds(2);
        indicatorPanel.depth = HIDE_DEPTH;
        indicatorView.SetActive(false);
    }

    private void ShowProgressView () {
        progressView.SetActive(true);
        progressPanel.depth = SHOW_DEPTH;
    }

    private void HideProgressView () {
        progressPanel.depth = HIDE_DEPTH;
        progressView.SetActive(false);
    }

    private void UpdateProgress (float progress) {
        progressSlider.value = progress;
    }
    #endregion
}

