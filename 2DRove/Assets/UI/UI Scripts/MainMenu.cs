using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadScene("(1) City Level");
    }

    public void ExitGame(){
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    //load the game
    public void LoadGame(){
        Debug.Log("Loading Game...");
    }
}
