using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadScene("Floor1");
    }

    public void ExitGame(){
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
