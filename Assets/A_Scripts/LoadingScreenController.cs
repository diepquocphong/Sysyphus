using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Slider progressBar;
    public Text loadingText;

    void Start()
    {
        string nextScene = PlayerPrefs.GetString("NextScene");
        StartLoading(nextScene);
    }

    public void StartLoading(string sceneName)
    {
        Debug.Log("Start loading scene: " + sceneName);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            loadingText.text = (progress * 100).ToString("F0") + "%";
            yield return null;
        }
    }

}
