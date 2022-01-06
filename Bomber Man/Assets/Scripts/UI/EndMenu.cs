using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject endMenuUI;
    public Button retryButton;
    public Button nextLevelButton;
    public Text gameOverText;

    void Start()
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void CheckEndMenu()
    {
        if (GameManager.manager.gameOver && GameManager.manager.isSingleplayer)
        {
            endMenuUI.SetActive(true);

            if (GameManager.manager.isGameWon)
            {
                gameOverText.text = "Game won";
                retryButton.gameObject.SetActive(false);
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                gameOverText.text = "Game lost";
                retryButton.gameObject.SetActive(true);
                nextLevelButton.gameObject.SetActive(false);
            }
        }
    }

    public void RetryLevel()
    {
        GameManager.manager.RetryLevel();
        endMenuUI.SetActive(false);
    }
    public void LoadNextLevel()
    {
        GameManager.manager.LoadNextLevel();
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
