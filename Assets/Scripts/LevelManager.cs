using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    //public GameObject startButton;
    //public GameObject exitButton;

    // Update is called once per frame
    void Update (){
    }

    public void sceneChange(string scene)
    {
        if (scene == "Exit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene);
    }
}
