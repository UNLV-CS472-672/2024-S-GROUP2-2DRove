using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelBoss : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject boss1 = null;
    [SerializeField] GameObject boss2 = null;
    void Update(){

        if (boss2 == null ? boss1.GetComponent<NewEnemy>().CurrentHeath() <= 0 : (boss1.GetComponent<NewEnemy>().CurrentHeath() <= 0  && boss2.GetComponent<NewEnemy>().CurrentHeath() <= 0))
            door.SetActive(true);
    }
}
