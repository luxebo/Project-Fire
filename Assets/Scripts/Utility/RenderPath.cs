using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RenderPath : MonoBehaviour {
    LineRenderer myLine;
    NavMeshAgent myAgent;
	// Use this for initialization
	void Start () {
        myLine = GetComponent<LineRenderer>();
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        NavMeshPath path = myAgent.path;
        myLine.SetPositions(path.corners);
	}
}
