// A basic example attack.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAttack : Attack {
    private GameObject hitboxObj;

    // Must initialize hitbox factory manually if not supplied.
    protected override void Start()
    {
        base.Start();
        hitboxObj = Hitbox.createHitbox(originObject, 10f, .33f, false, true);
        hitboxObj.transform.SetParent(originObject.transform, false);
        hitboxObj.transform.localScale = new Vector3(.5f, .7f, 2f);
        hitboxObj.transform.localPosition = new Vector3(1.05f, 0);
        hitboxObj.SetActive(false);
    }


    protected override void attackAction()
    {
        hitboxObj.SetActive(true);
    }
}
