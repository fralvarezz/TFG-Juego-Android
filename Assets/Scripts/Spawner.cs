using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    //Destructible Collectibles
    public int destructibleCollectPoolSize;
    public GameObject destructibleCollectPrefab;
    private GameObject[] destructibleCollects;
    private Vector2 destructibleCollectPoolPosition = new Vector2(-15f, -25f);
    private int currentDestructible = 0;
    public int destructibleThreshold;
    [SerializeField]
    private float destructibleCollectSpawnTimer;
    public float destructibleCollectSpawnRate;
    
    //Destructible Walls
    private float wallStartingY = -2.4594f;
    public int wallThreshold = 0;
    
    //Hazard Block
    public int hazardBlockPoolSize;
    public GameObject hazardBlockPrefab;
    private GameObject[] hazardBlocks;
    private Vector2 hazardBlockPoolPosition = new Vector2(-15f, -30f);
    private int currentHazardBlock = 0;
    public int hazardBlockThreshold;
    private float hazardBlockSpawnTimer;
    public float hazardBlockSpawnRate;
    
    //Needed variables
    public float min_spawnRate = 3f;
    public float max_spawnRate = 5f;
    private float nextSpawn = 1f;
    public float horizontalYpositionMin = -2.5f;
    public float tiltedYpositionMin = -1.75f;
    public float verticalYpositionMin = -2.5f;
    public float horizontalYpositionMax = 4.15f;
    public float tiltedYpositionMax = 0f;
    public float verticalYpositionMax = -2.5f;
    private float spawnXPosition = 10f;
    private float timeSinceLastSpawned;
    private int random;



    // Start is called before the first frame update
    void Start()
    {
        //Instantiate the destructibles
        destructibleCollects = new GameObject[destructibleCollectPoolSize];
        for (int i = 0; i < destructibleCollectPoolSize; i++)
        {
            destructibleCollects[i] =
                (GameObject) Instantiate(destructibleCollectPrefab, destructibleCollectPoolPosition, Quaternion.identity);
        }
        
        //Instantiate the hazard blocks
        hazardBlocks = new GameObject[hazardBlockPoolSize];
        for (int i = 0; i < hazardBlockPoolSize; i++)
        {
            hazardBlocks[i] =
                (GameObject) Instantiate(hazardBlockPrefab, hazardBlockPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            if (nextSpawn <= 0)
            {
                nextSpawn = Random.Range(min_spawnRate, max_spawnRate);

                random = Random.Range(0, 1000);

                if (random < destructibleThreshold)
                {
                    SpawnDestructible();
                }
                else if (random < hazardBlockThreshold)
                {
                    SpawnHazardBlock();
                }
                else if (random < wallThreshold)
                {
                    SpawnDestructibleWall();
                }
            }
        }

        nextSpawn -= Time.deltaTime;
    }

    private void SpawnDestructible()
    {
        float spawnYPosition = Random.Range(horizontalYpositionMin, horizontalYpositionMax);
        destructibleCollects[currentDestructible].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentDestructible++;
        if (currentDestructible >= destructibleCollectPoolSize)
        {
            currentDestructible = 0;
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

    private void SpawnDestructibleWall()
    {
        float spawnYPosition = wallStartingY;
        for (int i = 0; i <= 9; i++)
        {
            destructibleCollects[currentDestructible].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentDestructible++;
            if (currentDestructible >= destructibleCollectPoolSize)
            {
                currentDestructible = 0;
            }
            spawnYPosition++;
        }

    }
    
    public float MinSpawnRate
    {
        get => min_spawnRate;
        set => min_spawnRate = value;
    }

    public float MaxSpawnRate
    {
        get => max_spawnRate;
        set => max_spawnRate = value;
    }
    
}
