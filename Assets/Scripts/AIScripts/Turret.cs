using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : AIMovement {
    GameObject player;
    public float rotationPerSecond;
    float rotationRate;
	// Use this for initialization
	protected override void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rotationRate = rotationPerSecond * Time.deltaTime;
	}

    public override void move()
    {
        Vector3 towardsPlayer = (player.transform.position - transform.position).normalized;
        Quaternion rotationTarget = Quaternion.LookRotation(towardsPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, rotationRate);
    }
}
