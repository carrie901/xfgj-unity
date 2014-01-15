using UnityEngine;
using System.Collections.Generic;

public class GetPictureCommand : BaseCommand {

    private UpdateDelegate callback;
    public UpdateDelegate Callback {
        set {
            callback = value;
        }
    }

    private List<string> pictureIds;
    public List<string> PictureIds {
        set {
            pictureIds = value;
        }
    }

    public override void execute () {
        if (Application.internetReachability == NetworkReachability.NotReachable) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        ApiController.GetPictures(AppSetting.getInstance().token, pictureIds, handle);
    }

    private void handle (string str) {
        if (string.IsNullOrEmpty(str)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        List<Picture> list = PictureSerializer.ToObjects(str);
        if (list != null && list.Count != 0) {
            LogicController.ReplacePictures(list);
        }
        if (callback != null) {
            callback(true);
        }
    }
}

