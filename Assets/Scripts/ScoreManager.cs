using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public float score;
    
    public float pointsPerSecond;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            score += pointsPerSecond * Time.deltaTime;

            scoreText.text = "Score: " + Mathf.RoundToInt(score);
        }
        
    }

    public void PlayerScored(string gameTag)
    {
        switch (gameTag)
        {
            case "DestructibleCollectDown":
                score += 20;
                break;
            case "DestructibleCollectUp":
                score += 50;
                break;
            case "DestructibleCollectFront":
                score += 100;
                break;
            case "Collect":
                score += 1000;
                break;
        }
    }
}
