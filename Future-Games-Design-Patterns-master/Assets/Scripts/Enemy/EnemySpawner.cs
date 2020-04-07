using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float delay;
    [SerializeField] private bool loop;

    private void Start()
    {
        GameObject spawnTile = GameObject.FindWithTag("Start");
        if (spawnTile != null)
        {
            Spawn(spawnTile.transform.position);
        }
    }


    public void Spawn(Vector3 spawnLocation)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        }

        if (loop)
        {
            Invoke("Spawn", delay);
        }
        
    }
}
