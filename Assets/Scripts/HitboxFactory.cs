// A HitboxFactory allows for the creation of new Hitbox prefab objects on the fly.
// This should probably always be accessed via a HitboxFactory prefab.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxFactory : MonoBehaviour {
    public GameObject hitboxPrefab;
	
    // Creates a hitbox with specified parameters, no parent, at position (0,0,0) and scale (1,1,1)
	public GameObject createHitbox(GameObject user, float damage, float lifetime, bool canHitSelf)
    {
        GameObject newHitboxObj = Instantiate(hitboxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newHitboxObj.transform.parent = null;
        Hitbox hitbox = newHitboxObj.GetComponent<Hitbox>();
        hitbox.reinitialize(user, damage, lifetime, canHitSelf);
        return newHitboxObj;
    }
}
