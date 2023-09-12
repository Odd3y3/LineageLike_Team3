using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneIdx, int spawnPointNum)
    {
        StartCoroutine(LoadingScene(sceneIdx, spawnPointNum));
    }
    IEnumerator LoadingScene(int idx, int spawnPointNum)
    {
        yield return SceneManager.LoadSceneAsync(0);

        Slider slider = FindObjectOfType<Slider>();

        yield return StartCoroutine(Loading(slider, idx, spawnPointNum));
    }

    IEnumerator Loading(Slider slider, int idx, int spawnPointNum)
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

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            if (Input.anyKeyDown)
            {
                ao.allowSceneActivation = true;
                break;
            }
            yield return null;
        }

        //yield return new WaitForSeconds(1.0f);
        yield return null;
        GameManager.Inst.StartInGameScene(spawnPointNum);
    }
}
