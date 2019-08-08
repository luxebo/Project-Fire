using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

    public GameObject player;
    private PlayerData player_data;
    Text coinsText;

	// Use this for initialization
	void Start () {
        coinsText = this.GetComponent<Text>();
        player_data = player.GetComponent<PlayerData>();
    }
	
	// Update is called once per frame
	void Update () {
        coinsText.text = "Coins: " + player_data.coins;
	}
}
