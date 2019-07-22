using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public AudioMixer music;
    public AudioMixer soundfx;
    public Dropdown dropdown;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        int current = 0;
        for (int i = 0; i < resolutions.Length; ++i)
        {
            string opt = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(opt);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                current = i;
            }
        }
        dropdown.AddOptions(options);
        dropdown.value = current;
        dropdown.RefreshShownValue();
    }

    public void setMusic(float volume)
    {
        music.SetFloat("volume", volume);
    }

    public void setSoundfx(float volume)
    {
        soundfx.SetFloat("volume", volume);
    }
    
    public void setResolution(int resolution)
    {
        Resolution reso = resolutions[resolution];
        Screen.SetResolution(reso.width, reso.height, Screen.fullScreen);
    }

    public void setGraphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void setFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
