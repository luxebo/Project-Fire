using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public string objectTag;

    [SerializeField]
    private float spawnFrequency;
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
        frequencyInUpdates = Mathf.CeilToInt(spawnFrequency / Time.fixedDeltaTime);
	}

    // Spawn a new object. Return true if successful, else false
    bool spawn()
    {
        GameObject newObj = ObjectPooler.objectPool.getPooledObject(objectTag);
        if (newObj != null)
        {
            newObj.SetActive(true);
            newObj.transform.SetParent(gameObject.transform, true);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.SetParent(null, true);
            numSpawned++;
            return true;
        }
        return false; 
    }
	
	void FixedUpdate () {
		if(numSpawned < spawnLimit)
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
