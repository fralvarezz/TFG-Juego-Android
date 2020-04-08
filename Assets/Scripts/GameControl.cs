using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public bool gameOver = false;
    public static GameControl instance; //singleton

    public float backgroundScrollSpeed = -12f;

    private int score = 0;
    
    //Set up GameControl
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDestroyedBlock()
    {
        if (gameOver)
        {
            return;
        }
        else
        {
            score++;
        }
    }

    public void PlayerDied()
    {
        gameOver = true;
    }
    
}
