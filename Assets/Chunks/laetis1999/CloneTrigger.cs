using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace laetis1999 {

    public class CloneTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other) {
            GameObject clone = Instantiate(other.gameObject);
                
            if (clone.GetComponent<Destroy>() == null) {
                clone.AddComponent<Destroy>();
            }
        }
    }
}
