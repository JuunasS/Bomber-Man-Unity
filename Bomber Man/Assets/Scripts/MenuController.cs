using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadMapRandom()
    {
        SceneManager.LoadScene("Level-" + 1);
    }

    public void SingleplayerGame()
    {
        // Open map selection, set gamemanager singleplayer to true
    }

    public void MultiplayerGame()
    {
        // Open map selection, set gamemanager multiplayer to true
    }
}
