using UnityEngine;
using System.Collections;

public class LookTarget : MonoBehaviour {

    void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }
}
