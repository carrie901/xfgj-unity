using System.Collections.Generic;

public class SyncSceneTypeCommand : BaseCommand {

    public SyncSceneTypeCommand () {
    }

    public override void execute () {
        ApiController.GetAllSceneType(AppSetting.getInstance().token,
                                      new ApiCaller.ResponseHandle(handle));
    }

    private void handle (string str) {
        List<SceneType> list = SceneTypeSerializer.ToObjects(str);
        if (list != null && list.Count != 0) {
            LogicController.ReplaceSceneTypes(list);
        }
    }
}

