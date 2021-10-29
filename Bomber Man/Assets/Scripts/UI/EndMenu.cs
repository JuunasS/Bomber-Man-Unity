using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public GameObject endMenuUI;
    public bool gameOver = false;

    void Update()
    {
        if(gameOver)
        {
            endMenuUI.SetActive(true);
        }
    }
    public void LoadNextLevel()
    {
        endMenuUI.SetActive(false);
    }

    // Loads Mainmenu scene.
    public void LoadMenu()
    {
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.manager.LoadScene("MainMenu");
    }

    // Loads LevelSelection scene.
    public void LoadLevelSelection()
    {
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.manager.LoadScene("LevelSelection");
    }
}
