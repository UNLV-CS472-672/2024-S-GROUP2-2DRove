using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private static int levelCounter = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            levelCounter++;
            SceneManager.LoadScene("Floor" + levelCounter);
        }
    }
}