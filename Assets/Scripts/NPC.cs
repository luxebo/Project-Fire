using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing NPCs. Provides a more structured and flexible way of defining NPCs.
public class NPC : CombatActor {
    Attack attack;
    AIMovement movement;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        attack = GetComponent<Attack>();
        movement = GetComponent<AIMovement>();
	}
	
    // Set an Attack component as the current attack. Adds component if not already added.
    public void SetAttack<T>() where T : Attack
    {
        Attack nextAttack = GetComponent<T>();
        if (nextAttack != null)
        {
            attack.enabled = false;
            attack = nextAttack;
            attack.enabled = true;
        }
        else
        {
            attack.enabled = false;
            attack = gameObject.AddComponent<T>();
        }
        
    }
    // Set an AIMovement component as the current movement behavior. Adds component if not already added.
    public void SetMovement<T>() where T : AIMovement
    {
        AIMovement nextMovement = GetComponent<T>();
        if (nextMovement != null)
        {
            movement.enabled = false;
            movement = nextMovement;
            movement.enabled = true;
        }
        else
        {
            movement.enabled = false;
            movement = gameObject.AddComponent<T>();
        }

    }


    // Update is called once per frame
    protected override void Update () {
        if (attack != null)
            attack.attack();
        if (movement != null)
            movement.move();
        base.Update();
	}
}
