using UnityEngine;
using System.Collections.Generic;
using LitJson;

public class GetSceneSnapshotCommand : BaseCommand{

    private List<int> sceneIds;
    public List<int> SceneIds {
        set {
            sceneIds = value;
        }
    }

    private UpdateDelegate callback;
    public UpdateDelegate Callback {
        set {
            callback = value;
        }
    }

    public override void execute () {
        if (sceneIds == null) {
            throw new System.ArgumentNullException("sceneIds can't be null");
        }
        ApiController.GetSceneSnapshot(AppSetting.getInstance().token, sceneIds, handle);
    }

    void handle (string res) {
        if (string.IsNullOrEmpty(res)) {
            if (callback != null) {
                callback(false);
            }
            return;
        }
        JsonData jd = JsonMapper.ToObject(res);
        if (jd.Contains(Param.SCENE)) {
            List<Scene> updateList;
            List<Scene> deleteList;
            SceneSerializer.ToObjects(jd[Param.SCENE].ToJson(), out updateList, out deleteList);
            if (updateList != null && updateList.Count != 0) {
                LogicController.ReplaceScenesIgnoreFavourite(updateList);
            }
            if (deleteList != null && deleteList.Count != 0) {
                LogicController.DeleteScenes(deleteList);
            }
        }
        if (jd.Contains(Param.PRODUCT)) {
            List<Product> updateList;
            List<Product> deleteList;
            ProductSerializer.ToObjects(jd[Param.PRODUCT].ToJson(), out updateList, out deleteList);
            if (updateList != null && updateList.Count != 0) {
                LogicController.ReplaceProducts(updateList);
            }
            if (deleteList != null && deleteList.Count != 0) {
                LogicController.DeleteProducts(deleteList);
            }
        }
        if (jd.Contains(Param.PICTURE)) {
            List<Picture> list = PictureSerializer.ToObjects(jd[Param.PICTURE].ToJson());
            if (list != null && list.Count != 0) {
                LogicController.ReplacePictures(list);
            }
        }
        if (jd.Contains(Param.ASSET)) {
            List<Asset> list = AssetSerializer.ToObjects(jd[Param.ASSET].ToJson());
            if (list != null && list.Count != 0) {
                LogicController.ReplaceAssets(list);
            }
        }
        if (callback != null) {
            callback(true);
        }
    }
}

