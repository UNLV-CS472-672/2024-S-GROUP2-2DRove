using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelBoss : MonoBehaviour
{
    [SerializeField] GameObject door;
    int deathCount = 0;

    void Start(){
        deathCount = SceneManager.GetActiveScene().name == "(1-2) City Boss" ? 0 : 1;
    }

    public void deathCheck(){
        deathCount++;
        if(deathCount == 2)
            door.SetActive(true);
    }
}
