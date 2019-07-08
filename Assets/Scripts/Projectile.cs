using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hitbox {
    public float range;
    public Vector3 startVelocity;
    private float rangeTraveled;
    private Vector3 velocityInUpdates;
    

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

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

    private void convertVelocity()
    {
        velocityInUpdates = startVelocity * Time.fixedDeltaTime;
    }
	
	// Update is called once per frame
	protected override void FixedUpdate () {
        base.FixedUpdate();
        if (rangeTraveled < range)
        {
            //print(rangeTraveled);
            Vector3 actualVelocity = (rangeTraveled + velocityInUpdates.magnitude) < range ?
                velocityInUpdates : Vector3.ClampMagnitude(velocityInUpdates, range - rangeTraveled);
            gameObject.transform.position += velocityInUpdates;
            rangeTraveled += actualVelocity.magnitude;
        }
        
	}
}
