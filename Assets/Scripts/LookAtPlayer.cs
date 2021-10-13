using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookAtPlayer : MonoBehaviour {

    void Update() {
        if (Application.isPlaying) {
            if (Player.player) {
                transform.LookAt(Player.player.transform.position);
            }    
        } else {
            var player = FindObjectOfType<Player>();
            if (player) {
                transform.LookAt(player.transform.position);
            }
        }
    }
}
