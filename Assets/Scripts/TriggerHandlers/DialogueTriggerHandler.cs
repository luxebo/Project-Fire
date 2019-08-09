using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic TriggerHandler for dialogue. Plays the specified dialogue when activated.
public class DialogueTriggerHandler : TriggerHandler {

    public GameObject dialogueObj;
    Dialogue dialogue;
	// Use this for initialization
	protected override void Start () {
        dialogue = dialogueObj.GetComponent<Dialogue>();
	    if(dialogue == null)
        {
            throw new MissingComponentException("dialogueObj does not have a Dialogue component.");
        }
        dialogue.enabled = false;
	}

    public override void activate()
    {
        dialogue.enabled = true;
    }

    public override void deactivate()
    {
        dialogue.enabled = false;
        if (dialogue.disableAfterTalk)
        {
            gameObject.SetActive(false); // Deactivate the trigger handler so dialogue is not re-called.
        }
    }

}
