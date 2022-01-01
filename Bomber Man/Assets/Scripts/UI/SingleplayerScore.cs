using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleplayerScore : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {

        scoreText.text =  "Score: " + GameManager.manager.score;
        highScoreText.text = "Highscore: " + GameManager.manager.highScore;
    }

    public void UpdateScore(int score, int highScore)
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "Highscore: " + highScore;
    }
}
