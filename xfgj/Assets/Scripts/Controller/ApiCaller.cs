using UnityEngine;
using System;
using System.Collections.Generic;

public delegate void ResponseHandle(string res);

[ExecuteInEditMode]
public class ApiCaller : MonoBehaviour {

    public class RequestParams {
        public Dictionary<string, string> data;
        public ResponseHandle callback;
    }

    /*
     * required: app_key
     */
    public void Authorize (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/authorize";
        StartCoroutine(NetUtil.Post(url, rp.data, rp.callback));
    }

    /*
     * required: token, scene_id
     * optional: modified
     */
    public void GetScene (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/scene/" + rp.data[Param.SCENE_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.MODIFIED)) {
            url += "&" + Param.MODIFIED + "="  + rp.data[Param.MODIFIED];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     * optional: scene_type_id, last_modified
     */
    public void GetScenes (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/scenes";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.SCENE_TYPE_ID)) {
            url += "&" + Param.SCENE_TYPE_ID + "=" + rp.data[Param.SCENE_TYPE_ID];
        }
        if (rp.data.ContainsKey(Param.LATEST_MODIFIED)) {
            url += "&" + Param.LATEST_MODIFIED + "=" + rp.data[Param.LATEST_MODIFIED];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, scene_id
     */
    public void GetSceneSnapshot (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/scenes/snapshot";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        url += "&" + Param.SCENE_IDS + "=" + rp.data[Param.SCENE_IDS];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, scene_type_id
     * optional: modified
     */
    public void GetSceneType (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/scene_type/" + rp.data[Param.SCENE_TYPE_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.MODIFIED)) {
            url += "&" + Param.MODIFIED + "=" + rp.data[Param.SCENE_TYPE_ID];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    public void GetAllSceneType (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/scene_types";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, scene_id
     */
    public void GetProductInScene (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/product_in_scene/" + rp.data[Param.SCENE_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, product_id
     * optional: modified
     */
    public void GetProduct (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/product/" + rp.data[Param.PRODUCT_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.MODIFIED)) {
            url += "&" + Param.MODIFIED + "="  + rp.data[Param.MODIFIED];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, num_iid
     * optional: modified
     */
    public void GetItem (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/item/" + rp.data[Param.NUM_IID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.MODIFIED)) {
            url += "&" + Param.MODIFIED + "="  + rp.data[Param.MODIFIED];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, product_id
     */
    public void GetItemWithProductId (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/items_in_product/" + rp.data[Param.PRODUCT_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, producer_id
     */
    public void GetProducer (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/producer/" + rp.data[Param.PRODUCER_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, producer_id
     */
    public void GetProductsWithProducerId (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/products/" + rp.data[Param.PRODUCER_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, cid
     */
    public void GetCategory (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/category/" + rp.data[Param.CID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     * optional: parent_cid
     */
    public void GetCategorys (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/categorys";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.PARENT_CID)) {
            url += "&" + Param.PARENT_CID + "=" + rp.data[Param.PARENT_CID];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     */
    public void GetRecommends (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/recommends";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     */
    public void GetAppSetting (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/app";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }


    public void GetAssets (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/assets";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        url += "&" + Param.ASSET_IDS + "=" + rp.data[Param.ASSET_IDS];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     *           asset_file  file data
     *           meta_data   relate model
     *           type        atlas or scene
     */
    public void UploadAssetBundle (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/asset/upload";
        Dictionary<string, string> postFields = new Dictionary<string, string>();
        Dictionary<string, string> postFiles = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> postArg in rp.data) {
            if (postArg.Key.Equals(Param.ASSET_UNITY) ||
                postArg.Key.Equals(Param.ASSET_IPHONE) ||
                postArg.Key.Equals(Param.ASSET_ANDROID)) {
                postFiles.Add(postArg.Key, postArg.Value);
            }
            else {
                postFields.Add(postArg.Key, postArg.Value);
            }
        }
        StartCoroutine(NetUtil.Upload(url, postFields, postFiles, rp.callback));
    }

    public void GetPictures (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/pictures";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        url += "&" + Param.PICTURES + "=" + rp.data[Param.PICTURES];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    public void CheckUpdate (RequestParams rp) {
        string url = AppSetting.getInstance().serverUrl + "/api/checkupdate";
        url += "?" + Param.APP_NAME + "=" + rp.data[Param.APP_NAME];
        url += "&" + Param.APP_VERSION + "=" + rp.data[Param.APP_VERSION];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

}

