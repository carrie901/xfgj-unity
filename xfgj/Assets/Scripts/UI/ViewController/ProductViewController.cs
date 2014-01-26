using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;

public delegate void ProductViewDelegate ();

public class ProductViewController : MonoBehaviour {

    public GameObject closeButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject infoContent;
    public GameObject scrollCamera;
    public GameObject cover;
    public GameObject redButton;
    
    public GameObject sourceCamera;
    public GameObject topLeft;
    public GameObject bottomRight1;
    public GameObject bottomRight2;

    public Product product;
    public ProductViewDelegate hideDelegate;

    private UITable tableComp;
    private UILabel nameLabel;
    private UILabel sizeLabel;
    private UILabel detailLabel;

    private GameObject modelPrefab;
    private GameObject model;
    private GameObject labelPrefab;

    #region MonoBehaviour
    void Awake () {
        UIEventListener.Get(closeButton).onClick = CloseProductView;
        UIEventListener.Get(cover).onClick = CloseProductView;
        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        UIEventListener.Get(rightButton).onClick = RightButtonClick;

        tableComp = infoContent.GetComponent<UITable>();
        tableComp.onReposition = OnReposition;
    }

    void Start () {
    }

    void OnEnable () {
        if (product == null) {
            gameObject.SetActive(false);
            NotificationViewController.ShowNotification(Localization.Localize(StringKey.Msg_FindProductFail));
            return;
        }
        cover.SetActive(true);
        scrollCamera.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(true);
        modelPrefab = Resources.Load("Prefabs/ProductPrefab") as GameObject;
        model = Instantiate(modelPrefab) as GameObject;
        model.transform.position = new Vector3(0, 1000, 0);

        labelPrefab = Resources.Load("Prefabs/ProductLabel") as GameObject;

        SingleModelController smc = model.GetComponent<SingleModelController>();
        smc.beforeMoveAway = BeforeMoveAway;
        smc.beforeMoveBack = BeforeMoveBack;
        smc.afterMoveAway = AfterMoveAway;
        smc.afterMoveBack = AfterMoveBack;

        ResizeViewport();
        tableComp.Reposition();
    }

    void OnDisable () {
        infoContent.SetActive(false);
        scrollCamera.SetActive(false);
        product = null;
        if (model != null) {
            Destroy(model);
        }
        Resources.UnloadUnusedAssets();
        modelPrefab = null;
        labelPrefab = null;
        cover.SetActive(false);
    }
    #endregion

    #region private
    private void OnReposition () {
        for (int i = 0; i < infoContent.transform.childCount; ++i) {
            Destroy(infoContent.transform.GetChild(i).gameObject);
        }
        string[,] array = null;
        if (!string.IsNullOrEmpty(product.details)) {
            JsonData jd = JsonMapper.ToObject(product.details);
            if (jd.IsArray) {
                array = new string[jd.Count + 1, 2];
                for (int i = 0; i < jd.Count; ++i) {
                    array[i + 1, 0] = (string)jd[i]["key"];
                    array[i + 1, 1] = (string)jd[i]["value"];
                }
            }
        }
        if (array == null) {
            array = new string[1, 2];
        }
        array[0, 0] = "";
        array[0, 1] = product.name;

        UILabel label = labelPrefab.transform.Find("Content").gameObject.GetComponent<UILabel>();
        NGUIText.fontSize = label.fontSize;
        NGUIText.fontStyle = label.fontStyle;
        NGUIText.rectWidth = label.width;
        NGUIText.Update();

        int yOffset = 0;
        int lineSpace = 15;
        UIViewport vp = scrollCamera.GetComponent<UIViewport>();
        int height = (int)(vp.topLeft.localPosition.y - vp.bottomRight.localPosition.y);
        for (int i = 0; i < array.Length / 2; ++i) {
            GameObject row = NGUITools.AddChild(infoContent, labelPrefab);
            UILabel nameLabel = row.transform.Find("Name").gameObject.GetComponent<UILabel>();
            UILabel contentLabel = row.transform.Find("Content").gameObject.GetComponent<UILabel>();
            nameLabel.text = array[i, 0];
            contentLabel.text = array[i, 1];
            BoxCollider bc = row.GetComponent<BoxCollider>();
            UIDragCamera dc = row.GetComponent<UIDragCamera>();
            dc.draggableCamera = scrollCamera.GetComponent<UIDraggableCamera>();
            Vector2 rect = NGUIText.CalculatePrintedSize(array[i, 1]);
            if (i == array.Length / 2 - 1 && Math.Abs(yOffset - (int)rect.y - lineSpace)  < height) {
                contentLabel.height = height - Math.Abs(yOffset);
            }
            else {
                contentLabel.height = (int)rect.y;
            }
            bc.center = new Vector3(0, -(contentLabel.height + lineSpace)/2, 0);
            bc.size = new Vector3(bc.size.x, contentLabel.height + lineSpace, 0);
            row.transform.localPosition = new Vector3(0, yOffset, 0);
            yOffset -= (int)rect.y + lineSpace;
        }
    }

    private void ResizeViewport () {
        SingleModelController smc = model.GetComponent<SingleModelController>();
        UIViewport vp = smc.surroundCamera.GetComponent<UIViewport>();
        if (vp != null) {
            vp.sourceCamera = sourceCamera.GetComponent<Camera>();
            vp.topLeft = topLeft.transform;
            vp.bottomRight = bottomRight2.transform;
        }
    }

    private void CloseProductView (GameObject go) {
        Debug.Log("CloseProductView " + go.name);
        gameObject.SetActive(false);
        if (hideDelegate != null) {
            hideDelegate();
        }
    }

    private void LeftButtonClick (GameObject go) {
        SingleModelController smc = model.GetComponent<SingleModelController>();
        if (smc != null) {
            smc.MoveBack();
        }
    }

    private void RightButtonClick (GameObject go) {
        SingleModelController smc = model.GetComponent<SingleModelController>();
        if (smc != null) {
            smc.MoveAway();
        }
    }

    private void BeforeMoveAway () {
        infoContent.SetActive(true);
        TweenPosition tp = UITweener.Begin<TweenPosition>(infoContent, 1.0f);
        Vector3 pos = infoContent.transform.localPosition;
        tp.from = pos + infoContent.transform.right * 610;
        tp.to = pos;
        tp.onFinished.Clear();
        tp.PlayForward();
    }

    private void BeforeMoveBack () {
        TweenPosition tp = UITweener.Begin<TweenPosition>(infoContent, 1.0f);
        Vector3 pos = infoContent.transform.localPosition;
        tp.from = pos;
        tp.to = pos + infoContent.transform.right * 610;
        tp.onFinished.Clear();
        EventDelegate.Add(tp.onFinished, delegate () {
            infoContent.transform.localPosition = pos;
            infoContent.SetActive(false);
        });
        tp.PlayForward();
    }

    private void AfterMoveAway () {
        leftButton.SetActive(true);
        rightButton.SetActive(false);
    }

    private void AfterMoveBack () {
        leftButton.SetActive(false);
        rightButton.SetActive(true);
    }
    #endregion
}

