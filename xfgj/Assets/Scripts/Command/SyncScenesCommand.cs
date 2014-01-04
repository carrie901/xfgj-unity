using UnityEngine;
using System;
using System.Collections.Generic;

public class SyncScenesCommand : BaseCommand {

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
        DateTime? dt = null;
        Scene scene = LogicController.GetLatestMofifiedScene();
        if (scene != null) {
            dt = scene.modified;
        }
        ApiController.GetAllScenes(AppSetting.getInstance().token, dt,
                                   new ApiCaller.ResponseHandle(handle));
    }

    private void handle (string str) {
        if (string.IsNullOrEmpty(str)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        List<Scene> updateList;
        List<Scene> deleteList;
        SceneSerializer.ToObjects(str, out updateList, out deleteList);
        bool result = false;
        if (updateList != null && updateList.Count != 0) {
            LogicController.ReplaceScenesIgnoreFavourite(updateList);
            result = true;
        }
        if (deleteList != null && deleteList.Count != 0) {
            LogicController.DeleteScenes(deleteList);
            result = true;
        }
        if (callback != null) {
            callback(result);
        }
    }

}
