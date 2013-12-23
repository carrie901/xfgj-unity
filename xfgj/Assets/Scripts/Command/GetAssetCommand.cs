using System;
using System.Collections.Generic;

public class GetAssetCommand : BaseCommand {

    private UIDelegate.Update callback;
    public UIDelegate.Update Callback {
        set {
            callback = value;
        }
    }

    private int? assetId;
    public int? AssetId {
        set {
            assetId = value;
        }
    }

    public override void execute () {
        if (assetId == null) {
            throw new ArgumentNullException("assetId can't be null");
        }
        ApiController.GetAsset(AppSetting.getInstance().token, (int)assetId, null);
    }

    private void handle (string res) {
        if (string.IsNullOrEmpty(res)) {
            if (callback != null) {
                callback(null);
            }
            return;
        }
        Asset asset = AssetSerializer.ToObject(res);
        List<Asset> list = new List<Asset>();
        list.Add(asset);
        LogicController.ReplaceAssets(list);
        if (callback != null) {
            callback(asset);
        }
    }

}

