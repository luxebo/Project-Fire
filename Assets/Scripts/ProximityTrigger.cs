using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ProximityTrigger activates all TriggerHandler components that are attached to immediate children
// of this object or attached to objects in the array when the player enters.
public class ProximityTrigger : MonoBehaviour {
    [SerializeField]
    GameObject[] otherHandlers;
    List<TriggerHandler> handlers;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        handlers = new List<TriggerHandler>();
        // Add TriggerHandlers from children
        foreach (Transform t in gameObject.transform)
        {
            TriggerHandler handler = t.gameObject.GetComponent<TriggerHandler>();
            if(handler != null)
            {
                handlers.Add(handler);
            }
        }
        // Add TriggerHandlers from array
        foreach (GameObject obj in otherHandlers)
        {
            TriggerHandler handler = obj.GetComponent<TriggerHandler>();
            if (handler != null)
            {
                handlers.Add(handler);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
