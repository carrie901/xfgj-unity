using UnityEngine;
using System.Collections.Generic;

public class SyncSceneTypeCommand : BaseCommand {

    private UpdateDelegate callback;
    public UpdateDelegate Callback {
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
        ApiController.GetAllSceneType(AppSetting.getInstance().token, handle);
    }

    private void handle (string str) {
        if (string.IsNullOrEmpty(str)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        List<SceneType> updateList;
        List<SceneType> deleteList;
        SceneTypeSerializer.ToObjects(str, out updateList, out deleteList);
        if (updateList != null && updateList.Count != 0) {
            LogicController.ReplaceSceneTypes(updateList);
        }
        if (deleteList != null && deleteList.Count != 0) {
            LogicController.DeleteSceneTypes(deleteList);
        }
        if (callback != null) {
            callback(true);
        }
    }
}

