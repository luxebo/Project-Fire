// The GameObject this script is attached to will follow the player, trying to touch them.
// The GameObject will bounce off the player when it touches them.
// The GameObject must have a Rigidbody
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpEnemy : MonoBehaviour {
    public float maxSpeed;
    private GameObject player;
    private Rigidbody myRigidbody;
    private bool bounced = false;
    // How many updates to stop moving after bumping so player does not die too fast
    // Just a hacky fix, better to work on natural acceleration instead.

    // Michael: I think the solution is to let the enemy stay on the player, but only
    // apply damage every couple seconds instead of constantly. This forces player
    // to kite the melee enemy.
    private int stunned = 40;
    private int stun_timer = 0;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 myPosition = gameObject.transform.position;
        Vector3 playerPosition = player.transform.position;
        if (stun_timer <= 0)
        {
            if (bounced)
            {
                myRigidbody.MovePosition(Vector3.MoveTowards(myPosition, playerPosition, -15 * maxSpeed));
                bounced = false;
                stun_timer = stunned;
            }
            else
            {
                myRigidbody.MovePosition(Vector3.MoveTowards(myPosition, playerPosition, maxSpeed));
            }
        }
        else
        {
            stun_timer--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Bounce off if touching player
        if(other.gameObject == player && stun_timer == 0)
        {
            print("bumped");
            bounced = true;
        }
    }
}
