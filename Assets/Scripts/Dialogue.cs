using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    
    private KeyCode callDialogue = KeyCode.Q;
    private KeyCode continueDialogue = KeyCode.Return;
    HotkeysSettings hk;
    //Some dialogue can be triggered by an event. If not triggered, player has to press F near the object (or whatever key).
    //Let's say an enemy approaches and we want dialogue to appear, this will be triggered by an event rather
    //than the player having to push F AND be next to the object of dialogue to see this dialogue.
    public bool triggered = false;
    //Can the user only interact with this dialogue once?
    public bool disableAfterTalk = true;
    //Player to disable during dialogue.
    public GameObject player;
    //Canvas to enable during dialogue.
    public GameObject dialogueCanvas;
    //How far to detect the object of dialogue.
    public float range = 5f;
    //List of dialogue statements: set in editor.
    [TextArea]
    public string[] dialogue;
    //Which dialogue statement to say.
    private int index = 0;
    //If paused or not.
    private bool paused = false;
    //Text to change.
    private Text textCanvas;

    void Start()
    {
        textCanvas = dialogueCanvas.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        hk = Hotkeys.loadHotkeys();
        callDialogue = hk.loadHotkeySpecific(11);
        continueDialogue = hk.loadHotkeySpecific(12);
    }

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
        if (!paused && !triggered)
        {
            if (Input.GetKeyDown(callDialogue) && index == 0 && textCanvas.text == "Button")
            {
                startDialogue();
            }
            else if (Input.GetKeyDown(continueDialogue) && index != 0 && index < dialogue.Length)
            {
                textCanvas.text = dialogue[index];
                index += 1;
            }
            else if (Input.GetKeyDown(continueDialogue) && index == dialogue.Length)
            {
                index += 1;
            }
            else if (index > dialogue.Length)
            {
                endDialogue();
            }
        }
        else if (!paused && triggered)
        {
            if (index == 0 && textCanvas.text == "Button")
            {
                startDialogue();
            }
            else if (Input.GetKeyDown(continueDialogue) && index != 0 && index < dialogue.Length)
            {
                textCanvas.text = dialogue[index];
                index += 1;
            }
            else if (Input.GetKeyDown(continueDialogue) && index == dialogue.Length)
            {
                index += 1;
            }
            else if (index > dialogue.Length)
            {
                endDialogue();
            }
        }
    }

    void startDialogue()
    {
        Time.timeScale = 0f;
        playerDisable(false);
        dialogueCanvas.SetActive(true);
        textCanvas.text = dialogue[index];
        index += 1;
    }

    void endDialogue()
    {
        Time.timeScale = 1f;
        playerDisable(true);
        dialogueCanvas.SetActive(false);
        textCanvas.text = "Button";
        if (disableAfterTalk)
        {
            this.enabled = false;
        }
        else
        {
            index = 0;
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

    void playerDisable(bool enable)
    {
        Animator animate = player.GetComponent<Animator>();
        if (enable)
        {
            if(animate != null)
                animate.enabled = true;
            foreach (MonoBehaviour script in player.GetComponents(typeof(MonoBehaviour)))
            {
                script.enabled = true;
            }
        }
        else
        {
            if (animate != null)
                animate.enabled = false;
            foreach (MonoBehaviour script in player.GetComponents(typeof(MonoBehaviour)))
            {

                script.enabled = false;
            }
        }
    }
}
