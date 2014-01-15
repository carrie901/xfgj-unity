using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;


public class RecommendViewController : MonoBehaviour {

    public GameObject table;

    private UITable tableComp;

    private List<RecommendItemView> itemViewList;
    private List<string> picIdList;
    private List<int> assetIdList;

    #region MonoBehaviour
    void Awake () {
        tableComp = table.GetComponent<UITable>();
        tableComp.onReposition = OnReposition;
        itemViewList = new List<RecommendItemView>();
        picIdList = new List<string>();
        assetIdList = new List<int>();

        UIScrollView scrollComp = table.transform.parent.gameObject.GetComponent<UIScrollView>();
        scrollComp.onDragFinished = DragFinished;
    }

    void Start () {
        GetRecommendsCommand cmd = new GetRecommendsCommand();
        cmd.Callback = AfterGetRecomends;
        cmd.execute();
    }

    void OnEnable () {
        LoadViewController.ShowLoadIndicator();
        ClearView();
        GenerateView();
        LoadViewController.HideLoadIndicator();
    }

    void OnDisable () {
        ClearView();
    }
    #endregion

    #region private methods
    public List<Recommend> GetTestData () {
        List<Recommend> list = new List<Recommend>();
        for (int i = 0; i < 9; i++) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            JsonWriter jw = new JsonWriter(sb);
            jw.WriteObjectStart();
            jw.WritePropertyName(Param.X);
            jw.Write((i + 1) % 2 != 0 ? -190 : 190);
            jw.WritePropertyName(Param.Y);
            jw.Write(350 * (-i / 2));
            jw.WritePropertyName(Param.WIDTH);
            jw.Write(350);
            jw.WritePropertyName(Param.HEIGHT);
            jw.Write(300);
            jw.WriteObjectEnd();
            list.Add(new Recommend(i, "abc", "", DateTime.Now, DateTime.Now, sb.ToString()));
        }
        return list;
    }

    private void GenerateView () {
        picIdList.Clear();
        assetIdList.Clear();
        //List<Recommend> recommends = LogicController.GetRecommends();
        List<Recommend> recommends = GetTestData();
        for (int i = 0; i < recommends.Count; ++i) {
            RecommendItemView itemView = RecommendItemView.Create(table);
            itemView.RecommendId = recommends[i].recommendId;
            itemView.PictureId = recommends[i].pictureId;
            PositionSerialize(recommends[i].position, out itemView.x, out itemView.y, out itemView.width, out itemView.height);
            UIEventListener.Get(itemView.gameObject).onClick = ItemClick;
            itemViewList.Add(itemView);

            Picture picture = LogicController.GetPicture(recommends[i].pictureId);
            if (picture == null) {
                picIdList.Add(recommends[i].pictureId);
            }
            else {
                Asset asset = LogicController.GetAsset(picture.assetId);
                if (asset == null) {
                    assetIdList.Add(picture.assetId);
                }
            }
        }
        tableComp.Reposition();
        if (picIdList.Count != 0) {
            GetPictureCommand cmd = new GetPictureCommand();
            cmd.Callback = AfterGetPicture;
            cmd.PictureIds = picIdList;
            cmd.execute();
        }
        if (assetIdList.Count != 0) {
            GetAssetCommand cmd = new GetAssetCommand();
            cmd.Callback = AfterGetAsset;
            cmd.AssetIds = assetIdList;
            cmd.execute();
        }
        picIdList.Clear();
        assetIdList.Clear();
    }

    private void ClearView () {
        table.transform.DetachChildren();
        if (itemViewList != null && itemViewList.Count != 0) {
            foreach (RecommendItemView itemView in itemViewList) {
                if (itemView != null) {
                    itemView.release();
                }
            }
            itemViewList.Clear();
        }
    }

    private void PositionSerialize (string str, out int x, out int y, out int width, out int height) {
        if (str == null || str.Equals(String.Empty)) {
            x = 0;
            y = 0;
            width = 0;
            height = 0;
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        x = (int)jd[Param.X];
        y = (int)jd[Param.Y];
        width = (int)jd[Param.WIDTH];
        height = (int)jd[Param.HEIGHT];
    }
    #endregion

    #region View delegate
    private void DragFinished () {
        Debug.Log("RecommendViewController ");
    }

    private void OnReposition () {
        for (int i = 0; i < itemViewList.Count; ++i) {
            Transform t = itemViewList[i].gameObject.transform;
            t.localPosition = new Vector3(itemViewList[i].x, itemViewList[i].y, 0);
            itemViewList[i].MatchPictureSize();
        }
    }

    private void ItemClick (GameObject go) {
        Debug.Log("Recommend click");
#if UNITY_IPHONE
        IosPlugin.OpenWebsite("http://www.baidu.com");
#elif UNITY_ANDROID
        Debug.Log("Android will open website");
#endif
    }

    private void AfterGetRecomends (object obj) {
        Debug.Log("AfterGetPicture");
        if (obj != null && !(bool)obj) {
            return;
        }
        OnEnable();
    }

    private void AfterGetPicture (object obj) {
        Debug.Log("AfterGetPicture");
        if (obj != null && !(bool)obj) {
            return;
        }
        foreach (RecommendItemView itemView in itemViewList) {
            if (itemView != null) {
                itemView.ShowPicture();
            }
        }
    }

    private void AfterGetAsset (object obj) {
        Debug.Log("AfterGetAsset");
        if (obj != null && !(bool)obj) {
            return;
        }
        foreach (RecommendItemView itemView in itemViewList) {
            if (itemView != null) {
                itemView.ShowPicture();
            }
        }
    }
    #endregion

}

