// Shoot a projectile forward.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack {
    //private GameObject hitboxObj;
    //private Hitbox hitbox;

    protected override void Start()
    {
        base.Start();
    }

    protected override void attackAction()
    {
        float range = 3000f;
        Vector3 velocity = new Vector3(0, 0, range / .75f);
        GameObject hitboxObj = Projectile.createProjectile(originObject, 10f, 5f, false, range, velocity);
        Projectile proj = hitboxObj.GetComponent<Projectile>();
        hitboxObj.transform.SetParent(originObject.transform, false);
        hitboxObj.transform.localScale = new Vector3(.2f, .2f, 2f);
        hitboxObj.transform.localPosition = new Vector3(0, 0, 0.6f);
        hitboxObj.transform.localRotation = Quaternion.identity;
        proj.setVelocity(hitboxObj.transform.forward * velocity.magnitude);
        // Detach projectile so it moves independently of the origin object.
        hitboxObj.transform.SetParent(null, true);
    }

    protected void Update()
    {
        /**
        if (hitboxObj != null)
        {
            float step = 5.0f * Time.deltaTime; // calculate distance to move
            Vector3 target = hitboxObj.transform.localPosition + new Vector3(0, 0, 5.0f);
            hitboxObj.transform.localPosition = Vector3.Lerp(hitboxObj.transform.localPosition, target, step);
        }
        */
    }
}
