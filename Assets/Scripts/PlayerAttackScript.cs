using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {
    private KeyCode keybind = KeyCode.Z;
    private bool keyPressed = false;
    private Attack attack;

    void Start()
    {
        attack = GetComponent<Attack>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(keybind))
        {
            keyPressed = true;
        }
	}

    void FixedUpdate()
    {
        if (keyPressed && attack != null)
        {
            attack.attack();
        }
        keyPressed = false;
    }
}
