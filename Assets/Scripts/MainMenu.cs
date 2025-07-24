using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    public GameObject LevelsUI;
    public Slider VolumeSlider;
    private GameObject[] Menus;
    public bool isFullscreen = true;

    //place audiomanager here

    private void Start()
    {
        if (PlayerPrefs.HasKey("value"))
        {
            Debug.Log("There is a key");
        }
        else
        {
            Debug.Log("There is no key");
            PlayerPrefs.SetFloat("value", 0.5f); // Set default volume if not set
            PlayerPrefs.Save();
        }

        Menus = new GameObject[] { MainMenuUI, SettingsUI, LevelsUI };
        ShowOnly(MainMenuUI);
        LoadFullscreenSetting();
    }
    private void ShowOnly(GameObject menuToShow)
    {
        foreach (GameObject m in Menus)
        {
            m.SetActive(m == menuToShow);
        }
    }

    public void ShowMainMenu()
    {
        ShowOnly(MainMenuUI);
    }
    public void ShowSettings()
    {
        ShowOnly(SettingsUI);
    }
    public void ShowLevels()
    {
        ShowOnly(LevelsUI);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        ShowOnly(LevelsUI);
    }

    //settings
    public void SetFullscreen()
    {
        if (!isFullscreen)
        {
            Debug.Log("Setting fullscreen mode");
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            isFullscreen = true;
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else if (isFullscreen)
        {
            Debug.Log("Setting windowed mode");
            Screen.fullScreenMode = FullScreenMode.Windowed;
            isFullscreen = false;
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        PlayerPrefs.Save();
    }
    
    public void LoadFullscreenSetting()
    {
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            int fullscreenValue = PlayerPrefs.GetInt("fullscreen");
            isFullscreen = fullscreenValue == 1;
            
            if (isFullscreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Debug.Log("Loaded fullscreen mode");
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Debug.Log("Loaded windowed mode");
            }
        }
        else
        {
            Debug.Log("No saved fullscreen setting found, using default (fullscreen).");
            isFullscreen = true;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            PlayerPrefs.SetInt("fullscreen", 1);
            PlayerPrefs.Save();
        }
    }
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("value", volume);
        PlayerPrefs.Save();
        Debug.Log("Volume set to: " + volume);
    }
    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("value"))
        {
            float volume = PlayerPrefs.GetFloat("value");
            AudioListener.volume = volume;
            VolumeSlider.value = volume;
            Debug.Log("Loaded volume: " + volume);
        }
        else
        {
            Debug.Log("No saved volume found, using default.");
        }
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("value", VolumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log("Settings saved with volume: " + VolumeSlider.value);
    }
}
