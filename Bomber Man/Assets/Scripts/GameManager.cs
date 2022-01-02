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
    public bool levelStarted;
    public int nextScene;

    public GameObject[] playerTable;
    public List<GameObject> enemyList;

    public GameObject MenuCanvas;

    public int score;
    public int highScore;


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
        score = 0;
        gameOver = false;
        MenuCanvas.SetActive(false);

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void RetryLevel()
    {
        score = 0;
        MenuCanvas.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadNextLevel()
    {
        score = 0;
        gameOver = false;
        MenuCanvas.SetActive(false);

        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene > PlayerPrefs.GetInt("LevelAt"))
        {
            PlayerPrefs.SetInt("LevelAt", nextScene);
        }
        SceneManager.LoadScene(nextScene);

    }

    // Used for adding CountEnemies() after the scene is loaded.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int enemies = CountEnemies();
        int players = CountPlayers();
        if(SceneManager.GetActiveScene().buildIndex >= 5)
        {
            string highscoreKey = SceneManager.GetActiveScene().name;
            Debug.Log("Highscore set to " + highscoreKey + " value");
            highScore = PlayerPrefs.GetInt(highscoreKey, 0);
        }
    }

    public void AddScore(int num)
    {
        score += num;
        string highscoreKey = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(highscoreKey, highScore);
        GameObject.Find("ScoreCanvas").GetComponent<SingleplayerScore>().UpdateScore(score, PlayerPrefs.GetInt(highscoreKey, 0));
        if (this.score > highScore)
        {
            highScore = this.score;
        }
    }

    // Counts the amount of enemies at the start of the level.
    public int CountEnemies()
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

    public int CountPlayers()
    {
        playerTable = GameObject.FindGameObjectsWithTag("Player");

        if (playerTable.Length > 0)
        {
            MenuCanvas.SetActive(true);
        }

        Debug.Log("Player ammount: " + playerTable.Length);

        return playerTable.Length;
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
