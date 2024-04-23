using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject startButton;
    public void loadMainmenu()
    {
        startButton.SetActive(false);
        mainMenu.SetActive(true);
    }
}
