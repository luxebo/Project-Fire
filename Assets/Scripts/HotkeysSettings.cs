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

    public string loadHotkeyTranslated(int i)
    {
        KeyCode code = loadHotkeySpecific(i);
        string lowered = code.ToString().ToLower();
        if (((int)code >= 33 && (int)code <= 96) || ((int)code >= 256 && (int)code <= 276) 
            || ((int)code >= 256 && (int)code <= 276) || ((int)code == 280) || ((int)code == 281)
            || ((int)code >= 301 && (int)code <= 329))
        {
            lowered = mappingKeyCode((int)code);
        }
        return lowered;
    }

    private string mappingKeyCode(int code)
    {
        //%, {, }, ~, | not usable
        Dictionary<int, string> stored = new Dictionary<int, string>
        {
            {256, "[0]"},
            {257, "[1]"},
            {258, "[2]"},
            {259, "[3]"},
            {260, "[4]"},
            {261, "[5]"},
            {262, "[6]"},
            {263, "[7]"},
            {264, "[8]"},
            {265, "[9]"},
            {266, "[.]"},
            {267, "[/]"},
            {268, "[*]"},
            {269, "[-]"},
            {270, "[+]"},
            {272, "equals"},
            {271, "enter"},
            {273, "up"},
            {274, "down"},
            {275, "right"},
            {276, "left"},
            {280, "page up"},
            {281, "page down"},
            {48, "0"},
            {49, "1"},
            {50, "2"},
            {51, "3"},
            {52, "4"},
            {53, "5"},
            {54, "6"},
            {55, "7"},
            {56, "8"},
            {57, "9"},
            {45, "-"},
            {61, "="},
            {33, "!"},
            {64, "@"},
            {35, "#"},
            {36, "$"},
            {94, "^"},
            {38, "&"},
            {42, "*"},
            {40, "("},
            {41, ")"},
            {95, "_"},
            {43, "+"},
            {91, "["},
            {93, "]"},
            {96, "`"},
            {59, ";"},
            {39, "'"},
            {92, "\\"},
            {58, ":"},
            {34, "\""},
            {44, ","},
            {46, "."},
            {47, "/"},
            {60, "<"},
            {62, ">"},
            {63, "?"},
            {301, "caps lock"},
            {302, "scroll lock"},
            {303, "right shift"},
            {304, "left shift"},
            {305, "right ctrl"},
            {306, "left ctrl"},
            {307, "right alt"},
            {308, "left alt"},
            {309, "right cmd"},
            {310, "left cmd"},
            {312, "right super"},
            {311, "left super"},
            {313, "alt gr"},
            {315, "help"},
            {316, "print screen"},
            {317, "sys req"},
            {318, "break"},
            {319, "menu"},
            {323, "mouse 0"},
            {324, "mouse 1"},
            {325, "mouse 2"},
            {326, "mouse 3"},
            {327, "mouse 4"},
            {328, "mouse 5"},
            {329, "mouse 6"}
        };
        return stored[code];
    }
}
