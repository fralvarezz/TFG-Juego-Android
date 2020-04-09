using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public bool gameOver = false;
    public static GameControl instance; //singleton

    private float timeElapsed;

    public float initialBackgroundScrollSpeed;
    
    [SerializeField]
    private float backgroundScrollSpeed;

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

    void Start()
    {
        backgroundScrollSpeed = initialBackgroundScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            timeElapsed = Time.time;
            backgroundScrollSpeed = initialBackgroundScrollSpeed - Mathf.Sqrt(Time.time);
        }
        
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

    public float BackgroundScrollSpeed => backgroundScrollSpeed;
}
