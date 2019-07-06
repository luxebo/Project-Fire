using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement1 : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;
    private Rigidbody myRigidbody;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),  Input.GetAxisRaw("Vertical"));
        moveVelocity = Vector2.ClampMagnitude(moveInput * moveSpeed, moveSpeed); // Ensure that velocity is no greater than moveSpeed
        if (Input.GetAxisRaw("Horizontal") >= 1 && Input.GetAxisRaw("Vertical") >= 1)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal") / Mathf.Sqrt(2), Input.GetAxisRaw("Vertical") /Mathf.Sqrt(2));
        }
        if (moveInput != Vector2.zero)
        {
            NavMeshAgent player = GetComponent<NavMeshAgent>();
            player.isStopped = true;
        }
    }

    void FixedUpdate() {
        transform.Translate(moveVelocity[0], 0, moveVelocity[1]);
        myRigidbody.velocity = moveVelocity;
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