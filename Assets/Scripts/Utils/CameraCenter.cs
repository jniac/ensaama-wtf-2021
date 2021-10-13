using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {
    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if (Physics.Raycast(transform.position, transform.forward, out var hit)) {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawSphere(hit.point, 0.1f);
        } else {
            float d = GetComponent<Camera>()?.farClipPlane ?? 10f;
            Gizmos.DrawRay(transform.position, transform.forward * d);
        }
    }
}
