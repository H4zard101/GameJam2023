using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public List<GameObject> enemies;

    private float timeSinceLastSpawn = 0f;

    public float spawnInterval = 2f;

    private void Start()
    {
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            timeSinceLastSpawn = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 0.5f);

        System.Random rand = new System.Random();
        //const int iterations = 100;
        double range = (double)0.0f - (double)0.5f;        

        //for (int i = 0; i < iterations; i++)
        {
            double sample = rand.NextDouble();
            double scaled = (sample * range) + 0.0f;
            float f = (float)scaled;

            //Debug.Log("value: " + Math.Abs(f));
            if (Math.Abs(f) > 0.25f && Math.Abs(f) <= 0.5f)
            {
                Debug.LogWarning("Spawning : " + EnemyType.Rabbit);
                Instantiate(enemies[0], transform.position, transform.rotation);
            }
            else
            {
                Debug.LogWarning("Spawning Nothing");
            }
            // if (Math.Abs(f) > 0.15f && Math.Abs(f) <= 0.2f)
            // {
            //     Debug.LogWarning("Spawning : " + EnemyType.Beaver);
            //     //Instantiate(enemies[1], transform.position, transform.rotation);
            // }
            // if (Math.Abs(f) > 0.05f && Math.Abs(f) <= 0.15f)
            // {
            //     Debug.LogWarning("Spawning : " + EnemyType.Moles);
            //     //Instantiate(enemies[2], transform.position, transform.rotation);
            // }
            // if (Math.Abs(f) > 0.0f && Math.Abs(f) <= 0.05f)
            // {
            //     Debug.LogWarning("Spawning : " + EnemyType.Woodpecker);
            //     //Instantiate(enemies[3], transform.position, transform.rotation);
            // }
        }

    }
}
