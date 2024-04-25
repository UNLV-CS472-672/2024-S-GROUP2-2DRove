using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;        // Boolean to check if the game is paused

    public GameObject pauseMenuUI;                  // Reference to the pause menu UI

    // Update is called once per frame
    private void Update()
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

    // Function to resume the game
    private void Resume()
    {
        pauseMenuUI.SetActive(false);                // Deactivate the pause menu UI
        Time.timeScale = 1f;                         // Set the time scale to 1
        GameIsPaused = false;                        // Set the game to not paused
    }

    // Function to pause the game
    private void Pause()
    {
        pauseMenuUI.SetActive(true);                 // Activate the pause menu UI
        Time.timeScale = 0f;                         // Set the time scale to 0
        GameIsPaused = true;                         // Set the game to paused
    }

    // Function to quit the game
    public void QuitGame()
    {
        Application.Quit();                          // Quit the game
    }

    // Function to resume the game
    public void ResumeGame()
    {
        Resume();                                   // Resume the game
    }

    // Function to retry this level
    public void Retry(){
        // if the game is paused, resume it
        if(GameIsPaused){
            Resume();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //This reloads the current level
    }
    
    // Function to go to the main menu
    public void GotoMainMenu(){
        // if the game is paused, resume it
        if(GameIsPaused){
            Resume();
        }
        SceneManager.LoadScene(0); //This reloads the current level
    }
    // Save the game
    public void SaveGame(){
       
    }
}
