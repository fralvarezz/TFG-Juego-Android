using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public float score;
    
    public float pointsPerSecond;

    [SerializeField]
    private int currentChain = 0;
    [SerializeField]
    private int bestChain = 0;
    private bool activeChain = false;
    private float remainingChainTime;
    public float chainTime;
    private float scoreDuringChain;
    
    
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
            score += pointsPerSecond * Time.deltaTime;

            scoreText.text = "Score: " + Mathf.RoundToInt(score);
        }

        if (activeChain)
        {
            if (currentChain > bestChain)
            {
                bestChain = currentChain;
            }
            
            if (remainingChainTime <= 0)
            {
                score += (scoreDuringChain * FindMultiplier(currentChain));
                activeChain = false;
                currentChain = 0;
                scoreDuringChain = 0;
            }
            else
            {
                remainingChainTime -= Time.deltaTime;
            }
        }
        
    }

    public void PlayerScored(string gameTag)
    {
        switch (gameTag)
        {
            case "DestructibleCollectDown":
                AddScore(score);
                break;
            case "DestructibleCollectUp":
                AddScore(score);
                break;
            case "DestructibleCollectFront":
                AddScore(score);
                break;
            case "Collect":
                score += 1000;
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
        else if (chain == 5)
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
}
