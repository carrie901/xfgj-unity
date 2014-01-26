using UnityEngine;
using System.Collections;

public delegate void ContextButtonClick();

public class ContextButtonController : MonoBehaviour {
    public static readonly int FLAG_PRODUCT = 1;
    public static readonly int OFFSET_Y = 200;

    public GameObject productButton;
    public ContextButtonClick productDelegate;

    #region MonoBehaviour
    void Start () {
        UIEventListener.Get(productButton).onClick = ProductButtonClick;
    }
    #endregion

    #region public
    public void ShowContextButton(int flag) {
        if (flag == FLAG_PRODUCT) {
            ButtonLayout(productButton);
            ButtonAppear(productButton);
        }
    }

    public void HideContextButton() {
        if (productButton.activeSelf) {
            if (productButton.transform.parent.localPosition.y == 0) {
                ButtonDisappear(productButton);
            }
        }
    }
    #endregion

    #region private
    private void ButtonLayout (params GameObject[] buttons) {
        if (buttons == null) {
            return;
        }
        foreach (GameObject go in buttons) {
            go.SetActive(true);
        }
        switch (buttons.Length) {
        case 1:
            buttons[0].transform.parent.localPosition = new Vector3(0, 0, 0);
            break;
        case 2:
            buttons[0].transform.parent.localPosition = new Vector3(-129, 0, 0);
            buttons[1].transform.parent.localPosition = new Vector3(129, 0, 0);
            break;
        case 3:
            buttons[0].transform.parent.localPosition = new Vector3(-258, 0, 0);
            buttons[1].transform.parent.localPosition = new Vector3(0, 0, 0);
            buttons[2].transform.parent.localPosition = new Vector3(258, 0, 0);
            break;
        case 4:
            buttons[0].transform.parent.localPosition = new Vector3(-387, 0, 0);
            buttons[1].transform.parent.localPosition = new Vector3(-129, 0, 0);
            buttons[2].transform.parent.localPosition = new Vector3(129, 0, 0);
            buttons[3].transform.parent.localPosition = new Vector3(387, 0, 0);
            break;
        }
    }

    private void ButtonAppear (params GameObject[] buttons) {
        if (buttons == null) {
            return;
        }
        Debug.Log("ContextButton Appear");
        float delay = 0f;
        foreach (GameObject go in buttons) {
            Vector3 pos = go.transform.parent.localPosition;
            TweenPosition tp = UITweener.Begin<TweenPosition>(go.transform.parent.gameObject, 1.0f);
            tp.from = new Vector3(pos.x, pos.y - OFFSET_Y, pos.z);
            tp.to = new Vector3(pos.x, pos.y, pos.z);
            tp.delay = delay;
            tp.PlayForward();
            delay += 0.3f;
        }
    }

    private void ButtonDisappear (params GameObject[] buttons) {
        if (buttons == null) {
            return;
        }
        Debug.Log("ContextButton Disappear");
        float delay = 0f;
        foreach (GameObject go in buttons) {
            Vector3 pos = go.transform.parent.localPosition;
            TweenPosition tp = UITweener.Begin<TweenPosition>(go.transform.parent.gameObject, 1.0f);
            tp.from = new Vector3(pos.x, pos.y, pos.z);
            tp.to = new Vector3(pos.x, pos.y - OFFSET_Y, pos.z);
            tp.delay = delay;
            EventDelegate.Add(tp.onFinished, delegate() {go.SetActive(false);});
            tp.PlayForward();
            delay += 0.3f;
        }
    }

    private void ProductButtonClick (GameObject go) {
        if (productDelegate != null) {
            productDelegate();
        }
        ButtonDisappear(productButton);
    }
    #endregion
}

