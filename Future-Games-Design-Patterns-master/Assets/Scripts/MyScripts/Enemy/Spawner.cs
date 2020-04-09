using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject spawnTile;

    private int instanceID = 0;
    
    public EnemySpawnManager SpawnManager;

    private GameObjectPool enemyPool;

    public List<GameObject> spawnedEnemies;

    private GameObject spawnedEnemy;

    private void Awake()
    {
        enemyPool = new GameObjectPool(10, SpawnManager.enemyType, 5, new GameObject("Enemy Parent").transform);
        spawnedEnemies = new List<GameObject>();
    }

    public IEnumerator Spawn(Vector3 spawnLocation)
    {
        //Add delay
        for (int i = 0; i < SpawnManager.spawnAmount; i++)
        {
            spawnedEnemy = enemyPool.Rent(false);
            
            spawnedEnemies.Add(spawnedEnemy);

            spawnedEnemy.transform.position = spawnLocation;
            
            spawnedEnemy.SetActive(true);
            
            yield return new WaitForSeconds(SpawnManager.spawnDelay);
        }
    }
}