using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {

    // Use this for initialization
    Text txt;
    Button pressable;
    private bool editable = false;
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (editable)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey) && !(Input.GetKeyDown(KeyCode.Escape)))
                {
                    int check = checkSameKey(vKey);
                    if (check == -1)
                    {
                        txt.text = vKey.ToString();
                    }
                    else
                    {
                        Transform buttons = pressable.transform.parent;
                        Transform but = buttons.transform.GetChild(check);
                        Text txt2 = but.GetComponentInChildren<Text>();
                        txt2.text = txt.text;
                        txt.text = vKey.ToString();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    editable = false;
                    pressable = null;
                    break;
                }
            }
        }
    }

    public void replaceHotkey(Button button)
    {
        if (pressable == null)
        {
            txt = button.GetComponentInChildren<Text>();
            editable = true;
            pressable = button;
        }
    }

    private int checkSameKey(KeyCode vKey)
    {
        Transform buttons = pressable.transform.parent;
        for (int i = 0; i < buttons.childCount; ++i)
        {
            Transform but = buttons.transform.GetChild(i);
            string txt2 = but.GetComponentInChildren<Text>().text;
            if (txt2 == vKey.ToString())
            {
                return i;
            }
        }
        return -1;
    }
}
