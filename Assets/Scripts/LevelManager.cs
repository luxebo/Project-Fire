using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private KeyCode keybind = KeyCode.Escape;
    private bool paused = false;
    public GameObject PauseMenu;
    public GameObject player;
    string[] pausable = { "Level 1", "EnemyTest", "AssetTest" };

    public void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
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
        if (PauseMenu != null && sceneInPausable(scene))
        {
            paused = false;
            Time.timeScale = 1f;
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
                player.SetActive(true);
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
                player.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        player.SetActive(true);
    }

    private bool sceneInPausable(string scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        for (int i = 0; i < pausable.Length; i++)
        {
            if (currentScene.name == pausable[i])
            {
                return true;
            }
        }
        return false;
    }
}
