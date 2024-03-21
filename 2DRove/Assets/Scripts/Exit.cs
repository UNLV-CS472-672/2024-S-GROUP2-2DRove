using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public LoadingScreenManager loadingScreenManager;
    private static int levelCounter = 1;

    void Start()
    {
        loadingScreenManager = GameObject.Find("LoadingScreenManager").GetComponent<LoadingScreenManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            // levelCounter++;
            // string nextSceneName = "Floor" + levelCounter.ToString();
            // Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            loadingScreenManager.LoadScene(nextSceneIndex);
        }
    }
}