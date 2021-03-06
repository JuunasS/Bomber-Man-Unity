using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resumes game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Pauses game.
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Loads Mainmenu scene.
    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (GameManager.manager.isSingleplayer)
        {
            GameManager.manager.LoadScene("MainMenu");
        } 
        else
        {
            SwitchLevel("MainMenu");
        }
    }

    // Loads LevelSelection scene.
    public void LoadLevelSelection()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (GameManager.manager.isSingleplayer)
        {
            GameManager.manager.LoadScene("LevelSelection");
        }
        else
        {
            SwitchLevel("LevelSelection");
        }
    }

    public void SwitchLevel(string level)
    {
        StartCoroutine(DoSwitchLevel(level));
    }

    IEnumerator DoSwitchLevel(string level)
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        GameManager.manager.LoadScene(level);
    }
}
