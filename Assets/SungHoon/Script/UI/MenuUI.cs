using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;
    public GameObject BG;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TryOpenMenuUI();
        }
    }

    public void TryOpenMenuUI()
    {
        GameIsPaused = !GameIsPaused;
        if (GameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        BG.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        BG.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void ToSettingMenu()
    {
        Debug.Log("아직 미구현입니다...");
    }

    public void ToMain()
    {
        Debug.Log("아직 미구현입니다...");
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("아직 미구현입니다...");
        Application.Quit();
    }
}
