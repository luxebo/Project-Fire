// Must be attached to a gameObject with a collider; damages CombatActors that collide.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public GameObject user;
    public float damage;
    public float lifetimeSeconds; // How many seconds will this hitbox last? (Set negative if never disappears)
    public bool canHurtSelf;
    private int lifetimeUpdates; // How many updates will this last? (rounded up)
    private int currentLifetime = 0;

    protected virtual void Start()
    {
        convertLifetime();
        reset();
    }

    // Reinitialize variables, should be used if repurposing a hitbox.
    public virtual void reinitialize(GameObject u, float dam, float lifetime, bool hurtSelf)
    {
        user = u;
        damage = dam;
        lifetimeSeconds = lifetime;
        canHurtSelf = hurtSelf;

        convertLifetime();
        reset();
    }

    // Reset current lifetime. Use when reactivating hitbox.
    public virtual void reset()
    {
        currentLifetime = lifetimeUpdates;
    }

    // Converts lifetime in seconds to updates
    protected virtual void convertLifetime()
    {
        if (lifetimeSeconds < 0)
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!canHurtSelf && other.gameObject == user)
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

    // Default behavior is to deactivate hitbox object when lifetime is 0.

    protected virtual void FixedUpdate()
    {
        if (currentLifetime > 0)
        {
            currentLifetime--;
        }
        
        if (currentLifetime == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
