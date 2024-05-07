using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject currentUI;
    [SerializeField] private Slider loadingSlider;

    public void LoadSceneBtn(string sceneToLoad)
    {
        currentUI.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneASync(sceneToLoad));
    }

    public void QuitGameBtn()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneASync(string sceneToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }

}
