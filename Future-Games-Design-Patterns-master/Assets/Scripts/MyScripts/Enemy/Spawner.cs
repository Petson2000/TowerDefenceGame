using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemySpawnManager SpawnManager;

    private GameObjectPool enemyPool;

    private List<GameObject> currentWave = new List<GameObject>();

    [System.NonSerialized]public int killedEnemies;
    
    private List<int> amountPerWave = new List<int>();

    private GameObject spawnedEnemy;

    [SerializeField] private MapReader reader;

    private bool firstTimeCalled = true;
    private bool waveFinished = false;
    private int waveIndex = 0;
    
    [System.NonSerialized] public int amount;
    [System.NonSerialized] public Vector3 spawnTile;

    private bool finishedSpawning = false;

    private void Awake()
    {
        enemyPool = new GameObjectPool(10, SpawnManager.enemyType, 5, new GameObject("Enemy Parent").transform);
    }

    void GetSpawnWaves()
    {
        amountPerWave = reader.spawnWaves;
    }

    public IEnumerator Spawn(Vector3 spawnLocation)
    {
        finishedSpawning = false;
        
        if (firstTimeCalled)
        {
            spawnTile = spawnLocation;
            GetSpawnWaves();
            firstTimeCalled = false;
        }

        amount = amountPerWave[waveIndex];
        
        for (int i = 0; i < amount; i++)
        {
            spawnedEnemy = GetEnemy();
            currentWave.Add(spawnedEnemy);
            spawnedEnemy.transform.position = spawnLocation;
            spawnedEnemy.SetActive(true);
            
            yield return new WaitForSeconds(.5f);
        }

        finishedSpawning = true;
    }
    GameObject GetEnemy()
    {
        return enemyPool.Rent(false);
    }

    private void Update()
    {
        if (finishedSpawning)
        {
            if (killedEnemies >= currentWave.Count)
            {
                waveIndex++;
                killedEnemies = 0;
                currentWave.Clear();
                finishedSpawning = false;
                StartCoroutine(Spawn(spawnTile));
            }
        }
    }
}