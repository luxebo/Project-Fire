using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {

    // Use this for initialization
    Text txt;
    Button pressable;
    HotkeysSettings hk;
    private bool editable = false;
	void Start () {
        hk = loadHotkeys();
        GameObject buttonObj = GameObject.Find("Buttons");
        Transform buttons = buttonObj.transform;
        for (int i = 0; i < buttons.childCount; ++i)
        {
            Transform but = buttons.transform.GetChild(i);
            Text txt2 = but.GetComponentInChildren<Text>();
            txt2.text = hk.hotkeys[i];
        }
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
                        hk.changeHotkey(txt.text, vKey.ToString());
                        txt.text = vKey.ToString();
                    }
                    else
                    {
                        Transform buttons = pressable.transform.parent;
                        Transform but = buttons.transform.GetChild(check);
                        Text txt2 = but.GetComponentInChildren<Text>();
                        hk.swapHotkeys(txt.text, txt2.text, vKey.ToString());
                        txt2.text = txt.text;
                        txt.text = vKey.ToString();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    editable = false;
                    pressable = null;
                    saveHotkeys(hk);
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

    public static void saveHotkeys (HotkeysSettings hk)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/hotkeys";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, hk);
        stream.Close();
    }

    public static HotkeysSettings loadHotkeys()
    {
        string path = Application.persistentDataPath + "/hotkeys";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            HotkeysSettings hk = formatter.Deserialize(stream) as HotkeysSettings;
            stream.Close();
            return hk;
        }
        else //file not created yet
        {
            GameObject buttonObj = GameObject.Find("Buttons");
            Transform buttons = buttonObj.transform;
            string[] hotkeys = new string[buttons.childCount];
            for (int i = 0; i < buttons.childCount; i++)
            {
                Transform but = buttons.transform.GetChild(i);
                string txt2 = but.GetComponentInChildren<Text>().text;
                hotkeys[i] = txt2;
            }
            HotkeysSettings hk = new HotkeysSettings(hotkeys, buttons.childCount);
            saveHotkeys(hk);
            return hk;
        }
    }
}
