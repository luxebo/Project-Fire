using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ProximityTrigger activates all TriggerHandler components that are attached to immediate children
// of this object when the player enters.
public class ProximityTrigger : MonoBehaviour {
    List<TriggerHandler> handlers;
    GameObject player;
	// Use this for initialization
	void Start () {
        handlers = new List<TriggerHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform t in gameObject.transform)
        {
            TriggerHandler handler = t.gameObject.GetComponent<TriggerHandler>();
            if(handler != null)
            {
                handlers.Add(handler);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Entered");
        if (other.gameObject == player)
        {
            print("Entered");
            foreach (TriggerHandler t in handlers)
            {
                t.activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
            foreach (TriggerHandler t in handlers)
            {
                t.deactivate();
            }
    }
}
