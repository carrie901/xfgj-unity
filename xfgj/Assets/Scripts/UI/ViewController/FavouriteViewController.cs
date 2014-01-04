using UnityEngine;
using System;
using System.Collections.Generic;

public class FavouriteViewController : MonoBehaviour {

    #region MonoBehaviour
    void Awake () {
        ScenesViewController scenesViewController = gameObject.GetComponent<ScenesViewController>();
        scenesViewController.dataSource = DataSource;
        scenesViewController.enabled = true;
    }
    #endregion

    #region private
    private List<Scene> DataSource (int index, int count) {
        return LogicController.GetFavouriteScenes(index, count);
    }
    #endregion
}

