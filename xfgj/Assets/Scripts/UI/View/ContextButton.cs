using UnityEngine;
using System.Collections;

public class ContextButton : MonoBehaviour {

    void OnDisappear() {
        Debug.Log("Called OnDisappear");
        gameObject.SetActive(false);
    }
}

