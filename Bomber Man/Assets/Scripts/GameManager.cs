using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    public bool isSingleplayer;
    public bool gameOver;

    private int enemies;

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

    public void CountEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Debug.Log("Enemies left: " + enemies);

        if (enemies <= 0)
        {
            gameOver = true;
        }
    }

    /*
    public void EnemyDeath()
    {
        Debug.Log("Enemy has died");
        enemies--;
    }
    */

    // Loadscene so that i can add more in between scene changes
    public void LoadScene(string sceneName)
    {
        gameOver = false;
        SceneManager.LoadScene(sceneName);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CountEnemies();
    }


    

}
