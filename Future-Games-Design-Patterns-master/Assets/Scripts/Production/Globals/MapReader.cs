using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MapReader
{
    private string fileData;

    public List<string> ReadFile(TextAsset map)
    {
        string path = AssetDatabase.GetAssetPath(map);
        StreamReader reader = new StreamReader(path);

        List<String> testList = new List<string>();

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains("#"))
            {
                break;
            }

            testList.Add(line);
        }

        return testList;
    }
}

public class MapKeyData
{
    public MapKeyData(TileType type, GameObject prefab)
    {
        Type = type;
        Prefab = prefab;
    }
    
    public TileType Type { get; private set; }
    public GameObject Prefab { get; private set; }
}

public class EdMapReader
{
    private Dictionary<TileType, GameObject> prefabsById;

    public EdMapReader(IEnumerable<MapKeyData> mapKeyDatas)
    {
        prefabsById = new Dictionary<TileType, GameObject>();
        
        foreach (MapKeyData data in mapKeyDatas)
        {
            prefabsById.Add(data.Type, data.Prefab);
        }
    }

    public void ReadMap(char currentTileChar)
    {
        TileType tileType = TileMethods.TypeByIdChar[currentTileChar];
        GameObject currentPrefab = prefabsById[tileType];
        GameObject.Instantiate(currentPrefab);
    }
}