using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public LoadingScreenManager loadingScreenManager;
    public int nextSceneIndex = 0;
    void Start()
    {
        loadingScreenManager = GameObject.Find("LoadingScreenManager").GetComponent<LoadingScreenManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            loadingScreenManager.LoadScene(nextSceneIndex);
        }
    }
}