using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    public bool isSingleplayer;
    public bool gameOver;
    public bool isGameWon;

    public int enemies;

    private GameObject[] playersList;

    public GameObject MenuCanvas;


    private void Awake()
    {
        if(manager == null)
        {
            // If manager does not exist do't destroy this
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            // If manager already exists in scene destroy this
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        
    }

    // Loadscene so that i can add more in between scene changes
    public void LoadScene(string sceneName)
    {
        gameOver = false;
        MenuCanvas.SetActive(false);

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void RetryLevel()
    {
        gameOver = false;
        MenuCanvas.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Used for adding CountEnemies() after the scene is loaded.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CountEnemies();
        CountPlayers();
    }

    // Counts the amount of enemies at the start of the level.
    public void CountEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Debug.Log("Enemies left: " + enemies);
    }
    public void CountPlayers()
    {
        playersList = GameObject.FindGameObjectsWithTag("Player");

        if(playersList.Length > 0)
        {
            MenuCanvas.SetActive(true);
        }

        Debug.Log("Player ammount: " + playersList.Length);
    }

    // Checks the state of the game
    public void CheckGameState()
    {
        Debug.Log("Checking game state...");

        CountEnemies();
        if (enemies <= 0)
        {
            gameOver = true;
            isGameWon = true;
            Debug.Log("Game over! All enemies are dead.");
        }

        foreach (GameObject player in playersList){
            if(player.GetComponent<PlayerController>().GetHealth() <= 0)
            {
                gameOver = true;
                isGameWon = false;
                Debug.Log("Game over! Player has died.");
            }
        }
    }
}
