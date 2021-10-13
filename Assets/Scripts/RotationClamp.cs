using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dealing with rotation is hard.
/// This class aim to simplify the process of clamping rotation values.
/// Clamp is done during "LateUpdate", so any computation without regards for 
/// clamp could be done during the "Update" phase.
/// </summary>
[ExecuteAlways]
public class RotationClamp : MonoBehaviour
{
    public bool clampX = true;
    [Range(0, 360f)]
    public float fovX = 45;
    [Range(-180f, 180f)]
    public float angleX = 0;

    public bool clampY = false;
    [Range(0, 360f)]
    public float fovY = 60;
    [Range(-180f, 180f)]
    public float angleY = 0;

    public bool smoothClamp = true;

    public static float Mod180(float x) {
        x %= 360f;
        if (x > 180f) return x - 360f;
        if (x < -180f) return x + 360f;
        return x;
    }

    /// <summary>
    /// Cas particulier (p = 4) de la fonction limite, définit par :
    /// https://www.desmos.com/calculator/8k228ynfyy?lang=fr
    /// </summary>
    public static float Limited4 (float x, float limit = 1) {
        float t = x * 0.5f / limit + 1f;
        t *= t;
        t *= t;
        return (2f * t / (t + 1f) - 1f) * limit;
    }

    public static float Limited4Clamp01(float x, float margin = 0.4f) {
        if (x < margin) return margin - Limited4(margin - x, margin); 
        float max = 1f - margin;
        if (x <= max) return x; 
        return max + Limited4(x - max, margin);
    }

    public static float Limited4Clamp(float x, float min, float max) {
        float d = max - min;
        float t = Limited4Clamp01((x - min) / d);
        return min + t * d;
    }
    
    Quaternion smoothRotation;
    
    void Start() {
        smoothRotation = transform.localRotation;
    }

    void LateUpdate() {
        if (clampX || clampY) {
            Vector3 euler = transform.localEulerAngles;

            if (clampX) {
                euler.x -= angleX;
                euler.x = smoothClamp 
                    ? Limited4Clamp(Mod180(euler.x), -fovX * 0.5f, fovX * 0.5f)
                    : Mathf.Clamp(Mod180(euler.x), -fovX * 0.5f, fovX * 0.5f);
                euler.x += angleX;
            }

            if (clampY) {
                euler.y -= angleY;
                euler.y = smoothClamp 
                    ? Limited4Clamp(Mod180(euler.y), -fovY * 0.5f, fovY * 0.5f)
                    : Mathf.Clamp(Mod180(euler.y), -fovY * 0.5f, fovY * 0.5f);
                euler.y += angleY;
            }
            
            // transform.eulerAngles = euler;
            // Smooth rotation via Slerp is better.
            smoothRotation = Quaternion.Slerp(smoothRotation, Quaternion.Euler(euler), 0.4f);
            transform.localRotation = smoothRotation;
        }
    }

    public float gizmosSize = 2f;
    void OnDrawGizmos() {
        Vector3 d = new Vector3();
        // WARN: Very hazardous trigonometry. Should be rewritten.
        var ay = transform.eulerAngles.y * Mathf.Deg2Rad;
        void DrawX(float a, float size) {
            d.x = Mathf.Sin(ay);
            d.y = -Mathf.Sin(a * Mathf.Deg2Rad);
            d.z = -Mathf.Cos(ay) * -Mathf.Cos(a * Mathf.Deg2Rad);
            Gizmos.DrawRay(transform.position, d * size);
        }
        void DrawY(float a, float size) {
            d.x = Mathf.Sin(a * Mathf.Deg2Rad);
            d.y = 0;
            d.z = Mathf.Cos(a * Mathf.Deg2Rad);
            Gizmos.DrawRay(transform.position, d * size);
        }
        float step = 3f;
        int count;
        if (clampX) {
            Gizmos.color = Color.red;
            count = Mathf.CeilToInt(fovX / step);
            // range
            var x = transform.parent?.eulerAngles.x ?? 0;
            for (int i = 0; i <= count; i++) {
                DrawX(x + angleX + fovX * (-0.5f + (float)i / count), gizmosSize);
            }
            // current angle
            DrawX(transform.eulerAngles.x, gizmosSize * 1.5f);
        }
        if (clampY) {
            Gizmos.color = Color.green;
            count = Mathf.CeilToInt(fovY / step);
            // range
            var y = transform.parent?.eulerAngles.y ?? 0;
            for (int i = 0; i <= count; i++) {
                DrawY(y + angleY + fovY * (-0.5f + (float)i / count), gizmosSize);
            }
            // current angle
            DrawY(transform.eulerAngles.y, gizmosSize * 1.5f);
        }
    }
}
