using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    
    private KeyCode callDialogue = KeyCode.Q;
    private KeyCode continueDialogue = KeyCode.Return;
    //Some dialogue can be triggered by an event. If not triggered, player has to press F near the object (or whatever key).
    //Let's say an enemy approaches and we want dialogue to appear, this will be triggered by an event rather
    //than the player having to push F AND be next to the object of dialogue to see this dialogue.
    public bool triggered = false;
    //If paused or not.
    private bool paused = false;
    //Player to disable during dialogue.
    public GameObject player;
    //How far to detect the object of dialogue.
    public float range = 3f;
    //List of dialogue statements: set in editor.
    [TextArea]
    public string[] dialogue;
    //Which dialogue statement to say.
    private int index = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
        }
        if (!paused && !triggered && playerInRange())
        {
            if (Input.GetKeyDown(callDialogue) && index == 0)
            {
                Time.timeScale = 0f;
                player.SetActive(false);
                print(dialogue[index]);
                index += 1;
            }
            else if (Input.GetKeyDown(continueDialogue) && index != 0 && index < dialogue.Length)
            {
                print(dialogue[index]);
                index += 1;
            }
            else
            {
                Time.timeScale = 1f;
                player.SetActive(true);
            }
        }
        else if (!paused && triggered)
        {
            if (index == 0)
            {
                Time.timeScale = 0f;
                print(dialogue[index]);
                index += 1;
            }
            else if (Input.GetKeyDown(continueDialogue) && index != 0 && index < dialogue.Length)
            {
                print(dialogue[index]);
                index += 1;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    bool playerInRange()
    {
        return true;
    }
}
