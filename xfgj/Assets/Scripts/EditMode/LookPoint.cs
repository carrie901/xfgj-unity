using UnityEngine;
using System.Collections;

public class LookPoint : MonoBehaviour {

    void OnDrawGizmos () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
