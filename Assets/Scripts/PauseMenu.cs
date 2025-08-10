using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public Animator Transition;
    public Animator NarrationAnimator;
    public float NarrationTransitionTime = 1.3f;
    public float transitionTime = 1f;
    public GameObject PauseMenuButton;
    public GameObject transparentBackground;
    public GameObject MainMenuBackground;
    public GameObject FinishedBackground;

    [Header("Robot Missing Panel and script")]
    public GameObject robotMissingPanel;
    public missingRobot robotMissingScript;
    public Animator OutroAnimator;

    private bool escapeKeyPressed = false;
    public bool isPaused = false;
    private bool robotMissingHandled = false;
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
        if (MainMenuBackground == null)
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
        FinishedBackground.SetActive(false);

        if (robotMissingPanel != null)
        {
            robotMissingPanel.SetActive(false);
        }



        // DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.Log("tutorial scene detected");
            // NarrationAnimator.SetTrigger("Exit");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level_4")
        {
            RobotMissing();
        }
    }

    private void RobotMissing()
    {
        if (robotMissingScript.isRobotMissing && !robotMissingHandled)
        {
            robotMissingPanel.SetActive(true);
            PauseMenuButton.SetActive(false);
            isPaused = true;
            robotMissingHandled = true;
            Time.timeScale = 0f;
        }
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

    public void BackToMainMenu(string sceneName)
    {
        Debug.Log("Function called: BackToMainMenu");
        StartCoroutine(LoadLevel(sceneName));
    }

    public void LoadNextLevel(string sceneName)
    {
        Debug.Log("Function called: LoadNextLevel");
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        Time.timeScale = 1f;
        isPaused = false;
        //play animation
        Transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //Load Scene
        SceneManager.LoadScene(sceneName);
        transparentBackground.SetActive(false);
        MainMenuBackground.SetActive(false);
        Time.timeScale = 1f;
    }

    [ContextMenu("trigger Game Finished")] 
    public void GameFinised()
    {
        Time.timeScale = 0f;
        FinishedBackground.SetActive(true);
        PauseMenuButton.SetActive(false);
        isPaused = true;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
        FinishedBackground.SetActive(false);
    }

    public void PlayOutro()
    {
        Time.timeScale = 1f;
        FinishedBackground.SetActive(false);
        audioManager.EndingSoundSource.mute = false;
        audioManager.EndingSoundSource.Play();
        audioManager.MusicSource.mute = true;
        StartCoroutine(PlayOutroAnimation());
    }

    IEnumerator PlayOutroAnimation()
    {
        Debug.Log("Playing outro animation");
        OutroAnimator.SetTrigger("OutroStart");
        yield return new WaitForSeconds(25f);
        BackToMainMenu("MainMenu");
    }
}