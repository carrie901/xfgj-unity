using UnityEngine;
using System.Collections;

public class LoadViewController : MonoBehaviour {

    private static readonly int SHOW_DEPTH = 5;
    private static readonly int HIDE_DEPTH = -1;

    private static LoadViewController lvc;

    public GameObject simpleView;

    private UIPanel simplePanel;

    #region MonoBehaviour
    void Awake () {
        lvc = this;
        simplePanel = simpleView.GetComponent<UIPanel>();
    }

    void Start () {
        
    }
    #endregion

    #region static
    public static void ShowSimpleLoad () {
        lvc.ShowSimpleView();
    }

    public static void HideSimpleLoad () {
        lvc.StartCoroutine(lvc.HideSimpleView());
    }
    #endregion

    #region private
    private void ShowSimpleView () {
        simpleView.SetActive(true);
        simplePanel.depth = SHOW_DEPTH;
    }

    private IEnumerator HideSimpleView () {
        yield return new WaitForSeconds(2);
        simplePanel.depth = HIDE_DEPTH;
        simpleView.SetActive(false);
    }
    #endregion
}

