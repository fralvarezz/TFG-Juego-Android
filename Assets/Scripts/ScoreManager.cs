using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text addedScore;

    public float score;
    private float falseScore;
    
    public float pointsPerSecond;

    [SerializeField]
    private int currentChain = 0;
    [SerializeField]
    private int bestChain = 0;
    private bool activeChain = false;
    private float remainingChainTime;
    public float chainTime;
    private float scoreDuringChain;

    [Header("Points")]
    public int collectiblePoints;
    public int specialCollectPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        remainingChainTime = chainTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            if (activeChain)
            {
                if (currentChain > bestChain)
                {
                    bestChain = currentChain;
                }
            
                if (remainingChainTime <= 0)
                {
                    activeChain = false;
                    score += (scoreDuringChain * FindMultiplier(currentChain));
                    currentChain = 0;
                    scoreDuringChain = 0;
                    addedScore.text = "";
                }
                else
                {
                    scoreDuringChain += pointsPerSecond * Time.deltaTime;
                    
                    remainingChainTime -= Time.deltaTime;

                    addedScore.text = "+" + Mathf.RoundToInt(scoreDuringChain) + " x" + FindMultiplier(currentChain);
                }
            }
            else
            {
                score += pointsPerSecond * Time.deltaTime;
            }
            
            scoreText.text = "SCORE: " + Mathf.RoundToInt(score);
            
        }
        
    }

    public void PlayerScored(string gameTag)
    {
        switch (gameTag)
        {
            case "DestructibleCollectDown":
                AddScore(collectiblePoints);
                break;
            case "DestructibleCollectUp":
                AddScore(collectiblePoints);
                break;
            case "DestructibleCollectFront":
                AddScore(collectiblePoints);
                break;
            case "Collect":
                score += specialCollectPoints;
                break;
        }

    }

    private void ResetChainTime()
    {
        activeChain = true;
        remainingChainTime = chainTime;
    }

    private void AddScore(float scoreToAdd)
    {
        scoreDuringChain += scoreToAdd;
        ResetChainTime();
        currentChain++;
    }

    private float FindMultiplier(int chain)
    {
        float toret = 0;
        if (chain <= 1)
        {
            toret = 1;
        }
        else if (chain <= 3)
        {
            toret = 1.2f;
        }
        else if (chain <= 5)
        {
            toret = 1.4f;
        }
        else if (chain <= 7)
        {
            toret = 1.6f;
        }
        else if (chain <= 10)
        {
            toret = 2;
        }
        else if (chain <= 15)
        {
            toret = 3;
        }
        else if (chain <= 20)
        {
            toret = 4;
        }

        return toret;
    }

    public void ReportScore()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Note: make sure to add 'using GooglePlayGames'
            PlayGamesPlatform.Instance.ReportScore(Mathf.RoundToInt(score),
                GPGSIds.leaderboard_score,
                (bool success) =>
                {
                    Debug.Log("(Rockpunch) Leaderboard update success: " + success);
                });
        }
    }
}
