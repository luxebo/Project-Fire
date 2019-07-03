using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents one type of object to pool
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool; // The minimum amount of this type of item to pool.
    public int maxPoolAmount; // The maximum amount of this type of item to pool. Set to -1 for unbounded.
}

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler objectPool; // The pool for all objects. We do not need to explicitly instantiate one.
    public List<ObjectPoolItem> itemsToPool;
    internal Dictionary<string, Queue<GameObject>> allPools; // Each pool is implemented by a queue.
    internal Dictionary<string, int> objectCount; // How many objects have been created for each pool?

	// Use this for initialization
	void Awake () {
        objectPool = this;
        allPools = new Dictionary<string, Queue<GameObject>>();
        objectCount = new Dictionary<string, int>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            string iTag = item.objectToPool.tag;
            if (iTag == "Untagged" || allPools.ContainsKey(iTag))
            {
                throw new UnityException("Each ObjectPoolItem must have a unique tag!");
            }
            else
            {
                // Create pool for each object type.
                Queue<GameObject> newPool = new Queue<GameObject>();
                for (int i = 0; i< item.amountToPool; i++)
                {
                    newPool.Enqueue(Instantiate(item.objectToPool));
                }
                allPools[iTag] = newPool;
                objectCount[iTag] = newPool.Count;
            }
        }
	}

    // Use the following two methods to get objects from the pool.

    public GameObject getPooledObject(string tag)
    {
        if (!allPools.ContainsKey(tag))
        {
            Debug.LogError(string.Format("No object with the {0} exists in the pool.", tag));
        }
        else
        {
            // If pool still has objects, use those.
            if (allPools[tag].Count > 0)
            {
                return allPools[tag].Dequeue();
            }
            else // If pool was empty, make more objects
            {
                foreach (ObjectPoolItem item in itemsToPool)
                {
                    if (item.objectToPool.tag == tag)
                    {
                        return expandPool(item);
                    }
                }
            }
        }
        return null;
    }

    public List<GameObject> getPooledObject(string tag, int numObjs)
    {
        if (!allPools.ContainsKey(tag))
        {
            Debug.LogError(string.Format("No object with the {0} exists in the pool.", tag));
        }
        else if(numObjs > 0)
        {
            List<GameObject> objs = new List<GameObject>(numObjs);
            // Try to get as many objects from pool as possible.
            int fromPool = 0;
            while(allPools[tag].Count > 0)
            {
                objs.Add(allPools[tag].Dequeue());
                fromPool++;
            }
            // If pool did not have enough objects, make more.
            if (fromPool < numObjs)
            {
                foreach (ObjectPoolItem item in itemsToPool)
                {
                    if (item.objectToPool.tag == tag)
                    {
                        objs = expandPool(item, numObjs - fromPool, objs);
                        break;
                    }
                }
            }
            // Return null if could not add any.
            return objs.Count > 0 ? objs : null;
        }
        else
        {
            throw new UnityException("numObjs must be positive.");
        }
        return null;
    }

    // The following two methods return objects to the pool.

    public void returnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        allPools[obj.tag].Enqueue(obj);
    }

    public void returnPooledObject(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
            allPools[obj.tag].Enqueue(obj);
        }
    }
	
    /**
     * Internal methods
     **/

    internal GameObject expandPool(ObjectPoolItem item)
    {
        string tag = item.objectToPool.tag;
        if (objectCount[tag] < item.maxPoolAmount || item.maxPoolAmount < 0)
        {
            objectCount[tag]++;
            GameObject newObj = Instantiate(item.objectToPool);
            newObj.SetActive(true);
            return newObj;
        }
        return null;
    }

    internal List<GameObject> expandPool(ObjectPoolItem item, int numExpand, List<GameObject> fillList)
    {
        string tag = item.objectToPool.tag;
        // Find up to how many we can add
        int howMany = item.maxPoolAmount < 0? numExpand : 
            Mathf.Max(0, numExpand - Mathf.Max(0, objectCount[tag] + numExpand - item.maxPoolAmount));
        for (int i = 0; i < howMany; i++)
        {
            GameObject newObj = Instantiate(item.objectToPool);
            newObj.SetActive(true);
            fillList.Add(newObj);
        }
        objectCount[tag] += howMany;
            
        return fillList;
    }

}
