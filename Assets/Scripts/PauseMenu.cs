using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject animatedPauseMenu;
    private Animator pauseMenuAnimation;

    private void Start()
    {
        pauseMenuAnimation = animatedPauseMenu.GetComponent<Animator>();  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();               
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuAnimation.SetBool("GameIsPaused", false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //pauseMenuAnimation.Play("PauseMenuOnAnimation");
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuAnimation.SetBool("GameIsPaused", true);
    }

    public void BackToMainMenu()
    {       
        Initiate.Fade("MainMenu", new Color(0, 0, 0), 2.0f);  
        //fading effect is not working if pause menu is active (resume function disables it)
        Resume();
        Cursor.lockState = CursorLockMode.None;
    }
}
