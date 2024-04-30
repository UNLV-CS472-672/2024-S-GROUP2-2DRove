using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;

    public void EnableGameOverMenu(){
        gameOverMenuUI.SetActive(true);
        
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //This reloads the current level
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("(0) Main Menu");
    }
}
