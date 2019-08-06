using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    HotkeysSettings hk;
    string keybind = "Return";
    Text text;

    // Use this for initialization
    void Start ()
    {
        hk = Hotkeys.loadHotkeys();
        keybind = hk.loadHotkeySpecific(12).ToString();
        text = GetComponent<Text>();
        text.text = "Press " + keybind + " to Continue";
    }
}
