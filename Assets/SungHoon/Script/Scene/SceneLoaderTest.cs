using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(int sceneidx)
    {

        StartCoroutine(LoadingScene(sceneidx));
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

        while (!ao.isDone) //�ε��� ������ 
        {
            slider.value = ao.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                //ao.allowSceneActivation = true;
                break;
            }
            yield return null;
        }

        while (true) //���Ƿ� �����̽��� ������ �����ϰ� 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ao.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
    }
}
