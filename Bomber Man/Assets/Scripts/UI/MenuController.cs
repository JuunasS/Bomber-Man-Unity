using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void LoadSingleplayer()
    {
        // Open map selection, set gamemanager singleplayer to true
        GameManager.manager.LoadScene("LevelSelection");
        GameManager.manager.isSingleplayer = true;
    }

    public void LoadMultiplayerGame()
    {
        // Open map selection, set gamemanager singleplayer to false
        GameManager.manager.LoadScene("LevelSelection");
        GameManager.manager.isSingleplayer = false;
    }
}
