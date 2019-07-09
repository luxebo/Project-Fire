// Script for controlling the player's movement.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement1 : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;
    public bool cameraRelative;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float rotationInput;
    private float rotateSpeedInUpdates;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.freezeRotation = true;
        rotateSpeedInUpdates = rotateSpeed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveDirection = moveInput;
        // Camera relative movement
        if (cameraRelative)
        {
            Camera camera = Camera.main;

            Vector3 camForward = camera.transform.forward;
            Vector3 camRight = camera.transform.right;
            // Project camera onto horizontal plane
            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;

            moveDirection = camForward * moveInput.z + camRight * moveInput.x;
        }
        moveVelocity = Vector3.ClampMagnitude(moveDirection * moveSpeed, moveSpeed); // Ensure that velocity magnitude is no greater than moveSpeed

        if (moveInput != Vector3.zero)
        {
            NavMeshAgent player = GetComponent<NavMeshAgent>();
            player.isStopped = true;
        }

        // Rotation
        rotationInput = Input.GetAxisRaw("Horizontal2");
        

    }

    void FixedUpdate() {
        // Set relative to Space.world for movement independent of rotation.
        // Set relative to Space.Self for movement based on rotation.
        transform.Translate(moveVelocity, Space.World);
        myRigidbody.velocity = moveVelocity;

        // apply rotation
        transform.Rotate(transform.up * rotationInput * rotateSpeedInUpdates);
        

        // Bugged, idk if we need rotation in the direction the player moves. Also is hard to test with cube.
        // Should copy functionality of the navmesh (right click to move).
        /*
        // 10, 0 = right
        if (Mathf.Sign(moveVelocity[0]) == 1 && moveVelocity[0] != 0 && !(transform.eulerAngles.y < 91f && transform.eulerAngles.y > 89f))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        // -10, 0 = left
        if (Mathf.Sign(moveVelocity[0]) == -1 && moveVelocity[0] != 0 && !(transform.eulerAngles.y < -91f && transform.eulerAngles.y > -89f))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        // 0, 10 = up
        if (Mathf.Sign(moveVelocity[1]) == 1 && moveVelocity[1] != 0 && !(transform.eulerAngles.y < 1f && transform.eulerAngles.y > -1f))
        {
            transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
        }
        // 0, -10 = down
        if (Mathf.Sign(moveVelocity[1]) == -1 && moveVelocity[1] != 0 && !(transform.eulerAngles.y < 181f && transform.eulerAngles.y > 179f))
        {
            transform.Rotate(Vector3.down, -rotateSpeed * Time.deltaTime);
        }
        */
    }
}   