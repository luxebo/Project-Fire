using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// When activated, disables children objects. Optionally, can re-enable children objects when handler deactivated.
public class SelfDisableHandler : TriggerHandler {
    [SerializeField]
    bool enableOnLeave;
    // Use this for initialization

    public override void activate()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    public override void deactivate()
    {
        if (enableOnLeave)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
