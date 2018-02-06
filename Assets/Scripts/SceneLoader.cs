using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public GameObject loadingPanel;
    public Slider loadingBar;

    private float progress;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsychronusly(sceneName));
    }

    IEnumerator LoadSceneAsychronusly(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) {

            // // clamp between 0 - 1 because unity loading stops to value 0.9 and takes also 0.1 to activate the scene(throw away unused staff).
            progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingPanel.SetActive(true);
            loadingBar.value = progress;
            yield return null; // wait for the next frame before continuing
        }
    }

}
