using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    public GameObject Loading;
    public Slider LoadingSlider;

    private void Start()
    {
        Loading.SetActive(false);
    }
    public void SceneChanger(int BuildIndex)
    {
        StartCoroutine(AsyncScene(BuildIndex));
    }
    IEnumerator AsyncScene(int BuildIndex)
    {
        Loading.SetActive(true);
        AsyncOperation Loader = SceneManager.LoadSceneAsync(BuildIndex);
        Loading.SetActive(true);
        while (!Loader.isDone)
        {
            float progress = Mathf.Clamp01(Loader.progress / 0.9f);
            LoadingSlider.value = progress;
            yield return null;
        }
    }
}
