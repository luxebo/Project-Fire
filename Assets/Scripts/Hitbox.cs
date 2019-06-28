// Must be attached to a gameObject with a collider; damages CombatActors that collide.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public float damage;
    public float lifetimeSeconds; // How many seconds will this hitbox last? (Set negative if never disappears)
    public bool canHurtSelf;
    private int lifetimeUpdates; // How many updates will this last? (rounded up)

    private void Start()
    {
        if(lifetimeSeconds < 0)
        {
            lifetimeUpdates = -1;
        }
        else
        {
            lifetimeUpdates = Mathf.CeilToInt(lifetimeSeconds / Time.fixedDeltaTime);
        }
        
    }

    // Damage enemies that enter this hitbox. May want to change to OnTriggerStay if
    // we want hitboxes with continuous damage.
    private void OnTriggerEnter(Collider other)
    {
        if (!canHurtSelf && other.gameObject == this.gameObject)
        {
            return;
        }
        CombatActor enemy = other.gameObject.GetComponent<CombatActor>();
        if (enemy != null)
        {
            enemy.health -= Mathf.FloorToInt(damage);
            // for debugging
            print(enemy.health);
        }
    }

    void FixedUpdate()
    {
        if (lifetimeUpdates > 0)
        {
            lifetimeUpdates--;
        }
        
        if (lifetimeUpdates == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
