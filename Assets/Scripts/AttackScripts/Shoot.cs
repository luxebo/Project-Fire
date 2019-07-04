using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Attack {
    private GameObject hitboxObj;
    private Hitbox hitbox;

    protected override void Start()
    {
        base.Start();
    }

    protected override void attackAction()
    {
        hitboxObj = Hitbox.createHitbox(originObject, 10f, 5f, false, false);
        hitbox = hitboxObj.GetComponent<Hitbox>();
        hitboxObj.transform.SetParent(originObject.transform, false);
        hitboxObj.transform.localScale = new Vector3(.2f, .2f, 2f);
        hitboxObj.transform.localPosition = new Vector3(0, 0, 0.6f);
    }

    protected void Update()
    {
        if (hitboxObj != null)
        {
            float step = 5.0f * Time.deltaTime; // calculate distance to move
            Vector3 target = hitboxObj.transform.localPosition + new Vector3(0, 0, 5.0f);
            hitboxObj.transform.localPosition = Vector3.Lerp(hitboxObj.transform.localPosition, target, step);
        }
    }
}
