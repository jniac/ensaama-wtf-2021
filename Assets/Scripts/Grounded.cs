using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteAlways]
public class Grounded : MonoBehaviour {
    public float groundDistance = 0.7f;

    [System.NonSerialized]
    public bool isGrounded = false;
    [System.NonSerialized]
    public RaycastHit hit;

    void Update() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, ~0, QueryTriggerInteraction.Ignore);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundDistance);
        if (isGrounded) {
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Grounded))]
    class MyEditor : Editor {
        Grounded Target => target as Grounded;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var before = GUI.enabled;
            GUI.enabled = false;
            EditorGUILayout.Toggle("Is Grounded", Target.isGrounded);
            GUI.enabled = before;
        }
    }
#endif
}
