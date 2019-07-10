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
        moveVelocity = Vector3.ClampMagnitude(moveDirection * moveSpeed, moveSpeed); // Ensure that velocity is no greater than moveSpeed

        /**
        if (Input.GetAxisRaw("Horizontal") >= 1 && Input.GetAxisRaw("Vertical") >= 1)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal") / Mathf.Sqrt(2),0, Input.GetAxisRaw("Vertical") /Mathf.Sqrt(2));
        }
        */
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
    }
}   