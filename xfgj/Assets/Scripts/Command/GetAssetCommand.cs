using UnityEngine;
using System;
using System.Collections.Generic;

public class GetAssetCommand : BaseCommand {

    private UpdateDelegate callback;
    public UpdateDelegate Callback {
        set {
            callback = value;
        }
    }

    private List<int> assetIds;
    public List<int> AssetIds {
        set {
            assetIds = value;
        }
    }

    public override void execute () {
        if (assetIds == null) {
            throw new ArgumentNullException("assetIds can't be null");
        }
        if (assetIds.Count == 0) {
            throw new ArgumentNullException("assetIds can't be empty");
        }
        if (Application.internetReachability == NetworkReachability.NotReachable) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        ApiController.GetAssets(AppSetting.getInstance().token, assetIds, handle);
    }

    private void handle (string res) {
        if (string.IsNullOrEmpty(res)) {
            if (callback != null) {
                callback(null);
            }
            return;
        }
        List<Asset> list = AssetSerializer.ToObjects(res);
        if (list != null && list.Count != 0) {
            LogicController.ReplaceAssets(list);
        }
        if (callback != null) {
            callback(null);
        }
    }

}

