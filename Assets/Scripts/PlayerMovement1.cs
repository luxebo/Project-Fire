// Script for controlling the player's movement.
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

    [SerializeField]
    private bool canMove;
    [SerializeField]
    private Vector3 moveInput;
    [SerializeField]
    private Vector3 moveVelocity;
    private Vector3 mouseWorldPosition;
    private Plane playerPlane;
    private Vector3 lastMousePosition;
    private NavMeshAgent playerAgent;

    private KeyCode up = KeyCode.W;
    private KeyCode down = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode dash = KeyCode.LeftShift;
    private KeyCode click = KeyCode.Mouse1;
    HotkeysSettings hk;

    bool is_moving;
    public bool isMoving
    {
        get { return is_moving; }
    }

    // Use this for initialization
    void Start()
    {
        hk = Hotkeys.loadHotkeys();
        up = hk.loadHotkeySpecific(0);
        down = hk.loadHotkeySpecific(1);
        left = hk.loadHotkeySpecific(2);
        right = hk.loadHotkeySpecific(3);
        dash = hk.loadHotkeySpecific(10);
        click = hk.loadHotkeySpecific(9);
        //myCharacterController = GetComponent<CharacterController>();
        canMove = true;

        playerAgent = GetComponent<NavMeshAgent>();
        playerPlane = new Plane(transform.up, transform.position); // Need plane for raycasting
        lastMousePosition = Vector3.zero;

        playerAgent.speed = moveSpeed;

        is_moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // reset
        moveInput = Vector3.zero;
        moveVelocity = Vector3.zero;
        
        if (Input.GetKeyDown(click)) // Process click movement.
        {
            RaycastHit hit;
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            playerAgent.isStopped = false;
            if (Physics.Raycast(pos, out hit, 1000))
            {
                playerAgent.destination = hit.point;
            }
        }
        else // Get movement input. No movement along an axis if opposite directions pressed at same time.
        {
            if (Input.GetKey(up) && !Input.GetKey(down))
            {
                print("up");
                moveInput.z = 1;
            }
            else if (!Input.GetKey(up) && Input.GetKey(down))
            {
                print("down");
                moveInput.z = -1;
            }

            if (Input.GetKey(left) && !Input.GetKey(right))
            {
                print("left");
                moveInput.x = -1;
            }
            else if (!Input.GetKey(left) && Input.GetKey(right))
            {
                print("right");
                moveInput.x = 1;
            }
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
            playerAgent.isStopped = true;
        }

        // Dashing
        if (canMove && Input.GetKeyDown(dash))
        {
            StartCoroutine(dashAction(moveVelocity));
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

        // move
        if (canMove && moveVelocity != Vector3.zero)
        {
            playerAgent.velocity = moveVelocity;
        }

        // is player moving?
        is_moving = (playerAgent.velocity != Vector3.zero);
    }



    // The object this is attached to moves faster in a single direction for a specified amount of updates,
    // then slows down for a specified amount of updates.
    IEnumerator dashAction(Vector3 moveVel)
    {
        Vector3 dashVelocity = moveVel != Vector3.zero? moveVel: 
            Vector3.ClampMagnitude(gameObject.transform.forward * moveSpeed, moveSpeed);
        canMove = false; // Prevent normal movement during a dash.
        print("dashing");
        // Dashing
        for(int i =0; i< dashUpdates; i++)
        {
            yield return null;
            playerAgent.velocity = dashVelocity * dashSpeedMultiplier;
        }
        // Cooldown
        for(int i =0; i< dashCooldownUpdates; i++)
        {

            yield return null;
            playerAgent.velocity = dashVelocity * dashCooldownMultiplier;
        }
        yield return null; // Make sure that multiple calls to move are not allowed in one update.
        canMove = true; // reenable normal movement
        print("end dashing");
    }

    


}   