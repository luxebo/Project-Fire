// Base class for Attacks. An Attack has a cooldown (that may be 0),
// a method called attackAction(), which does the attack effects (spawns Hitboxes
// and whatever else you want an attack to do), and attack(), which
// should be called by the combatActors.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Attack : MonoBehaviour {
    public float cooldownSeconds; // How long is attack disabled after use?
    public GameObject originObject; // Which object to use for relative position of hitboxes
    public GameObject hitboxFactoryObj; // Allows Attacks to construct their own hitboxes
    protected HitboxFactory hitboxFactory;
    protected int cooldownUpdates; // Cooldown in updates, rounded up
    protected int currentCooldown; // How many updates left until usable
	
    // Use this for initialization
	protected virtual void Start () {
        hitboxFactory = hitboxFactoryObj.GetComponent<HitboxFactory>();
        if(hitboxFactory == null)
        {
            throw new MissingComponentException("hitboxFactoryObj must have HitboxFactory component.");
        }
		if(cooldownSeconds < 0)
        {
            Debug.LogWarning("Cooldown should be non-negative. Negative cooldown is treated as 0.");
        }
        else
        {
            cooldownUpdates = Mathf.CeilToInt(cooldownSeconds / Time.fixedDeltaTime);
        }
        currentCooldown = 0;
        // default origin to the object this is attached to.
        if(originObject == null)
        {
            originObject = gameObject;
        }
	}

    // Calls attackAction and sets cooldown. Probably doesn't
    // need to be overridden.
    public virtual void attack()
    {
        if(currentCooldown == 0)
        {
            currentCooldown = cooldownUpdates;
            attackAction();
        }
    }

    // Children classes must override this method.
    abstract protected void attackAction();

    void FixedUpdate()
    {
        if (currentCooldown > 0)
        {
            currentCooldown--;
        }
    }
}
