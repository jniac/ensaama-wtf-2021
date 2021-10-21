using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robignol {

    public class scaleDestroyPump : MonoBehaviour
    {
        // https://docs.unity3d.com/ScriptReference/EditorGUILayout.CurveField.html
        public AnimationCurve scaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        float duration = 3f;
        float time = 0f;

        void Start() {
            time = 0f;
            duration = Random.Range(3f, 6f);
        }

        void Update() {
            time += Time.deltaTime;
            float progress = Mathf.Clamp01(time / duration);
            float scale = scaleCurve.Evaluate(progress);
            transform.localScale = Vector3.one * scale;

            if (progress == 1) {
                Destroy(gameObject);
            }
        }
    }
}
