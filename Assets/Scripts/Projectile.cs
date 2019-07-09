// A projectile is a Hitbox that handles its own movement. A hitbox will move in 
// the direction of the specified velocity, up to a certain range. Like hitboxes,
// the transform of the projectile is not handled by the projectile, and must be
// manually performed.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hitbox {
    public float range;
    public Vector3 startVelocity; // Per second
    private float rangeTraveled;
    private Vector3 velocityPerUpdate;
    

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

    // Get a GameObject with an attached Projectile component.
    public static GameObject createProjectile(GameObject u, float dam, float lifetime, bool hurtSelf, float range, Vector3 vel)
    {
        GameObject newHitboxObj = ObjectPooler.objectPool.getPooledObject("Projectile");
        newHitboxObj.transform.parent = null;
        Projectile hitbox = newHitboxObj.GetComponent<Projectile>();
        hitbox.reinitialize(u, dam, lifetime, hurtSelf, false);
        hitbox.setVelocity(vel);
        hitbox.range = range;
        newHitboxObj.SetActive(true);
        return newHitboxObj;
    }

    // Resets lifetime and range traveled.
    public override void reset()
    {
        base.reset();
        rangeTraveled = 0f;
    }

    public void setVelocity(Vector3 newVelocity)
    {
        startVelocity = newVelocity;
        convertVelocity();
    }

    // Convert the velocity per second to updates.
    private void convertVelocity()
    {
        velocityPerUpdate = startVelocity * Time.fixedDeltaTime;
    }
	
	// Update is called once per frame
	protected override void FixedUpdate () {
        base.FixedUpdate();
        if (rangeTraveled < range)
        {
            // Determine how many units to move
            Vector3 actualVelocity = (rangeTraveled + velocityPerUpdate.magnitude) < range ?
                velocityPerUpdate : Vector3.ClampMagnitude(velocityPerUpdate, range - rangeTraveled);
            gameObject.transform.position += velocityPerUpdate;
            // keep track of units traveled
            rangeTraveled += actualVelocity.magnitude;
        }
        
	}
}
