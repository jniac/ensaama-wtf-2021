using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jniac {

    public class Head : MonoBehaviour {

        public bool playerIsNear = false;

        public float scale = 1;

        void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Player>() != null) {
                playerIsNear = true;
            }
        }

        void OnTriggerExit(Collider other) {
            if (other.GetComponent<Player>() != null) {
                playerIsNear = false;
            }
        }

        void Update() {
            float d = (playerIsNear ? 1f : -1f) * Time.deltaTime;
            scale = Mathf.Clamp(scale + d, 1f, 2f);
            transform.localScale = Vector3.one * scale;
        }
    }
}
