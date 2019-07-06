using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private KeyCode keybind = KeyCode.Escape;
    private bool paused = false;
    public GameObject PauseMenu;
    public GameObject DisablePlayer;

    public void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string[] pausable = { "Level 1", "EnemyTest" };
        for (int i = 0; i < pausable.Length; i++)
        {
            if (currentScene.name == pausable[i])
            {
                callPauseMenu(keybind);
            }
        }
    }

    public void sceneChange(string scene)
    {
        if (scene == "Exit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene);
    }

    public void callPauseMenu(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            if (paused)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
                DisablePlayer.SetActive(true);
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
                DisablePlayer.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        DisablePlayer.SetActive(true);
    }
}
