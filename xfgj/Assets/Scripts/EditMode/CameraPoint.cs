using UnityEngine;

public class CameraPoint : MonoBehaviour {

    void OnDrawGizmos () {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }

}
