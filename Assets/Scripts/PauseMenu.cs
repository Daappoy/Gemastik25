using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuButton;
    public GameObject transparentBackground;
    public GameObject MainMenuBackground;
    public GameObject FinishedBackground;
    private bool escapeKeyPressed = false;
    public bool isPaused = false;
    // Start is called before the first frame update

    // public AudioManager audioManager;

    
    void Awake()
    {
        // Assign PauseMenuButton in the Inspector or find by name if needed
        // PauseMenuButton = GameObject.Find("PauseMenuButton");
        // If you want to ensure it's assigned, you can add a check:
        if (PauseMenuButton == null)
        {
            PauseMenuButton = GameObject.Find("PauseMenuButton");
        }
        if (transparentBackground == null)
        {
            transparentBackground = GameObject.Find("TransparentBG");
        }
        if( MainMenuBackground == null)
        {
            MainMenuBackground = GameObject.Find("Background");
        }
        if (FinishedBackground == null)
        {
            FinishedBackground = GameObject.Find("GameFinished");
        }

        PauseMenuButton.SetActive(true);
        MainMenuBackground.SetActive(false);
        transparentBackground.SetActive(false);

        // DontDestroyOnLoad(this.gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {

        // if (Input.GetKeyDown(KeyCode.Escape) && !escapeKeyPressed)
        // {

        //     escapeKeyPressed = true;
        //     if (isPaused)
        //     {
        //         ResumeGame();
        //     }
        //     else
        //     {
        //         PauseGame();
        //     }
        // }

        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     escapeKeyPressed = false;
        // }
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        
        if (isPaused) return;
        transparentBackground.SetActive(true);
        PauseMenuButton.SetActive(false);
        // audioManager.PlaySFX(audioManager.Pause);
        Time.timeScale = 0f;
        // MainMenuPanel.SetActive(true);
        MainMenuBackground.SetActive(true);
        isPaused = true;

    }

    public void ResumeGame()
    {
        transparentBackground.SetActive(false);
        PauseMenuButton.SetActive(true);
        // audioManager.PlaySFX(audioManager.ClickOnPause);
        Time.timeScale = 1f;
        // MainMenuPanel.SetActive(false);
        MainMenuBackground.SetActive(false);
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        // audioManager.PlaySFX(audioManager.MouseClick);
        SceneManager.LoadScene("MainMenu");
        // audioManager.PlaySFX(audioManager.ClickOnPause);
        Time.timeScale = 1f;
    }

    public void GameFinised()
    {
        Time.timeScale = 0f;
        FinishedBackground.SetActive(true);
        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FinishedBackground.SetActive(false);
        isPaused = false;
    }
}