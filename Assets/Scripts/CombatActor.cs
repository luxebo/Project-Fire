// A CombatActor is an object that has HP.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatActor : MonoBehaviour {
    public int health;
    public int regen;
    private bool alive;
    private int max;
    private float timer = 0.0f;
    private float prevTime = 1.0f;
	// Use this for initialization
	void Start () {
        max = health;
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (health < max)
        {
            if (timer > prevTime)
            {
                prevTime += 1.0f;
                health += regen;
                print(health);
            }
        }
		if (alive == false)
        {
            die();
        }
	}

    void FixedUpdate()
    {
        if (health <= 0)
        {
            alive = false;
        }
    }

    // Derived classes can override; for example, might not want to destroy the player object
    void die()
    {
        print("CombatActor died\n");
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene("Dead");
        }
        Destroy(this.gameObject);
    }
}
