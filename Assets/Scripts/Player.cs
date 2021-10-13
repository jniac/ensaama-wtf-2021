using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Player : MonoBehaviour {

    // Singleton class, for convenient access from third party scripts (cf LookAtPlayer).
    public static Player player;
    
    void Awake() {
        Player.player = this;
    }
}
