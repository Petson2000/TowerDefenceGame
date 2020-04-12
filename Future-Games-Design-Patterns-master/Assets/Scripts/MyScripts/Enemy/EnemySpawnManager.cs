using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Spawn Manager", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class EnemySpawnManager : ScriptableObject
{
    public int health;

    public int spawnAmount;

    public GameObject enemyType;

    public float spawnDelay; //Delay between enemy spawns
    
    public List<int> waveData = new List<int>();

    public void GetSpawnWaves()
    {
        MapReader reader = FindObjectOfType<MapReader>();
    }
    
    //Spawn waves according to this list
    //Add player damage
    //Done!
}
