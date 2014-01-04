using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

public class ApiController {

    private static GameObject obj;

    static ApiController () {
        obj = GameObject.Find("InitObj");
    }

    public static void Authorize (string appKey, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.APP_KEY, appKey);
        rp.callback = handle;
        obj.SendMessage("Authorize", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetScene (string token, int sceneId, DateTime? modified,
                                 ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "" + sceneId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        obj.SendMessage("GetScene", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetScenes (string token, int? sceneTypeId,
                                  ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (sceneTypeId != null) {
            rp.data.Add(Param.SCENE_TYPE_ID, "" + sceneTypeId);
        }
        rp.callback = handle;
        obj.SendMessage("GetScenes", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetAllScenes (string token, DateTime? modified,
                                     ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.LATEST_MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        obj.SendMessage("GetScenes", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetSceneSnapshot (string token, List<int> sceneIds,
                                         ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        StringBuilder sb = new StringBuilder();
        foreach (int sceneId in sceneIds) {
            sb.Append(sceneId + ",");
        }
        rp.data.Add(Param.SCENE_IDS, sb.ToString());
        rp.callback = handle;
        obj.SendMessage("GetSceneSnapshot", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetSceneType (string token, int sceneTypeId, DateTime? modified,
                                     ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_TYPE_ID, "" + sceneTypeId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        obj.SendMessage("GetSceneType", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetAllSceneType (string token, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetAllSceneType", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetProductInScene (string token, int sceneId, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "" + sceneId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetProductInScene", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetProduct (string token, int productId, DateTime? modified,
                                   ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + productId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        obj.SendMessage("GetProduct", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetItem (string token, int numIid, DateTime? modified,
                                ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + numIid);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        obj.SendMessage("GetItem", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetItemWithProductId (string token, int productId, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + productId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetItemWithProductId", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetProducer (string token, int producerId, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "" + producerId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetProducer", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetProductsWithProducerId (string token, int producerId,
                                                  ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "" + producerId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetProductsWithProducerId", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetCategory (string token, int cid, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.CID, "" + cid);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetCategory", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetCategorys (string token, int? parentCid, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (parentCid != null) {
            rp.data.Add(Param.PARENT_CID, "" + (int)parentCid);
        }
        rp.callback = handle;
        obj.SendMessage("GetCategorys", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetRecommends (string token, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        obj.SendMessage("GetRecommends", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetAppSetting (string token) {

    }

    public static void GetAssets (string token, List<int> assetIds, ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < assetIds.Count; ++i) {
            sb.Append(assetIds[i]);
            if (i != assetIds.Count - 1) {
                sb.Append(",");
            }
        }
        rp.data.Add(Param.ASSET_IDS, sb.ToString());
        rp.callback = handle;
        obj.SendMessage("GetAssets", rp, SendMessageOptions.RequireReceiver);
    }

    public static void UploadAtlas (string token, string metaData, string unityPath,
                                    string iphonePath, string androidPath,
                                    ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.data.Add(Param.META_DATA, metaData);
        rp.data.Add(Param.ASSET_TYPE, "picture");
        if (!string.IsNullOrEmpty(unityPath)) {
            rp.data.Add(Param.ASSET_UNITY, unityPath);
        }
        if (!string.IsNullOrEmpty(iphonePath)) {
            rp.data.Add(Param.ASSET_IPHONE, iphonePath);
        }
        if (!string.IsNullOrEmpty(androidPath)) {
            rp.data.Add(Param.ASSET_ANDROID, androidPath);
        }
        rp.callback = handle;
        obj.SendMessage("UploadAssetBundle", rp, SendMessageOptions.RequireReceiver);
    }

    public static void UploadScene (string token, string metaData, string unityPath,
                                    string iphonePath, string androidPath,
                                    ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.data.Add(Param.META_DATA, metaData);
        rp.data.Add(Param.ASSET_TYPE, "scene");
        if (!string.IsNullOrEmpty(unityPath)) {
            rp.data.Add(Param.ASSET_UNITY, unityPath);
        }
        if (!string.IsNullOrEmpty(iphonePath)) {
            rp.data.Add(Param.ASSET_IPHONE, iphonePath);
        }
        if (!string.IsNullOrEmpty(androidPath)) {
            rp.data.Add(Param.ASSET_ANDROID, androidPath);
        }
        rp.callback = handle;
        obj.SendMessage("UploadAssetBundle", rp, SendMessageOptions.RequireReceiver);
    }

    public static void GetPictures (string token, List<string> pictureIds,
                                    ApiCaller.ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < pictureIds.Count; ++i) {
            if (pictureIds[i] == null) {
                continue;
            }
            sb.Append(pictureIds[i]);
            if (i != pictureIds.Count - 1) {
                sb.Append(",");
            }
        }
        rp.data.Add(Param.PICTURES, sb.ToString());
        rp.callback = handle;
        obj.SendMessage("GetPictures", rp, SendMessageOptions.RequireReceiver);
    }
}

