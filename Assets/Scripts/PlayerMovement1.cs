﻿// Script for controlling the player's movement.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeedMultiplier;
    public float dashCooldownMultiplier;
    public int dashUpdates;
    public int dashCooldownUpdates;
    public float centerMouseDeadzoneRadius; // Defines a deadzone around the player where mouse movements are not read.
    public bool cameraRelative;


    private bool canMove;
    private CharacterController myCharacterController;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 mouseWorldPosition;
    private Plane playerPlane;
    private Vector3 lastMousePosition;

    private KeyCode up = KeyCode.W;
    private KeyCode down = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode dash = KeyCode.LeftShift;
    HotkeysSettings hk;

    // Use this for initialization
    void Start()
    {
        hk = Hotkeys.loadHotkeys();
        up = hk.loadHotkeySpecific(0);
        down = hk.loadHotkeySpecific(1);
        left = hk.loadHotkeySpecific(2);
        right = hk.loadHotkeySpecific(3);
        dash = hk.loadHotkeySpecific(10);
        myCharacterController = GetComponent<CharacterController>();
        canMove = true;

        playerPlane = new Plane(transform.up, transform.position); // Need plane for raycasting
        lastMousePosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector3.zero;
        // Get movement input. No movement along an axis if opposite directions pressed at same time.
        if (Input.GetKey(up) && !Input.GetKey(down))
        {
            moveInput.z = 1;
        }
        else if (!Input.GetKey(up) && Input.GetKey(down))
        {
            moveInput.z = -1;
        }

        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            moveInput.x = -1;
        }
        else if (!Input.GetKey(left) && Input.GetKey(right))
        {
            moveInput.x = 1;
        }

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
        // Dashing
        if(canMove && Input.GetKeyDown(dash) && moveVelocity != Vector3.zero)
        {
            StartCoroutine(dashAction());
        }

        // Rotate to face mouse position.
        if (Input.mousePosition != lastMousePosition) // Only update when mouse actually moves.
        {
            lastMousePosition = Input.mousePosition;
            bool hit = false;
            playerPlane.SetNormalAndPosition(transform.up, transform.position);
            mouseWorldPosition = MouseUtility.MouseWorldPoint(playerPlane, out hit);
            if (hit && Vector3.Distance(transform.position, mouseWorldPosition) > centerMouseDeadzoneRadius)
            {
                transform.LookAt(mouseWorldPosition);
            }
        }
    }

    void FixedUpdate()
    {
        // Set relative to Space.World for movement independent of rotation.
        // Set relative to Space.Self for movement based on rotation.
        if(canMove)
            myCharacterController.SimpleMove(moveVelocity);


    }

    // The object this is attached to moves faster in a single direction for a specified amount of updates,
    // then slows down for a specified amount of updates.
    IEnumerator dashAction()
    {
        Vector3 dashVelocity = moveVelocity;
        canMove = false; // Prevent normal movement during a dash.
        print("dashing");
        // Dashing
        for(int i =0; i< dashUpdates; i++)
        {
            yield return new WaitForFixedUpdate();
            myCharacterController.SimpleMove(dashVelocity * dashSpeedMultiplier);
        }
        // Cooldown
        for(int i =0; i< dashCooldownUpdates; i++)
        {
            yield return new WaitForFixedUpdate();
            myCharacterController.SimpleMove(dashVelocity * dashCooldownMultiplier);
        }
        canMove = true; // reenable normal movement
        print("end dashing");
    }
    


}   