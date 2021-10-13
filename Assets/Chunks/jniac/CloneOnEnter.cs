using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jniac {

    public class CloneOnEnter : MonoBehaviour
    {
        void OnTriggerEnter(Collider other) {
            GameObject clone = Instantiate(other.gameObject);
                
            if (clone.GetComponent<ScaleDownAndDestroy>() == null) {
                clone.AddComponent<ScaleDownAndDestroy>();
            }
        }
    }
}
