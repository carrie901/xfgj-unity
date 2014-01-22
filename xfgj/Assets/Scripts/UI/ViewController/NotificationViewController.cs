using UnityEngine;
using System.Collections;

public class NotificationViewController : MonoBehaviour {

    private static readonly int SHOW_DEPTH = 6;
    private static readonly int HIDE_DEPTH = -1;

    private static NotificationViewController nvc;

    public GameObject notificationView;

    private UILabel label;

    #region MonoBehaviour
    void Awake () {
        nvc = this;
        label = notificationView.transform.Find("Label").gameObject.GetComponent<UILabel>();
    }

    void Start () {
    
    }
    #endregion

    #region static
    public static void ShowNotification (string message) {
        nvc.Show(message);
    }
    #endregion

    #region private
    private void Show (string message) {
        notificationView.SetActive(true);
        label.text = message;
        TweenAlpha ta = UITweener.Begin<TweenAlpha>(notificationView, 1.0f);
        ta.from = 0.0f;
        ta.to = 1.0f;
        ta.onFinished.Clear();
        EventDelegate.Add(ta.onFinished, OnCompleteAppear);
        ta.PlayForward();
    }

    private void OnCompleteAppear () {
        Debug.Log("Notification OnCompleteAppear");
        TweenAlpha ta = UITweener.Begin<TweenAlpha>(notificationView, 1.0f);
        ta.from = 1.0f;
        ta.to = 0.0f;
        ta.delay = 2.0f;
        ta.onFinished.Clear();
        Debug.Log("onFinished count " + ta.onFinished.Count);
        EventDelegate.Add(ta.onFinished, OnCompleteDisappear);
        ta.PlayForward();
    }

    private void OnCompleteDisappear () {
        Debug.Log("Notification OnCompleteDisappear");
        notificationView.SetActive(false);
    }
    #endregion
}

