using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SaveData saveData; // Reference to SaveManager
    public GameObject PoweroffUI;
    public GameObject PoweronUI;
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    public GameObject LevelsUI;
    private GameObject[] Menus;
    public Animator Transition;
    public Animator NarrationAnimator;
    public Animator PowerAnimator;
    public float transitionTime = 1f;
    public bool isFullscreen = true;

    //place audiomanager here
    public AudioManager audioManager;

    private void Start()
    {
        Debug.Log("power is... " + saveData.PowerIsOff);
        if (saveData.PowerIsOff)
        {
            Debug.Log("Power is off, turning off power UI");
            // PoweroffUI.SetActive(true);
            // PoweronUI.SetActive(false);
        }
        else if (!saveData.PowerIsOff)
        {
            Debug.Log("Power is on, turning on power UI");
            // PoweroffUI.SetActive(false);
            // PoweronUI.SetActive(true);
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
        audioManager.PlaySFX(audioManager.MouseClick);
    }
    public void ShowSettings()
    {
        ShowOnly(SettingsUI);
        audioManager.PlaySFX(audioManager.MouseClick);
    }
    public void ShowLevels()
    {
        ShowOnly(LevelsUI);
        audioManager.PlaySFX(audioManager.MouseClick);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        if (sceneName != "Tutorial")
        {
            Debug.Log("Loading other scene: " + sceneName);
            //play animation
            Transition.SetTrigger("Start");
            //wait
            yield return new WaitForSeconds(transitionTime);
            //Load Scene
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1f;
        }
        else if (sceneName == "Tutorial")
        { //kalo player pilih tutorial
            Debug.Log("Loading tutorial scene: " + sceneName);
            saveData.PowerIsOff = true;
            Debug.Log("power is..." + saveData.PowerIsOff);

            PowerAnimator.SetTrigger("matiLampu");
            
            yield return new WaitForSeconds(2.4f);
            // PoweronUI.SetActive(false);
            //play animation
            NarrationAnimator.SetTrigger("Enter");
            //wait
            yield return new WaitForSeconds(11f);
            //Load Scene
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1f;
        }
    }

    public void LevelMenu()
    {
        // SceneManager.LoadScene("MainMenu");
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
