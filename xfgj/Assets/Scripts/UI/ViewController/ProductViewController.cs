using UnityEngine;
using System;

public delegate void ProductViewDelegate ();

public class ProductViewController : MonoBehaviour {

    public GameObject closeButton;
    public GameObject cover;

    public int productId;
    public ProductViewDelegate hideDelegate;

    private Product product;
    private GameObject model;

    #region MonoBehaviour
    void Awake () {
        UIEventListener.Get(closeButton).onClick = CloseProductView;
        UIEventListener.Get(cover).onClick = CloseProductView;
    }

    void Start () {
    }

    void OnEnable () {
        product = LogicController.GetProduct(productId);
        GameObject prefab = Resources.Load("Prefabs/ProductPrefab") as GameObject;
        model = Instantiate(prefab) as GameObject;
        model.transform.position = new Vector3(0, 1000, 0);
    }

    void OnDisable () {
        productId = 0;
        product = null;
        Destroy(model);
    }
    #endregion

    #region private
    private void CloseProductView (GameObject go) {
        Debug.Log("CloseProductView " + go.name);
        gameObject.SetActive(false);
        if (hideDelegate != null) {
            hideDelegate();
        }
    }
    #endregion
}

