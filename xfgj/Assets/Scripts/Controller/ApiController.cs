using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

public class ApiController {

    private static ApiCaller apiCaller;

    static ApiController () {
        GameObject obj = GameObject.Find("InitObj");
        if (obj == null) {
            throw new Exception("Can't find InitObj");
        }
        apiCaller = obj.GetComponent<ApiCaller>();
        if (apiCaller == null) {
            throw new Exception("Can't find ApiCaller");
        }
    }

    public static void Authorize (string appKey, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.APP_KEY, appKey);
        rp.callback = handle;
        apiCaller.Authorize(rp);
    }

    public static void GetScene (string token, int sceneId, DateTime? modified,
                                 ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "" + sceneId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        apiCaller.GetScene(rp);
    }

    public static void GetScenes (string token, int? sceneTypeId,
                                  ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (sceneTypeId != null) {
            rp.data.Add(Param.SCENE_TYPE_ID, "" + sceneTypeId);
        }
        rp.callback = handle;
        apiCaller.GetScenes(rp);
    }

    public static void GetAllScenes (string token, DateTime? modified,
                                     ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.LATEST_MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        apiCaller.GetScenes(rp);
    }

    public static void GetSceneSnapshot (string token, List<int> sceneIds,
                                         ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        StringBuilder sb = new StringBuilder();
        foreach (int sceneId in sceneIds) {
            sb.Append(sceneId + ",");
        }
        rp.data.Add(Param.SCENE_IDS, sb.ToString());
        rp.callback = handle;
        apiCaller.GetSceneSnapshot(rp);
    }

    public static void GetSceneType (string token, int sceneTypeId, DateTime? modified,
                                     ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_TYPE_ID, "" + sceneTypeId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        apiCaller.GetSceneType(rp);
    }

    public static void GetAllSceneType (string token, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetAllSceneType(rp);
    }

    public static void GetProductInScene (string token, int sceneId, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "" + sceneId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetProductInScene(rp);
    }

    public static void GetProduct (string token, int productId, DateTime? modified,
                                   ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + productId);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        apiCaller.GetProduct(rp);
    }

    public static void GetItem (string token, int numIid, DateTime? modified,
                                ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + numIid);
        rp.data.Add(Param.TOKEN, token);
        if (modified != null) {
            rp.data.Add(Param.MODIFIED, StringUtil.DateTimeToString((DateTime)modified));
        }
        rp.callback = handle;
        apiCaller.GetItem(rp);
    }

    public static void GetItemWithProductId (string token, int productId, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "" + productId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetItemWithProductId(rp);
    }

    public static void GetProducer (string token, int producerId, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "" + producerId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetProducer(rp);
    }

    public static void GetProductsWithProducerId (string token, int producerId,
                                                  ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "" + producerId);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetProductsWithProducerId(rp);
    }

    public static void GetCategory (string token, int cid, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.CID, "" + cid);
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetCategory(rp);
    }

    public static void GetCategorys (string token, int? parentCid, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        if (parentCid != null) {
            rp.data.Add(Param.PARENT_CID, "" + (int)parentCid);
        }
        rp.callback = handle;
        apiCaller.GetCategorys(rp);
    }

    public static void GetRecommends (string token, ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, token);
        rp.callback = handle;
        apiCaller.GetRecommends(rp);
    }

    public static void GetAppSetting (string token) {

    }

    public static void GetAssets (string token, List<int> assetIds, ResponseHandle handle) {
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
        apiCaller.GetAssets(rp);
    }

    public static void UploadAtlas (string token, string metaData, string unityPath,
                                    string iphonePath, string androidPath,
                                    ResponseHandle handle) {
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
        apiCaller.UploadAssetBundle(rp);
    }

    public static void UploadScene (string token, string metaData, string unityPath,
                                    string iphonePath, string androidPath,
                                    ResponseHandle handle) {
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
        apiCaller.UploadAssetBundle(rp);
    }

    public static void GetPictures (string token, List<string> pictureIds,
                                    ResponseHandle handle) {
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
        apiCaller.GetPictures(rp);
    }

    public static void CheckUpdate (string appName, string appVersion,
                                    ResponseHandle handle) {
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.APP_NAME, appName);
        rp.data.Add(Param.APP_VERSION, appVersion);
        rp.callback = handle;
        apiCaller.CheckUpdate(rp);
    }
}

