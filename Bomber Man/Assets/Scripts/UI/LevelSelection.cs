using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    private bool isInteractable;

    public int levelAt;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.manager.isSingleplayer)
        {
            isInteractable = false;
        } else
        {
            isInteractable = true;
        }

        levelAt = PlayerPrefs.GetInt("LevelAt", 4);
        Debug.Log("Level at: " + levelAt);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                lvlButtons[i].interactable = isInteractable;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Back to main menu");
            GameManager.manager.LoadScene("MainMenu");
        }
    }

    public void LoadLevel1()
    {
        GameManager.manager.LoadScene("Level-1");
    }
    public void LoadLevel2()
    {
        GameManager.manager.LoadScene("Level-2");
    }
    
    public void LoadLevel3()
    {
        GameManager.manager.LoadScene("Level-3");
    }
}
