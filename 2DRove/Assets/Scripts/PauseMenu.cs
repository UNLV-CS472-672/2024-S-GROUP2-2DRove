using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;        // Boolean to check if the game is paused

    public GameObject pauseMenuUI;                  // Reference to the pause menu UI

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))         // If the escape key is pressed
        {
            if(GameIsPaused)                         // If the game is paused
            {
                Resume();                           // Resume the game
            }
            else
            {
                Pause();                            // Pause the game
            }
        }
        
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);                // Deactivate the pause menu UI
        Time.timeScale = 1f;                         // Set the time scale to 1
        GameIsPaused = false;                        // Set the game to not paused
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);                 // Activate the pause menu UI
        Time.timeScale = 0f;                         // Set the time scale to 0
        GameIsPaused = true;                         // Set the game to paused
    }

    public void QuitGame()
    {
        Application.Quit();                          // Quit the game
    }

    public void ResumeGame()
    {
        Resume();                                   // Resume the game
    }
}
