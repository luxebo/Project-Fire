using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    public GameObject player;
    private CombatActor player_ca;
    private Slider bar;
	// Use this for initialization
	void Start () {
        player_ca = player.GetComponent<CombatActor>();
        bar = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        bar.value = player_ca.Health;
	}
}
