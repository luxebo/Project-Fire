using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public GameObject player;
    public float range = 5f;
    //Determines what type of item this is.
    public string typeOfItem = "Health";
    //Whether user has to use a key press to pick up.
    public bool interactable = false;
    private CombatActor player_ca;

    // Use this for initialization
    void Start()
    {
        player_ca = player.GetComponent<CombatActor>();
    }

    // Update is called once per frame
    void Update ()
    {
		if (playerInRange() && (typeOfItem == "Health"))
        {
            player_ca.Health += 100;
            Destroy(this.gameObject);
        }
	}

    bool playerInRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            return true;
        }
        return false;
    }
}
