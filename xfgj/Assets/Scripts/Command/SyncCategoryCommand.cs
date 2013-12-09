using UnityEngine;
using System.Collections.Generic;

public class SyncCategoryCommand : BaseCommand {

    public SyncCategoryCommand (){
    }

    public override void execute () {
        ApiController.GetCategorys(AppSetting.getInstance().token, null,
                                   new ApiCaller.ResponseHandle(handle));
    }

    private void handle (string str) {
        List<Category> list = CategorySerializer.ToObjects(str);
        if (list != null) {
            Debug.Log("Category count " + list.Count);
        }
    }
}

