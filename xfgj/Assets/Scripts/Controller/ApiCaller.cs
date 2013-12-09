using UnityEngine;
using System;
using System.Collections.Generic;

public class ApiCaller : MonoBehaviour {

    public delegate void ResponseHandle(string res);

    public class RequestParams {
        public Dictionary<string, string> data;
        public ResponseHandle callback;
    }

    /*
     * required: app_key
     */
    public void Authorize (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/authorize";
        StartCoroutine(NetUtil.Post(url, rp.data, rp.callback));
    }

    /*
     * required: token, scene_id
     * optional: modified
     */
    public void GetScene (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/scene/" + rp.data[Param.SCENE_ID];
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
        string url = Config.SERVER_URL + "/api/scenes";
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
     * required: token, scene_type_id
     * optional: modified
     */
    public void GetSceneType (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/scene_type/" + rp.data[Param.SCENE_TYPE_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.MODIFIED)) {
            url += "&" + Param.MODIFIED + "=" + rp.data[Param.SCENE_TYPE_ID];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    public void GetAllSceneType (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/scene_types";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, scene_id
     */
    public void GetProductInScene (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/product_in_scene/" + rp.data[Param.SCENE_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, product_id
     * optional: modified
     */
    public void GetProduct (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/product/" + rp.data[Param.PRODUCT_ID];
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
        string url = Config.SERVER_URL + "/api/item/" + rp.data[Param.NUM_IID];
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
        string url = Config.SERVER_URL + "/api/items_in_product/" + rp.data[Param.PRODUCT_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, producer_id
     */
    public void GetProducer (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/producer/" + rp.data[Param.PRODUCER_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, producer_id
     */
    public void GetProductsWithProducerId (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/products/" + rp.data[Param.PRODUCER_ID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token, cid
     */
    public void GetCategory (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/category/" + rp.data[Param.CID];
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

    /*
     * required: token
     * optional: parent_cid
     */
    public void GetCategorys (RequestParams rp) {
        string url = Config.SERVER_URL + "/api/categorys";
        url += "?" + Param.TOKEN + "=" + rp.data[Param.TOKEN];
        if (rp.data.ContainsKey(Param.PARENT_CID)) {
            url += "&" + Param.PARENT_CID + "=" + rp.data[Param.PARENT_CID];
        }
        StartCoroutine(NetUtil.Get(Uri.EscapeUriString(url), rp.callback));
    }

}

