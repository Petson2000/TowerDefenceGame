using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConverter
{
    private TileType type;
    private readonly GameObject prefab;

    public ObjectConverter(GameObject Prefab)
    {
        prefab = Prefab;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
