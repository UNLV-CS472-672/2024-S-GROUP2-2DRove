using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    [SerializeField] Collider2D col;
    [SerializeField] int level;
    // Start is called before the first frame update
    void OnTriggerEnter2D (Collider2D col){
        SceneManager.LoadScene(level);
    }

    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
    //  OnTriggerEnterdadaNewLevel(col);   
    }
}
