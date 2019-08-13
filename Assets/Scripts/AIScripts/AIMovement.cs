using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Base class for NPC movement.
 */
public abstract class AIMovement : MonoBehaviour {

	// Use this for initialization
	protected virtual void Start () {
		
	}

    protected virtual void Update()
    {

    }

    public abstract void move();
}
