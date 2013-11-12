using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;

public class ApiTest : UUnitTestCase
{

    [UUnitTest]
    public void AuthorizeTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiController.RequestParams rp = new ApiController.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.APP_KEY, Config.APP_KEY);
        rp.callback = new ApiController.ResponseHandle(AuthorizeCallback);
        obj.SendMessage("Authorize", rp, SendMessageOptions.RequireReceiver);
    }

    void AuthorizeCallback (string res) {
        JsonData jd = JsonMapper.ToObject(res);
        UUnitAssert.True(jd.Contains("token"));
    }

    [UUnitTest]
    public void GetSceneTest() {
        GameObject obj = GameObject.Find("InitObj");
        ApiController.RequestParams rp = new ApiController.RequestParams();
        rp.data = new Dictionary<string, string>();
        rp.data.Add(Param.SCENE_ID, 1);
        rp.data.Add(Param.TOKEN, AppSetting.getInstance().token);
        rp.callback = new ApiController.ResponseHandle(GetSceneCallback);
        obj.SendMessage("GetScene", rp, SendMessageOptions.RequireReceiver);
    }

    void GetSceneCallback(string res) {
        JsonData jd = JsonMapper.ToObject(res);
    }
}


