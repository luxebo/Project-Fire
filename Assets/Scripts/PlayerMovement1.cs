// Script for controlling the player's movement.
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    HotkeysSettings hk;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.freezeRotation = true;
        rotateSpeedInUpdates = rotateSpeed * Time.fixedDeltaTime;
        hk = Hotkeys.loadHotkeys();
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
        SerializedProperty hPos = GetChildProperty(axesProperty, "m_Name", "Horizontal", true);
        SerializedProperty hNeg = GetChildProperty(axesProperty, "m_Name", "Horizontal", false);
        SerializedProperty vPos = GetChildProperty(axesProperty, "m_Name", "Vertical", true);
        SerializedProperty vNeg = GetChildProperty(axesProperty, "m_Name", "Vertical", false);
        SerializedProperty h2Pos = GetChildProperty(axesProperty, "m_Name", "Horizontal2", true);
        SerializedProperty h2Neg = GetChildProperty(axesProperty, "m_Name", "Horizontal2", false);
        hPos.stringValue = hk.loadHotkeyTranslated(2);
        hNeg.stringValue = hk.loadHotkeyTranslated(3);
        vPos.stringValue = hk.loadHotkeyTranslated(0);
        vNeg.stringValue = hk.loadHotkeyTranslated(1);
        h2Pos.stringValue = hk.loadHotkeyTranslated(5);
        h2Neg.stringValue = hk.loadHotkeyTranslated(4);
        serializedObject.ApplyModifiedProperties();
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
    }

    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name, string stringVal, bool positive)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name && child.stringValue == stringVal && positive)
            {
                child.Next(false);
                child.Next(false);
                child.Next(false);
                child.Next(false);
                return child; //Return positive button
            }
            else if (child.name == name && child.stringValue == stringVal && !positive)
            {
                child.Next(false);
                child.Next(false);
                child.Next(false);
                return child; //Return negative button
            }
        }
        while (child.Next(true));
        return null;
    }
}   