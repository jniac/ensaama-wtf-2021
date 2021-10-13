using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Force" Unity à ajouter un composant Rigidbody et Grounded lorsque une instance de CubeController est créé.
[RequireComponent(typeof(Rigidbody), typeof(Grounded))]
public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float jumpRandomAngularSpeed = 12f;
    public float jumpAirMax = 1;

    int jumpAirCount = 0;

    Rigidbody body;
    Grounded grounded;

    void Start() {
        body = GetComponent<Rigidbody>();
        // Make sure no gravity applies in the physics loop.
        body.useGravity = false;
        grounded = GetComponent<Grounded>();
    }

    void UpdateMove() {

        Vector3 velocity = body.velocity;

        float ix = Input.GetAxis("Horizontal");
        float iz = Input.GetAxis("Vertical");
        float input = Mathf.Clamp01(Mathf.Sqrt(ix * ix + iz * iz));

        velocity.x = Mathf.Lerp(velocity.x, ix * moveSpeed, input);
        // No gravity on the rigidbody, so gravity applies here;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        velocity.z = Mathf.Lerp(velocity.z, iz * moveSpeed, input);

        body.velocity = velocity;
    }

    void UpdateJump() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            
            bool canJump = grounded.isGrounded || jumpAirCount < jumpAirMax;

            if (grounded.isGrounded) {
                jumpAirCount = 0;
            } else {
                jumpAirCount = jumpAirCount + 1;
            }

            if (canJump) {
                Vector3 velocity = body.velocity;
                velocity.y = jumpSpeed;

                body.velocity = velocity;
                body.angularVelocity = Random.onUnitSphere * jumpRandomAngularSpeed;
            }
        }
    }

    void Update() {
        
        UpdateMove();
        UpdateJump();
    }
}
