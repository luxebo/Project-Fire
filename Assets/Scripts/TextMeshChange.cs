using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshChange : MonoBehaviour
{
    HotkeysSettings hk;
    string keybind = "Q";
    TextMesh text;

    // Use this for initialization
    void Start()
    {
        hk = Hotkeys.loadHotkeys();
        keybind = hk.loadHotkeySpecific(11).ToString();
        text = GetComponent<TextMesh>();
        text.text = "Press " + keybind + " to Interact";
    }
}
