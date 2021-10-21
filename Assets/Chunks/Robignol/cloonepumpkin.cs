using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robignol {

    public class cloonepumpkin : MonoBehaviour
    {
        void OnTriggerEnter(Collider other) {
            GameObject clone = Instantiate(other.gameObject);
                
            if (clone.GetComponent<scaleDestroyPump>() == null) {
                clone.AddComponent<scaleDestroyPump>();
            }
        }
    }
}
