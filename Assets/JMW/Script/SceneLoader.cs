using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    private void Awake()
    {
        base.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneIdx)
    {
        StartCoroutine(LoadingScene(sceneIdx));
    }
    IEnumerator LoadingScene(int idx)
    {
        yield return SceneManager.LoadSceneAsync(0);

        Slider slider = FindObjectOfType<Slider>();

        yield return StartCoroutine(Loading(slider, idx));
    }

    IEnumerator Loading(Slider slider, int idx)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(idx);

        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            slider.value = ao.progress / 0.9f;
            if(Mathf.Approximately(slider.value, 1.0f))
            {
                break;
            }
            yield return null;
        }

        while (true)
        {
            if (Input.anyKeyDown)
            {
                ao.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
    }
}