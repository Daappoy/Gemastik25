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
    private GameObject[] Menus;
    public bool isFullscreen = true;

    //place audiomanager here

    private void Start()
    {
    
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
}
