using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public LoadingScreenManager loadingScreenManager;
    public int nextSceneIndex;
    void Start()
    {
        // nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        loadingScreenManager = GameObject.Find("LoadingScreenManager").GetComponent<LoadingScreenManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.enabled = false;
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            loadingScreenManager.LoadScene(nextSceneIndex);
        }
    }
}