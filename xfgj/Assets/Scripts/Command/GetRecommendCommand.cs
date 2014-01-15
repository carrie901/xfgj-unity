using System;
using System.Collections.Generic;

public class GetRecommendsCommand : BaseCommand {

    private UpdateDelegate callback;
    public UpdateDelegate Callback {
        set {
            callback = value;
        }
    }

    public override void execute () {
        ApiController.GetRecommends(AppSetting.getInstance().token, handle);
    }

    private void handle(string res) {
        if (string.IsNullOrEmpty(res)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        List<Recommend> updateList;
        List<Recommend> deleteList;
        RecommendSerializer.ToObjects(res, out updateList, out deleteList);
        if (updateList != null && updateList.Count != 0) {
            LogicController.ReplaceRecommends(updateList);
        }
        if (deleteList != null && deleteList.Count != 0) {
            LogicController.DeleteRecommends(deleteList);
        }
        if (callback != null) {
            callback(true);
        }
    }
}

