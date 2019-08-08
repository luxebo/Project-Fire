using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    private KeyCode keybind1 = KeyCode.E;
    public GameObject player;
    public float range = 5f;
    //Determines what type of item this is.
    public string typeOfItem = "Health";
    //Whether user has to use a key press to pick up.
    public bool interactable = false;
    private CombatActor player_ca;
    private PlayerData player_data;

    // Use this for initialization
    void Start()
    {
        player_ca = player.GetComponent<CombatActor>();
        player_data = player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Items that are auto-pickup are done like:
		if (playerInRange() && (typeOfItem == "Health"))
        {
            player_ca.Health += 100;
            Destroy(this.gameObject);
        }
        if (playerInRange() && (typeOfItem == "Coins"))
        {
            player_data.coins += (10 * (int)player_data.goldMultiplier);
            Destroy(this.gameObject);
        }
        //Items that affect more than basic stuff and require interacting will be done like:
        if (playerInRange() && (typeOfItem == "Midas") && interactable)
        {
            if (Input.GetKeyDown(keybind1))
            {
                player_data.midas = true;
                Destroy(this.transform.parent.gameObject);
            }
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
