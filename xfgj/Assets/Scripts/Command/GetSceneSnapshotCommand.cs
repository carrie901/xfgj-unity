using UnityEngine;
using System.Collections.Generic;
using LitJson;

public class GetSceneSnapshotCommand : BaseCommand{

    private int sceneId;

    public GetSceneSnapshotCommand (int sceneId) {
        this.sceneId = sceneId;
    }

    public override void execute () {
        ApiController.GetSceneSnapshot(AppSetting.getInstance().token, sceneId, handle);
    }

    void handle (string res) {
        if (res == null) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(res);
        if (jd.Contains(Param.SCENE)) {
            Scene scene;
            bool isDeleted;
            SceneSerializer.ToObject(jd[Param.SCENE].ToJson(), out scene, out isDeleted);
            if (isDeleted) {
                LogicController.DeleteScene(scene);
            }
            else {
                List<Scene> list = new List<Scene>();
                list.Add(scene);
                LogicController.ReplaceScenes(list);
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
    }
}

