using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMove : MonoBehaviour {

    private KeyCode keybind1 = KeyCode.Mouse1;
    private NavMeshAgent player;
    HotkeysSettings hk;

    // Use this for initialization
    void Start () {
        player = GetComponent<NavMeshAgent>();
        hk = Hotkeys.loadHotkeys();
        keybind1 = hk.loadHotkeySpecific(11);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(keybind1))
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
