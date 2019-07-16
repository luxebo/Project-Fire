using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HotkeysSettings {

    public string[] hotkeys;
    public int count;

    public HotkeysSettings(string[] buttons, int size)
    {
        count = size;
        hotkeys = buttons;
    }

    public void changeHotkey(string current, string change)
    {
        for (int i = 0; i < count; i++)
        {
            if (hotkeys[i] == current)
            {
                hotkeys[i] = change;
            }
        }
    }

    public void swapHotkeys(string current1, string current2, string change)
    {
        for (int i = 0; i < count; i++)
        {
            if (hotkeys[i] == current1)
            {
                hotkeys[i] = change;
            }
            else if (hotkeys[i] == current2)
            {
                hotkeys[i] = current1;
            }
        }
    }

    public KeyCode loadHotkeySpecific(int i)
    {
        string keycode = hotkeys[i];
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keycode);
    }
}
