// An example attack that creates a hitbox that damages the user if they enter.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThatCanHitSelf : Attack {


    // Must initialize hitbox factory manually if not supplied.
    protected override void Start()
    {
        base.Start();

    }


    protected override void attackAction()
    {
        GameObject hitboxObj = Hitbox.createHitbox(originObject, 25f, 2.5f, true, false);
        // reposition hitbox
        hitboxObj.transform.SetParent(originObject.transform, false);
        hitboxObj.transform.localScale = new Vector3(1f, 1f, 1f);
        hitboxObj.transform.localPosition = new Vector3(1.05f, 0);

        hitboxObj.SetActive(true);
        // Detach hitbox so it does not move with the origin object.
        hitboxObj.transform.SetParent(null, true);
    }
}
