using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    public float moveSpeed;
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
        moveVelocity = moveInput * moveSpeed;

        if (Input.GetAxisRaw("Horizontal") >= 1 && Input.GetAxisRaw("Vertical") >= 1)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal") / Mathf.Sqrt(2), Input.GetAxisRaw("Vertical") /Mathf.Sqrt(2));
        }
    }

    void FixedUpdate() {
        transform.Translate(moveVelocity[0], 0, moveVelocity[1]);
        myRigidbody.velocity = moveVelocity;
    }
}   