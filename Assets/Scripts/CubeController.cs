using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Force" Unity à ajouter un composant Rigidbody lorsque une instance de CubeController est créé.
[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float jumpCoolDown = 0f;
    public float jumpRandomAngularSpeed = 12f;

    // Ici body est null par défaut, la variable sera initialisée durant "Start".
    Rigidbody body;

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void UpdateMove() {

        Vector3 velocity = body.velocity;

        float ix = Input.GetAxis("Horizontal");
        float iz = Input.GetAxis("Vertical");
        float input = Mathf.Clamp01(Mathf.Sqrt(ix * ix + iz * iz));

        velocity.x = Mathf.Lerp(velocity.x, ix * moveSpeed, input);
        velocity.z = Mathf.Lerp(velocity.z, iz * moveSpeed, input);

        body.velocity = velocity;
    }

    void UpdateJump() {

        jumpCoolDown = jumpCoolDown - Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCoolDown <=0) {
            jumpCoolDown = 1.5f;

            Vector3 velocity = body.velocity;
            velocity.y = jumpSpeed;

            body.velocity = velocity;
            body.angularVelocity = Random.onUnitSphere * jumpRandomAngularSpeed;
        }
    }

    void Update() {
        
        UpdateMove();
        UpdateJump();
    }
}
