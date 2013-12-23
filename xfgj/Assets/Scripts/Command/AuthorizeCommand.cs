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
        if (callback != null) {
            callback(true);
        }
    }

}

