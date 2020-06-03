using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    [Header("Probability Thresholds")]
    public int destructibleFrontThreshold;
    public int destructibleDownThreshold;
    public int destructibleUpThreshold;
    public int hazardBlockThreshold;
    public int twoHazardBlockThreshold;
    public int wallThreshold = 0;
    
    [Header("Spawn Prefabs")]
    public GameObject destructibleCollectFrontPrefab;
    public GameObject destructibleCollectDownPrefab;
    public GameObject destructibleCollectUpPrefab;
    public GameObject hazardBlockPrefab;
    public GameObject collectPrefab;
    
    [Header("Spawn Pool Sizes")]
    public int destructibleCollectFrontPoolSize;
    public int destructibleCollectDownPoolSize;
    public int destructibleCollectUpPoolSize;
    public int hazardBlockPoolSize;
    public int collectsPoolSize;
    
    [Header("Spawn Timers")]
    public float minHazardSpawnRate;
    public float maxHazardSpawnRate;
    [SerializeField]
    private float nextHazardSpawn = 1f;
    
    
    [Header("Destructible Collects Front")]
    private GameObject[] destructibleCollectsFront;
    private Vector2 destructibleCollectFrontPoolPosition = new Vector2(-15f, -15f);
    private int currentDestructibleFront = 0;
    
    [Header("Destructible Collects Down")]
    private GameObject[] destructibleCollectsDown;
    private Vector2 destructibleCollectDownPoolPosition = new Vector2(-15f, -20f);
    private int currentDestructibleDown = 0;
    
    [Header("Destructible Collects Up")]
    private GameObject[] destructibleCollectsUp;
    private Vector2 destructibleCollectUpPoolPosition = new Vector2(-15f, -25f);
    private int currentDestructibleUp = 0;
    
    [Header("Hazard Blocks")]
    private GameObject[] hazardBlocks;
    private Vector2 hazardBlockPoolPosition = new Vector2(-15f, -30f);
    private int currentHazardBlock = 0;
    private float hazardBlockSpawnTimer;
    
    [Header("Collects")]
    private GameObject[] collects;
    private Vector2 collectsPoolPosition = new Vector2(-15f, -35f);
    private int currentCollect = 0;
    
    [Header("Collects Spawn Rate")]
    public float minCollectSpawnRate;
    public float maxCollectSpawnRate;
    [SerializeField]
    private float nextCollectSpawn = 4f;
    
    [Header("Screen positioning")]
    private float thirdOfScreen;
    private float spawnXPosition = 10f;
    
    [Header("Hazard Blocks positioning")]
    public float horizontalYpositionMin;
    public float horizontalYpositionMax;
    private float thirdOfScreenHorizontal;
    public float tiltedYpositionMin;
    public float tiltedYpositionMax;
    private float thirdOfScreenTilted;
    public float verticalYpositionMin;
    public float verticalYpositionMax;
    private float thirdOfScreenVertical;
    
    [Header("Destructibles positioning")]
    public float destructDownYpositionMin;
    public float destructUpYpositionMax;
    public float destructYPositionMax;
    public float destructYPositionMin;
    
    
    
    private float timeSinceLastSpawned;
    private int random;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate the destructibles front
        destructibleCollectsFront = new GameObject[destructibleCollectFrontPoolSize];
        for (int i = 0; i < destructibleCollectFrontPoolSize; i++)
        {
            destructibleCollectsFront[i] =
                (GameObject) Instantiate(destructibleCollectFrontPrefab, destructibleCollectFrontPoolPosition, Quaternion.identity);
        }
        
        //Instantiate the destructibles down
        destructibleCollectsDown = new GameObject[destructibleCollectDownPoolSize];
        for (int i = 0; i < destructibleCollectDownPoolSize; i++)
        {
            destructibleCollectsDown[i] =
                (GameObject) Instantiate(destructibleCollectDownPrefab, destructibleCollectDownPoolPosition, Quaternion.identity);
        }
        
        //Instantiate the destructibles up
        destructibleCollectsUp = new GameObject[destructibleCollectUpPoolSize];
        for (int i = 0; i < destructibleCollectUpPoolSize; i++)
        {
            destructibleCollectsUp[i] =
                (GameObject) Instantiate(destructibleCollectUpPrefab, destructibleCollectUpPoolPosition, Quaternion.identity);
        }
        
        //Instantiate the hazard blocks
        hazardBlocks = new GameObject[hazardBlockPoolSize];
        for (int i = 0; i < hazardBlockPoolSize; i++)
        {
            hazardBlocks[i] =
                (GameObject) Instantiate(hazardBlockPrefab, hazardBlockPoolPosition, Quaternion.identity);
        }
        
        //Instantiate the collects
        collects = new GameObject[collectsPoolSize];
        for (int i = 0; i < collectsPoolSize; i++)
        {
            collects[i] = (GameObject) Instantiate(collectPrefab, collectsPoolPosition, Quaternion.identity);
        }

        //thirdOfScreen = (Mathf.Abs(yPositionMin) + Mathf.Abs(yPositionMax)) - 0.5f / 3;
        
        thirdOfScreenHorizontal = (Mathf.Abs(horizontalYpositionMin) + Mathf.Abs(horizontalYpositionMax)) / 3;
        thirdOfScreenTilted = (Mathf.Abs(tiltedYpositionMin) + Mathf.Abs(tiltedYpositionMax)) / 3;
        thirdOfScreenVertical = (Mathf.Abs(verticalYpositionMin) + Mathf.Abs(verticalYpositionMax)) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            if (nextHazardSpawn <= 0)
            {
                nextHazardSpawn = Random.Range(minHazardSpawnRate, maxHazardSpawnRate);

                random = Random.Range(0, 100);

                if (random < destructibleFrontThreshold)
                {
                    SpawnDestructibleFront();
                }
                else if (random < destructibleDownThreshold)
                {
                    SpawnDestructibleDown();
                }
                else if (random < destructibleUpThreshold)
                {
                    SpawnDestructibleUp();
                }
                else if (random < hazardBlockThreshold)
                {
                    SpawnHazardBlock();
                }
                else if (random < twoHazardBlockThreshold)
                {
                    Spawn2HazardBlocks();
                }
                else if (random < wallThreshold)
                {
                    SpawnDestructibleWall();
                }
            }
            nextHazardSpawn -= Time.deltaTime;

            if (nextCollectSpawn <= 0)
            {
                nextCollectSpawn = Random.Range(minCollectSpawnRate, maxCollectSpawnRate);
                
                SpawnCollect();
            }

            nextCollectSpawn -= Time.deltaTime;

        }

    }

    private void SpawnDestructibleFront()
    {
        float spawnYPosition = Random.Range(destructYPositionMin, destructYPositionMax);
        destructibleCollectsFront[currentDestructibleFront].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentDestructibleFront++;
        if (currentDestructibleFront >= destructibleCollectFrontPoolSize)
        {
            currentDestructibleFront = 0;
        }
    }
    
    private void SpawnDestructibleDown()
    {
        float spawnYPosition = Random.Range(destructDownYpositionMin, destructYPositionMax);
        destructibleCollectsDown[currentDestructibleDown].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentDestructibleDown++;
        if (currentDestructibleDown >= destructibleCollectDownPoolSize)
        {
            currentDestructibleDown = 0;
        }
    }

    private void SpawnDestructibleUp()
    {
        float spawnYPosition = Random.Range(destructYPositionMin, destructUpYpositionMax);
        destructibleCollectsUp[currentDestructibleUp].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentDestructibleUp++;
        if (currentDestructibleUp >= destructibleCollectUpPoolSize)
        {
            currentDestructibleUp = 0;
        }
    }
    
    private void SpawnHazardBlock()
    {
        int degrees = randomFaceDirection();
        hazardBlocks[currentHazardBlock].transform.position = new Vector2(spawnXPosition, randomSpawnY(degrees));
        hazardBlocks[currentHazardBlock].transform.eulerAngles = Vector3.forward * degrees;
        currentHazardBlock++;
        if (currentHazardBlock >= hazardBlockPoolSize)
        {
            currentHazardBlock = 0;
        }
    }

    private void Spawn2HazardBlocks()
    {
        int degrees1 = randomFaceDirection();
        float randomY1 = randomSpawnY(degrees1);
        int degrees2 = randomFaceDirection();
        float randomY2 = randomSpawnY(degrees1, randomY1, degrees2);

        hazardBlocks[currentHazardBlock].transform.position = new Vector2(spawnXPosition, randomY1);
        hazardBlocks[currentHazardBlock].transform.eulerAngles = Vector3.forward * degrees1;
        currentHazardBlock++;
        if (currentHazardBlock >= hazardBlockPoolSize)
        {
            currentHazardBlock = 0;
        }
        
        hazardBlocks[currentHazardBlock].transform.position = new Vector2(spawnXPosition, randomY2);
        hazardBlocks[currentHazardBlock].transform.eulerAngles = Vector3.forward * degrees2;
        currentHazardBlock++;
        if (currentHazardBlock >= hazardBlockPoolSize)
        {
            currentHazardBlock = 0;
        }
        
    }

    private int randomFaceDirection()
    {
        int facing = Random.Range(0, 3);
        int toret = 0;
        switch (facing)
        {
            case 0:
                toret = 90;
                break;
            case 1:
                toret = 45;
                break;
            case 2:
                toret = 0;
                break;
            case 3:
                toret = -45;
                break;
            default:
                toret = 0;
                break;
        }
        return toret;
    }

    private float randomSpawnY(int degrees)
    {
        float toret = 0;
        if (degrees == 0)
        {
            toret = Random.Range(horizontalYpositionMin, horizontalYpositionMax);
        }
        else if (degrees == 45 || degrees == -45)
        {
            toret = Random.Range(tiltedYpositionMin, tiltedYpositionMax);
        }
        else
        {
            toret = Random.Range(verticalYpositionMin, verticalYpositionMax);
        }

        return toret;
    }

    private float randomSpawnY(int degrees1, float otherY, int degreesNew)
    {
        int partOfScreen = 0;
        
        if (degrees1 == 0)
        {
            if (otherY >= horizontalYpositionMin && otherY < (horizontalYpositionMin + thirdOfScreenHorizontal))
            {
                partOfScreen = 0;
            }
            else if ((otherY >= horizontalYpositionMin + thirdOfScreenHorizontal) && (otherY < (horizontalYpositionMin + thirdOfScreenHorizontal * 2)) )
            {
                partOfScreen = 1;
            }
            else
            {
                partOfScreen = 2;
            }
        }
        else if (degrees1 == 45 || degrees1 == -45)
        {
            if (otherY >= tiltedYpositionMin && otherY < (tiltedYpositionMin + thirdOfScreenTilted))
            {
                partOfScreen = 0;
            }
            else if ((otherY >= tiltedYpositionMin + thirdOfScreenTilted) && (otherY < (tiltedYpositionMin + thirdOfScreenTilted * 2)) )
            {
                partOfScreen = 1;
            }
            else
            {
                partOfScreen = 2;
            }
        }
        else
        {
            if (otherY >= verticalYpositionMin && otherY < (verticalYpositionMin + thirdOfScreenVertical))
            {
                partOfScreen = 0;
            }
            else if ((otherY >= verticalYpositionMin + thirdOfScreenVertical) && (otherY < (verticalYpositionMin + thirdOfScreenVertical * 2)) )
            {
                partOfScreen = 1;
            }
            else
            {
                partOfScreen = 2;
            }
        }
        
        
        float toret = 0;
        
        if (degreesNew == 0)
        {
            switch (partOfScreen)
            {
                case 0:
                    toret = Random.Range(horizontalYpositionMin + thirdOfScreenHorizontal, horizontalYpositionMax);
                    break;
                case 1:
                    int randomThirdOfScreen = Random.Range(0, 2);
                    if (randomThirdOfScreen == 0)
                    {
                        toret = Random.Range(horizontalYpositionMin, horizontalYpositionMin + thirdOfScreenHorizontal);
                    }
                    else
                    {
                        toret = Random.Range(horizontalYpositionMin + thirdOfScreenHorizontal * 2, horizontalYpositionMax);
                    }
                    break;
                case 2:
                    toret = Random.Range(horizontalYpositionMin, horizontalYpositionMin + thirdOfScreenHorizontal * 2);
                    break;
            }
        }
        else if (degreesNew == 45 || degreesNew == -45)
        {
            switch (partOfScreen)
            {
                case 0:
                    toret = Random.Range(tiltedYpositionMin + thirdOfScreenTilted, tiltedYpositionMax);
                    break;
                case 1:
                    int randomThirdOfScreen = Random.Range(0, 2);
                    if (randomThirdOfScreen == 0)
                    {
                        toret = Random.Range(tiltedYpositionMin, tiltedYpositionMin + thirdOfScreenTilted);
                    }
                    else
                    {
                        toret = Random.Range(tiltedYpositionMin + thirdOfScreenTilted * 2, tiltedYpositionMax);
                    }
                    break;
                case 2:
                    toret = Random.Range(tiltedYpositionMin, tiltedYpositionMin + thirdOfScreenTilted * 2);
                    break;
            }
        }
        else
        {
            switch (partOfScreen)
            {
                case 0:
                    toret = Random.Range(verticalYpositionMin + thirdOfScreenVertical, verticalYpositionMax);
                    break;
                case 1:
                    int randomThirdOfScreen = Random.Range(0, 2);
                    if (randomThirdOfScreen == 0)
                    {
                        toret = Random.Range(verticalYpositionMin, verticalYpositionMin + thirdOfScreenVertical);
                    }
                    else
                    {
                        toret = Random.Range(verticalYpositionMin + thirdOfScreenVertical * 2, verticalYpositionMax);
                    }
                    break;
                case 2:
                    toret = Random.Range(verticalYpositionMin, verticalYpositionMin + thirdOfScreenVertical * 2);
                    break;
            }
        }

        return toret;
    }

    private void SpawnDestructibleWall()
    {
        float spawnYPosition = destructYPositionMin;
        for (int i = 0; i <= 4; i++)
        {
            destructibleCollectsFront[currentDestructibleFront].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentDestructibleFront++;
            if (currentDestructibleFront >= destructibleCollectFrontPoolSize)
            {
                currentDestructibleFront = 0;
            }
            spawnYPosition+=1.9f;
        }

    }
    
    public float MinSpawnRate
    {
        get => minHazardSpawnRate;
        set => minHazardSpawnRate = value;
    }

    public float MaxSpawnRate
    {
        get => maxHazardSpawnRate;
        set => maxHazardSpawnRate = value;
    }

    private void SpawnCollect()
    {
        float spawnYPosition = Random.Range(horizontalYpositionMin, horizontalYpositionMax);
        collects[currentCollect].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentCollect++;
        if (currentCollect >= collectsPoolSize)
        {
            currentCollect = 0;
        }
    }
    
}
