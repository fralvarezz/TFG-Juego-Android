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
    public float spawnRate = 4f;
    public float YpositionMin = -5f;
    public float YpositionMax = 3.5f;
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
            timeSinceLastSpawned += Time.deltaTime;
            if (timeSinceLastSpawned > spawnRate)
            {
                timeSinceLastSpawned = 0;
            
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
    }

    private void SpawnDestructible()
    {
        float spawnYPosition = Random.Range(YpositionMin, YpositionMax);
        destructibleCollects[currentDestructible].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentDestructible++;
        if (currentDestructible >= destructibleCollectPoolSize)
        {
            currentDestructible = 0;
        }
    }
    
    private void SpawnHazardBlock()
    {
        float spawnYPosition = Random.Range(YpositionMin, YpositionMax);
        hazardBlocks[currentHazardBlock].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentHazardBlock++;
        if (currentHazardBlock >= hazardBlockPoolSize)
        {
            currentHazardBlock = 0;
        }
    }

    private void SpawnDestructibleWall()
    {
        float spawnYPosition = wallStartingY;
        //Debug.Log(spawnYPosition);
        for (int i = 0; i <= 9; i++)
        {
            Debug.Log(spawnYPosition);
            destructibleCollects[currentDestructible].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentDestructible++;
            if (currentDestructible >= destructibleCollectPoolSize)
            {
                currentDestructible = 0;
            }
            spawnYPosition++;
        }

    }
}
