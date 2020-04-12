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

    private GameObject m_spawnedEnemy;

    [SerializeField] private MapReader reader;

    private bool m_firstTimeCalled = true;
    private bool m_waveFinished = false;
    private int m_waveIndex = 0;
    
    [System.NonSerialized] public int amount;
    [System.NonSerialized] public Vector3 spawnTile;

    private bool finishedSpawning = false;

    private void Awake()
    {
        enemyPool = new GameObjectPool(10, SpawnManager.enemyType, 5, new GameObject("Enemy Parent").transform);
    }

    private void GetSpawnWaves()
    {
        amountPerWave = reader.spawnWaves;
    }

    /// <summary>
    /// Spawns enemies in waves read from the map text file
    /// </summary>
    /// <param name="spawnLocation"></param>
    public IEnumerator Spawn(Vector3 spawnLocation)
    {
        finishedSpawning = false;
        
        if (m_firstTimeCalled)
        {
            spawnTile = spawnLocation;
            GetSpawnWaves();
            m_firstTimeCalled = false;
        }

        amount = amountPerWave[m_waveIndex];
        
        for (int i = 0; i < amount; i++)
        {
            m_spawnedEnemy = GetEnemy();
            currentWave.Add(m_spawnedEnemy);
            m_spawnedEnemy.transform.position = spawnLocation;
            m_spawnedEnemy.SetActive(true);
            
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
                m_waveIndex++;
                killedEnemies = 0;
                currentWave.Clear();
                finishedSpawning = false;
                StartCoroutine(Spawn(spawnTile));
            }
        }
    }
}