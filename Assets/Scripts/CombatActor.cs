// A CombatActor is an object that has HP.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActor : MonoBehaviour {
    public int health;
    private bool alive;
	// Use this for initialization
	void Start () {
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (alive == false)
        {
            die();
        }
	}

    void FixedUpdate()
    {
        if (health <= 0)
        {
            alive = false;
        }
    }

    // Derived classes can override; for example, might not want to destroy the player object
    void die()
    {
        print("CombatActor died\n");
        Destroy(this.gameObject);
    }
}
