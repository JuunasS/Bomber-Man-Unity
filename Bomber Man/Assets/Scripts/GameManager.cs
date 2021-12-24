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
    public int nextScene;

    public GameObject[] playerTable;
    public List<GameObject> enemyList;

    public GameObject MenuCanvas;


    private void Awake()
    {
        if (manager == null)
        {
            // If manager does not exist don't destroy this
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
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
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
    public void LoadNextLevel()
    {
        gameOver = false;
        MenuCanvas.SetActive(false);

        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextScene);
        }
        SceneManager.LoadScene(nextScene);
       

    }

    // Used for adding CountEnemies() after the scene is loaded.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CountEnemies();
        CountPlayers();
    }

    // Counts the amount of enemies at the start of the level.
    public float CountEnemies()
    {
        GameObject[] enemyTable = GameObject.FindGameObjectsWithTag("Enemy");

        enemyList.Clear();
        Debug.Log("Tyhjennetty lista: " + enemyList.Count);

        foreach (GameObject enemy in enemyTable)
        {
            Debug.Log("Vihollinen jäljellä +1");
            enemyList.Add(enemy);
        }

        enemyList.RemoveAll(item => item == null);

        Debug.Log("Enemies left: " + enemyList.Count);
        return enemyList.Count;
    }

    public void CountPlayers()
    {
        playerTable = GameObject.FindGameObjectsWithTag("Player");

        if (playerTable.Length > 0)
        {
            MenuCanvas.SetActive(true);
        }

        Debug.Log("Player ammount: " + playerTable.Length);
    }

    // Checks the state of the game
    public void CheckGameState()
    {
        Debug.Log("Checking game state...");

        float numOfEnemies = CountEnemies();
        if (numOfEnemies == 0)
        {
            gameOver = true;
            isGameWon = true;
            Debug.Log("Game over! All enemies are dead.");
        }

        foreach (GameObject player in playerTable)
        {
            if (player.GetComponent<PlayerController>().GetHealth() <= 0)
            {
                gameOver = true;
                isGameWon = false;
                Debug.Log("Game over! Player has died.");
            }
        }
    }
}
