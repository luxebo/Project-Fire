﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAttack : Attack {
    private GameObject hitboxObj;
    private Hitbox hitbox;

    // Must initialize hitbox factory manually if not supplied.
    protected override void Start()
    {
        base.Start();
        hitboxObj = hitboxFactory.createHitbox(this.gameObject, 10f, .33f, false);
        hitbox = hitboxObj.GetComponent<Hitbox>();
        hitboxObj.transform.SetParent(this.gameObject.transform, false);
        hitboxObj.transform.localScale = new Vector3(.5f, .7f, 2f);
        hitboxObj.transform.localPosition = new Vector3(1.05f, 0);
        hitboxObj.SetActive(false);
    }


    protected override void attackAction()
    {
        hitboxObj.SetActive(true);
        hitbox.reset();
    }
}