using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;

public class ApiTest : UUnitTestCase
{

    [UUnitTest]
    public void AuthorizeTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.APP_KEY, Config.APP_KEY);
        rp.callback = new ApiCaller.ResponseHandle(AuthorizeCallback);
        obj.SendMessage("Authorize", rp, SendMessageOptions.RequireReceiver);
    }

    void AuthorizeCallback (string res) {
        JsonData jd = JsonMapper.ToObject(res);
        UUnitAssert.True(jd.Contains("token"));
    }

    [UUnitTest]
    public void GetSceneTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetSceneCallback);
        obj.SendMessage("GetScene", rp, SendMessageOptions.RequireReceiver);
    }

    void GetSceneCallback(string res) {
        JsonData jd = JsonMapper.ToObject(res);
        Scene scene = new Scene((int)(jd[Param.SCENE_ID]), (string)(jd[Param.NAME]),
                                (int)(jd[Param.TYPE_ID]), (string)(jd[Param.PIC_URL]),
                                (string)(jd[Param.DETAILS]), StringUtil.StringToDateTime((string)(jd[Param.MODIFIED])),
                                (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
        UUnitAssert.Equals(100001, scene.sceneId);
    }

    [UUnitTest]
    public void GetScenesTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetScenesCallback);
        obj.SendMessage("GetScenes", rp, SendMessageOptions.RequireReceiver);
    }

    void GetScenesCallback(string res) {
        JsonData jd = JsonMapper.ToObject(res);
        UUnitAssert.True(jd.IsArray);
        Scene scene = new Scene((int)(jd[0][Param.SCENE_ID]), (string)(jd[0][Param.NAME]),
                                (int)(jd[0][Param.TYPE_ID]), (string)(jd[0][Param.PIC_URL]),
                                (string)(jd[0][Param.DETAILS]), StringUtil.StringToDateTime((string)(jd[0][Param.MODIFIED])),
                                (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
        UUnitAssert.NotNull(scene);
    }

    [UUnitTest]
    public void GetSceneTypeTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_TYPE_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetSceneTypeCallback);
        obj.SendMessage("GetSceneType", rp, SendMessageOptions.RequireReceiver);
    }

    void GetSceneTypeCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        SceneType sceneType = new SceneType((int)(jd[Param.TYPE_ID]), (string)(jd[Param.NAME]),
                                            StringUtil.StringToDateTime((string)(jd[Param.MODIFIED])));
        UUnitAssert.Equals(100001, sceneType.typeId);
    }

    [UUnitTest]
    public void GetAllSceneTypeTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetAllSceneTypeCallback);
        obj.SendMessage("GetAllSceneType", rp, SendMessageOptions.RequireReceiver);
    }

    void GetAllSceneTypeCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        UUnitAssert.True(jd.IsArray);
        SceneType sceneType = new SceneType((int)(jd[0][Param.TYPE_ID]), (string)(jd[0][Param.NAME]),
                                            StringUtil.StringToDateTime((string)(jd[0][Param.MODIFIED])));
        UUnitAssert.NotNull(sceneType);
    }
    
    [UUnitTest]
    public void GetProductInSceneTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetProductInSceneCallback);
        obj.SendMessage("GetProductInScene", rp, SendMessageOptions.RequireReceiver);
    }

    void GetProductInSceneCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        UUnitAssert.True(jd.IsArray);
    }

    [UUnitTest]
    public void GetProductTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCT_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetProductCallback);
        obj.SendMessage("GetProduct", rp, SendMessageOptions.RequireReceiver);
    }

    void GetProductCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        UUnitAssert.True(!jd.IsArray);
        Product product = new Product((int)(jd[Param.PRODUCT_ID]),
                                      (int)(jd[Param.PRODUCER_ID]),
                                      (string)(jd[Param.NAME]),
                                      (int)(jd[Param.CID]),
                                      (string)(jd[Param.SIZE]),
                                      (string)(jd[Param.PIC_URL]),
                                      (string)(jd[Param.DETAILS]),
                                      StringUtil.StringToDateTime((string)(jd[Param.MODIFIED])),
                                      (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
        UUnitAssert.NotNull(product);
    }

    [UUnitTest]
    public void GetProducerTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetProducerCallback);
        obj.SendMessage("GetProducer", rp, SendMessageOptions.RequireReceiver);
    }

    void GetProducerCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        Producer producer = new Producer((int)(jd[Param.PRODUCER_ID]),
                                        (string)(jd[Param.NAME]),
                                        (string)(jd[Param.DETAILS]),
                                        StringUtil.StringToDateTime((string)(jd[Param.MODIFIED])));
        UUnitAssert.NotNull(producer);
    }

    [UUnitTest]
    public void GetProductsWithProducerIdTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiCaller.RequestParams rp = new ApiCaller.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.PRODUCER_ID, "100001");
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiCaller.ResponseHandle(GetProductsWithProducerIdCallback);
        obj.SendMessage("GetProductsWithProducerId", rp, SendMessageOptions.RequireReceiver);
    }

    void GetProductsWithProducerIdCallback(string res) {
        JsonData jd =JsonMapper.ToObject(res);
        UUnitAssert.True(jd.IsArray);
        if (jd.Count > 0) {
            Product product = new Product((int)(jd[0][Param.PRODUCT_ID]),
                                      (int)(jd[0][Param.PRODUCER_ID]),
                                      (string)(jd[0][Param.NAME]),
                                      (int)(jd[0][Param.CID]),
                                      (string)(jd[0][Param.SIZE]),
                                      (string)(jd[0][Param.PIC_URL]),
                                      (string)(jd[0][Param.DETAILS]),
                                      StringUtil.StringToDateTime((string)(jd[0][Param.MODIFIED])),
                                      (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
            UUnitAssert.NotNull(product);
        }
    }
}


