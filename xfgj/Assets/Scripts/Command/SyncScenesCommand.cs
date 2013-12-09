using UnityEngine;
using System;
using System.Collections.Generic;

public class SyncScenesCommand : BaseCommand {

    private UIDelegate.Update callback;

    public SyncScenesCommand (UIDelegate.Update callback) {
        this.callback = callback;
    }

    public override void execute () {
        DateTime? dt = null;
        Scene scene = LogicController.GetLatestMofifiedScene();
        if (scene != null) {
            dt = scene.modified;
        }
        ApiController.GetAllScenes(AppSetting.getInstance().token, dt,
                                   new ApiCaller.ResponseHandle(handle));
    }

    private void handle (string str) {
        List<Scene> list = SceneSerializer.ToObjects(str);
        if (list != null && list.Count != 0) {
            LogicController.ReplaceScenes(list);
            callback(true);
        }
        else {
            callback(false);
        }
    }

}
