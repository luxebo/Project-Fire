using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectSpawner : MonoBehaviour {

    public string objectTag;

    [SerializeField]
    private float spawnFrequency; // If negative, never automatically spawn.
    [SerializeField]
    private int spawnLimit;
    private int frequencyInUpdates;

    // Counters
    private int numSpawned;
    private int updatesSinceLastSpawn;

	// Use this for initialization
	void Start () {

        if (!ObjectPooler.objectPool.hasObject(objectTag))
        {
            throw new UnityException(string.Format("No object with the tag {0} exists in the pool", objectTag));
        }
        frequencyInUpdates = spawnFrequency >= 0 ? Mathf.CeilToInt(spawnFrequency / Time.fixedDeltaTime):-1;
        updatesSinceLastSpawn = frequencyInUpdates;
	}

    // Reset spawn timer
    public void resetTimer()
    {
        updatesSinceLastSpawn = frequencyInUpdates;
    }

    // Spawn a new object. Return true if successful, else false
    public bool spawn()
    {
        GameObject newObj = ObjectPooler.objectPool.getPooledObject(objectTag);
        if (newObj != null)
        {
            newObj.SetActive(true);
            newObj.transform.SetParent(gameObject.transform, true);
            //newObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            NavMeshAgent spawnedObjAgent = newObj.GetComponent<NavMeshAgent>();
            if(spawnedObjAgent != null)
                spawnedObjAgent.Warp(gameObject.transform.position);
            else
                newObj.transform.localPosition = Vector3.zero;
            newObj.transform.SetParent(gameObject.transform.parent, true);
            numSpawned++;
            return true;
        }
        return false; 
    }
	
	void FixedUpdate () {
		if(frequencyInUpdates != -1 && numSpawned < spawnLimit)
        {
            if(updatesSinceLastSpawn == frequencyInUpdates)
            {
                // Try to spawn a new object. Keep trying if failed.
                if (spawn())
                {
                    updatesSinceLastSpawn = 0;
                }
            }
            else if (updatesSinceLastSpawn < frequencyInUpdates)
            {
                updatesSinceLastSpawn++;
            }
        }
	}
}
