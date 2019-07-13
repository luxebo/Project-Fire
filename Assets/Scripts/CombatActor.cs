// A CombatActor is an object that has HP.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatActor : MonoBehaviour {
    public int maxHealth;
    public int regen;
    public bool pooled;
    private bool alive;
    private int currentHealth;
    private float timer = 0.0f;
    private float prevTime = 1.0f;
    // Use this for initialization
    protected virtual void Start() {
        currentHealth = maxHealth;
        alive = true;
    }

    protected void OnEnable()
    {
        currentHealth = maxHealth;
        alive = true;
        timer = 0.0f;
        prevTime = 1.0f;
    }

    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Min(maxHealth, Mathf.Max(value, 0)); }
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        timer += Time.deltaTime;
        if (regen != 0 && currentHealth < maxHealth)
        {
            if (timer > prevTime)
            {
                prevTime += 1.0f;
                currentHealth = currentHealth + regen <= maxHealth ? currentHealth + regen : maxHealth; // Do not overheal
                print(currentHealth);
            }
        }
        if (alive == false)
        {
            die();
        }
	}


    protected virtual void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            alive = false;
        }
    }

    // Derived classes can override; for example, might not want to destroy the player object
    protected virtual void die()
    {
        print("CombatActor died\n");
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene("Dead");
        }
        if (pooled)
            ObjectPooler.objectPool.returnPooledObject(gameObject);
        else
            Destroy(this.gameObject);
    }
}
