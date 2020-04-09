using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;

    [System.NonSerialized]public List<GameObject> pools;

    void CreatePool()
    {
        pools = new List<GameObject>();
    }
}
