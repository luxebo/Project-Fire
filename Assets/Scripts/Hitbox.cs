// A Hitbox deals damage to CombatActors that enter.
// They last roughly the specified number of seconds, and may or may not hurt the user.
// Must be attached to a gameObject with a collider, which defines the bounding box of the hitbox.

//TODO: Expand friendly fire mechanics; i.e. enemy attacks shouldn't normally hit other enemies.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public GameObject user;
    public float damage;
    public float lifetimeSeconds; // How many seconds will this hitbox last? (Set negative if never disappears)
    public bool canHurtSelf; // Is the hitbox capabe of hurting the user?
    public bool reusable; // If true, do not return the attached object to pool
    private int lifetimeUpdates; // How many updates will this last? (rounded up)
    private int currentLifetime = 0;

    protected virtual void Start()
    {
        convertLifetime();
        reset();
    }


    public static GameObject createHitbox(GameObject user, float damage, float lifetime, bool canHitSelf, bool reusable)
    {
        // Get a pooled hitbox
        GameObject newHitboxObj = ObjectPooler.objectPool.getPooledObject("Hitbox");
        newHitboxObj.transform.parent = null;
        Hitbox hitbox = newHitboxObj.GetComponent<Hitbox>();
        hitbox.reinitialize(user, damage, lifetime, canHitSelf, reusable);
        newHitboxObj.SetActive(true);
        return newHitboxObj;
    }

    // Reinitialize variables, should be used if repurposing a hitbox.
    public virtual void reinitialize(GameObject u, float dam, float lifetime, bool hurtSelf, bool reuse)
    {
        user = u;
        damage = dam;
        lifetimeSeconds = lifetime;
        canHurtSelf = hurtSelf;
        reusable = reuse;

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
            enemy.Health -= Mathf.FloorToInt(damage);
            // for debugging
            print(enemy.Health);
        }
        /**
         * // Commenting out this code b/c it means hitboxes colliding also damages user

        else
        {
            CombatActor dmg = other.gameObject.GetComponentInParent<CombatActor>();
            if (dmg != null)
            {
                dmg.health -= Mathf.FloorToInt(damage);
                print(dmg.health);
            }
        }
        **/
        
    }

    // Hitboxes handle their own lifetime. When lifetime runs out, they deactivate themselves.

    protected virtual void FixedUpdate()
    {
        if (currentLifetime > 0)
        {
            currentLifetime--;
        }
        else if (currentLifetime == 0)
        {
            if (reusable == true)
            {
                this.gameObject.SetActive(false);
                this.reset();
            }
            else
            {
                ObjectPooler.objectPool.returnPooledObject(this.gameObject);
            }
        }
    }
}
