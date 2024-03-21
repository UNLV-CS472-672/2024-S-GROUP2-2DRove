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
        float minLoadingTime = 5f; // Minimum time to show the loading screen
        float startTime = Time.time;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreenCanvas.gameObject.SetActive(true);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float timeProgress = (Time.time - startTime) / minLoadingTime;
            float loadProgress = operation.progress / 0.9f;
            float progress = Mathf.Clamp01(Mathf.Max(timeProgress, loadProgress));
            loadingBar.value = progress;
            yield return null;
        }
        yield return new WaitForSeconds(minLoadingTime - (Time.time - startTime));
        loadingScreenCanvas.gameObject.SetActive(false);
        loadingScreen.SetActive(false);
    }
}