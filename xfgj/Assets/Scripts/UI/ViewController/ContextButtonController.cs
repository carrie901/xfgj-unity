using UnityEngine;
using System.Collections;

public delegate void ContextButtonClick();

public class ContextButtonController : MonoBehaviour {
    public static readonly int FLAG_PRODUCT = 1;
    public static readonly int FLAG_LIGHT = 2;
    public static readonly int OFFSET_Y = 200;

    public GameObject productButton;
    public GameObject lightButton;
    public ContextButtonClick productDelegate;
    public ContextButtonClick lightDelegate;

    #region MonoBehaviour
    void Start () {
        UIEventListener.Get(productButton).onClick = ProductButtonClick;
        UIEventListener.Get(lightButton).onClick = LightButtonClick;
    }
    #endregion

    #region public
    public void ShowContextButton(int flag) {
        GameObject[] buttonGroup = GetButtonGroup(flag);
        if (buttonGroup != null) {
            ButtonLayout(buttonGroup);
            ButtonAppear(buttonGroup);
        }
    }

    public void HideContextButton() {
        Debug.Log("HideContextButton");
        GameObject[] buttons = new GameObject[] {productButton, lightButton};
        foreach (GameObject go in buttons) {
            if (go.activeSelf) {
                ButtonDisappear(go);
            }
        }
    }
    #endregion

    #region private
    private GameObject[] GetButtonGroup (int flag) {
        if (flag == FLAG_PRODUCT) {
            return new GameObject[] {productButton};
        }
        else if (flag == FLAG_LIGHT) {
            return new GameObject[] {productButton, lightButton};
        }
        return null;
    }

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

    private void ButtonAppear (GameObject[] buttons) {
        if (buttons == null) {
            return;
        }
        Debug.Log("ContextButton Appear");
        foreach (GameObject go in buttons) {
            if (go.animation != null) {
                go.animation.Play();
            }
        }
    }

    private void ButtonDisappear (params GameObject[] buttons) {
        if (buttons == null) {
            return;
        }
        Debug.Log("ContextButton Disappear");
        foreach (GameObject go in buttons) {
            if (go.animation != null) {
                go.animation.Play("ContextButtonDisappear");
            }
        }
    }

    private void ProductButtonClick (GameObject go) {
        if (productDelegate != null) {
            productDelegate();
        }
        HideContextButton();
    }

    private void LightButtonClick (GameObject go) {
        if (lightDelegate != null) {
            lightDelegate();
        }
        HideContextButton();
    }
    #endregion
}

