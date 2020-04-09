using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Spawn Manager", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class EnemySpawnManager : ScriptableObject
{
    public int health;

    public int spawnAmount;

    public GameObject enemyType;

    public float spawnDelay; //Delay between enemy spawns
}
