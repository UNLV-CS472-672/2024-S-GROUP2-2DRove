using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    
    public void StartGame(){
        SceneManager.LoadScene("Level_00");
    }

    public void OpenSettings(){
        SettingsPanel.SetActive(true);
    }

    public void CloseSettings(){
        SettingsPanel.SetActive(false);
    }

    public void ExitGame(){
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
