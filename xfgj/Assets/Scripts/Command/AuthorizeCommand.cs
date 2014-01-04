using UnityEngine;
using System;
using LitJson;

public class AuthorizeCommand : BaseCommand{

    private UIDelegate.Update callback;
    public UIDelegate.Update Callback {
        set {
            callback = value;
        }
    }

    public override void execute () {
        if (Application.internetReachability == NetworkReachability.NotReachable) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        if (DateTime.Compare(AppSetting.getInstance().authorizeTime, DateTime.Today) > 0) {
            if (callback != null) {
                callback(true);
            }
            return;
        }
        ApiController.Authorize(Config.APP_KEY, handle);
    }

    void handle(string res) {
        if (string.IsNullOrEmpty(res)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        JsonData jd = JsonMapper.ToObject(res);
        AppSetting.getInstance().token = (string)jd[Param.TOKEN];
        AppSetting.getInstance().authorizeTime = DateTime.Now;
        if (callback != null) {
            callback(true);
        }
    }

}

