// The GameObject this script is attached to will follow the player, trying to touch them.
// The GameObject will bounce off the player when it touches them.
// The GameObject must have a Rigidbody
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BumpEnemy : MonoBehaviour {
    //public float maxSpeed;
    private GameObject player;
    //private Rigidbody myRigidbody;
    private NavMeshAgent myAgent;
    private bool bounced = false;
    // How many updates to stop moving after bumping so player does not die too fast

    // Michael: I think the solution is to let the enemy stay on the player, but only
    // apply damage every couple seconds instead of constantly. This forces player
    // to kite the melee enemy.
    private int stunned = 40;
    private int stun_timer = 0;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        myAgent.isStopped = false;

        //myRigidbody = gameObject.GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        StartCoroutine(updateDestination());
    }

    // Update is called once per frame
    void Update () {
        Vector3 myPosition = gameObject.transform.position;
        
        if (stun_timer <= 0)
        {
            if (bounced)
            {
                //myRigidbody.MovePosition(Vector3.MoveTowards(myPosition, playerPosition, -15 * maxSpeed));
                myAgent.isStopped = true;
                myAgent.velocity = myAgent.desiredVelocity * -4f;
                bounced = false;
                stun_timer = stunned;
            }
            else
            {
                //print(Vector3.MoveTowards(myPosition, playerPosition, myAgent.speed));
                myAgent.isStopped = false;
                //myAgent.velocity = myAgent.desiredVelocity.normalized * myAgent.speed;
            }
        }
        else
        {
            stun_timer--;
        }
        Vector3[] path = myAgent.path.corners;
        print(string.Format("Player: {0}/ Path: {1}", player.transform.position, path[path.Length-1]));
    }
    


    private void OnCollisionStay(Collision other)
    {
        // Bounce off if touching player
        if (other.gameObject == player && stun_timer == 0)
        {
            print("bumped");
            bounced = true;
        }
    }

    IEnumerator updateDestination()
    {
        int updateDelay = 10;
        int currentUpdate = 0;
        yield return null;
        do
        {
            myAgent.destination = player.transform.position; 
            currentUpdate = updateDelay;
            while(currentUpdate > 0 && gameObject.activeInHierarchy)
            {
                yield return null;
                currentUpdate--;
            }
        }
        while (gameObject.activeInHierarchy);
    }
}

