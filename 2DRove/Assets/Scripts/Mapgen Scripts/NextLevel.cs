using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    [SerializeField] Collider2D col;
    public static LoadingScreenManager loadingScreenManager;
    public int nextSceneIndex = 0;

    void Start()
    {
        loadingScreenManager = GameObject.Find("LoadingScreenManager").GetComponent<LoadingScreenManager>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            loadingScreenManager.LoadScene(nextSceneIndex);
        }
    }
}
