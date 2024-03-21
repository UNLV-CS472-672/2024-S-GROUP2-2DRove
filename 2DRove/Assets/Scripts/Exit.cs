using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public LoadingScreenManager loadingScreenManager;
    void Start()
    {
        loadingScreenManager = GameObject.Find("LoadingScreenManager").GetComponent<LoadingScreenManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            loadingScreenManager.LoadScene(nextSceneIndex);
        }
    }
}