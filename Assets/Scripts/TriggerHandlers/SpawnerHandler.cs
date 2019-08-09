using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Basic TriggerHandler for ObjectSpawners.
 * Just sets the ObjectSpawners to active when activated, and sets inactive when deactivated.
 */
public class SpawnerHandler : TriggerHandler {

    List<GameObject> spawners;
	// Use this for initialization
	protected override void Start () {
        spawners = new List<GameObject>();
        // Get all immediate children objects that are ObjectSpawners
        foreach (Transform t in gameObject.transform)
        {
            if (t.gameObject.GetComponent<ObjectSpawner>() != null)
            {
                spawners.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }
        }
	}

    public override void activate()
    {
        foreach(GameObject obj in spawners)
        {
            obj.GetComponent<ObjectSpawner>().resetTimer();
            obj.SetActive(true);
        }
    }

    public override void deactivate()
    {
        foreach (GameObject obj in spawners)
        {
            obj.SetActive(false);
        }
    }
}
