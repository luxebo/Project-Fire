using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    
    public int coins = 100;
    public float goldMultiplier = 1.0f;
    public bool midas = false;

    void Update ()
    {
        if (midas)
        {
            goldMultiplier = 2.0f;
        }
    }
}
