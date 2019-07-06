using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMove : MonoBehaviour {

    private NavMeshAgent player;

	// Use this for initialization
	void Start () {
        player = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            player.isStopped = false;
            if (Physics.Raycast(pos, out hit, 1000))
            {
                player.destination = hit.point;
            }
        }
    }
}
