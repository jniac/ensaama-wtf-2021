using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class WTFMain : MonoBehaviour {

    public GameObject defaultChunk;
    public GameObject[] coolChunks;

    public Vector2Int gridSize = new Vector2Int(4, 3);
    public static Vector3 GetOffset(int gx, int gy) {
        float d = Mathf.Floor(gy / 2);
        float x = gx - gy + d;
        float y = gy;
        float z = gx + d;
        return new Vector3(x, y, z) * 10f;
    }
    public void Build() {
        Clear();
        var center = -GetOffset(gridSize.x - 1, gridSize.y - 1) / 2f;

        var sourceChunks = new GameObject[] { defaultChunk }
            .Concat(coolChunks)
            .Where(c => !!c) // Make sure no nullified instance will pass.
            .ToArray();

        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                GameObject source = sourceChunks[Random.Range(0, sourceChunks.Length)];
                GameObject instance = Instantiate(source, transform);
                instance.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
                instance.transform.localPosition = center + GetOffset(x, y);
                instance.transform.localRotation = Quaternion.identity;

                // Make sure clone have a chunk on it.
                if (instance.GetComponent<Chunk>() == null) {
                    instance.AddComponent<Chunk>();
                }
            }
        }

        transform.position = Vector3.forward * 5f;
    }

    public void Clear() {
        foreach(var chunk in GetComponentsInChildren<Chunk>()) {
            DestroyImmediate(chunk.gameObject);
        }
        // foreach(var chunk in FindObjectsOfType<Chunk>()) {
        //     DestroyImmediate(chunk.gameObject);
        // }
    }

    void Start() {
        Build();    
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(WTFMain))]
    class MyEditor : Editor {
        WTFMain Target => target as WTFMain;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("(Re) Build")) {
                Target.Build();
            }
            if (GUILayout.Button("Clear")) {
                Target.Clear();
            }
        }
    }
#endif
}
