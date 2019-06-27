// Must be attached to a gameObject with a collider; damages CombatActors that collide.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public int damage;
    public bool canHurtSelf;

    // Damage enemies that enter this hitbox. May want to change to OnTriggerStay if
    // we want hitboxes with continuous damage.
    private void OnTriggerEnter(Collider other)
    {
        if (!canHurtSelf && other.gameObject == this.gameObject)
        {
            return;
        }
        CombatActor enemy = other.gameObject.GetComponent<CombatActor>();
        if (enemy != null)
        {
            enemy.health -= damage;
            // for debugging
            print(enemy.health);
        }
    }
}
