using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Planneur : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3();
    
    void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}





