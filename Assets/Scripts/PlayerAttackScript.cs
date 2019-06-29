// Script that controls activating player attacks.
// We could probably improve this to be more general
// so we don't have to use the derived class as the variable type.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {
    private KeyCode keybind1 = KeyCode.Z;
    private KeyCode keybind2 = KeyCode.X;
    private KeyCode keybind3 = KeyCode.C;

    private bool keyPressed1 = false;
    private bool keyPressed2 = false;
    private bool keyPressed3 = false;

    private ExampleAttack attack1;
    private AttackWithRecoilDamage attack2;
    private AttackThatCanHitSelf attack3;

    void Start()
    {
        attack1 = GetComponent<ExampleAttack>();
        attack2 = GetComponent<AttackWithRecoilDamage>();
        attack3 = GetComponent<AttackThatCanHitSelf>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(keybind1))
        {
            keyPressed1 = true;
        }
        if (Input.GetKeyDown(keybind2))
        {
            keyPressed2 = true;
        }
        if (Input.GetKeyDown(keybind3))
        {
            keyPressed3 = true;
        }
    }

    void FixedUpdate()
    {
        if (keyPressed1 && attack1 != null)
        {
            attack1.attack();
        }
        keyPressed1 = false;
        if (keyPressed2 && attack2 != null)
        {
            attack2.attack();
        }
        keyPressed2 = false;
        if (keyPressed3 && attack3 != null)
        {
            attack3.attack();
        }
        keyPressed3 = false;
    }
}
