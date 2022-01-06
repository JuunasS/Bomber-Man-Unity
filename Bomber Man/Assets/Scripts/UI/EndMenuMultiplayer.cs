using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EndMenuMultiplayer : MonoBehaviourPunCallbacks
{

    public GameObject endMenuUI;
    public Text gameOverText;

    void Start()
    {
    }

    void Update()
    {

    }

    public void CheckEndMenu()
    {

        Debug.Log("EndMenuMultiplayer Checking...");
        if (GameManager.manager.gameOver)
        {
            Debug.Log("EndMenuMultiplayer game over");
            endMenuUI.SetActive(true);

            gameOverText.text = "Game over!";
        }
    }

    public void PlayAgain()
    {
        GameManager.manager.RetryLevel();
        endMenuUI.SetActive(false);
    }

    // Loads Mainmenu scene.
    public void LoadMenu()
    {
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SwitchLevel("MainMenu");
    }

    // Loads LevelSelection scene.
    public void LoadLevelSelection()
    {
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SwitchLevel("LevelSelection");
    }


    // These methods are to ensure that player is disconnected.
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
