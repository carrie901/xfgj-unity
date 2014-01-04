using UnityEngine;
using System.Collections;

public class TestBehaviour : MonoBehaviour
{
    void Awake () {
        Debug.Log("TestBehaviour Awake");
    }

    void Start () {
        Debug.Log("TestBehaviour Start");
    }

    void OnEnable () {
        Debug.Log("TestBehaviour OnEnable");
    }

    void OnDisable () {
        Debug.Log("TestBehaviour OnDisable");
    }

}

