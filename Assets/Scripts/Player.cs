using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Player : MonoBehaviour {

    // Singleton class, for convenient access from third party scripts (cf LookAtPlayer).
    public static Player player;
    
    void Awake() {
        if (Player.player == null) {
            Player.player = this;
        }
    }
    
    public void CameraTrack(bool force = false) {
        Cinemachine.CinemachineVirtualCamera camera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if (camera != null) {
            if (camera.Follow == null || force) {
                camera.Follow = transform;
            }
        }
    }
}
