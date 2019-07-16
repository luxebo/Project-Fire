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
    private KeyCode keybind4 = KeyCode.V;

    private bool keyPressed1 = false;
    private bool keyPressed2 = false;
    private bool keyPressed3 = false;
    private bool keyPressed4 = false;

    private ExampleAttack attack1;
    private AttackWithRecoilDamage attack2;
    private AttackThatCanHitSelf attack3;
    private Shoot attack4;
    HotkeysSettings hk;

    void Start()
    {
        hk = Hotkeys.loadHotkeys();
        attack1 = GetComponent<ExampleAttack>();
        attack2 = GetComponent<AttackWithRecoilDamage>();
        attack3 = GetComponent<AttackThatCanHitSelf>();
        attack4 = GetComponent<Shoot>();
        keybind1 = hk.loadHotkeySpecific(7);
        keybind2 = hk.loadHotkeySpecific(8);
        keybind3 = hk.loadHotkeySpecific(9);
        keybind4 = hk.loadHotkeySpecific(10);
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
        if (Input.GetKeyDown(keybind4))
        {
            keyPressed4 = true;
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
        if (keyPressed4 && attack4 != null)
        {
            attack4.attack();
        }
        keyPressed4 = false;
    }
}
