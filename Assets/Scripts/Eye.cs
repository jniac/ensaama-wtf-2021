using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public Transform target;
    public bool isFalling = false;
    GameObject clone;

    void Update() {
        if (target != null) {
            if (isFalling == false) {
                transform.LookAt(target);
            }
        }
    }

    void OnMouseDown() {
        clone = Instantiate(gameObject, transform.parent);
        clone.SetActive(false);

        isFalling = true;
        Rigidbody body = gameObject.AddComponent<Rigidbody>();

        // "traînée angulaire", càd la friction associée à la rotation
        // 0.05f par défaut, nous mettons 2.0f pour ralentir les yeux lorsqu'ils roulent.
        body.angularDrag = 1.0f;
        body.drag = 0.25f;

        // On retire la limite de rotation (l'oeil peut bouger librement désormais).
        Destroy(GetComponent<RotationClamp>());

        StartCoroutine(FallEnd());
    }

    // pénible mais c'est ainsi dans Unity, pour retarder une exécution
    // il faut créer une fonction de type "IEnumerator"
    IEnumerator FallEnd() {
        yield return new WaitForSeconds(4f);
        isFalling = false;

        clone.SetActive(true);
    }
}
