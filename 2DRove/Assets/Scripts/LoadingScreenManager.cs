using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public Slider loadingBar;
    public GameObject loadingScreen;
    public Canvas loadingScreenCanvas;
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        float minLoadingTime = 2f; // Minimum time to show the loading screen
        float startTime = Time.time;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false; // Don't allow the scene to activate until it's fully loaded
        loadingScreenCanvas.gameObject.SetActive(true);
        loadingScreen.SetActive(true);
        while (Time.time - startTime < minLoadingTime || !operation.isDone)
        {
            float progress = Mathf.Clamp01((Time.time - startTime) / minLoadingTime);
            loadingBar.value = progress;
            if (Time.time - startTime >= minLoadingTime && operation.progress >= 0.9f) // If the scene has loaded and the minimum loading time has passed
            {
                operation.allowSceneActivation = true; // Allow the scene to activate
            }
            yield return null;
        }
        loadingScreenCanvas.gameObject.SetActive(false);
        loadingScreen.SetActive(false);
    }
}