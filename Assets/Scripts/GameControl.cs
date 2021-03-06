﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public AudioSource mainTheme;
    
    private ScoreManager scoreManager;
    
    public GameObject gameOverText;

    public Text fps;

    public bool gameOver = false;
    public static GameControl instance; //singleton

    private float timeElapsed;

    public float initialBackgroundScrollSpeed;
    public float maxBackgroundScrollSpeed;
    
    [SerializeField]
    private float backgroundScrollSpeed;
    
    [SerializeField]
    private float nextBackgroundScrollSpeed;

    private Spawner spawner;
    
    [SerializeField]
    private int numDifficultyLevels = 5;
    [SerializeField]
    private int actualDifficultyLevel = 1;
    private int checkedDifficulty;
    private float difficultyJump;
    private int score = 0;

    private int firstTime = 0;
    private TutorialManager tutorialManager;

    public GameObject pauseMenu;

    public GameObject optionsMenu;
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
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

    }

    void Start()
    {
        backgroundScrollSpeed = initialBackgroundScrollSpeed;
        nextBackgroundScrollSpeed = backgroundScrollSpeed;
        
        spawner = gameObject.GetComponent<Spawner>();
        difficultyJump = (maxBackgroundScrollSpeed - initialBackgroundScrollSpeed) / (numDifficultyLevels - 1);
        scoreManager = GetComponent<ScoreManager>();
        
        
        tutorialManager = GetComponent<TutorialManager>();
        firstTime = PlayerPrefs.GetInt("savedFirstTime", 1);
        if (firstTime == 1)
        {
            tutorialManager.enabled = true;
            PlayerPrefs.SetInt("savedFirstTime", 0);
        }
        else
        {
            tutorialManager.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameOver && Input.GetMouseButtonUp(0) && !AnyMenuActive())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (!gameOver && backgroundScrollSpeed > maxBackgroundScrollSpeed)
        {
            nextBackgroundScrollSpeed -= Time.deltaTime / 10;
            if (backgroundScrollSpeed - 0.25f >= nextBackgroundScrollSpeed)
            {
                backgroundScrollSpeed = nextBackgroundScrollSpeed;
            }
            
            checkedDifficulty = GetDifficulty();

            if (checkedDifficulty != actualDifficultyLevel)
            {
                actualDifficultyLevel = checkedDifficulty;
                UpdateDifficulty(actualDifficultyLevel);
            }
        }

        fps.text = "FPS: " + 1.0f / Time.deltaTime;
        
    }

    public void PlayerScored(string gameTag)
    {
        scoreManager.PlayerScored(gameTag);
    }

    private int GetDifficulty()
    {
        int difficultyLevel = 1;
        
        if (backgroundScrollSpeed <= initialBackgroundScrollSpeed - 4)
        {
            difficultyLevel = 5;
        }
        else if(backgroundScrollSpeed <= initialBackgroundScrollSpeed - 3)
        {
            difficultyLevel = 4;
        }
        else if(backgroundScrollSpeed <= initialBackgroundScrollSpeed - 2)
        {
            difficultyLevel = 3;
        }
        else if(backgroundScrollSpeed <= initialBackgroundScrollSpeed - 1)
        {
            difficultyLevel = 2;
        }
        else
        {
            difficultyLevel = 1;
        }

        return difficultyLevel;
    }

    private void UpdateDifficulty(int difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case 2:
                spawner.hazardBlockThreshold = 85;
                spawner.twoHazardBlockThreshold = 95;
                
                spawner.minHazardSpawnRate = 1.75f;
                spawner.maxHazardSpawnRate = 2.75f;
                break;
            case 3:
                spawner.destructibleFrontThreshold = 18;
                spawner.destructibleDownThreshold = 32;
                spawner.destructibleUpThreshold = 50;
                spawner.hazardBlockThreshold = 75;
                spawner.twoHazardBlockThreshold = 90;
                
                spawner.minHazardSpawnRate = 1.5f;
                spawner.maxHazardSpawnRate = 2.5f;
                break;
            case 4:
                spawner.destructibleFrontThreshold = 18;
                spawner.destructibleDownThreshold = 32;
                spawner.destructibleUpThreshold = 50;
                spawner.hazardBlockThreshold = 70;
                spawner.twoHazardBlockThreshold = 90;
                spawner.minHazardSpawnRate = 1f;
                spawner.maxHazardSpawnRate = 2f;
                break;
            case 5:
                spawner.destructibleFrontThreshold = 18;
                spawner.destructibleDownThreshold = 32;
                spawner.destructibleUpThreshold = 50;
                spawner.hazardBlockThreshold = 65;
                spawner.twoHazardBlockThreshold = 85;
                
                spawner.minHazardSpawnRate = 1f;
                spawner.maxHazardSpawnRate = 1.5f;
                break;
        }
    }
    

    public void PlayerDied()
    {
        gameOver = true;
        gameOverText.SetActive(true);
        scoreManager.CalculateLastScore();
        scoreManager.ReportScore();
        if (mainTheme.isPlaying)
        {
            mainTheme.Stop();
        }
    }


    public float BackgroundScrollSpeed
    {
        get => backgroundScrollSpeed;
        set => backgroundScrollSpeed = value;
    }

    public float NextBackgroundScrollSpeed
    {
        get => nextBackgroundScrollSpeed;
        set => nextBackgroundScrollSpeed = value;
    }

    private bool AnyMenuActive()
    {
        return pauseMenu.activeSelf || optionsMenu.activeSelf;
    }
}
