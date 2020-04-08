using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleCollectPool : MonoBehaviour
{

    public int destructibleCollectPoolSize = 5;
    public GameObject destructibleCollectPrefab;
    public float spawnRate = 4f;
    public float destructibleCollectMin = -5f;
    public float destructibleCollectMax = 3.5f;
    
    private GameObject[] destructibleCollects;
    private Vector2 objectPoolPosition = new Vector2(-15F, -25F);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentDestructible = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        destructibleCollects = new GameObject[destructibleCollectPoolSize];
        for (int i = 0; i < destructibleCollectPoolSize; i++)
        {
            destructibleCollects[i] =
                (GameObject) Instantiate(destructibleCollectPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (!GameControl.instance.gameOver && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(destructibleCollectMin, destructibleCollectMax);
            destructibleCollects[currentDestructible].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentDestructible++;
            if (currentDestructible >= destructibleCollectPoolSize)
            {
                currentDestructible = 0;
            }

        }
    }
}
